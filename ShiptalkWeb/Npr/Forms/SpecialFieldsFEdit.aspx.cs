using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkWeb;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;
using System.Linq;
using System.Drawing;

namespace ShiptalkWeb
{
    public partial class SpecialFieldsFEdit : Page, IRouteDataPage
    {
        private const string FIELD_ID = "SpecialFieldID";
        private string StateFIPS = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            pnlInvalidEndDate.Visible = false;
            pnlInvalidStartDate.Visible = false;
            if (!IsPostBack)
                OnInitialize();
                
        }


        private void OnInitialize()
        {
            ViewSpecialFieldsViewData FieldData = SpecialFieldsBLL.GetSpecialFieldsView(int.Parse(SpecialFieldID));
            TxtDescription.Text = FieldData.Description;
            TxtName.Text = FieldData.Name;

            //check if the startdate falls after today's date
            //then disbale the startdate

            DateTime startdate; 
            if (System.DateTime.TryParse(FieldData.StartDate, out startdate)== false) 
                startdate = System.DateTime.MinValue;

            if (startdate <= System.DateTime.Today)
            {
                StartTextDate.Enabled = false;
                StartTextDate.CssClass = "";
                ddlValidationType.Enabled = false;
                rdIsRequired.Enabled = false;
            }

            if (FieldData.Ordinal < 11)
            {
                ddlFieldType.SelectedItem.Selected = false;
                ddlFieldType.Items.FindByText("NATIONWIDE").Selected = true;
                ddlFieldType.Enabled = false;
                ddlFieldType_SelectedIndexChanged(this, null);
            }
            else
            {
                ddlFieldType.SelectedItem.Selected = false;
                ddlFieldType.Items.FindByText("STATE").Selected = true;
                ddlFieldType.Enabled = false;
                ddlFieldType_SelectedIndexChanged(this, null);
            }

            ddlFieldType.Enabled = false;
           
            DateTime StartDate = Convert.ToDateTime(FieldData.StartDate);
            StartTextDate.Text = StartDate.ToString("MM/dd/yyyy");

            DateTime EndDate = Convert.ToDateTime(FieldData.EndDate);
            EndTextDate.Text = EndDate.ToString("MM/dd/yyyy");

            ListItem SelectedValidationRule =  ddlValidationType.Items.FindByText(FieldData.ValidationType.ToString());
            SelectedValidationRule.Selected = true;

            txtValidationRange.Text = FieldData.Range;

            if (FieldData.FormType.Contains("Client Contact"))
            {
                lblFormType.Text = "ClientContact";
            }
            else
            {
                lblFormType.Text = "Public Media Activity";
            }

            
            lblState.Text = FieldData.StateName;
            Session.Add("SPECIAL_FIELDS_EDIT_STATEFIPS", FieldData.State.Code);
            if(FieldData.IsRequired.ToUpper() == "TRUE")
                rdIsRequired.Items.FindByText("Yes").Selected = true;
            else
                rdIsRequired.Items.FindByText("No").Selected = true;




        }

