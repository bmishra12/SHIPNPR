using System;
using System.Collections.Generic;
using System.Web.Routing;
using System.Web.UI;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;
using System.Linq;

namespace ShiptalkWeb.Agency
{
    public partial class AgencySearch : Page, IRouteDataPage
    {
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";
        private const string ScopeKey = "Scope";
        private const string SearchTypeKey = "SearchType";

        private AgencyBLL _logic;

        #region Properties

        public State DefaultState { get { return (State)ViewState[DefaultStateKey]; } set { ViewState[DefaultStateKey] = value; } }

        public bool IsAdmin { get { return (bool)ViewState[IsAdminKey]; } set { ViewState[IsAdminKey] = value; } }

        public Scope Scope { get { return (Scope)ViewState[ScopeKey]; } set { ViewState[ScopeKey] = value; } }

        public AgencyBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new AgencyBLL();

                return _logic;
            }
        }

        private SearchType SearchType { get { return (SearchType)ViewState[SearchTypeKey]; } set { ViewState[SearchTypeKey] = value; } }

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

                string StateFIPSForCMS = State.GetStateFIPSForCMS();

                IEnumerable<KeyValuePair<string, string>> StatesData
                    = State.GetStatesWithFIPSKey().Where(p => p.Key != StateFIPSForCMS);

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
                        where stFIPS.Key == cmsStFIPS
                        select stFIPS
                        );
                }
                dropDownListState.DataSource = StatesForUser;

            }
            else
            {
                dropDownListState.DataSource = Logic.GetStates();
            }

            dropDownListState.DataBind();
            
            aAddAgency.DataBind();
            aAddSubStateRegion.DataBind();
        }

        // <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void OnViewInitialized()
        {
            DefaultState = new State(AccountInfo.StateFIPS);
            IsAdmin = AccountInfo.IsAdmin;
            Scope = (Scope)AccountInfo.ScopeId;
            BindDependentData();
        }

        /// <summary>
        /// Called when the page is loaded.
        /// </summary>
        protected void OnViewLoaded()
        {
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) OnViewInitialized();

            OnViewLoaded();
        }

        protected void buttonSearch_Click(object sender, EventArgs e)
        {
            SearchType = SearchType.Keyword;
            pager.SetPageProperties(0, pager.PageSize, false);
            listViewAgencies.DataBind();
        }

        protected void linkButtonListAllAgencies_Click(object sender, EventArgs e)
        {
            SearchType = SearchType.ListAll;
            pager.SetPageProperties(0, pager.PageSize, false);
            listViewAgencies.DataBind();
        }

        protected void dataSourceAgencies_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            if (!IsPostBack) return;

            IList<SearchAgenciesViewData> agencies = null;

            switch (SearchType)
            {
                case SearchType.Keyword:
                    {
                        agencies = new List<SearchAgenciesViewData>(Logic.SearchAgencies(AccountInfo.UserId,new State(dropDownListState.SelectedValue), AccountInfo.Scope, textBoxSearchKeywords.Text.Replace("\"", string.Empty).Replace(",", string.Empty) ));
                        dataSourceAgencies.DataSource = agencies;

                        break;
                    }
                case SearchType.ListAll:
                    {
                        agencies = new List<SearchAgenciesViewData>(Logic.ListAllAgencies(AccountInfo.UserId, new State(dropDownListState.SelectedValue), Scope));
                        dataSourceAgencies.DataSource = agencies;

                        break;
                    }
            }

            panelNoResults.Visible = (agencies.Count == 0);
        }

        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }
        
        public RouteData RouteData { get; set; }

        #endregion

        
    }

    internal enum SearchType
    {
        Keyword,
        ListAll
    }
}
