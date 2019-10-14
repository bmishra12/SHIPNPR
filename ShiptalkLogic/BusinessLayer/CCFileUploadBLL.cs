using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShiptalkLogic.DataLayer;
using System.Data;
using System.Xml;
using System.Diagnostics;
using ShiptalkLogic.BusinessObjects;
namespace ShiptalkLogic.BusinessLayer
{
    public class CCFileUploadBLL :  ILoader
    {

        #region Members
        AllCCTopics AllTopicsObject;
        ClientContactBatch ClientContactBatchObject;
        PrescriptiionDrugAssistance PrescriptiionDrugAssistanceObject;
        LowIncomeSubsidy LowIncomeSubsidyObject;
        OtherPrescriptionAssistance OtherPrescriptionAssistanceObject;
        ClientRaceEthinicity ClientRaceEthinicityObject;
        MedicarePartsAB MedicarePartsABObject;
        MedicareAdvantage MedicareAdvantageObject;
        MedicaidSupplement MedicaidSupplementObject;
        Medicaid MedicaidObject;
        Other OtherObject;
        NationwideCMSSpecialUseFields NationWideCMSSpecialFields;
        StateLocalSpecialUseFields StateLocalSpeciaFields; 
        protected List<absRecordValidate> ValidationObjects = new List<absRecordValidate>();
        public bool IsValidationSuccessful = true;
        string[] CCParsedFields = new string[126];
        protected int? UserID = 0;
        #endregion
       
        
        #region Methods


        public static int AddClientContact(ClientContact CCRec)
        {
            CCFDAL CCInserter = new CCFDAL();
            return CCInserter.CreateClientContact(CCRec);
        }

        public static int GetClientContactID(string BatchStateUniqueID, int AgencyID)
        {
            return FileUploadDAL.GetClientContactID(BatchStateUniqueID, AgencyID);
        }

        public static void UpdateClientContact(ClientContact CCRec)
        {
            CCFDAL CCUpdater = new CCFDAL();
            CCRec.Id = CCFileUploadBLL.GetClientContactID(CCRec.BatchStateUniqueID, CCRec.AgencyId);
            CCUpdater.UpdateClientContact(CCRec);
        }

        public static void DeleteClientContact(string StateFIPS,string AgencyCode, string @BatchStateUniqueID)
        {
            CCFDAL CCDelete = new CCFDAL();
            CCDelete.DeleteUploadedClientContactRecord(StateFIPS,AgencyCode, BatchStateUniqueID);
        }

        public static string GetSpecialFieldName(DateTime DateOfContact, FormType FrmType, int Ordinal, string StateFips)
        {
            return SpecialFieldsDAL.GetSpecialFieldName(DateOfContact, FrmType, Ordinal, StateFips);
        }

        public static SpecialFieldValue GetSpecialFieldInfo(DateTime DateOfContact, FormType FrmType, int Ordinal, string StateFips)
        {
            return SpecialFieldsDAL.GetSpecialFieldInfo(DateOfContact, FrmType, Ordinal, StateFips);
        }
        //
        public static DataTable GetSpecialFieldInformation(DateTime DateOfContact, FormType FrmType, string StateFips)
        {
            return SpecialFieldsDAL.GetSpecialFieldInformation(DateOfContact, FrmType, StateFips);
        }
        //

        public static string GetAssignedClientID( string StateSpecificClientID, string AgencyCode, string StateFIPS)
        {
            string ClientID = FileUploadDAL.FindExistingAutoAssignedClientID(StateSpecificClientID, AgencyCode, StateFIPS);
            if (string.IsNullOrEmpty(ClientID))
            {
                ClientID = FileUploadDAL.GetNextAutoAssignedClientID(AgencyCode);
            }

            return ClientID.Trim();

        }

        /// <summary>
        /// Intialize the objects used to process data.
        /// </summary>
        public void Initialize()
        {
            MedicareAdvantageObject = null;
            MedicarePartsABObject = null;
            MedicaidSupplementObject = null;
            MedicaidObject = null;
            OtherObject = null;
            ClientContactBatchObject = null;
            PrescriptiionDrugAssistanceObject = null;
            LowIncomeSubsidyObject = null;
            OtherPrescriptionAssistanceObject = null;
            ClientRaceEthinicityObject = null;
            NationWideCMSSpecialFields = null;
            AllTopicsObject = null;

            AllTopicsObject = new AllCCTopics();
            MedicareAdvantageObject = new MedicareAdvantage();
            MedicarePartsABObject = new MedicarePartsAB();
            MedicaidSupplementObject = new MedicaidSupplement();
            MedicaidObject = new Medicaid();
            OtherObject = new Other();
            ClientContactBatchObject = new ClientContactBatch();
            PrescriptiionDrugAssistanceObject = new PrescriptiionDrugAssistance();
            LowIncomeSubsidyObject = new LowIncomeSubsidy();
            OtherPrescriptionAssistanceObject = new OtherPrescriptionAssistance();
            ClientRaceEthinicityObject = new ClientRaceEthinicity();
            NationWideCMSSpecialFields = new NationwideCMSSpecialUseFields();
            StateLocalSpeciaFields = new StateLocalSpecialUseFields();
        }




        
        public void Load(string[] Record, string UserState, int ShipUserID)
        {
            UserID = ShipUserID;
            CCParsedFields = Record;
            ClientContactBatchObject.Load(CCParsedFields, UserState, ShipUserID);
            ClientRaceEthinicityObject.Load(CCParsedFields);
            PrescriptiionDrugAssistanceObject.Load(CCParsedFields);
            LowIncomeSubsidyObject.Load(CCParsedFields);
            OtherPrescriptionAssistanceObject.Load(CCParsedFields);

            MedicarePartsABObject.Load(CCParsedFields);
            MedicareAdvantageObject.Load(CCParsedFields);
            MedicaidSupplementObject.Load(CCParsedFields);
            MedicaidObject.Load(CCParsedFields);
            OtherObject.Load(CCParsedFields);
            NationWideCMSSpecialFields.Load(CCParsedFields);
            StateLocalSpeciaFields.Load(CCParsedFields);
            AllTopicsObject.Load(CCParsedFields);
        }


        public string Validate()
        {
            ValidationErrors = string.Empty;
            ValidationObjects.Clear();
            ValidationObjects.Add(ClientContactBatchObject);
            ValidationObjects.Add(ClientRaceEthinicityObject);
            ValidationObjects.Add(PrescriptiionDrugAssistanceObject);
            ValidationObjects.Add(LowIncomeSubsidyObject);
            ValidationObjects.Add(OtherPrescriptionAssistanceObject);
            ValidationObjects.Add(MedicareAdvantageObject);
            ValidationObjects.Add(MedicarePartsABObject);
            ValidationObjects.Add(MedicaidSupplementObject);
            ValidationObjects.Add(MedicaidObject);
            ValidationObjects.Add(OtherObject);
            ValidationObjects.Add(NationWideCMSSpecialFields);
            ValidationObjects.Add(StateLocalSpeciaFields);
            ValidationObjects.Add(AllTopicsObject);

            foreach (absRecordValidate ObjectToValidate in ValidationObjects)
            {
                bool retvalue = ObjectToValidate.AttemptValidate();
                this.IsValidationSuccessful = this.IsValidationSuccessful && retvalue;
            }
           

            //Errors were found so we retun the errors to the client to displayed and
            //saved.
            return this.ValidationErrors;
                
          

        }
            
        #endregion

        #region Properties
        string _ValidationErrors = string.Empty;
        public string ValidationErrors
        {
            get
            {
                string _ValidationErrors = string.Empty;
                foreach (absRecordValidate ValidateRecord in ValidationObjects)
                {
                    _ValidationErrors = _ValidationErrors + ValidateRecord.FormattedErrors;
                }

                return _ValidationErrors;
            }
            set
            {
                _ValidationErrors = value;
            }
        }

       public string[] ParsedFields
        {
            get
            {
                return CCParsedFields;
            }
        }

        #endregion


        #region InnerClass
        public class ClientContactBatch : absRecordValidate
        {

            string _ValidState = string.Empty;
            [PamProperty("Action", "Client Contact")]
            public string Action { get; set; }

            [PamProperty("State FIPS Code", "Client Contact")]
            public string StateFIPSCode { get; set; }

            [PamProperty("BatchStateUniqueID is missing", "Client Contact")]
            public string BatchStateUniqueID { get; set; }


            [PamProperty("First Name", "Client Contact")]
            public string FirstName { get; set; }

            [PamProperty("Last Name", "Client Contact")]
            public string LastName { get; set; }


            [PamProperty("Phone Number", "Client Contact")]
            public string PhoneNumber { get; set; }



            [PamProperty("Representative First Name", "Client Contact")]
            public string RepresentativeFirstName { get; set; }

            [PamProperty("Representative Last Name", "Client Contact")]
            public string RepresentativeLastName { get; set; }



            [PamProperty("How did client learn about the SHIP", "Client Contact")]
            public string HowClientLearnAboutSHIP { get; set; }


            [PamProperty("Client Resident Zip Code", "Client Contact")]
            public string ClientResidentZipCode { get; set; }

            [PamProperty("Fips County Code", "Client Contact")]
            public string FipsCountyCode { get; set; }


            [PamProperty("Counserlors UserId", "Client Contact")]
            public string CounserlorsUserId { get; set; }

            [PamProperty("AgencyCode", "Client Contact")]
            public string AgencyCode { get; set; }

            [PamProperty("FIPS County Code of Counselor Location", "Client Contact")]
            public string FIPSCountyCodeCounselorLocation { get; set; }


            [PamProperty("Zip code of counselor location", "Client Contact")]
            public string ZipCodeCounselorLocation { get; set; }


            [PamProperty("Method of contact ", "Client Contact")]
            public string MethodOfContact { get; set; }


            [PamProperty("Date of contact ", "Client Contact")]
            public string DateOfContact { get; set; }


            [PamProperty("FirstVSContinueContact", "Client Contact")]
            public string FirstVSContinueContact { get; set; }


            [PamProperty("ClientAgeGroup", "Client Contact")]
            public string ClientAgeGroup { get; set; }


            [PamProperty("ClientGender", "Client Contact")]
            public string ClientGender { get; set; }

            [PamProperty("StateSpecificClientID", "Client Contact")]
            public string StateSpecificClientID { get; set; }

            

            /// <summary>
            /// Loads Contact Client data from a fields array.
            /// </summary>
            /// <param name="Fields">Array of strings</param>
            /// <param name="UserState">The state the user is currently part of</param>
            public void Load(string[] Fields, string UserState, int ShipUserID)
            {
                _ValidState = UserState;
                Action = Fields[0];
                StateFIPSCode = Fields[1];
                RecordNum = Fields[2];
                BatchStateUniqueID = Fields[2];
                StateSpecificClientID = Fields[3];
                FirstName = Fields[4];
                LastName = Fields[5];
                PhoneNumber = Fields[6];
                RepresentativeFirstName = Fields[7];
                RepresentativeLastName = Fields[8];
                HowClientLearnAboutSHIP = Fields[9];
                ClientResidentZipCode = Fields[10];
                FipsCountyCode  = Fields[11];
                CounserlorsUserId = Fields[12];
                AgencyCode = Fields[13];
                FIPSCountyCodeCounselorLocation = Fields[14];
                ZipCodeCounselorLocation = Fields[15];
                MethodOfContact = Fields[16];
                DateOfContact = Fields[17];
                FirstVSContinueContact = Fields[18];
                ClientAgeGroup = Fields[19];
                ClientGender = Fields[20];
               
                UserID = ShipUserID;

            }


