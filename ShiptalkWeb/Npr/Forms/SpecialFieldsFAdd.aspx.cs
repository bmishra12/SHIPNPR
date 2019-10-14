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

namespace ShiptalkWeb.Npr.Forms
{
    public partial class SpecialFieldsFAdd : Page, IRouteDataPage
    {
        private const string StateCode = "StateCode";
        private const string FormDataType = "FormType";
       
        
        protected void Page_Load(object sender, EventArgs e)
        {
            lblFeedBack.Text = string.Empty;
            lblFeedBack.Visible = false;

            pnlInvalidEndDate.Visible = false;
            pnlInvalidStartDate.Visible = false;
            
            if (!IsPostBack)
                OnInitialize();

        }
        #region Methods

        private void OnInitialize()
        {
            if ((int)FormType.ClientContact == int.Parse(DataFormType))
            {
                lblFormType.Text = "Client Contact";
            }

            if ((int)FormType.PublicMediaActivity == int.Parse(DataFormType))
            {
                lblFormType.Text = "Public Media Activity";
            }

            lblState.Text = State.GetStateName(StateID);
            if (lblState.Text == "CMS")
            {
                ddlFieldType.SelectedItem.Selected = false;
                ddlFieldType.Items.FindByText("NATIONWIDE").Selected = true;
                ddlFieldType.Enabled = false;
            }
            else
            {
                ddlFieldType.SelectedItem.Selected = false;
                ddlFieldType.Items.FindByText("STATE").Selected = true;
                ddlFieldType.Enabled = false;
            }
            
            
            

        }

        
        private void MonthChanged()
        {
            /*calStartDate.Style.Remove("display");
            calStartDate.Style.Add("display", "block");
            

            calEndDate.Style.Remove("display");
            calEndDate.Style.Add("display", "block");*/
            
        }
        #endregion

        #region Properties
        private string StateID
        {
            get
            {
                if (RouteData.Values[StateCode] == null) return null;
                return RouteData.Values[StateCode].ToString();
            }

        }

        private string DataFormType
        {
            get
            {
                if (RouteData.Values[FormDataType] == null) return null;
                return RouteData.Values[FormDataType].ToString();
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

       
        protected void dataSourceViewSpecialField_Inserting(object sender, ObjectContainerDataSourceInsertingEventArgs e)
        {
            
        }

        
        protected void calStartDate_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            MonthChanged();
            
        }

       
        protected void calEndDate_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            MonthChanged();
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
                return false;
            }
            else
            {
                if ((ddlValidationType.SelectedValue == "None" || ddlValidationType.SelectedValue == "Numeric" || ddlValidationType.SelectedValue == "AlphaNumeric") && (!string.IsNullOrEmpty(txtValidationRange.Text)))
                {
                    lblFeedBack.Text = "Range data is not needed for None, Numeric, and Alphanumeric validation type; empty the validation range field.";
                    lblFeedBack.Visible = true;
                    return false;
                }
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
                if (StartDt < DateTime.Now.Subtract(new TimeSpan(1,0,0,0)))
                {
                    pnlInvalidStartDate.Visible = true;
                    return false;
                }

                if (EndDt < DateTime.Now)
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
                //end
           
            }
            catch (System.Exception exFormat)
            {
                lblFeedBack.Text = "Invalid date entered.";
                lblFeedBack.Visible = true;
                return false;
            }
            return true;
        }

        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }
    
         
            ViewSpecialFieldsViewData FieldValue = new ViewSpecialFieldsViewData();
            FieldValue.Id = 0;
            FieldValue.Description = TxtDescription.Text;
            FieldValue.StartDate = StartTextDate.Text;
            FieldValue.EndDate = EndTextDate.Text;
           
            if (int.Parse(DataFormType) == (int)FormType.ClientContact)
            {
                FieldValue.FormType = FormType.ClientContact.ToString();
            }
            else
            {
                FieldValue.FormType = FormType.PublicMediaActivity.ToString();
            }

            FieldValue.IsRequired = rdIsRequired.SelectedItem.Text;
            FieldValue.Name = TxtName.Text;
            FieldValue.State = new State(StateID);
            FieldValue.ValidationName = ddlValidationType.SelectedItem.Text;
            //Added by Lavanya
            FieldValue.Range = txtValidationRange.Text;
            //end
            FieldValue.CreatedBy = AccountInfo.UserId;
            FieldValue.Ordinal = -1;
            try
            {
                int SpecialFieldID = SpecialFieldsBLL.AddUpdateSpecialField(FieldValue);
                RouteController.RouteTo(RouteController.SpeciaFieldsView(SpecialFieldID));
            }
            catch (ApplicationException exApp)
            {
                lblFeedBack.Text = exApp.Message;
                if (exApp.Message.Contains("The special field was created and assign available ordinal value"))
                {
                   
                    btnSave.Visible = false;
                }
                lblFeedBack.Visible = true;
            }
            
        }


    }
}
