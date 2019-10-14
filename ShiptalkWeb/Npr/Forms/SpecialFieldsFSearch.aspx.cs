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
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkWeb.Routing;
using System.Xml;


namespace ShiptalkWeb.Npr.Forms
{
    public partial class SpecialFieldsFSearch : Page, IRouteDataPage
    {
        enum FORM_REQUEST { NONE, CLIENTCONTACT, PAM }
        private const string DEFAULTFRM = "DataForm";

        protected void Page_Load(object sender, EventArgs e)
        {
            lblFeedBack.Text = string.Empty;
            lblFeedBack.Visible = false;
            if (!IsPostBack) OnViewInitialized();
        }

       
        #region Properties
        /// <summary>
        /// Indicates what data is being requested from the calling page.
        /// </summary>
        private string RequestedSpecialData
        {
            get
            {
                if (RouteData != null)
                {
                    if (RouteData.Values[DEFAULTFRM] == null) return null;
                    return RouteData.Values[DEFAULTFRM].ToString();
                }
                else
                {
                    return "0";
                }
            }

        }

        


      
        #endregion
        #region Methods
        private FormType GetFormType()
        {
            if (RequestedSpecialData.ToUpper() == "1")
            {
                return FormType.ClientContact;
            }
            else
            {
                return FormType.PublicMediaActivity;
                
            }
        }

       
        /// <summary>
        /// <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void OnViewInitialized()
        {
            //Determine what kind of data is being requested from calling web page.
            FORM_REQUEST SpecialFieldDataType = FORM_REQUEST.NONE;
            if (int.Parse(RequestedSpecialData) == (int)FORM_REQUEST.PAM)
            {
                SpecialFieldDataType = FORM_REQUEST.PAM;

            }

            if (int.Parse(RequestedSpecialData) == (int)FORM_REQUEST.CLIENTCONTACT)
            {
                SpecialFieldDataType = FORM_REQUEST.CLIENTCONTACT;
            }


            //Load the States data in the drop down list.
            AgencyBLL StatesInfo = new AgencyBLL();
            IEnumerable SuppotedStates = StatesInfo.GetStates();


            foreach (System.Collections.Generic.KeyValuePair<string, string> StateValueFound in SuppotedStates)
            {
                ddlStates.Items.Add(new ListItem(StateValueFound.Value, State.GetCode(StateValueFound.Key)));


            }
            

            if (AccountInfo.IsStateAdmin)
            {
                ddlStates.SelectedIndex = -1;
                KeyValuePair<string, string> StateValue = State.GetState(AccountInfo.StateFIPS);
                ListItem FoundState = ddlStates.Items.FindByText(StateValue.Value);
                ddlStates.Items[0].Selected = false;
                FoundState.Selected = true;
                ddlStates.Enabled = false;
                ddlStates.Visible = false;
                string UserState = State.GetStateName(AccountInfo.StateFIPS);
                lblState.Text = UserState;
                cStates.Visible = false;
                return;
            }

            if (AccountInfo.IsAdmin && AccountInfo.ScopeId == (short)Scope.CMS )
            {
                ddlStates.Items.Add(new ListItem("CMS", "99"));
                ddlStates.SelectedIndex = -1;
                ddlStates.Items.Insert(0, "--Select A State--");
                ddlStates.Items[0].Selected = true;
                ddlStates.Visible = true;
                cStates.Visible = true;
                return;
            }

            

        }


       
        /// <summary>
        /// Bind the data to the grid
        /// </summary>
        private void BindDependantData()
        {
            if (AccountInfo.IsStateAdmin || AccountInfo.IsAdmin)
            {
                IEnumerable<ViewSpecialFieldsViewData> ValidRecords = null;
                DateTime? StartDate = null;
                DateTime? EndDate = null;
                try
                {
                    StartDate = Convert.ToDateTime(TxtStartDT.Text);
                    EndDate = Convert.ToDateTime(TxtEndDT.Text);
                    if (StartDate >= EndDate)
                    {
                        lblFeedBack.Text = "Filter Start date must come after end date.";
                        lblFeedBack.Visible = true;
                        return;
                    }
                    
                }
                catch (System.Exception exDate)
                {
                    lblFeedBack.Text = "Invalid search date entered.";
                    lblFeedBack.Visible = true;
                    return;
                }
                if (AccountInfo.IsAdmin)
                    ValidRecords = SpecialFieldsBLL.GetSpecialFieldsView(ddlStates.SelectedValue.ToString(), StartDate.Value , EndDate.Value, GetFormType());    
                else
                    ValidRecords = SpecialFieldsBLL.GetSpecialFieldsView(AccountInfo.StateFIPS, StartDate.Value , EndDate.Value, GetFormType());
             
   
                grdFields.DataSource = ValidRecords;
                grdFields.DataBind();

                //Check to see if any records were returned.
                //IEnumerable does not return a count therefore we enumerate and check for at least 1
                bool bContainsRecords = false;
                foreach (ViewSpecialFieldsViewData rec in ValidRecords)
                {
                    bContainsRecords = true;
                    break;
                }

                if (!bContainsRecords)
                {
                    //OnViewInitialized();
                    lblFeedBack.Text = "No special fields could be found.";
                    lblFeedBack.Visible = true;
                }
            }
            else
            {
                lblFeedBack.Text = "You do not have permission to add or modify special fields.";
                lblFeedBack.Visible = true;
                return;
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

        #region Events

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            if ((ddlStates.SelectedIndex == -1 || ddlStates.SelectedIndex == 0) && string.IsNullOrEmpty(lblState.Text) )
            {
                lblFeedBack.Text = "You must select state.";
                lblFeedBack.Visible = true;
                return;
            }
            if (TxtStartDT.Text == string.Empty)
            {
                lblFeedBack.Text = "You must select start date.";
                lblFeedBack.Visible = true;
                return;
            }
            BindDependantData();
        }

        #endregion

        protected void grdFields_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            const int FIELDID = 0;
            const int FIELDNAME = 1;
            const int STARTDATE = 2;
            const int ENDDATE = 3;
            const int FIELDTYPE = 4;

 
            //enum FIELDLAYOUT {ID,FIELDNAME,STARTDATE,ENDDATE,FIELDTYPE};
            
            e.Row.Cells[FIELDID].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (int.Parse(e.Row.Cells[FIELDTYPE].Text) < 10)
                {
                    e.Row.Cells[FIELDTYPE].Text = "CMS";
                }
                else
                {
                    e.Row.Cells[FIELDTYPE].Text = "STATE";
                }
            }


        }

