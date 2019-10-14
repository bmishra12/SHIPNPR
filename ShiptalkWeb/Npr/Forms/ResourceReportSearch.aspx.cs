using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkWeb;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessObjects.UI;
using System.Data;
using System.Drawing;
using ShiptalkWeb.Routing;
using System.Web.Routing;
using System.Linq;


namespace ShiptalkWeb
{
    public partial class ResourceReportSearch : Page, IRouteDataPage, IAuthorize
    {
        private const string IsAdminKey = "IsAdmin";
        private const string ScopeKey = "Scope";

        ResourceReportBLL Logic = new ResourceReportBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            msgFeedBack.Visible = false;
            //Label lblRecordCount = (Label)formViewResourceReport.FindControl("lblRecordCount");
            //lblRecordCount.Visible = false;
           
            if (!IsPostBack) OnViewInitialized();
            
        }

        public bool IsAdmin { get { return (bool)ViewState[IsAdminKey]; } set { ViewState[IsAdminKey] = value; } }
        public Scope Scope { get { return (Scope)ViewState[ScopeKey]; } set { ViewState[ScopeKey] = value; } }

        #region Methods

        private void InitializeDropDownList()
        {
            //Add the years to drop down list

            //We have 25 years we will permit.
            int YearMax = 25;
            int CurrentYear = 2010;
            for (int i = 0; i < YearMax; i++)
            {
                cmbYear.Items.Add((CurrentYear + i).ToString());
            }
            cmbYear.Items.Insert(0, "--Select A Year--");
            cmbYear.Items[0].Selected = true;

        }
        /// <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void OnViewInitialized()
        {
            btnNew.Visible = false;
            AgencyBLL StatesInfo = new AgencyBLL();

            IEnumerable<KeyValuePair<string, string>> StatesData = StatesInfo.GetStates();


            //if user is Cms Regional user populate only his states
            if (AccountInfo.Scope == Scope.CMSRegional)
            {


                IEnumerable<KeyValuePair<string, string>> StatesForUser = null;

                List<string> StateFIPSForCMSRegions = new List<string>();
                IEnumerable<UserRegionalAccessProfile> profiles = UserCMSBLL.GetUserCMSRegionalProfiles(AccountInfo.UserId, false);
                foreach (UserRegionalAccessProfile profile in profiles)
                {
                    IEnumerable<string> CMSStateFIPS = LookupBLL.GetStatesForCMSRegion(profile.RegionId);
                    if (CMSStateFIPS != null)
                        StateFIPSForCMSRegions.AddRange(CMSStateFIPS);
                }
                if (StateFIPSForCMSRegions.Count > 0)
                {
                    StatesForUser = (
                        from stFIPS in StatesData
                        from cmsStFIPS in StateFIPSForCMSRegions
                        where stFIPS.Key == State.GetState(cmsStFIPS).Key
                        select stFIPS
                        );
                }
                //ddlStates.DataSource = StatesForUser;
                foreach (System.Collections.Generic.KeyValuePair<string, string> StateValueFound in StatesForUser)
                {
                    ddlStates.Items.Add(new ListItem(StateValueFound.Value, StateValueFound.Key));

                }

            }
            else
            {
               // ddlStates.DataSource = StatesData;
                foreach (System.Collections.Generic.KeyValuePair<string, string> StateValueFound in StatesData)
                {
                    ddlStates.Items.Add(new ListItem(StateValueFound.Value, StateValueFound.Key));

                }
            }

            ddlStates.Items.Insert(0, "--Select A State--");
            ddlStates.Items[0].Selected = true;


            InitializeDropDownList();

            IsAdmin = AccountInfo.IsAdmin;
            Scope = (Scope)AccountInfo.ScopeId;

            if (AccountInfo.Scope== Scope.State)
            {

                //LookupBLL.GetStateFipsCodeByShortName(
                //April 15, TODO - Have states loaded from db so statename and id can be bound to dropdownlist - This may help if text changes.
                KeyValuePair<string, string> StateValue = State.GetState(AccountInfo.StateFIPS);
                lblDefaultState.Text = " <strong>:</strong> " + StateValue.Value;
                lblDefaultState.Visible = true;
                ListItem FoundState = ddlStates.Items.FindByText(StateValue.Value);
                ddlStates.Items[0].Selected = false;
                FoundState.Selected = true;
                ddlStates.Enabled = false;
                ddlStates.Visible = false;
                StateContentCell.Align = "Left";

                if (AccountInfo.IsAdmin) btnNew.Visible = true;

                return;
            }

            if (AccountInfo.IsAdmin)
            {
                ddlStates.Visible = true;
                btnNew.Visible = true;
                return;
            }

            
            
        }

        
      
        
        #endregion


        #region Events

