using System.Globalization;
using System;
namespace ShiptalkWeb.Upload
{
    public class Validator
    {
        public enum ValidationType
        {
            Required,
            Numeric,
            RequiredNumeric,
            LogicalOperator,
            ComparisonOperator,
            Range,
            Date,
            RequiredDate,
            NotEmpty,
            NumericRange,
            BooleanNumericRange,
            OptionalNumeric,
            OptionalNumericRange,
            OptionalDate,
            BooleanNumericRelaxed,
            OptionalNotEmpty,
            DateRange,
            RequiredNumericRange,
            RequiredTime
        }

        private string msgVal;
        private bool isValidRecVal = true;
        private bool skipRecordVal = false;
        private bool exOut = false;

        public bool ExceptionOutput
        {
            get { return exOut; }
            set { exOut = value; }
        }

        //-- Property accessor methods 
        public string Msg
        {
            get { return msgVal; }
            set { msgVal = value; }
        }

        public bool IsValidRec
        {
            get { return isValidRecVal; }
            set { isValidRecVal = value; }
        }

        public bool SkipRecord
        {
            get { return skipRecordVal; }
            set { skipRecordVal = value; }
        }

        public Validator()
        {
            msgVal = "Detail Error Message";
        }

        public void Clear()
        {
            msgVal = "";
            //-- Flush the flag of previous record IsRecValid 
            IsValidRec = true;
        }

        private void Except(string ExMessage)
        {
            if ((exOut))
            {
                
                throw new Exception(ExMessage);
            }
        }