            /// <summary>
            /// Validate the data loaded into object.
            /// </summary>
            /// <returns></returns>
            public override bool AttemptValidate()
            {
                bool IsValid = true;
                
                //RecordNum
                if (!IsRequired(RecordNum))
                {
                    GenerateError(this.GetType(), "Record ID", string.Empty, "No record ID found.");
                    IsValid = false;
                    
                }
                //StateSpecificClientID
                if (!ValidateRequiredMaxLength(StateSpecificClientID, 40))
                {
                    GenerateError(this.GetType(), "StateSpecificClientID", RecordNum, "State Specific Client ID " + ErrMsg);
                    IsValid = false;
                }

                //StateFIPSCode
                if (!ValidateRequiredMaxLength(StateFIPSCode, 2))
                {
                    GenerateError(this.GetType(), "StateFIPSCode", RecordNum, "State FIPS code is missing.");
                    IsValid = false;
                }
                else
                {
                    
                    //If this is not an admin check the state to ensure they are permitted to upload
                    //for that state.  Only sys Admins can upload for any state.
                    if (_ValidState.ToUpper() != "ADMIN")
                    {
                        //Check if the STATE FIPCODE exist and if user has the same state code
                        if (!LookupDAL.IsStateFipCodeValid(_ValidState))
                        {
                            //Check if the StateFIPCode is a substate
                            GenerateError(this.GetType(), "StateFIPSCode", RecordNum, "Invalid state fips code found.");
                            IsValid = false;
                        }
                        else
                        {
                            //Check if state code is a sub state
                            if ((StateFIPSCode != _ValidState) && !FileUploadDAL.IsSubStateValid(StateFIPSCode, UserID.Value))
                            {
                                //Check if the StateFIPCode is a substate
                                GenerateError(this.GetType(), "StateFIPSCode", RecordNum, "Invalid state fips code found.");
                                IsValid = false;

                            }
                    
                        }
                    }
                    else
                    {
                        //Check if the STATE FIPCODE exist and if user has the same state code
                        if (!LookupDAL.IsStateFipCodeValid(StateFIPSCode))
                        {
                            GenerateError(this.GetType(), "StateFIPSCode", RecordNum, "Invalid state fips code found.");
                            IsValid = false;
                        }
                    }
                }
                
 

                //Agency Code
                if (!ValidateRequiredMaxLength(AgencyCode, 6))
                {
                    GenerateError(this.GetType(), "AgencyCode", RecordNum, ErrMsg);
                    IsValid = false;
                }
                else
                {
                    //GetUserSubStateRegionProfiles
                    //UserSubStateRegionDAL.GetUserSubStateRegionalProfiles();
                    if (!IsAgencyCodeValid(AgencyCode,_ValidState,StateFIPSCode))
                    {
                        GenerateError(this.GetType(), "AgencyCode", RecordNum, "Agency code could not be found for the state (FIPs)");
                        IsValid = false;
                    }
                }

                //BatchStateUniqueID
                if (!ValidateRequiredMaxLength(BatchStateUniqueID, 50))
                {
                    GenerateError(this.GetType(), "BatchStateUniqueID", RecordNum, ErrMsg);
                    IsValid = false;
                }

                
                //Action 
                if (!IsRequired(Action) || !IsRequired(BatchStateUniqueID))
                {
                    GenerateError(this.GetType(), "Action", RecordNum, ErrMsg);
                    IsValid = false;
                }
                else
                {
                    if (Action != "A" && Action != "U" && Action != "D")
                    {
                        GenerateError(this.GetType(), "Action", RecordNum, "Invalid value for action.");
                        IsValid = false;
                    }
                    else
                    {

                        if (!IsValidLength(Action, 1))
                        {
                            GenerateError(this.GetType(), "Action", RecordNum, ErrMsg);
                            IsValid = false;
                        }
                        else
                        {
                            bool RecordExists = IsClientContactRecordUploaded(AgencyCode, BatchStateUniqueID);
                            if ((Action.ToUpper() == "A"))
                            {
                                //If Record has been uploaded this is an error.
                                if (RecordExists)
                                {
                                    GenerateError(this.GetType(), "Action", RecordNum, "Record has been previously uploaded.");
                                    IsValid = false;
                                }
                            }
                            else
                            {
                                //If this is an update or delete verify we have previously uploaded an existing record.
                                if (Action.ToUpper() == "U" || Action.ToUpper() == "D")
                                {
                                    //See if records has been previously uploaded. 
                                    if (!RecordExists)
                                    {
                                        GenerateError(this.GetType(), "Action", RecordNum, "Record does not exists for update or delete.");
                                    }

                                }
                            }
                        }
                    }
                   

                }


                //DateOfContact
                try
                {
                    DateTime dStart = Convert.ToDateTime(DateOfContact);

                    string EarliestStartDate = System.Configuration.ConfigurationManager.AppSettings["EarlyStartDate"].ToString();
                        if (string.IsNullOrEmpty(EarliestStartDate))
                        {
                            throw (new System.Configuration.ConfigurationErrorsException("Configuration exception missing EarlyStartDate"));
                        }
                        DateTime dtEarliestStartDate = Convert.ToDateTime(EarliestStartDate);

                        if (dStart < dtEarliestStartDate)
                        {

                            GenerateError(this.GetType(), "DateOfContact", RecordNum, "Invalid Date Of Contact.  Date before " + dtEarliestStartDate.ToLongDateString() + " not permitted.");
                            IsValid = false;
                        }

                }
                catch (System.FormatException exFormat)
                {
                    GenerateError(this.GetType(), "DateOfContact", RecordNum, "Invalidation check failed Date Of Contact. Invalid date format.");
                    IsValid = false;
                }


                
                //First Name
                if (!string.IsNullOrEmpty(FirstName))
                {
                    if (!IsValidLength(FirstName, 50))
                    {
                        GenerateError(this.GetType(), "FirstName", RecordNum, ErrMsg);
                    }
                }

                //Last Name
                if (!string.IsNullOrEmpty(LastName))
                {
                    if (!IsValidLength(LastName, 50))
                    {
                        GenerateError(this.GetType(), "LastName", RecordNum, ErrMsg);
                    }
                }

                //Phone Number
                if (!string.IsNullOrEmpty(PhoneNumber))
                {
                    if (!IsValidLength(PhoneNumber, 20))
                    {
                        GenerateError(this.GetType(), "PhoneNumber", RecordNum, ErrMsg);
                    }
                }


                //Representative First Name
                if (!string.IsNullOrEmpty(RepresentativeFirstName))
                {
                    if (!IsValidLength(RepresentativeFirstName, 50))
                    {
                        GenerateError(this.GetType(), "RepresentativeFirstName", RecordNum, ErrMsg);
                    }
                }


                //Representative Last Name
                if (!string.IsNullOrEmpty(RepresentativeLastName))
                {
                    if (!IsValidLength(RepresentativeLastName, 50))
                    {
                        GenerateError(this.GetType(), "RepresentativeLastName", RecordNum, ErrMsg);
                    }
                }


                //How did client learn about ship
                if (ValidateRequiredMaxLength(HowClientLearnAboutSHIP, 2))
                {
                    if (!IsNumeric(HowClientLearnAboutSHIP))
                    {
                        GenerateError(this.GetType(), "HowClientLearnAboutSHIP", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                    else
                    {
                        int Value = int.Parse(HowClientLearnAboutSHIP);
                        
                            if (!IsValidRange(1, 9, Value) && Value != 99)
                            {
                                GenerateError(this.GetType(), "HowClientLearnAboutSHIP", RecordNum, ErrMsg + ",99");
                                IsValid = false;
                            }
                    }
                }
                else
                {
                    GenerateError(this.GetType(), "HowClientLearnAboutSHIP", RecordNum, ErrMsg);
                    IsValid = false;
                }


                //Client Resident Zip Code
                if (!ValidateRequiredLength(ClientResidentZipCode, 5))
                {
                    GenerateError(this.GetType(), "ClientResidentZipCode", RecordNum, ErrMsg);
                    IsValid = false;
                }
                else
                {
                    //Sammit: zip code for ClientResidentZipCode = "99999" is valid
                    //Sammit: out of state zip code is allowed so check if the zip valid in the Country
                    //Bimal: Per Dennis commented the below loigc 04/28/11
                    //if (ClientResidentZipCode != "99999")
                    //{
                        if (!LookupDAL.IsZipCodeValid(ClientResidentZipCode))
                        {
                            GenerateError(this.GetType(), "ClientResidentZipCode", RecordNum, "Invalid value for client zip code");
                            IsValid = false;
                        }
                    //}
                }
                

                
                //Fips County Code
                if (!string.IsNullOrEmpty(FipsCountyCode))
                {
                    if (!IsValidLength(FipsCountyCode, 5))
                    {
                        IsValid = false;
                        GenerateError(this.GetType(), "FipsCountyCode", RecordNum, ErrMsg);
                    }
                    else
                    {
                        //Sammit: County code for Client = "99999" is valid
                        //Sammit: out of county code is allowed so check if the county valid in the Country
                        if (FipsCountyCode != "99999")
                        {
                            if (!LookupDAL.IsCountyCodeValid(FipsCountyCode))
                            {
                                GenerateError(this.GetType(), "FipsCountyCode", RecordNum, "Invalid value for County FIPS code");
                                IsValid = false;
                            }
                        }

                        //sammit: match the client county and zip  exception: county = "99999" ,zip = "99999"
                        //        this is implemented in the IsCountyCodeMatchForZip() function

                        if (!IsCountyCodeMatchForZip())
                        {
                            IsValid = false;
                            GenerateError(this.GetType(), "FipsCountyCode", RecordNum, "County FIPS code is not valid for State Zip code");
                        }

                    }

                }

                //CouselorUserId
                if (!ValidateRequiredMaxLength(CounserlorsUserId,6))
                {
                    GenerateError(this.GetType(), "CounserlorsUserId", RecordNum, ErrMsg);
                    IsValid = false;
                }
                else
                {
                    if (!IsNumeric(CounserlorsUserId))
                    {
                        GenerateError(this.GetType(), "CounserlorsUserId", RecordNum, ErrMsg);
                        IsValid = false;

                    }
                    else 
                    {
                        //Determine if this is a valid Counselor
                        var aUser = UserBLL.GetUser(int.Parse(CounserlorsUserId));
                        if (aUser == null || (aUser.GetAllDescriptorsForUser.Where(p => p.Key == Descriptor.Counselor.EnumValue<int>()).Count() == 0))
                        {
                            ErrMsg = "Counserlor ship user ID could not be found or not a valid Counselor. ";
                            GenerateError(this.GetType(), "CounserlorsUserId", RecordNum, ErrMsg);
                            IsValid = false;
                        }
                        else //check if the counselor is part of the the agency
                        {
                            if ( FileUploadDAL.IsUserHasAgencyAssociation(AgencyCode, int.Parse(CounserlorsUserId)) == false)
                            {
                                ErrMsg = "Counselor ship user ID could not be found in the Agency. ";
                                GenerateError(this.GetType(), "CounserlorsUserId", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                        }
                    }
                    
                }


                if (!string.IsNullOrEmpty(FIPSCountyCodeCounselorLocation))
                {
                    //FIPS County Code Counselor Location
                    if (!IsValidLength(FIPSCountyCodeCounselorLocation, 5))
                    {
                        GenerateError(this.GetType(), "FIPSCountyCodeCounselorLocation", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                    else
                    {
                        if (!IsCounselorCountyCodeMatchForZip())
                        {
                            GenerateError(this.GetType(), "FIPSCountyCodeCounselorLocation", RecordNum, "Counselor location county FIPS code does not match zip.");
                            IsValid = false;
                        }
                    }
                }
                
                //Zip Code of Counselor Location
                if (!ValidateRequiredMaxLength(ZipCodeCounselorLocation, 5))
                {
                    GenerateError(this.GetType(), "ZipCodeCounselorLocation", RecordNum, ErrMsg);
                    IsValid = false;
                }
                else
                {
                    if (ZipCodeCounselorLocation.Length != 5)
                    {
                        GenerateError(this.GetType(), "ZipCodeCounselorLocation", RecordNum, "Counselor’ zip code must contain 5 digits");
                        IsValid = false;
                    }
                   
                    if (!IsCounserlorZipCodeFromSameState() || ZipCodeCounselorLocation == "99999")
                    {
                        GenerateError(this.GetType(), "ZipCodeCounselorLocation", RecordNum, "Counselor’ zip code does not match state");
                        IsValid = false;
                    }
                }



                //Method Of Contact
                if (!ValidateRequiredMaxLength(MethodOfContact, 1))
                {
                    GenerateError(this.GetType(), "MethodOfContact", RecordNum, ErrMsg);
                    IsValid = false;
                }
                else
                {
                    if (!IsNumeric(MethodOfContact))
                    {
                        GenerateError(this.GetType(), "MethodOfContact", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                    else
                    {
                        
                        if(!IsValidRange(1,5,int.Parse(MethodOfContact)))
                        {
                            GenerateError(this.GetType(), "MethodOfContact", RecordNum, ErrMsg);
                            IsValid = false;
                        }
                    }

                }

                //Date of contact
                if (!ValidateRequiredMaxLength(DateOfContact, 20))
                {
                    GenerateError(this.GetType(), "DateOfContact", RecordNum, ErrMsg);
                    IsValid = false;
                }
                else
                {
                    try
                    {
                        DateOfContact = FormatDate(DateOfContact);
                    }
                    catch (ApplicationException exApp)
                    {
                        GenerateError(this.GetType(), "DateOfContact", RecordNum, "Date of Contact " + exApp.Message);
                        IsValid = false;
                    }
                }


                //First VS Continuing Contact
                if (!ValidateRequiredMaxLength(FirstVSContinueContact, 1))
                {
                    GenerateError(this.GetType(), "FirstVSContinueContact", RecordNum, ErrMsg);
                    IsValid = false;
                }
                else
                {
                    if (!IsNumeric(FirstVSContinueContact))
                    {
                        GenerateError(this.GetType(), "FirstVSContinueContact", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                    else
                    {
                        if(!IsValidRange(1,2,int.Parse(FirstVSContinueContact)))
                        {
                            GenerateError(this.GetType(), "FirstVSContinueContact", RecordNum, ErrMsg);
                            IsValid = false;
                        }

                    }
                }



                //Client Age Group
                if (!ValidateRequiredMaxLength(ClientAgeGroup, 1))
                {
                    GenerateError(this.GetType(), "ClientAgeGroup", RecordNum, ErrMsg);
                    IsValid = false;
                }
                else
                {
                    if (!IsNumeric(ClientAgeGroup))
                    {
                        GenerateError(this.GetType(), "ClientAgeGroup", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                    else
                    {
                        int _ClientAgeGroup = int.Parse(ClientAgeGroup);
                        if (!IsValidRange(1,4,_ClientAgeGroup) && _ClientAgeGroup != 9)
                        {
                                GenerateError(this.GetType(), "ClientAgeGroup", RecordNum, ErrMsg + ",9");
                                IsValid = false;
                        }

                    }
                }

                //Client Gender
                if (!ValidateRequiredMaxLength(ClientGender, 1))
                {
                    GenerateError(this.GetType(), "ClientGender", RecordNum, ErrMsg);
                    IsValid = false;
                }
                else
                {
                    if (!IsNumeric(ClientGender))
                    {
                        GenerateError(this.GetType(), "ClientGender", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                    else
                    {
                        int _ClientGender = int.Parse(ClientGender);
                        if (!IsValidRange(1, 2, _ClientGender) && _ClientGender != 9)
                        {
                            GenerateError(this.GetType(), "ClientGender", RecordNum, ErrMsg);
                            IsValid = false;
                        }
                        

                    }
                }

                return IsValid;
            }

            /// <summary>
            /// Formats Date
            /// </summary>
            /// <param name="UnFormattedDate"></param>
            /// <returns></returns>
            private string FormatDate(string UnFormattedDate)
            {


                char[] dtSep = { '/' };
                string[] DateParts = UnFormattedDate.Split(dtSep);

                if (UnFormattedDate.Length == 0)
                {
                    throw (new ApplicationException("Date invalid format"));
                }
                else
                {
                    try
                    {
                        string Yr = DateParts[2].Substring(0, 4);
                        DateTime dtContacted = new DateTime(int.Parse(Yr), int.Parse(DateParts[0]), int.Parse(DateParts[1]));
                        return dtContacted.ToString("MM/dd/yyyy");
                    }
                    catch (System.Exception exDateConversion)
                    {
                        throw (new ApplicationException("Date invalid format"));
                    }
                }

               
            }

            

            public bool IsCounserlorZipCodeFromSameState()
            {
                if (!LookupDAL.IsZipCodeValidForState(StateFIPSCode, ZipCodeCounselorLocation))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }

            public bool IsCountyCodeMatchForZip()
            {
                //special rules for client county and zip
                if (FipsCountyCode == "99999" || ClientResidentZipCode == "99999")
                    return true;

                //County code must be valid for the zip code 
                if (!LookupDAL.IsZipCodeValidForCounty(FipsCountyCode, ClientResidentZipCode))
                {
                    
                    return false;
                }
                else
                {
                    return true;
                }
            }

            public bool IsCounselorCountyCodeMatchForZip()
            {
                //County code must be valid for the zip code 
                if (!LookupDAL.IsZipCodeValidForCounty( FIPSCountyCodeCounselorLocation, ZipCodeCounselorLocation))
                {

                    return false;
                }
                else
                {
                    return true;
                }
            }
            public bool IsClientZipCodeFromSameState()
            {
                if (!LookupDAL.IsZipCodeValidForState(StateFIPSCode, this.ClientResidentZipCode))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }


            /// <summary>
            /// Determines if an existing record was alreay uploaded.
            /// </summary>
            /// <param name="AgencyCode"></param>
            /// <param name="BatchRecordID"></param>
            /// <returns></returns>
            public bool IsClientContactRecordUploaded(string AgencyCode, string BatchRecordID)
            {
                return FileUploadDAL.IsClientContactRecordUploaded(AgencyCode, BatchRecordID);
            }


        }

        public class AllCCTopics : absRecordValidate
        {
            //All topic validation
         
                   

            [PamProperty("All Topic validation", "At least one topic selection is required")]
            public string AllTopicvalidation { get; set; }


            //  Eligibility Screening
            public string EligibilityScreening { get; set; }

            //Benefit Explanation
           
            public string BenefitExplanation { get; set; }

            //Plans Comparison
           
            public string PlansComparison { get; set; }

            //Plans Enrollment/Disenrollment

            public string PlansEnrollmentDisenrollment { get; set; }


            //Claims/Billing
      
            public string ClaimsBilling { get; set; }

            //Appeals/Grievances
       
            public string AppealsGrievances { get; set; }

            //Fraud and Abuse
     
            public string FraudAndAbuse { get; set; }


            //MarketingSalesComplaintsOrIssues
         
            public string MarketingSalesComplaintsOrIssues { get; set; }


            //Quality of Care
         
            public string QualityOfCare { get; set; }


            //Plan Non-Renewal
          
            public string PlanNonRenewal { get; set; }

   
             public string EligibilityScreening1 {get; set;}
             public string BenefitExplanation1 {get; set;}
             public string ApplicationAssistance {get; set;}  
             public string ClaimsBilling1 {get; set;}  
             public string AppealsGrievances1 {get; set;}


             public string UnionEmployerPlan { get; set; }
             public string MilitaryDrugBenefits { get; set; }
             public string ManufacturerPrograms { get; set; }
             public string StatePharmaceuticalAssistancePrograms { get; set; }
             public string Other { get; set; }  
             
             public string EligibilityScreening2 {get; set;}
             public string BenefitExplanation2 {get; set;}
             public string ClaimsBilling2 {get; set;} 
             public string AppealsGrievances2 {get; set;}
             public string FraudAndAbuse2 {get; set;}  
             public string QualityOfCare2 {get; set;}  


             public string EligibilityScreening3 {get; set;}
             public string BenefitExplanation3 {get; set;}
             public string PlansComparison3 {get; set;}
             public string PlansEnrollmentDisenrollment3 {get; set;}
             public string ClaimsBilling3 {get; set;} 
             public string AppealsGrievances3 {get; set;}
             public string FraudAndAbuse3 {get; set;}  
             public string MarketingSalesComplaintsOrIssues3 {get; set;}
             public string QualityOfCare3 {get; set;}  
             public string PlanNonRenewal3 {get; set;}

             public string EligibilityScreening4 { get; set; }
             public string BenefitExplanation4 { get; set; }
             public string PlansComparison4 { get; set; }
             public string ClaimsBilling4 { get; set; }
             public string AppealsGrievances4 { get; set; }
             public string FraudAndAbuse4 { get; set; }
             public string MarketingSalesComplaintsOrIssues4 { get; set; }
             public string QualityOfCare4 { get; set; }
             public string PlanNonRenewal4 { get; set; }  

              
             public string MedicareSavingsPrograms { get; set; }
             public string MSPApplicationAssistance { get; set; }
             public string MedicaidSSINursingHome { get; set; }
             public string MedicaidApplicationAssistance { get; set; }
             public string MedicaidQMBClaims { get; set; }
             public string FraudAndAbuse5 { get; set; }
       

             public string LongTermCare { get; set; }
             public string LTCPartnership { get; set; }
             public string LTCOther { get; set; }
             public string MilitaryHealthBenefits { get; set; }
             public string EmployerFederal { get; set; }
             public string COBRA { get; set; }
             public string OtherHealthInsurance { get; set; }
             public string Other1 { get; set; }
       
                                     

            public void Load(string[] Fields)
            {
                EligibilityScreening = Fields[43];
                BenefitExplanation = Fields[44];
                PlansComparison = Fields[45];
                PlansEnrollmentDisenrollment = Fields[46];
                ClaimsBilling = Fields[47];
                AppealsGrievances = Fields[48];
                FraudAndAbuse = Fields[49];
                MarketingSalesComplaintsOrIssues = Fields[50];
                QualityOfCare = Fields[51];
                PlanNonRenewal = Fields[52];
// 10
                EligibilityScreening1 = Fields[53];
                BenefitExplanation1 = Fields[54];
                ApplicationAssistance = Fields[55];
                ClaimsBilling1 = Fields[56];
                AppealsGrievances1 = Fields[57];
//5
                UnionEmployerPlan = Fields[58];
                MilitaryDrugBenefits = Fields[59];
                ManufacturerPrograms = Fields[60];
                StatePharmaceuticalAssistancePrograms = Fields[61];
                Other = Fields[62];
//5
                EligibilityScreening2 = Fields[63];
                BenefitExplanation2 = Fields[64];
                ClaimsBilling2 = Fields[65];
                AppealsGrievances2 = Fields[66];
                FraudAndAbuse2 = Fields[67];
                QualityOfCare2 = Fields[68];
//6
                EligibilityScreening3 = Fields[69];
                BenefitExplanation3 = Fields[70];
                PlansComparison3 = Fields[71];
                PlansEnrollmentDisenrollment3 = Fields[72];
                ClaimsBilling3 = Fields[73];
                AppealsGrievances3 = Fields[74];
                FraudAndAbuse3 = Fields[75];
                MarketingSalesComplaintsOrIssues3 = Fields[76];
                QualityOfCare3 = Fields[77];
                PlanNonRenewal3 = Fields[78];
//10

                EligibilityScreening4 = Fields[79];
                BenefitExplanation4 = Fields[80];
                PlansComparison4 = Fields[81];
                ClaimsBilling4 = Fields[82];
                AppealsGrievances4 = Fields[83];
                FraudAndAbuse4 = Fields[84];
                MarketingSalesComplaintsOrIssues4 = Fields[85];
                QualityOfCare4 = Fields[86];
                PlanNonRenewal4 = Fields[87];
//9
                MedicareSavingsPrograms = Fields[88];
                MSPApplicationAssistance = Fields[89];
                MedicaidSSINursingHome = Fields[90];
                MedicaidApplicationAssistance = Fields[91];
                MedicaidQMBClaims = Fields[92];
                FraudAndAbuse5 = Fields[93];
//6
                LongTermCare = Fields[94];
                LTCPartnership = Fields[95];
                LTCOther = Fields[96];
                MilitaryHealthBenefits = Fields[97];
                EmployerFederal = Fields[98];
                COBRA = Fields[99];
                OtherHealthInsurance = Fields[100];
                Other1 = Fields[101];
    //8         

                RecordNum = Fields[2];


            }

            public void Load(ClientContact CCData)
            {

            }

            public override bool AttemptValidate()
            {
                // At least one Prescription Drug Assistance Topic selection is required
                bool IsValid = false;
                //EligibilityScreening

                if (IsBooleanTrue(EligibilityScreening)
                    || IsBooleanTrue(BenefitExplanation)
                    || IsBooleanTrue(PlansComparison)
                    || IsBooleanTrue(PlansEnrollmentDisenrollment)
                    || IsBooleanTrue(ClaimsBilling)
                    || IsBooleanTrue(AppealsGrievances)
                    || IsBooleanTrue(FraudAndAbuse)
                    || IsBooleanTrue(MarketingSalesComplaintsOrIssues)
                    || IsBooleanTrue(QualityOfCare)
                    || IsBooleanTrue(PlanNonRenewal)
                    || IsBooleanTrue(EligibilityScreening1)
                    || IsBooleanTrue(BenefitExplanation1)
                    || IsBooleanTrue(ApplicationAssistance)
                    || IsBooleanTrue(ClaimsBilling1)
                    || IsBooleanTrue(AppealsGrievances1)
                    || IsBooleanTrue(UnionEmployerPlan)
                    || IsBooleanTrue(MilitaryDrugBenefits)
                    || IsBooleanTrue(ManufacturerPrograms)
                    || IsBooleanTrue(StatePharmaceuticalAssistancePrograms)
                    || !string.IsNullOrEmpty(Other)
                    || IsBooleanTrue(EligibilityScreening2)
                    || IsBooleanTrue(BenefitExplanation2)
                    || IsBooleanTrue(ClaimsBilling2)
                    || IsBooleanTrue(AppealsGrievances2)
                    || IsBooleanTrue(FraudAndAbuse2)
                    || IsBooleanTrue(QualityOfCare2)
                    || IsBooleanTrue(EligibilityScreening3)
                    || IsBooleanTrue(BenefitExplanation3)
                    || IsBooleanTrue(PlansComparison3)
                    || IsBooleanTrue(PlansEnrollmentDisenrollment3)
                    || IsBooleanTrue(ClaimsBilling3)
                    || IsBooleanTrue(AppealsGrievances3)
                    || IsBooleanTrue(FraudAndAbuse3)
                    || IsBooleanTrue(MarketingSalesComplaintsOrIssues3)
                    || IsBooleanTrue(QualityOfCare3)
                    || IsBooleanTrue(PlanNonRenewal3)
                    || IsBooleanTrue(EligibilityScreening4)
                    || IsBooleanTrue(BenefitExplanation4)
                    || IsBooleanTrue(PlansComparison4)
                    || IsBooleanTrue(ClaimsBilling4)
                    || IsBooleanTrue(AppealsGrievances4)
                    || IsBooleanTrue(FraudAndAbuse4)
                    || IsBooleanTrue(MarketingSalesComplaintsOrIssues4)
                    || IsBooleanTrue(QualityOfCare4)
                    || IsBooleanTrue(PlanNonRenewal4)
                    || IsBooleanTrue(MedicareSavingsPrograms)
                    || IsBooleanTrue(MSPApplicationAssistance)
                    || IsBooleanTrue(MedicaidSSINursingHome)
                    || IsBooleanTrue(MedicaidApplicationAssistance)
                    || IsBooleanTrue(MedicaidQMBClaims)
                    || IsBooleanTrue(FraudAndAbuse5)
                    || IsBooleanTrue(LongTermCare)
                    || IsBooleanTrue(LTCPartnership)
                    || IsBooleanTrue(LTCOther)
                    || IsBooleanTrue(MilitaryHealthBenefits)
                    || IsBooleanTrue(EmployerFederal)
                    || IsBooleanTrue(COBRA)
                    || IsBooleanTrue(OtherHealthInsurance)
                    || !string.IsNullOrEmpty(Other1))
                   
                     { 
                    IsValid = true;
                    }
                       
         
                if (!IsValid)
                {


                    GenerateError(this.GetType(), "AllTopicvalidation", RecordNum, ErrMsg);
                  
                }
                return IsValid;
             


            }

        }


        public class PrescriptiionDrugAssistance : absRecordValidate
        {
            //Eligibility/Screening
            [PamProperty("Eligibility/Screening", "PrescriptionDrugAssistance")]
            public string EligibilityScreening { get; set; }


            //Benefit Explanation
            [PamProperty("Benefit Explanation", "PrescriptionDrugAssistance.")]
            public string BenefitExplanation { get; set; }

            //Plans Comparison
            [PamProperty("Plans Comparison", "PrescriptionDrugAssistance.")]
            public string PlansComparison { get; set; }

            //Plans Enrollment/Disenrollment
            [PamProperty("Plans Enrollment/Disenrollment", "PrescriptionDrugAssistance.")]
            public string PlansEnrollmentDisenrollment { get; set; }


            //Claims/Billing
            [PamProperty("Claims/Billing", "PrescriptionDrugAssistance.")]
            public string ClaimsBilling { get; set; }

            //Appeals/Grievances
            [PamProperty("Appeals/Grievances", "PrescriptionDrugAssistance.")]
            public string AppealsGrievances { get; set; }

            //Fraud and Abuse
            [PamProperty("Fraud and Abuse", "PrescriptionDrugAssistance.")]
            public string FraudAndAbuse { get; set; }


            //MarketingSalesComplaintsOrIssues
            [PamProperty("Marketing Sales Complaints or issues", "PrescriptionDrugAssistance.")]
            public string MarketingSalesComplaintsOrIssues { get; set; }


            //Quality of Care
            [PamProperty("Quality of care", "PrescriptionDrugAssistance.")]
            public string QualityOfCare { get; set; }


            //Plan Non-Renewal
            [PamProperty("Plan Non-Renewal", "PrescriptionDrugAssistance.")]
            public string PlanNonRenewal { get; set; }



            public void Load(string[] Fields)
            {
                EligibilityScreening = Fields[43];
                BenefitExplanation = Fields[44];
                PlansComparison = Fields[45];
                PlansEnrollmentDisenrollment = Fields[46];
                ClaimsBilling = Fields[47];
                AppealsGrievances = Fields[48];
                FraudAndAbuse = Fields[49];
                MarketingSalesComplaintsOrIssues = Fields[50];
                QualityOfCare = Fields[51];
                PlanNonRenewal = Fields[52];
                RecordNum = Fields[2];


            }

            public void Load(ClientContact CCData)
            {
            
            }

            public override bool AttemptValidate()
            {
                bool IsValid = true;
                //EligibilityScreening
                if (!string.IsNullOrEmpty(EligibilityScreening))
                {
                    if (!IsBooleanValue(EligibilityScreening))
                    {
                        GenerateError(this.GetType(), "EligibilityScreening", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Benefit Explanation
                if (!string.IsNullOrEmpty(BenefitExplanation))
                {
                    if (!IsBooleanValue(BenefitExplanation))
                    {
                        GenerateError(this.GetType(), "BenefitExplanation", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Plans Comparison
                if (!string.IsNullOrEmpty(PlansComparison))
                {
                    if (!IsBooleanValue(PlansComparison) )
                    {
                        GenerateError(this.GetType(), "PlansComparison", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }
                //Plans Enrollment/Disenrollment
                if (!string.IsNullOrEmpty(PlansEnrollmentDisenrollment))
                {
                    if (!IsBooleanValue(PlansEnrollmentDisenrollment) )
                    {
                        GenerateError(this.GetType(), "PlansEnrollmentDisenrollment", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Claims/Billing
                if (!string.IsNullOrEmpty(ClaimsBilling))
                {
                    if (!IsBooleanValue(ClaimsBilling) )
                    {
                        GenerateError(this.GetType(), "ClaimsBilling", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Appeals/Grievances
                if (!string.IsNullOrEmpty(AppealsGrievances))
                {
                    if (!IsBooleanValue(AppealsGrievances) )
                    {
                        GenerateError(this.GetType(), "AppealsGrievances", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //FraudAndAbuse
                if (!string.IsNullOrEmpty(FraudAndAbuse))
                {
                    if (!IsBooleanValue(FraudAndAbuse) )
                    {
                        GenerateError(this.GetType(), "FraudAndAbuse", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }


                //MarketingSalesComplaintsOrIssues
                if (!string.IsNullOrEmpty(MarketingSalesComplaintsOrIssues))
                {
                    if (!IsBooleanValue(MarketingSalesComplaintsOrIssues))
                    {
                        GenerateError(this.GetType(), "MarketingSalesComplaintsOrIssues", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //QualityOfCare
                if (!string.IsNullOrEmpty(QualityOfCare))
                {
                    if (!IsBooleanValue(QualityOfCare) )
                    {
                        GenerateError(this.GetType(), "QualityOfCare", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //PlanNonRenewal
                if (!string.IsNullOrEmpty(PlanNonRenewal))
                {
                    if (!IsBooleanValue(PlanNonRenewal))
                    {
                        GenerateError(this.GetType(), "PlanNonRenewal", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                return IsValid;


            }

        }


        public class LowIncomeSubsidy : absRecordValidate
        {
            //Eligibility/Screening
            [PamProperty("Eligibility/Screening", "Low income subsidy.")]
            public string EligibilityScreening { get; set; }


            //Benefit Explanation
            [PamProperty("Benefit Explanation", "Low income subsidy.")]
            public string BenefitExplanation { get; set; }

            //Application Assistance
            [PamProperty("Application Assistance", "Low income subsidy.")]
            public string ApplicationAssistance { get; set; }


            //Claims/Billing
            [PamProperty("Claims/Billing", "Low income subsidy.")]
            public string ClaimsBilling { get; set; }



            //Appeals/Grievances
            [PamProperty("Appeals/Grievances", "Low income subsidy.")]
            public string AppealsGrievances { get; set; }

           


            public void Load(string[] Fields)
            {
                EligibilityScreening = Fields[53];
                BenefitExplanation = Fields[54];
                ApplicationAssistance = Fields[55];
                ClaimsBilling = Fields[56];
                AppealsGrievances = Fields[57];
                RecordNum = Fields[2];

            }

            public override bool AttemptValidate()
            {
                bool IsValid = true;
                //EligibilityScreening
                if (!string.IsNullOrEmpty(EligibilityScreening))
                {
                    if (!IsBooleanValue(EligibilityScreening))
                    {
                        GenerateError(this.GetType(), "EligibilityScreening", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Benefit Explanation
                if (!string.IsNullOrEmpty(BenefitExplanation))
                {
                    if (!IsBooleanValue(BenefitExplanation))
                    {
                        GenerateError(this.GetType(), "BenefitExplanation", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Plans Comparison
                if (!string.IsNullOrEmpty(ApplicationAssistance))
                {
                    if (!IsBooleanValue(ApplicationAssistance))
                    {
                        GenerateError(this.GetType(), "ApplicationAssistance", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }
                
                //Claims/Billing
                if (!string.IsNullOrEmpty(ClaimsBilling))
                {
                    if (!IsBooleanValue(ClaimsBilling))
                    {
                        GenerateError(this.GetType(), "ClaimsBilling", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Appeals/Grievances
                if (!string.IsNullOrEmpty(AppealsGrievances))
                {
                    if (!IsBooleanValue(AppealsGrievances))
                    {
                        GenerateError(this.GetType(), "AppealsGrievances", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

               
                return IsValid;


            }

        }



        public class OtherPrescriptionAssistance : absRecordValidate
        {

            [PamProperty("Union/Employer Plan", "Other Prescription Assistance")]
            public string UnionEmployerPlan { get; set; }

            [PamProperty("Military Drug Benefits", "Other Prescription Assistance")]
            public string MilitaryDrugBenefits { get; set; }



            [PamProperty("Manufacturer Programs", "Other Prescription Assistance")]
            public string ManufacturerPrograms { get; set; }


            [PamProperty("State Pharmaceutical Assistance Programs", "Other Prescription Assistance")]
            public string StatePharmaceuticalAssistancePrograms { get; set; }

            [PamProperty("Other", "Other Prescription Assistance")]
            public string Other { get; set; }



           
            public void Load(string[] Fields)
            {
                UnionEmployerPlan = Fields[58];
                MilitaryDrugBenefits = Fields[59];
                ManufacturerPrograms = Fields[60];
                StatePharmaceuticalAssistancePrograms = Fields[61];
                Other = Fields[62];
                RecordNum = Fields[2];

            }


            public override bool AttemptValidate()
            {
                bool IsValid = true;
                if (!string.IsNullOrEmpty(UnionEmployerPlan))
                {
                    if (!IsBooleanValue(UnionEmployerPlan) )
                    {
                        GenerateError(this.GetType(), "UnionEmployerPlan", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }


                if (!string.IsNullOrEmpty(MilitaryDrugBenefits))
                {
                    if (!IsBooleanValue(MilitaryDrugBenefits))
                    {
                        GenerateError(this.GetType(), "MilitaryDrugBenefits", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }


                if (!string.IsNullOrEmpty(ManufacturerPrograms))
                {
                    if (!IsBooleanValue(ManufacturerPrograms) )
                    {
                        GenerateError(this.GetType(), "ManufacturerPrograms", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }


                if (!string.IsNullOrEmpty(StatePharmaceuticalAssistancePrograms))
                {
                    if (!IsBooleanValue(StatePharmaceuticalAssistancePrograms) )
                    {
                        GenerateError(this.GetType(), "StatePharmaceuticalAssistancePrograms", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                if (!string.IsNullOrEmpty(Other))
                {
                    if (!IsValidLength(Other,255))
                    {
                        GenerateError(this.GetType(), "Other", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                return IsValid;
               
                
            }
        }




        public class ClientRaceEthinicity : absRecordValidate
        {
            [PamProperty("Hispanic, Latino or Spanish Origin", "Client Ethnicity")]
            public string HispanicLatino{ get; set; }

            [PamProperty("White non hispanic", "Client Ethnicity")]
            public string WhiteNonHispanic { get; set; }


            [PamProperty("African American", "Client Ethnicity")]
            public string AfricanAmerican { get; set; }


            [PamProperty("American Indian or Alaska Native", "Client Ethnicity")]
            public string AmericanIndianOrAlaskaNative { get; set; }


            [PamProperty("American Indian or Alaska Native", "Client Ethnicity")]
            public string AsianIndian { get; set; }

            [PamProperty("Chinese", "Client Ethnicity")]
            public string Chinese { get; set; }

            [PamProperty("Filipino", "Client Ethnicity")]
            public string Filipino { get; set; }

            [PamProperty("Japanese", "Client Ethnicity")]
            public string Japanese { get; set; }


            [PamProperty("Korean", "Client Ethnicity")]
            public string Korean { get; set; }


            [PamProperty("Vietnamese", "Client Ethnicity")]
            public string Vietnamese { get; set; }

            [PamProperty("Native Hawaiian", "Client Ethnicity")]
            public string NativeHawaiian  { get; set; }

            [PamProperty("GuamanianOrChamorro", "Client Ethnicity")]
            public string GuamanianOrChamorro { get; set; }

            [PamProperty("Samoan", "Client Ethnicity")]
            public string Samoan { get; set; }


            [PamProperty("OtherAsian", "Client Ethnicity")]
            public string OtherAsian { get; set; }

            [PamProperty("OtherPacificIslander", "Client Ethnicity")]
            public string OtherPacificIslander { get; set; }

            [PamProperty("SomeOtherRaceEthnicity", "Client Ethnicity")]
            public string SomeOtherRaceEthnicity { get; set; }

            [PamProperty("NotCollected", "Not Collected")]
            public string NotCollected { get; set; }

            [PamProperty("Client Primary Language", "Client Primary Language Other than english")]
            public string ClientPrimaryLanguage { get; set; }

            [PamProperty("ClientMonthlyIncome", "Client Monthly Income")]
            public string ClientMonthlyIncome { get; set; }

            [PamProperty("ClientAssets", "Client Assests.")]
            public string ClientAssets { get; set; }

            [PamProperty("ReceivingSocialSecurity", "Receiving or Applying for Social Security Disability or Medicare Disability")]
            public string ReceivingSocialSecurity{ get; set; }


            [PamProperty("DualEligibleMentalIllness", "Dual Eligible with Mental Illness Mental Disability")]
            public string DualEligibleMentalIllness{ get; set; }

            [PamProperty("ClientAgeGroup", "Client Age Group")]
            public string ClientAgeGroup { get; set; }


            
            public void Load(string[] Fields)
            {
                RecordNum = Fields[2];
                HispanicLatino = Fields[21];
                WhiteNonHispanic = Fields[22];
                AfricanAmerican = Fields[23];
                AmericanIndianOrAlaskaNative = Fields[24];
                AsianIndian = Fields[25];
                Chinese = Fields[26];
                Filipino = Fields[27];
                Japanese = Fields[28];
                Korean = Fields[29];
                Vietnamese = Fields[30];
                NativeHawaiian = Fields[31];
                GuamanianOrChamorro = Fields[32];
                Samoan = Fields[33];
                OtherAsian = Fields[34];
                OtherPacificIslander = Fields[35];
                SomeOtherRaceEthnicity = Fields[36];
                NotCollected = Fields[37];
                ClientPrimaryLanguage = Fields[38];
                ClientMonthlyIncome = Fields[39];
                ClientAssets = Fields[40];
                ReceivingSocialSecurity = Fields[41];
                DualEligibleMentalIllness = Fields[42];
                ClientAgeGroup = Fields[19];
                RecordNum = Fields[2];
                
            }

            public override bool AttemptValidate()
            {
                bool bEthnicitySelected = false;
                bool IsValid = true;
                if (!string.IsNullOrEmpty(HispanicLatino))
                {
                    if (!IsBooleanValue(HispanicLatino))
                    {
                        GenerateError(this.GetType(), "HispanicLatino", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if(!bEthnicitySelected)
                            bEthnicitySelected = IsBooleanTrue(HispanicLatino);
                    }
                }
               

                if (!string.IsNullOrEmpty(WhiteNonHispanic))
                {
                    if (!IsBooleanValue(WhiteNonHispanic) )
                    {
                        GenerateError(this.GetType(), "WhiteNonHispanic", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if (!bEthnicitySelected)
                            bEthnicitySelected = IsBooleanTrue(WhiteNonHispanic);
                    }
                }
               

                if (!string.IsNullOrEmpty(AfricanAmerican))
                {
                    if (!IsBooleanValue(AfricanAmerican))
                    {
                        GenerateError(this.GetType(), "AfricanAmerican", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if (!bEthnicitySelected)
                            bEthnicitySelected = IsBooleanTrue(AfricanAmerican); ;
                    }
                }
               

                if (!string.IsNullOrEmpty(AmericanIndianOrAlaskaNative))
                {
                    if (!IsBooleanValue(AmericanIndianOrAlaskaNative))
                    {
                        GenerateError(this.GetType(), "AmericanIndianOrAlaskaNative", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if (!bEthnicitySelected)
                            bEthnicitySelected = IsBooleanTrue(AmericanIndianOrAlaskaNative);
                    }
                }
               

                if (!string.IsNullOrEmpty(AsianIndian))
                {
                    if (!IsBooleanValue(AsianIndian))
                    {
                        GenerateError(this.GetType(), "AsianIndian", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if (!bEthnicitySelected)
                            bEthnicitySelected =  IsBooleanTrue(AsianIndian);
                    }
                }
               
                if (!string.IsNullOrEmpty(Chinese))
                {
                    if (!IsBooleanValue(Chinese) )
                    {
                        GenerateError(this.GetType(), "Chinese", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if (!bEthnicitySelected)
                            bEthnicitySelected = IsBooleanTrue(Chinese);
                    }
                }
               
                if (!string.IsNullOrEmpty(Filipino))
                {
                    if (!IsBooleanValue(Filipino) )
                    {
                        GenerateError(this.GetType(), "Filipino", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if (!bEthnicitySelected)
                            bEthnicitySelected = IsBooleanTrue(Filipino);
                    }
                }
               
                //Japanese
                if (!string.IsNullOrEmpty(Japanese))
                {
                    if (!IsBooleanValue(Japanese) )
                    {
                        GenerateError(this.GetType(), "Japanese", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if (!bEthnicitySelected)
                            bEthnicitySelected = IsBooleanTrue(Japanese);
                    }
                }
               
                //Korean
                if (!string.IsNullOrEmpty(Korean))
                {
                    if (!IsBooleanValue(Korean) )
                    {
                        GenerateError(this.GetType(), "Korean", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if (!bEthnicitySelected)
                            bEthnicitySelected = IsBooleanTrue(Korean);
                    }
                }
               
                //Vietnamese
                if (!string.IsNullOrEmpty(Vietnamese))
                {
                    if (!IsBooleanValue(Vietnamese))
                    {
                        GenerateError(this.GetType(), "Vietnamese", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if (!bEthnicitySelected)
                            bEthnicitySelected = IsBooleanTrue(Vietnamese);
                    }
                }
               
                //NativeHawaiian
                if (!string.IsNullOrEmpty(NativeHawaiian))
                {
                    if (!IsBooleanValue(NativeHawaiian) )
                    {
                        GenerateError(this.GetType(), "NativeHawaiian", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if (!bEthnicitySelected)
                            bEthnicitySelected = IsBooleanTrue(NativeHawaiian);
                    }
                }
               
                //GuamanianOrChamorro
                if (!string.IsNullOrEmpty(GuamanianOrChamorro))
                {
                    if (!IsBooleanValue(GuamanianOrChamorro) )
                    {
                        GenerateError(this.GetType(), "GuamanianOrChamorro", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if (!bEthnicitySelected)
                            bEthnicitySelected = IsBooleanTrue(GuamanianOrChamorro);
                    }
                }
               
                //Samoan
                if (!string.IsNullOrEmpty(Samoan))
                {
                    if (!IsBooleanValue(Samoan) )
                    {
                        GenerateError(this.GetType(), "Samoan", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if (!bEthnicitySelected)
                            bEthnicitySelected = IsBooleanTrue(Samoan);
                    }
                }
               
                //OtherAsian
                if (!string.IsNullOrEmpty(OtherAsian))
                {
                    if (!IsBooleanValue(OtherAsian) )
                    {
                        GenerateError(this.GetType(), "OtherAsian", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if (!bEthnicitySelected)
                            bEthnicitySelected = IsBooleanTrue(OtherAsian);
                    }
                }
               
                //OtherPacificIslander
                if (!string.IsNullOrEmpty(OtherPacificIslander))
                {
                    if (!IsBooleanValue(OtherPacificIslander) )
                    {
                        GenerateError(this.GetType(), "OtherPacificIslander", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if (!bEthnicitySelected)
                            bEthnicitySelected = IsBooleanTrue(OtherPacificIslander);
                    }
                }
               
                //SomeOtherRaceEthnicity
                if (!string.IsNullOrEmpty(SomeOtherRaceEthnicity))
                {
                    if (!IsBooleanValue(SomeOtherRaceEthnicity) )
                    {
                        GenerateError(this.GetType(), "SomeOtherRaceEthnicity", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                        if (!bEthnicitySelected)
                            bEthnicitySelected = IsBooleanTrue(SomeOtherRaceEthnicity);
                    }
                }
               
                //NotCollected
                if (!string.IsNullOrEmpty(NotCollected))
                {

                    if (!IsBooleanValue(NotCollected))
                    {
                        GenerateError(this.GetType(), "NotCollected", RecordNum, "Invalid value found");
                        IsValid = false;
                    }
                    else
                    {
                            if( !bEthnicitySelected )
                                bEthnicitySelected = IsBooleanTrue(NotCollected);
                        
                    }
                }

                //If no ethnicity has been selected generate error.
                if( !bEthnicitySelected )
                { 
                    GenerateError(this.GetType(), "NotCollected", RecordNum, "Ethinicity of client was not specified");
                    IsValid = false;
                    
                }
               
                //ClientPrimaryLanguage
                if (IsRequired(ClientPrimaryLanguage))
                {
                    if (IsNumeric(ClientPrimaryLanguage))
                    {
                        int PrimLanguage = int.Parse(ClientPrimaryLanguage);
                        if (!IsValidRange(1, 2, PrimLanguage) && PrimLanguage != 9)
                        {
                            GenerateError(this.GetType(), "ClientPrimaryLanguage", RecordNum, ErrMsg + ",9");
                            IsValid = false;
                        }
                    }
                    else
                    {
                        GenerateError(this.GetType(), "ClientPrimaryLanguage", RecordNum, ErrMsg);
                        IsValid = false;
                    }


                }
                else
                {
                    GenerateError(this.GetType(), "ClientPrimaryLanguage", RecordNum, ErrMsg);
                    IsValid = false;
                }

                //ClientMonthlyIncome
                if (IsRequired(ClientMonthlyIncome))
                {
                    if (IsNumeric(ClientMonthlyIncome))
                    {
                        if (!IsValidRange(1, 2, int.Parse(ClientMonthlyIncome)) && ClientMonthlyIncome != "9")
                        {
                            GenerateError(this.GetType(), "ClientMonthlyIncome", RecordNum, ErrMsg + ",9");
                            IsValid = false;
                        }
                       
                    }
                    else
                    {
                        GenerateError(this.GetType(), "ClientMonthlyIncome", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }
                else
                {
                    GenerateError(this.GetType(), "ClientMonthlyIncome", RecordNum, ErrMsg);
                    IsValid = false;
                }

                //ClientAssets
                if (IsRequired(ClientAssets))
                {
                    if (IsNumeric(ClientAssets))
                    {
                        if (!IsValidRange(1, 2, int.Parse(ClientAssets)) && ClientAssets != "9")
                        {
                            GenerateError(this.GetType(), "ClientAssets", RecordNum, ErrMsg + ",9");
                            IsValid = false;
                        }
                       
                    }
                    else
                    {
                        GenerateError(this.GetType(), "ClientAssets", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }
                else
                {
                    GenerateError(this.GetType(), "ClientAssets", RecordNum, ErrMsg);
                    IsValid = false;
                }

                //ReceivingSocialSecurity
                if (IsRequired(ReceivingSocialSecurity))
                {
                    if (!IsRequired(ClientAgeGroup))
                    {
                        ErrMsg = "Invalid value for disability.";
                        GenerateError(this.GetType(), "ClientAssets", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                    else
                    {
                        //Disability MUST be Numeric Value 2 (e.g., 'No') if Client Age Group is either 2, 3 or 4.
                        if ((ClientAgeGroup == "2" || ClientAgeGroup == "3" || ClientAgeGroup == "4") && ReceivingSocialSecurity != "2")
                        {
                            ErrMsg = "Invalid value found for disability client age group";
                            GenerateError(this.GetType(), "ReceivingSocialSecurity", RecordNum, ErrMsg);
                            IsValid = false;

                        }
                        else
                        {
                            if (IsNumeric(ReceivingSocialSecurity))
                            {
                                if (!IsValidRange(1, 2, int.Parse(ReceivingSocialSecurity)) && ReceivingSocialSecurity != "9")
                                {
                                    ErrMsg = "Invalid value found.  Number outside of permitted values 1,2,9";
                                    GenerateError(this.GetType(), "ReceivingSocialSecurity", RecordNum, ErrMsg);
                                    IsValid = false;
                                }
                            }
                            else
                            {
                                GenerateError(this.GetType(), "ReceivingSocialSecurity", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                        }

                    }
                }
                else
                {
                    GenerateError(this.GetType(), "ReceivingSocialSecurity", RecordNum, ErrMsg);
                    IsValid = false;
                }

                //DualEligibleMentalIllness
                if (!string.IsNullOrEmpty(DualEligibleMentalIllness))
                {
                    if (IsNumeric(DualEligibleMentalIllness))
                    {
                        if (!IsValidRange(1, 2, int.Parse(DualEligibleMentalIllness)) && DualEligibleMentalIllness != "9")
                        {
                            ErrMsg = "Invalid value found. Number outside permitted values 1,2,9";
                            GenerateError(this.GetType(), "DualEligibleMentalIllness", RecordNum, ErrMsg);
                            IsValid = false;
                        }
                    }
                    else
                    {
                        GenerateError(this.GetType(), "DualEligibleMentalIllness", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }
                else
                {
                    GenerateError(this.GetType(), "DualEligibleMentalIllness", RecordNum, ErrMsg);
                    IsValid = false;
                }

                return false;
            }
        }



        public class MedicarePartsAB: absRecordValidate
        {
            //Eligibility/Screening
            [PamProperty("Eligibility/Screening", "Medicare parts A B.")]
            public string EligibilityScreening { get; set; }


            //Benefit Explanation
            [PamProperty("Benefit Explanation", "Medicare parts A B.")]
            public string BenefitExplanation { get; set; }

          

            //Claims/Billing
            [PamProperty("Claims/Billing", "Medicare parts A B.")]
            public string ClaimsBilling { get; set; }

            //Appeals/Grievances
            [PamProperty("Appeals/Grievances", "Medicare parts A B.")]
            public string AppealsGrievances { get; set; }

            //Fraud and Abuse
            [PamProperty("FraudAndAbuse", "Medicare parts A B.")]
            public string FraudAndAbuse { get; set; }

            //Quality of Care
            [PamProperty("QualityOfCare", "Medicare parts A B.")]
            public string QualityOfCare { get; set; }

            public void Load(string[] Fields)
            {
                RecordNum = Fields[2];
                EligibilityScreening = Fields[63];
                BenefitExplanation = Fields[64];
                ClaimsBilling = Fields[65];
                AppealsGrievances = Fields[66];
                FraudAndAbuse = Fields[67];
                QualityOfCare = Fields[68];
               

            }

            public override bool AttemptValidate()
            {
                bool IsValid = true;
                //EligibilityScreening
                if (!string.IsNullOrEmpty(EligibilityScreening))
                {
                    if (!IsBooleanValue(EligibilityScreening) )
                    {
                        GenerateError(this.GetType(), "EligibilityScreening", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Benefit Explanation
                if (!string.IsNullOrEmpty(BenefitExplanation))
                {
                    if (!IsBooleanValue(BenefitExplanation))
                    {
                        GenerateError(this.GetType(), "BenefitExplanation", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

               
                //Claims/Billing
                if (!string.IsNullOrEmpty(ClaimsBilling))
                {
                    if (!IsBooleanValue(ClaimsBilling) )
                    {
                        GenerateError(this.GetType(), "ClaimsBilling", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Appeals/Grievances
                if (!string.IsNullOrEmpty(AppealsGrievances))
                {
                    if (!IsBooleanValue(AppealsGrievances) )
                    {
                        GenerateError(this.GetType(), "AppealsGrievances", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //FraudAndAbuse
                if (!string.IsNullOrEmpty(FraudAndAbuse))
                {
                    if (!IsBooleanValue(FraudAndAbuse))
                    {
                        GenerateError(this.GetType(), "FraudAndAbuse", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //QualityOfCare
                if (!string.IsNullOrEmpty(QualityOfCare))
                {
                    if (!IsBooleanValue(QualityOfCare))
                    {
                        GenerateError(this.GetType(), "QualityOfCare", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                return IsValid;


            }

        }




        public class MedicareAdvantage : absRecordValidate
        {
            //Eligibility/Screening
            [PamProperty("Eligibility/Screening", "MedicareAdvantage.")]
            public string EligibilityScreening { get; set; }


            //Benefit Explanation
            [PamProperty("Benefit Explanation", "MedicareAdvantage.")]
            public string BenefitExplanation { get; set; }

            //Plans Comparison
            [PamProperty("Plans Comparison", "MedicareAdvantage.")]
            public string PlansComparison { get; set; }

            //Plans Enrollment/Disenrollment
            [PamProperty("Plans Enrollment/Disenrollment", "MedicareAdvantage.")]
            public string PlansEnrollmentDisenrollment { get; set; }


            //Claims/Billing
            [PamProperty("Claims/Billing", "MedicareAdvantage.")]
            public string ClaimsBilling { get; set; }

            //Appeals/Grievances
            [PamProperty("Appeals/Grievances", "MedicareAdvantage.")]
            public string AppealsGrievances { get; set; }

            //Fraud and Abuse
            [PamProperty("Fraud and Abuse", "MedicareAdvantage.")]
            public string FraudAndAbuse { get; set; }


            //MarketingSalesComplaintsOrIssues
            [PamProperty("Marketing Sales Complaints or issues", "MedicareAdvantage.")]
            public string MarketingSalesComplaintsOrIssues { get; set; }


            //Quality of Care
            [PamProperty("Quality of care", "MedicareAdvantage.")]
            public string QualityOfCare { get; set; }


            //Plan Non-Renewal
            [PamProperty("Plan Non-Renewal", "MedicareAdvantage.")]
            public string PlanNonRenewal { get; set; }



            public void Load(string[] Fields)
            {
                RecordNum = Fields[2];
                EligibilityScreening = Fields[69];
                BenefitExplanation = Fields[70];
                PlansComparison = Fields[71];
                PlansEnrollmentDisenrollment = Fields[72];
                ClaimsBilling = Fields[73];
                AppealsGrievances = Fields[74];
                FraudAndAbuse = Fields[75];
                MarketingSalesComplaintsOrIssues = Fields[76];
                QualityOfCare = Fields[77];
                PlanNonRenewal = Fields[78];


            }

            public override bool AttemptValidate()
            {
                bool IsValid = true;
                //EligibilityScreening
                if (!string.IsNullOrEmpty(EligibilityScreening))
                {
                    if (!IsBooleanValue(EligibilityScreening))
                    {
                        GenerateError(this.GetType(), "EligibilityScreening", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Benefit Explanation
                if (!string.IsNullOrEmpty(BenefitExplanation))
                {
                    if (!IsBooleanValue(BenefitExplanation))
                    {
                        GenerateError(this.GetType(), "BenefitExplanation", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Plans Comparison
                if (!string.IsNullOrEmpty(PlansComparison))
                {
                    if (!IsBooleanValue(PlansComparison))
                    {
                        GenerateError(this.GetType(), "PlansComparison", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }
                //Plans Enrollment/Disenrollment
                if (!string.IsNullOrEmpty(PlansEnrollmentDisenrollment))
                {
                    if (!IsBooleanValue(PlansEnrollmentDisenrollment))
                    {
                        GenerateError(this.GetType(), "PlansEnrollmentDisenrollment", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Claims/Billing
                if (!string.IsNullOrEmpty(ClaimsBilling))
                {
                    if (!IsBooleanValue(ClaimsBilling) )
                    {
                        GenerateError(this.GetType(), "ClaimsBilling", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Appeals/Grievances
                if (!string.IsNullOrEmpty(AppealsGrievances))
                {
                    if (!IsBooleanValue(AppealsGrievances))
                    {
                        GenerateError(this.GetType(), "AppealsGrievances", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //FraudAndAbuse
                if (!string.IsNullOrEmpty(FraudAndAbuse))
                {
                    if (!IsBooleanValue(FraudAndAbuse) )
                    {
                        GenerateError(this.GetType(), "FraudAndAbuse", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }


                //MarketingSalesComplaintsOrIssues
                if (!string.IsNullOrEmpty(MarketingSalesComplaintsOrIssues))
                {
                    if (!IsBooleanValue(MarketingSalesComplaintsOrIssues))
                    {
                        GenerateError(this.GetType(), "MarketingSalesComplaintsOrIssues", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //QualityOfCare
                if (!string.IsNullOrEmpty(QualityOfCare))
                {
                    if (!IsBooleanValue(QualityOfCare) )
                    {
                        GenerateError(this.GetType(), "QualityOfCare", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //PlanNonRenewal
                if (!string.IsNullOrEmpty(PlanNonRenewal))
                {
                    if (!IsBooleanValue(PlanNonRenewal) )
                    {
                        GenerateError(this.GetType(), "PlanNonRenewal", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                return IsValid;


            }

        }





        public class MedicaidSupplement : absRecordValidate
        {
            //Eligibility/Screening
            [PamProperty("Eligibility/Screening", "MedicaidSupplement")]
            public string EligibilityScreening { get; set; }


            //Benefit Explanation
            [PamProperty("Benefit Explanation", "MedicaidSupplement")]
            public string BenefitExplanation { get; set; }

            //Plans Comparison
            [PamProperty("Plans Comparison", "MedicaidSupplement")]
            public string PlansComparison { get; set; }

            //Plans Enrollment/Disenrollment
            [PamProperty("Plans Enrollment/Disenrollment", "MedicaidSupplement")]
            public string PlansEnrollmentDisenrollment { get; set; }


            //Claims/Billing
            [PamProperty("Claims/Billing", "MedicaidSupplement")]
            public string ClaimsBilling { get; set; }

            //Appeals/Grievances
            [PamProperty("Appeals/Grievances", "MedicaidSupplement")]
            public string AppealsGrievances { get; set; }

            //Fraud and Abuse
            [PamProperty("Fraud and Abuse", "MedicaidSupplement")]
            public string FraudAndAbuse { get; set; }


            //MarketingSalesComplaintsOrIssues
            [PamProperty("Marketing Sales Complaints or issues", "MedicaidSupplement")]
            public string MarketingSalesComplaintsOrIssues { get; set; }


            //Quality of Care
            [PamProperty("Quality of care", "MedicaidSupplement")]
            public string QualityOfCare { get; set; }


            //Plan Non-Renewal
            [PamProperty("Plan Non-Renewal", "MedicaidSupplement")]
            public string PlanNonRenewal { get; set; }



            public void Load(string[] Fields)
            {
                RecordNum = Fields[2];
                EligibilityScreening = Fields[79];
                BenefitExplanation = Fields[80];
                PlansComparison = Fields[81];
                ClaimsBilling = Fields[82];
                AppealsGrievances = Fields[83];
                FraudAndAbuse = Fields[84];
                MarketingSalesComplaintsOrIssues = Fields[85];
                QualityOfCare = Fields[86];
                PlanNonRenewal = Fields[87];


            }

            public override bool AttemptValidate()
            {
                bool IsValid = true;
                //EligibilityScreening
                if (!string.IsNullOrEmpty(EligibilityScreening))
                {
                    if (!IsBooleanValue(EligibilityScreening))
                    {
                        GenerateError(this.GetType(), "EligibilityScreening", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Benefit Explanation
                if (!string.IsNullOrEmpty(BenefitExplanation))
                {
                    if (!IsBooleanValue(BenefitExplanation))
                    {
                        GenerateError(this.GetType(), "BenefitExplanation", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Plans Comparison
                if (!string.IsNullOrEmpty(PlansComparison))
                {
                    if (!IsBooleanValue(PlansComparison) )
                    {
                        GenerateError(this.GetType(), "PlansComparison", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }
               
                //Claims/Billing
                if (!string.IsNullOrEmpty(ClaimsBilling))
                {
                    if (!IsBooleanValue(ClaimsBilling))
                    {
                        GenerateError(this.GetType(), "ClaimsBilling", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Appeals/Grievances
                if (!string.IsNullOrEmpty(AppealsGrievances))
                {
                    if (!IsBooleanValue(AppealsGrievances))
                    {
                        GenerateError(this.GetType(), "AppealsGrievances", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //FraudAndAbuse
                if (!string.IsNullOrEmpty(FraudAndAbuse))
                {
                    if (!IsBooleanValue(FraudAndAbuse))
                    {
                        GenerateError(this.GetType(), "FraudAndAbuse", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }


                //MarketingSalesComplaintsOrIssues
                if (!string.IsNullOrEmpty(MarketingSalesComplaintsOrIssues))
                {
                    if (!IsBooleanValue(MarketingSalesComplaintsOrIssues))
                    {
                        GenerateError(this.GetType(), "MarketingSalesComplaintsOrIssues", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //QualityOfCare
                if (!string.IsNullOrEmpty(QualityOfCare))
                {
                    if (!IsBooleanValue(QualityOfCare))
                    {
                        GenerateError(this.GetType(), "QualityOfCare", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //PlanNonRenewal
                if (!string.IsNullOrEmpty(PlanNonRenewal))
                {
                    if (!IsBooleanValue(PlanNonRenewal) )
                    {
                        GenerateError(this.GetType(), "PlanNonRenewal", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                return IsValid;
            }

        }


        public class Medicaid : absRecordValidate
        {
            //Medicare Savings Programs (MSP) Screening (QMB,SLMB,QI)
            [PamProperty("Plan Non-Renewal", "Medicare Savings Programs (MSP) Screening (QMB,SLMB,QI)")]
            public string MedicareSavingsPrograms { get; set; }


            //MSP Application Assistance
            [PamProperty("MSP Application Assistance", "Medicaid")]
            public string MSPApplicationAssistance { get; set; }


            //Medicaid (SSI, Nursing Home, MEPD, Elderly Waiver) Screening
            [PamProperty("Medicaid (SSI, Nursing Home, MEPD, Elderly Waiver) Screening", "Medicaid")]
            public string MedicaidSSINursingHome{ get; set; }


            //Medicaid Application Assistance
            [PamProperty("Medicaid Application Assistance", "Medicaid")]
            public string MedicaidApplicationAssistance { get; set; }


            //Medicaid/QMB Claims
            [PamProperty("Medicaid/QMB Claims", "Medicaid")]
            public string MedicaidQMBClaims { get; set; }


            //Fraud and Abuse
            [PamProperty("Fraud and Abuse", "Medicaid")]
            public string FraudandAbuse { get; set; }



            public void Load(string[] Fields)
            {
                RecordNum = Fields[2];
                MedicareSavingsPrograms = Fields[88];
                MSPApplicationAssistance = Fields[89];
                MedicaidSSINursingHome = Fields[90];
                MedicaidApplicationAssistance = Fields[91];
                MedicaidQMBClaims = Fields[92];
                FraudandAbuse = Fields[93];

            }

            public override bool AttemptValidate()
            {
                bool IsValid = true;

                if (!string.IsNullOrEmpty(MedicareSavingsPrograms))
                {
                    if (!IsBooleanValue(MedicareSavingsPrograms))
                    {
                        GenerateError(this.GetType(), "MedicareSavingsPrograms", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                if (!string.IsNullOrEmpty(MSPApplicationAssistance))
                {
                    if (!IsBooleanValue(MSPApplicationAssistance))
                    {
                        GenerateError(this.GetType(), "MSPApplicationAssistance", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                if (!string.IsNullOrEmpty(MedicaidSSINursingHome))
                {
                    if (!IsBooleanValue(MedicaidSSINursingHome) )
                    {
                        GenerateError(this.GetType(), "MedicaidSSINursingHome", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                if (!string.IsNullOrEmpty(MedicaidApplicationAssistance))
                {
                    if (!IsBooleanValue(MedicaidApplicationAssistance))
                    {
                        GenerateError(this.GetType(), "MedicaidApplicationAssistance", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                if (!string.IsNullOrEmpty(MedicaidQMBClaims))
                {
                    if (!IsBooleanValue(MedicaidQMBClaims))
                    {
                        GenerateError(this.GetType(), "MedicaidQMBClaims", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                if (!string.IsNullOrEmpty(FraudandAbuse))
                {
                    if (!IsBooleanValue(FraudandAbuse) )
                    {
                        GenerateError(this.GetType(), "FraudandAbuse", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }



                return IsValid;
            }
        }


        public class Other : absRecordValidate
        {

            //Claims/Billing
            [PamProperty("LongTermCare", "Long Term Care (LTC) Insurance")]
            public string LongTermCare  { get; set; }

            //Appeals/Grievances
            [PamProperty("LTCPartnership", "Other")]
            public string LTCPartnership { get; set; }

            //Fraud and Abuse
            [PamProperty("LTCOther", "Other")]
            public string LTCOther { get; set; }


            //MarketingSalesComplaintsOrIssues
            [PamProperty("MilitaryHealthBenefits", "Military Health Bene fits")]
            public string MilitaryHealthBenefits{ get; set; }


            //Quality of Care
            [PamProperty("EmployerFederal", "Employer/Federal Employer Health Benefits (FEHB)")]
            public string EmployerFederal { get; set; }


            //Plan Non-Renewal
            [PamProperty("COBRA", "COBRA")]
            public string COBRA { get; set; }

            //Other Health Insurance
            [PamProperty("OtherHealthInsurance", "Other Health Insurance")]
            public string OtherHealthInsurance { get; set; }


            //Other             
            [PamProperty("Other1", "Other1")]
            public string Other1{ get; set; }



            //Hours Spent
            [PamProperty("HoursSpent", "Hours Spent")]
            public string HoursSpent { get; set; }

            //Minutes Spent
            [PamProperty("MinutesSpent", "Minutes Spent")]
            public string MinutesSpent { get; set; }

            //Status
            [PamProperty("Status", "Status")]
            public string Status { get; set; }


            public void Load(string[] Fields)
            {
                RecordNum = Fields[2];
                LongTermCare = Fields[94];
                LTCPartnership = Fields[95];
                LTCOther = Fields[96];
                MilitaryHealthBenefits = Fields[97];
                EmployerFederal = Fields[98];
                COBRA = Fields[99];
                OtherHealthInsurance  = Fields[100];
                Other1 = Fields[101];
                HoursSpent = Fields[102];
                MinutesSpent = Fields[103];
                Status = Fields[104];
               
            }

            public override bool AttemptValidate()
            {
                bool IsValid = true;
                //LongTermCare 
                if (!string.IsNullOrEmpty(LongTermCare))
                {
                    if (!IsBooleanValue(LongTermCare))
                    {
                        GenerateError(this.GetType(), "LongTermCare", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //LTCPartnership
                if (!string.IsNullOrEmpty(LTCPartnership))
                {
                    if (!IsBooleanValue(LTCPartnership))
                    {
                        GenerateError(this.GetType(), "LTCPartnership", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //LTCOther
                if (!string.IsNullOrEmpty(LTCOther))
                {
                    if (!IsBooleanValue(LTCOther))
                    {
                        GenerateError(this.GetType(), "LTCOther", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //Military Health Benefits
                if (!string.IsNullOrEmpty(MilitaryHealthBenefits))
                {
                    if (!IsBooleanValue(MilitaryHealthBenefits))
                    {
                        GenerateError(this.GetType(), "MilitaryHealthBenefits", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //EmployerFederal
                if (!string.IsNullOrEmpty(EmployerFederal))
                {
                    if (!IsBooleanValue(EmployerFederal))
                    {
                        GenerateError(this.GetType(), "EmployerFederal", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //COBRA
                if (!string.IsNullOrEmpty(COBRA))
                {
                    if (!IsBooleanValue(COBRA) )
                    {
                        GenerateError(this.GetType(), "COBRA", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //OtherHealthInsurance
                if (!string.IsNullOrEmpty(OtherHealthInsurance))
                {
                    if (!IsBooleanValue(OtherHealthInsurance) )
                    {
                        GenerateError(this.GetType(), "OtherHealthInsurance", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }
                //Other
                if (!string.IsNullOrEmpty(Other1))
                {
                    if (!IsValidLength(Other1,255) )
                    {
                        GenerateError(this.GetType(), "Other1", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                }

                //HoursSpent
            
             

                if (IsRequired(HoursSpent))
                {
                        if (!IsDecimal(HoursSpent))
                        {
                            GenerateError(this.GetType(), "HoursSpent", RecordNum, ErrMsg);
                            IsValid = false;
                        }
                        else
                        {
                            try
                            {
                                if (int.Parse(HoursSpent) < 0)
                                {
                                    GenerateError(this.GetType(), "HoursSpent", RecordNum, "Invalid value for hours spent");
                                    IsValid = false;
                                   
                                }
                                if (!IsRequired(MinutesSpent))
                                {
                                    GenerateError(this.GetType(), "MinutesSpent", RecordNum, ErrMsg);
                                    IsValid = false;
                                }
                                else
                                {
                                    if (!IsDecimal(MinutesSpent))
                                    {
                                        GenerateError(this.GetType(), "MinutesSpent", RecordNum, ErrMsg);
                                        IsValid = false;
                                    }
                                    else
                                    {
                                        if (int.Parse(MinutesSpent) < 0)
                                    
                                        {
                                            GenerateError(this.GetType(), "MinutesSpent", RecordNum, "Invalid value for minutes spent");
                                            IsValid = false;
                                        }

                                        if (int.Parse(MinutesSpent) >= 0 && int.Parse(HoursSpent) >= 0)
                                        
                                        {
                                            string CalculatedMinutes = "";
                                            string CalculatedHours = "";
                                             
                                            CalculatedHours = HoursSpent;
                                            CalculatedMinutes = MinutesSpent;
                                            HourMinuteCalc.CalcHours(ref CalculatedHours, ref CalculatedMinutes, false);

                                            HoursSpent = CalculatedHours;
                                            MinutesSpent = CalculatedMinutes;
                                        }
                                        if (IsValid == false)
                                        {
                                            int x = 0;
                                        }
                                    }
                                }
                            }
                            catch (ApplicationException exCalcTimeSpent)
                            {
                                GenerateError(this.GetType(), "HoursSpent", RecordNum, exCalcTimeSpent.Message);
                                IsValid = false;
                            }
                        }
                    
                }
                else
                {
                    GenerateError(this.GetType(), "HoursSpent", RecordNum, ErrMsg);
                    IsValid = false;
                }

                
                //Status
                if (IsRequired(Status))
                {
                    if (!IsNumeric(Status))
                    {
                        GenerateError(this.GetType(), "Status", RecordNum, ErrMsg);
                        IsValid = false;
                    }
                    else
                    {
                        if (!IsValidRange(1, 5, int.Parse(Status)))
                        {
                            GenerateError(this.GetType(), "Status", RecordNum, ErrMsg);
                            IsValid = false;
                        }
                    }
                }
                else
                {
                    GenerateError(this.GetType(), "Status", RecordNum, ErrMsg);
                    IsValid = false;
                }
           
                return IsValid;
            }

            

        }



        public class NationwideCMSSpecialUseFields : absRecordValidate
        {
            
            [PamProperty("Nationwide and CMS Special Use Fields", "Field1")]
            public string Field1 { get; set; }

            [PamProperty("Nationwide and CMS Special Use Fields", "Field2")]
            public string Field2 { get; set; }

            [PamProperty("Nationwide and CMS Special Use Fields", "Field3")]
            public string Field3 { get; set; }

            [PamProperty("Nationwide and CMS Special Use Fields", "Field4")]
            public string Field4 { get; set; }

            [PamProperty("Nationwide and CMS Special Use Fields", "Field5")]
            public string Field5 { get; set; }

            [PamProperty("Nationwide and CMS Special Use Fields", "Field6")]
            public string Field6 { get; set; }

            [PamProperty("Nationwide and CMS Special Use Fields", "Field7")]
            public string Field7 { get; set; }

            [PamProperty("Nationwide and CMS Special Use Fields", "Field8")]
            public string Field8 { get; set; }

            [PamProperty("Nationwide and CMS Special Use Fields", "Field8")]
            public string Field9 { get; set; }

            [PamProperty("Nationwide and CMS Special Use Fields", "Field10")]
            public string Field10 { get; set; }

            string ContactDateString;
            DateTime? DateOfContact = null;
            string StateFIPS = string.Empty;
            public void Load(string[] Fields)
            {
                RecordNum = Fields[2];
                Field1 = Fields[105];
                Field2 = Fields[106];
                Field3 = Fields[107];
                Field4 = Fields[108];
                Field5 = Fields[109];
                Field6 = Fields[110];
                Field7 = Fields[111];
                Field8 = Fields[112];
                Field9 = Fields[113];
                Field10 = Fields[114];
                ContactDateString = Fields[17];
                //StateFIPS = Fields[1];
                StateFIPS = "99";
            }

            public override bool AttemptValidate()
            {
                bool IsValid = false;

                if (!IsValidDate(ContactDateString))
                {
                    ErrMsg = "Date of contact missing could not validate special fields.";
                    GenerateError(RecordNum, ErrMsg);
                    return false;
                }

                DateOfContact = Convert.ToDateTime(ContactDateString);
                State StateValue = new State(StateFIPS);
                IEnumerable<SpecialField> spFieldsRules = FileUploadDAL.GetSpecialUploadFieldsValues(FormType.ClientContact, StateValue);

                if (!ValidateSpecialField(StateFIPS, Field1, DateOfContact, 1, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field2, DateOfContact, 2, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field3, DateOfContact, 3, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field4, DateOfContact, 4, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }


                if (!ValidateSpecialField(StateFIPS, Field5, DateOfContact, 5, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field6, DateOfContact, 6, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field7, DateOfContact, 7, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field8, DateOfContact, 8, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field9, DateOfContact, 9, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field10, DateOfContact, 10, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                //If you complete one CC Duals data field, you must complete all nine Duals data elements.

                ArrayList FieldItemsList = new ArrayList();
                int specialFieldListCMSItemCount = 0;

                FieldItemsList.Add(Field2);
                FieldItemsList.Add(Field3);
                FieldItemsList.Add(Field4);
                FieldItemsList.Add(Field5);
                FieldItemsList.Add(Field6);
                FieldItemsList.Add(Field7);
                FieldItemsList.Add(Field8);
                FieldItemsList.Add(Field9);
                FieldItemsList.Add(Field10);

                foreach (string ItemValue in FieldItemsList)
                {
                    if (!string.IsNullOrEmpty(ItemValue))
                    {
                        specialFieldListCMSItemCount += 1;       
                    }
                }

                if (specialFieldListCMSItemCount != 0 && specialFieldListCMSItemCount != FieldItemsList.Count)
                {
                    ErrMsg = "If you complete one CC Duals data field, you must complete all nine Duals data elements.";
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }              
                
                return IsValid;
            }
                
        }


        public class StateLocalSpecialUseFields : absRecordValidate
        {
            [PamProperty("State and Local Special Use Fields", "Field1")]
            public string Field1 { get; set; }

            [PamProperty("State and Local Special Use Fields", "Field2")]
            public string Field2 { get; set; }

            [PamProperty("State and Local Special Use Fields", "Field3")]
            public string Field3 { get; set; }

            [PamProperty("State and Local Special Use Fields", "Field4")]
            public string Field4 { get; set; }

            [PamProperty("State and Local Special Use Fields", "Field5")]
            public string Field5 { get; set; }

            [PamProperty("State and Local Special Use Fields", "Field6")]
            public string Field6 { get; set; }

            [PamProperty("State and Local Special Use Fields", "Field7")]
            public string Field7 { get; set; }

            [PamProperty("State and Local Special Use Fields", "Field8")]
            public string Field8 { get; set; }

            [PamProperty("State and Local Special Use Fields", "Field8")]
            public string Field9 { get; set; }

            [PamProperty("State and Local Special Use Fields", "Field10")]
            public string Field10 { get; set; }

            [PamProperty("State and Local Special Use Fields", "Comments")]
            public string Comments { get; set; }

            string ContactDateString;
            DateTime? DateOfContact = null;
            string StateFIPS = string.Empty;
            

            public void Load(string[] Fields)
            {
                RecordNum = Fields[2];
                Field1 = Fields[115];
                Field2 = Fields[116];
                Field3 = Fields[117];
                Field4 = Fields[118];
                Field5 = Fields[119];
                Field6 = Fields[120];
                Field7 = Fields[121];
                Field8 = Fields[122];
                Field9 = Fields[123];
                Field10 = Fields[124];
                Comments = Fields[125];
                ContactDateString = Fields[17];
                StateFIPS = Fields[1];
                
            }

            public override bool AttemptValidate()
            {
                bool IsValid = false;

                if (!IsValidDate(ContactDateString))
                {
                    ErrMsg = "Date of contact missing could not validate special fields.";
                    GenerateError(RecordNum, ErrMsg);
                    return false;
                }
                
                DateOfContact = Convert.ToDateTime(ContactDateString);
                State StateValue = new State(StateFIPS);
                IEnumerable<SpecialField> spFieldsRules = FileUploadDAL.GetSpecialUploadFieldsValues(FormType.ClientContact, StateValue);

                if (!ValidateSpecialField(StateFIPS, Field1, DateOfContact, 11, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field2, DateOfContact, 12, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field3, DateOfContact, 13, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field4, DateOfContact, 14, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }


                if (!ValidateSpecialField(StateFIPS, Field5, DateOfContact, 15, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field6, DateOfContact, 16, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field7, DateOfContact, 17, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field8, DateOfContact, 18, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field9, DateOfContact, 19, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }

                if (!ValidateSpecialField(StateFIPS, Field10, DateOfContact, 20, spFieldsRules, FormType.ClientContact))
                {
                    GenerateError(RecordNum, ErrMsg);
                    IsValid = false;
                }
                
                
                return IsValid;
            }
            
        }
        #endregion

    }
    
}
