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
    public partial class SubStateRegionEdit : Page, IRouteDataPage, IAuthorize
    {
        private const string IdKey = "Id";
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";
        private const string ScopeKey = "Scope";

        private AgencyBLL _logic;

        #region Properties

        public IEnumerable<KeyValuePair<int, string>> Agencies { get; set; }

        public State DefaultState { get { return (State)ViewState[DefaultStateKey]; } set { ViewState[DefaultStateKey] = value; } }

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

        public bool IsAdmin { get { return (bool)ViewState[IsAdminKey]; } set { ViewState[IsAdminKey] = value; } }

        public AgencyBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new AgencyBLL();

                return _logic;
            }
        }

        private EditSubStateRegionViewData _viewData;

        private EditSubStateRegionViewData ViewData
        {
            get
            {
                if (_viewData == null)
                    _viewData = Logic.GetEditSubStateRegion(Id.GetValueOrDefault(0));

                return _viewData;
            }
        }
        
        public Scope Scope { get { return (Scope)ViewState[ScopeKey]; } set { ViewState[ScopeKey] = value; } }

        #endregion

        #region Methods

        /// <summary>
        /// Binds the dependent data for the form.
        /// </summary>
        protected void BindDependentData()
        {
            Agencies = Logic.GetAgencies(ViewData.State.Code);
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

        protected void dataSourceSubStateRegion_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceSubStateRegion.DataSource = ViewData;
        }

        protected void dataSourceSubStateRegion_Updated(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            var viewData = (EditSubStateRegionViewData)e.Instance;
            viewData.SetIsActive(viewData.IsActive);
            viewData.SetCreated(AccountInfo.UserId);
            viewData.SetLastUpdated(AccountInfo.UserId);
            Logic.UpdateSubStateRegion(viewData);
            RouteController.RouteTo(RouteController.AgencyRegionSuccess(Id.GetValueOrDefault(0)));
        }

        protected void validatorRegionAgencies_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !string.IsNullOrEmpty(((HiddenField)formViewSubStateRegion.FindControl("hiddenRegionAgencies")).Value);
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            Validate();
            formViewSubStateRegion.UpdateItem(true);
        }

        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        #region Implementation of IAuthorize

        public bool IsAuthorized()
        {
            if ((AccountInfo.Scope == Scope.CMS || AccountInfo.Scope == Scope.State) && AccountInfo.IsAdmin)
                return true;

            if (AccountInfo.Scope == Scope.SubStateRegion)
            {
                var accessProfiles = Logic.GetSubStateRegionalAccessProfiles(AccountInfo.UserId);

                if (accessProfiles == null)
                    return false;

                foreach (var accessProfile in accessProfiles)
                    if (accessProfile.RegionId == Id.GetValueOrDefault(0) && accessProfile.IsAdmin)
                        return true;
            }

            return false;
        }

        #endregion
    }
}
