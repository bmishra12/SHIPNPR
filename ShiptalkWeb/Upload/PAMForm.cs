using System;
using System.Configuration;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;

namespace ShiptalkWeb.Upload
{

    // Declare a delegate type for processing a book:
    public delegate void StoreError(PamProperty prop);


    [AttributeUsage(AttributeTargets.Property)]
    public class PamProperty : System.Attribute
    {
        public string PropertyDescription = "Pam File Property";
        public string DomainNameDescription = "Pam File Property";
        public PamProperty()
        {

        }
        public PamProperty(string aDescription, string DomainName)
        {
            PropertyDescription = aDescription;
            DomainNameDescription = DomainName;
        }
    }


  

    public abstract class absRecordValidate
    {
        public abstract bool AttemptValidate();
        protected char[] sep = {'\t'};
        protected string OrginRecord = string.Empty;


        protected PamProperty GenerateError(Type t, string PropertyName)
        {
            PamProperty ErrorInfo = null;
            PropertyInfo[] Properties = t.GetProperties();
            var query = from prop in Properties
                        where prop.Name == PropertyName
                        select prop;
            PropertyInfo FoundProperty = query.SingleOrDefault();
            object[] PropAttribute = FoundProperty.GetCustomAttributes(false);
            if (PropAttribute.Length > 0)
            {
                if (PropAttribute[0] is PamProperty)
                {
                    PamProperty PropInfo = (PamProperty)PropAttribute[0];
                    ErrorInfo = PropInfo;
                    ProcessError(ErrorInfo);
                }
                else
                {
                    throw (new ApplicationException("Validation Error - Property description information not found "));
                }
            }
            else
            {
                throw (new ApplicationException("Validation Error - Property description information not found "));
            }

            return ErrorInfo;

        }

        protected bool IsNumeric(string Number)
        {
            int Val = 1;
            if (int.TryParse(Number, out Val))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        protected bool IsValidRange(int min, int Max, int Value)
        {
            return (min <= Value && Max >= Value);
        }


        protected bool MinimumMaximumValue(string Maximum, string DependantMax, string FieldValue )
        {
            //This is not a required field so do not generate an error.
                bool IsValid = true;
            int Max = int.Parse(Maximum);
                if (DependantMax != string.Empty)
                {
                    if (!IsNumeric(DependantMax))
                    {
                        IsValid = false;

                    }
                    else
                    {
                        if (IsValidRange(0, Max, int.Parse(DependantMax)))
                        {
                            IsValid = false;
                        }

                    }
                }

                if (FieldValue != string.Empty)
                {
                    if (!IsNumeric(FieldValue))
                    {
                        GenerateError(this.GetType(), "EstPersonsProvidedAssistant");
                        IsValid = false;
                    }
                    else
                    {
                        if (IsValidRange(0, Max, int.Parse(FieldValue)))
                        {
                            if ((int.Parse(DependantMax) < Max) && (int.Parse(FieldValue) != int.Parse(DependantMax)))
                            {
                                /*Error Message Minimum 0, Maximum value must be the least of the following values:
                                    9999 OR Estimated Number of Direct Interactions with Attendees.*/
                                IsValid = false;
                            }
                            else
                            {
                                IsValid = true;
                            }
                        }
                        else
                        {
                            IsValid = false;
                        }
                    }
                }

                return IsValid;
            }
        

        public Validator ValidityCheckObject { set; get; }
        public StoreError ProcessError { get; set; }
        public string RecordId { get; set; }
       
            


    }
    public class PAMForm
    {

        #region Members
        Validator _ValidChecker = new Validator();
        string _ErrorFilePath;
        string _OriginalFilePath;
        string _CleanFilePath;
        string _OriginalRecord;
        #endregion

