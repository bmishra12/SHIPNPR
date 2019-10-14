using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using ShiptalkLogic.DataLayer;
using ShiptalkLogic.BusinessObjects;
using System.Configuration;
using System.Xml;
using System.Data;
using System.Text.RegularExpressions;


namespace ShiptalkLogic.BusinessLayer
{
        //*****************************************************

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



        /// <summary>
        ///absRecordValidate is abstract class that contains functionality that could be used by 
        ///other future validators.  All file upload validators can use this.
        /// </summary>
        public abstract class absRecordValidate
        {
            public abstract bool AttemptValidate();
            protected char[] sep = { '\t' };
            protected string OrginRecord = string.Empty;
            protected List<string> Errors = new List<string>();
            protected string RecordNum = string.Empty;
            protected string ErrMsg;
            protected int? UserID;

            /// <summary>
            /// Generates and formats an error
            /// </summary>
            /// <param name="t">object type</param>
            /// <param name="PropertyName">Name of property which generated error</param>
            /// <param name="RecordNumber">Unique Identifier of the record</param>
            /// <param name="Msg">The customize error message</param>
            /// <returns>PamProperty</returns>
            protected PamProperty GenerateError(Type t, string PropertyName, string RecordNumber, string Msg)
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
                        ErrorInfo = (PamProperty)PropAttribute[0];
                        ErrorInfo.PropertyDescription = "Record: " + RecordNumber + " " + ErrorInfo.DomainNameDescription + " " + ErrorInfo.PropertyDescription + " - " + Msg;
                        Errors.Add("<div>" + ErrorInfo.PropertyDescription + "</div>");
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

