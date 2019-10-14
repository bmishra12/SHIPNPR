using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;
using ShiptalkWeb;


namespace NPRRebuild.ShiptalkWeb.PAMF
{
    public partial class PAMFSearch : Page, IRouteDataPage, IAuthorize
    {
        private const string ActiveAgenciesKey = "ActiveAgencies";
        private const string ActiveAgencyDescriptorsKey = "ActiveAgencyDescriptors";
        private const string PamfSearchTypeKey = "PamfSearchType";
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";
        private const string ScopeKey = "Scope";
        private const string UserIdKey = "UserId";


        protected string AGENCY_DROPDOWN_DEFAULT_TEXT = "-- All of my agencies --";

        private AgencyBLL _agencyLogic;
        private PamBLL _logic;
        private CCFBLL _ccLogic;

        #region Properties


        protected SearchType PamfSearchType { get { return (SearchType)ViewState[PamfSearchTypeKey]; } set { ViewState[PamfSearchTypeKey] = value; } }

        protected State DefaultState { get { return new State(((IRouteDataPage)this).AccountInfo.StateFIPS); } }

        protected bool IsCMSAdmin { get { return this.AccountInfo.IsAdmin && this.AccountInfo.IsCMSScope; } }

        protected bool ISCMSLevel { get { return this.AccountInfo.IsCMSScope; } }


        protected Scope Scope { get { return this.AccountInfo.Scope; } }

        protected int UserId { get { return this.AccountInfo.UserId; }  }

        protected AgencyBLL AgencyLogic
        {
            get
            {
                if (_agencyLogic == null) _agencyLogic = new AgencyBLL();

                return _agencyLogic;
            }
        }

        protected PamBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new PamBLL();

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

        #region Search Form Dependencies
        protected IEnumerable<KeyValuePair<string, string>> States { get; set; }

        #endregion

        #region Search Form Properties

        protected int? PresenterId
        {
            get
            {
                return (Convert.ToInt32(dropDownlistCounselor.SelectedValue) > 0) ? (int?)Convert.ToInt32(dropDownlistCounselor.SelectedValue) : null;
            }
        }

        protected DateTime? FromContactDate
        {
            get
            {
                return (!string.IsNullOrEmpty(textBoxFromDateOfContact.Text)) ? (DateTime?)DateTime.Parse(textBoxFromDateOfContact.Text) : null;
            }
        }


        protected State State
        {
            get
            {
                return new State(dropDownListState.SelectedValue);
            }
        }

        protected int? SubmitterId
        {
            get
            {
                return (Convert.ToInt32(dropDownlistSubmitter.SelectedValue) > 0) ? (int?)Convert.ToInt32(dropDownlistSubmitter.SelectedValue) : null;
            }
        }

        protected DateTime? ToContactDate
        {
            get
            {
                return (!string.IsNullOrEmpty(textBoxToDateOfContact.Text)) ? (DateTime?)DateTime.Parse(textBoxToDateOfContact.Text) : null;
            }
        }

        protected int? SelectedAgencyId
        {
            get
            {
                return dropDownListAgency.SelectedIndex != 0 ? Convert.ToInt32(dropDownListAgency.SelectedValue) : (int?)null;
            }
        }
        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Binds the dependent data for the form.
        /// </summary>
        protected void BindDependentData()
        {



            //if user is Cms Regional user populate only his states
            if (AccountInfo.Scope == Scope.CMSRegional)
            {

                IEnumerable<KeyValuePair<string, string>> StatesData = AgencyLogic.GetStates();

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
                States = StatesForUser;

            }
            else
            {
                States = AgencyLogic.GetStates();
            }

            //show the addPam link
            aAddPam.DataBind();

            //check the correct authorization
            if (AccountInfo.IsStateAdmin
                    || (AccountInfo.IsCMSScope && AccountInfo.IsAdmin)
                || AccountInfo.IsShipDirector)
            {
                aAddSpecialField.Visible = true;
                aAddSpecialField.DataBind();
            }

            panelSearchCriteria.DataBind();
            if(Scope.IsLowerOrEqualTo(Scope.State)) BindStateDependentData(new State(dropDownListState.SelectedValue));
        }

        // <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void OnViewInitialized()
        {
            PamfSearchType = SearchType.Recent;

            BindDependentData();
        }

        /// <summary>
        /// Called when the page is loaded.
        /// </summary>
        protected void OnViewLoaded()
        {
        }

        private void PopulateCounselorDropDown()
        {
            dropDownlistCounselor.DataSource = Presenters;
            dropDownlistCounselor.DataTextField = "Value";
            dropDownlistCounselor.DataValueField = "Key";
            dropDownlistCounselor.DataBind();
            dropDownlistCounselor.Items.Insert(0, new ListItem("-- Select a Presenter --", "0"));
        }

        private void PopulateSubmitterDropDown()
        {
            dropDownlistSubmitter.DataSource = Submitters;
            dropDownlistSubmitter.DataTextField = "Value";
            dropDownlistSubmitter.DataValueField = "Key";
            dropDownlistSubmitter.DataBind();
            dropDownlistSubmitter.Items.Insert(0, new ListItem("-- Select a Data Submitter --", "0"));
        }



