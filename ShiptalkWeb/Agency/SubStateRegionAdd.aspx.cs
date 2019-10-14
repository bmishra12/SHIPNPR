using System;
using System.Collections.Generic;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;

namespace ShiptalkWeb.Agency
{
    public partial class SubStateRegionAdd : Page, IRouteDataPage, IAuthorize
    {
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";
        private const string ScopeKey = "Scope";

        private AgencyBLL _logic;

        #region Properties

        public IEnumerable<KeyValuePair<int, string>> Agencies { get; set; }

        public State DefaultState { get { return (State)ViewState[DefaultStateKey]; } set { ViewState[DefaultStateKey] = value; } }

        public bool IsAdmin { get { return (bool)ViewState[IsAdminKey]; } set { ViewState[IsAdminKey] = value; } }

        public AgencyBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new AgencyBLL();

                return _logic;
            }
        }

        public Scope Scope { get { return (Scope)ViewState[ScopeKey]; } set { ViewState[ScopeKey] = value; } }

        public IEnumerable<KeyValuePair<string, string>> States { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Binds the dependent data for the form.
        /// </summary>
        protected void BindDependentData()
        {
            Agencies = Logic.GetAgencies(DefaultState.Code);
            States = Logic.GetStates();
        }

        /// <summary>
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
        /// Called when page is loaded.
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

        protected void dropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefaultState = new State(((DropDownList)sender).SelectedValue);
            BindDependentData();
            formViewSubStateRegion.DataBind();
            listViewSubStateRegions.DataBind();
        }

        protected void validatorRegionAgencies_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !string.IsNullOrEmpty(((HiddenField)formViewSubStateRegion.FindControl("hiddenRegionAgencies")).Value);
        }

        protected void dataSourceSubStateRegion_Inserted(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            var viewData = (AddSubStateRegionViewData)e.Instance;
            viewData.SetIsActive(true);
            viewData.SetCreated(AccountInfo.UserId);
            viewData.SetLastUpdated(AccountInfo.UserId);
            RouteController.RouteTo(RouteController.AgencyRegionSuccess(Logic.AddSubStateRegion(viewData)));
        }

        protected void dataSourceSubStateRegion_Inserting(object sender, ObjectContainerDataSourceInsertingEventArgs e)
        {
            e.NewValues["State"] = DefaultState;
        }

        protected void dataSourceSubStateRegions_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            var subStateRegions = new List<SearchSubStateRegionsViewData>(Logic.SearchSubStateRegions(DefaultState));

            dataSourceSubStateRegions.DataSource = subStateRegions;
            panelNoResults.Visible = (subStateRegions.Count == 0);
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            Validate();
            formViewSubStateRegion.InsertItem(true);
        }

        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        #region Implementation of IAuthorize

        public bool IsAuthorized()
        {
            //Only CMS and State Admins are able to add a new sub-state region.
            return ((AccountInfo.Scope == Scope.CMS || AccountInfo.Scope == Scope.State) && AccountInfo.IsAdmin);
        }

        #endregion
    }
}
