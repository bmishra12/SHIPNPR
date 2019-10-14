using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using ShiptalkWeb.Upload;
using ShiptalkLogic.BusinessLayer;
using ShiptalkWeb.Routing;
using System.Web.Routing;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.DataLayer;
using ShiptalkLogic;
using System.Xml;
using ShiptalkLogic.BusinessObjects.UI;





namespace ShiptalkWeb.Upload
{
    public partial class UploadFile : Page, IRouteDataPage, IAuthorize
    {
        #region Enumerations
        enum ProcessFeedBack { SUCCESS, WARNING, ERROR };
        enum ERRORS_FOUND
        {
            NONE,
            FILE_FORMAT,
            DATA,
            APPLICATION
        }


        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        #region Implementation of IAuthorize

        private AgencyBLL _logic;
        public AgencyBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new AgencyBLL();

                return _logic;
            }
        }

        public bool IsAuthorized()
        {
            //sammit: the authorized route is set to have isAdmin at any scope.
            return true;
        }

        #endregion

        #region structures

        struct StructStatus
        {
            /// <summary>
            /// Comment in reference to the status that was set,
            /// </summary>
            public string Comment;

            /// <summary>
            /// Category of the Error that was found.
            /// </summary>
            public ERRORS_FOUND ErrorType;

            /// <summary>
            /// Id of the status
            /// </summary>
            public int StatusId;
        }

        #endregion


        #region Private Members
        private StringBuilder _Errors = null;
        /// <summary>
        /// Contains Errors that are due to validation processing of file content.
        /// </summary>
        private List<string> _ProcessErrors = null;

        /// <summary>
        /// Contains errors due to original format of the file 
        /// </summary>
        private List<string> _InvalidFormatErrors = null;

        private List<string> _GUIErrors = null;

        /// <summary>
        /// Specifies the confirguration the code is running in. Affects upload directory.
        /// </summary>
        private string _CurrentEnvironment = string.Empty;

        /// <summary>
        /// Path to the error file.
        /// </summary>
        string ErrorFilePath;

        /// <summary>
        /// Path to clean file
        /// </summary>
        string CleanFilePath;

        /// <summary>
        /// Original File Path.
        /// </summary>
        string OriginalFilePath;



        int _InsertCount = 0;
        int _RecordCount = 0;
        int UserId = -1;
        PamFileUpLoadBLL _pam;
        CCFileUploadBLL _cc;
        List<PamProperty> _ErrorList = null;

        #endregion




        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            //Initialize this class.
            Initialize();
            if (!IsPostBack)
            {

                btnValidate.OnClientClick = "javascript:disableButtons();";
                btnProcess.OnClientClick = "javascript:disableButtons();";
                btnViewRecentUploads.OnClientClick = "javascript:disableButtons();";

                btnProcess.Visible = false;
            }


        }

        /// <summary>
        /// Initialize the class members.
        /// </summary>
        private void Initialize()
        {
            bool IsAdmin = AccountInfo.IsAdmin; //access the accountInfo object in initialize..

            pnlContent.BorderWidth = Unit.Pixel(0);

  
            //Get the environment this is running in ex DEV or PROD
            _CurrentEnvironment = ConfigurationManager.AppSettings["Env"].ToString();

            _Errors = new StringBuilder();
            _ErrorList = new List<PamProperty>();
            _ProcessErrors = new List<string>();
            _InvalidFormatErrors = new List<string>();
            _GUIErrors = new List<string>();

            //Environment defaults to DEV enviornment if not available.
            if (string.IsNullOrEmpty(_CurrentEnvironment))
            {
                _Errors.Append("<div>Configuration error: undetermined executable environment,defaulting to dev</div>");
                _CurrentEnvironment = "DEV";
            }
        }
        #endregion
        #region properties

        public string UploadDirectoryPam
        {
            get
            {
                string mainDirectory = null;

                if (_CurrentEnvironment == "PROD")
                {
                    mainDirectory = ConfigurationManager.AppSettings["PRODUploadFilepathPAM"];
                }
                else
                {
                    mainDirectory = ConfigurationManager.AppSettings["DevUploadFilepathPAM"];
                }

                mainDirectory = mainDirectory.Trim();
                if (!Path.IsPathRooted(mainDirectory))
                {
                    string appPath = HttpContext.Current.Server.MapPath("~/");
                    mainDirectory = Path.Combine(appPath, mainDirectory);
                }
                return mainDirectory;
            }
        }

        public string UploadDirectoryCC
        {
            get
            {
                string mainDirectory = null;

                if (_CurrentEnvironment == "PROD")
                {
                    mainDirectory = ConfigurationManager.AppSettings["PRODUploadFilepathCC"];
                }
                else
                {
                    mainDirectory = ConfigurationManager.AppSettings["DevUploadFilepathCC"];
                }

                mainDirectory = mainDirectory.Trim();
                if (!Path.IsPathRooted(mainDirectory))
                {
                    string appPath = HttpContext.Current.Server.MapPath("~/");
                    mainDirectory = Path.Combine(appPath, mainDirectory);
                }

                return mainDirectory;
            }
        }




        #endregion

        #region Helper Methods

        private Boolean ProcessSomeFile(ref StreamReader OriginalFileHandler, string FileName, int UploadId, string FieldCountConfigValue, ILoader ValidationLoader)
        {
            string LineRead = null;
            string AllErrors = string.Empty;
            string AllErrorsInALoop = string.Empty;

            int InvalidKount = 0;
            //Insert status Validation Completed

            UpLoadStatusManager.AddUploadStatus("VALIDATION STARTED", "validation started at " + DateTime.Now, UploadId);
            //Continue reading/processing lines from the file 
            //while there is data to be read.
            bool bFileErrorFound = false;
            StreamWriter ErrorFileHandler = null;
            StreamWriter CleanFileHandler = null;
            string ErrorCaptured = string.Empty;
            try
            {
                try
                {
                    ErrorFileHandler = new StreamWriter(ErrorFilePath, false);
                    CleanFileHandler = File.CreateText(CleanFilePath);
                }
                catch (System.Exception exFileAccess)
                {
                    lblFeedBack.InnerText = exFileAccess.InnerException.Message + " " + exFileAccess.StackTrace;
                    lblFeedBack.Visible = true;
                    btnProcess.Visible = false;
                    return false;
                }
                Dictionary<string, string> Dict = new Dictionary<string, string>();
                while (OriginalFileHandler.Peek() >= 0)
                {
                    //Read Line from file
                    LineRead = OriginalFileHandler.ReadLine();


                    //Check for correct number of columns in file uploaded.
                    bool IsValidFieldNumber = false;
                    string[] Fields = ParseFields(LineRead, FieldCountConfigValue, ref IsValidFieldNumber);


                    if (!IsValidFieldNumber)
                    {
                        //Record the Error that was found.
                        if (Fields.Length > 1)
                            lblStatus.InnerHtml = "Error: Invalid number of fields in file - record " + Fields[2];
                        else
                            lblStatus.InnerHtml = "Error: Invalid number of fields in file ";

                        lblStatus.Visible = true;
                        //Do not continue processing at this point.
                        UpLoadStatusManager.AddUploadStatus("INVALID FILE FOUND", "File contain invalid number of fields Record", UploadId);
                        btnProcess.Visible = false;
                        return false;
                    }

                    // increment record count 
                    _RecordCount++;

                    //Parse record and values for validation
                    ValidationLoader.Initialize();
                    if (AccountInfo.IsAdmin)
                    {
                        //Update the state of the upload with the state that is in the file.
                        //Admins can upload multiple states.  It may not be the default state of the admin account.
                        try
                        {
                            if (Fields[1].Length != 2)
                            {
                                lblStatus.InnerHtml = "Invalid State fips Code found in file upload.";
                                lblStatus.Visible = true;
                                btnProcess.Visible = false;
                                return false;
                            }
                            FileUploadDAL.UpdateBatchUploadState(UploadId, Fields[1]);
                        }
                        catch (System.Exception exFipsCode)
                        {
                            lblStatus.InnerHtml = "Error: " + exFipsCode.Message + " upload validation execution halted.";

                            lblStatus.Visible = true;
                            btnProcess.Visible = false;
                            //Do not continue processing at this point.
                            UpLoadStatusManager.AddUploadStatus("INVALID FILE FOUND", "File contain invalid StateFIPS code", UploadId);
                            return false;
                        }

                        //of the admin account.
                        ValidationLoader.Load(Fields, "Admin", AccountInfo.UserId);
                    }
                    else
                    {

                        ValidationLoader.Load(Fields, AccountInfo.StateFIPS, AccountInfo.UserId);
                    }

                    try
                    {
                        if (ValidationLoader is CCFileUploadBLL)
                        {
                            StringBuilder NewKey = new StringBuilder();

                            //Agency Code
                            NewKey.Append(ValidationLoader.ParsedFields[13]);

                            //State FIPS Code
                            NewKey.Append(ValidationLoader.ParsedFields[2]);


                            //State Specific Client ID
                            NewKey.Append(ValidationLoader.ParsedFields[3]);

                            try
                            {
                                Dict.Add(NewKey.ToString(), ValidationLoader.ParsedFields[2]);
                            }
                            catch (System.Exception exArg)
                            {
                                //Duplicate record found.
                                AllErrorsInALoop = AllErrorsInALoop + "<div>Record:" + ValidationLoader.ParsedFields[2] + " is a duplicate record </div>";
                                AllErrors = AllErrors + AllErrorsInALoop;
                            }

                        }

                        //If we are deleting we do not validate.
                        if (Fields[0] != "D")
                        {
                            ErrorCaptured = ValidationLoader.Validate();
                            AllErrorsInALoop = AllErrorsInALoop + ErrorCaptured;
                            AllErrors = AllErrors + AllErrorsInALoop;
                        }
                        if (AllErrorsInALoop != string.Empty)
                        {
                            bFileErrorFound = true;
                            //Write Error record to errofile
                            ErrorFileHandler.WriteLine("---------------");
                            ErrorFileHandler.WriteLine(string.Empty);
                            ErrorFileHandler.WriteLine(LineRead);
                            ErrorFileHandler.WriteLine(string.Empty);

                            //Normalize HTML so it has a root tag and can be parsed to get errors text
                            string ErrorsXmlDoc = "<content>" + ErrorCaptured + "</content>";
                            XmlDocument doc = new XmlDocument();
                            StringReader sr = new StringReader(ErrorsXmlDoc);
                            doc.Load(sr);
                            XmlNodeList ErrorList = doc.SelectNodes("content/div");

                            //Write Errors to error file
                            foreach (XmlNode ErrorValue in ErrorList)
                            {
                                ErrorFileHandler.WriteLine(ErrorValue.InnerText);
                            }
                            ErrorFileHandler.WriteLine(string.Empty);
                        }

                    }
                    catch (ApplicationException exApp)
                    {
                        lblFeedBack.InnerHtml = exApp.Message + " " + exApp.StackTrace;
                        btnProcess.Visible = false;
                    }

                    if (string.IsNullOrEmpty(AllErrorsInALoop))
                    {
                        _InsertCount++;

                        //Create a new client contact object to save the data.
                        CleanFileHandler.WriteLine(LineRead);
                    }
                    else
                    {
                        btnDownload.Visible = true;
                        bFileErrorFound = true;
                    }


                    AllErrorsInALoop = string.Empty;
                }// end loop 


                //OriginalFileHandler.Close();
                //ErrorFileHandler.Close();
                InvalidKount = _RecordCount - _InsertCount;
                UpLoadStatusManager.AddUploadStatus("ERROR FOUND", _InsertCount.ToString() + " of " + _RecordCount.ToString() + " records passed validation", UploadId);
                pnlContent.BorderWidth = Unit.Pixel(1);
            }
            catch (System.Exception ex)
            {
                lblFeedBack.InnerText = "Application Error: " + ex.Message + " " + ex.StackTrace;
                btnProcess.Visible = false;

            }

            finally
            {
                ErrorFileHandler.Close();
                CleanFileHandler.Close();
                OriginalFileHandler.Close();
            }


            if (bFileErrorFound)
            {
                divbtn.Style.Remove("right");
                divbtn.Style.Add("right", "50px");
                ErrorCaptured = "<error>" + AllErrors + "</error>";
                btnProcess.Visible = false;
                lblValidation.InnerHtml = "<div><font style='font-Color:Red'>Validation Output</font><br/><br/></div> " + AllErrors;
                lblValidation.Visible = true;
                if (_InsertCount > 0)
                    lblStatus.InnerHtml = "<strong> " + InvalidKount.ToString() + "<strong>  out of  a total of " + _RecordCount.ToString() + " records did not pass the validation checks. <br/>" + _InsertCount.ToString() + " out of " + _RecordCount.ToString() + " records did pass validation checks." + "<br/>No records have been uploaded at this time. Please fix all the errors and process again. </strong>";
                else
                    lblStatus.InnerHtml = "<strong> " + InvalidKount.ToString() + "<strong>  out of  a total of " + _RecordCount.ToString() + " records did not pass the validation checks. <br/> No records have been uploaded at this time, fix the error(s) and process it again. </strong>";

                lblStatus.Visible = true;

            }
            else
            {
                btnDownload.Visible = false;
                btnProcess.Enabled = true;
                UpLoadStatusManager.AddUploadStatus("SUCCESSFUL", "validation successful at " + DateTime.Now, UploadId);
               // btnProcess.Visible = true;

                //Commented by Lavanya: 06/27/2013
                //lblStatus.InnerHtml = "<strong>Records were successfully validated with no errors.</strong>";
                //lblStatus.Visible = true;             
            }


            grdUploadStatus.Visible = false;
            //Update Records Processed info for file Upload

            UpLoadStatusManager.UpdateFileUploadsRecordsProcessed(UploadId, InvalidKount, _InsertCount);
            if (InvalidKount > 0)
            {
                bFileErrorFound = false;
            }
            else
            {
                bFileErrorFound = true;
            }

            return bFileErrorFound;
        }



        private void WriteErrosToFile(StreamWriter ErrorFileHandler, string ErrorCaptured, string OriginalRecord)
        {

            //Normalize HTML so it has a root tag and can be parsed to get errors text
            string ErrorsXmlDoc = "<content>" + ErrorCaptured + "</content>";
            XmlDocument doc = new XmlDocument();
            StringReader sr = new StringReader(ErrorsXmlDoc);
            doc.Load(sr);
            XmlNodeList ErrorList = doc.SelectNodes("content/div");

            string CurrentRecNum = string.Empty;
            string PrevRecNum = string.Empty;
            //Write Errors to error file
            foreach (XmlNode ErrorValue in ErrorList)
            {
                char[] sep = { ' ' };
                string[] RecNumExtract = ErrorValue.InnerText.Split(sep);
                string RecNum = RecNumExtract[1];

                if (PrevRecNum != RecNum)
                {
                    //Write Error record to errofile
                    ErrorFileHandler.WriteLine("---------------");
                    ErrorFileHandler.WriteLine(string.Empty);
                    ErrorFileHandler.WriteLine(OriginalRecord);
                    ErrorFileHandler.WriteLine(string.Empty);
                    PrevRecNum = RecNum;

                }

                ErrorFileHandler.WriteLine(ErrorValue.InnerText);
            }
            ErrorFileHandler.WriteLine(string.Empty);
        }



        /// <summary>
        /// Checks to see if file is a valid size.
        /// </summary>
        /// <returns></returns>
        private bool IsFileSizeValid()
        {
            //Max File Size 
            int MegBytesAllowed = int.Parse(ConfigurationManager.AppSettings["MaxUploadSize"]);
            int SizeInBytes = 1048576 * MegBytesAllowed;
            if (FileUpload1.PostedFile.ContentLength > SizeInBytes)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Check to see if Uploaded file has correct number of fields.
        /// </summary>
        /// <param name="FileLine"></param>
        /// <param name="FileType"></param>
        /// <returns>true or false</returns>
        /// <exception cref="ConfigurationException"></exception>
        private string[] ParseFields(string FileLine, string ConfigCountKey, ref bool IsNumberFieldsValid)
        {
            char[] sep = { '\t' };
            string[] Fields = FileLine.Split(sep);
            int RequredFieldCount = -1;
            if (int.TryParse(ConfigurationManager.AppSettings[ConfigCountKey].ToString(), out RequredFieldCount))
            {
                if (Fields.Length == RequredFieldCount)
                {
                    IsNumberFieldsValid = true;
                }
                else
                {
                    IsNumberFieldsValid = false;
                }
            }
            else
            {
                //Scenario - PamFieldCount which contains the number of allowed 
                //fields for a pam file cound not be found.
                throw (new ConfigurationException("Missing configuration - PamFieldCount system setup"));
            }
            return Fields;
        }


        /// <summary>
        /// Checks to see if the file name starts with PAM_ OR CC_  and returns string 
        /// indicating type.  If invalid name has been used it returns a blank for file type.
        /// </summary>
        /// <returns>Extension or blank if invalid file has been found.</returns>
        private string GetFileUploadType()
        {
            if (FileUpload1.FileName.StartsWith("PAM_"))
            {
                return "PAM";
            }

            if (FileUpload1.FileName.StartsWith("CC_"))
            {
                return "CC";
            }

            return string.Empty;

        }

        private void SaveToErrorFile(StructStatus CurrentStatus)
        {

        }


        /// <summary>
        /// Determines if extension is a valid, only accepts .TXT files
        /// </summary>
        /// <param name="FileExtension">3 digit extension</param>
        /// <returns>true or false</returns>
        private bool IsExtensionValid(string FileName)
        {
            char[] sep = { '.' };
            string[] FileExtension = FileName.Split(sep);
            if (FileExtension.Length <= 1)
            {
                return false;
            }
            else
            {
                //Check for valid file extension
                if (FileExtension[1].ToUpper() == "TXT")
                {
                    return true;
                }
                else
                {
                    //Secenario - Error has occured because the extension was not  PAM or CC extension
                    return false;
                }
            }
        }


        /// <summary>
        /// Displays Error to the user.
        /// </summary>
        private void ShowError(ERRORS_FOUND ErrDisplay)
        {
            List<string> ErrorContainer = null;
            switch ((int)ErrDisplay)
            {
                case (int)ERRORS_FOUND.APPLICATION:
                    ErrorContainer = _GUIErrors;
                    break;
                case (int)ERRORS_FOUND.FILE_FORMAT:
                    ErrorContainer = _InvalidFormatErrors;
                    break;
                default:
                    throw (new ApplicationException("ShowError: Error not supported, error type  " + ErrDisplay.ToString()));

            }


            foreach (string FormatError in ErrorContainer)
            {
                lblFeedBack.InnerHtml += FormatError;
            }

        }

        /// <summary>
        /// Insert Client contact record
        /// </summary>
        /// <param name="Fields"></param>
        private void InsertClientContact(string[] Fields)
        {
            string FieldValue = string.Empty;
            string LineRead = string.Empty;
            char[] RecordSeparator = { '\t' };

            ClientContact CCData = new ClientContact();

            CCData.Action = Fields[0];
            CCData.AgencyCode = Fields[13];
            CCData.AgencyState = new State(Fields[1]);
            CCData.AgencyId = LookupBLL.GetAgencyID(Fields[13], Fields[1]);
            CCData.BatchStateUniqueID = Fields[2];

            //Last Name
            CCData.ClientLastName = Fields[5];
            //First Name
            CCData.ClientFirstName = Fields[4];

            //County
            CCData.ClientCountyCode = Fields[11];

            //Comments
            CCData.Comments = Fields[125];

            CCData.Counselor = UserBLL.GetUserProfile(int.Parse(Fields[12]));

            //Counselor County Code
            CCData.CounselorCountyCode = Fields[14];

            //Counselor UserId
            CCData.CounselorUserId = int.Parse(Fields[12]);

            //Counselor ZIP Code
            CCData.CounselorZIPCode = Fields[15];

            //CreatedBy
            CCData.CreatedBy = AccountInfo.UserId;

            //CreatedDate 
            CCData.CreatedDate = DateTime.Now;

            //Date of contact
            CCData.DateOfContact = Convert.ToDateTime(Fields[17]);

            //IsBatchUploadData  **** Is Available but is read only.
            CCData.IsBatchUploadData = true;

            //ClientZIPCode
            CCData.ClientZIPCode = Fields[10];

            //State Specific Client ID
            CCData.StateSpecificClientId = Fields[3];

            //Get the correct client ID

            CCData.ClientId = CCFileUploadBLL.GetAssignedClientID(CCData.StateSpecificClientId, CCData.AgencyCode, CCData.AgencyState.Code);
            CCData.ClientId = CCData.ClientId.Trim();

            string Hours = Fields[102];
            string Minutes = Fields[103];

            //Hours Spent
            HourMinuteCalc.CalcHours(ref Hours, ref Minutes, false);
            CCData.HoursSpent = int.Parse(Hours);


            //Minutes Spent 
            CCData.MinutesSpent = int.Parse(Minutes);

            CCData.ClientPhoneNumber = Fields[6];

            CCData.RepresentativeFirstName = Fields[4];
            CCData.RepresentativeLastName = Fields[5];


            //OTHER TOPIC
            CCData.OtherDrugTopics = new List<Topic_OTHER>();
            if (!string.IsNullOrEmpty(Fields[101]))
            {
                CCData.OtherDrugTopicsSpecified = Fields[101];
                CCData.OtherDrugTopics.Add(Topic_OTHER.Other);
            }

            //Topic Other Prescription Assistance
            CCData.OtherPrescriptionAssistanceTopics = new List<Topic_OtherPrescriptionAssistance>();
            if (!string.IsNullOrEmpty(Fields[62]))
            {
                CCData.OtherPrescriptionAssitanceSpecified = Fields[62];
                CCData.OtherPrescriptionAssistanceTopics.Add(Topic_OtherPrescriptionAssistance.Other);
            }



            //Client Status
            switch (int.Parse(Fields[104]))
            {
                case (int)ClientStatus.DetailedAssistanceFullyCompleted:
                    CCData.ClientStatus = ClientStatus.DetailedAssistanceFullyCompleted;
                    break;

                case (int)ClientStatus.DetailedAssistanceInProgress:
                    CCData.ClientStatus = ClientStatus.DetailedAssistanceInProgress;
                    break;

                case (int)ClientStatus.GeneralInformationAndReferral:
                    CCData.ClientStatus = ClientStatus.GeneralInformationAndReferral;
                    break;
                case (int)ClientStatus.ProblemSolvingOrProblemResolutionFullyCompleted:
                    CCData.ClientStatus = ClientStatus.ProblemSolvingOrProblemResolutionFullyCompleted;
                    break;
                case (int)ClientStatus.ProblemSolvingOrProblemResolutionInProgress:
                    CCData.ClientStatus = ClientStatus.ProblemSolvingOrProblemResolutionInProgress;
                    break;
                default:
                    throw (new ApplicationException("Invalid Client Status found."));
            }

            //Client Age Group
            switch (int.Parse(Fields[19]))
            {
                case 1:
                    CCData.ClientAgeGroup = ClientAgeGroup.Age64OrYounger;
                    break;
                case 2:
                    CCData.ClientAgeGroup = ClientAgeGroup.Age65To74;
                    break;
                case 3:
                    CCData.ClientAgeGroup = ClientAgeGroup.Age75To84;
                    break;
                case 4:
                    CCData.ClientAgeGroup = ClientAgeGroup.Age85OrOlder;
                    break;
                case 9:
                    CCData.ClientAgeGroup = ClientAgeGroup.NotCollected;
                    break;
                default:
                    throw (new ApplicationException("Invalid Client Age Group found."));

            }

            //Client Assests
            switch (int.Parse(Fields[40]))
            {
                case 1:
                    CCData.ClientAssets = ClientAssets.BelowLISAssetLimits;
                    break;
                case 2:
                    CCData.ClientAssets = ClientAssets.AboveLISAssetLimits;
                    break;
                case 9:
                    CCData.ClientAssets = ClientAssets.NotCollected;
                    break;
                default:
                    throw (new ApplicationException("Invalid Client Assest found."));
            }
            //Client Dual Eligibility
            switch (int.Parse(Fields[42]))
            {
                case 1:
                    CCData.ClientDualEligble = ClientDualEligble.Yes;
                    break;
                case 2:
                    CCData.ClientDualEligble = ClientDualEligble.No;
                    break;
                case 9:
                    CCData.ClientDualEligble = ClientDualEligble.NotCollected;
                    break;
                default:
                    throw (new ApplicationException("Invalid Client Dual Eligibility found."));
            }

            //ClientFirstVsContinuingContact
            switch (int.Parse(Fields[18]))
            {
                case 1:
                    CCData.ClientFirstVsContinuingContact = ClientFirstVsContinuingContact.FirstContactForIssue;
                    break;
                case 2:
                    CCData.ClientFirstVsContinuingContact = ClientFirstVsContinuingContact.ContinuingContactsForIssue;
                    break;
                default:
                    throw (new ApplicationException("Invalid Client First Vs Continuing Contact found."));
            }

            //Client Gender
            switch (int.Parse(Fields[20]))
            {
                case 1:
                    CCData.ClientGender = ClientGender.Female;
                    break;
                case 2:
                    CCData.ClientGender = ClientGender.Male;
                    break;
                case 3:
                    //TODO: Add Transgender lookup value
                    break;
                case 9:
                    CCData.ClientGender = ClientGender.NotCollected;
                    break;
                default:
                    throw (new ApplicationException("Invalid Client Gender found."));

            }




            //ClientLearnedAboutSHIP
            switch (int.Parse(Fields[9]))
            {
                case 1:
                    CCData.ClientLearnedAboutSHIP = ClientLearnedAboutSHIP.PreviousContact;
                    break;
                case 2:
                    CCData.ClientLearnedAboutSHIP = ClientLearnedAboutSHIP.CMSorMedicare;
                    break;
                case 3:
                    CCData.ClientLearnedAboutSHIP = ClientLearnedAboutSHIP.Presentations;
                    break;
                case 4:
                    CCData.ClientLearnedAboutSHIP = ClientLearnedAboutSHIP.Mailings;
                    break;
                case 5:
                    CCData.ClientLearnedAboutSHIP = ClientLearnedAboutSHIP.Agency;
                    break;
                case 6:
                    CCData.ClientLearnedAboutSHIP = ClientLearnedAboutSHIP.FriendOrRelative;
                    break;
                case 7:
                    CCData.ClientLearnedAboutSHIP = ClientLearnedAboutSHIP.Media;
                    break;
                case 8:
                    CCData.ClientLearnedAboutSHIP = ClientLearnedAboutSHIP.StateWebsite;
                    break;
                case 9:
                    CCData.ClientLearnedAboutSHIP = ClientLearnedAboutSHIP.Other;
                    break;
                case 99:
                    CCData.ClientLearnedAboutSHIP = ClientLearnedAboutSHIP.NotCollected;
                    break;
                default:
                    throw (new ApplicationException("Invalid How Client Learned About SHIP found."));

            }

            //ClientMethodOfContact 
            switch (int.Parse(Fields[16]))
            {
                case 1:
                    CCData.ClientMethodOfContact = ClientMethodOfContact.PhoneCall;
                    break;
                case 2:
                    CCData.ClientMethodOfContact = ClientMethodOfContact.FaceToFaceAtCounselingLocationOrEventSite;
                    break;
                case 3:
                    CCData.ClientMethodOfContact = ClientMethodOfContact.FaceToFaceAtClientHomeOrFacility;
                    break;
                case 4:
                    CCData.ClientMethodOfContact = ClientMethodOfContact.Email;
                    break;
                case 5:
                    CCData.ClientMethodOfContact = ClientMethodOfContact.PostalMailOrFax;
                    break;
                default:
                    throw (new ApplicationException("Invalid Client Method Of Contact  found."));

            }

            //ClientMonthlyIncome 
            switch (int.Parse(Fields[39]))
            {
                case 1:
                    CCData.ClientMonthlyIncome = ClientMonthlyIncome.Below150PercentFPL;
                    break;
                case 2:
                    CCData.ClientMonthlyIncome = ClientMonthlyIncome.AtOrAbove150PercentFPL;
                    break;
                case 9:
                    CCData.ClientMonthlyIncome = ClientMonthlyIncome.NotCollected;
                    break;
                default:
                    throw (new ApplicationException("Invalid Client Monthly Income found."));
            }

            //Client Primary Language Other Than English
            switch (int.Parse(Fields[38]))
            {
                case 1:
                    CCData.ClientPrimaryLanguageOtherThanEnglish = ClientPrimaryLanguageOtherThanEnglish.PrimaryLanguageOtherThanEnglish;
                    break;
                case 2:
                    CCData.ClientPrimaryLanguageOtherThanEnglish = ClientPrimaryLanguageOtherThanEnglish.EnglishIsClientPrimaryLanguage;
                    break;
                case 9:
                    CCData.ClientPrimaryLanguageOtherThanEnglish = ClientPrimaryLanguageOtherThanEnglish.NotCollected;
                    break;
                default:
                    throw (new ApplicationException("Invalid Client Primary Language Other Than English found."));


            }

            //Get Ethnicity information
            //Hispanic
            bool bEthnicitySelected = false;
            CCData.ClientRaceDescriptions = new List<ClientRaceDescription>();
            if (IsBooleanValueTrue(Fields[21].ToUpper()))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.Hispanic_Latino_SpanishOrigin);
                bEthnicitySelected = true;
            }


            //White Non Hispanic
            if (IsBooleanValueTrue(Fields[22].ToUpper()))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.White_NonHispanic);
                bEthnicitySelected = true;
            }

            //Black African American
            if (IsBooleanValueTrue(Fields[23]))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.Black_AfricanAmerican);
                bEthnicitySelected = true;
            }

            //American Indian Alaskan Native
            if (IsBooleanValueTrue(Fields[24]))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.AmericanIndianOrAlaskaNative);
                bEthnicitySelected = true;
            }

            //Asian Indian
            if (IsBooleanValueTrue(Fields[25]))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.AsianIndian);
                bEthnicitySelected = true;
            }

            //Chinese
            if (IsBooleanValueTrue(Fields[26]))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.Chinese);
                bEthnicitySelected = true;
            }

            //Filipino
            if (IsBooleanValueTrue(Fields[27]))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.Filipino);
                bEthnicitySelected = true;
            }

            //Japanese
            if (IsBooleanValueTrue(Fields[28]))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.Japanese);
                bEthnicitySelected = true;
            }

            //Korean
            if (IsBooleanValueTrue(Fields[29]))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.Korean);
                bEthnicitySelected = true;
            }


            //Vietnamese
            if (IsBooleanValueTrue(Fields[30]))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.Vietnamese);
                bEthnicitySelected = true;
            }

            //Native Hawiian
            if (IsBooleanValueTrue(Fields[31]))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.NativeHawaiian);
                bEthnicitySelected = true;
            }

            //Guamanian or Chamorro
            if (IsBooleanValueTrue(Fields[32]))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.GuamanianOrChamorro);
                bEthnicitySelected = true;
            }

            //Somoan
            if (IsBooleanValueTrue(Fields[33]))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.Samoan);
                bEthnicitySelected = true;
            }

            //Other Asian
            if (IsBooleanValueTrue(Fields[34]))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.OtherAsian);
                bEthnicitySelected = true;
            }

            //Other Pacific Islander
            if (IsBooleanValueTrue(Fields[35]))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.OtherPacificIslander);
                bEthnicitySelected = true;
            }

            //Some Other Race or Ethnicity
            if (IsBooleanValueTrue(Fields[36]))
            {
                CCData.ClientRaceDescriptions.Add(ClientRaceDescription.SomeOtherRace);
                bEthnicitySelected = true;
            }

            //Not Collected
            if (IsBooleanValueTrue(Fields[37]))
            {
                if (!bEthnicitySelected)
                    CCData.ClientRaceDescriptions.Add(ClientRaceDescription.NotCollected);

            }



            //Client Receiving SS Or Medicare Disability
            switch (int.Parse(Fields[41]))
            {
                case 1:
                    CCData.ClientReceivingSSOrMedicareDisability = ClientReceivingSSOrMedicareDisability.Yes;
                    break;
                case 2:
                    CCData.ClientReceivingSSOrMedicareDisability = ClientReceivingSSOrMedicareDisability.No;
                    break;
                case 9:
                    CCData.ClientReceivingSSOrMedicareDisability = ClientReceivingSSOrMedicareDisability.NotCollected;
                    break;
                    throw (new ApplicationException("Invalid Client Receiving Social Security Or Medicare Disability."));

            }


            //Client Status
            switch (int.Parse(Fields[104]))
            {
                case 1:
                    CCData.ClientStatus = ClientStatus.GeneralInformationAndReferral;
                    break;
                case 2:
                    CCData.ClientStatus = ClientStatus.DetailedAssistanceInProgress;
                    break;
                case 3:
                    CCData.ClientStatus = ClientStatus.DetailedAssistanceFullyCompleted;
                    break;
                case 4:
                    CCData.ClientStatus = ClientStatus.ProblemSolvingOrProblemResolutionInProgress;
                    break;
                case 5:
                    CCData.ClientStatus = ClientStatus.ProblemSolvingOrProblemResolutionFullyCompleted;
                    break;
                default:
                    throw (new ApplicationException("Invalid Problem Solving Or Problem Resolution Fully Completed found."));

            }






            //MedicaidTopics 
            int MedicaidStartingIndex = 88;
            CCData.MedicaidTopics = new List<Topic_MEDICAID>();
            for (int iMedicaidTopic = 0; iMedicaidTopic < 6; iMedicaidTopic++)
            {
                if (!string.IsNullOrEmpty(Fields[MedicaidStartingIndex + iMedicaidTopic]))
                {
                    FieldValue = Fields[MedicaidStartingIndex + iMedicaidTopic];
                    if (IsBooleanValueTrue(FieldValue))
                    {
                        int SelectedTopic = MedicaidStartingIndex + iMedicaidTopic;
                        switch (SelectedTopic)
                        {
                            case 88:
                                CCData.MedicaidTopics.Add(Topic_MEDICAID.MedicareSavingsProgramsScreening_QMB_SLMB_QI);
                                break;
                            case 89:
                                CCData.MedicaidTopics.Add(Topic_MEDICAID.MSPApplicationAssistance);
                                break;
                            case 90:
                                CCData.MedicaidTopics.Add(Topic_MEDICAID.MedicaidScreening_SSI_NursingHome_MEPD_ElderlyWaiver);
                                break;
                            case 91:
                                CCData.MedicaidTopics.Add(Topic_MEDICAID.MedicaidApplicationAssistance);
                                break;
                            case 92:
                                CCData.MedicaidTopics.Add(Topic_MEDICAID.MedicaidOrQMBClaims);
                                break;
                            case 93:
                                CCData.MedicaidTopics.Add(Topic_MEDICAID.FraudAndAbuse);
                                break;
                            default:
                                throw (new ApplicationException("Invalid MedicaidTopics found."));
                        }
                    }
                }

            }


            //MedicaidAdvantage Topics
            int MedicaidAdvnatageStartingIndex = 69;
            CCData.MedicareAdvantageTopics = new List<Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost>();
            for (int iMedicaidAdvantageTopic = 0; iMedicaidAdvantageTopic < 10; iMedicaidAdvantageTopic++)
            {
                if (!string.IsNullOrEmpty(Fields[MedicaidStartingIndex + iMedicaidAdvantageTopic]))
                {
                    FieldValue = Fields[MedicaidAdvnatageStartingIndex + iMedicaidAdvantageTopic];
                    if (IsBooleanValueTrue(FieldValue))
                    {
                        int SelectedTopic = MedicaidAdvnatageStartingIndex + iMedicaidAdvantageTopic;
                        switch (SelectedTopic)
                        {
                            case 69:
                                CCData.MedicareAdvantageTopics.Add(Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost.EligibilityOrScreening);
                                break;
                            case 70:
                                CCData.MedicareAdvantageTopics.Add(Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost.BenefitExplanation);
                                break;
                            case 71:
                                CCData.MedicareAdvantageTopics.Add(Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost.PlansComparison);
                                break;
                            case 72:
                                CCData.MedicareAdvantageTopics.Add(Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost.PlanEnrollmentOrDisenrollment);
                                break;
                            case 73:
                                CCData.MedicareAdvantageTopics.Add(Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost.ClaimsOrBilling);
                                break;
                            case 74:
                                CCData.MedicareAdvantageTopics.Add(Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost.AppealsOrGrievances);
                                break;
                            case 75:
                                CCData.MedicareAdvantageTopics.Add(Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost.FraudAndAbuse);
                                break;
                            case 76:
                                CCData.MedicareAdvantageTopics.Add(Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost.MarketingOrSalesComplaintsOrIssues);
                                break;
                            case 77:
                                CCData.MedicareAdvantageTopics.Add(Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost.QualityOfCare);
                                break;
                            case 78:
                                CCData.MedicareAdvantageTopics.Add(Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost.PlanNonRenewal);
                                break;
                            default:
                                throw (new ApplicationException("Invalid Medicaid Advantage Topics found."));

                        }
                    }
                }
            }

            //MedicarePartsAandBTopics
            int MedicarePartsAandBTopicsStartingIndex = 63;
            CCData.MedicarePartsAandBTopics = new List<Topic_MEDICARE_PartsAandB>();
            for (int iMedicarePartsAandBTopic = 0; iMedicarePartsAandBTopic < 6; iMedicarePartsAandBTopic++)
            {
                if (!string.IsNullOrEmpty(Fields[MedicarePartsAandBTopicsStartingIndex + iMedicarePartsAandBTopic]))
                {
                    FieldValue = Fields[MedicarePartsAandBTopicsStartingIndex + iMedicarePartsAandBTopic];
                    if (IsBooleanValueTrue(FieldValue))
                    {
                        int SelectedTopic = MedicarePartsAandBTopicsStartingIndex + iMedicarePartsAandBTopic;
                        switch (SelectedTopic)
                        {
                            case 63:
                                CCData.MedicarePartsAandBTopics.Add(Topic_MEDICARE_PartsAandB.Eligibility);
                                break;
                            case 64:
                                CCData.MedicarePartsAandBTopics.Add(Topic_MEDICARE_PartsAandB.BenefitExplanation);
                                break;
                            case 65:
                                CCData.MedicarePartsAandBTopics.Add(Topic_MEDICARE_PartsAandB.ClaimsOrBilling);
                                break;
                            case 66:
                                CCData.MedicarePartsAandBTopics.Add(Topic_MEDICARE_PartsAandB.AppealsOrGrievances);
                                break;
                            case 67:
                                CCData.MedicarePartsAandBTopics.Add(Topic_MEDICARE_PartsAandB.FraudAndAbuse);
                                break;
                            case 68:
                                CCData.MedicarePartsAandBTopics.Add(Topic_MEDICARE_PartsAandB.QualityOfCare);
                                break;
                            default:
                                throw (new ApplicationException("Invalid Medicare Parts A and B Topics found."));

                        }
                    }

                }
            }


            //Medicare Prescription Drug Coverage Topics
            int MedicarePrescriptionDrugCoverageTopicsStartingIndex = 43;
            CCData.MedicarePrescriptionDrugCoverageTopics = new List<Topic_MedicarePrescriptionDrugCoverage_PartD>();
            for (int iMedicarePrescriptionDrugCoverageTopic = 0; iMedicarePrescriptionDrugCoverageTopic < 10; iMedicarePrescriptionDrugCoverageTopic++)
            {
                if (!string.IsNullOrEmpty(Fields[MedicarePrescriptionDrugCoverageTopicsStartingIndex + iMedicarePrescriptionDrugCoverageTopic]))
                {
                    FieldValue = Fields[MedicarePrescriptionDrugCoverageTopicsStartingIndex + iMedicarePrescriptionDrugCoverageTopic];
                    if (IsBooleanValueTrue(FieldValue))
                    {
                        int SelectedTopic = MedicarePrescriptionDrugCoverageTopicsStartingIndex + iMedicarePrescriptionDrugCoverageTopic;
                        switch (SelectedTopic)
                        {
                            case 43:
                                CCData.MedicarePrescriptionDrugCoverageTopics.Add(Topic_MedicarePrescriptionDrugCoverage_PartD.EligibilityOrScreening);
                                break;
                            case 44:
                                CCData.MedicarePrescriptionDrugCoverageTopics.Add(Topic_MedicarePrescriptionDrugCoverage_PartD.BenefitExplanation);
                                break;
                            case 45:
                                CCData.MedicarePrescriptionDrugCoverageTopics.Add(Topic_MedicarePrescriptionDrugCoverage_PartD.PlansComparison);
                                break;
                            case 46:
                                CCData.MedicarePrescriptionDrugCoverageTopics.Add(Topic_MedicarePrescriptionDrugCoverage_PartD.PlanEnrollmentOrDisenrollment);
                                break;
                            case 47:
                                CCData.MedicarePrescriptionDrugCoverageTopics.Add(Topic_MedicarePrescriptionDrugCoverage_PartD.ClaimsOrBilling);
                                break;
                            case 48:
                                CCData.MedicarePrescriptionDrugCoverageTopics.Add(Topic_MedicarePrescriptionDrugCoverage_PartD.AppealsOrGrievances);
                                break;
                            case 49:
                                CCData.MedicarePrescriptionDrugCoverageTopics.Add(Topic_MedicarePrescriptionDrugCoverage_PartD.FraudAndAbuse);
                                break;
                            case 50:
                                CCData.MedicarePrescriptionDrugCoverageTopics.Add(Topic_MedicarePrescriptionDrugCoverage_PartD.MarketingOrSalesComplaintsOrIssues);
                                break;
                            case 51:
                                CCData.MedicarePrescriptionDrugCoverageTopics.Add(Topic_MedicarePrescriptionDrugCoverage_PartD.QualityOfCare);
                                break;
                            case 52:
                                CCData.MedicarePrescriptionDrugCoverageTopics.Add(Topic_MedicarePrescriptionDrugCoverage_PartD.PlanNonRenewal);
                                break;
                            default:
                                throw (new ApplicationException("Invalid Medicare Prescription Drug Coverage Topics found."));

                        }
                    }
                }

            }

            //Medicare Supplement Topics
            int MedicareSupplementTopicsStartingIndex = 79;
            CCData.MedicareSupplementTopics = new List<Topic_MedicareSupplementOrSelect>();
            for (int iMedicareSupplementTopic = 0; iMedicareSupplementTopic < 9; iMedicareSupplementTopic++)
            {
                if (!string.IsNullOrEmpty(Fields[MedicareSupplementTopicsStartingIndex + iMedicareSupplementTopic]))
                {
                    FieldValue = Fields[MedicareSupplementTopicsStartingIndex + iMedicareSupplementTopic];
                    if (IsBooleanValueTrue(FieldValue))
                    {
                        int SelectedTopic = MedicareSupplementTopicsStartingIndex + iMedicareSupplementTopic;
                        switch (SelectedTopic)
                        {
                            case 79:
                                CCData.MedicareSupplementTopics.Add(Topic_MedicareSupplementOrSelect.EligibilityOrScreening);
                                break;
                            case 80:
                                CCData.MedicareSupplementTopics.Add(Topic_MedicareSupplementOrSelect.BenefitExplanation);
                                break;
                            case 81:
                                CCData.MedicareSupplementTopics.Add(Topic_MedicareSupplementOrSelect.PlansComparison);
                                break;
                            case 82:
                                CCData.MedicareSupplementTopics.Add(Topic_MedicareSupplementOrSelect.ClaimsOrBilling);
                                break;
                            case 83:
                                CCData.MedicareSupplementTopics.Add(Topic_MedicareSupplementOrSelect.AppealsOrGrievances);
                                break;
                            case 84:
                                CCData.MedicareSupplementTopics.Add(Topic_MedicareSupplementOrSelect.FraudAndAbuse);
                                break;
                            case 85:
                                CCData.MedicareSupplementTopics.Add(Topic_MedicareSupplementOrSelect.MarketingOrSalesComplaintsOrIssues);
                                break;
                            case 86:
                                CCData.MedicareSupplementTopics.Add(Topic_MedicareSupplementOrSelect.QualityOfCare);
                                break;
                            case 87:
                                CCData.MedicareSupplementTopics.Add(Topic_MedicareSupplementOrSelect.PlanNonRenewal);
                                break;
                            default:
                                throw (new ApplicationException("Invalid Medicare Supplement Topics found."));
                                break;
                        }
                    }

                }

            }

            //Other Drug Topics
            int OtherDrugTopicsStartingIndex = 94;
            //CCData.OtherDrugTopics = new List<Topic_OTHER>();
            for (int iOtherDrugTopic = 0; iOtherDrugTopic < 7; iOtherDrugTopic++)
            {
                if (!string.IsNullOrEmpty(Fields[OtherDrugTopicsStartingIndex + iOtherDrugTopic]))
                {
                    FieldValue = Fields[OtherDrugTopicsStartingIndex + iOtherDrugTopic];
                    if (IsBooleanValueTrue(FieldValue))
                    {
                        int SelectedTopic = OtherDrugTopicsStartingIndex + iOtherDrugTopic;
                        if (CCData.OtherDrugTopics == null)
                            CCData.OtherDrugTopics = new List<Topic_OTHER>();
                        switch (SelectedTopic)
                        {

                            case 94:
                                CCData.OtherDrugTopics.Add(Topic_OTHER.LongTermCareInsurance);
                                break;
                            case 95:
                                CCData.OtherDrugTopics.Add(Topic_OTHER.LTCPartnership);
                                break;
                            case 96:
                                CCData.OtherDrugTopics.Add(Topic_OTHER.LTCOther);
                                break;
                            case 97:
                                CCData.OtherDrugTopics.Add(Topic_OTHER.MilitaryHealthBenefits);
                                break;
                            case 98:
                                CCData.OtherDrugTopics.Add(Topic_OTHER.EmployerOrFederalEmployeeHealthBenefits);
                                break;
                            case 99:
                                CCData.OtherDrugTopics.Add(Topic_OTHER.COBRA);
                                break;
                            case 100:
                                CCData.OtherDrugTopics.Add(Topic_OTHER.OtherHealthInsurance);
                                break;
                            default:
                                throw (new ApplicationException("Invalid Other Drug Topics found."));
                        }
                    }

                }

            }

            //Other Prescription Assistance Topics
            int OtherPrescriptionAssistanceTopicsStartingIndex = 58;
            if (CCData.OtherPrescriptionAssistanceTopics == null)
                CCData.OtherPrescriptionAssistanceTopics = new List<Topic_OtherPrescriptionAssistance>();
            for (int iOtherPrescriptionAssistanceTopic = 0; iOtherPrescriptionAssistanceTopic < 4; iOtherPrescriptionAssistanceTopic++)
            {
                if (!string.IsNullOrEmpty(Fields[OtherPrescriptionAssistanceTopicsStartingIndex + iOtherPrescriptionAssistanceTopic]))
                {
                    FieldValue = Fields[OtherPrescriptionAssistanceTopicsStartingIndex + iOtherPrescriptionAssistanceTopic];
                    if (IsBooleanValueTrue(FieldValue))
                    {
                        int SelectedTopic = OtherPrescriptionAssistanceTopicsStartingIndex + iOtherPrescriptionAssistanceTopic;
                        switch (SelectedTopic)
                        {
                            case 58:
                                CCData.OtherPrescriptionAssistanceTopics.Add(Topic_OtherPrescriptionAssistance.UnionOrEmployerPlan);
                                break;
                            case 59:
                                CCData.OtherPrescriptionAssistanceTopics.Add(Topic_OtherPrescriptionAssistance.MilitaryDrugBenefits);
                                break;
                            case 60:
                                CCData.OtherPrescriptionAssistanceTopics.Add(Topic_OtherPrescriptionAssistance.ManufacturerPrograms);
                                break;
                            case 61:
                                CCData.OtherPrescriptionAssistanceTopics.Add(Topic_OtherPrescriptionAssistance.StatePharmaceuticalAssistancePrograms);
                                break;
                            default:
                                throw (new ApplicationException("Invalid Other Prescription Assistance Topics found."));

                        }
                    }
                }

            }

            //Part D Low Income Subsidy Topics
            int PartDLowIncomeSubsidyTopicsStartingIndex = 53;
            CCData.PartDLowIncomeSubsidyTopics = new List<Topic_PartDLowIncomeSubsidy_LISOrExtraHelp>();
            for (int iPartDLowIncomeSubsidyTopic = 0; iPartDLowIncomeSubsidyTopic < 5; iPartDLowIncomeSubsidyTopic++)
            {
                if (!string.IsNullOrEmpty(Fields[PartDLowIncomeSubsidyTopicsStartingIndex + iPartDLowIncomeSubsidyTopic]))
                {
                    FieldValue = Fields[PartDLowIncomeSubsidyTopicsStartingIndex + iPartDLowIncomeSubsidyTopic];
                    if (IsBooleanValueTrue(FieldValue))
                    {
                        int SelectedTopic = PartDLowIncomeSubsidyTopicsStartingIndex + iPartDLowIncomeSubsidyTopic;
                        switch (SelectedTopic)
                        {
                            case 53:
                                CCData.PartDLowIncomeSubsidyTopics.Add(Topic_PartDLowIncomeSubsidy_LISOrExtraHelp.EligibilityOrScreening);
                                break;
                            case 54:
                                CCData.PartDLowIncomeSubsidyTopics.Add(Topic_PartDLowIncomeSubsidy_LISOrExtraHelp.BenefitExplanation);
                                break;
                            case 55:
                                CCData.PartDLowIncomeSubsidyTopics.Add(Topic_PartDLowIncomeSubsidy_LISOrExtraHelp.ApplicationAssistance);
                                break;
                            case 56:
                                CCData.PartDLowIncomeSubsidyTopics.Add(Topic_PartDLowIncomeSubsidy_LISOrExtraHelp.ClaimsOrBilling);
                                break;
                            case 57:
                                CCData.PartDLowIncomeSubsidyTopics.Add(Topic_PartDLowIncomeSubsidy_LISOrExtraHelp.AppealsOrGrievances);
                                break;
                            default:
                                throw (new ApplicationException("Invalid Other Prescription Assistance Topics found."));

                        }
                    }

                }

            }

            //CMSSpecialUseFields
            int CMSSpecialStartingFieldIndex = 105;

              //Validation for CMSspecialFields: Added by Lavanya
                //If you complete one CC Duals data field, you must complete all nine Duals data elements.

            if (Fields[0] != "D")
            {
                ArrayList FieldItemsList = new ArrayList();
                int specialFieldListCMSItemCount = 0;

                FieldItemsList.Add(Fields[CMSSpecialStartingFieldIndex + 1]);
                FieldItemsList.Add(Fields[CMSSpecialStartingFieldIndex + 2]);
                FieldItemsList.Add(Fields[CMSSpecialStartingFieldIndex + 3]);
                FieldItemsList.Add(Fields[CMSSpecialStartingFieldIndex + 4]);
                FieldItemsList.Add(Fields[CMSSpecialStartingFieldIndex + 5]);
                FieldItemsList.Add(Fields[CMSSpecialStartingFieldIndex + 6]);
                FieldItemsList.Add(Fields[CMSSpecialStartingFieldIndex + 7]);
                FieldItemsList.Add(Fields[CMSSpecialStartingFieldIndex + 8]);
                FieldItemsList.Add(Fields[CMSSpecialStartingFieldIndex + 9]);

                foreach (string ItemValue in FieldItemsList)
                {
                    if (!string.IsNullOrEmpty(ItemValue))
                    {
                        specialFieldListCMSItemCount += 1;
                    }
                }

                if (specialFieldListCMSItemCount != 0 && specialFieldListCMSItemCount != FieldItemsList.Count)
                {
                    throw (new ApplicationException("If you complete one CC Duals data field, you must complete all nine Duals data elements."));
                }

                CCData.CMSSpecialUseFields = new List<SpecialFieldValue>();
                DataTable dtSpecialFieldInfo = null;

                dtSpecialFieldInfo = CCFileUploadBLL.GetSpecialFieldInformation(CCData.DateOfContact, FormType.ClientContact, CCData.AgencyState.Code);

                if (dtSpecialFieldInfo != null)
                {
                    for (int iCMSSpecialFields = 0; iCMSSpecialFields < 10; iCMSSpecialFields++)
                    {
                        Boolean IsCMSSpecialUseFieldValid = false;
                        SpecialFieldValue SFieldValue = new SpecialFieldValue();
                        var validationTypeValue = ShiptalkLogic.BusinessObjects.ValidationType.None;

                        var query = from dataRow in dtSpecialFieldInfo.AsEnumerable()
                                    where dataRow.Field<int>("Ordinal") == iCMSSpecialFields + 1
                                    select dataRow;
                        if (query.FirstOrDefault() != null)
                        {
                            foreach (DataRow dr in query)
                            {
                                if (DBNull.Value != dr["Name"])
                                    SFieldValue.Name = dr["Name"].ToString();

                                if (DBNull.Value != dr["SpecialFieldID"])
                                    SFieldValue.Id = int.Parse(dr["SpecialFieldID"].ToString());

                                if (DBNull.Value != dr["Range"])
                                    SFieldValue.Range = dr["Range"].ToString();

                                if (DBNull.Value != dr["ValidationType"])
                                    SFieldValue.ValidationType = int.Parse(dr["ValidationType"].ToString());

                                if (DBNull.Value != dr["IsRequired"])
                                    SFieldValue.IsRequired = Convert.ToBoolean(dr["IsRequired"].ToString());
                            }
                        }

                        validationTypeValue = (ShiptalkLogic.BusinessObjects.ValidationType)SFieldValue.ValidationType;

                        SFieldValue.Value = Fields[CMSSpecialStartingFieldIndex + iCMSSpecialFields] + "";
                        SFieldValue.CreatedDate = DateTime.Now;

                        #region Validate Special Field
                        //Check for Required validation
                        if ((SFieldValue.IsRequired) && (!string.IsNullOrEmpty(SFieldValue.Value)))
                        {
                            IsCMSSpecialUseFieldValid = true;
                        }

                        if (!string.IsNullOrEmpty(SFieldValue.Value))
                        {
                            //Check for Range Validation
                            if (validationTypeValue == ShiptalkLogic.BusinessObjects.ValidationType.Range)
                            {
                                string MinimumValue = string.Empty;
                                string MaximumValue = string.Empty;
                                string Seperator = string.Empty;

                                Seperator = "-";

                                MinimumValue = GetRangeStartValue(SFieldValue.Range.Trim(), Seperator);
                                MaximumValue = GetRangeEndValue(SFieldValue.Range.Trim(), Seperator);

                                if (!IsValidRange(int.Parse(MinimumValue), int.Parse(MaximumValue), int.Parse(SFieldValue.Value.Trim())))
                                {
                                    IsCMSSpecialUseFieldValid = false;
                                    throw (new ApplicationException("Range must be from " + MinimumValue + " to " + MaximumValue + " for special field '" + SFieldValue.Name + "'"));
                                }
                                else
                                {
                                    IsCMSSpecialUseFieldValid = true;
                                }
                            }

                            //Check for Option "Y/N" validation
                            else if (validationTypeValue == ShiptalkLogic.BusinessObjects.ValidationType.Option)
                            {
                                string Option1 = string.Empty;
                                string Option2 = string.Empty;
                                string Seperator = string.Empty;

                                Seperator = ",";

                                Option1 = GetRangeStartValue(SFieldValue.Range.Trim(), Seperator);
                                Option2 = GetRangeEndValue(SFieldValue.Range.Trim(), Seperator);

                                if (!IsValidOption(SFieldValue.Value.Trim(), Option1, Option2))
                                {
                                    IsCMSSpecialUseFieldValid = false;
                                    throw (new ApplicationException("Please enter 'Y'/'N' for special field '" + SFieldValue.Name + "'"));
                                }
                                else
                                {
                                    IsCMSSpecialUseFieldValid = true;
                                }
                            }

                        }

                        #endregion

                        //If validation is successful, then add the item
                        if (IsCMSSpecialUseFieldValid)
                            CCData.CMSSpecialUseFields.Add(SFieldValue);
                    }
                }
            }

                //for (int iCMSSpecialFields = 0; iCMSSpecialFields < 10; iCMSSpecialFields++)
                //{
                //    Boolean IsCMSSpecialUseFieldValid = false;
                //    SpecialFieldValue SFieldValue = new SpecialFieldValue();
                //    var validationTypeValue = ShiptalkLogic.BusinessObjects.ValidationType.None;
                //    SFieldValue = CCFileUploadBLL.GetSpecialFieldInfo(CCData.DateOfContact, FormType.ClientContact, iCMSSpecialFields + 1, CCData.AgencyState.Code);                   

                //    if (SFieldValue != null)
                //    {
                //        //Need to get the ValidationType value only when SFieldValue is not null. 
                //        validationTypeValue = (ShiptalkLogic.BusinessObjects.ValidationType)SFieldValue.ValidationType;

                //        SFieldValue.Value = Fields[CMSSpecialStartingFieldIndex + iCMSSpecialFields] + "";
                //        SFieldValue.CreatedDate = DateTime.Now;

                //        if (Fields[0] != "D")
                //        {
                //            //Check for Required validation
                //            if ((SFieldValue.IsRequired) && (!string.IsNullOrEmpty(SFieldValue.Value)))
                //            {
                //                IsCMSSpecialUseFieldValid = true;
                //            }

                //            if (!string.IsNullOrEmpty(SFieldValue.Value))
                //            {
                //                //Check for Range Validation
                //                if (validationTypeValue == ShiptalkLogic.BusinessObjects.ValidationType.Range)
                //                {
                //                    string MinimumValue = string.Empty;
                //                    string MaximumValue = string.Empty;
                //                    string Seperator = string.Empty;

                //                    Seperator = "-";

                //                    MinimumValue = GetRangeStartValue(SFieldValue.Range.Trim(), Seperator);
                //                    MaximumValue = GetRangeEndValue(SFieldValue.Range.Trim(), Seperator);

                //                    if (!IsValidRange(int.Parse(MinimumValue), int.Parse(MaximumValue), int.Parse(SFieldValue.Value.Trim())))
                //                    {
                //                        IsCMSSpecialUseFieldValid = false;
                //                        throw (new ApplicationException("Range must be from " + MinimumValue + " to " + MaximumValue + " for special field '" + SFieldValue.Name + "'"));
                //                    }
                //                    else
                //                    {
                //                        IsCMSSpecialUseFieldValid = true;
                //                    }
                //                }

                //                //Check for Option "Y/N" validation
                //                else if (validationTypeValue == ShiptalkLogic.BusinessObjects.ValidationType.Option)
                //                {
                //                    string Option1 = string.Empty;
                //                    string Option2 = string.Empty;
                //                    string Seperator = string.Empty;

                //                    Seperator = ",";

                //                    Option1 = GetRangeStartValue(SFieldValue.Range.Trim(), Seperator);
                //                    Option2 = GetRangeEndValue(SFieldValue.Range.Trim(), Seperator);

                //                    if (!IsValidOption(SFieldValue.Value.Trim(), Option1, Option2))
                //                    {
                //                        IsCMSSpecialUseFieldValid = false;
                //                        throw (new ApplicationException("Please enter 'Y'/'N' for special field '" + SFieldValue.Name + "'"));
                //                    }
                //                    else
                //                    {
                //                        IsCMSSpecialUseFieldValid = true;
                //                    }
                //                }
                //            }
                //        }

                //        //If validation is successful, then add the item
                //        if (IsCMSSpecialUseFieldValid)
                //            CCData.CMSSpecialUseFields.Add(SFieldValue);
                //    }
                //}
        


            //State Special Use Fields
            //Get Special Fields look up table.
            State StateValue = new State(Fields[2]);
            //Commented by Lavanya Maram: they are not using it anywhere. so to reduce the database calls I commented it.
           // IEnumerable<SpecialField> spFieldsRules = FileUploadDAL.GetSpecialUploadFieldsValues(FormType.ClientContact, StateValue);

            int StateSpecialUseFieldsStartingIndex = 105;
            CCData.StateSpecialUseFields = new List<SpecialFieldValue>();

            if (Fields[0] != "D")
            {
                DataTable dtStateSpecialFieldInfo = null;

                try
                {
                    dtStateSpecialFieldInfo = CCFileUploadBLL.GetSpecialFieldInformation(CCData.DateOfContact, FormType.ClientContact, CCData.AgencyState.Code);

                    if (dtStateSpecialFieldInfo != null)
                    {
                        for (int iStateSpecialUseFields = 10; iStateSpecialUseFields < 20; iStateSpecialUseFields++)
                        {
                            //Boolean IsCMSSpecialUseFieldValid = false;
                            SpecialFieldValue StateSpecialField = new SpecialFieldValue();                           
                            //var validationTypeValue = ShiptalkLogic.BusinessObjects.ValidationType.None;

                            var query = from dataRow in dtStateSpecialFieldInfo.AsEnumerable()
                                        where dataRow.Field<int>("Ordinal") == iStateSpecialUseFields + 1
                                        select dataRow;

                            if (query.FirstOrDefault() != null)
                            {
                                foreach (DataRow dr in query)
                                {
                                    if (DBNull.Value != dr["Name"])
                                        StateSpecialField.Name = dr["Name"].ToString();

                                    if (DBNull.Value != dr["SpecialFieldID"])
                                        StateSpecialField.Id = int.Parse(dr["SpecialFieldID"].ToString());

                                    if (DBNull.Value != dr["Range"])
                                        StateSpecialField.Range = dr["Range"].ToString();

                                    if (DBNull.Value != dr["ValidationType"])
                                        StateSpecialField.ValidationType = int.Parse(dr["ValidationType"].ToString());

                                    if (DBNull.Value != dr["IsRequired"])
                                        StateSpecialField.IsRequired = Convert.ToBoolean(dr["IsRequired"].ToString());
                                }
                            }
                               
                            StateSpecialField.Value = Fields[StateSpecialUseFieldsStartingIndex + iStateSpecialUseFields] + "";
                            StateSpecialField.CreatedDate = DateTime.Now;

                            if (!string.IsNullOrEmpty(StateSpecialField.Value))
                               CCData.StateSpecialUseFields.Add(StateSpecialField);                           
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }


            ////

            //for (int iStateSpecialUseFields = 10; iStateSpecialUseFields <= 20; iStateSpecialUseFields++)
            //{
            //    SpecialFieldValue StateSpecialField = new SpecialFieldValue();

            //    StateSpecialField = CCFileUploadBLL.GetSpecialFieldInfo(CCData.DateOfContact, FormType.ClientContact, iStateSpecialUseFields + 1, CCData.AgencyState.Code);

            //    //if field has been returned - field is has be defined.
            //    if (StateSpecialField != null)
            //    {
            //        StateSpecialField.Value = Fields[StateSpecialUseFieldsStartingIndex + iStateSpecialUseFields] + "";
            //        StateSpecialField.CreatedDate = DateTime.Now;
            //        if (!string.IsNullOrEmpty(StateSpecialField.Value))
            //            CCData.StateSpecialUseFields.Add(StateSpecialField);
            //    }
            //}






            CCData.Submitter = UserBLL.GetUserProfile(AccountInfo.UserId);

            //******************MAKE CALL TO ADD UPDATE DELETE CLIENT CONTACT*********
            try
            {
                CCFBLL ClientContactRecords = new CCFBLL();
                //Add
                if (Fields[0] == "A")
                {
                    CCFileUploadBLL.AddClientContact(CCData);
                }
                //Update
                if (Fields[0] == "U")
                {
                    CCData.Reviewer = UserBLL.GetUserProfile(int.Parse(Fields[12]));
                    CCFileUploadBLL.UpdateClientContact(CCData);
                }
                //Delete
                if (Fields[0] == "D")
                {
                    CCFileUploadBLL.DeleteClientContact(Fields[1], CCData.AgencyCode, CCData.BatchStateUniqueID);
                }
            }
            catch (System.Exception exSaveClientContact)
            {
                lblFeedBack.InnerHtml = "Record" + Fields[2] + " " + exSaveClientContact.ToString();
                lblFeedBack.Visible = true;
            }



        }

        private string GetRangeStartValue(string strValue, string seperator)
        {
            string RangeStartValue = string.Empty;

            if (strValue.Length > 0)
            {
                RangeStartValue = strValue.Substring(0, strValue.IndexOf(seperator));
            }
            return RangeStartValue;
        }
        private string GetRangeEndValue(string strValue, string seperator)
        {
            string RangeStartValue = string.Empty;
            string RangeEndValue = string.Empty;

            if (strValue.Length > 0)
            {
                RangeStartValue = strValue.Substring(0, strValue.IndexOf(seperator));
                RangeEndValue = strValue.Substring(strValue.IndexOf(seperator) + 1);
            }
            return RangeEndValue;
        }

        protected bool IsValidRange(int min, int Max, int Value)
        {
            if ((Value < min || Value > Max))
            {
                //ErrMsg = "Invalid value found. Number is outside valid range of " + min.ToString() + "  -  " + Max.ToString();
                return false;
            }
            else
            {
                return true;
            }
        }

        protected bool IsValidOption(string Value, string Option1, string Option2)
        {
            if (Value.ToUpper() == Option1.ToUpper() || Value.ToUpper() == Option2.ToUpper())
            {
                return true;

            }
            else
            {
                return false;
            }
        }



        private void InsertUpdateDeletePam(string[] Fields)
        {
            string FieldValue = string.Empty;
            PublicMediaEvent PAMData = new PublicMediaEvent();

            //Unique Record
            PAMData.BatchStateUniqueID = Fields[2];
            //Agency Code
            PAMData.AgencyCode = Fields[3];

            //Presenter
            PAMData.PamPresenters = new List<ShiptalkLogic.BusinessObjects.UI.PamPresenters>();

            int HoursSpentIndex = 8;
            int iKount = 0;
            while (iKount < 25)
            {
                //Check For required field value Hours spent
                if (Fields[HoursSpentIndex] != string.Empty)
                {
                    //We get the data of the presenter by caculating the index offset relative to hours spent.  Hours spent is a required field.                   
                    PamPresenters PNewPresenter = new PamPresenters();
                    string CalculatedMinutes = "";
                    string CalculatedHours = "";

                    CalculatedHours = Fields[HoursSpentIndex];
                    HourMinuteCalc.CalcHours(ref CalculatedHours, ref CalculatedMinutes, true);
                    if (string.IsNullOrEmpty(CalculatedMinutes))
                        CalculatedMinutes = "0";

                    if (string.IsNullOrEmpty(CalculatedHours))
                        CalculatedHours = "0";

                    //PNewPresenter.HoursSpent = int.Parse(Fields[HoursSpentIndex]);
                    // Pbattineni - Convert Hours and check the Sum.
                    PNewPresenter.HoursSpent = Convert.ToDecimal(Fields[HoursSpentIndex]);
                    PNewPresenter.Affiliation = Fields[HoursSpentIndex - 1];
                    PNewPresenter.PAMUserId = Fields[HoursSpentIndex - 4];
                    PAMData.PamPresenters.Add(PNewPresenter);
                }
                HoursSpentIndex = HoursSpentIndex + 5;
                iKount++;
            }

            int IsNumber = -1;
            //Estimated interactive Attendees
            if (int.TryParse(Fields[129], out IsNumber))
            {
                PAMData.InteractiveEstAttendees = IsNumber;
            }

            //Estimated Persons Provided Enrollment Assistance
            if (int.TryParse(Fields[130], out IsNumber))
            {
                PAMData.InteractiveEstProvidedEnrollAssistance = IsNumber;
            }

            //Estimated Number of Direct Interactions with Attendees
            if (int.TryParse(Fields[131], out IsNumber))
            {
                PAMData.BoothEstDirectContacts = IsNumber;
            }

            //Estimated Persons Provided Enrollment Assistance
            if (int.TryParse(Fields[132], out IsNumber))
            {
                PAMData.BoothEstEstProvidedEnrollAssistance = IsNumber;
            }


            //Est Number Persons Reached at Event Regardless of Enroll Assistance
            if (int.TryParse(Fields[133], out IsNumber))
            {
                PAMData.DedicatedEstPersonsReached = IsNumber;
            }

            //Estimated Number Persons Provided Any Enrollment Assistance
            if (int.TryParse(Fields[134], out IsNumber))
            {
                PAMData.DedicatedEstAnyEnrollmentAssistance = IsNumber;
            }

            //Estimated Number Provided Enrollment Assistance with Part D
            if (int.TryParse(Fields[135], out IsNumber))
            {
                PAMData.DedicatedEstPartDEnrollmentAssistance = IsNumber;
            }

            //Estimated Number Provided Enrollment Assistance with LIS
            if (int.TryParse(Fields[136], out IsNumber))
            {
                PAMData.DedicatedEstLISEnrollmentAssistance = IsNumber;
            }


            //Estimated Number Provided Enrollment Assistance with MSP
            if (int.TryParse(Fields[137], out IsNumber))
            {
                PAMData.DedicatedEstMSPEnrollmentAssistance = IsNumber;
            }


            //Estimated Number Provided Enrollment Assist Other Medicare Program
            if (int.TryParse(Fields[138], out IsNumber))
            {
                PAMData.DedicatedEstOtherEnrollmentAssistance = IsNumber;
            }

            //Estimated Number of Radio Listeners Reached
            if (int.TryParse(Fields[139], out IsNumber))
            {
                PAMData.RadioEstListenerReach = IsNumber;
            }

            //Estimated Number of TV Viewers Reached
            if (int.TryParse(Fields[140], out IsNumber))
            {
                PAMData.TVEstViewersReach = IsNumber;
            }

            //Est Persons Viewing or Listening to PSA, Electronic Ad, Crawl Across Entire Campaign, Video Conf,Web Conf, Web Chat
            if (int.TryParse(Fields[141], out IsNumber))
            {
                PAMData.ElectronicEstPersonsViewingOrListening = IsNumber;
            }

            //Est Persons Reading Article, Newsletter, Ad or Pieces of Targeted Mail or Other Printed Across Entire Campaign
            if (int.TryParse(Fields[142], out IsNumber))
            {
                PAMData.PrintEstPersonsReading = IsNumber;
            }

            // PAMData.ActivityStartDate = Convert.ToDateTime(FormatDate(Fields[143]));
            //PAMData.ActivityEndDate = Convert.ToDateTime(FormatDate(Fields[144]));

            PAMData.ActivityStartDate = Convert.ToDateTime(Fields[143]);
            PAMData.ActivityEndDate = Convert.ToDateTime(Fields[144]);

            PAMData.EventName = Fields[145];
            PAMData.ContactFirstName = Fields[146];
            PAMData.ContactLastName = Fields[147];
            PAMData.ContactPhone = Fields[148];
            PAMData.EventState = new State(Fields[149]);
            PAMData.EventCountycode = Fields[150];
            PAMData.EventZIPCode = Fields[151];
            PAMData.EventCity = Fields[152];
            PAMData.EventStreet = Fields[153];
            PAMData.SubmitterUserID = AccountInfo.UserId;
            PAMData.ReviewerUserID = null;
            PAMData.AgencyId = LookupBLL.GetAgencyID(Fields[3], Fields[1]);
            PAMData.IsBatchUploadData = true;

            //Topic Focus
            int TopicFocusIndex = 154;
            PAMData.PAMSelectedTopics = new List<PAMTopic>();
            for (int iTopicFocus = 0; iTopicFocus < 17; iTopicFocus++)
            {
                if (!string.IsNullOrEmpty(Fields[TopicFocusIndex + iTopicFocus]))
                {
                    FieldValue = Fields[TopicFocusIndex + iTopicFocus];
                    if (IsBooleanValueTrue(FieldValue))
                    {
                        int SelectedTopic = TopicFocusIndex + iTopicFocus;
                        switch (SelectedTopic)
                        {
                            case 154:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.MedicarePartsAandB);
                                break;
                            case 155:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.PlanIssues_NonRenewal_Termination_EmployerCOBRA);
                                break;
                            case 156:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.LongTermCare);
                                break;
                            case 157:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.MedigapMedicareSupplements);
                                break;
                            case 158:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.MedicareFraudAndAbuse);
                                break;
                            case 159:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.MedicarePrescriptionDrugCoverage_PDP_MAPD);
                                break;
                            case 160:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.OtherPrescriptionDrugCoverageAssistance);
                                break;
                            case 161:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.MedicareAdvantage);
                                break;
                            case 162:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.QMB_SLMB_QI);
                                break;
                            case 163:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.OtherMedicaid);
                                break;
                            case 164:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.GeneralSHIPProgramInformation);
                                break;
                            case 165:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.MedicarePreventiveServices);
                                break;
                            case 166:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.LowIncomeAssistance);
                                break;
                            case 167:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.DualEligiblewithMentalIllnessMentalDisability);
                                break;
                            case 168:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.VolunteerRecruitment);
                                break;
                            case 169:
                                PAMData.PAMSelectedTopics.Add(PAMTopic.PartnershipRecruitment);
                                break;
                            case 170:
                                if (!string.IsNullOrEmpty(FieldValue))
                                {
                                    PAMData.OtherPamTopicSpecified = FieldValue + "";
                                    PAMData.PAMSelectedTopics.Add(PAMTopic.OtherTopics);
                                }
                                break;

                            default:
                                throw (new ApplicationException("Invalid Topic Focus found."));
                        }
                    }
                }

            }

            //Target Audiences
            int TargetAudienceStartIndex = 171;
            PAMData.PAMSelectedAudiences = new List<PAMAudiance>();
            for (int iTargetAudience = 0; iTargetAudience < 29; iTargetAudience++)
            {
                if (!string.IsNullOrEmpty(Fields[TargetAudienceStartIndex + iTargetAudience]))
                {
                    FieldValue = Fields[TargetAudienceStartIndex + iTargetAudience];
                    if (IsBooleanValueTrue(FieldValue))
                    {
                        int SelectedTopic = TargetAudienceStartIndex + iTargetAudience;
                        switch (SelectedTopic)
                        {
                            case 171:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.MedicarePreEnrolleesAge45to64);
                                break;
                            case 172:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.MedicareBeneficiaries);
                                break;
                            case 173:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.FamilyMembersCaregiversOfMedicareBeneficiaries);
                                break;
                            case 174:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.LowIncome);
                                break;
                            case 175:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.HispanicLatinoOrSpanishOrigin);
                                break;
                            case 176:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.White_NonHispanic);
                                break;
                            case 177:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.BlackOrAfricanAmerican);
                                break;
                            case 178:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.AmericanIndianOrAlaskaNative);
                                break;
                            case 179:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.AsianIndian);
                                break;
                            case 180:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.Chinese);
                                break;
                            case 181:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.Filipino);
                                break;
                            case 182:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.Japanese);
                                break;
                            case 183:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.Vietnamese);
                                break;
                            case 184:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.Korean);
                                break;
                            case 185:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.NativeHawaiian);
                                break;
                            case 186:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.GuamanianOrChamorro);
                                break;
                            case 187:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.Samoan);
                                break;
                            case 188:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.OtherAsian);
                                break;
                            case 189:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.OtherPacificIslander);
                                break;
                            case 190:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.SomeOtherRaceEthnicity);
                                break;
                            case 191:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.Disabled);
                                break;
                            case 192:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.Rural);
                                break;
                            case 193:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.EmployerRelatedGroups);
                                break;
                            case 194:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.MentalHealthProfessionals);
                                break;
                            case 195:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.SocialWorkProfessionals);
                                break;
                            case 196:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.DualEligibleGroups);
                                break;
                            case 197:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.PartnershipOutreach);
                                break;
                            case 198:
                                PAMData.PAMSelectedAudiences.Add(PAMAudiance.PresentationsToGroupsInLanguagesOtherThanEnglish);
                                break;
                            case 199:
                                if (!string.IsNullOrEmpty(FieldValue))
                                {
                                    PAMData.PAMSelectedAudiences.Add(PAMAudiance.OtherAudiences);
                                    PAMData.OtherPamAudienceSpecified = FieldValue;
                                }
                                break;
                            default:
                                throw (new ApplicationException("Invalid Target Audience found."));
                        }
                    }
                }

            }


            int CMSSpecialStartingFieldIndex = 200;

            //Validation for CMSspecialFields: Added by Lavanya
            //If you complete one PAM Duals data field, you must complete both Duals data elements.
            if (Fields[0] != "D")
            {
                ArrayList FieldItemsList = new ArrayList();
                int specialFieldListCMSItemCount = 0;

                FieldItemsList.Add(Fields[CMSSpecialStartingFieldIndex + 1]);
                FieldItemsList.Add(Fields[CMSSpecialStartingFieldIndex + 2]);

                foreach (string ItemValue in FieldItemsList)
                {
                    if (!string.IsNullOrEmpty(ItemValue))
                    {
                        specialFieldListCMSItemCount += 1;
                    }
                }

                if (specialFieldListCMSItemCount != 0 && specialFieldListCMSItemCount != FieldItemsList.Count)
                {
                    throw (new ApplicationException("If you complete one PAM Duals data field, you must complete both Duals data elements."));
                }


                PAMData.CMSSpecialUseFields = new List<SpecialFieldValue>();
                DataTable dtPAMCMSSpecialFieldInfo = null;

                dtPAMCMSSpecialFieldInfo = CCFileUploadBLL.GetSpecialFieldInformation(PAMData.ActivityStartDate.Value, FormType.PublicMediaActivity, Fields[1]);

                if (dtPAMCMSSpecialFieldInfo != null)
                {
                    for (int iCMSSpecialFields = 0; iCMSSpecialFields < 3; iCMSSpecialFields++)
                    {
                        Boolean IsCMSSpecialUseFieldValid = false;
                        SpecialFieldValue SFieldValue = new SpecialFieldValue();
                        var validationTypeValue = ShiptalkLogic.BusinessObjects.ValidationType.None;

                        var query = from dataRow in dtPAMCMSSpecialFieldInfo.AsEnumerable()
                                    where dataRow.Field<int>("Ordinal") == iCMSSpecialFields + 1
                                    select dataRow;

                        if (query.FirstOrDefault() != null)
                        {
                            foreach (DataRow dr in query)
                            {
                                if (DBNull.Value != dr["Name"])
                                    SFieldValue.Name = dr["Name"].ToString();

                                if (DBNull.Value != dr["SpecialFieldID"])
                                    SFieldValue.Id = int.Parse(dr["SpecialFieldID"].ToString());

                                if (DBNull.Value != dr["Range"])
                                    SFieldValue.Range = dr["Range"].ToString();

                                if (DBNull.Value != dr["ValidationType"])
                                    SFieldValue.ValidationType = int.Parse(dr["ValidationType"].ToString());

                                if (DBNull.Value != dr["IsRequired"])
                                    SFieldValue.IsRequired = Convert.ToBoolean(dr["IsRequired"].ToString());
                            }
                        }

                        validationTypeValue = (ShiptalkLogic.BusinessObjects.ValidationType)SFieldValue.ValidationType;

                        SFieldValue.Value = Fields[CMSSpecialStartingFieldIndex + iCMSSpecialFields] + "";
                        SFieldValue.CreatedDate = DateTime.Now;

                        //Check for Required validation
                        if ((SFieldValue.IsRequired) && (!string.IsNullOrEmpty(SFieldValue.Value)))
                        {
                            IsCMSSpecialUseFieldValid = true;
                        }

                        if (!string.IsNullOrEmpty(SFieldValue.Value))
                        {
                            //Check for Range Validation
                            if (validationTypeValue == ShiptalkLogic.BusinessObjects.ValidationType.Range)
                            {
                                string MinimumValue = string.Empty;
                                string MaximumValue = string.Empty;
                                string Seperator = string.Empty;

                                Seperator = "-";

                                MinimumValue = GetRangeStartValue(SFieldValue.Range.Trim(), Seperator);
                                MaximumValue = GetRangeEndValue(SFieldValue.Range.Trim(), Seperator);

                                if (!IsValidRange(int.Parse(MinimumValue), int.Parse(MaximumValue), int.Parse(SFieldValue.Value.Trim())))
                                {
                                    IsCMSSpecialUseFieldValid = false;
                                    throw (new ApplicationException("Range must be from " + MinimumValue + " to " + MaximumValue + " for special field '" + SFieldValue.Name + "'"));
                                }
                                else
                                {
                                    IsCMSSpecialUseFieldValid = true;
                                }
                            }


                                //Check for Option "Y/N" validation
                            else if (validationTypeValue == ShiptalkLogic.BusinessObjects.ValidationType.Option)
                            {
                                string Option1 = string.Empty;
                                string Option2 = string.Empty;
                                string Seperator = string.Empty;

                                Seperator = ",";

                                Option1 = GetRangeStartValue(SFieldValue.Range.Trim(), Seperator);
                                Option2 = GetRangeEndValue(SFieldValue.Range.Trim(), Seperator);

                                if (!IsValidOption(SFieldValue.Value.Trim(), Option1, Option2))
                                {
                                    IsCMSSpecialUseFieldValid = false;
                                    throw (new ApplicationException("Please enter 'Y'/'N' for special field '" + SFieldValue.Name + "'"));
                                }
                                else
                                {
                                    IsCMSSpecialUseFieldValid = true;
                                }
                            }
                        }

                        //If validation is successful, then add the item
                        if (IsCMSSpecialUseFieldValid)
                            PAMData.CMSSpecialUseFields.Add(SFieldValue);

                    }
                }
            }
            /////////////////
            ////////////////////////////////////////////////////////////////////////////*
            //PAMData.CMSSpecialUseFields = new List<SpecialFieldValue>();
            //for (int iCMSSpecialFields = 0; iCMSSpecialFields < 3; iCMSSpecialFields++)
            //{
            //    Boolean IsCMSSpecialUseFieldValid = false;
            //    var validationTypeValue = ShiptalkLogic.BusinessObjects.ValidationType.None;

            //    SpecialFieldValue SFieldValue = new SpecialFieldValue();
            //    SFieldValue = CCFileUploadBLL.GetSpecialFieldInfo(PAMData.ActivityStartDate.Value, FormType.PublicMediaActivity, iCMSSpecialFields + 1, Fields[1]);

            //    if (SFieldValue != null)
            //    {
            //        //Need to get the ValidationType value only when SFieldValue is not null. 
            //        validationTypeValue = (ShiptalkLogic.BusinessObjects.ValidationType)SFieldValue.ValidationType;

            //        SFieldValue.Value = Fields[CMSSpecialStartingFieldIndex + iCMSSpecialFields] + "";
            //        SFieldValue.CreatedDate = DateTime.Now;

            //        if (Fields[0] != "D")
            //        {
            //            //Check for Required validation
            //            if ((SFieldValue.IsRequired) && (!string.IsNullOrEmpty(SFieldValue.Value)))
            //            {
            //                IsCMSSpecialUseFieldValid = true;
            //            }

            //            if (!string.IsNullOrEmpty(SFieldValue.Value))
            //            {
            //                //Check for Range Validation
            //                if (validationTypeValue == ShiptalkLogic.BusinessObjects.ValidationType.Range)
            //                {
            //                    string MinimumValue = string.Empty;
            //                    string MaximumValue = string.Empty;
            //                    string Seperator = string.Empty;

            //                    Seperator = "-";

            //                    MinimumValue = GetRangeStartValue(SFieldValue.Range.Trim(), Seperator);
            //                    MaximumValue = GetRangeEndValue(SFieldValue.Range.Trim(), Seperator);

            //                    if (!IsValidRange(int.Parse(MinimumValue), int.Parse(MaximumValue), int.Parse(SFieldValue.Value.Trim())))
            //                    {
            //                        IsCMSSpecialUseFieldValid = false;
            //                        throw (new ApplicationException("Range must be from " + MinimumValue + " to " + MaximumValue + " for special field '" + SFieldValue.Name + "'"));
            //                    }
            //                    else
            //                    {
            //                        IsCMSSpecialUseFieldValid = true;
            //                    }
            //                }


            //                    //Check for Option "Y/N" validation
            //                else if (validationTypeValue == ShiptalkLogic.BusinessObjects.ValidationType.Option)
            //                {
            //                    string Option1 = string.Empty;
            //                    string Option2 = string.Empty;
            //                    string Seperator = string.Empty;

            //                    Seperator = ",";

            //                    Option1 = GetRangeStartValue(SFieldValue.Range.Trim(), Seperator);
            //                    Option2 = GetRangeEndValue(SFieldValue.Range.Trim(), Seperator);

            //                    if (!IsValidOption(SFieldValue.Value.Trim(), Option1, Option2))
            //                    {
            //                        IsCMSSpecialUseFieldValid = false;
            //                        throw (new ApplicationException("Please enter 'Y'/'N' for special field '" + SFieldValue.Name + "'"));
            //                    }
            //                    else
            //                    {
            //                        IsCMSSpecialUseFieldValid = true;
            //                    }
            //                }
            //            }
            //        }

            //        //If validation is successful, then add the item
            //        if (IsCMSSpecialUseFieldValid)
            //            PAMData.CMSSpecialUseFields.Add(SFieldValue);
            //    }
            //}

            //Added by Lavanya
            //State Special Use Fields
            int StateSpecialUseFieldsStartingIndex = 200;
            PAMData.StateSpecialUseFields = new List<SpecialFieldValue>();

            if (Fields[0] != "D")
            {
                DataTable dtPAMStateSpecialFieldInfo = null;

                try
                {
                    dtPAMStateSpecialFieldInfo = CCFileUploadBLL.GetSpecialFieldInformation(PAMData.ActivityStartDate.Value, FormType.PublicMediaActivity, Fields[1]);

                    if (dtPAMStateSpecialFieldInfo != null)
                    {
                        for (int iStateSpecialUseFields = 10; iStateSpecialUseFields < 20; iStateSpecialUseFields++)
                        {
                            SpecialFieldValue StateSpecialField = new SpecialFieldValue();

                            var query = from dataRow in dtPAMStateSpecialFieldInfo.AsEnumerable()
                                        where dataRow.Field<int>("Ordinal") == iStateSpecialUseFields + 1
                                        select dataRow;

                            if (query.FirstOrDefault() != null)
                            {
                                foreach (DataRow dr in query)
                                {
                                    if (DBNull.Value != dr["Name"])
                                        StateSpecialField.Name = dr["Name"].ToString();

                                    if (DBNull.Value != dr["SpecialFieldID"])
                                        StateSpecialField.Id = int.Parse(dr["SpecialFieldID"].ToString());

                                    if (DBNull.Value != dr["Range"])
                                        StateSpecialField.Range = dr["Range"].ToString();

                                    if (DBNull.Value != dr["ValidationType"])
                                        StateSpecialField.ValidationType = int.Parse(dr["ValidationType"].ToString());

                                    if (DBNull.Value != dr["IsRequired"])
                                        StateSpecialField.IsRequired = Convert.ToBoolean(dr["IsRequired"].ToString());
                                }
                            }
                                
                            StateSpecialField.Value = Fields[StateSpecialUseFieldsStartingIndex + iStateSpecialUseFields];
                            StateSpecialField.CreatedDate = DateTime.Now;
                            StateSpecialField.CreatedBy = AccountInfo.UserId;

                            if (!string.IsNullOrEmpty(StateSpecialField.Value))
                            {
                                PAMData.StateSpecialUseFields.Add(StateSpecialField);
                            }
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            //end



            ///////
            //int StateSpecialUseFieldsStartingIndex = 200;
            //PAMData.StateSpecialUseFields = new List<SpecialFieldValue>();
            //for (int iStateSpecialUseFields = 10; iStateSpecialUseFields <= 20; iStateSpecialUseFields++)
            //{
            //    SpecialFieldValue StateSpecialField = new SpecialFieldValue();
            //    StateSpecialField = CCFileUploadBLL.GetSpecialFieldInfo(PAMData.ActivityStartDate.Value, FormType.PublicMediaActivity, iStateSpecialUseFields + 1, Fields[1]);

            //    if (StateSpecialField != null)
            //    {
            //        StateSpecialField.Value = Fields[StateSpecialUseFieldsStartingIndex + iStateSpecialUseFields];
            //        StateSpecialField.CreatedDate = DateTime.Now;
            //        StateSpecialField.CreatedBy = AccountInfo.UserId;

            //        if (!string.IsNullOrEmpty(StateSpecialField.Value))
            //        {
            //            PAMData.StateSpecialUseFields.Add(StateSpecialField);
            //        }

            //    }
            //}


            try
            {
                //******************MAKE CALL TO ADD UPDATE DELETE PAM*********
                if (Fields[0] == "A")
                {
                    if (PamFileUpLoadBLL.AddPamRecord(PAMData) < 1)
                    {
                        lblFeedBack.InnerHtml = "Record failed to be inserted.";
                        lblFeedBack.Visible = true;
                    }
                }
                if (Fields[0] == "U")
                {

                    PamFileUpLoadBLL.UpdatePamRecord(PAMData, Fields[3], Fields[2]);
                }

                if (Fields[0] == "D")
                {
                    PamFileUpLoadBLL.DeletePamRecord(Fields[1], Fields[3], Fields[2]);
                }
            }
            catch (System.Exception exInsertUpdateDelete)
            {
                //TODO: Create error file that will contain records that could not be saved to DB.
                lblFeedBack.InnerHtml = exInsertUpdateDelete.ToString() + " " + exInsertUpdateDelete.StackTrace;
                lblFeedBack.Visible = true;
            }
        }

        /// <summary>
        /// Formats Date
        /// </summary>
        /// <param name="UnFormattedDate"></param>
        /// <returns></returns>
        private string FormatDate(string UnFormattedDate)
        {
            char[] dtSep = { '/' };
            if (UnFormattedDate.Length != 9 && UnFormattedDate.Length != 10)
            {
                throw (new ApplicationException("Date invalid format"));
            }
            if (UnFormattedDate.Length == 9)
            {
                UnFormattedDate = UnFormattedDate.Substring(0, 9);
            }
            else
            {
                UnFormattedDate = UnFormattedDate.Substring(0, 10);
            }
            string[] DateParts = UnFormattedDate.Split(dtSep);

            if (UnFormattedDate.Length == 0)
            {
                throw (new ApplicationException("Date invalid format"));
            }
            else
            {
                try
                {
                    DateTime dtContacted = new DateTime(int.Parse(DateParts[2]), int.Parse(DateParts[0]), int.Parse(DateParts[1]));
                    return dtContacted.ToString("MM/dd/yyyy");
                }
                catch (System.Exception exDateConversion)
                {
                    throw (new ApplicationException("Date invalid format"));
                }
            }
        }

        #endregion


        #region Control Events



        protected void btnProcess_Click(object sender, EventArgs e)
        {
            if (Session["UPLOAD_CLEAN_FILE_PATH"] == null)
            {
                Server.Transfer("../default.aspx");
            }
            CleanFilePath = Session["UPLOAD_CLEAN_FILE_PATH"].ToString();
            StreamReader CleanFileHandler = new StreamReader(CleanFilePath);
            StreamWriter ErrorUploadFileHandler = null;
            try
            {
                string LineRead = string.Empty;
                char[] RecordSeparator = { '\t' };
                lblFeedBack.InnerHtml = string.Empty;
                string AssignedClientID = string.Empty;
                while (CleanFileHandler.Peek() >= 0)
                {
                    LineRead = CleanFileHandler.ReadLine();
                    string[] Fields = LineRead.Split(RecordSeparator);
                    try
                    {
                        if (CleanFilePath.Contains("PAM"))
                        {
                            //Insert Update Delete PAM records
                            InsertUpdateDeletePam(Fields);
                        }
                        else
                        {
                            //Insert client contact file
                            InsertClientContact(Fields);
                        }
                    }
                    catch (System.Exception ExRecord)
                    {
                        if (ErrorUploadFileHandler == null)
                        {
                            string ProcessErrorPath = System.Configuration.ConfigurationManager.AppSettings["ProcessErrorPath"];
                            ProcessErrorPath = ProcessErrorPath.Trim();
                            if (!Path.IsPathRooted(ProcessErrorPath))
                            {
                                string appPath = HttpContext.Current.Server.MapPath("~/");
                                ProcessErrorPath = Path.Combine(appPath, ProcessErrorPath);
                            }
                            if (!Directory.Exists(ProcessErrorPath))
                            {
                                try
                                {
                                    Directory.CreateDirectory(ProcessErrorPath);
                                }
                                catch (System.Exception exErrorFilePath)
                                {
                                    lblFeedBack.InnerHtml = "Failed: Logging errored out records for database commitment.";
                                    return;
                                }

                            }
                        }



                    }
                }//End to While loop
                if (lblFeedBack.InnerHtml == string.Empty)
                {
                    lblFeedBack.InnerHtml = "<strong>Records have been successfully uploaded. </strong>";
                    lblFeedBack.Visible = true;
                }
                else
                {
                    //Condition Satisfied: Error Occured during insertion, updating or deleting of record.
                    //Store the record in the process error file.

                }
                lblFeedBack.Visible = true;
            }
            finally
            {
                try
                {
                    CleanFileHandler.Close();
                    File.Delete(CleanFilePath);
                }
                catch (System.Exception exFileAccess)
                {
                    lblFeedBack.InnerText = exFileAccess.Message + " " + exFileAccess.StackTrace;
                    lblFeedBack.Visible = true;
                }
                btnProcess.Visible = false;
            }
        }

        private Boolean Insertrecords( string filepath , string filetype )
        {
            StreamReader CleanFileHandler = new StreamReader(filepath);
            StreamWriter ErrorUploadFileHandler = null;
            Boolean bresult;
            try
            {
                string LineRead = string.Empty;
                char[] RecordSeparator = { '\t' };
                lblFeedBack.InnerHtml = string.Empty;
                string AssignedClientID = string.Empty;

                string Action = string.Empty;

                while (CleanFileHandler.Peek() >= 0)
                {
                    LineRead = CleanFileHandler.ReadLine();
                    string[] Fields = LineRead.Split(RecordSeparator);

                    if (Fields[0] == "A")
                        Action = "A";
                    else if (Fields[0] == "U")
                        Action = "U";
                    else if (Fields[0] == "D")
                        Action = "D";

                    try
                    {
                        if (CleanFilePath.Contains("PAM"))
                        {
                            //Insert Update Delete PAM records
                            InsertUpdateDeletePam(Fields);
                        }
                        else
                        {
                            //Insert client contact file
                            InsertClientContact(Fields);
                        }
                    }
                    catch (System.Exception ExRecord)
                    {
                        if (ErrorUploadFileHandler == null)
                        {
                            string ProcessErrorPath = System.Configuration.ConfigurationManager.AppSettings["ProcessErrorPath"];
                            ProcessErrorPath = ProcessErrorPath.Trim();
                            if (!Path.IsPathRooted(ProcessErrorPath))
                            {
                                string appPath = HttpContext.Current.Server.MapPath("~/");
                                ProcessErrorPath = Path.Combine(appPath, ProcessErrorPath);
                            }
                            if (!Directory.Exists(ProcessErrorPath))
                            {
                                try
                                {
                                    Directory.CreateDirectory(ProcessErrorPath);
                                }
                                catch (System.Exception exErrorFilePath)
                                {
                                    lblFeedBack.InnerHtml = "Failed: Logging errored out records for database commitment.";
                                    bresult = false;
                                    return bresult;
                                }
                            }
                        }



                    }
                }//End to While loop
                if (lblFeedBack.InnerHtml == string.Empty)
                {     
                    //lblFeedBack.InnerHtml = "<strong>Records have been successfully uploaded.</strong>";
                    lblFeedBack.Visible = true;

                    //Added by Lavanya: 06/27/2013
                    if (Action == "A")
                    {
                        lblFeedBack.InnerHtml = "<strong>Records have been successfully uploaded.</strong>";
                    }

                    else if (Action == "U")
                    {
                        lblFeedBack.InnerHtml = "<strong>Records have been successfully updated.</strong>";
                    }

                    else if (Action == "D")
                    {
                        lblFeedBack.InnerHtml = "<strong>Records have been successfully Deleted.</strong>";
                    }
                    //End adding :Lavanya
                                        
                    bresult = true;
                }
                else
                {
                    //Condition Satisfied: Error Occured during insertion, updating or deleting of record.
                    //Store the record in the process error file.
                    bresult = false;
                }
                lblFeedBack.Visible = true;
            }
            finally
            {
                try
                {
                    CleanFileHandler.Close();
                    File.Delete(CleanFilePath);
                }
                catch (System.Exception exFileAccess)
                {
                    lblFeedBack.InnerText = exFileAccess.Message + " " + exFileAccess.StackTrace;
                    lblFeedBack.Visible = true;
                }
                btnProcess.Visible = false;
            }
            return bresult;
        }


        private bool IsBooleanValueTrue(string FieldValue)
        {
            FieldValue = FieldValue.ToUpper();
            if (FieldValue == "Y" || FieldValue == "YES" || FieldValue == "TRUE")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            grdUploadStatus.DataSource = null;
            grdUploadStatus.Visible = false;
            lblFeedBack.InnerHtml = string.Empty;
            lblStatus.InnerHtml = string.Empty;
            lblValidation.InnerHtml = string.Empty;


            _Errors = new StringBuilder();
            if (FileUpload1.HasFile)
            {
                try
                {
                    //Check for valid file name
                    string Ext = GetFileUploadType();

                    if (string.IsNullOrEmpty(Ext))
                    {
                        lblFeedBack.InnerHtml = "Invalid file name found. File name must start with PAM_ or CC_";

                        //Do not continue processing at this point.
                        return;
                    }

                    //Check to make sure the file ends in .TXT
                    if (IsExtensionValid(FileUpload1.FileName) == false)
                    {
                        lblFeedBack.InnerHtml = "Invalid file extension found must be .txt file type";

                        //Do not continue processing at this point.
                        return;
                    }

                    //Check for invalid file size
                    if (!IsFileSizeValid())
                    {
                        lblFeedBack.InnerHtml = "Size of file is greater than the amount permitted.";

                        //Do not continue processing at this point.
                        return;
                    }

                    //Determine what the first characters in the file name will be.
                    //AND directory to upload the file to.
                    string mainDirectory = string.Empty;
                    string PreName = string.Empty;

                    if (Ext == "PAM")
                    {
                        PreName = "PAM";
                        mainDirectory = this.UploadDirectoryPam;
                    }
                    else
                    {
                        PreName = "CC";
                        mainDirectory = this.UploadDirectoryCC;
                    }


                    //Create Directory to upload file.
                    if (!Directory.Exists(mainDirectory))
                    {
                        try
                        {
                            Directory.CreateDirectory(mainDirectory);
                        }
                        catch (UnauthorizedAccessException exAuth)
                        {
                            lblFeedBack.InnerHtml = "<b>Unauthorized access to upload directory was encountered.</b>";
                            lblFeedBack.Visible = true;
                            return;
                        }
                        catch (DirectoryNotFoundException exDir)
                        {
                            lblFeedBack.InnerHtml = "<b>Upload processing directory not found.</b>";
                            lblFeedBack.Visible = true;
                            return;
                        }
                        catch (System.Exception exDirectory)
                        {
                            lblFeedBack.InnerHtml = "<b>" + exDirectory.Message + "</b>";
                            lblFeedBack.Visible = true;
                            return;
                        }
                    }

                    //Get a timestamp to be used in the name thus making file name unique
                    string sYear = DateTime.Now.Year.ToString();
                    string sMonth = DateTime.Now.Month.ToString();
                    string sDay = DateTime.Now.Day.ToString();
                    string sHour = DateTime.Now.Hour.ToString();
                    string sMin = DateTime.Now.Minute.ToString();
                    string sSec = DateTime.Now.Second.ToString();
                    string timeStamp = sMonth + sDay + sHour + sMin + sSec;
                    

                    char[] sep = { '.' };
                    //Original File
                    string[] OriginalRootFileName = FileUpload1.FileName.Split(sep);
                    OriginalFilePath = Path.Combine(mainDirectory, PreName + timeStamp + OriginalRootFileName[0].Replace(PreName, string.Empty) + "Original.txt");
                    FileUpload1.SaveAs(OriginalFilePath);



                    //Create Error file
                    string[] ErrorRootFileName = FileUpload1.FileName.Split(sep);
                    ErrorFilePath = Path.Combine(mainDirectory, PreName + timeStamp + ErrorRootFileName[0].Replace(PreName, string.Empty) + "Error.txt");
                    File.Create(ErrorFilePath).Close();
                    Session.Add("UPLOAD_ERROR_FILE_PATH", ErrorFilePath);

                    //Save the Clean File
                    string[] CleanRootFileName = FileUpload1.FileName.Split(sep);
                    CleanFilePath = Path.Combine(mainDirectory, PreName + "_" + timeStamp + CleanRootFileName[0].Replace(PreName, string.Empty) + "Clean.txt");
                    Session.Add("UPLOAD_CLEAN_FILE_PATH", CleanFilePath);
                    //Insert a batch upload file record capture the Primary Key in UploadId
                    int UploadId = UpLoadStatusManager.AddUploadfile(AccountInfo.StateFIPS, CleanFilePath, ErrorFilePath, OriginalFilePath, FileUpload1.FileName, PreName, AccountInfo.UserId);

                    //Set Status File Upload Started - 
                    UpLoadStatusManager.AddUploadStatus("UPLOAD STARTED", "Size of file for upload is " + FileUpload1.FileBytes.Length.ToString() + " bytes", UploadId);


                    //Set Status File Upload to UPLOAD COMPLETED
                    UpLoadStatusManager.AddUploadStatus("UPLOAD COMPLETED", "Size of file for upload is " + FileUpload1.FileBytes.Length.ToString() + " bytes", UploadId);

                    StreamReader OriginalFileStreamReader = null;
                    //Start validation of file content.
                    Boolean bValidsuccess;
                    Boolean bInsertSuccess;                    

                    if (Ext == "PAM")
                    {
                        _pam = new PamFileUpLoadBLL();

                        //Insert status Start Validation
                        UpLoadStatusManager.AddUploadStatus("VALIDATION STARTED", "validation initialized.", UploadId);

                        OriginalFileStreamReader = new StreamReader(FileUpload1.PostedFile.InputStream);

                        //Validate Pam file 
                        bValidsuccess= ProcessSomeFile(ref OriginalFileStreamReader, OriginalFilePath, UploadId, "PamFieldCount", (ILoader)_pam);
                        if (bValidsuccess)
                        {
                            UpLoadStatusManager.AddUploadStatus("RECORDS INSERT STARTED", "Records Insert Started at " + DateTime.Now, UploadId);

                            bInsertSuccess = Insertrecords(OriginalFilePath, "PAM");
                            if (bInsertSuccess)
                            {
                                UpLoadStatusManager.AddUploadStatus("RECORDS INSERT SUCCSESSFUL", "Records Inserted Successfully at" + DateTime.Now, UploadId);

                            }
                        }
                    }

                    if (Ext == "CC")
                    {
                        OriginalFileStreamReader = new StreamReader(FileUpload1.PostedFile.InputStream);
                        _cc = new CCFileUploadBLL();

                        bValidsuccess = ProcessSomeFile(ref OriginalFileStreamReader, OriginalFilePath, UploadId, "CCFieldCount", (ILoader)_cc);
                        if (bValidsuccess)                     
                        {
                            UpLoadStatusManager.AddUploadStatus("RECORDS INSERT STARTED", "Records Insert started at " + DateTime.Now, UploadId);

                            bInsertSuccess= Insertrecords(OriginalFilePath, "CC");
                            if (bInsertSuccess)
                            {
                                UpLoadStatusManager.AddUploadStatus("RECORDS INSERT SUCCSESSFUL", "Records Inserted Sucessfully at " + DateTime.Now, UploadId);

                            }
                        }

                    }
                }
                catch (System.Web.HttpException objr)
                {
                    lblFeedBack.InnerHtml = "<b>" + objr.Message + "</b>" + " " + objr.StackTrace;
                    lblFeedBack.Visible = true;
                    return;


                }
                catch (System.Exception objError)
                {
                    lblFeedBack.InnerHtml = "<b>" + objError.Message + "</b>" + " " + objError.StackTrace;
                    lblFeedBack.Visible = true;
                    return;
                }
            }
            else
            {
                //Condition - User has not selected a file to upload with file control.
                lblStatus.InnerHtml = "<div>Click on the Browse button to select a file and then click validate to start the validation processs for file upload.</div>";
                btnDownload.Visible = false;
            }


        }


        private void DownloadErrorFile(string FileName)
        {
            //To Get the physical Path of the file(me2.doc)   
            string filepath = FileName;
            // Create New instance of FileInfo class to get the properties of the file being downloaded   
            FileInfo myfile = new FileInfo(filepath);
            // Checking if file exists   
            if (myfile.Exists)
            {
                // Clear the content of the response    
                Response.ClearContent();
                // Add the file name and attachment, which will force the open/cancel/save dialog box to show, to the header    
                Response.AddHeader("Content-Disposition", "attachment; filename=" + myfile.Name);
                // Add the file size into the response header    
                Response.AddHeader("Content-Length", myfile.Length.ToString());
                // Set the ContentType    
                Response.ContentType = "text/plain";
                // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)    
                Response.TransmitFile(myfile.FullName);
                // End the response    
                Response.End();
            }
        }


        private void ShowUploadGrid()
        {
            //GetFileUploadStatusByUser is inherited therefore CCFileUploadBLL and PamFileUploadBLL can both be used
            grdUploadStatus.DataSource = UpLoadStatusManager.GetFileUploadStatusByUser(AccountInfo.UserId);

            DataSet retData = (DataSet)grdUploadStatus.DataSource;
            if (retData.Tables[0].Rows.Count > 0)
            {
                grdUploadStatus.DataBind();
                grdUploadStatus.Visible = true;
                lblStatus.Visible = false;
                lblValidation.Visible = false;
            }
            else
            {
                grdUploadStatus.Visible = true;
                lblStatus.InnerHtml = "<div>No files have been previously uploaded</div>";
                lblValidation.Visible = false;
                lblStatus.Visible = true;
            }

        }
        #endregion

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            ErrorFilePath = Session["UPLOAD_ERROR_FILE_PATH"].ToString();

            DownloadErrorFile(ErrorFilePath);

        }

        protected void btnViewRecentUploads_Click(object sender, EventArgs e)
        {
            ShowUploadGrid();
            btnDownload.Visible = false;
            btnProcess.Visible = false;
        }


        protected void grdUploadStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnLink = (LinkButton)e.Row.FindControl("lnkbtnFileName");
                LinkButton btnErrorLink = (LinkButton)e.Row.FindControl("lnkbtnErrorFileName");

                btnLink.Text = e.Row.Cells[7].Text;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;

                //If no error were found set the text for the error link download button to blank.
                if (e.Row.Cells[10].Text == "0")
                {
                    btnErrorLink.Text = string.Empty;
                    btnErrorLink.Enabled = false;
                }
                else
                {
                    if (e.Row.Cells[11].Text == "INVALID FILE FOUND")
                    {
                        //INVALID FILE FOUND
                        btnErrorLink.Text = "Invalid File Uploaded";
                        btnErrorLink.Enabled = false;

                    }
                    else
                    {
                        //INVALID FILE FOUND
                        btnErrorLink.Text = "Error";
                    }

                }

                btnLink.CommandArgument = ((GridViewRow)e.Row).RowIndex.ToString();
                btnErrorLink.CommandArgument = ((GridViewRow)e.Row).RowIndex.ToString();
            }

        }

        protected void grdUploadStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //(sender as GridView).Rows[0].Cells[7]
            int Index = -1;
            GridView gvr = (GridView)sender;
            if (e.CommandName.ToUpper() == "SELECTORIGINAL")
            {
                Index = int.Parse(e.CommandArgument.ToString());
                string OrginalFile = gvr.Rows[Index].Cells[8].Text;
                DownloadErrorFile(OrginalFile);
            }
            if (e.CommandName.ToUpper() == "SELECTERROR")
            {
                Index = int.Parse(e.CommandArgument.ToString());
                string ErrorFile = gvr.Rows[Index].Cells[9].Text;
                DownloadErrorFile(ErrorFile);
            }
        }


        protected void grdUploadStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //GetFileUploadStatusByUser is inherited therefore CCFileUploadBLL and PamFileUploadBLL can both be used
            grdUploadStatus.DataSource = UpLoadStatusManager.GetFileUploadStatusByUser(AccountInfo.UserId);
            grdUploadStatus.PageIndex = e.NewPageIndex;
            grdUploadStatus.DataBind();
        }






    }
}
