using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkWeb.Routing;

using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkCommon;
using System.Text;
using System.Text.RegularExpressions;

using RegistrationObject = ShiptalkLogic.BusinessObjects.UserRegistration;

namespace ShiptalkWeb.ShipNPR_Reports
{
    public partial class NprPAMReportSearch : Page, IRouteDataPage
    {
        protected string AGENCY_DROPDOWN_DEFAULT_TEXT = "-- All of my agencies --";

        private AgencyBLL _logic;
        private CCFBLL _ccLogic;
        private PamBLL _pamLogic;


        private Role _selectedRole = null;
        public PAMReportType ccReportType
        {
            get;
            set;
        }
        public AgencyBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new AgencyBLL();

                return _logic;
            }
        }

        protected CCFBLL CCLogic
        {
            get
            {
                if (_ccLogic == null) _ccLogic = new CCFBLL();

                return _ccLogic;
            }
        }

        protected PamBLL PAMLogic
        {
            get
            {
                if (_pamLogic == null) _pamLogic = new PamBLL();

                return _pamLogic;
            }
        }

        protected Scope Scope { get { return this.AccountInfo.Scope; } }
        private int UserId { get { return this.AccountInfo.UserId; } }

        private const string IdKey = "Id";
        public int? Id
        {
            get
            {
                if (RouteData.Values[IdKey] == null) return null;

                int id;

                if (int.TryParse(RouteData.Values[IdKey].ToString(), out id))
                    return id;

                return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            errorMessage.Visible = false;
            btnSubmit.Enabled = true;
            if (!IsPostBack)
            {
                DisplayReportTypeForScope();
                DisplayUICriteriaByReportType();
            }
        }
        protected void PopulateStates()
        {
            ddlStates.Items.Clear();

            IEnumerable<KeyValuePair<string, string>> states = null;
            ccReportType = (PAMReportType)Convert.ToInt16(ReportType.SelectedValue.ToString());

            if (ccReportType != PAMReportType.National)
            {
                states = State.GetStatesWithFIPSKey();
            }
            if (states != null) {
                if (states.Count() > 0)
                {
                    ddlStates.DataSource = states;
                    ddlStates.DataTextField = "Value";
                    ddlStates.DataValueField = "Key";
                    ddlStates.DataBind();
                }
            }

            if (ccReportType == PAMReportType.State)
            {
                ddlStates.AutoPostBack = false;
            }
            else
            {
                ddlStates.AutoPostBack = true;
            }
        }
        private bool CheckDateRange()
        {
            DateTime fromDate = Convert.ToDateTime(dtStartContactDate.Text.Trim());
            DateTime toDate = Convert.ToDateTime(dtEndContactDate.Text.Trim());

            if (fromDate > toDate)
            {
                errorMessage.Visible = true;
                errorMessage.Text = "To Date Should be greater than From Date.";
                return false;
            }
            return true;
           
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Page.Validate("NprReport");

            if(CheckDateRange()){

                Session["ReportType"] = "";
                Session["ReportStartDate"] = "";
                Session["ReportEndDate"] = "";
                Session["ReportStateFIPSCode"] = "";
                Session["ReportAgencyId"] = "";
                Session["ReportAgencyName"] = string.Empty;

                Session["ReportCountyOfActivityEvent"]= string.Empty;
                Session["ReportCountyName"] = string.Empty;

                Session["ReportZipCodeOfActivityEvent"] = string.Empty;

                Session["PresenterContributorUserId"]= string.Empty;
                Session["PresenterContributor"] = string.Empty;
                Session["SubmitterUserId"]= string.Empty;
                Session["SubmitterUserName"] = string.Empty;

                Session["ScopeId"] = String.Empty;
                Session["UserId"] = String.Empty;

                //clear the session value for substateRegionID and name
                Session["SubStateRegionId"] = String.Empty;
                Session["SubStateRegionName"] = String.Empty;
                Session["IsSubStateAdmin"] = String.Empty;



                //pass the scope and userId always.
                Session["ScopeId"] = AccountInfo.ScopeId;
                Session["UserId"] = AccountInfo.UserId;

                if (this.AccountInfo.Scope == Scope.SubStateRegion && AccountInfo.IsAdmin)
                    Session["IsSubStateAdmin"] = true;
                else
                    Session["IsSubStateAdmin"] = false;

                Session["ReportType"] = ReportType.SelectedValue;
                Session["ReportStartDate"] = dtStartContactDate.Text.Trim();
                Session["ReportEndDate"] = dtEndContactDate.Text.Trim();
                Session["ReportStateFIPSCode"] = ddlStates.SelectedValue;


                if (ReportType.SelectedValue == "5")
                {
                    Session["ReportAgencyId"] = dropDownListAgencyForCounselor.SelectedValue;
                    Session["ReportAgencyName"] = dropDownListAgencyForCounselor.SelectedItem.Text;
                }

                if (ReportType.SelectedValue == "6")
                {
                    Session["ReportAgencyId"] = dropDownListAgencyForSubmitter.SelectedValue;
                    Session["ReportAgencyName"] = dropDownListAgencyForSubmitter.SelectedItem.Text;
                }

                if (ddlAgency.SelectedIndex != -1)
                {
                    Session["ReportAgencyId"] = ddlAgency.SelectedValue;
                    Session["ReportAgencyName"] = ddlAgency.SelectedItem.Text;
                }
                if (ddlCountyOfActivityEvent.SelectedIndex != -1)
                {
                    Session["ReportCountyOfActivityEvent"] = ddlCountyOfActivityEvent.SelectedValue;
                    Session["ReportCountyName"] = ddlCountyOfActivityEvent.SelectedItem.Text;
                }
                if (ddlZipCodeOfActivityEvent.SelectedIndex != -1)
                {
                    Session["ReportZipCodeOfActivityEvent"] = ddlZipCodeOfActivityEvent.SelectedValue;
                }
                if (ddlPresenterContributor.SelectedIndex != -1)
                {
                    Session["PresenterContributorUserId"] = ddlPresenterContributor.SelectedValue;
                    Session["PresenterContributor"] = ddlPresenterContributor.SelectedItem.Text;
                }
                if (ddlSubmitter.SelectedIndex != -1)
                {
                    Session["SubmitterUserId"] = ddlSubmitter.SelectedValue;
                    Session["SubmitterUserName"] = ddlSubmitter.SelectedItem.Text;
                }
                if (ddlSubstateRegionsBasedOnAgencyGroupings.SelectedIndex != -1)
                {
                    Session["SubStateRegionId"] = ddlSubstateRegionsBasedOnAgencyGroupings.SelectedValue;
                    Session["SubStateRegionName"] = ddlSubstateRegionsBasedOnAgencyGroupings.SelectedItem.Text;
                }
                if (ddlSubstateRegionBasedOnCountiesOfActivityEvent.SelectedIndex != -1)
                {
                    Session["SubStateRegionId"] = ddlSubstateRegionBasedOnCountiesOfActivityEvent.SelectedValue;
                    Session["SubStateRegionName"] = ddlSubstateRegionBasedOnCountiesOfActivityEvent.SelectedItem.Text;
                }
                RouteController.RouteTo(RouteController.NprPAMSummaryReport());
            }
          
        }
        private void BindAgency(string StateFIPS)
        {
            ddlAgency.Items.Clear();

            if (this.AccountInfo.Scope.IsHigherOrEqualTo(Scope.State))
            {
                PopulateAgenciesForState(StateFIPS);
            }
            else if (this.AccountInfo.Scope.IsEqual(Scope.SubStateRegion))
            {
                PopulateAgenciesForSubStateUser();
            }
            else
            {
                PopulateAgenciesForUser();
            }
        }
        private void PopulateAgenciesForSubStateUser()
        {
            IEnumerable<UserRegionalAccessProfile> SubStateProfiles = UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(UserId, false);
            List<KeyValuePair<int, string>> Agencies = new List<KeyValuePair<int, string>>();
            foreach (UserRegionalAccessProfile subStProfile in SubStateProfiles)
            {
                IEnumerable<ShiptalkLogic.BusinessObjects.Agency> AgencyForSubState = LookupBLL.GetAgenciesForSubStateRegion(subStProfile.RegionId);
                Agencies.AddRange(AgencyForSubState.Where(elem => elem.IsActive == true).Select(p => (new KeyValuePair<int, string>(p.Id.Value, p.Name))));
            }
            if (Agencies != null && Agencies.Count > 0)
                ddlAgency.DataSource = Agencies.Distinct().OrderBy(p => p.Value);
            else
                ddlAgency.DataSource = Agencies;

            ddlAgency.DataTextField = "Value";
            ddlAgency.DataValueField = "Key";
            ddlAgency.DataBind();

            if (Agencies == null || Agencies.Count() == 0)
            {
                ddlAgency.Items.Add(new ListItem("No agencies available", "0"));
                btnSubmit.Enabled = false;
            }
            else if (Agencies.Count() > 0)
            {
                btnSubmit.Enabled = true;
                ddlAgency.Items.Insert(0, new ListItem("<-- Select agency -->", "0"));
            }


            //clean up the dependent dropdowns if the user is substate admin..
            if (AccountInfo.Scope == Scope.SubStateRegion && AccountInfo.IsAdmin)
            {
                ddlCountyOfActivityEvent.Items.Clear();
                ddlZipCodeOfActivityEvent.Items.Clear();
            }

        }
        private bool IsSelectedRoleApproverSpecial
        {
            get
            {
                bool IsSpecialRole = false;

                if (_selectedRole.scope.IsEqual(this.AccountInfo.Scope) && _selectedRole.IsAdmin && !this.AccountInfo.IsShipDirector)
                    IsSpecialRole = true;

                return IsSpecialRole;
            }
        }
        private void BindUserAgency(IEnumerable<UserRegionalAccessProfile> UserAgencies)
        {
            if (UserAgencies != null)
            {
                ddlAgency.DataSource = UserAgencies;
                ddlAgency.DataTextField = "RegionName";
                ddlAgency.DataValueField = "RegionId";
                ddlAgency.DataBind();
            }

            if (UserAgencies == null || UserAgencies.Count() == 0)
            {
                btnSubmit.Enabled = false;
                ddlAgency.Items.Add(new ListItem("No agencies available", "0"));
            }
            else if (ddlAgency.Items.Count > 0)
            {
                btnSubmit.Enabled = true;
                ddlAgency.Items.Insert(0, new ListItem("<-- Select agency -->", "0"));
            }
        }
        private void PopulateAgenciesForState(string StateFIPS)
        {
            var Agencies = LookupBLL.GetAgenciesForState(StateFIPS,true);
            if (Agencies != null)
            {
                IEnumerable<KeyValuePair<int, string>> OrderedAgencies = Agencies.OrderBy(p => p.Value);
                ddlAgency.DataSource = OrderedAgencies;
            }
            else
                ddlAgency.DataSource = Agencies;

            ddlAgency.DataTextField = "Value";
            ddlAgency.DataValueField = "Key";
            ddlAgency.DataBind();

            if (ddlAgency.Items.Count > 0)
            {
                ddlAgency.Items.Insert(0, new ListItem("<-- Select agency -->", "0"));
                btnSubmit.Enabled = true;
            }
            else
            {
                ddlAgency.Items.Add(new ListItem("No agencies available", "0"));
                btnSubmit.Enabled = false;
            }
        }
        private void PopulateAgenciesForUser()
        {
            IEnumerable<UserRegionalAccessProfile> UserAgencies = UserAgencyBLL.GetUserAgencyProfiles(UserId, false);

            if (UserAgencies != null)
                UserAgencies = UserAgencies.Where(p => p.IsActive == true);

            BindUserAgency(UserAgencies);
        }
       
        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        protected void ddlStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            ccReportType = (PAMReportType)Convert.ToInt16(ReportType.SelectedValue.ToString());
            switch (ccReportType)
            {
                case PAMReportType.Agency:
                    BindAgency(ddlStates.SelectedValue);
                    break;
                case PAMReportType.CountyOfActivityEvent:
                    BindCountyAndOrAgencyForCounselorLocation();
                    break;
                case PAMReportType.ZipCodeOfActivityEvent:
                    BindZipCodeAndOrAgencyForCounselorLocation();
                    break;
                case PAMReportType.SubStateRegionOnAgency:
                    BindRegionID(5, ddlSubstateRegionsBasedOnAgencyGroupings);
                    break;
                case PAMReportType.SubStateRegionOnCountiesOfActivityEvent:
                    BindRegionID(3, ddlSubstateRegionBasedOnCountiesOfActivityEvent);
                    break;
                case PAMReportType.PresenterContributor:
                    //Update the agency list based on the users scope and clear the counselors and submitters lists.
                    BindStateDependentData(new State(ddlStates.SelectedValue));
                   /// BindCounselor();

                    break;
                case PAMReportType.Submitter:

                    //Update the agency list based on the users scope and clear the counselors and submitters lists.
                    BindStateDependentData(new State(ddlStates.SelectedValue));

                    ///BindSubmitter();
                    break;
            }
        }
        private void DisplayReportTypeForScope()
        {
            if (this.AccountInfo.Scope == Scope.CMS)
                return;

            if (this.AccountInfo.Scope == Scope.State)
            {
                RemoveReportTypeItems("9");
                return;
            }

            if (this.AccountInfo.Scope == Scope.SubStateRegion)
            {
                RemoveReportTypeItems("1");
                RemoveReportTypeItems("7");
                RemoveReportTypeItems("8");
                RemoveReportTypeItems("9");

                if (this.AccountInfo.IsAdmin == false)
                {
                    RemoveReportTypeItems("3");
                    RemoveReportTypeItems("4");

                }
                return;
            }

            if (this.AccountInfo.Scope == Scope.Agency)
            {
                RemoveReportTypeItems("1");
                RemoveReportTypeItems("7");
                RemoveReportTypeItems("8");

                RemoveReportTypeItems("9");
                return;
            }
        }
        private void RemoveReportTypeItems(string ReportValue)
        {
            ListItem liReportValue = ReportType.Items.FindByValue(ReportValue);
            ReportType.Items.Remove(liReportValue);
        }
        protected void ReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CheckAccess();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        protected void CheckAccess()
        {
            switch (this.AccountInfo.Scope)
            {
                case Scope.CMS:
                    
                    PopulateStates();
                    SelectStateByScope();
                    DisplayUICriteriaByReportType();
                    DisplayCriteria();
                    break;
                case Scope.State:
                    PopulateStates();
                    SelectStateByScope();
                    DisplayUICriteriaByReportType();
                    DisplayCriteria();
                    break;
                case Scope.SubStateRegion:
                    PopulateStates();
                    SelectStateByScope();
                    DisplayUICriteriaByReportType();
                    DisplayCriteria();
                    break;
                case Scope.Agency:
                    PopulateStates();
                    SelectStateByScope();
                    DisplayUICriteriaByReportType();
                    DisplayCriteria();
                    break;
            }
        }
        protected void DisplayCriteria()
        {
            ccReportType = (PAMReportType)Convert.ToInt16(ReportType.SelectedValue.ToString());
            switch (ccReportType)
            {
                case PAMReportType.Agency:
                    BindAgency(this.AccountInfo.StateFIPS);
                    break;
                case PAMReportType.CountyOfActivityEvent:
                    BindCountyAndOrAgencyForCounselorLocation();
                    break;
                case PAMReportType.ZipCodeOfActivityEvent:
                    BindZipCodeAndOrAgencyForCounselorLocation();
                    break;
                case PAMReportType.SubStateRegionOnAgency:
                    BindRegionID(7,ddlSubstateRegionsBasedOnAgencyGroupings);
                    break;
                case PAMReportType.SubStateRegionOnCountiesOfActivityEvent:
                    BindRegionID(6,ddlSubstateRegionBasedOnCountiesOfActivityEvent);
                    break;
                case PAMReportType.PresenterContributor:
                    //Update the agency list based on the users scope and clear the counselors and submitters lists.
                    BindStateDependentData(new State(ddlStates.SelectedValue));

                    ////BindCounselor();
                    break;
                case PAMReportType.Submitter:
                    //Update the agency list based on the users scope and clear the counselors and submitters lists.
                    BindStateDependentData(new State(ddlStates.SelectedValue));

                    /////BindSubmitter();
                    break;
            }
        }

        //not used..
        private void BindAgencyForSubstateRegionId()
        {
            //ddlAgency.Items.Clear();
            if (ddlSubstateRegionsBasedOnAgencyGroupings.SelectedValue != "0")
            {
                int RegionID = Convert.ToInt32(ddlSubstateRegionsBasedOnAgencyGroupings.SelectedValue.ToString());
                ////// IEnumerable<KeyValuePair<int, string>> Agencies;
                ////// Agencies = Logic.GetSubStateAgenciesForSubStateRegion(ddlStates.SelectedValue.ToString(), RegionID);

                ////// if (Agencies != null)
                //////{
                //////    ddlAgency.DataSource = Agencies;
                //////    ddlAgency.DataTextField = "value";
                //////    ddlAgency.DataValueField = "key";
                //////    ddlAgency.DataBind();
                //////}
                ////// if (Agencies == null || Agencies.Count() == 0)
                ////// {
                //////     ddlAgency.Width = Unit.Pixel(470);
                //////     ddlAgency.Items.Add(new ListItem("No Agencies available", "0"));
                //////     btnSubmit.Enabled = false;
                ////// }
                ////// else if (ddlAgency.Items.Count > 0)
                ////// {
                //////     ddlAgency.Width = Unit.Pixel(470);
                //////     ddlAgency.Items.Insert(0, new ListItem("<-- Select Agency ID -->", "0"));
                //////     btnSubmit.Enabled = true;
                ////// }
            }
        }
        private void BindRegionID(int SubStateRegionType, DropDownList ddl)
        {
            ddl.Items.Clear();

            var substateregion = new List<SearchSubStateRegionsViewData>(Logic.GetSubStateRegionsForCCReports(ddlStates.SelectedValue.ToString(), SubStateRegionType));

            if (substateregion != null)
            {
                ddl.DataSource = substateregion;
                ddl.DataTextField = "Name";
                ddl.DataValueField = "Id";
                ddl.DataBind();
            }
            if (substateregion == null || substateregion.Count() == 0)
            {
                ddl.Width = Unit.Pixel(470);
                ddl.Items.Add(new ListItem("No Region ID available", "0"));

                btnSubmit.Enabled = false;
            }
            else if (ddl.Items.Count > 0)
            {
                ddl.Width = Unit.Pixel(470);
                ddl.Items.Insert(0, new ListItem("<-- Select Region ID -->", "0"));
            }
        }


        private void BindCountyAndOrAgencyForCounselorLocation()
        {
            //if the user is SubstateRegionADMIN show him the agency dropdown...
            if (AccountInfo.Scope == Scope.SubStateRegion && AccountInfo.IsAdmin)
            {
                PopulateAgenciesForSubStateUser();
                trAgency.Visible = true;
                ddlAgency.AutoPostBack = true;

            }
            else
            {
                trAgency.Visible = false;
                ddlAgency.AutoPostBack = false;
                BindCountyofCounselorLocation(false);
            }
        }

        private void BindZipCodeAndOrAgencyForCounselorLocation()
        {
            //if the user is SubstateRegionADMIN show him the agency dropdown...
            if (AccountInfo.Scope == Scope.SubStateRegion && AccountInfo.IsAdmin)
            {
                PopulateAgenciesForSubStateUser();
                trAgency.Visible = true;
                ddlAgency.AutoPostBack = true;

            }
            else
            {
                trAgency.Visible = false;
                ddlAgency.AutoPostBack = false;
                BindZipCodeofCounselorLocation(false);
            }
        }

        private void BindCountyofCounselorLocation(bool IsSubStateRegionAdmin)
        {


            ddlCountyOfActivityEvent.Items.Clear();
            AgencyBLL counties = new AgencyBLL();
            IEnumerable<KeyValuePair<string, string>> CountiesForActivityEvent = null;

            if (IsSubStateRegionAdmin)
                //find the counties by agency
                CountiesForActivityEvent = counties.GetCountyByAgencyIdForReport(Convert.ToInt32(ddlAgency.SelectedValue), FormType.PublicMediaActivity);
            else
                //find the counties by state
                CountiesForActivityEvent = counties.GetCountyForCounselorLocationByState(ddlStates.SelectedValue, FormType.PublicMediaActivity);

            if (CountiesForActivityEvent.Count() > 0)
            {
                ddlCountyOfActivityEvent.DataSource = CountiesForActivityEvent;
                ddlCountyOfActivityEvent.DataTextField = "value";
                ddlCountyOfActivityEvent.DataValueField = "key";
                ddlCountyOfActivityEvent.DataBind();
            }
            if (CountiesForActivityEvent == null || CountiesForActivityEvent.Count() == 0)
            {
                btnSubmit.Enabled = false;
                ddlCountyOfActivityEvent.Width = Unit.Pixel(470);
                ddlCountyOfActivityEvent.Items.Add(new ListItem("No county of activity event available", "0"));
            }
            else if (ddlCountyOfActivityEvent.Items.Count > 0)
            {
                btnSubmit.Enabled = true;
                ddlCountyOfActivityEvent.Width = Unit.Pixel(470);
                ddlCountyOfActivityEvent.Items.Insert(0, new ListItem("<-- Select county of activity event -->", "0"));
            }
        }

        private void BindZipCodeofCounselorLocation(bool IsSubStateRegionAdmin)
        {
            ddlZipCodeOfActivityEvent.Items.Clear();
            IEnumerable<KeyValuePair<string, string>> ZipCodeForActivityEvent= null;
            AgencyBLL zipcode = new AgencyBLL();

            if (IsSubStateRegionAdmin)
                //find the counties by agency
                ZipCodeForActivityEvent = zipcode.GetZipByAgencyIdForReport(Convert.ToInt32(ddlAgency.SelectedValue), FormType.PublicMediaActivity);
            else
                //find the counties by state
                ZipCodeForActivityEvent = zipcode.GetZipCodeForCounselorLocationByState(ddlStates.SelectedValue, FormType.PublicMediaActivity);

            if (ZipCodeForActivityEvent != null)
            {
                ddlZipCodeOfActivityEvent.DataSource = ZipCodeForActivityEvent;
                ddlZipCodeOfActivityEvent.DataTextField = "value";
                ddlZipCodeOfActivityEvent.DataValueField = "key";
                ddlZipCodeOfActivityEvent.DataBind();
            }
            if (ZipCodeForActivityEvent == null || ZipCodeForActivityEvent.Count() == 0)
            {
                ddlZipCodeOfActivityEvent.Width = Unit.Pixel(370);
                ddlZipCodeOfActivityEvent.Items.Add(new ListItem("No zipcode of activity event available", "0"));
            }
            else if (ddlZipCodeOfActivityEvent.Items.Count > 0)
            {
                ddlZipCodeOfActivityEvent.Width = Unit.Pixel(370);
                ddlZipCodeOfActivityEvent.Items.Insert(0, new ListItem("<-- Select zipcode of activity event -->", "0"));
            }
        }
       
        protected void SelectStateByScope()
        {
            if (ddlStates.Items.Count > 0)
            {
                if (this.AccountInfo.Scope.IsEqual(Scope.CMS))
                {
                    ddlStates.Items[0].Selected = true;
                }
                else
                {
                    ddlStates.Items.FindByValue(this.AccountInfo.StateFIPS).Selected = true;
                    ddlStates.Enabled = false;
                }
            }
        }



        private void BindCounselor()
        {
            ddlPresenterContributor.Items.Clear();

            //IEnumerable<KeyValuePair<int, string>> PresenterContributor = LookupBLL.GetPresenterForState(ddlStates.SelectedValue);
            //Populate Counselors from DB
            IEnumerable<KeyValuePair<int, string>> PresenterContributor = PamPresenters;

            if (PresenterContributor != null)
            {
                ddlPresenterContributor.DataSource = PresenterContributor;
                ddlPresenterContributor.DataTextField = "value";
                ddlPresenterContributor.DataValueField = "key";
                ddlPresenterContributor.DataBind();
            }
            if (PresenterContributor == null || PresenterContributor.Count() == 0)
            {
                ddlPresenterContributor.Width = Unit.Pixel(470);
                ddlPresenterContributor.Items.Add(new ListItem("No presenter contributor", "0"));
            }
            else if (ddlPresenterContributor.Items.Count > 0)
            {
                ddlPresenterContributor.Width = Unit.Pixel(470);
                ddlPresenterContributor.Items.Insert(0, new ListItem("<-- Select Presenter-Contributor -->", "0"));
            }
        }


        private void BindSubmitter()
        {
            ddlSubmitter.Items.Clear();

           // IEnumerable<KeyValuePair<int, string>> submitter = LookupBLL.GetClientContactSubmitterForState(ddlStates.SelectedValue);
            //Populate Submitters from DB
            IEnumerable<KeyValuePair<int, string>> submitter = Submitters;

            if (submitter != null)
            {
                ddlSubmitter.DataSource = submitter;
                ddlSubmitter.DataTextField = "value";
                ddlSubmitter.DataValueField = "key";
                ddlSubmitter.DataBind();
            }
            if (submitter == null || submitter.Count() == 0)
            {
                ddlSubmitter.Width = Unit.Pixel(470);
                ddlSubmitter.Items.Add(new ListItem("No submitter", "0"));
            }
            else if (ddlSubmitter.Items.Count > 0)
            {
                ddlSubmitter.Width = Unit.Pixel(470);
                ddlSubmitter.Items.Insert(0, new ListItem("<-- Select submitter -->", "0"));
            }
        }

        protected void BindStateDependentData(State state)
        {
            ccReportType = (PAMReportType)Convert.ToInt16(ReportType.SelectedValue.ToString());
            if (ccReportType == PAMReportType.Submitter)
            {
                dropDownListAgencyForSubmitter.Items.Clear();
                dropDownListAgencyForSubmitter.DataSource = new List<KeyValuePair<int, string>>(UserBLL.GetAllAgenciesForUser(UserId, Scope, state.Code));
                dropDownListAgencyForSubmitter.DataBind();
                dropDownListAgencyForSubmitter.Items.Insert(0, new ListItem(AGENCY_DROPDOWN_DEFAULT_TEXT, "0"));

                dropDownListAgencyForSubmitter.Enabled = true;

                BindAgencyDependatData(1);
            }
            else if (ccReportType == PAMReportType.PresenterContributor)
            {
                dropDownListAgencyForCounselor.Items.Clear();
                dropDownListAgencyForCounselor.DataSource = new List<KeyValuePair<int, string>>(UserBLL.GetAllAgenciesForUser(UserId, Scope, state.Code));
                dropDownListAgencyForCounselor.DataBind();
                dropDownListAgencyForCounselor.Items.Insert(0, new ListItem(AGENCY_DROPDOWN_DEFAULT_TEXT, "0"));

                dropDownListAgencyForCounselor.Enabled = true;

                BindAgencyDependatData(2);
            }
        }

        protected void DisplayUICriteriaByReportType()
        {
            ccReportType = (PAMReportType)Convert.ToInt16(ReportType.SelectedValue.ToString());
            switch(ccReportType)
            {
                case PAMReportType.State:
                    DisplayTR(true, true, false, true, false, false, false, false, false, false,false,false);
                    break;
                case PAMReportType.Agency:
                    DisplayTR(true, true, true, true, false, false, false, false, false, false, false, false);
                    break;
                case PAMReportType.CountyOfActivityEvent:
                    DisplayTR(true, true, false, true, true, false, false, false, false, false, false, false);
                    break;
                case PAMReportType.ZipCodeOfActivityEvent:
                    DisplayTR(true, true, false, true, false, true, false, false, false, false, false, false);
                    break;
                case PAMReportType.PresenterContributor:
                    DisplayTR(true, true, false, true, false, false, false, false, true, false, false, true);
                    break;
                case PAMReportType.Submitter:
                    DisplayTR(true, true, false, true, false, false, false, false, false, true, true, false);
                    break;
                case PAMReportType.SubStateRegionOnAgency:
                    DisplayTR(true, true, false, true, false, false, true, false, false, false, false, false);
                    break;
                case PAMReportType.SubStateRegionOnCountiesOfActivityEvent:
                    DisplayTR(true, true, false, true, false, false, false, true, false, false, false, false);
                    break;
                case PAMReportType.National:
                    DisplayTR(false, true, false, true, false, false, false, false, false, false, false, false);
                    break;
                default:
                    DisplayTR(false, false, false, false, false, false, false, false, false, false, false, false);
                    break;
            }
        }
        private void DisplayTR(bool bState, bool bDateOfContact, bool bAgency, bool bSubmit,
            bool bCountyOfActivityEvent, bool bZipCodeActivityEvent,
            bool bSubstateRegionsBasedOnAgencyGroupings, bool bSubStateRegionOnCountiesOfActivityEvent,
        bool bPresenterContributor, bool bSubmitter, bool bAgencyForSubmitter, bool bAgencyForCounselor)
        {
            trState.Visible = bState;
            DateOfContact.Visible = bDateOfContact;
            trAgency.Visible = bAgency;
            Submit.Visible = bSubmit;
            trCountyOfActivityEvent.Visible = bCountyOfActivityEvent;
            trZipCodeOfActivityEvent.Visible = bZipCodeActivityEvent;
            trSubstateRegionsBasedOnAgencyGroupings.Visible = bSubstateRegionsBasedOnAgencyGroupings;
            trSubstateRegionBasedOnCountiesOfActivityEvent.Visible = bSubStateRegionOnCountiesOfActivityEvent;
            trPresenterContributor.Visible = bPresenterContributor;
            trSubmitter.Visible = bSubmitter;
            trAgencySubmmiter.Visible = bAgencyForSubmitter;
            trAgencyCounselor.Visible = bAgencyForCounselor;
        }

        public enum PAMReportType
        {
            State = 1,
            Agency = 2,
            CountyOfActivityEvent = 3,
            ZipCodeOfActivityEvent = 4,
            PresenterContributor = 5,
            Submitter = 6,
            SubStateRegionOnAgency = 7,
            SubStateRegionOnCountiesOfActivityEvent = 8,
            National = 9
        }
        public enum PAMLocationType
        {
            CountyOfActivityEvent = 3,
            ZIPCodeOfActivityEvent = 2, 
            Agency=5
        }



        //not used..
        protected void ddlSubstateRegionsBasedOnAgencyGroupings_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAgencyForSubstateRegionId();
        }


        protected void dropDownListAgencyForSubmitter_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAgencyDependatData(1);
        }

        protected void dropDownListAgencyForCounselor_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAgencyDependatData(2);
        }


        //submitter 1, counselor 2
        protected void BindAgencyDependatData(int submitterOrCounselor)
        {
            if (submitterOrCounselor == 1)
            {
                BindSubmitter();
            }
            else if (submitterOrCounselor == 2)
            {
                BindCounselor();
            }

        }



        private IEnumerable<KeyValuePair<int, string>> _Submitters { get; set; }
        protected IEnumerable<KeyValuePair<int, string>> Submitters
        {
            get
            {
                if (_Submitters == null)
                {
                    int? AgencyId = null;
                    if (IsPostBack && dropDownListAgencyForSubmitter.SelectedIndex != 0)
                        AgencyId = Convert.ToInt32(dropDownListAgencyForSubmitter.SelectedValue);

                    _Submitters = GetSubmitters(AgencyId);
                }

                return _Submitters;
            }
            set
            {
                _Submitters = value;
            }
        }

        private IEnumerable<KeyValuePair<int, string>> _PamPresenters { get; set; }
        protected IEnumerable<KeyValuePair<int, string>> PamPresenters
        {
            get
            {
                if (_PamPresenters == null)
                {
                    int? AgencyId = null;
                    if (IsPostBack && dropDownListAgencyForCounselor.SelectedIndex != 0)
                        AgencyId = Convert.ToInt32(dropDownListAgencyForCounselor.SelectedValue);

                    _PamPresenters = GetCounselors(AgencyId);
                }

                return _PamPresenters;
            }
            set
            {
                _PamPresenters = value;
            }
        }



        private IEnumerable<KeyValuePair<int, string>> GetSubmitters(int? AgencyId)
        {
            //Populate Submitters from DB
            CCFBLL.CCSubmittersFilterCriteria criteria = new CCFBLL.CCSubmittersFilterCriteria();
            criteria.StateFIPS = AccountInfo.IsCMSLevel ? ddlStates.SelectedValue : AccountInfo.StateFIPS;
            criteria.scope = this.AccountInfo.Scope;
            criteria.UserId = this.AccountInfo.UserId;
            criteria.AgencyId = AgencyId;
            return CCLogic.GetClientContactSubmitters(criteria);
        }

        private IEnumerable<KeyValuePair<int, string>> GetCounselors(int? AgencyId)
        {
            //Populate Counselors from DB
            PamBLL.PamPresenterFilterCriteria criteria = new PamBLL.PamPresenterFilterCriteria();
            criteria.StateFIPS = AccountInfo.IsCMSLevel ? ddlStates.SelectedValue : AccountInfo.StateFIPS;
            criteria.scope = this.AccountInfo.Scope;
            criteria.UserId = this.AccountInfo.UserId;
            criteria.AgencyId = AgencyId;
            return PAMLogic.GetPamPresenters(criteria);
        }

        protected void ddlAgencyForSubstateAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if the user is SubstateRegionADMIN show him the agency dropdown...
            if (AccountInfo.Scope == Scope.SubStateRegion && AccountInfo.IsAdmin)
            {
                if ((PAMReportType)Convert.ToInt16(ReportType.SelectedValue.ToString()) == PAMReportType.CountyOfActivityEvent)
                    BindCountyofCounselorLocation(true);
                else if ((PAMReportType)Convert.ToInt16(ReportType.SelectedValue.ToString()) == PAMReportType.ZipCodeOfActivityEvent)
                    BindZipCodeofCounselorLocation(true);
            }
        }
    }
    
}