        protected void grdReports_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = ((Control)e.CommandSource).NamingContainer as GridViewRow;
            //GridView grdReports = (GridView) formViewResourceReport.FindControl("grdReports");
            string ReportId =  grdReports.DataKeys[row.RowIndex].Value.ToString();
           
            //(sender as GridView).Rows[0].Cells[7]
            if (e.CommandName.ToUpper() == "VIEWREPORT")
            {
                RouteController.RouteTo(RouteController.ResourceReportView(int.Parse(ReportId)));
                
            }
            if (e.CommandName.ToUpper() == "DELETEREPORT")
            {
                Logic.DeActivateReport(int.Parse(ReportId));
                BindGridData();
                msgFeedBack.Text = "Report has successfuly been deleted.";
                msgFeedBack.ForeColor = Color.Red;
                msgFeedBack.Visible = true;

                
            }
            if (e.CommandName.ToUpper() == "EDITREPORT")
            {
                RouteController.RouteTo(RouteController.ResourceReportEdit(int.Parse(ReportId)));
                
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlStates.SelectedIndex == 0 )
            {
                msgFeedBack.Text = "You must select a state to search for resource reports";
                msgFeedBack.Visible = true;
                return;
            }
            //Bind Report information to grid
            DataTable FoundReports = BindGridData();
            if (FoundReports.Rows.Count > 0)
            {
                //Show message to user displaying feedback about the data in the grid.
               // Label lblRecordCount = (Label)formViewResourceReport.FindControl("lblRecordCount");
               // lblRecordCount.Visible = true;
            }
            else
            {
                //Display message back to the user if no records wer found.
                msgFeedBack.Text = "No resource reports could be found.";
                msgFeedBack.Visible = true;

            }

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            DataTable FoundExistingReport = null;
            ResourceReportBLL rpt = new ResourceReportBLL();
            
            //If this is a state admin get the state they are admin for 
            //and use it.
            if (AccountInfo.IsStateAdmin)
            {
                if (cmbYear.SelectedIndex == 0)
                {
                    msgFeedBack.Text = "You must select a year to create a resource report";
                    msgFeedBack.Visible = true;
                    return;
                }
                FoundExistingReport = rpt.GetResourceReportByStateFipCode(AccountInfo.StateFIPS, int.Parse(cmbYear.SelectedItem.Text));
            }
            else
            {
                //Store the selected state in a session so the Add Resource Report Form knows
                //The state that should be pre-selected.
                if (ddlStates.SelectedIndex == 0 || cmbYear.SelectedIndex == 0)
                {
                    msgFeedBack.Text = "You must select a state and year to create a resource report";
                    msgFeedBack.Visible = true;
                    return;
                }
                FoundExistingReport = rpt.GetResourceReportByStateFipCode(LookupBLL.GetStateFipsCodeByShortName(ddlStates.SelectedValue), int.Parse(cmbYear.SelectedItem.Text));
            }
            
            
            if (FoundExistingReport.Rows.Count > 0)
            {
                msgFeedBack.Text = "Report already created for selected state and year.";
                msgFeedBack.Visible = true;
                return;
            }
            
            Session.Add("RR_SELECTED_STATE", ddlStates.SelectedItem.Text);
            Session.Add("RR_SELECTED_YEAR", cmbYear.SelectedItem.Text);

            RouteController.RouteTo(RouteController.ResourceReportAdd(0));
        }

        #endregion



        /// <summary>
        /// Binds data to the grid.
        /// </summary>
        /// <returns></returns>
        private DataTable BindGridData()
        {
            
            DataTable FoundReports = null;
            string StateFipCode = LookupBLL.GetStateFipsCodeByShortName(ddlStates.SelectedValue);
            if (cmbYear.SelectedIndex == 0)
            {
                FoundReports = Logic.GetResourceReportByStateFipCode(StateFipCode, -1);
            }
            else
            {
                FoundReports = Logic.GetResourceReportByStateFipCode(StateFipCode, int.Parse(cmbYear.SelectedItem.Text));
            }

            if (FoundReports.Rows.Count > 0)
            {
                lblRecordCount.Text = "Most recent 100 RR Forms";
                lblRecordCount.Visible = true;
            }
            else
            {
                lblRecordCount.Text = string.Empty;
                lblRecordCount.Visible = false;

            }
            dataSourceViewResourceReports.DataSource = FoundReports;
            dataSourceViewResourceReports.DataBind();
            grdReports.DataSource = FoundReports;
            grdReports.DataBind();
            return FoundReports;
        }


        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        #region Implementation of IAuthorize

        public bool IsAuthorized()
        {
            // AuthorizedRoutes table filters the unauthorized Users

            return true;

        }

        #endregion

        
    }
}