        public bool ValidateForm()
        {
            if (string.IsNullOrEmpty(EndTextDate.Text.ToString())
                || string.IsNullOrEmpty(TxtName.Text)
                || ddlValidationType.SelectedIndex == -1 )
            {
                lblFeedBack.Text = "Please enter all data for special fields.";
                lblFeedBack.Visible = true;
                return false;
            }

            if ((ddlValidationType.SelectedValue == "Range" || ddlValidationType.SelectedValue == "Option") && (string.IsNullOrEmpty(txtValidationRange.Text)))
            {
                lblFeedBack.Text = "When validation type is Range or Option, Range or Option data must be entered in validation range field.";
                lblFeedBack.Visible = true;
                return false;}
                else{
                    if ((ddlValidationType.SelectedValue == "None" || ddlValidationType.SelectedValue == "Numeric" || ddlValidationType.SelectedValue == "AlphaNumeric") && (!string.IsNullOrEmpty(txtValidationRange.Text)))
                {
                lblFeedBack.Text = "Range data is not needed for None, Numeric, and Alphanumeric validation type; empty the validation range field.";
                lblFeedBack.Visible = true;
                return false;}
            }

            if (ddlFieldType.SelectedIndex == 0)
            {
                lblFeedBack.Text = "Please select field type.";
                lblFeedBack.Visible = true;
                return false;
            }

           
            try
            {
                DateTime StartDt = Convert.ToDateTime(StartTextDate.Text);
                DateTime EndDt = Convert.ToDateTime(EndTextDate.Text);
                //if (StartDt < DateTime.Now.AddDays(-1))
                //{
                //    pnlInvalidStartDate.Visible = true;
                //    return false;
                //}

                if (EndDt < DateTime.Now.AddDays(1))
                {
                    pnlInvalidEndDate.Visible = true;
                    
                    return false;
                }

                if (EndDt <= StartDt)
                {
                    lblFeedBack.Text = "End date cannot come before start date.";
                    lblFeedBack.Visible = true;
                    return false;
                }

                //Added by Lavanya Maram: 06/27/2013
                //Validate "Validation Range"              

                string ValidationExpression = string.Empty;

                if (ddlValidationType.SelectedItem.Text == ShiptalkLogic.BusinessObjects.ValidationType.Range.ToString())
                {
                    ValidationExpression = @"^([0-9]{1,4}-[0-9]{1,4})$";

                    if (!Regex.IsMatch(txtValidationRange.Text, ValidationExpression))
                    {
                        lblFeedBack.Text = "Validation range should be in numbers. Ex: 1-9 or 11-20 or 100-200, 1000-2000.";
                        lblFeedBack.Visible = true;
                        return false;
                    }
                }

                else if (ddlValidationType.SelectedItem.Text == ShiptalkLogic.BusinessObjects.ValidationType.Option.ToString())
                {
                    ValidationExpression = "^(([A-Z]|[a-z]{1}),([A-Z]|[a-z]{1}))$";

                    if (!Regex.IsMatch(txtValidationRange.Text, ValidationExpression))
                    {
                        lblFeedBack.Text = "Validation range for 'Option' should be in 'Y,N' format.";
                        lblFeedBack.Visible = true;
                        return false;
                    }
                }         
            }
            catch (System.Exception exFormat)
            {
                lblFeedBack.Text = "Invalid date entered.";
                lblFeedBack.Visible = true;
                return false;
            }
            return true;
        }



      
       


        #region Properties
        private string SpecialFieldID
        {
            get
            {
                if (RouteData.Values[FIELD_ID] == null) return null;
                return RouteData.Values[FIELD_ID].ToString();
            }

        }

        


        #endregion

        #region Implementation of IRouteDataPage


        public RouteData RouteData { get; set; }
        public UserAccount AccountInfo { get; set; }


        #endregion


        

        #region Implementation of IAuthorize
        public bool IsAuthorized()
        {
            //Only CMS and State Admins are able to add a new sub-state region.
            return ((AccountInfo.Scope == Scope.CMS || AccountInfo.Scope == Scope.State) && AccountInfo.IsAdmin);

        }
        #endregion


        protected void ddlFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }


           
            ViewSpecialFieldsViewData FieldValue = new ViewSpecialFieldsViewData();
            FieldValue.Description = TxtDescription.Text;
            FieldValue.EndDate = EndTextDate.Text;
            FieldValue.StartDate = StartTextDate.Text;
            if (lblFormType.Text == FormType.ClientContact.ToString())
            {
                FieldValue.FormType = FormType.ClientContact.ToString();
            }
            else
            {
                FieldValue.FormType = FormType.PublicMediaActivity.ToString();
            }

            FieldValue.IsRequired = rdIsRequired.SelectedItem.Text;
            FieldValue.Name = TxtName.Text;
            FieldValue.Id = int.Parse(SpecialFieldID);

            string StateID = string.Empty;
            ViewSpecialFieldsViewData FieldData = SpecialFieldsBLL.GetSpecialFieldsView(int.Parse(SpecialFieldID));
            StateID = FieldData.State.Code;
            
            FieldValue.State = new State(StateID);
            FieldValue.ValidationName = ddlValidationType.SelectedItem.Text;
            //Added by Lavanya
            FieldValue.Range = txtValidationRange.Text;
            //end
            FieldValue.CreatedBy = AccountInfo.UserId;
            FieldValue.Ordinal = -1;
            try
            {
               SpecialFieldsBLL.AddUpdateSpecialField(FieldValue);
               RouteController.RouteTo(RouteController.SpeciaFieldsView(int.Parse(SpecialFieldID)));
               
            }
            catch (ApplicationException exApp)
            {
                lblFeedBack.Text = exApp.Message;
                lblFeedBack.Visible = true;

                if (exApp.Message.Contains("The special field was created and assign available ordinal value"))
                {
                    btnSave.Visible = false;
                }
                lblFeedBack.Visible = true;
            }

        }

    }
}