        protected void grdFields_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            const int FIELDID = 0;
           
            int Index = -1;
            int SpecialFieldID = -1;
            GridView gvr = (GridView)sender;
            Index = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "FIELD_EDIT")
            {
                SpecialFieldID = int.Parse(gvr.Rows[Index].Cells[FIELDID].Text);
                RouteController.RouteTo(RouteController.SpeciaFieldsEdit(SpecialFieldID));
            }

            if (e.CommandName == "FIELD_DELETE")
            {
                SpecialFieldID = int.Parse(gvr.Rows[Index].Cells[FIELDID].Text);
                SpecialFieldsBLL.DeleteSpecialField(SpecialFieldID);
                BindDependantData();
                
            }

            

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if ((ddlStates.SelectedIndex == -1 || ddlStates.SelectedIndex == 0) && string.IsNullOrEmpty(lblState.Text))
            {
                lblFeedBack.Text = "You must select state.";
                lblFeedBack.Visible = true;
                return;
            }
            
            if (ddlStates.Visible)
            {
                RouteController.RouteTo(RouteController.SpeciaFieldsAdd((int)GetFormType(), ddlStates.SelectedItem.Value.ToString()));
            }
            else
            {
                RouteController.RouteTo(RouteController.SpeciaFieldsAdd((int)GetFormType(), AccountInfo.StateFIPS));
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            TxtStartDT.Text = string.Empty;
            TxtEndDT.Text = string.Empty;
        }

        

       


    }
}