        #region  Constructor
        public PAMForm(string ErrorFileName, string CleanFileName, string OrginalFileName)
        {
            
            RecordPresenterObjectList = new List<RecordPresenter>();
            RecordPresenterObject = new RecordPresenter();
            InteractivePresentationToPublicObject = new InteractivePresentationToPublic();
            BoothOrExhibitObject = new BoothOrExhibit();
            DedicatedEnrollmentObject = new DedicatedEnrollment();
            RadioShowLiveOrTapedObject = new RadioShowLiveOrTaped();
            ElectronicOtherActivityObject = new ElectronicOtherActivity();
            PrintOtherActivityObject = new PrintOtherActivity();
            MedEventObject = new MedEvent();
            TopicFocusObject = new TopicFocus();
            AudienceObject = new Audience();
            NationwideCMSSpecialUseFieldsObject = new NationwideCMSSpecialUseFields();
            StateAndLocalSpecialUseFieldsObject = new StateAndLocalSpecialUseFields();
            
            PublicMediaBatchObject = new PublicMediaBatch();
            PublicMediaBatchObject.ValidityCheckObject = _ValidChecker;

            _ErrorFilePath = ErrorFileName;
            _OriginalFilePath = OrginalFileName;
            _CleanFilePath = CleanFileName;

        }
        #endregion

      

        #region Methods


        public void Load(string Record)
        {
            _OriginalRecord = Record;
            string RecordId = Record.Substring(3, 40);
            PublicMediaBatchObject.Load(Record);
            RecordPresenterObject.Load(Record);
            InteractivePresentationToPublicObject.Load(Record);
            MedEventObject.Load(Record, RecordId);
              
        }
       
        public void Validate()
        {
            IsValidationSuccessful = true;
            List<absRecordValidate> ValidationObjects = new List<absRecordValidate>();

            ValidationObjects.Add(PublicMediaBatchObject);
            ValidationObjects.Add(RecordPresenterObject);
            ValidationObjects.Add(InteractivePresentationToPublicObject);
            ValidationObjects.Add(MedEventObject);
            
            /*ValidationObjects.Add(BoothOrExhibitObject);
            ValidationObjects.Add(DedicatedEnrollmentObject);
            ValidationObjects.Add(RadioShowLiveOrTapedObject);
            ValidationObjects.Add(ElectronicOtherActivityObject);
            ValidationObjects.Add(PrintOtherActivityObject);
             ValidationObjects.Add(TopicFocusObject);
            ValidationObjects.Add(AudienceObject);
            ValidationObjects.Add(NationwideCMSSpecialUseFieldsObject);
            ValidationObjects.Add(StateAndLocalSpecialUseFieldsObject);
            ValidationObjects.Add(TVOrCableShowLiveOrTapedObject);
            //There are a up to 25 presenter objects.
            foreach (absRecordValidate Rec in RecordPresenterObjectList)
            {
                ValidationObjects.Add(Rec);
            }
            */
            foreach (absRecordValidate ObjectToValidate in ValidationObjects)
            {
                this.IsValidationSuccessful = this.IsValidationSuccessful && ObjectToValidate.AttemptValidate();
            }

            if (this.IsValidationSuccessful)
            {
                //Successful validation write to clean file
                StreamWriter swCleanFile = new StreamWriter(_CleanFilePath, true);
                swCleanFile.WriteLine(_OriginalRecord);
                swCleanFile.Close();
            }
            else
            {
                //Unsuccessful validatiion write to error file.
                StreamWriter swErrorFile = new StreamWriter(_ErrorFilePath, true);
                swErrorFile.WriteLine(_OriginalRecord);
                swErrorFile.Close();
            }
        }

       
        #endregion


        #region properties
        
        public List<RecordPresenter> RecordPresenterObjectList { get; set; }
        public RecordPresenter RecordPresenterObject { get; set; } 
        public InteractivePresentationToPublic InteractivePresentationToPublicObject { get; set; }
        public BoothOrExhibit BoothOrExhibitObject { get; set; }
        public DedicatedEnrollment DedicatedEnrollmentObject { get; set; }
        public RadioShowLiveOrTaped RadioShowLiveOrTapedObject { get; set; }
        public ElectronicOtherActivity ElectronicOtherActivityObject { get; set; }
        public PrintOtherActivity PrintOtherActivityObject { get; set; }
        public MedEvent MedEventObject { get; set; }
        public TopicFocus TopicFocusObject { get; set; }
        public Audience AudienceObject { get; set; }
        public NationwideCMSSpecialUseFields NationwideCMSSpecialUseFieldsObject { get; set; }
        public StateAndLocalSpecialUseFields StateAndLocalSpecialUseFieldsObject { get; set; }
        public TVOrCableShowLiveOrTaped TVOrCableShowLiveOrTapedObject = new TVOrCableShowLiveOrTaped();
        public bool IsValidationSuccessful { get; set; }
        public PublicMediaBatch PublicMediaBatchObject { get; set; }



        
        
        
        #endregion