        public bool Validate(string data, int type, string name, int value, string operator1, bool skipRecIfNotValid)
        {
            bool functionReturnValue = false;
            data = data.Trim();
            int IsNumeric = -1;
            switch ((type))
            {
                case (int)ValidationType.Required:
                    functionReturnValue = (!string.IsNullOrEmpty(data) ? true : false);
                    if ((!functionReturnValue))
                    {
                        Except(name + " (" + data + ")" + " is a required field.");
                        msgVal = msgVal + ":" + name + " Required " + "(" + data + ")";
                        if ((skipRecIfNotValid)) isValidRecVal = false;
                    }


                    break;
                case (int)ValidationType.Numeric:
                  
                    functionReturnValue = int.TryParse(data, out IsNumeric);
                    if ((!functionReturnValue))
                    {
                        Except(name + " (" + data + ")" + " must be a Numeric Value.");
                        msgVal = msgVal + ":" + name + " Not Numeric " + "(" + data + ")";
                        if ((skipRecIfNotValid)) isValidRecVal = false;
                    }


                    break;
                case (int)ValidationType.Date:
                    
                    functionReturnValue = int.TryParse(data,out IsNumeric);
                    if ((!functionReturnValue))
                    {
                        msgVal = msgVal + ":" + name + " Date Not Valid " + "(" + data + ")";
                        Except(name + " (" + data + ")" + " does not contain a valid date.");
                        if ((skipRecIfNotValid)) isValidRecVal = false;
                    }


                    break;
                case (int)ValidationType.NotEmpty:
                    functionReturnValue = (!string.IsNullOrEmpty(data) ? true : false);
                    if ((!functionReturnValue))
                    {
                        Except(name + " (" + data + ")" + " (" + data + ")" + " cannot be empty.");
                        msgVal = msgVal + ":" + name + " Empty " + "(" + data + ")";
                        if ((skipRecIfNotValid)) isValidRecVal = false;
                    }


                    break;
                case (int)ValidationType.OptionalNotEmpty:
                    functionReturnValue = (!string.IsNullOrEmpty(data) ? true : false);
                    if ((!functionReturnValue))
                    {
                        //msgVal = msgVal & ":" & name & " Empty " & "(" & data & ")" 
                        if ((skipRecIfNotValid)) isValidRecVal = false;
                    }


                    break;
                case (int)ValidationType.Range:
                    int min = 0;
                    int max = 0;
                    int d = 0;
                    string[] Range = null;

                    d = int.Parse(data);
                    char[] sep = { '-' };
                    Range = operator1.Split(sep);
                    min = int.Parse(Range[0]);
                    max = int.Parse(Range[1]);

                    //-- Validate range 
                    functionReturnValue = ((d >= min & d <= max) ? true : false);
                    if ((!functionReturnValue))
                    {
                        Except(name + " (" + data + ")" + " is not in Range. Min: " + min + " Max: " + max);
                        msgVal = msgVal + ":" + name + " Not in Range(" + operator1 + ") " + "(" + data + ")";
                        if ((skipRecIfNotValid)) isValidRecVal = false;
                    }


                    break;
                case (int)ValidationType.OptionalNumeric:
                    if ((Validate(data, (int)ValidationType.OptionalNotEmpty, name, 0, "", skipRecIfNotValid)))
                    {
                        if ((Validate(data, (int)ValidationType.Numeric, name, 0, "", skipRecIfNotValid)))
                        {
                            functionReturnValue = true;
                        }
                        else
                        {
                            functionReturnValue = false;
                            if ((skipRecIfNotValid)) isValidRecVal = false;
                        }
                    }


                    break;
                case (int)ValidationType.RequiredNumeric:
                    if ((Validate(data, (int)ValidationType.Required, name, 0, "", skipRecIfNotValid)))
                    {
                        if ((Validate(data, (int)ValidationType.Numeric, name, 0, "", skipRecIfNotValid)))
                        {
                            functionReturnValue = true;
                        }
                        else
                        {
                            functionReturnValue = false;
                            if ((skipRecIfNotValid)) isValidRecVal = false;
                        }
                    }


                    break;
                case (int)ValidationType.NumericRange:
                    if ((Validate(data, (int)ValidationType.NotEmpty, name, 0, "", skipRecIfNotValid)))
                    {
                        if ((Validate(data, (int)ValidationType.Numeric, name, 0, "", skipRecIfNotValid)))
                        {
                            if ((Validate(data, (int)ValidationType.Range, name, 0, operator1, skipRecIfNotValid)))
                            {
                                functionReturnValue = true;
                            }
                            else
                            {
                                functionReturnValue = false;
                                if ((skipRecIfNotValid)) isValidRecVal = false;
                            }
                        }
                    }

                    break;
                case (int)ValidationType.RequiredNumericRange:
                    if ((Validate(data, (int)ValidationType.Required, name, 0, "", skipRecIfNotValid)))
                    {
                        if ((Validate(data, (int)ValidationType.Numeric, name, 0, "", skipRecIfNotValid)))
                        {
                            if ((Validate(data, (int)ValidationType.Range, name, 0, operator1, skipRecIfNotValid)))
                            {
                                functionReturnValue = true;
                            }
                            else
                            {
                                functionReturnValue = false;
                                if ((skipRecIfNotValid)) isValidRecVal = false;
                            }
                        }
                    }

                    break;
                case(int) ValidationType.OptionalNumericRange:
                    if ((Validate(data, (int)ValidationType.OptionalNotEmpty, name, 0, "", skipRecIfNotValid)))
                    {
                        if ((Validate(data, (int)ValidationType.Numeric, name, 0, "", skipRecIfNotValid)))
                        {
                            if ((Validate(data, (int)ValidationType.Range, name, 0, operator1, skipRecIfNotValid)))
                            {
                                functionReturnValue = true;
                            }
                            else
                            {
                                functionReturnValue = false;
                                if ((skipRecIfNotValid)) isValidRecVal = false;
                            }
                        }
                    }


                    break;
                case (int)ValidationType.DateRange:
                    DateTime dtStartDate = Convert.ToDateTime(data);
                    functionReturnValue = (dtStartDate.Year > 1753 && dtStartDate.Year < 9999);
                    if ((!functionReturnValue))
                    {
                        Except(name + " (" + data + ")" + " is not in year range of 1753 and 9999 ");
                        msgVal = msgVal + ":" + name + " not in year range of 1753 and 9999 " + "(" + data + ")";
                        if ((skipRecIfNotValid)) isValidRecVal = false;
                    }


                    break;
                case (int)ValidationType.RequiredDate:
                    if (((Validate(data, (int)ValidationType.Required, name, 0, "", skipRecIfNotValid))))
                    {
                        if ((Validate(data, (int)ValidationType.DateRange, name, 0, "", skipRecIfNotValid)))
                        {
                            if ((Validate(data, (int)ValidationType.Date, name, 0, "", skipRecIfNotValid)))
                            {
                                functionReturnValue = true;
                            }
                            else
                            {
                                functionReturnValue = false;
                                if ((skipRecIfNotValid)) isValidRecVal = false;
                            }
                        }
                    }


                    break;
                //-- Compound validation type (Numeric & Range & Boolean (1-true or 2-false) 
                case (int) ValidationType.BooleanNumericRange:
                    if ((Validate(data, (int)ValidationType.NotEmpty, name, 0, "", skipRecIfNotValid)))
                    {
                        if ((Validate(data, (int)ValidationType.Numeric, name, 0, "", skipRecIfNotValid)))
                        {
                            if ((Validate(data, (int)ValidationType.Range, name, 0, operator1, skipRecIfNotValid)))
                            {
                                if ((data == "1"))
                                {
                                    functionReturnValue = true;
                                }
                            }
                            else
                            {
                                functionReturnValue = false;
                                if ((skipRecIfNotValid)) isValidRecVal = false;
                            }
                        }
                    }

                    break;
                case (int)ValidationType.BooleanNumericRelaxed:
                    if ((Validate(data, (int)ValidationType.NotEmpty, name, 0, "", skipRecIfNotValid)))
                    {
                        if ((Validate(data, (int)ValidationType.Numeric, name, 0, "", skipRecIfNotValid)))
                        {
                            if ((data == "1"))
                            {
                                functionReturnValue = true;
                            }
                            else
                            {
                                functionReturnValue = false;
                            }
                        }
                        else
                        {
                            functionReturnValue = false;
                            if ((skipRecIfNotValid)) isValidRecVal = false;
                        }
                    }


                    break;
                case (int)ValidationType.OptionalDate:
                    if ((Validate(data, (int)ValidationType.OptionalNotEmpty, name, 0, "", skipRecIfNotValid)))
                    {
                        if ((Validate(data, (int)ValidationType.DateRange, name, 0, "", skipRecIfNotValid)))
                        {
                            if ((Validate(data, (int)ValidationType.Date, name, 0, "", skipRecIfNotValid)))
                            {
                                functionReturnValue = true;
                            }
                            else
                            {
                                functionReturnValue = false;
                                if ((skipRecIfNotValid)) isValidRecVal = false;
                            }
                        }
                    }


                    break;
                case (int)ValidationType.ComparisonOperator:
                    switch ((operator1))
                    {
                        case "<":
                            functionReturnValue = ((int.Parse(data) < value) ? true : false);
                            break;
                    }
                    if ((!functionReturnValue))
                    {
                        Except(name + " (" + data + ")" + " failed Comparison of " + data + " " + operator1 + " " + value);
                        msgVal = msgVal + ":" + name + " Failed Comparison " + "(" + data + ")";
                        if ((skipRecIfNotValid)) isValidRecVal = false;
                    }

                    break;
                case (int)ValidationType.RequiredTime:
                    DateTime dt = default(DateTime);
                    DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                    dtfi.ShortTimePattern = "hh:mm";
                    dt = DateTime.Parse(data, dtfi);
                    // The time string is valid 
                    functionReturnValue = true;
                    break;
                default:
                    functionReturnValue = false;
                    break;
            }
            return functionReturnValue;
        }




     }
}