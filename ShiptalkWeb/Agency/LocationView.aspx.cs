using System;
using System.Security;
using System.Web.Routing;
using System.Web.UI;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;

namespace ShiptalkWeb.Agency
{
    public partial class LocationView : Page, IRouteDataPage, IAuthorize
    {
        private const string AgencyIdKey = "AgencyId";
        private const string IdKey = "Id";
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";

        private AgencyBLL _logic;

        #region Properties

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

        public int? AgencyId { get { return (int)ViewState[AgencyIdKey]; } set { ViewState[AgencyIdKey] = value; } }

        public bool IsAdmin { get { return (bool)ViewState[IsAdminKey]; } set { ViewState[IsAdminKey] = value; } }

        public AgencyBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new AgencyBLL();

                return _logic;
            }
        }

        private ViewAgencyLocationViewData _viewData;

        private ViewAgencyLocationViewData ViewData
        {
            get
            {
                if (_viewData == null)
                    _viewData = Logic.GetViewAgencyLocation(Id.Value);

                return _viewData;
            }
        }

        #endregion

        #region Methods

        // <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void OnViewInitialized()
        {
            DefaultState = new State(AccountInfo.StateFIPS);
            IsAdmin = AccountInfo.IsAdmin;
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

        protected void dataSourceViewAgencyLocation_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            AgencyId = ViewData.AgencyId;
            dataSourceViewAgencyLocation.DataSource = ViewData;
        }

        protected void buttonEditAgencyLocation_Click(object sender, EventArgs e)
        {
            RouteController.RouteTo(RouteController.AgencyLocationEdit(Id.Value));
        }

        protected void buttonDeleteAgencyLocation_Click(object sender, EventArgs e)
        {
            Logic.DeleteAgencyLocation(Id.Value);
            RouteController.RouteTo(RouteController.AgencyView(AgencyId.Value));
        }

        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        #region Implementation of IAuthorize

        public bool IsAuthorized()
        {
             return ( (AccountInfo.Scope == Scope.CMS || AccountInfo.Scope == Scope.CMSRegional) 
                 || AccountInfo.StateFIPS == ViewData.PhysicalState.Code);
        }

        #endregion
    }
}