        #region InnerClass

        public class PublicMediaBatch : absRecordValidate
        {
            
            [PamProperty("Action of Record value is not one of the permitted values of A,U, or D", "Loading Record")]
            public string Action { get; set; }

            [PamProperty("State FIPS Code", "Loading Record")]
            public string StateFIPSCode { get; set; }


            [PamProperty("Agency code granted by NPR", "Loading Record")]
            public string AgencyCode { get; set; }

            public override bool  AttemptValidate()
            {
                bool IsValid = true;
                //Action
                if (Action.ToUpper() != "A" && Action.ToUpper() != "U" && Action.ToUpper() != "D")
                {
                    GenerateError(this.GetType(), "Action");
                    IsValid = false;
                }
                else
                {
                    IsValid = true;
                }

                //TODO: State FIPS Code
                if (
                     !ValidityCheckObject.Validate(StateFIPSCode, (int)Validator.ValidationType.Required, null, -1, null, false)
                    || !ValidityCheckObject.Validate(StateFIPSCode, (int)Validator.ValidationType.Numeric, null, -1, null, false)
                    )
                {
                    GenerateError(this.GetType(), "RecordID");
                    IsValid = false;
                }

                //Unique ID
                if( !ValidityCheckObject.Validate(StateFIPSCode, (int)Validator.ValidationType.Required, null, -1, null, false))
                {
                    GenerateError(this.GetType(), "RecordID");
                    IsValid = false;
                }

                if( !ValidityCheckObject.Validate(AgencyCode, (int)Validator.ValidationType.Required, null, -1, null, false))
                {
                    GenerateError(this.GetType(), "AgencyCode");
                    IsValid = false;
                }

                if (!IsValid)
                {
                    //StreamWriter ErrorFile = new StreamWriter(_
                }
                return IsValid;
                
            }

            public void Load(string Record)
            {
                string[] Fields = Record.Split(sep);
                Action = Fields[0];
                StateFIPSCode = Fields[1];
                RecordId = Fields[2];
                AgencyCode = Fields[3];
            }
        }

        public class RecordPresenter : absRecordValidate
        {
            private string _OriginalRecord = string.Empty;
            [PamProperty("Presenter SHIP User ID", "Presenter")]
            public string PresenterShipUserId { get; set; }

            [PamProperty("Presenter First Name", "Presenter")]
            public string FirstName { get; set; }

            [PamProperty("Presenter Last Name", "Presenter")]
            public string LastName { get; set; }

            [PamProperty("Name of Other Agency or Entity for Non-SHIP Presenters", "Presenter")]
            public string Affiliation { get; set; }

            [PamProperty("Total Hours Spent on Activity", "Presenter")]
            public decimal? HoursSpent { get; set; }

            public override bool AttemptValidate()
            {
                bool PassValidation = false;
                //If the Presenter's SHIP User ID is NOT available, provide the First name  and last name of the Presenter.
                if (PresenterShipUserId == string.Empty)
                {
                    if (FirstName == string.Empty || LastName == string.Empty)
                    {
                        throw (new ApplicationException("Invalid data found missing presenter user id and first name"));
                    }
                    PassValidation = true;
                }
                else
                {
                    if (PresenterShipUserId == "\t")
                    {
                        throw (new ApplicationException("Missing presenter record."));

                    }
                    else
                    {
                        PassValidation = true;
                    }
                }

                //Hours Spent 

                if (HoursSpent == null)
                {
                    PamProperty ErrorProperty = new PamProperty("Presenter Hours spent is missing", "Presenter");
                    ProcessError(ErrorProperty);
                    PassValidation = false;
                }
                else
                {
                    HoursSpent = decimal.Parse(HoursSpent.Value.ToString("##0.00"));

                    char[] HoursSep = { '.' };
                    string[] TimeSpent = Convert.ToString(HoursSpent.Value).Split(HoursSep);
                    if (TimeSpent[1] != "00" && TimeSpent[1] != "25" && TimeSpent[1] != "50" && TimeSpent[1] != "75")
                    {
                        PamProperty ErrorProperty = new PamProperty("Invalid format for presneter hours spent - column Hours spent", "Presenter");
                        ProcessError(ErrorProperty);
                        PassValidation = false;
                    }
                    else
                    {
                        PassValidation = true;
                    }
                }


                string[] PresenterRecord = OrginRecord.Split(sep);
                int TabCount = 9;
                int BlankTabs = 1;
                for (BlankTabs = 1; BlankTabs <= 24; BlankTabs++)
                {
                    if (PresenterRecord[TabCount] == "\t")
                    {
                        TabCount++;
                    }
                    else
                    {
                        PamProperty ErrorProperty = new PamProperty("Invalid format for incomplete presenter record.", "Presenter");
                        PassValidation = false;
                        break;
                    }
                }

                return PassValidation;
            }