        private IEnumerable<KeyValuePair<int, string>> GetPresenters(int? AgencyId)
        {
            //Populate presenters from DB
            PamBLL.PamPresenterFilterCriteria criteria = new PamBLL.PamPresenterFilterCriteria();
            criteria.StateFIPS = AccountInfo.IsCMSLevel ? State.GetCode(dropDownListState.SelectedValue) : AccountInfo.StateFIPS;
            criteria.scope = this.AccountInfo.Scope;
            criteria.UserId = this.AccountInfo.UserId;
            criteria.AgencyId = AgencyId;
            return Logic.GetPamPresenters(criteria);
        }

        private IEnumerable<KeyValuePair<int, string>> GetSubmitters(int? AgencyId)
        {
            //Populate Submitters from DB
            CCFBLL.CCSubmittersFilterCriteria criteria = new CCFBLL.CCSubmittersFilterCriteria();
            criteria.StateFIPS = AccountInfo.IsCMSLevel ? State.GetCode(dropDownListState.SelectedValue) : AccountInfo.StateFIPS;
            criteria.scope = this.AccountInfo.Scope;
            criteria.UserId = this.AccountInfo.UserId;
           criteria.AgencyId = AgencyId;
            return CCLogic.GetClientContactSubmitters(criteria);
        }

        private IEnumerable<KeyValuePair<int, string>> _Presenters { get; set; }
        protected IEnumerable<KeyValuePair<int, string>> Presenters
        {
            get
            {
                if (_Presenters == null)
                {
                    int? AgencyId = null;
                    if (IsPostBack && dropDownListAgency.SelectedIndex != 0)
                        AgencyId =  Convert.ToInt32(dropDownListAgency.SelectedValue);

                    _Presenters = GetPresenters(AgencyId);
                }

                return _Presenters;
            }
            set
            {
                _Presenters = value;
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
                    if (IsPostBack && dropDownListAgency.SelectedIndex != 0)
                        AgencyId = Convert.ToInt32(dropDownListAgency.SelectedValue);

                    _Submitters = GetSubmitters(AgencyId);
                }

                return _Submitters;
            }
            set
            {
                _Submitters = value;
            }
        }


        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) OnViewInitialized();

            OnViewLoaded();
        }

        protected void dropDownlistCounselor_DataBound(object sender, EventArgs e)
        {
        }

        protected void dropDownlistSubmitter_DataBound(object sender, EventArgs e)
        {

        }

        protected void linkButtonListRecentPams_Click(object sender, EventArgs e)
        {
            PamfSearchType = SearchType.Recent;
            listViewPams.DataBind();
        }

        protected void buttonSearch_Click(object sender, EventArgs e)
        {
            PamfSearchType = SearchType.Keyword;
            pager.SetPageProperties(0, pager.PageSize, false);

            listViewPams.DataBind();
        }

        protected void dataSourcePublicMediaEvent_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            IList<SearchPublicMediaEventViewData> pams = null;

            switch (PamfSearchType)
            {
                case SearchType.Keyword:
                    {
                        pams = new List<SearchPublicMediaEventViewData>(Logic.SearchPams
                                                                                     (
                                                                                     new PamBLL.PamSearchCriteria
                                                                                         {
                                                                                             UserId = AccountInfo.UserId,
                                                                                             scope = AccountInfo.Scope,
                                                                                             AgencyId = SelectedAgencyId,
                                                                                             StateFIPS = State.Code,
                                                                                             PresenterId = PresenterId,
                                                                                             SubmitterId = SubmitterId,
                                                                                             FromDate = FromContactDate,
                                                                                             ToDate = ToContactDate,
                                                                                         }
                                                                                    )
                                                                             );
                        dataSourcePams.DataSource = pams;

                        break;
                    }
                case SearchType.Recent:
                    {
                        pams = new List<SearchPublicMediaEventViewData>(Logic.GetRecentPams(UserId));
                        dataSourcePams.DataSource = pams;

                        break;
                    }
            }

            panelNoResults.Visible = (pams.Count == 0);
        }

        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        #region Sub Types

        protected enum SearchType
        {
            Keyword,
            Recent
        }

        #endregion


        protected void dropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Update the agency list based on the users scope and clear the counselors and submitters lists.
            BindStateDependentData(new State(dropDownListState.SelectedValue));
        }

        protected void dropDownListAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAgencyDependatData(Convert.ToInt32(dropDownListAgency.SelectedValue));
        }

        protected void BindStateDependentData(State state)
        {
            dropDownListAgency.Items.Clear();
            dropDownListAgency.DataSource = new List<KeyValuePair<int, string>>(UserBLL.GetAllAgenciesForUser(UserId, Scope, state.Code));
            dropDownListAgency.DataBind();
            dropDownListAgency.Items.Insert(0, new ListItem(AGENCY_DROPDOWN_DEFAULT_TEXT, "0"));

            dropDownListAgency.Enabled = true;
            
            BindAgencyDependatData(null);
        }

        protected void BindAgencyDependatData(int? agencyId)
        {
            dropDownlistCounselor.Items.Clear();
            dropDownlistSubmitter.Items.Clear();
            PopulateCounselorDropDown();
            PopulateSubmitterDropDown();
            dropDownlistCounselor.Enabled = true;
            dropDownlistSubmitter.Enabled = true;
        }



        #region IAuthorize Members

        public bool IsAuthorized()
        {
            return true;  //search page is viewable by every body.
        }

        #endregion
    }


}