            protected void GenerateError(string RecordNumber, string Msg)
            {
                Errors.Add("<div>" + "Record: " + RecordNumber +  " "  + Msg +  "</div>");
            }
            

            
            protected bool IsValidDate(string DateValue)
            {
                bool blnReturnValue = false;
                try
                {
                    Convert.ToDateTime(DateValue);
                    blnReturnValue = true;
                }
                catch (System.FormatException exFormat)
                {

                    blnReturnValue = false;
                }
                return blnReturnValue;
            }
            /// <summary>
            /// Validates special fields
            /// </summary>
            /// <param name="StateFIPS"></param>
            /// <param name="FieldValue"></param>
            /// <param name="SpecialDate"></param>
            /// <param name="FieldName"></param>
            /// <param name="spFieldsRules"></param>
            /// <param name="DataFormat"></param>
            /// <returns>Returns true if it passes false if it fails.</returns>
            protected bool ValidateSpecialField(string StateFIPS, string FieldValue, DateTime? SpecialDate, int FieldIndex, IEnumerable<SpecialField> spFieldsRules, FormType DataFormat)
            {
                if (spFieldsRules == null)
                {
                    return true;
                }
              
               
                
                if (SpecialDate == null || string.IsNullOrEmpty(StateFIPS) )
                {
                    ErrMsg = "Missing required special fields data, Date,State FIPS Code";
                    return false;
                }

                SpecialField SpecialRule = null;

                if (FieldIndex >= 1 && FieldIndex <= 10)
                {
                    //CMS - 99 means value is global for all states
                    var qryCMS = from rule in spFieldsRules
                                 where SpecialDate.Value >= rule.StartDate && SpecialDate <= rule.EndDate
                                 && (rule.State.Code == StateFIPS || rule.State.Code == "99")
                                 && rule.FormType == DataFormat
                                 && rule.Ordinal == FieldIndex
                                 orderby rule.State.Code descending
                                 select rule;

                    SpecialRule = qryCMS.FirstOrDefault();
                    //If not special field found no validation is needed
                    if (SpecialRule == null)
                        return true;
                }
                else
                {

                    //State Special Use Fields
                    //Get Special Fields look up table.
                    //Find the buisness rule that applies to the field
                    //State rule can override global 99 for state fields
                    var qry = from rule in spFieldsRules
                              where SpecialDate.Value >= rule.StartDate && SpecialDate <= rule.EndDate
                              && (rule.State.Code == StateFIPS || rule.State.Code == "99")
                              && rule.FormType == DataFormat
                              && rule.Ordinal == FieldIndex
                              orderby rule.State.Code ascending
                              select rule;

                    //Rule may not be in database.
                    SpecialRule = qry.FirstOrDefault();
                        //If not special field found no validation is needed
                        if (SpecialRule == null)
                            return true;
                   
                }

                if (!IsValidLength(FieldValue, 10))
                {
                    ErrMsg = "Invalid value length for special field " + SpecialRule.Name;
                    return false;
                }
                

                if (SpecialRule.IsRequired && string.IsNullOrEmpty(FieldValue))
                {

                    ErrMsg = "Required special field missing for " + SpecialRule.Name;
                    return false;
                }

                //See if rule is check for numeric value
                if (SpecialRule.ValidationType == ShiptalkLogic.BusinessObjects.ValidationType.Numeric)
                {
                    if (FieldValue.Trim().Length> 0 && !IsNumeric(FieldValue))
                    {
                        ErrMsg = "Invalid value non numeric character found for special field " + SpecialRule.Name;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (SpecialRule.ValidationType == ShiptalkLogic.BusinessObjects.ValidationType.AlphaNumeric)
                    {
                        if (FieldValue.Trim().Length> 0 && !IsAlphaNumeric(FieldValue))
                        {
                            ErrMsg = "Invalid alpha numeric character found for special field " + SpecialRule.Name;
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }                   
                
                    //Check for valid range : Added by Lavanya
                else if (SpecialRule.ValidationType == ShiptalkLogic.BusinessObjects.ValidationType.Range)
                    {
                        if (FieldValue.Trim().Length > 0)
                        {
                            string MinimumValue = string.Empty;
                            string MaximumValue = string.Empty;
                            string Seperator = string.Empty;

                            Seperator = "-";

                            MinimumValue = GetRangeStartValue(SpecialRule.Range.Trim(), Seperator);
                            MaximumValue = GetRangeEndValue(SpecialRule.Range.Trim(), Seperator);

                            if (!IsNumeric(FieldValue))
                            {
                                ErrMsg = "Non numeric character found for special field " + SpecialRule.Name;
                                return false;
                            }
                            else
                            {
                                if (IsValidRange(int.Parse(MinimumValue), int.Parse(MaximumValue), int.Parse(FieldValue.Trim())))
                                {
                                    return true;
                                }
                                else
                                {
                                    ErrMsg = "Range must be from " + MinimumValue + " to " + MaximumValue + " for special field '" + SpecialRule.Name + "'";
                                    return false;
                                }
                            }
                          
                        }
                        else
                            return true;
                    }
                                    
                else //Check for valid Option "Y/N" : Added by Lavanya
                {
                    if (SpecialRule.ValidationType == ShiptalkLogic.BusinessObjects.ValidationType.Option)
                    {
                        if (FieldValue.Trim().Length > 0)
                        {
                            string Option1 = string.Empty;
                            string Option2 = string.Empty;
                            string Seperator = string.Empty;

                            Seperator = ",";

                            Option1 = GetRangeStartValue(SpecialRule.Range.Trim(), Seperator);
                            Option2 = GetRangeEndValue(SpecialRule.Range.Trim(), Seperator);


                            if (!IsAlphaNumeric(FieldValue))
                            {
                                ErrMsg = "Invalid alpha numeric character found for special field " + SpecialRule.Name;
                                return false;
                            }
                            else
                            {
                                if (IsValidOption(FieldValue.Trim(), Option1, Option2))
                                {
                                    return true;
                                }
                                else
                                {
                                    ErrMsg = "Please enter 'Y'/'N' for special field '" + SpecialRule.Name + "'";
                                    return false;
                                }
                            }                           
                        }
                        else
                            return true;
                    }
                    return true;
                }
    
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
                    ErrMsg = "Non numeric character found.";
                    return false;
                }
            }

            protected bool IsAlphaNumeric(string Value)
            {
                Regex pattern = new Regex("[a-zA-Z0-9]");
                return pattern.IsMatch(Value);
            }

            protected bool IsDecimal(string Number)
            {
                decimal Val = 1;
                if (decimal.TryParse(Number, out Val))
                {
                    return true;
                }
                else
                {
                    ErrMsg = "Non numeric character found.";
                    return false;
                }
                
            }

            protected bool IsValidRange(decimal min, decimal Max, decimal Value)
            {
                if (!(min <= Value && Max >= Value))
                {
                    ErrMsg = "Invalid value found. Number is outside valid range of 0 - " + Max.ToString();
                    return false;
                }
                else
                {
                    return true;
                }
            }

            protected bool IsValidRange(int min, int Max, int Value)
            {
                if ((Value < min  || Value > Max ))
                {
                    ErrMsg = "Invalid value found. Number is outside valid range of " + min.ToString() + "  -  " + Max.ToString();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            protected bool MinimumMaximumValue(string Maximum, string DependantMax, string FieldValue)
            {
                ErrMsg = "Invalid Value";
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
                        if (!IsValidRange(0, Max, int.Parse(DependantMax)))
                        {
                            IsValid = false;
                        }

                    }
                }

                if (FieldValue != string.Empty)
                {
                    if (!IsNumeric(FieldValue))
                    {
                        IsValid = false;
                    }
                    else
                    {
                        if (IsValidRange(0, Max, int.Parse(FieldValue)))
                        {
                            if (!(int.Parse(DependantMax) <= Max) && (int.Parse(FieldValue) <= int.Parse(DependantMax)))
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

                if(IsValid)
                    ErrMsg = string.Empty;
                return IsValid;
            }
            protected bool IsRequired(string Value)
            {
                if (string.IsNullOrEmpty(Value))
                {
                    ErrMsg = "Value is missing.";
                    return false;
                }
                else
                {
                    return true;
                }

            }
            //protected bool IsBooleanValue(string Value)
            protected bool IsBooleanValue(string Value)
            {
                
                if (Value.ToUpper() == "YES" ||
                    Value.ToUpper() == "Y" ||
                    Value.ToUpper() == "TRUE" ||
                    Value.ToUpper() == "NO" ||
                    Value.ToUpper() == "N" ||
                    Value.ToUpper() == "FALSE")
                {
                    return true;
                }
                else
                {
                    ErrMsg = "Invalid value found";
                    return false;
                }

                //Null indicates invalid value was found.
            }


            protected bool IsBooleanTrue(string Value)
            {
                if (Value.ToUpper() == "YES" ||
                    Value.ToUpper() == "Y" ||
                    Value.ToUpper() == "TRUE")
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            protected bool IsValidLength(string Value, int ValueLength)
            {
                if (Value.Length <= ValueLength)
                {
                    return true;
                }
                else
                {
                    ErrMsg = "Maximum number of characters surpassed.";
                    return false;

                }

            }

            protected bool ValidateRequiredMaxLength(string Value, int ValueLength)
            {
                if (!IsRequired(Value))
                {
                    return false;
                }
                else
                {
                    if (!IsValidLength(Value, ValueLength))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }


            }

            protected bool ValidateRequiredLength(string Value, int ValueLength)
            {
                if (!IsRequired(Value))
                {
                    return false;
                }
                else
                {
                    if (Value.Length != ValueLength)
                    {
                        ErrMsg = "Invalid value found.";
                        return false;
                    }
                    else
                    {
                        return true;
                    }
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

            /// <summary>
            /// Determines if the Agency Code is valid for the state 
            /// </summary>
            /// <param name="AgencyCode"></param>
            /// <param name="ValidState"></param>
            /// <param name="StateFIPS"></param>
            /// <returns>true or false</returns>
            /// <summary>
            /// Determines if the Agency Code is valid for the state 
            /// </summary>
            /// <returns>true or false</returns>
            public bool IsAgencyCodeValid(string AgencyCode, string ValidState, string StateFIPS)
            {

                //Agency code must be from the same state (FIPs) unless this is sys admin
                if (ValidState.ToUpper() == "ADMIN")
                {
                    return LookupDAL.IsAgencyValidForState(AgencyCode, StateFIPS);

                }
                else
                {
                    return LookupDAL.IsAgencyValidForState(AgencyCode, ValidState);
                }
            }

            

            

            



            #region Properties
            public string FormattedErrors
            {
                get
                {
                    string ErrorFeedBack = string.Empty;
                    foreach (string FoundError in Errors)
                    {
                        ErrorFeedBack = ErrorFeedBack + FoundError;
                    }

                    return ErrorFeedBack;
                }
            }
            #endregion



        }


        public interface ILoader
        {
            void Initialize();
            void Load(string[] Record, string UserState, int UserID);
            string Validate();

            string[] ParsedFields { get; }
           
           
        }

        public interface IEventUpload
        {
            DateTime? StartDateTime { get; set; }
            DateTime? EndDateTime { get; set; }
        }




    
       public class HourMinuteCalc
        {

            public static void CalcHours(ref string Hours, ref string Minutes, bool IsPam)
            {
                int intHours = 0;
                if( string.IsNullOrEmpty(Minutes))
                    Minutes = "0";
                if (!int.TryParse(Hours, out intHours))
                {
                    CalcHoursOld(ref Hours, ref Minutes, IsPam);
                }


                // Pbattineni on 09/26/12 - Hours Plus Minutes Can't be Zero - Convert Hours and Mins and check the Sum.

                if (!IsPam)
                {
                    if (Convert.ToInt32(Hours) > 24 && Convert.ToDecimal(Minutes) > 59)
                        //if (int.Parse(Hours) > 24 && int.Parse(Minutes) > 59)
                        throw (new ApplicationException("Hours spans more than one day."));

                    else if (Convert.ToDecimal(Minutes) > 59)
                        //if (int.Parse(Minutes) > 59)
                        throw (new ApplicationException("Invalid value for minutes spent."));

                    else if (int.Parse(Hours) > 24)
                        //if (int.Parse(Hours) > 24)
                        throw (new ApplicationException("Invalid value for hours spent."));

                    else if ((Convert.ToInt32(Hours)+Convert.ToInt32(Minutes)) == 0)
                        //if (int.Parse > 24)
                        //throw (new ApplicationException("Invalid value for hours spent."));
                        throw (new ApplicationException("Hours Plus Minutes Can't be Zero"));
                }
                else
                {
                    if (Convert.ToInt32(Hours) > 9999)
                      
                        throw (new ApplicationException("Hours spent is more than 9999 hrs."));
                    else if ((Convert.ToInt32(Hours) + Convert.ToInt32(Minutes)) == 0)
                        //if (int.Parse > 24)
                        //throw (new ApplicationException("Invalid value for hours spent."));
                        throw (new ApplicationException("Hours Plus Minutes Can't be Zero"));

                }

            }



            /// <summary>
            /// 
            /// </summary>
            /// <param name="Hours"></param>
            /// <param name="Minutes"></param>
            /// <returns></returns>
            /// <exception cref="ApplicationException">Hours plus minutes spans more than one day.</exception>
            public static void CalcHoursOld(ref string Hours, ref string Minutes, bool IsPam)
            {
               
                    int HoursFound = 0;
                    if (string.IsNullOrEmpty(Minutes))
                        Minutes = "0";
                    int MinutesFound = int.Parse(Minutes);
                    int HoursIntValue = 0;
                    const int TOTAL_MINUTES_IN_DAY = 1440;
                    if (!int.TryParse(Hours, out HoursIntValue))
                    {
                        //See if this is a decimal value. - It should be
                        decimal HoursDecValue = 0;
                        if (decimal.TryParse(Hours, out HoursDecValue) && !IsPam)
                        {
                            HoursFound = (int)Math.Floor(HoursDecValue);
                            MinutesFound = (int)(HoursDecValue - HoursFound);
                            if (HoursFound > 24)
                            {
                                throw (new ApplicationException("Hours spans more than one day."));
                            }
                        }
                        else
                        {
                            //Satified Condition: Decimal value has been set for hours this is a PAM file
                            HoursFound = (int)Math.Floor(HoursDecValue);

                            //Get the  Minutes from the hours value
                            MinutesFound = (int)((HoursDecValue - HoursFound) * 100);

                            switch (MinutesFound)
                            {
                                case 25:
                                    MinutesFound = 15;
                                    break;
                                case 50:
                                    MinutesFound = 30;
                                    break;
                                case 75:
                                    MinutesFound = 45;
                                    break;
                                case 0:
                                    MinutesFound = 0;
                                    break;
                                default:
                                    throw (new ApplicationException("Hours does not contain a valid value."));
                            }
                            Hours = HoursFound.ToString();
                            Minutes = MinutesFound.ToString();
                            return;
                        }

                        //Check to see if Hours includes minutes and CC form only
                        if (!IsPam) 
                        {
                            if (HoursFound > 24)
                            {
                                MinutesFound = MinutesFound + HoursFound;
                                HoursFound = 0;
                                if (MinutesFound > TOTAL_MINUTES_IN_DAY)
                                {
                                    throw (new ApplicationException("Hours plus minutes spans more than one day."));
                                }
                            }
                            else
                            {
                                //Condition Satisfied: Hours is less than 24

                                //Check to see if hours + minutes will be greater than 1 day.
                                if (TOTAL_MINUTES_IN_DAY < (60 * HoursFound + MinutesFound))
                                {
                                    throw (new ApplicationException("Hours plus minutes spans more than one day."));
                                }
                            }
                        }
                        else
                        {
                            if (HoursFound > 9999)
                                throw (new ApplicationException("Hours spent is more than 9999 hrs."));

                        }
                    }
                    else
                    {
                        //Condition: hours contains an int value
                        if (!IsPam)
                        {

                            HoursFound = int.Parse(Hours);
                            MinutesFound = int.Parse(Minutes);
                            if (HoursFound < 59 && HoursFound > 24)
                            {
                                if (MinutesFound == 0)
                                {
                                    //Hours represents minutes
                                    Hours = "0";
                                    Minutes = HoursFound.ToString();
                                    return;
                                }
                                else
                                {
                                    //Hours  greater than 24 and minutes is not equal to 0
                                    throw (new ApplicationException("Hours spent and minutes spent spans more than one day."));
                                }
                            }

                            if (HoursFound <= 24)
                            {
                                //Check to see if Hours plus Minutes does not span a day.
                                if ((HoursFound * 60) + MinutesFound > TOTAL_MINUTES_IN_DAY)
                                {
                                    throw (new ApplicationException("Hours spent and minutes spent spans more than one day."));
                                }
                                else
                                {
                                    Hours = HoursFound.ToString();
                                    Minutes = MinutesFound.ToString();
                                }

                            }

                            if (HoursFound > 59)
                            {
                                if (HoursFound == 99 && MinutesFound == 0)
                                {
                                    MinutesFound = 59;
                                }
                                MinutesFound = HoursFound;
                                HoursFound = Convert.ToInt32((Math.Floor(Convert.ToDouble(MinutesFound) / 60)));
                                if ((HoursFound * 60) > MinutesFound)
                                    MinutesFound = (HoursFound * 60) - MinutesFound;
                                else
                                    MinutesFound = MinutesFound - (HoursFound * 60);

                                if (HoursFound > 23 && MinutesFound > 59)
                                    throw (new ApplicationException("Hours spent and minutes spent spans more than one day."));

                            }

                            Hours = HoursFound.ToString();
                            Minutes = MinutesFound.ToString();
                        }

                        else
                        {
                            if (HoursFound > 9999 )
                                throw (new ApplicationException("Hours spent is more than 9999 hrs."));

                        }

                    }

                }
               

            }



       
        

            
            public class UpLoadStatusManager
            {
                #region Members
                //protected List<absRecordValidate> ValidationObjects = new List<absRecordValidate>();
                #endregion






                #region methods


                /// <summary>
                /// Add Batch Up Load status record for file upload.
                /// </summary>
                /// <param name="UserId"></param>
                /// <param name="StateFIPS"></param>
                /// <param name="Status"></param>
                /// <param name="Comments"></param>
                /// <param name="UploadId"></param>
                /// <returns></returns>
                public static int AddUploadStatus(string Status, string Comments, int UploadId)
                {
                    int PkId = -1;
                    //Add a new batch upload status 
                    PkId = FileUploadDAL.AddUploadStatus(Status, Comments, UploadId);
                    return PkId;
                }

                public static int UpdateFileUploadsRecordsProcessed(int UploadId, int InvalidRecords, int RecordsProcessed)
                {
                    return FileUploadDAL.UpdateFileUploadsRecordsProcessed(UploadId, InvalidRecords, RecordsProcessed);
                }



                public static int AddUploadfile(string StateFIPS, string CleanFileName, string ErrorFileName, string OriginalFileName, string FileName, string FileType, int UserId)
                {
                    int PkId = -1;
                    if (string.IsNullOrEmpty(CleanFileName) ||
                        string.IsNullOrEmpty(ErrorFileName) ||
                        string.IsNullOrEmpty(OriginalFileName) ||
                        string.IsNullOrEmpty(FileName) ||
                        string.IsNullOrEmpty(StateFIPS) ||
                        string.IsNullOrEmpty(FileType))
                    {
                        throw (new ArgumentException());
                    }
                    else
                    {
                        //Add a new batch upload status 
                        PkId = FileUploadDAL.AddUploadfile(StateFIPS, CleanFileName, OriginalFileName, FileName, ErrorFileName, FileType, UserId);

                    }

                    return PkId;
                }



                public static DataSet GetFileUploadStatusByUser(int UserId)
                {
                    return FileUploadDAL.GetFileUploadStatusByUser(UserId);
                }


                public int CalculateHours(string HoursTime)
                {
                    return 0;
                }



                #endregion



            }


            public class PamFileUpLoadBLL : ILoader
            {
                string[] PAMParsedFields = new string[220];
                List<absRecordValidate> ValidationObjects = new List<absRecordValidate>();



                public void Initialize()
                {
                    RecordPresenterObjectList = new List<RecordPresenter>();
                    RecordPresenterObject = new RecordPresenter();
                    InteractivePresentationToPublicObject = new InteractivePresentationToPublic();
                    BoothOrExhibitObject = new BoothOrExhibit();
                    DedicatedEnrollmentObject = new DedicatedEnrollment();
                    RadioShowLiveOrTapedObject = new RadioShowLiveOrTaped();
                    ElectronicOtherActivityObject = new ElectronicOtherActivity();
                    //PrintOtherActivityObject = new PrintOtherActivity();
                    PAMEventObject = new PAMEvent();
                    TopicFocusObject = new TopicFocus();
                    AudienceObject = new Audience();
                    NationwideCMSSpecialUseFieldsObject = new NationwideCMSSpecialUseFields();
                    StateAndLocalSpecialUseFieldsObject = new StateAndLocalSpecialUseFields();
                    PublicMediaBatchObject = new PublicMediaBatch();
                }




                #region Methods

                /// <summary>
                /// Loads the record into for parsing.
                /// </summary>
                /// <param name="Record"></param>
                public void Load(string[] Record, string UserState, int ShipUserID)
                {
                    PAMParsedFields = Record;
                    //_OriginalRecord = Record;
                    string RecordId = Record[2];
                    PublicMediaBatchObject.Load(PAMParsedFields, UserState, ShipUserID);
                    RecordPresenterObject.Load(PAMParsedFields);
                    InteractivePresentationToPublicObject.Load(PAMParsedFields);
                    PAMEventObject.Load(PAMParsedFields);
                    BoothOrExhibitObject.Load(PAMParsedFields);
                    DedicatedEnrollmentObject.Load(PAMParsedFields);
                    RadioShowLiveOrTapedObject.Load(PAMParsedFields);
                    ElectronicOtherActivityObject.Load(PAMParsedFields);
                    TopicFocusObject.Load(PAMParsedFields);
                    AudienceObject.Load(PAMParsedFields);
                    TVOrCableShowLiveOrTapedObject.Load(PAMParsedFields);
                    StateAndLocalSpecialUseFieldsObject.Load(PAMParsedFields, Record[143]);
                    NationwideCMSSpecialUseFieldsObject.Load(PAMParsedFields, Record[143]);
                }


                public static int AddPamRecord(PublicMediaEvent PAMRec)
                {
                    PAMDAL PAMInserter = new PAMDAL();
                    return PAMInserter.CreatePam(PAMRec);
                }

                /// <summary>
                /// Updates an existing Uploaded Pam record
                /// </summary>
                /// <param name="PAMRec"></param>
                /// <param name="AgencyCode"></param>
                /// <param name="BatchStateUniqueID"></param>
                public static void UpdatePamRecord(PublicMediaEvent PAMRec, string AgencyCode, string BatchStateUniqueID)
                {
                    //Get the PAM ID for the record that are attempting to update
                    PAMRec.PamID = FileUploadDAL.GetUploadedPamID(AgencyCode, BatchStateUniqueID);
                    PAMDAL PAMUpdater = new PAMDAL();
                    PAMUpdater.UpdatePam(PAMRec);
                }

                public static void DeletePamRecord(string StateFIPS, string AgencyCode, string BatchStateUniqueID)
                {
                    PAMDAL PAMUpdater = new PAMDAL();
                    PAMUpdater.DeletePam(StateFIPS, AgencyCode, BatchStateUniqueID);
                }

                /// <summary>
                /// Validates the record that has been previously loaded.
                /// </summary>
                public string Validate()
                {
                    IsValidationSuccessful = true;
                    ValidationObjects.Clear();
                    ValidationObjects.Add(PublicMediaBatchObject);
                    ValidationObjects.Add(RecordPresenterObject);
                    ValidationObjects.Add(InteractivePresentationToPublicObject);
                    ValidationObjects.Add(PAMEventObject);
                    ValidationObjects.Add(BoothOrExhibitObject);
                    ValidationObjects.Add(DedicatedEnrollmentObject);
                    ValidationObjects.Add(RadioShowLiveOrTapedObject);
                    ValidationObjects.Add(ElectronicOtherActivityObject);
                    ValidationObjects.Add(TopicFocusObject);
                    ValidationObjects.Add(AudienceObject);
                    ValidationObjects.Add(NationwideCMSSpecialUseFieldsObject);
                    ValidationObjects.Add(StateAndLocalSpecialUseFieldsObject);
                    ValidationObjects.Add(TVOrCableShowLiveOrTapedObject);


                    foreach (absRecordValidate ObjectToValidate in ValidationObjects)
                    {
                        bool returnValue = ObjectToValidate.AttemptValidate();

                        this.IsValidationSuccessful = this.IsValidationSuccessful && returnValue;
                    }

                    if (this.IsValidationSuccessful)
                    {
                        return string.Empty;
                    }
                    else
                    {
                        //Unsuccessful validatiion write to validation errors to  file.
                        return this.ValidationErrors;

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
                public PAMEvent PAMEventObject { get; set; }
                public TopicFocus TopicFocusObject { get; set; }
                public Audience AudienceObject { get; set; }
                public NationwideCMSSpecialUseFields NationwideCMSSpecialUseFieldsObject { get; set; }
                public StateAndLocalSpecialUseFields StateAndLocalSpecialUseFieldsObject { get; set; }
                public TVOrCableShowLiveOrTaped TVOrCableShowLiveOrTapedObject = new TVOrCableShowLiveOrTaped();
                public bool IsValidationSuccessful { get; set; }
                public PublicMediaBatch PublicMediaBatchObject { get; set; }

                public string ValidationErrors
                {
                    get
                    {
                        string Errors = string.Empty;
                        foreach (absRecordValidate ValidateRecord in ValidationObjects)
                        {
                            Errors = Errors + ValidateRecord.FormattedErrors;
                        }

                        return Errors;
                    }
                }

                public string[] ParsedFields
                {
                    get
                    {
                        return PAMParsedFields;

                    }
                }
                #endregion


                #region InnerClass

                public class PublicMediaBatch : absRecordValidate
                {
                    string _ValidState = string.Empty;
                    [PamProperty("Action", "PAM")]
                    public string Action { get; set; }

                    [PamProperty("State FIPS Code", "PAM")]
                    public string StateFIPSCode { get; set; }


                    [PamProperty("Agency code missing", "PAM")]
                    public string AgencyCode { get; set; }

                    [PamProperty("BatchStateUniqueID is missing", "Client Contact")]
                    public string BatchStateUniqueID { get; set; }

                    int UserID;

                    public override bool AttemptValidate()
                    {
                        bool IsValid = true;

                        //RecordNum
                        if (!IsRequired(RecordNum))
                        {
                            GenerateError(this.GetType(), "RecordNum", string.Empty, "No record number found.");
                            IsValid = false;
                        }


                        //StateFIPSCode
                        if (!ValidateRequiredMaxLength(StateFIPSCode, 2))
                        {
                            GenerateError(this.GetType(), "StateFIPSCode", RecordNum, ErrMsg);
                            IsValid = false;
                        }
                        else
                        {
                            //If this is not an admin check the state to ensure they are permitted to upload
                            //for that state.  Only sys Admins can upload for any state.
                            if (_ValidState.ToUpper() == "ADMIN")
                            {
                                //Check if the STATE FIPCODE exist and if user has the same state code
                                if (!LookupDAL.IsStateFipCodeValid(StateFIPSCode))
                                {
                                    /*  throw (new ApplicationException("Invalid state found in upload file"));*/
                                    GenerateError(this.GetType(), "StateFIPSCode", RecordNum, "Invalid state fips code found.");
                                    IsValid = false;
                                }
                            }
                            else
                            {
                                //Check if the STATE FIPCODE exist and if user has the same state code
                                //Check if state code is a sub state
                                if ((StateFIPSCode != _ValidState) && !FileUploadDAL.IsSubStateValid(StateFIPSCode, UserID))
                                {
                                    //Check if the StateFIPCode is a substate
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
                            //UserSubStateRegionDAL.GetUserSubStateRegionalProfiles(
                            if (!IsAgencyCodeValid(AgencyCode, _ValidState, StateFIPSCode))
                            {
                                GenerateError(this.GetType(), "AgencyCode", RecordNum, "Agency code could not be found for the state (FIPs)");
                                IsValid = false;
                            }
                        }

                        //Action
                        if (Action.ToUpper() != "A" && Action.ToUpper() != "U" && Action.ToUpper() != "D")
                        {
                            GenerateError(this.GetType(), "Action", RecordNum, "Invalid value found.");
                            IsValid = false;
                        }
                        else
                        {
                            bool RecordExists = FileUploadDAL.IsPamRecordUploaded(AgencyCode, BatchStateUniqueID);
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

                        return IsValid;
                    }

                    /// <summary>
                    /// Determines if an existing record was alreay uploaded.
                    /// </summary>
                    /// <param name="AgencyCode"></param>
                    /// <param name="BatchRecordID"></param>
                    /// <returns></returns>
                    public bool IsPamRecordUploaded(string AgencyCode, string BatchRecordID)
                    {
                        return FileUploadDAL.IsPamRecordUploaded(AgencyCode, BatchRecordID);

                    }

                    public void Load(string[] Fields, string UserState, int ShipUserID)
                    {
                        //string[] Fields = Record.Split(sep);
                        Action = Fields[0];
                        StateFIPSCode = Fields[1];
                        RecordNum = Fields[2];
                        AgencyCode = Fields[3];
                        _ValidState = UserState;
                        BatchStateUniqueID = Fields[2];
                        UserID = ShipUserID;
                    }
                }

                public class RecordPresenter : absRecordValidate
                {

                    //private string _OriginalRecord = string.Empty;
                    [PamProperty("Presenter SHIP User ID", "Presenter")]
                    public string PresenterShipUserId { get; set; }

                    [PamProperty("Presenter First Name", "Presenter")]
                    public string FirstName { get; set; }

                    [PamProperty("Presenter Last Name", "Presenter")]
                    public string LastName { get; set; }

                    [PamProperty("Presenter Affiliation", "Presenter")]
                    public string Affiliation { get; set; }

                    //Hours Spent
                    [PamProperty("Hours Spent", "Presenter hours spent")]
                    public string HoursSpent { get; set; }



                    public override bool AttemptValidate()
                    {
                        int iKount = 0;
                        int HoursSpentIndex = 8;
                        bool IsValid = true;
                        int PresenterRecordsFound = 0;
                        Dictionary<string, string> DictPresenterID = new Dictionary<string, string>();
                        while (iKount < 25)
                        {
                            //OrginRecord = Record;
                            PresenterShipUserId = PresenterFields[HoursSpentIndex - 4];
                            LastName = PresenterFields[HoursSpentIndex - 3];
                            FirstName = PresenterFields[HoursSpentIndex - 2];
                            Affiliation = PresenterFields[HoursSpentIndex - 1];
                            HoursSpent = PresenterFields[HoursSpentIndex];

                            //Check For required field value Hours spent
                            if (HoursSpent != string.Empty)
                            {
                                PresenterRecordsFound++;
                                //If the Presenter's SHIP User ID is NOT available, provide the First name  and last name of the Presenter.
                                if (!IsRequired(PresenterShipUserId))
                                {
                                    GenerateError(this.GetType(), "PresenterShipUserId", RecordNum, ErrMsg);
                                    IsValid = false;
                                }
                                else
                                {
                                    //See if the shipuserid
                                    if (!IsNumeric(PresenterShipUserId))
                                    {
                                        GenerateError(this.GetType(), "PresenterShipUserId", RecordNum, ErrMsg);
                                        IsValid = false;
                                    }
                                    else
                                    {
                                        //Determine if this is a valid Ship User ID and a presenter
                                        var aUser = UserBLL.GetUser(int.Parse(PresenterShipUserId));
                                        if (aUser == null || (aUser.GetAllDescriptorsForUser.Where(p => p.Key == Descriptor.PresentationAndMediaStaff.EnumValue<int>()).Count() == 0))
                                        {
                                            ErrMsg = "Presenters ship user ID could not be found or not a Valid Presenter. ";
                                            GenerateError(this.GetType(), "PresenterShipUserId", RecordNum, ErrMsg);
                                            IsValid = false;
                                        }

                                        else

                                        {

                                            try
                                            {
                                                string FoundUserID = DictPresenterID[PresenterShipUserId];
                                                if (FoundUserID != string.Empty)
                                                {
                                                    ErrMsg = "User ID found multiple times as a presenter.";
                                                    GenerateError(this.GetType(), "PresenterShipUserId", RecordNum, ErrMsg);
                                                    IsValid = false;
                                                }

                                            }
                                            catch (KeyNotFoundException exValidKey)
                                            {
                                                DictPresenterID.Add(PresenterShipUserId, "FOUND");
                                            }
                                        }

                                    }
                                }
                                //Affiliation
                                if (!string.IsNullOrEmpty(Affiliation))
                                {
                                    if (!ValidateRequiredMaxLength(Affiliation, 255))
                                    {
                                        GenerateError(this.GetType(), "Affiliation", RecordNum, ErrMsg);
                                        IsValid = false;
                                    }
                                }

                                //HoursSpent
                                if (IsRequired(HoursSpent))
                                {
                                    if (HoursSpent == "0" || HoursSpent == "99")
                                    {
                                        GenerateError(this.GetType(), "HoursSpent", RecordNum, "Invalid value found for hours spent.");
                                        IsValid = false;
                                    }

                                    else
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
                                                string CalculatedMinutes = "";
                                                string CalculatedHours = "";

                                                CalculatedHours = HoursSpent;
                                                HourMinuteCalc.CalcHours(ref CalculatedHours, ref CalculatedMinutes, true);
                                            }
                                            catch (ApplicationException exCalcTimeSpent)
                                            {
                                                GenerateError(this.GetType(), "HoursSpent", RecordNum, exCalcTimeSpent.Message);
                                                IsValid = false;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    GenerateError(this.GetType(), "HoursSpent", RecordNum, ErrMsg);
                                    IsValid = false;
                                }
                            }
                            HoursSpentIndex = HoursSpentIndex + 5;
                            iKount++;
                        }
                        //Check to see if we did find at least one presenter record.
                        if (PresenterRecordsFound == 0)
                        {
                            ErrMsg = "Presenter record was not found.";
                            GenerateError(this.GetType(), "PresenterShipUserId", RecordNum, ErrMsg);
                            IsValid = false;
                        }

                        return IsValid;
                    }

                    public void Load(string[] Fields)
                    {
                        PresenterFields = Fields;
                        //string[] PresenterRecord = Record.Split(sep);
                        PresenterShipUserId = Fields[4];
                        FirstName = Fields[5];
                        LastName = Fields[6];
                        Affiliation = Fields[7];
                        //OrginRecord = Record;
                        RecordNum = Fields[2];
                        HoursSpent = Fields[8];

                    }

                    #region Properties
                    string[] PresenterFields { get; set; }
                    #endregion
                }



                public class InteractivePresentationToPublic : absRecordValidate
                {
                    [PamProperty("Estimated Number of Attendees for activity or event - Interactive Presentation to Public", "Interactive Presentation to Public. Face to Face In-Person.")]
                    public string EstNumberOfAttendees { get; set; }

                    [PamProperty("Estimated Persons Provided Enrollment Assistance - Interactive Presentation to Public - Interactive Presentation to Public", "Interactive Presentation to Public. Face to Face In-Person.")]
                    public string EstPersonsProvidedAssistant { get; set; }



                    public void Load(string[] Fields)
                    {
                        //string[] Fields = Record.Split(sep);
                        EstNumberOfAttendees = Fields[129];
                        EstPersonsProvidedAssistant = Fields[130];
                        RecordNum = Fields[2];
                    }


                    public override bool AttemptValidate()
                    {
                        bool IsValid = true;
                        if (!string.IsNullOrEmpty(EstPersonsProvidedAssistant) || !string.IsNullOrEmpty(EstNumberOfAttendees))
                        {
                            if (!IsNumeric(EstPersonsProvidedAssistant))
                            {
                                GenerateError(this.GetType(), "EstPersonsProvidedAssistant", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            if (!IsNumeric(EstNumberOfAttendees))
                            {
                                GenerateError(this.GetType(), "EstNumberOfAttendees", RecordNum, ErrMsg);
                                IsValid = false;
                            }

                            if (IsValid)
                            {
                                IsValid = MinimumMaximumValue("999", EstNumberOfAttendees, EstPersonsProvidedAssistant);
                                if (!IsValid)
                                    GenerateError(this.GetType(), "EstPersonsProvidedAssistant", RecordNum, "Invalid value for estimated persons provided enrollment assistance");
                            }
                               
                            

                        }
                        return IsValid;
                    }
                }

                public class BoothOrExhibit : absRecordValidate
                {
                    [PamProperty("Estimated Number of Direct Interactions with Attendees - BoothOrExhibit", "Booth or Exhibit. At Heath Fair, Senior Fair, or Special Event.")]
                    public string EstNumberOfDirectInteractionsWithAttendees { get; set; }

                    [PamProperty("Estimated Persons Provided Enrollment Assistance - BoothOrExhibit", "Booth or Exhibit. At Heath Fair, Senior Fair, or Special Event.")]
                    public string EstPersonsProvidedAssistant { get; set; }


                    public void Load(string[] Fields)
                    {
                        //string[] Fields = Record.Split(sep);
                        EstNumberOfDirectInteractionsWithAttendees = Fields[131];
                        EstPersonsProvidedAssistant = Fields[132];
                        RecordNum = Fields[2];
                    }
                    public override bool AttemptValidate()
                    {
                        bool IsValid = true;

                        if (!string.IsNullOrEmpty(EstNumberOfDirectInteractionsWithAttendees) || !string.IsNullOrEmpty(EstPersonsProvidedAssistant))
                        {
                            if (!IsNumeric(EstNumberOfDirectInteractionsWithAttendees))
                            {
                                GenerateError(this.GetType(), "EstNumberOfDirectInteractionsWithAttendees", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                           

                           
                            if (!IsNumeric(EstPersonsProvidedAssistant))
                            {
                                GenerateError(this.GetType(), "EstPersonsProvidedAssistant", RecordNum, ErrMsg);
                                IsValid = false;
                            }

                            if (IsValid)
                            {
                                if (!IsValidRange(0, 9999, int.Parse(EstNumberOfDirectInteractionsWithAttendees)))
                                {
                                    this.GenerateError(this.GetType(), "EstNumberOfDirectInteractionsWithAttendees", RecordNum, ErrMsg);
                                    IsValid = false;
                                }
                                else
                                {
                                    IsValid = MinimumMaximumValue("9999", EstNumberOfDirectInteractionsWithAttendees, EstPersonsProvidedAssistant);
                                    if (!IsValid)
                                    {
                                        this.GenerateError(this.GetType(), "EstNumberOfDirectInteractionsWithAttendees", RecordNum, ErrMsg);
                                        IsValid = false;
                                    }
                                }
                            }
                           
                            
                        }
                        return IsValid;
                    }
                }

                public class DedicatedEnrollment : absRecordValidate
                {
                    [PamProperty("Estimated Number Persons Reached at Event Regardless of Enroll Assistance", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
                    public string EstNumberPersonsReachedAtEvent { get; set; }

                    [PamProperty("Estimated Number Persons Provided Any Enrollment Assistance - DedicatedEnrollment", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
                    public string EstPersonsProvidedAssistant { get; set; }

                    [PamProperty("Estimated Number Provided Enrollment Assistance with Part D", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
                    public string EstPersonsProvidedAssistantPartD { get; set; }


                    [PamProperty("Estimated Number Provided Enrollment Assistance with LIS", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
                    public string EstPersonsProvidedAssistantLis { get; set; }

                    [PamProperty("Estimated Number Provided Enrollment Assistance with MSP", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
                    public string EstPersonsProvidedAssistantMSRP { get; set; }

                    [PamProperty("Estimated Number Provided Enrollment Assist Other Medicare Program", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
                    public string EstPersonsProvidedAssistantOtherMedicareProgram { get; set; }


                    public void Load(string[] Fields)
                    {
                        //string[] Fields = Record.Split(sep);
                        EstNumberPersonsReachedAtEvent = Fields[133];
                        EstPersonsProvidedAssistant = Fields[134];
                        EstPersonsProvidedAssistantPartD = Fields[135];
                        EstPersonsProvidedAssistantLis = Fields[136];
                        EstPersonsProvidedAssistantMSRP = Fields[137];
                        EstPersonsProvidedAssistantOtherMedicareProgram = Fields[138];
                        RecordNum = Fields[2];
                    }

                    public override bool AttemptValidate()
                    {
                        try
                        {
                            bool IsValid = true;
                            
                            if (!string.IsNullOrEmpty(EstPersonsProvidedAssistant) ||
                                !string.IsNullOrEmpty(EstPersonsProvidedAssistantPartD) ||
                                !string.IsNullOrEmpty(EstPersonsProvidedAssistantLis) ||
                                !string.IsNullOrEmpty(EstPersonsProvidedAssistantMSRP) ||
                                !string.IsNullOrEmpty(EstPersonsProvidedAssistantOtherMedicareProgram))
                            {
                                if (!IsNumeric(EstNumberPersonsReachedAtEvent))
                                {
                                    GenerateError(GetType(), "EstNumberPersonsReachedAtEvent", RecordNum, ErrMsg);
                                    IsValid = false;
                                }
                            }

                            if (IsValid)
                            {
                                if (!string.IsNullOrEmpty(EstPersonsProvidedAssistant))
                                {
                                    IsValid = MinimumMaximumValue("999", EstNumberPersonsReachedAtEvent, EstPersonsProvidedAssistant);
                                    if (!IsValid)
                                    {
                                        GenerateError(GetType(), "EstPersonsProvidedAssistant", RecordNum, ErrMsg);
                                        return false;
                                    }
                                }
                                bool retValue = false;
                                if (!string.IsNullOrEmpty(EstPersonsProvidedAssistantPartD))
                                {
                                    retValue = MinimumMaximumValue("999", EstNumberPersonsReachedAtEvent, EstPersonsProvidedAssistantPartD);
                                    if (!retValue)
                                        GenerateError(GetType(), "EstPersonsProvidedAssistantPartD", RecordNum, ErrMsg);

                                    IsValid = IsValid && retValue;
                                }

                                if (!string.IsNullOrEmpty(EstPersonsProvidedAssistantLis))
                                {
                                    retValue = MinimumMaximumValue("999", EstNumberPersonsReachedAtEvent, EstPersonsProvidedAssistantLis);
                                    if (!retValue)
                                        GenerateError(GetType(), "EstPersonsProvidedAssistantLis", RecordNum, ErrMsg);

                                    IsValid = retValue;
                                }

                                if (!string.IsNullOrEmpty(EstPersonsProvidedAssistantMSRP))
                                {
                                    retValue = MinimumMaximumValue("999", EstNumberPersonsReachedAtEvent, EstPersonsProvidedAssistantMSRP);
                                    if (!retValue)
                                        GenerateError(GetType(), "EstPersonsProvidedAssistantMSRP", RecordNum, ErrMsg);
                                    IsValid = retValue;
                                }

                                if (!string.IsNullOrEmpty(EstPersonsProvidedAssistantOtherMedicareProgram))
                                {
                                    retValue = MinimumMaximumValue("999", EstNumberPersonsReachedAtEvent, EstPersonsProvidedAssistantOtherMedicareProgram);
                                    if (!retValue)
                                        GenerateError(GetType(), "EstPersonsProvidedAssistantOtherMedicareProgram", RecordNum, ErrMsg);
                                    IsValid = retValue;
                                }
                            }
                            return IsValid;
                        }
                        catch (System.Exception ex)
                        {
                            int x = 0;
                            return false;
                        }

                        }
                }


                public class RadioShowLiveOrTaped : absRecordValidate
                {
                    [PamProperty("Estimated Number of Listeners Reached", "Radio Show. Live or Taped. Not a Public Service Announce or Ad")]
                    public string EstNumberOfListenersReached { get; set; }

                    public override bool AttemptValidate()
                    {
                        bool IsValid = true;
                        //This is not a required field so do not generate an error.
                        if (!string.IsNullOrEmpty(EstNumberOfListenersReached))
                        {
                            if (IsNumeric(EstNumberOfListenersReached))
                            {

                                if (!IsValidRange(0, 999999, int.Parse(EstNumberOfListenersReached)))
                                {
                                    GenerateError(this.GetType(), "EstNumberOfListenersReached", RecordNum, ErrMsg);
                                }
                            }
                            else
                            {
                                GenerateError(this.GetType(), "EstNumberOfListenersReached", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                        }
                        return IsValid;
                    }

                    public void Load(string[] Fields)
                    {
                        EstNumberOfListenersReached = Fields[139];
                        RecordNum = Fields[2];
                    }
                }

                public class TVOrCableShowLiveOrTaped : absRecordValidate
                {
                    [PamProperty("Estimated Number of Listeners Reached", "TV or Cable Show. Live or Taped. Not a Public Service Announce or Ad")]
                    public string EstNumberOfViewersReached { get; set; }

                    public override bool AttemptValidate()
                    {
                        bool IsValid = true;
                        if (!string.IsNullOrEmpty(EstNumberOfViewersReached))
                        {
                            if (IsNumeric(EstNumberOfViewersReached))
                            {
                                if (!IsValidRange(0, 999999, int.Parse(EstNumberOfViewersReached)))
                                {
                                    GenerateError(this.GetType(), "EstNumberOfViewersReached", RecordNum, ErrMsg);
                                }
                            }
                            else
                            {
                                IsValid = false;
                            }
                        }
                        return IsValid;
                    }

                    public void Load(string[] Fields)
                    {
                        EstNumberOfViewersReached = Fields[140];
                        RecordNum = Fields[2];
                    }
                }


                public class ElectronicOtherActivity : absRecordValidate
                {
                    [PamProperty("Estimated Persons Viewing or Listening to PSA", "Electronic Other Activity. PSAs, Electronic Ads, Crawls, Video Conf, Web Conf, Web Chat")]
                    public string EstPersonsViewingOrListeningToPSA { get; set; }

                    public override bool AttemptValidate()
                    {
                        bool IsValid = true;
                        if (!string.IsNullOrEmpty(EstPersonsViewingOrListeningToPSA))
                        {
                            //This is not a required field so do not generate an error.
                            if (IsNumeric(EstPersonsViewingOrListeningToPSA))
                            {
                                IsValid = IsValidRange(0, 9999999, int.Parse(EstPersonsViewingOrListeningToPSA));
                                if (!IsValid)
                                {
                                    GenerateError(this.GetType(), "EstPersonsViewingOrListeningToPSA", RecordNum, ErrMsg);
                                }
                            }
                            else
                            {
                                IsValid = false;

                            }
                        }
                        return IsValid;

                    }
                    public void Load(string[] Fields)
                    {
                        EstPersonsViewingOrListeningToPSA = Fields[141];
                        RecordNum = Fields[2];
                    }

                }

                public class PAMEvent : absRecordValidate
                {
                    //sammit added all the event type to have the validation done..


                    [PamProperty("All Activity event validation", "All Activity event validation Error.")]
                    public string EventyTypeValidation { get; set; }

                    [PamProperty("Estimated Number of Attendees for activity or event - Interactive Presentation to Public", "Interactive Presentation to Public. Face to Face In-Person.")]
                    public string InteractiveEstAttendees { get; set; }

                    [PamProperty("Estimated Persons Provided Enrollment Assistance - Interactive Presentation to Public - Interactive Presentation to Public", "Interactive Presentation to Public. Face to Face In-Person.")]
                    public string InteractiveEstProvidedEnrollAssistance { get; set; }


                    [PamProperty("Estimated Number of Direct Interactions with Attendees - BoothOrExhibit", "Booth or Exhibit. At Heath Fair, Senior Fair, or Special Event.")]
                    public string BoothEstDirectContacts { get; set; }

                    [PamProperty("Estimated Persons Provided Enrollment Assistance - BoothOrExhibit", "Booth or Exhibit. At Heath Fair, Senior Fair, or Special Event.")]
                    public string BoothEstEstProvidedEnrollAssistance { get; set; }



                    [PamProperty("Estimated Number Persons Reached at Event Regardless of Enroll Assistance", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
                    public string DedicatedEstPersonsReached { get; set; }

                    [PamProperty("Estimated Number Persons Provided Any Enrollment Assistance - DedicatedEnrollment", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
                    public string DedicatedEstAnyEnrollmentAssistance { get; set; }

                    [PamProperty("Estimated Number Provided Enrollment Assistance with Part D", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
                    public string DedicatedEstPartDEnrollmentAssistance { get; set; }


                    [PamProperty("Estimated Number Provided Enrollment Assistance with LIS", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
                    public string DedicatedEstLISEnrollmentAssistance { get; set; }

                    [PamProperty("Estimated Number Provided Enrollment Assistance with MSP", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
                    public string DedicatedEstMSPEnrollmentAssistance { get; set; }

                    [PamProperty("Estimated Number Provided Enrollment Assist Other Medicare Program", " Dedicated Enrollment Event Sponsored By SHIP or in Partnership.")]
                    public string DedicatedEstOtherEnrollmentAssistance { get; set; }



                    [PamProperty("Estimated Number of Listeners Reached", "Radio Show. Live or Taped. Not a Public Service Announce or Ad")]
                    public string RadioEstListenerReach { get; set; }



                    [PamProperty("Estimated Number of Listeners Reached", "TV or Cable Show. Live or Taped. Not a Public Service Announce or Ad")]
                    public string TVEstViewersReach { get; set; }



                    [PamProperty("Estimated Persons Viewing or Listening to PSA", "Electronic Other Activity. PSAs, Electronic Ads, Crawls, Video Conf, Web Conf, Web Chat")]
                    public string ElectronicEstPersonsViewingOrListening { get; set; }



                    [PamProperty("EstPersonsReadingArticle", "Activity")]
                    public string EstPersonsReadingArticle { get; set; }



                    [PamProperty("StartDate", "Start Date of Activity")]
                    public string StartDate { get; set; }

                    [PamProperty("EndDate", "End Date of Activity")]
                    public string EndDate { get; set; }


                    [PamProperty("Name of event", "Activity.")]
                    public string EventName { get; set; }
                    [PamProperty("Contact first name", "Activity.")]
                    public string ContactFirstName { get; set; }
                    [PamProperty("Contact last name", "Activity.")]
                    public string ContactLastName { get; set; }
                    [PamProperty("Contact phone", "Activity.")]
                    public string ContactPhone { get; set; }
                    [PamProperty("State", "Activity.")]
                    public string FIPSEventState { get; set; }
                    [PamProperty("Event", "Activity.")]
                    public string FIPSEventCounty { get; set; }
                    [PamProperty("Zip", "Activity.")]
                    public string FIPSEventZip { get; set; }
                    [PamProperty("City", "Activity.")]
                    public string FIPSEventCity { get; set; }
                    [PamProperty("Address", "Activity.")]
                    public string FIPSAddressCity { get; set; }


                    public int? ToNullableInt32(string s)
                    {
                        int i;
                        if (Int32.TryParse(s, out i)) return i;
                        return null;
                    }


                    string ValidateSections()
                    {
                        string _validationError = string.Empty;

                        
                       //section 1
                       
                        int? interactiveEstAttendees = ToNullableInt32(InteractiveEstAttendees);
                        int? interactiveEstProvidedEnrollAssistance = ToNullableInt32(InteractiveEstProvidedEnrollAssistance);

                        //section 2
                        int? boothEstDirectContacts = ToNullableInt32(BoothEstDirectContacts);
                        int? boothEstEstProvidedEnrollAssistance = ToNullableInt32(BoothEstEstProvidedEnrollAssistance);

                        //section 3
                        int? dedicatedEstPersonsReached = ToNullableInt32(DedicatedEstPersonsReached);
                        int? dedicatedEstAnyEnrollmentAssistance = ToNullableInt32(DedicatedEstAnyEnrollmentAssistance);
                        int? dedicatedEstLISEnrollmentAssistance = ToNullableInt32(DedicatedEstLISEnrollmentAssistance);
                        int? dedicatedEstPartDEnrollmentAssistance = ToNullableInt32(DedicatedEstPartDEnrollmentAssistance);
                        int? dedicatedEstMSPEnrollmentAssistance = ToNullableInt32(DedicatedEstMSPEnrollmentAssistance);
                        int? dedicatedEstOtherEnrollmentAssistance = ToNullableInt32(DedicatedEstOtherEnrollmentAssistance);

                        //section 4
                        int? radioEstListenerReach = ToNullableInt32(RadioEstListenerReach);

                        //section 5
                        int? tVEstViewersReach = ToNullableInt32(TVEstViewersReach);

                        //section 6
                        int? electronicEstPersonsViewingOrListening = ToNullableInt32(ElectronicEstPersonsViewingOrListening);
                       

                        //section 7
                        int? printEstPersonsReading = ToNullableInt32(EstPersonsReadingArticle);

                        //check at least one section value is populated
                        if (interactiveEstAttendees == null
                            && interactiveEstProvidedEnrollAssistance == null
                            && boothEstDirectContacts == null
                            && boothEstEstProvidedEnrollAssistance == null
                            && dedicatedEstPersonsReached == null
                            && dedicatedEstAnyEnrollmentAssistance == null
                            && dedicatedEstLISEnrollmentAssistance == null
                            && dedicatedEstPartDEnrollmentAssistance == null
                            && dedicatedEstMSPEnrollmentAssistance == null
                            && dedicatedEstOtherEnrollmentAssistance == null
                            && radioEstListenerReach == null
                            && tVEstViewersReach == null
                            && electronicEstPersonsViewingOrListening == null
                            && printEstPersonsReading == null)
                        {
                            _validationError = "You have to enter value for atleast one Event Type.";
                            return _validationError;
                        }

                        //check Section 1
                        if (interactiveEstAttendees >= 0 || interactiveEstProvidedEnrollAssistance >= 0)
                        {
                            //all the value in the section should be 0 or > 0
                            if (interactiveEstAttendees == null || interactiveEstProvidedEnrollAssistance == null)
                                _validationError = "You have to enter a non-blank value for all the boxes in Event 1.";

                            //check all the other section value should be null
                            if (boothEstDirectContacts != null
                                || boothEstEstProvidedEnrollAssistance != null
                                || dedicatedEstPersonsReached != null
                                || dedicatedEstAnyEnrollmentAssistance != null
                                || dedicatedEstLISEnrollmentAssistance != null
                                || dedicatedEstPartDEnrollmentAssistance != null
                                || dedicatedEstMSPEnrollmentAssistance != null
                                || dedicatedEstOtherEnrollmentAssistance != null
                                || radioEstListenerReach != null
                                || tVEstViewersReach != null
                                || electronicEstPersonsViewingOrListening != null
                                || printEstPersonsReading != null)
                            {

                                _validationError += "<br /> You have to enter value for only one Event Type.";
                                return _validationError;
                            }

                        }
                        //section 1 interactiveEstAttendees should be the gt value
                        if (interactiveEstProvidedEnrollAssistance > interactiveEstAttendees)
                        {
                            _validationError += "<br /> Estimated Number of Attendees should be greater than Estimated Persons Provided Enrollment Assistance in Event 1.";
                            return _validationError;
                        }

                        //check Section 2
                        if (boothEstDirectContacts >= 0 || boothEstEstProvidedEnrollAssistance >= 0)
                        {
                            //all the value in the section should be 0 or > 0
                            if (boothEstDirectContacts == null || boothEstEstProvidedEnrollAssistance == null)
                                _validationError = "You have to enter a non-blank value for all the boxes in Event 2.";

                            //check all the other section value should be null
                            if (interactiveEstAttendees != null
                                || interactiveEstProvidedEnrollAssistance != null
                                || dedicatedEstPersonsReached != null
                                || dedicatedEstAnyEnrollmentAssistance != null
                                || dedicatedEstLISEnrollmentAssistance != null
                                || dedicatedEstPartDEnrollmentAssistance != null
                                || dedicatedEstMSPEnrollmentAssistance != null
                                || dedicatedEstOtherEnrollmentAssistance != null
                                || radioEstListenerReach != null
                                || tVEstViewersReach != null
                                || electronicEstPersonsViewingOrListening != null
                                || printEstPersonsReading != null)
                            {
                                _validationError += "<br /> You have to enter value for only one Event Type.";
                                return _validationError;
                            }

                        }
                        //section 2 boothEstDirectContacts should be the gt value
                        if (boothEstEstProvidedEnrollAssistance > boothEstDirectContacts)
                        {
                            _validationError += "<br /> Estimated Number of Direct Interactions with Attendees should be greater than Estimated Persons Provided Enrollment Assistance in Event 2.";
                            return _validationError;
                        }

                        //check Section 3 
                        if (dedicatedEstPersonsReached >= 0 || dedicatedEstAnyEnrollmentAssistance >= 0
                            || dedicatedEstLISEnrollmentAssistance >= 0 || dedicatedEstPartDEnrollmentAssistance >= 0
                                        || dedicatedEstMSPEnrollmentAssistance >= 0 || dedicatedEstOtherEnrollmentAssistance >= 0
                            )
                        {
                            //all the value in the section should be 0 or > 0
                            if (dedicatedEstPersonsReached == null || dedicatedEstAnyEnrollmentAssistance == null
                                 || dedicatedEstLISEnrollmentAssistance == null || dedicatedEstPartDEnrollmentAssistance == null
                                   || dedicatedEstMSPEnrollmentAssistance == null || dedicatedEstOtherEnrollmentAssistance == null
                                )
                                _validationError = "You have to enter a non-blank value for all the boxes in Event 3.";

                            //check all the other section value should be null
                            if (interactiveEstAttendees != null
                                || interactiveEstProvidedEnrollAssistance != null
                                || boothEstDirectContacts != null
                                || boothEstEstProvidedEnrollAssistance != null
                                || radioEstListenerReach != null
                                || tVEstViewersReach != null
                                || electronicEstPersonsViewingOrListening != null
                                || printEstPersonsReading != null)
                            {
                                _validationError += "<br /> You have to enter value for only one Event Type.";
                                return _validationError;
                            }

                        }
                        //section 3 dedicatedEstPersonsReached should be the gt value
                        if (dedicatedEstAnyEnrollmentAssistance > dedicatedEstPersonsReached
                            || dedicatedEstLISEnrollmentAssistance > dedicatedEstPersonsReached
                            || dedicatedEstPartDEnrollmentAssistance > dedicatedEstPersonsReached
                            || dedicatedEstMSPEnrollmentAssistance > dedicatedEstPersonsReached
                            || dedicatedEstOtherEnrollmentAssistance > dedicatedEstPersonsReached
                            )
                        {
                            _validationError += "<br /> Est Number Persons Reached at Event Regardless of Enroll Assistance should be the greatest value in Event 3.";
                            return _validationError;
                        }


                        //check Section 4
                        if (radioEstListenerReach > 0)
                        {
                            //check all the other section value should be null
                            if (interactiveEstAttendees != null
                                || interactiveEstProvidedEnrollAssistance != null
                                || boothEstDirectContacts != null
                                || boothEstEstProvidedEnrollAssistance != null
                                || dedicatedEstPersonsReached != null
                                || dedicatedEstAnyEnrollmentAssistance != null
                                || dedicatedEstLISEnrollmentAssistance != null
                                || dedicatedEstPartDEnrollmentAssistance != null
                                || dedicatedEstMSPEnrollmentAssistance != null
                                || dedicatedEstOtherEnrollmentAssistance != null
                                || tVEstViewersReach != null
                                || electronicEstPersonsViewingOrListening != null
                                || printEstPersonsReading != null)
                            {
                                _validationError = "You have to enter value for only one Event Type.";
                                return _validationError;
                            }

                        }

                        //check Section 5
                        if (tVEstViewersReach > 0)
                        {
                            //check all the other section value should be null
                            if (interactiveEstAttendees != null
                                || interactiveEstProvidedEnrollAssistance != null
                                || boothEstDirectContacts != null
                                || boothEstEstProvidedEnrollAssistance != null
                                || dedicatedEstPersonsReached != null
                                || dedicatedEstAnyEnrollmentAssistance != null
                                || dedicatedEstLISEnrollmentAssistance != null
                                || dedicatedEstPartDEnrollmentAssistance != null
                                || dedicatedEstMSPEnrollmentAssistance != null
                                || dedicatedEstOtherEnrollmentAssistance != null
                                || radioEstListenerReach != null
                                || electronicEstPersonsViewingOrListening != null
                                || printEstPersonsReading != null)
                            {
                                _validationError = "You have to enter value for only one Event Type.";
                                return _validationError;
                            }

                        }

                        //check Section 6: todo
                        if (electronicEstPersonsViewingOrListening > 0)
                        {

                            //check all the other section value should be null
                            if (interactiveEstAttendees != null
                                || interactiveEstProvidedEnrollAssistance != null
                                || boothEstDirectContacts != null
                                || boothEstEstProvidedEnrollAssistance != null
                                || dedicatedEstPersonsReached != null
                                || dedicatedEstAnyEnrollmentAssistance != null
                                || dedicatedEstLISEnrollmentAssistance != null
                                || dedicatedEstPartDEnrollmentAssistance != null
                                || dedicatedEstMSPEnrollmentAssistance != null
                                || dedicatedEstOtherEnrollmentAssistance != null
                                || radioEstListenerReach != null
                                || tVEstViewersReach != null
                                || printEstPersonsReading != null)
                            {
                                _validationError = "You have to enter value for only one Event Type.";
                                return _validationError;
                            }

                        }

                        //check Section 7
                        if (printEstPersonsReading > 0)
                        {

                            //check all the other section value should be null
                            if (interactiveEstAttendees != null
                                || interactiveEstProvidedEnrollAssistance != null
                                || boothEstDirectContacts != null
                                || boothEstEstProvidedEnrollAssistance != null
                                || dedicatedEstPersonsReached != null
                                || dedicatedEstAnyEnrollmentAssistance != null
                                || dedicatedEstLISEnrollmentAssistance != null
                                || dedicatedEstPartDEnrollmentAssistance != null
                                || dedicatedEstMSPEnrollmentAssistance != null
                                || dedicatedEstOtherEnrollmentAssistance != null
                                || radioEstListenerReach != null
                                || tVEstViewersReach != null
                                || electronicEstPersonsViewingOrListening != null)
                            {
                                _validationError = "You have to enter value for only one Event Type.";
                                return _validationError;
                            }

                        }

                        return _validationError;

                    }

                    public override bool AttemptValidate()
                    {
                        bool IsValid = false;

                        // Estimated Persons Reading Article
                        if (!string.IsNullOrEmpty(EstPersonsReadingArticle))
                        {
                            IsValid = IsNumeric(EstPersonsReadingArticle);
                            if (IsValid)
                            {
                                IsValid = IsValidRange(0, 9999999, int.Parse(EstPersonsReadingArticle));
                                if (!IsValid)
                                {
                                    GenerateError(this.GetType(), EstPersonsReadingArticle, RecordNum, ErrMsg);
                                }
                            }
                            else
                            {
                                GenerateError(this.GetType(), "EstPersonsReadingArticle", RecordNum, ErrMsg);
                            }
                        }

                        //validate all the event types :sammit
                        string _Errors = ValidateSections();
                        if (_Errors.Length > 0)
                        {
                            IsValid = false;
                            GenerateError(this.GetType(), "EventyTypeValidation", RecordNum, _Errors);
                        }



                        //StartDate
                        if (IsRequired(StartDate))
                        {
                            try
                            {
                                StartDate = FormatDate(StartDate);

                            }
                            catch (ApplicationException exStartDateApp)
                            {
                                GenerateError(this.GetType(), "StartDate", RecordNum, "Start Date " + exStartDateApp.Message);
                                IsValid = false;
                            }
                        }
                        else
                        {
                            GenerateError(this.GetType(), "StartDate", RecordNum, ErrMsg);
                            IsValid = false;
                        }

                        //EndDate
                        if (IsRequired(EndDate))
                        {
                            try
                            {
                                EndDate = FormatDate(EndDate);
                            }
                            catch (ApplicationException exEndDateApp)
                            {
                                GenerateError(this.GetType(), "EndDate", RecordNum, "End Date " + exEndDateApp.Message);
                                IsValid = false;
                            }

                            try
                            {
                                DateTime dStart = Convert.ToDateTime(StartDate);
                                if (dStart > Convert.ToDateTime(EndDate))
                                {
                                    GenerateError(this.GetType(), "StartDate", RecordNum, "Invalid activity date endDate cannot come before start date.");
                                    IsValid = false;
                                }
                                else
                                {
                                    string EarliestStartDate = ConfigurationManager.AppSettings["EarlyStartDate"].ToString();
                                    if (string.IsNullOrEmpty(EarliestStartDate))
                                    {
                                        throw (new System.Configuration.ConfigurationErrorsException("Configuration exception missing EarlyStartDate"));
                                    }
                                    DateTime dtEarliestStartDate = Convert.ToDateTime(EarliestStartDate);

                                    if (dStart < dtEarliestStartDate)
                                    {

                                        GenerateError(this.GetType(), "StartDate", RecordNum, "Invalid activity start date.  Start date before " + dtEarliestStartDate.ToLongDateString() + " not permitted.");
                                        IsValid = false;
                                    }



                                }
                            }
                            catch (System.FormatException exFormat)
                            {
                                GenerateError(this.GetType(), "StartDate", RecordNum, "Invalidation check failed start date before end date. Invalid date format.");
                                IsValid = false;
                            }
                        }
                        else
                        {
                            GenerateError(this.GetType(), "EndDate", RecordNum, ErrMsg);
                            IsValid = false;
                        }

                        //Eventname
                        if (!IsRequired(EventName))
                        {
                            GenerateError(this.GetType(), "EventName", RecordNum, ErrMsg);
                            IsValid = false;

                        }
                        else
                        {
                            if (!ValidateRequiredMaxLength(EventName, 255))
                            {
                                GenerateError(this.GetType(), "EventName", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                        }

                        //Contact First Name
                        if (!string.IsNullOrEmpty(ContactFirstName))
                        {
                            if (!ValidateRequiredMaxLength(ContactFirstName, 50))
                            {
                                GenerateError(this.GetType(), "ContactFirstName", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                        }

                        //Contact Last Name
                        if (!string.IsNullOrEmpty(ContactLastName))
                        {
                            if (!ValidateRequiredMaxLength(ContactLastName, 50))
                            {
                                GenerateError(this.GetType(), "ContactLastName", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                        }

                        //Contact Phone
                        if (!string.IsNullOrEmpty(ContactPhone))
                        {
                            if (!ValidateRequiredMaxLength(ContactPhone, 20))
                            {
                                GenerateError(this.GetType(), "ContactPhone", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                        }



                        if (!IsRequired(FIPSEventState))
                        {
                            GenerateError(this.GetType(), "FIPSEventState", RecordNum, ErrMsg);
                        }
                        else
                        {
                            if (!ValidateRequiredMaxLength(FIPSEventState, 2))
                            {
                                GenerateError(this.GetType(), "FIPSEventState", RecordNum, "Invalid value for State FIPS code of event found.");
                                IsValid = false;
                            }
                            else
                            {
                                if (!LookupDAL.IsStateFipCodeValid(FIPSEventState))
                                {
                                    GenerateError(this.GetType(), "FIPSEventState", RecordNum, "Invalid State Code of event found.");
                                    IsValid = false;
                                }
                            }
                        }

                        if (!IsRequired(FIPSEventZip))
                        {

                            GenerateError(this.GetType(), "FIPSEventZip", RecordNum, ErrMsg);
                            IsValid = false;
                        }
                        else
                        {
                            if (!IsValidLength(FIPSEventZip, 5))
                            {
                                GenerateError(this.GetType(), "FIPSEventZip", RecordNum, "Invalid value for zip code of event found");
                                IsValid = false;
                            }

                            ////Sammit: out of state zip code is allowed
                            ////else
                            ////{
                            ////    if (!LookupDAL.IsZipCodeValidForState(FIPSEventState, FIPSEventZip))
                            ////    {
                            ////        GenerateError(this.GetType(), "FIPSEventZip", RecordNum, "Invalid zip code of event found");
                            ////        IsValid = false;
                            ////    }
                            ////}
                        }



                        if (!IsRequired(FIPSEventCounty))
                        {
                            GenerateError(this.GetType(), "FIPSEventCounty", RecordNum, ErrMsg);
                            IsValid = false;
                        }
                        else
                        {
                            if (!IsValidLength(FIPSEventCounty.Trim(), 5))
                            {
                                GenerateError(this.GetType(), "FIPSEventCounty", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                if (!LookupDAL.IsZipCodeValidForCounty(FIPSEventCounty, FIPSEventZip))
                                {
                                    GenerateError(this.GetType(), "FIPSEventCounty", RecordNum, "Invalid county Fips county code for zip code of event.");
                                    IsValid = false;

                                }
                            }
                        }



                        if (!IsRequired(FIPSEventCity))
                        {
                            GenerateError(this.GetType(), "FIPSEventCity", RecordNum, ErrMsg);
                            IsValid = false;
                        }
                        else
                        {
                            if (!ValidateRequiredMaxLength(FIPSEventCity, 50))
                            {
                                GenerateError(this.GetType(), "FIPSEventCity", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                        }

                        if (!IsRequired(FIPSAddressCity))
                        {
                            GenerateError(this.GetType(), "FIPSAddressCity", RecordNum, ErrMsg);
                            IsValid = false;
                        }
                        else
                        {
                            if (!ValidateRequiredMaxLength(FIPSAddressCity, 50))
                            {
                                GenerateError(this.GetType(), "FIPSAddressCity", RecordNum, ErrMsg);
                                IsValid = false;
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

                    public void Load(string[] Fields)
                    {
                        //string[] Fields = Record.Split(sep);
                        //sammit adde all the event types
                        EventyTypeValidation = string.Empty;

                        //section 1
                        InteractiveEstAttendees = Fields[129];
                        InteractiveEstProvidedEnrollAssistance = Fields[130];

                        //section 2
                        BoothEstDirectContacts = Fields[131];
                        BoothEstEstProvidedEnrollAssistance= Fields[132];

                        
                        //section 3
                        DedicatedEstPersonsReached = Fields[133];
                        DedicatedEstAnyEnrollmentAssistance = Fields[134];
                        DedicatedEstPartDEnrollmentAssistance = Fields[135];
                        DedicatedEstLISEnrollmentAssistance = Fields[136];
                        DedicatedEstMSPEnrollmentAssistance = Fields[137];
                        DedicatedEstOtherEnrollmentAssistance = Fields[138];


                        //section 4
                        RadioEstListenerReach = Fields[139];

                        //section 5
                        TVEstViewersReach = Fields[140];

                        //section 6
                        ElectronicEstPersonsViewingOrListening = Fields[141];


                        //section 7
                        EstPersonsReadingArticle = Fields[142];


                        StartDate = Fields[143];
                        EndDate = Fields[144];

                        EventName = Fields[145];
                        ContactFirstName = Fields[146];
                        ContactLastName = Fields[147];
                        ContactPhone = Fields[148];
                        FIPSEventState = Fields[149];
                        FIPSEventCounty = Fields[150];
                        FIPSEventZip = Fields[151];
                        FIPSEventCity = Fields[152];
                        FIPSAddressCity = Fields[153];
                        RecordNum = Fields[2];
                    }
                }

                public class TopicFocus : absRecordValidate
                {
                    [PamProperty("Medicare Parts A and B", "Topic.")]
                    public string MedicareAB { get; set; }

                    [PamProperty("Plan Issues - Non-Renewal, Termination, Employer-COBRA", "Topic.")]
                    public string NonRenewalSituation { get; set; }

                    [PamProperty("Long-Term Care", "Topic.")]
                    public string LongTermCare { get; set; }


                    [PamProperty("Medigap - Medicare Supplements", "Topic.")]
                    public string MedigapMedicare { get; set; }

                    [PamProperty("Medicare Fraud and Abuse", "Topic.")]
                    public string FraudAbuse { get; set; }

                    [PamProperty("Medicare Prescription Drug Coverage - PDP / MA-PD", "Topic.")]
                    public string MedicarePrescriptionDrug { get; set; }


                    [PamProperty("Other Prescription Drug Coverage - Assistance", "Topic.")]
                    public string OtherPrescriptionDrug { get; set; }

                    [PamProperty("Medicare Advantage", "Topic.")]
                    public string MedicareAdvantage { get; set; }

                    [PamProperty("QMB - SLMB - QI", "Topic.")]
                    public string QMBSLMBQI { get; set; }

                    [PamProperty("Other Medicaid", "Topic.")]
                    public string OtherMedicaid { get; set; }

                    [PamProperty(" General SHIP Program Information", "Topic.")]
                    public string MedicareHealthPlans { get; set; }

                    [PamProperty("Medicare Preventive Services", "Topic.")]
                    public string MedicarePreventiveSevices { get; set; }

                    [PamProperty("Medicare Preventive Services", "Topic.")]
                    public string LowIncomeAssistant { get; set; }


                    [PamProperty("Low-Income Assistance", "Topic.")]
                    public string DualEligibleMentalAssistant { get; set; }


                    [PamProperty("Volunteer Recruitment", "Topic.")]
                    public string VolunteerRecruitment { get; set; }

                    [PamProperty("Partnership Recruitment", "Topic.")]
                    public string PartnershipRecruitment { get; set; }

                    [PamProperty("Other Topics - Description", "Topic.")]
                    public string OtherDescription { get; set; }

                    public override bool AttemptValidate()
                    {
                        bool ItemSelected = false;
                        bool IsValid = true;
                        if (!string.IsNullOrEmpty(MedicareAB))
                        {
                            if (!IsBooleanValue(MedicareAB))
                            {
                                GenerateError(this.GetType(), "MedicareAB", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                //Property has value set to true
                                ItemSelected = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(NonRenewalSituation))
                        {
                            if (!IsBooleanValue(NonRenewalSituation))
                            {
                                GenerateError(this.GetType(), "NonRenewalSituation", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                //Property has value set to true
                                ItemSelected = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(LongTermCare))
                        {
                            if (!IsBooleanValue(LongTermCare))
                            {
                                GenerateError(this.GetType(), "LongTermCare", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                //Property has value set to true
                                ItemSelected = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(MedigapMedicare))
                        {
                            if (!IsBooleanValue(MedigapMedicare))
                            {
                                GenerateError(this.GetType(), "MedigapMedicare", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                //Property has value set to true
                                ItemSelected = true;
                            }
                        }

                        if (string.IsNullOrEmpty(FraudAbuse))
                        {
                            if (!IsBooleanValue(FraudAbuse))
                            {
                                GenerateError(this.GetType(), "FraudAbuse", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                //Property has value set to true
                                ItemSelected = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(MedicarePrescriptionDrug))
                        {
                            if (!IsBooleanValue(MedicarePrescriptionDrug))
                            {
                                GenerateError(this.GetType(), "MedicarePrescriptionDrug", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                //Property has value set to true
                                ItemSelected = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(OtherPrescriptionDrug))
                        {
                            if (!IsBooleanValue(OtherPrescriptionDrug))
                            {
                                GenerateError(this.GetType(), "OtherPrescriptionDrug", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                //Property has value set to true
                                ItemSelected = true;
                            }
                        }

                        if (MedicareAdvantage == null)
                        {
                            if (!IsBooleanValue(MedicareAdvantage))
                            {
                                GenerateError(this.GetType(), "MedicareAdvantage", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                //Property has value set to true
                                ItemSelected = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(QMBSLMBQI))
                        {
                            if (!IsBooleanValue(QMBSLMBQI))
                            {
                                GenerateError(this.GetType(), "QMBSLMBQI", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                //Property has value set to true
                                ItemSelected = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(OtherMedicaid))
                        {
                            if (!IsBooleanValue(OtherMedicaid))
                            {
                                GenerateError(this.GetType(), "OtherMedicaid", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                //Property has value set to true
                                ItemSelected = true;
                            }
                        }


                        if (!string.IsNullOrEmpty(MedicareHealthPlans))
                        {
                            if (!IsBooleanValue(MedicareHealthPlans))
                            {
                                GenerateError(this.GetType(), "MedicareHealthPlans", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                //Property has value set to true
                                ItemSelected = true;
                            }
                        }


                        if (!string.IsNullOrEmpty(MedicarePreventiveSevices))
                        {
                            if (!IsBooleanValue(MedicarePreventiveSevices))
                            {
                                GenerateError(this.GetType(), "MedicarePreventiveSevices", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                //Property has value set to true
                                ItemSelected = true;
                            }
                        }


                        if (!string.IsNullOrEmpty(LowIncomeAssistant))
                        {
                            if (!IsBooleanValue(LowIncomeAssistant))
                            {
                                GenerateError(this.GetType(), "LowIncomeAssistant", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                //Property has value set to true
                                ItemSelected = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(VolunteerRecruitment))
                        {
                            if (!IsBooleanValue(VolunteerRecruitment))
                            {
                                GenerateError(this.GetType(), "VolunteerRecruitment", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                //Property has value set to true
                                ItemSelected = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(PartnershipRecruitment))
                        {
                            if (!IsBooleanValue(PartnershipRecruitment))
                            {
                                GenerateError(this.GetType(), "VolunteerRecruitment", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                            else
                            {
                                //Property has value set to true
                                ItemSelected = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(OtherDescription))
                        {
                            if (!ValidateRequiredMaxLength(OtherDescription, 255))
                            {
                                GenerateError(this.GetType(), "OtherDescription", RecordNum, ErrMsg);
                                IsValid = false;
                            }
                        }

                        if (!ItemSelected)
                        {
                            GenerateError(this.GetType(), "OtherDescription", RecordNum, "No topic was selected");
                            IsValid = false;
                        }
                        return IsValid;

                    }

                    public void Load(string[] Fields)
                    {
                        //string[] Fields = Record.Split(sep);
                        MedicareAB = Fields[154];
                        NonRenewalSituation = Fields[155];
                        LongTermCare = Fields[156];
                        MedigapMedicare = Fields[157];
                        FraudAbuse = Fields[158];
                        MedicarePrescriptionDrug = Fields[159];
                        OtherPrescriptionDrug = Fields[160];
                        MedicareAdvantage = Fields[161];
                        QMBSLMBQI = Fields[162];
                        OtherMedicaid = Fields[163];
                        MedicareHealthPlans = Fields[164];
                        MedicarePreventiveSevices = Fields[165];
                        LowIncomeAssistant = Fields[166];
                        DualEligibleMentalAssistant = Fields[167];
                        VolunteerRecruitment = Fields[168];
                        PartnershipRecruitment = Fields[169];
                        OtherDescription = Fields[170];
                        RecordNum = Fields[2];

                    }

                }

                public class Audience : absRecordValidate
                {
                    [PamProperty("Medicare Pre-Enrollees - Age 45-64", "Target Audience.")]
                    public string MedicarePreEnrollees { get; set; }

                    [PamProperty("Medicare Beneficiaries", "Target Audience.")]
                    public string MedicareBeneficiaries { get; set; }

                    [PamProperty("Family Members - Caregivers of Medicare Beneficiaries", "Target Audience.")]
                    public string FamilyMembersCaregivers { get; set; }

                    [PamProperty("Low-Income", "Target Audience.")]
                    public string LowIncome { get; set; }

                    [PamProperty("Hispanic, Latino, or Spanish Origin", "Target Audience.")]
                    public string Hispanic { get; set; }

                    [PamProperty("White, Non-Hispanic", "Target Audience.")]
                    public string WhiteNonHispanic { get; set; }

                    [PamProperty("Black, African American", "Target Audience.")]
                    public string BlackorAfricanAmerican { get; set; }

                    [PamProperty("American Indian", "Target Audience.")]
                    public string AmericanIndian { get; set; }

                    [PamProperty("Asian Indian", "Target Audience.")]
                    public string AsianIndian { get; set; }

                    [PamProperty("Chinese", "Target Audience.")]
                    public string Chinese { get; set; }

                    [PamProperty("Filipino", "Target Audience.")]
                    public string Filipino { get; set; }

                    [PamProperty("Japanese", "Target Audience.")]
                    public string Japanese { get; set; }

                    [PamProperty("Korean", "Target Audience.")]
                    public string Korean { get; set; }

                    [PamProperty("Vietnamese", "Target Audience.")]
                    public string Vietnamese { get; set; }

                    [PamProperty("Native Hawaiian", "Target Audience.")]
                    public string NativeHawaiian { get; set; }

                    [PamProperty("Guamanian Or Chamorro", "Target Audience.")]
                    public string GuamanianOrChamorro { get; set; }

                    [PamProperty("Samoan", "Target Audience.")]
                    public string Samoan { get; set; }

                    [PamProperty("Other Asian", "Target Audience.")]
                    public string OtherAsian { get; set; }

                    [PamProperty("Other Pacific Islander", "Target Audience.")]
                    public string OtherPacificIslander { get; set; }

                    [PamProperty("Some Other RaceEthnicity", "Target Audience.")]
                    public string SomeOtherRaceEthnicity { get; set; }

                    [PamProperty("Disabled", "Target Audience.")]
                    public string Disabled { get; set; }

                    [PamProperty("Rural", "Target Audience.")]
                    public string Rural { get; set; }

                    [PamProperty("Employer Related Groups", "Target Audience.")]
                    public string EmployerRelatedGroups { get; set; }

                    [PamProperty("Mental Health Proffessionals", "Target Audience.")]
                    public string MentalHealthProffessionals { get; set; }

                    [PamProperty("Socal Work Proffessionals", "Target Audience.")]
                    public string SocalWorkProffessionals { get; set; }

                    [PamProperty("Dual Eligible Groups", "Target Audience.")]
                    public string DualEligibleGroups { get; set; }

                    [PamProperty("Partnership OutReach", "Target Audience.")]
                    public string PartnerShipOutReach { get; set; }

                    [PamProperty("Partner Presentation to Groups", "Target Audience.")]
                    public string PartnerPresentationToGroups { get; set; }

                    [PamProperty("Others", "Target Audience.")]
                    public string Others { get; set; }


                    public override bool AttemptValidate()
                    {
                        bool IsValid = true;

                        if (!string.IsNullOrEmpty(MedicarePreEnrollees))
                        {
                            if (!IsBooleanValue(MedicarePreEnrollees))
                            {

                                IsValid = false;
                                GenerateError(this.GetType(), "MedicarePreEnrollees", RecordNum, ErrMsg);
                            }
                        }

                        if (!string.IsNullOrEmpty(MedicareBeneficiaries))
                        {
                            if (!IsBooleanValue(MedicareBeneficiaries))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "MedicareBeneficiaries", RecordNum, ErrMsg);
                            }
                        }

                        if (!string.IsNullOrEmpty(FamilyMembersCaregivers))
                        {
                            if (!IsBooleanValue(FamilyMembersCaregivers))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "FamilyMembersCaregivers", RecordNum, ErrMsg);
                            }
                        }

                        if (!string.IsNullOrEmpty(LowIncome))
                        {
                            if (!IsBooleanValue(LowIncome))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "FamilyMembersCaregivers", RecordNum, ErrMsg);
                            }
                        }

                        if (!string.IsNullOrEmpty(Hispanic))
                        {
                            if (!IsBooleanValue(Hispanic))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "Hispanic", RecordNum, ErrMsg);
                            }
                        }

                        if (!string.IsNullOrEmpty(WhiteNonHispanic))
                        {
                            if (!IsBooleanValue(WhiteNonHispanic))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "Hispanic", RecordNum, ErrMsg);

                            }
                        }

                        if (!string.IsNullOrEmpty(BlackorAfricanAmerican))
                        {
                            if (!IsBooleanValue(BlackorAfricanAmerican))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "BlackorAfricanAmerican", RecordNum, ErrMsg);
                            }
                        }

                        if (!string.IsNullOrEmpty(AmericanIndian))
                        {
                            if (!IsBooleanValue(AmericanIndian))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "AmericanIndian", RecordNum, ErrMsg);
                            }
                        }


                        if (!string.IsNullOrEmpty(AsianIndian))
                        {
                            if (!IsBooleanValue(AsianIndian))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "AsianIndian", RecordNum, ErrMsg);
                            }
                        }


                        if (!string.IsNullOrEmpty(Chinese))
                        {
                            if (!IsBooleanValue(Chinese))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "Chinese", RecordNum, ErrMsg);
                            }
                        }

                        if (!string.IsNullOrEmpty(Filipino))
                        {
                            if (!IsBooleanValue(Filipino))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "Filipino", RecordNum, ErrMsg);
                            }
                        }

                        if (!string.IsNullOrEmpty(Japanese))
                        {
                            if (!IsBooleanValue(Japanese))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "Japanese", RecordNum, ErrMsg);
                            }
                        }


                        if (!string.IsNullOrEmpty(Korean))
                        {
                            if (!IsBooleanValue(Korean))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "Korean", RecordNum, ErrMsg);
                            }
                        }


                        if (!string.IsNullOrEmpty(Vietnamese))
                        {
                            if (!IsBooleanValue(Vietnamese))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "Vietnamese", RecordNum, ErrMsg);
                            }
                        }


                        if (!string.IsNullOrEmpty(NativeHawaiian))
                        {
                            if (!IsBooleanValue(NativeHawaiian))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "Vietnamese", RecordNum, ErrMsg);
                            }
                        }


                        if (!string.IsNullOrEmpty(GuamanianOrChamorro))
                        {
                            if (!IsBooleanValue(GuamanianOrChamorro))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "GuamanianOrChamorro", RecordNum, ErrMsg);
                            }
                        }


                        if (!string.IsNullOrEmpty(Samoan))
                        {
                            if (!IsBooleanValue(Samoan))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "Samoan", RecordNum, ErrMsg);
                            }
                        }

                        if (!string.IsNullOrEmpty(OtherAsian))
                        {
                            if (!IsBooleanValue(OtherAsian))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "OtherAsian", RecordNum, ErrMsg);
                            }
                        }


                        if (!string.IsNullOrEmpty(OtherPacificIslander))
                        {
                            if (!IsBooleanValue(OtherPacificIslander))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "OtherAsian", RecordNum, ErrMsg);
                            }
                        }


                        if (!string.IsNullOrEmpty(SomeOtherRaceEthnicity))
                        {
                            if (!IsBooleanValue(SomeOtherRaceEthnicity))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "SomeOtherRaceEthnicity", RecordNum, ErrMsg);
                            }
                        }


                        if (!string.IsNullOrEmpty(Disabled))
                        {
                            if (!IsBooleanValue(Disabled))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "Disabled", RecordNum, ErrMsg);
                            }
                        }

                        if (!string.IsNullOrEmpty(Rural))
                        {
                            if (!IsBooleanValue(Rural))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "Rural", RecordNum, ErrMsg);
                            }
                        }


                        if (!string.IsNullOrEmpty(EmployerRelatedGroups))
                        {
                            if (!IsBooleanValue(EmployerRelatedGroups))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "EmployerRelatedGroups", RecordNum, ErrMsg);
                            }
                        }

                        if (!string.IsNullOrEmpty(MentalHealthProffessionals))
                        {
                            if (!IsBooleanValue(MentalHealthProffessionals))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "MentalHealthProffessionals", RecordNum, ErrMsg);
                            }
                        }

                        if (!string.IsNullOrEmpty(SocalWorkProffessionals))
                        {
                            if (!IsBooleanValue(SocalWorkProffessionals))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "SocalWorkProffessionals", RecordNum, ErrMsg);
                            }
                        }

                        if (!string.IsNullOrEmpty(DualEligibleGroups))
                        {
                            if (!IsBooleanValue(DualEligibleGroups))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "DualEligibleGroups", RecordNum, ErrMsg);
                            }
                        }

                        if (!string.IsNullOrEmpty(PartnerShipOutReach))
                        {
                            if (!IsBooleanValue(PartnerShipOutReach))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "PartnerShipOutReach", RecordNum, ErrMsg);
                            }
                        }


                        if (!string.IsNullOrEmpty(PartnerPresentationToGroups))
                        {
                            if (!IsBooleanValue(PartnerPresentationToGroups))
                            {
                                IsValid = false;
                                GenerateError(this.GetType(), "PartnerPresentationToGroups", RecordNum, ErrMsg);
                            }
                        }

                        return IsValid;
                    }

                    public void Load(string[] Fields)
                    {
                        //string[] Fields = Record.Split(sep);

                        MedicarePreEnrollees = Fields[171];

                        MedicareBeneficiaries = Fields[172];

                        FamilyMembersCaregivers = Fields[173];

                        LowIncome = Fields[174];

                        Hispanic = Fields[175];

                        WhiteNonHispanic = Fields[176];

                        BlackorAfricanAmerican = Fields[177];

                        AmericanIndian = Fields[178];

                        AsianIndian = Fields[179];

                        Chinese = Fields[180];

                        Filipino = Fields[181];

                        Japanese = Fields[182];

                        Korean = Fields[183];

                        Vietnamese = Fields[184];

                        NativeHawaiian = Fields[185];

                        GuamanianOrChamorro = Fields[186];

                        Samoan = Fields[187];

                        OtherAsian = Fields[188];

                        OtherPacificIslander = Fields[189];

                        SomeOtherRaceEthnicity = Fields[190];

                        Disabled = Fields[191];

                        Rural = Fields[192];

                        EmployerRelatedGroups = Fields[193];

                        MentalHealthProffessionals = Fields[194];

                        SocalWorkProffessionals = Fields[195];

                        DualEligibleGroups = Fields[196];

                        PartnerShipOutReach = Fields[197];

                        PartnerPresentationToGroups = Fields[198];

                        Others = Fields[199];
                        RecordNum = Fields[2];
                    }

                }

                public class NationwideCMSSpecialUseFields : absRecordValidate
                {
                    [PamProperty("Nationwide CMS Special Use", "Field1")]
                    string Field1 { get; set; }
                    [PamProperty("Nationwide CMS Special Use", "Field2")]
                    string Field2 { get; set; }
                    [PamProperty("Nationwide CMS Special Use", "Field3")]
                    string Field3 { get; set; }
                    [PamProperty("Nationwide CMS Special Use", "Field4")]
                    string Field4 { get; set; }
                    [PamProperty("Nationwide CMS Special Use", "Field5")]
                    string Field5 { get; set; }
                    [PamProperty("Nationwide CMS Special Use", "Field6")]
                    string Field6 { get; set; }
                    [PamProperty("Nationwide CMS Special Use", "Field7")]
                    string Field7 { get; set; }
                    [PamProperty("Nationwide CMS Special Use", "Field8")]
                    string Field8 { get; set; }
                    [PamProperty("Nationwide CMS Special Use", "Field9")]
                    string Field9 { get; set; }
                    [PamProperty("Nationwide CMS Special Use", "Field10")]
                    string Field10 { get; set; }


                    string EventDateString = string.Empty;
                    DateTime? DateOfEvent = null;
                    string StateFIPS = string.Empty;

                    public void Load(string[] Fields, string dtEvent)
                    {
                        RecordNum = Fields[2];
                        Field1 = Fields[200];
                        Field2 = Fields[201];
                        Field3 = Fields[202];
                        //Field4 = Fields[203];
                        //Field5 = Fields[204];
                        //Field6 = Fields[205];
                        //Field7 = Fields[206];
                        //Field8 = Fields[207];
                        //Field9 = Fields[208];
                        //Field10 = Fields[209];
                        EventDateString = dtEvent;
                        //StateFIPS = Fields[1];
                        StateFIPS = "99";

                    }

                    public override bool AttemptValidate()
                    {
                        bool IsValid = false;

                        if (!IsValidDate(EventDateString))
                        {
                            ErrMsg = "Event start date is invalid could not validate special fields.";
                            GenerateError(RecordNum, ErrMsg);
                            return false;
                        }

                        DateOfEvent = Convert.ToDateTime(EventDateString);
                        State StateValue = new State(StateFIPS);
                        IEnumerable<SpecialField> spFieldsRules = FileUploadDAL.GetSpecialUploadFieldsValues(FormType.PublicMediaActivity, StateValue);

                        if (!ValidateSpecialField(StateFIPS, Field1, DateOfEvent, 1, spFieldsRules, FormType.PublicMediaActivity))
                        {
                            GenerateError(RecordNum, ErrMsg);
                            IsValid = false;
                        }

                        if (!ValidateSpecialField(StateFIPS, Field2, DateOfEvent, 2, spFieldsRules, FormType.PublicMediaActivity))
                        {
                            GenerateError(RecordNum, ErrMsg);
                            IsValid = false;
                        }

                        if (!ValidateSpecialField(StateFIPS, Field3, DateOfEvent, 3, spFieldsRules, FormType.PublicMediaActivity))
                        {
                            GenerateError(RecordNum, ErrMsg);
                            IsValid = false;
                        }

                        //if (!ValidateSpecialField(StateFIPS, Field4, DateOfEvent, 4, spFieldsRules, FormType.PublicMediaActivity))
                        //{
                        //    GenerateError(RecordNum, ErrMsg);
                        //    IsValid = false;
                        //}


                        //if (!ValidateSpecialField(StateFIPS, Field5, DateOfEvent, 5, spFieldsRules, FormType.PublicMediaActivity))
                        //{
                        //    GenerateError(RecordNum, ErrMsg);
                        //    IsValid = false;
                        //}

                        //if (!ValidateSpecialField(StateFIPS, Field6, DateOfEvent, 6, spFieldsRules, FormType.PublicMediaActivity))
                        //{
                        //    GenerateError(RecordNum, ErrMsg);
                        //    IsValid = false;
                        //}

                        //if (!ValidateSpecialField(StateFIPS, Field7, DateOfEvent, 7, spFieldsRules, FormType.PublicMediaActivity))
                        //{
                        //    GenerateError(RecordNum, ErrMsg);
                        //    IsValid = false;
                        //}

                        //if (!ValidateSpecialField(StateFIPS, Field8, DateOfEvent, 8, spFieldsRules, FormType.PublicMediaActivity))
                        //{
                        //    GenerateError(RecordNum, ErrMsg);
                        //    IsValid = false;
                        //}

                        //if (!ValidateSpecialField(StateFIPS, Field9, DateOfEvent, 9, spFieldsRules, FormType.PublicMediaActivity))
                        //{
                        //    GenerateError(RecordNum, ErrMsg);
                        //    IsValid = false;
                        //}

                        //if (!ValidateSpecialField(StateFIPS, Field10, DateOfEvent, 20, spFieldsRules, FormType.PublicMediaActivity))
                        //{
                        //    GenerateError(RecordNum, ErrMsg);
                        //    IsValid = false;
                        //}

                        //If you complete one PAM Duals data field, you must complete both Duals data elements.

                        ArrayList FieldItemsList = new ArrayList();
                        int specialFieldListCMSItemCount = 0;

                        FieldItemsList.Add(Field2);
                        FieldItemsList.Add(Field3);
                       
                        foreach (string ItemValue in FieldItemsList)
                        {
                            if (!string.IsNullOrEmpty(ItemValue))
                            {
                                specialFieldListCMSItemCount += 1;
                            }
                        }

                        if (specialFieldListCMSItemCount != 0 && specialFieldListCMSItemCount != FieldItemsList.Count)
                        {
                            ErrMsg = "If you complete one PAM Duals data field, you must complete both Duals data elements.";
                            GenerateError(RecordNum, ErrMsg);
                            IsValid = false;
                        }     

                        return IsValid;

                    }
                }


                public class StateAndLocalSpecialUseFields : absRecordValidate
                {
                    [PamProperty("State And Local Special Use", "Field1")]
                    string Field1 { get; set; }
                    [PamProperty("State And Local Special Use", "Field2")]
                    string Field2 { get; set; }
                    [PamProperty("State And Local Special Use", "Field3")]
                    string Field3 { get; set; }
                    [PamProperty("State And Local Special Use", "Field4")]
                    string Field4 { get; set; }
                    [PamProperty("State And Local Special Use", "Field5")]
                    string Field5 { get; set; }
                    [PamProperty("State And Local Special Use", "Field6")]
                    string Field6 { get; set; }
                    [PamProperty("State And Local Special Use", "Field7")]
                    string Field7 { get; set; }
                    [PamProperty("State And Local Special Use", "Field8")]
                    string Field8 { get; set; }
                    [PamProperty("State And Local Special Use", "Field9")]
                    string Field9 { get; set; }
                    [PamProperty("State And Local Special Use", "Field10")]
                    string Field10 { get; set; }

                    string EventDateString = string.Empty;
                    DateTime? DateOfEvent = null;
                    string StateFIPS = string.Empty;

                    public void Load(string[] Fields, string dtEvent)
                    {
                        RecordNum = Fields[2];
                        Field1 = Fields[210];
                        Field2 = Fields[211];
                        Field3 = Fields[212];
                        Field4 = Fields[213];
                        Field5 = Fields[214];
                        Field6 = Fields[215];
                        Field7 = Fields[216];
                        Field8 = Fields[217];
                        Field9 = Fields[218];
                        Field10 = Fields[219];
                        EventDateString = dtEvent;
                        StateFIPS = Fields[1];

                    }
                    public override bool AttemptValidate()
                    {
                        bool IsValid = false;

                        if (!IsValidDate(EventDateString))
                        {
                            ErrMsg = "Event start date is invalid could not validate special fields.";
                            GenerateError(RecordNum, ErrMsg);
                            return false;
                        }

                        DateOfEvent = Convert.ToDateTime(EventDateString);
                        State StateValue = new State(StateFIPS);
                        IEnumerable<SpecialField> spFieldsRules = FileUploadDAL.GetSpecialUploadFieldsValues(FormType.PublicMediaActivity, StateValue);

                        if (!ValidateSpecialField(StateFIPS, Field1, DateOfEvent, 11, spFieldsRules, FormType.PublicMediaActivity))
                        {
                            GenerateError(RecordNum, ErrMsg);
                            IsValid = false;
                        }

                        if (!ValidateSpecialField(StateFIPS, Field2, DateOfEvent, 12, spFieldsRules, FormType.PublicMediaActivity))
                        {
                            GenerateError(RecordNum, ErrMsg);
                            IsValid = false;
                        }

                        if (!ValidateSpecialField(StateFIPS, Field3, DateOfEvent, 13, spFieldsRules, FormType.PublicMediaActivity))
                        {
                            GenerateError(RecordNum, ErrMsg);
                            IsValid = false;
                        }

                        if (!ValidateSpecialField(StateFIPS, Field4, DateOfEvent, 14, spFieldsRules, FormType.PublicMediaActivity))
                        {
                            GenerateError(RecordNum, ErrMsg);
                            IsValid = false;
                        }


                        if (!ValidateSpecialField(StateFIPS, Field5, DateOfEvent, 15, spFieldsRules, FormType.PublicMediaActivity))
                        {
                            GenerateError(RecordNum, ErrMsg);
                            IsValid = false;
                        }

                        if (!ValidateSpecialField(StateFIPS, Field6, DateOfEvent, 16, spFieldsRules, FormType.PublicMediaActivity))
                        {
                            GenerateError(RecordNum, ErrMsg);
                            IsValid = false;
                        }

                        if (!ValidateSpecialField(StateFIPS, Field7, DateOfEvent, 17, spFieldsRules, FormType.PublicMediaActivity))
                        {
                            GenerateError(RecordNum, ErrMsg);
                            IsValid = false;
                        }

                        if (!ValidateSpecialField(StateFIPS, Field8, DateOfEvent, 18, spFieldsRules, FormType.PublicMediaActivity))
                        {
                            GenerateError(RecordNum, ErrMsg);
                            IsValid = false;
                        }

                        if (!ValidateSpecialField(StateFIPS, Field9, DateOfEvent, 19, spFieldsRules, FormType.PublicMediaActivity))
                        {
                            GenerateError(RecordNum, ErrMsg);
                            IsValid = false;
                        }

                        if (!ValidateSpecialField(StateFIPS, Field10, DateOfEvent, 20, spFieldsRules, FormType.PublicMediaActivity))
                        {
                            GenerateError(RecordNum, ErrMsg);
                            IsValid = false;
                        }

                        return IsValid;

                    }
                }

                #endregion


            }

        } //****************************************************
        

 
        
    