            public void Load(string Record)
            {
                string[] PresenterRecord = Record.Split(sep);
                PresenterShipUserId = PresenterRecord[4];
                FirstName = PresenterRecord[5];
                LastName = PresenterRecord[6];
                Affiliation = PresenterRecord[7];
                OrginRecord = Record;
                
                try
                {
                    HoursSpent = Convert.ToDecimal(PresenterRecord[8]);
                }
                catch (FormatException exFormat)
                {
                    HoursSpent = null;
                }
          
            }
        }
        


        public class InteractivePresentationToPublic : absRecordValidate
        {
            [PamProperty("Estimated Number of Attendees for activity or event", "Interactive Presentation to Public. Face to Face In-Person.")]
            public string EstNumberOfAttendees { get; set; }

            [PamProperty("Estimated Persons Provided Enrollment Assistance", "Interactive Presentation to Public. Face to Face In-Person.")]
            public string EstPersonsProvidedAssistant { get; set; }


            public void Load(string Record)
            {
                string[] Fields = Record.Split(sep);
                EstNumberOfAttendees = Fields[32];
                EstPersonsProvidedAssistant = Fields[33];
            }


            public override bool AttemptValidate()
            {
                bool IsValid = MinimumMaximumValue("999", EstNumberOfAttendees, EstPersonsProvidedAssistant);
                //TODO: Add Error Messsage to be recorded for field.
                return IsValid;
            }
        }




        public class BoothOrExhibit : absRecordValidate
        {
            [PamProperty("Estimated Number of Direct Interactions with Attendees", "Booth or Exhibit. At Heath Fair, Senior Fair, or Special Event.")]
            public string EstNumberOfDirectInteractionsWithAttendees { get; set; }

            [PamProperty("Estimated Persons Provided Enrollment Assistance", "Booth or Exhibit. At Heath Fair, Senior Fair, or Special Event.")]
            public string EstPersonsProvidedAssistant { get; set; }


            public void Load(string Record)
            {
                string[] Fields = Record.Split(sep);
                EstNumberOfDirectInteractionsWithAttendees = Fields[34];
                EstPersonsProvidedAssistant = Fields[35];
            }
            public override bool AttemptValidate()
            {
                bool IsValid = MinimumMaximumValue("9999", EstNumberOfDirectInteractionsWithAttendees, EstPersonsProvidedAssistant);
                //TODO: Add Error Messsage to be recorded for field.
                return IsValid;
            }
        }

