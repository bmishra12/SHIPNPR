using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Linq;

namespace ShiptalkWebControls
{
    [ToolboxData("<{0}:SpecialFieldList runat=server></{0}:SpecialFieldList>")]
    public class SpecialFieldList : CompositeDataBoundControl
    {
        private const string KeysKey = "Keys";
        private const string DataKeyAttributeKey = "dataKey";

        private IDictionary _items;

        #region Constructors

        public SpecialFieldList()
        {
            DataBound += SpecialFieldList_DataBound;
        }

        #endregion

        #region Properties

        protected HybridDictionary Keys
        {
            get
            {
                if (ViewState[KeysKey] == null)
                    ViewState[KeysKey] = new HybridDictionary();

                return (HybridDictionary) ViewState[KeysKey];
            }
        }

        public string DataKeyName { get; set; }

        public string DataTextField { get; set; }

        public string DataTextFormat { get; set; }

        public string DataValueField { get; set; }

        public string DataValidationTypeField { get; set; }

        public string DataIsRequiredField { get; set; }

        public string DataRangeField { get; set; }

        public IDictionary Items
        {
            get
            {
                var items = new HybridDictionary();

                if (Controls.Count == 0)
                    return null;

                foreach (TableRow row in ((Table) Controls[0]).Rows)
                {
                    var textBoxValue = (TextBox) row.Cells[1].Controls[0];
                   
                    items.Add(textBoxValue.Attributes[DataKeyAttributeKey], textBoxValue.Text);
                }

                return items;              
            }
            set
            {
                _items = value;

                if (value == null || Controls.Count == 0) return;

                foreach (TableRow row in ((Table) Controls[0]).Rows)
                {
                    var textBox = row.Cells[1].Controls[0] as TextBox;

                    if (textBox == null) continue;

                    foreach (DictionaryEntry item in value)
                    {
                        if (textBox.ID != item.Key.ToString()) continue;

                        textBox.Text = item.Value.ToString();
                        break;
                    }
                }
            }
        }

        public int ValueMaxLength { get; set; }

        #endregion

        #region Event Handlers

        private void SpecialFieldList_DataBound(object sender, EventArgs e)
        {
            ((SpecialFieldList) sender).Items = _items;
        }

        #endregion

        #region Overrides of WebControl

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }

        #endregion

        #region Overrides of CompositeDataBoundControl

        protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
        {
            ValidateProperties();

            var rowCount = 0;

            if (dataSource != null)
            {
                var table = new Table();              
                table.CssClass = "SpcialFieldTable";
                Controls.Add(table);

                foreach (var dataItem in dataSource)
                {
                    var row = new TableRow();
                    var cellTextCol = new TableCell();
                    var cellValueCol = new TableCell();
                    var literalSpanRequiredIndicator = new Literal();
                    var literalBr = new Literal();
                    var labelText = new Label();
                    var textBoxValue = new TextBox();
                    var requiredValidator = new RequiredFieldValidator();
                    string toolTipText = string.Empty;                  
                    var rangeValidator = new RangeValidator();
                    var regularExpressionValidator = new RegularExpressionValidator();
            
                    table.Rows.Add(row);
                    row.Cells.Add(cellTextCol);
                    row.Cells.Add(cellValueCol);
                    cellTextCol.Controls.Add(literalSpanRequiredIndicator);
                    cellTextCol.Controls.Add(labelText);
                    cellValueCol.Controls.Add(textBoxValue);
                    cellValueCol.Controls.Add(literalBr);
                    cellValueCol.Controls.Add(requiredValidator);
                    cellValueCol.Controls.Add(rangeValidator);
                    cellValueCol.Controls.Add(regularExpressionValidator);
                    
                    cellTextCol.VerticalAlign = VerticalAlign.Top;

                    if (dataBinding && dataItem != null)
                    {
                        var dataKeyNameValue =
                            dataItem.GetType().GetProperty(DataKeyName).GetValue(dataItem, null).ToString();

                        var textValue = string.Empty;
                        var valueValue = string.Empty;
                        var isRequiredValue = false;
                        var validationTypeValue = ValidationType.None;                        
                        var rangeValue = string.Empty;                    

                        if (!string.IsNullOrEmpty(DataTextField))
                        {
                            var textProperty = GetDataItemProperty(dataItem, DataTextField);
                            var textInstance = textProperty.GetValue(dataItem, null);
                            textValue = (textInstance != null) ? textInstance.ToString() : string.Empty;
                            validationTypeValue = (ValidationType)dataItem.GetType().GetProperty("ValidationType").GetValue(dataItem, null);
                            toolTipText = dataItem.GetType().GetProperty("Description").GetValue(dataItem, null).ToString();                          
                        }

                        if (!string.IsNullOrEmpty(DataValueField))
                        {
                            var valueProperty = GetDataItemProperty(dataItem, DataValueField);
                            var valueInstance = valueProperty.GetValue(dataItem, null);
                            valueValue = (valueInstance != null) ? valueInstance.ToString() : string.Empty;
                        }

                        if (!string.IsNullOrEmpty(DataIsRequiredField))
                        {
                            try
                            {
                                var isRequiredProperty = GetDataItemProperty(dataItem, DataIsRequiredField);
                                var isRequiredInstance = isRequiredProperty.GetValue(dataItem, null);
                                isRequiredValue = (isRequiredInstance != null)
                                                      ? Convert.ToBoolean(isRequiredInstance)
                                                      : false;
                            }
                            catch
                            {
                                throw new InvalidCastException(
                                    "The DataIsRequiredField property value must support explicit conversion to a Boolean value.");

                            }
                        }

                        //Range validation
                        if (!string.IsNullOrEmpty(DataRangeField))
                        {
                            try
                            {
                                var rangeProperty = GetDataItemProperty(dataItem, DataRangeField);
                                var rangeInstance = rangeProperty.GetValue(dataItem, null);

                                rangeValue = (rangeInstance != null)
                                                 ? Convert.ToString(rangeInstance)
                                                 : "";
                            }
                            catch
                            {
                                throw new InvalidCastException(
                                    "The DataRangeField property value must support explicit conversion to a string value.");

                            }
                        }
                        // end

                        labelText.Text = (!string.IsNullOrEmpty(DataTextFormat))
                                             ? string.Format(DataTextFormat, textValue)
                                             : textValue;
                        textBoxValue.Text = (DataValueField == null) ? string.Empty : valueValue;
                        textBoxValue.Attributes.Add(DataKeyAttributeKey, dataKeyNameValue);
                        textBoxValue.ID = dataKeyNameValue;
                        labelText.AssociatedControlID = textBoxValue.ID;

                        if (validationTypeValue == ValidationType.Numeric)
                            textBoxValue.CssClass = "onlynum";

                        else if (validationTypeValue == ValidationType.AlphaNumeric)
                            textBoxValue.CssClass = "alphanum";                  

                        if (ValueMaxLength > 0)
                            textBoxValue.MaxLength = ValueMaxLength;
                        textBoxValue.ToolTip = toolTipText;
                        labelText.ToolTip = toolTipText;

                       // literalBr.Text = "<br />";
                        requiredValidator.ControlToValidate = textBoxValue.ID;
                        requiredValidator.ErrorMessage = string.Format("{0} is required.", textValue);
                        requiredValidator.CssClass = "required";
                        literalSpanRequiredIndicator.Text = "<span class=\"required\">*</span>&nbsp;";
                        requiredValidator.Display = ValidatorDisplay.Dynamic;

                        if (!isRequiredValue)
                        {
                            literalSpanRequiredIndicator.Visible = false;
                            requiredValidator.Enabled = false;
                        }

                        //This validation has been implemented on 06/05/2013 : Lavanya Maram
                        //Range validation for Integers
                       
                        literalBr.Text = "<br />";
                        rangeValidator.ControlToValidate = textBoxValue.ID;
                       
                        if (rangeValue == "" || validationTypeValue == ValidationType.Option)
                        {
                            rangeValidator.Enabled = false;
                        }
                        else
                        {
                            string MinimumValue = string.Empty;
                            string MaximumValue = string.Empty;
                            string Seperator = string.Empty;

                            if (validationTypeValue == ValidationType.Range)
                                Seperator = "-";
                           
                            MinimumValue = GetRangeStartValue(rangeValue.Trim(), Seperator);
                            MaximumValue = GetRangeEndValue(rangeValue.Trim(), Seperator);
                                                      
                            rangeValidator.Type = ValidationDataType.Integer;
                            rangeValidator.MinimumValue = MinimumValue;
                            rangeValidator.MaximumValue = MaximumValue;
                            rangeValidator.ErrorMessage = "Range must be from " + MinimumValue + " to " + MaximumValue + ".";
                            rangeValidator.CssClass = "required";
                            rangeValidator.Display = ValidatorDisplay.Dynamic;
                           
                        }

                        //This validation has been implemented on 06/05/2013 : Lavanya Maram
                        //Range validation for "Option(Y/N)"
                        regularExpressionValidator.ControlToValidate = textBoxValue.ID;

                        if (rangeValue == "" || validationTypeValue == ValidationType.Range)
                        {
                            regularExpressionValidator.Enabled = false;
                        }
                        else
                        {
                            string Option1 = string.Empty;
                            string Option2 = string.Empty;
                            string Seperator = string.Empty;
                            string regExpression = string.Empty;

                            if (validationTypeValue == ValidationType.Option)
                                Seperator = ",";

                            Option1 = GetRangeStartValue(rangeValue.Trim(), Seperator);
                            Option2 = GetRangeEndValue(rangeValue.Trim(), Seperator);

                            regExpression = "^([" + Option1 + Option1.ToLower() + Option2 + Option2.ToLower() + "])$";

                            regularExpressionValidator.CssClass = "required";
                            regularExpressionValidator.ValidationExpression = regExpression;
                            regularExpressionValidator.ErrorMessage = "Please enter 'Y'/'N'.";
                            regularExpressionValidator.Display = ValidatorDisplay.Dynamic;
                         }
                          
                        //end

                        Keys.Add(rowCount, textBoxValue.ID);
                    }
                    else
                    {
                        textBoxValue.ID = Keys[rowCount].ToString();
                        labelText.AssociatedControlID = textBoxValue.ID;
                    }

                    rowCount++;
                }
            }

            return rowCount;
        }

        private void ValidateProperties()
        {
            if (string.IsNullOrEmpty(DataKeyName))
                throw new NullReferenceException(
                    "A DataKeyName is required for the SpecialFieldList to function correctly.");
        }

        private PropertyInfo GetDataItemProperty(object dataItem, string propertyName)
        {
            var propInfo = dataItem.GetType().GetProperty(propertyName);

            if (propInfo == null)
                throw new NullReferenceException(string.Format("Unable to find property \"{0}\"", propertyName));

            return propInfo;
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

        public enum ValidationType
        {
            None = 0,
            AlphaNumeric = 1,
            Numeric = 2,
            Range = 3,
            Option = 4
        }

        #endregion
    } 
}
