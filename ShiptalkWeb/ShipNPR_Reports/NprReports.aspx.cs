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
    public partial class NprReports : Page, IRouteDataPage
    {

        protected string AGENCY_DROPDOWN_DEFAULT_TEXT = "-- All of my agencies --";

        private AgencyBLL _logic;
        private CCFBLL _ccLogic;
        private Role _selectedRole = null;
        public CCReportType ccReportType
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

         protected Scope Scope { get { return this.AccountInfo.Scope; } }

        protected int UserId { get { return this.AccountInfo.UserId; } }


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
            if(!IsPostBack)
            {
                aReportList.DataBind();
                DisplayReportTypeForScope();
                DisplayUICriteriaByReportType();

             }
        }
        protected void PopulateStates()
        {
            ddlStates.Items.Clear();

            IEnumerable<KeyValuePair<string, string>> states = null;
            ccReportType = (CCReportType)Convert.ToInt16(ReportType.SelectedValue.ToString());

            if (ccReportType != CCReportType.ContactsByNational)
                states = State.GetStatesWithFIPSKey();

            if (states != null) {
                if (states.Count() > 0)
                {
                    ddlStates.DataSource = states;
                    ddlStates.DataTextField = "Value";
                    ddlStates.DataValueField = "Key";
                    ddlStates.DataBind();
                }
            }

            if (ccReportType == CCReportType.ContactsByState)
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
                Session["ReportAgencyName"] = String.Empty;

                Session["ReportCountyFipsCode"]= String.Empty;
                Session["ReportCountyName"] = String.Empty;

                Session["ReportZipCodeOfCounselorLocation"] = String.Empty;

                Session["ScopeId"] = String.Empty;
                Session["UserId"] = String.Empty;
                Session["ReportZIPCodeofClientResidenceTitle"] = String.Empty;

                //clear the session value for substateRegionID and name
                Session["SubStateRegionId"] = String.Empty;
                Session["SubStateRegionName"] = String.Empty;

                Session["ReportCountyofClientResidenceFipsCode"] = String.Empty;
                Session["ReportCountyNameofClientResidence"] = String.Empty;
                Session["ReportZIPCodeofClientResidence"] = String.Empty;
                Session["ReportZIPCodeofClientResidenceTitle"] = String.Empty;
                Session["CounselorUserId"] = String.Empty;
                Session["CounselorUserName"] = String.Empty;
                Session["SubmitterUserId"] = String.Empty;
                Session["SubmitterUserName"] = String.Empty;
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

                if (ReportType.SelectedValue == "7")
                {
                    Session["ReportAgencyId"] = dropDownListAgencyForCounselor.SelectedValue;
                    Session["ReportAgencyName"] = dropDownListAgencyForCounselor.SelectedItem.Text;

                }

                if (ReportType.SelectedValue == "8")
                {
                    Session["ReportAgencyId"] = dropDownListAgencyForSubmitter.SelectedValue;
                    Session["ReportAgencyName"] = dropDownListAgencyForSubmitter.SelectedItem.Text;

                }
                    if (ddlAgency.SelectedIndex != -1)
                        {
                            Session["ReportAgencyId"] = ddlAgency.SelectedValue;
                            Session["ReportAgencyName"] = ddlAgency.SelectedItem.Text;
                        }



                if (ddlCountyOfCounselorLocation.SelectedIndex != -1)
                {
                    Session["ReportCountyFipsCode"] = ddlCountyOfCounselorLocation.SelectedValue;
                    Session["ReportCountyName"] = ddlCountyOfCounselorLocation.SelectedItem.Text;
                }
                if (ddlZipCodeOfCounselorLocation.SelectedIndex != -1)
                {
                    Session["ReportZipCodeOfCounselorLocation"] = ddlZipCodeOfCounselorLocation.SelectedValue;
                }
                if (ddlCountyofClientResidence.SelectedIndex != -1)
                {
                    Session["ReportCountyofClientResidenceFipsCode"] = ddlCountyofClientResidence.SelectedValue;
                    Session["ReportCountyNameofClientResidence"] = ddlCountyofClientResidence.SelectedItem.Text;
                }
                if (ddlZIPCodeofClientResidence.SelectedIndex != -1)
                {
                    Session["ReportZIPCodeofClientResidence"] = ddlZIPCodeofClientResidence.SelectedValue;
                    Session["ReportZIPCodeofClientResidenceTitle"]= ddlZIPCodeofClientResidence.SelectedItem.Text;
                }
                if (ddlCounselor.SelectedIndex != -1)
                {
                    Session["CounselorUserId"] = ddlCounselor.SelectedValue;
                    Session["CounselorUserName"] = ddlCounselor.SelectedItem.Text;
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
                if (ddlSubstateRegionBasedOnCountiesOfCounselorLocations.SelectedIndex != -1)
                {
                    Session["SubStateRegionId"] = ddlSubstateRegionBasedOnCountiesOfCounselorLocations.SelectedValue;
                    Session["SubStateRegionName"] = ddlSubstateRegionBasedOnCountiesOfCounselorLocations.SelectedItem.Text;
                }
                if (ddlSubstateRegionBasedOnZIPCodesOfCounselorLocations.SelectedIndex != -1)
                {
                    Session["SubStateRegionId"] = ddlSubstateRegionBasedOnZIPCodesOfCounselorLocations.SelectedValue;
                    Session["SubStateRegionName"] = ddlSubstateRegionBasedOnZIPCodesOfCounselorLocations.SelectedItem.Text;
                }
                if (ddlSubstateRegionBasedonCountiesofClientResidence.SelectedIndex != -1)
                {
                    Session["SubStateRegionId"] = ddlSubstateRegionBasedonCountiesofClientResidence.SelectedValue;
                    Session["SubStateRegionName"] = ddlSubstateRegionBasedonCountiesofClientResidence.SelectedItem.Text;
                }
                if (ddlSubstateRegionBasedOnZIPCodesofClientResidence.SelectedIndex != -1)
                {
                    Session["SubStateRegionId"] = ddlSubstateRegionBasedOnZIPCodesofClientResidence.SelectedValue;
                    Session["SubStateRegionName"] = ddlSubstateRegionBasedOnZIPCodesofClientResidence.SelectedItem.Text;
                }
                RouteController.RouteTo(RouteController.NprSummaryReport());
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
                ddlCountyOfCounselorLocation.Items.Clear();
                ddlCountyofClientResidence.Items.Clear();
                ddlZipCodeOfCounselorLocation.Items.Clear();
                ddlZIPCodeofClientResidence.Items.Clear();
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
            ccReportType = (CCReportType)Convert.ToInt16(ReportType.SelectedValue.ToString());
            if (ccReportType == CCReportType.ContactsByAgency)
            {
                BindAgency(ddlStates.SelectedValue);
            }
            if (ccReportType == CCReportType.ContactsByCountyOfCounselorLocation)
            {
                BindCountyAndOrAgencyForCounselorLocation();
            }
            if (ccReportType == CCReportType.ContactsByZipCodeOfCounselorLocation)
            {
                BindZipCodeAndOrAgencyForCounselorLocation();
            }
            if (ccReportType == CCReportType.ContactsByCountyOfClientResidence)
            {
                BindCountyAndOrAgencyForClientResidence();
            }
            if (ccReportType == CCReportType.ContactsByZipcodeOfClientResidence)
            {
                BindZipCodeAndOrAgencyForClientResidence();
            }
            if (ccReportType == CCReportType.ContactsBySubStateRegionOnAgency)
            {
                BindRegionID(5,ddlSubstateRegionsBasedOnAgencyGroupings);
            }
            if (ccReportType == CCReportType.ContactsBySubStateRegionOnCountiesOfCounselorLocation)
            {
                BindRegionID(3, ddlSubstateRegionBasedOnCountiesOfCounselorLocations);
            }
            if (ccReportType == CCReportType.ContactsBySubStateRegionOnZipcodesOfCounselorLocation)
            {
                BindRegionID(2, ddlSubstateRegionBasedOnZIPCodesOfCounselorLocations);
            }
            if (ccReportType == CCReportType.ContactsBySubStateRegionOnCountiesOfClientResidence)
            {
                BindRegionID(1,ddlSubstateRegionBasedonCountiesofClientResidence);
            }
            if (ccReportType == CCReportType.ContactsBySubStateRegionOnZipcodeOfClientResidence)
            {
                BindRegionID(4, ddlSubstateRegionBasedOnZIPCodesofClientResidence);
            }
            if (ccReportType == CCReportType.ContactsByCounselor) 
            {

                //Update the agency list based on the users scope and clear the counselors and submitters lists.
                BindStateDependentData(new State(ddlStates.SelectedValue));

                //////BindCounselor();

            }
            if (ccReportType == CCReportType.ContactsBySubmitter)
            {

                //Update the agency list based on the users scope and clear the counselors and submitters lists.
                BindStateDependentData(new State(ddlStates.SelectedValue));
  

                ///////BindSubmitter();
            }
        }

        private void DisplayReportTypeForScope()
        {
            if (this.AccountInfo.Scope == Scope.CMS)
                return;

            if (this.AccountInfo.Scope == Scope.State)
            {
                RemoveReportTypeItems("14");
                return;
            }

            if (this.AccountInfo.Scope == Scope.SubStateRegion)
            {
                RemoveReportTypeItems("1");
                RemoveReportTypeItems("9");
                RemoveReportTypeItems("10");
                RemoveReportTypeItems("11");
                RemoveReportTypeItems("12");
                RemoveReportTypeItems("13");

                RemoveReportTypeItems("14");

                if (this.AccountInfo.IsAdmin == false)
                {
                    RemoveReportTypeItems("3");
                    RemoveReportTypeItems("4");
                    RemoveReportTypeItems("5");
                    RemoveReportTypeItems("6");
                }
                return;
            }

            if (this.AccountInfo.Scope == Scope.Agency)
            {
                RemoveReportTypeItems("1");
                RemoveReportTypeItems("9");
                RemoveReportTypeItems("10");
                RemoveReportTypeItems("11");
                RemoveReportTypeItems("12");
                RemoveReportTypeItems("13");

                RemoveReportTypeItems("14");
                return;
            }
        }

        private void OLD_DisplayReportTypeForScope()
        {
            switch (this.AccountInfo.Scope)
            {
                case Scope.CMS:
                    break;
                case Scope.State:
                    RemoveReportTypeItems("14");
                    break;
                case Scope.SubStateRegion:
                    RemoveReportTypeItems("1");

                    RemoveReportTypeItems("3");
                    RemoveReportTypeItems("4");
                    RemoveReportTypeItems("5");
                    RemoveReportTypeItems("6");

                    RemoveReportTypeItems("9");
                    RemoveReportTypeItems("10");
                    RemoveReportTypeItems("11");
                    RemoveReportTypeItems("12");
                    RemoveReportTypeItems("13");

                    RemoveReportTypeItems("14");
                    break;
                case Scope.Agency:
                    RemoveReportTypeItems("1");
                    RemoveReportTypeItems("9");
                    RemoveReportTypeItems("10");
                    RemoveReportTypeItems("11");
                    RemoveReportTypeItems("12");
                    RemoveReportTypeItems("13");

                    RemoveReportTypeItems("14");
                    break;
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
            ccReportType = (CCReportType)Convert.ToInt16(ReportType.SelectedValue.ToString());
            switch (ccReportType)
            {
                case CCReportType.ContactsByAgency:
                    BindAgency(this.AccountInfo.StateFIPS);
                    break;
                case CCReportType.ContactsByCountyOfCounselorLocation:
                    BindCountyAndOrAgencyForCounselorLocation();
                    break;
                case CCReportType.ContactsByZipCodeOfCounselorLocation:
                    BindZipCodeAndOrAgencyForCounselorLocation();
                    break;
                case CCReportType.ContactsByCountyOfClientResidence:
                    BindCountyAndOrAgencyForClientResidence();
                    break;
                case CCReportType.ContactsByZipcodeOfClientResidence:
                    BindZipCodeAndOrAgencyForClientResidence();
                    break;
                case CCReportType.ContactsBySubStateRegionOnAgency:
                    BindRegionID(5,ddlSubstateRegionsBasedOnAgencyGroupings);
                    break;
                case CCReportType.ContactsBySubStateRegionOnCountiesOfCounselorLocation:
                    BindRegionID(3,ddlSubstateRegionBasedOnCountiesOfCounselorLocations);
                    break;
                case CCReportType.ContactsBySubStateRegionOnZipcodesOfCounselorLocation:
                    BindRegionID(2, ddlSubstateRegionBasedOnZIPCodesOfCounselorLocations);
                    break;
                case CCReportType.ContactsBySubStateRegionOnCountiesOfClientResidence:
                    BindRegionID(1, ddlSubstateRegionBasedonCountiesofClientResidence);
                    break;
                case CCReportType.ContactsBySubStateRegionOnZipcodeOfClientResidence:
                    BindRegionID(4, ddlSubstateRegionBasedOnZIPCodesofClientResidence);
                    break;
                case CCReportType.ContactsByCounselor:
                    ////////BindCounselor();
                    BindStateDependentData(new State(ddlStates.SelectedValue));

                    break;
                case CCReportType.ContactsBySubmitter:

                    BindStateDependentData(new State(ddlStates.SelectedValue));

                   //////// BindSubmitter();
                    break;
            }
        }


        private void BindRegionID(int SubStateRegionType, DropDownList ddl)
        {
            ddl.Items.Clear();

            //clear all other substate dropdown also..
            ddlSubstateRegionsBasedOnAgencyGroupings.Items.Clear();
            ddlSubstateRegionBasedOnCountiesOfCounselorLocations.Items.Clear();
            ddlSubstateRegionBasedOnZIPCodesOfCounselorLocations.Items.Clear();
            ddlSubstateRegionBasedonCountiesofClientResidence.Items.Clear();
            ddlSubstateRegionBasedOnZIPCodesofClientResidence.Items.Clear();


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

                ddlAgency.Items.Clear();
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
        private void BindCountyAndOrAgencyForClientResidence()
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
                BindCountyofClientResidence(false);
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
        private void BindZipCodeAndOrAgencyForClientResidence()
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
                BindZipCodeofClientResidence(false);
            }
        }

        private void BindCountyofCounselorLocation(bool IsSubStateRegionAdmin)
        {
             ddlCountyOfCounselorLocation.Items.Clear();
             AgencyBLL counties = new AgencyBLL();
             IEnumerable<KeyValuePair<string, string>> CountiesForCounselorLocation = null;

             if (IsSubStateRegionAdmin)
                 //find the counties by agency
                 CountiesForCounselorLocation = counties.GetCountyByAgencyIdForReport(Convert.ToInt32(ddlAgency.SelectedValue), FormType.ClientContact);
             else
                 //find the counties by state
                CountiesForCounselorLocation = counties.GetCountyForCounselorLocationByState(ddlStates.SelectedValue, FormType.ClientContact);
           
             if (CountiesForCounselorLocation.Count() > 0)
            {
                ddlCountyOfCounselorLocation.DataSource = CountiesForCounselorLocation;
                ddlCountyOfCounselorLocation.DataTextField = "value";
                ddlCountyOfCounselorLocation.DataValueField = "key";
                ddlCountyOfCounselorLocation.DataBind();
            }
             if (CountiesForCounselorLocation == null || CountiesForCounselorLocation.Count() == 0)
             {
                 btnSubmit.Enabled = false;
                 ddlCountyOfCounselorLocation.Width = Unit.Pixel(470);
                 ddlCountyOfCounselorLocation.Items.Add(new ListItem("No county of counselor location available", "0"));
             }
             else if (ddlCountyOfCounselorLocation.Items.Count > 0)
             {
                 btnSubmit.Enabled = true;
                 ddlCountyOfCounselorLocation.Width = Unit.Pixel(470);
                 ddlCountyOfCounselorLocation.Items.Insert(0, new ListItem("<-- Select county of counselor location -->", "0"));
             }
        }
        private void BindCountyofClientResidence(bool IsSubStateRegionAdmin)
        {
            ddlCountyofClientResidence.Items.Clear();
            AgencyBLL counties = new AgencyBLL();
            IEnumerable<KeyValuePair<string, string>> Counties;

            if (IsSubStateRegionAdmin)
                //find the counties by agency
                Counties = counties.GetCountyOfClientResidenceByAgencyIdForReport(Convert.ToInt32(ddlAgency.SelectedValue));
            else
                //find the counties by state
                Counties = counties.GetCountyForClientResidenceByState(ddlStates.SelectedValue);

            if (Counties != null)
            {
                ddlCountyofClientResidence.DataSource = Counties;
                ddlCountyofClientResidence.DataTextField = "value";
                ddlCountyofClientResidence.DataValueField = "key";
                ddlCountyofClientResidence.DataBind();
            }
            if (Counties == null || Counties.Count() == 0)
            {
                ddlCountyofClientResidence.Width = Unit.Pixel(470);
                ddlCountyofClientResidence.Items.Add(new ListItem("No county of client residence available", "0"));
            }
            else if (ddlCountyofClientResidence.Items.Count > 0)
            {
                ddlCountyofClientResidence.Width = Unit.Pixel(470);
                ddlCountyofClientResidence.Items.Insert(0, new ListItem("<-- Select county of client residence -->", "0"));
            }
        }
        private void BindZipCodeofCounselorLocation(bool IsSubStateRegionAdmin)
        {
            ddlZipCodeOfCounselorLocation.Items.Clear();
            IEnumerable<KeyValuePair<string, string>> ZipCodeForCounselorLocation = null;
            AgencyBLL zipcode = new AgencyBLL();

            if (IsSubStateRegionAdmin)
                //find the counties by agency
                ZipCodeForCounselorLocation = zipcode.GetZipByAgencyIdForReport(Convert.ToInt32(ddlAgency.SelectedValue), FormType.ClientContact);
            else
                //find the counties by state
                ZipCodeForCounselorLocation = zipcode.GetZipCodeForCounselorLocationByState(ddlStates.SelectedValue, FormType.ClientContact);

            if (ZipCodeForCounselorLocation != null)
            {
                ddlZipCodeOfCounselorLocation.DataSource = ZipCodeForCounselorLocation;
                ddlZipCodeOfCounselorLocation.DataTextField = "value";
                ddlZipCodeOfCounselorLocation.DataValueField = "key";
                ddlZipCodeOfCounselorLocation.DataBind();
            }
            if (ZipCodeForCounselorLocation == null || ZipCodeForCounselorLocation.Count() == 0)
            {
                ddlZipCodeOfCounselorLocation.Width = Unit.Pixel(370);
                ddlZipCodeOfCounselorLocation.Items.Add(new ListItem("No zipcode of counselor location available", "0"));
            }
            else if (ddlZipCodeOfCounselorLocation.Items.Count > 0)
            {
                ddlZipCodeOfCounselorLocation.Width = Unit.Pixel(370);
                ddlZipCodeOfCounselorLocation.Items.Insert(0, new ListItem("<-- Select zipcode of counselor location -->", "0"));
            }
        }
        private void BindZipCodeofClientResidence(bool IsSubStateRegionAdmin)
        {
            ddlZIPCodeofClientResidence.Items.Clear();
             AgencyBLL zipcode = new AgencyBLL();
             IEnumerable<KeyValuePair<string, string>> ZipCodeofClientResidence = null;

             if (IsSubStateRegionAdmin)
                 //find the Zip by agency
                 ZipCodeofClientResidence = zipcode.GetZipCodeOfClientResidenceByAgencyIdForReport(Convert.ToInt32(ddlAgency.SelectedValue));
             else
                 //find the Zip by state
                 ZipCodeofClientResidence = zipcode.GetZipCodeForClientResidenceByState(ddlStates.SelectedValue);

             if (ZipCodeofClientResidence != null)
            {
                ddlZIPCodeofClientResidence.DataSource = ZipCodeofClientResidence;
                ddlZIPCodeofClientResidence.DataTextField = "value";
                ddlZIPCodeofClientResidence.DataValueField = "key";
                ddlZIPCodeofClientResidence.DataBind();
            }
             if (ZipCodeofClientResidence == null || ZipCodeofClientResidence.Count() == 0)
            {
                ddlZIPCodeofClientResidence.Width = Unit.Pixel(470);
                ddlZIPCodeofClientResidence.Items.Add(new ListItem("No zipcode of client residence available", "0"));
            }
            else if (ddlZIPCodeofClientResidence.Items.Count > 0)
            {
                ddlZIPCodeofClientResidence.Width = Unit.Pixel(470);
                ddlZIPCodeofClientResidence.Items.Insert(0, new ListItem("<-- Select zipcode of client residence -->", "0"));
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
            ddlCounselor.Items.Clear();

            //Populate Counselors from DB
            IEnumerable<KeyValuePair<int, string>> counselor = Counselors;

            if (counselor != null)
            {
                ddlCounselor.DataSource = counselor;
                ddlCounselor.DataTextField = "value";
                ddlCounselor.DataValueField = "key";
                ddlCounselor.DataBind();
            }
            if (counselor == null || counselor.Count() == 0)
            {
                ddlCounselor.Width = Unit.Pixel(470);
                ddlCounselor.Items.Add(new ListItem("No counselor", "0"));
            }
            else if (ddlCounselor.Items.Count > 0)
            {
                ddlCounselor.Width = Unit.Pixel(470);
                ddlCounselor.Items.Insert(0, new ListItem("<-- Select counselor -->", "0"));
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

        private IEnumerable<KeyValuePair<int, string>> _Counselors { get; set; }
        protected IEnumerable<KeyValuePair<int, string>> Counselors
        {
            get
            {
                if (_Counselors == null)
                {
                    int? AgencyId = null;
                    if (IsPostBack && dropDownListAgencyForCounselor.SelectedIndex != 0)
                        AgencyId = Convert.ToInt32(dropDownListAgencyForCounselor.SelectedValue);

                    _Counselors = GetCounselors(AgencyId);
                }

                return _Counselors;
            }
            set
            {
                _Counselors = value;
            }
        }
        

        protected void dropDownListAgencyForSubmitter_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAgencyDependatData(1);
        }

        protected void dropDownListAgencyForCounselor_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAgencyDependatData(2);
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
            CCFBLL.CCCounselorsFilterCriteria criteria = new CCFBLL.CCCounselorsFilterCriteria();
            criteria.StateFIPS = AccountInfo.IsCMSLevel ? ddlStates.SelectedValue : AccountInfo.StateFIPS;
            criteria.scope = this.AccountInfo.Scope;
            criteria.UserId = this.AccountInfo.UserId;
            criteria.AgencyId = AgencyId;
            return CCLogic.GetClientContactCounselors(criteria);
        }
        private void BindSubmitter()
        {
            ddlSubmitter.Items.Clear();

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
            ccReportType = (CCReportType)Convert.ToInt16(ReportType.SelectedValue.ToString());
            if (ccReportType == CCReportType.ContactsBySubmitter)
            {
                dropDownListAgencyForSubmitter.Items.Clear();
                dropDownListAgencyForSubmitter.DataSource = new List<KeyValuePair<int, string>>(UserBLL.GetAllAgenciesForUser(UserId, Scope, state.Code));
                dropDownListAgencyForSubmitter.DataBind();
                dropDownListAgencyForSubmitter.Items.Insert(0, new ListItem(AGENCY_DROPDOWN_DEFAULT_TEXT, "0"));

                dropDownListAgencyForSubmitter.Enabled = true;

                BindAgencyDependatData(1);
            }
            else if (ccReportType == CCReportType.ContactsByCounselor)
            {
                dropDownListAgencyForCounselor.Items.Clear();
                dropDownListAgencyForCounselor.DataSource = new List<KeyValuePair<int, string>>(UserBLL.GetAllAgenciesForUser(UserId, Scope, state.Code));
                dropDownListAgencyForCounselor.DataBind();
                dropDownListAgencyForCounselor.Items.Insert(0, new ListItem(AGENCY_DROPDOWN_DEFAULT_TEXT, "0"));

                dropDownListAgencyForCounselor.Enabled = true;

                BindAgencyDependatData(2);
            }
        }

        //submitter 1, counselor 2
        protected void BindAgencyDependatData(int submitterOrCounselor)
        {
            if (submitterOrCounselor ==1)
            {
                BindSubmitter();
            }
            else if (submitterOrCounselor ==2)
            {
                BindCounselor();
            }

        }

        protected void DisplayUICriteriaByReportType()
        {
            switch(ReportType.SelectedValue)
            {
                //Contacts By State
                case "1":
                    DisplayTR(true, true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false);
                    break;
                //Contacts By Agency
                case "2":
                    DisplayTR(true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false);
                    break;
                //Contacts By County of counselor location
                case "3":
                    DisplayTR(true, true, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false);
                    break;
                //Contacts By Zip code of counselor location
                case "4":
                    DisplayTR(true, true, false, true, false, true, false, false, false, false, false, false, false, false, false, false, false);
                    break;
                //Contacts By County of client residence
                case "5":
                    DisplayTR(true, true, false, true, false, false, true, false, false, false, false, false, false, false, false, false, false);
                    break;
                //Contacts By Zip code of client residence
                case "6":
                    DisplayTR(true, true, false, true, false, false, false, true, false, false, false, false, false, false, false, false,false);
                    break;
                //Contacts By Counselor
                case "7":
                    DisplayTR(true, true, false, true, false, false, false, false, false, false, false, false, false, true, false, false, true);
                    break;
                //Contacts By Submitter
                case "8":
                    DisplayTR(true, true, false, true, false, false, false, false, false, false, false, false, false, false, true, true,false);
                    break;
                //Contacts By SubStateRegion on agency
                case "9":
                    DisplayTR(true, true, false, true, false, false, false, false, true, false, false, false, false, false, false, false, false);
                    break;
                //Contacts By SubStateRegion on counties of counselor location
                case "10":
                    DisplayTR(true, true, false, true, false, false, false, false, false, true, false, false, false, false, false, false, false);
                    break;
                //Contacts By SubStateRegion on zip codes of counselor location
                case "11":
                    DisplayTR(true, true, false, true, false, false, false, false, false, false, true, false, false, false, false, false, false);
                    break;
                //Contacts By SubStateRegion on counties of client residence
                case "12":
                    DisplayTR(true, true, false, true, false, false, false, false, false, false, false, true, false, false, false, false, false);
                    break;
                //Contacts By SubStateRegion on zip code of client residence
                case "13":
                    DisplayTR(true, true, false, true, false, false, false, false, false, false, false, false, true, false, false, false, false);
                    break;
                    //Contacts By National
                case "14":
                    DisplayTR(false, true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false);
                    break;
                default:
                    DisplayTR(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                    break;
            }
        }
        private void DisplayTR(bool bState, bool bDateOfContact, bool bAgency, bool bSubmit,
            bool btrCountyOfCounselorLocation, bool btrZipCodeCounselorLocation,
            bool bCountyofClientResidence, bool bZIPCodeofClientResidence,
            bool bSubstateRegionsBasedOnAgencyGroupings, bool bSubstateRegionBasedOnCountiesOfCounselorLocations,
            bool bSubstateRegionBasedOnZIPCodesOfCounselorLocations,
        bool bSubstateRegionBasedonCountiesofClientResidence, bool bSubstateRegionBasedOnZIPCodesofClientResidence,
        bool bCounselor, bool bSubmitter, bool bAgencyForSubmitter, bool bAgencyForCounselor)
        {
            trState.Visible = bState;
            DateOfContact.Visible = bDateOfContact;
            trAgency.Visible = bAgency;
            Submit.Visible = bSubmit;
            trCountyOfCounselorLocation.Visible = btrCountyOfCounselorLocation;
            trZipCodeCounselorLocation.Visible = btrZipCodeCounselorLocation;
            CountyofClientResidence.Visible = bCountyofClientResidence;
            ZIPCodeofClientResidence.Visible = bZIPCodeofClientResidence;
            Counselor.Visible = bCounselor;
            Submitter.Visible = bSubmitter;
            SubstateRegionsBasedOnAgencyGroupings.Visible = bSubstateRegionsBasedOnAgencyGroupings;
            SubstateRegionBasedOnCountiesOfCounselorLocations.Visible = bSubstateRegionBasedOnCountiesOfCounselorLocations;
            SubstateRegionBasedOnZIPCodesOfCounselorLocations.Visible = bSubstateRegionBasedOnZIPCodesOfCounselorLocations;
            SubstateRegionBasedonCountiesofClientResidence.Visible = bSubstateRegionBasedonCountiesofClientResidence;
            SubstateRegionBasedOnZIPCodesofClientResidence.Visible = bSubstateRegionBasedOnZIPCodesofClientResidence;
            trAgencySubmmiter.Visible = bAgencyForSubmitter;
            trAgencyCounselor.Visible = bAgencyForCounselor;

        }

        public enum CCReportType
        {
            ContactsByState = 1,
            ContactsByAgency = 2,
            ContactsByCountyOfCounselorLocation = 3,
            ContactsByZipCodeOfCounselorLocation = 4,
            ContactsByCountyOfClientResidence = 5,
            ContactsByZipcodeOfClientResidence = 6,
            ContactsByCounselor = 7,
            ContactsBySubmitter = 8,
            ContactsBySubStateRegionOnAgency = 9,
            ContactsBySubStateRegionOnCountiesOfCounselorLocation = 10,
            ContactsBySubStateRegionOnZipcodesOfCounselorLocation = 11,
            ContactsBySubStateRegionOnCountiesOfClientResidence = 12,
            ContactsBySubStateRegionOnZipcodeOfClientResidence = 13,
            ContactsByNational = 14
        }
        public enum CCLocationType
        {
            ZIPCodeOfClientRes=4,
            CountyOfCounselorLocation=3, 
            ZIPCodeOfCounselorocation=2, 
            CountyOFClientRes=1

        }

        

        protected void ddlAgencyForSubstateAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if the user is SubstateRegionADMIN show him the agency dropdown...
            if (AccountInfo.Scope == Scope.SubStateRegion && AccountInfo.IsAdmin)
            {
                if ((CCReportType)Convert.ToInt16(ReportType.SelectedValue.ToString()) == CCReportType.ContactsByCountyOfCounselorLocation)
                    BindCountyofCounselorLocation(true);
                else if ((CCReportType)Convert.ToInt16(ReportType.SelectedValue.ToString()) == CCReportType.ContactsByCountyOfClientResidence)
                    BindCountyofClientResidence(true);
                else if ((CCReportType)Convert.ToInt16(ReportType.SelectedValue.ToString()) == CCReportType.ContactsByZipCodeOfCounselorLocation)
                    BindZipCodeofCounselorLocation(true);
                else if ((CCReportType)Convert.ToInt16(ReportType.SelectedValue.ToString()) == CCReportType.ContactsByZipcodeOfClientResidence)
                    BindZipCodeofClientResidence(true);

            }
        }

        


        protected void lnkPamReports_Click(object sender, EventArgs e)
        {
            RouteController.RouteTo(RouteController.NprPAMReports());
        }

    }
    
}