        public class DedicatedEnrollment : absRecordValidate
        {
            [PamProperty("Estimated Number Persons Reached at Event Regardless of Enroll Assistance", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
            public string EstNumberPersonsReachedAtEvent{ get; set; }

            [PamProperty("Estimated Number Persons Provided Any Enrollment Assistance", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
            public string EstPersonsProvidedAssistant { get; set; }

            [PamProperty("Estimated Number Provided Enrollment Assistance with Part D", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
            public string EstPersonsProvidedAssistantPartD { get; set; }


            [PamProperty("Estimated Number Provided Enrollment Assistance with LIS", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
            public string EstPersonsProvidedAssistantLis { get; set; }

            [PamProperty("Estimated Number Provided Enrollment Assistance with MSP", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
            public string EstPersonsProvidedAssistantMSRP { get; set; }

            [PamProperty("Estimated Number Provided Enrollment Assist Other Medicare Program", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]            
            public string EstPersonsProvidedAssistantOtherMedicareProgram { get; set; }


            public void Load(string Record)
            {
                string[] Fields = Record.Split(sep);
                EstNumberPersonsReachedAtEvent = Fields[134];
                EstPersonsProvidedAssistant = Fields[135];
                EstPersonsProvidedAssistantPartD = Fields[136];
                EstPersonsProvidedAssistantLis = Fields[137];
                EstPersonsProvidedAssistantMSRP = Fields[138];
                EstPersonsProvidedAssistantOtherMedicareProgram = Fields[139];

            }
            
            public override bool  AttemptValidate()
            {
                bool IsValid = true;
                IsValid = IsValid && MinimumMaximumValue("999", EstNumberPersonsReachedAtEvent, EstPersonsProvidedAssistant);
                IsValid = IsValid && MinimumMaximumValue("999", EstNumberPersonsReachedAtEvent, EstPersonsProvidedAssistantPartD);
                IsValid = IsValid && MinimumMaximumValue("999", EstNumberPersonsReachedAtEvent, EstPersonsProvidedAssistantLis);
                IsValid = IsValid && MinimumMaximumValue("999", EstNumberPersonsReachedAtEvent, EstPersonsProvidedAssistantMSRP);
                IsValid = IsValid && MinimumMaximumValue("999", EstNumberPersonsReachedAtEvent, EstPersonsProvidedAssistantOtherMedicareProgram);

                return IsValid;

            }

           



        }


        public class RadioShowLiveOrTaped : absRecordValidate
        {
            [PamProperty("Estimated Number of Listeners Reached", "Radio Show. Live or Taped. Not a Public Service Announce or Ad")]
            public string EstNumberOfListenersReached { get; set; }

            public override bool  AttemptValidate()
            {
                bool IsValid = true;
                //This is not a required field so do not generate an error.
                IsValid = IsNumeric(EstNumberOfListenersReached);
                if (IsValid)
                {
                    IsValid = IsValid && IsValidRange(0, 999999, int.Parse(EstNumberOfListenersReached));
                }
                return IsValid;
            }

            public void Load(string Record, int RecordOffSet, string RecordId)
            {
                string[] Fields = Record.Split(sep);
                EstNumberOfListenersReached = Fields[140];
            }
        }

        public class  TVOrCableShowLiveOrTaped : absRecordValidate
        {
            [PamProperty("Estimated Number of Listeners Reached", "TV or Cable Show. Live or Taped. Not a Public Service Announce or Ad")]
            public string EstNumberOfViewersReached {get;set;}

             public override bool  AttemptValidate()
            {
                bool IsValid = true;
                //This is not a required field so do not generate an error.
                IsValid = IsNumeric(EstNumberOfViewersReached);
                if (IsValid)
                {
                    IsValid = IsValid && IsValidRange(0, 999999, int.Parse(EstNumberOfViewersReached));
                }
                return IsValid;
            }

             public void Load(string Record, int RecordOffSet, string RecordId)
             {
                 string[] Fields = Record.Split(sep);
                 EstNumberOfViewersReached  =  Fields[141];
             }
        }

            
        public class ElectronicOtherActivity : absRecordValidate
        {
            [PamProperty("Estimated Persons Viewing or Listening to PSA", "Electronic Other Activity. PSAs, Electronic Ads, Crawls, Video Conf, Web Conf, Web Chat")]
            public string EstPersonsViewingOrListeningToPSA {get;set;}
            
            public override bool  AttemptValidate()
            {
                //This is not a required field so do not generate an error.
                bool IsValid = true;
                //This is not a required field so do not generate an error.
                IsValid = IsNumeric(EstPersonsViewingOrListeningToPSA);
                if (IsValid)
                {
                    IsValid = IsValid && IsValidRange(0, 9999999, int.Parse(EstPersonsViewingOrListeningToPSA));
                }
                return IsValid;
                
            }
            public void Load(string Record, int RecordOffSet, string RecordId)
            {
                string[] Fields = Record.Split(sep);
                EstPersonsViewingOrListeningToPSA = Fields[141];
            }

        }

        public class PrintOtherActivity : absRecordValidate
        {
            public string EstPersonsReadingArticle { get; set; }

            public override bool  AttemptValidate()
            {
                //This is not a required field so do not generate an error.
                //This is not a required field so do not generate an error.
                bool IsValid = true;
                //This is not a required field so do not generate an error.
                IsValid = IsNumeric(EstPersonsReadingArticle);
                if (IsValid)
                {
                    IsValid = IsValid && IsValidRange(0, 9999999, int.Parse(EstPersonsReadingArticle));
                }
                return IsValid;
            }
            public void Load(string Record, int RecordOffSet, string RecordId)
            {
                string[] Fields = Record.Split(sep);
                EstPersonsReadingArticle = Fields[141];
            }
        }

        public class MedEvent : absRecordValidate
        {
            [PamProperty("Name of event", "Activity or Event.")]
            public string EventName { get; set; }
            [PamProperty("Contact first name", "Activity or Event.")]
            public string ContactFirstName { get; set; }
            [PamProperty("Contact last name", "Activity or Event.")]
            public string ContactLastName { get; set; }
            [PamProperty("Contact phone number", "Activity or Event.")]
            public string ContactPhone { get; set; }
            [PamProperty("Event location - state", "Activity or Event.")]
            public string FIPSEventState { get; set; }
            [PamProperty("Event location - county", "Activity or Event.")]
            public string FIPSEventCounty { get; set; }
            [PamProperty("Event location - zip code", "Activity or Event.")]
            public string FIPSEventZip { get; set; }
            [PamProperty("Event location - city", "Activity or Event.")]
            public string FIPSEventCity { get; set; }
            [PamProperty("Event location - address", "Activity or Event.")]
            public string FIPSAddressCity { get; set; }

            DateTime? _StartDate = null;
            [PamProperty("Start Date Of Activity", "Activity.")]
            public System.DateTime? StartDate
            {get;set;}

            
            [PamProperty("End Date Of Activity", "Activity.")]
            public System.DateTime? EndDate
            { get; set; }

            public override bool  AttemptValidate()
            {
                bool IsValid = true;
                if (StartDate == null)
                {
                    GenerateError(this.GetType(), "StartDate");
                    IsValid = false;
                }

                if (EndDate == null)
                {
                    GenerateError(this.GetType(), "EndDate");
                    IsValid = false;
                }

                if (string.IsNullOrEmpty(EventName))
                {
                    GenerateError(this.GetType(), "EventName");
                    IsValid = false;
                }

                if (string.IsNullOrEmpty(FIPSEventCounty))
                {
                    GenerateError(this.GetType(), "FIPSEventCounty");
                    IsValid = false;
                }

                if (string.IsNullOrEmpty(FIPSEventZip))
                {
                    GenerateError(this.GetType(), "FIPSEventZip");
                    IsValid = false;
                }

                if (string.IsNullOrEmpty(FIPSEventCity))
                {
                    GenerateError(this.GetType(), "FIPSEventCity");
                    IsValid = false;
                }

                if (string.IsNullOrEmpty(FIPSAddressCity))
                {
                    GenerateError(this.GetType(), "FIPSAddressCity");
                    IsValid = false;
                }

                return IsValid;
            }

            public void Load(string Record, string RecordId)
            {
                string[] Fields = Record.Split(sep);
                try
                {
                    //DateTime dtVal = Convert.ToDateTime(Fields[44]);
                    //DateTime dtVal = Convert.ToDateTime(Record.Substring(RecordOffSet, 10));
                    StartDate = Convert.ToDateTime(Fields[143]);
                }
                catch(FormatException exFormatStartDate)
                {
                    PamProperty TotalhoursDataFormat = new PamProperty("Hours spent for presenter does not contain numeric value.", "Presenter");
                    GenerateError(this.GetType(), "StartDate");
                }


                try
                {
                   // DateTime dtVal = Convert.ToDateTime(Record.Substring(RecordOffSet, 10));
                    EndDate = Convert.ToDateTime(Fields[144]);
                }
                catch (FormatException exFormatEndDate)
                {
                    PamProperty TotalhoursDataFormat = new PamProperty("Hours spent for presenter does not contain numeric value.", "Presenter");
                    GenerateError(this.GetType(), "StartDate");
                }

                EventName = Fields[145];

                //This is not a required field so do not generate an error.
            }
        

        }

        public class TopicFocus : absRecordValidate
        {
            [PamProperty("Medicare Parts A and B", "TopicFocus")]
            public bool MedicareAB{ get; set; }
            [PamProperty("Plan Issues - Non-Renewal, Termination, Employer-COBRA", "TopicFocus")]
            public bool NonRenewalSituation { get; set; }
            [PamProperty("Long-Term Care", "TopicFocus")]
            public bool LongTermCare { get; set; }
            [PamProperty("Medigap - Medicare Supplements", "TopicFocus")]
            public bool MedigapMedicare { get; set; }
            [PamProperty("Medicare Fraud and Abuse", "TopicFocus")]
            public bool FraudAbuse { get; set; }
            [PamProperty("Medicare Prescription Drug Coverage - PDP / MA-PD", "TopicFocus")]
            public bool MedicarePrescriptionDrug { get; set; }
            [PamProperty("Other Prescription Drug Coverage - Assistance", "TopicFocus")]
            public bool OtherPrescriptionDrug { get; set; }
            [PamProperty("Medicare Advantage", "TopicFocus")]
            public bool MedicareAdvantage { get; set; }
            [PamProperty("QMB - SLMB - QI", "TopicFocus")]
            public bool QMBSLMBQI { get; set; }
            [PamProperty("Other Medicaid", "TopicFocus")]
            public bool OtherMedicaid { get; set; }
            [PamProperty("General SHIP Program Information", "TopicFocus")]
            public bool GeneralSHIPProgram { get; set; }
            [PamProperty("Medicare Preventive Services", "TopicFocus")]
            public bool MedicarePreventiveSevices { get; set; }
            [PamProperty("Low-Income Assistance", "TopicFocus")]
            public bool LowIncomeAssistant { get; set; }

            [PamProperty("Dual Eligible with Mental Illness Mental Disability", "TopicFocus")]
            public bool DualEligibleMentalAssistant { get; set; }

            [PamProperty("Volunteer Recruitment", "TopicFocus")]
            public bool VolunteerRecruitment { get; set; }
            [PamProperty("Partnership Recruitment", "TopicFocus")]
            public bool PartnershipRecruitment { get; set; }

            [PamProperty("Other Topics - Describe:", "TopicFocus")]
            public string OtherDescription { get; set; }

            public override bool  AttemptValidate()
            {
                return true;
            }
        
        }

        public class Audience : absRecordValidate
        {
            public bool MedicarePreEnrollees { get; set; }
            public bool MedicareBeneficiaries { get; set; }
            public bool FamilyMembersCaregivers { get; set; }
            public bool LowIncome { get; set; }
            public bool Hispanic { get; set; }
            public bool WhiteNonHispanic { get; set; }
            public bool BlackorAfricanAmerican { get; set; }
            public bool AmericanIndian { get; set; }
            public bool AsianIndian { get; set; }
            public bool Chinese { get; set; }
            public bool Filipino { get; set; }
            public bool Japanese { get; set; }
            public bool Korean { get; set; }
            public bool Vietnamese { get; set; }
            public bool NativeHawaiian { get; set; }
            public bool GuamanianOrChamorro { get; set; }
            public bool Samoan { get; set; }
            public bool OtherAsian { get; set; }
            public bool OtherPacificIslander { get; set; }
            public bool SomeOtherRaceEthnicity { get; set; }
            public bool Disabled { get; set; }
            public bool Rural { get; set; }
            public bool EmployerRelatedGroups { get; set; }
            public bool MentalHealthProffessionals { get; set; }
            public bool SocalWorkProffessionals { get; set; }
            public bool DualEligibleGroups { get; set; }
            public bool PartnerShipOutReach { get; set; }
            public bool PartnerPresentationToGroups { get; set; }
            public string Others { get; set; }

            public override bool  AttemptValidate()
            {
                return true;
            }


        }

        public class NationwideCMSSpecialUseFields : absRecordValidate
        {
            string Field1 { get; set; }
            string Field2 { get; set; }
            string Field3 { get; set; }
            string Field4 { get; set; }
            string Field5 { get; set; }
            string Field6 { get; set; }
            string Field7 { get; set; }
            string Field8 { get; set; }
            string Field9 { get; set; }
            string Field10 { get; set; }

            public override bool  AttemptValidate()
            {
                return true;
            }
        }


        public class StateAndLocalSpecialUseFields : absRecordValidate
        {
            string Field1 { get; set; }
            string Field2 { get; set; }
            string Field3 { get; set; }
            string Field4 { get; set; }
            string Field5 { get; set; }
            string Field6 { get; set; }
            string Field7 { get; set; }
            string Field8 { get; set; }
            string Field9 { get; set; }
            string Field10 { get; set; }

            public override bool  AttemptValidate()
            {
                return true;
            }
        }

    #endregion


    } 
} 




   

