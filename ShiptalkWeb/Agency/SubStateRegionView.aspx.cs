using System;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;

namespace ShiptalkWeb.Agency
{
    public partial class SubStateRegionView : Page, IRouteDataPage
    {
        private const string IdKey = "Id";
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";
        private const string ScopeKey = "Scope";

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

        private ViewSubStateRegionViewData _viewData;

        private ViewSubStateRegionViewData ViewData
        {
            get
            {
                if (_viewData == null)
                    _viewData = Logic.GetViewSubStateRegion(Id.GetValueOrDefault(0));

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
            Scope = (Scope)AccountInfo.ScopeId;
            panelSuccess.DataBind();
        }

        /// <summary>
        /// Called when the page is loaded.
        /// </summary>
        protected void OnViewLoaded()
        {
        }

        protected bool DisplaySuccessMessage()
        {
            return (RouteData != null && ((AuthorizeRoute) RouteData.Route).Url.ToLower().Contains("success"));
        }

        protected string GetSuccessMessage()
        {
            var message = string.Empty;

            if (HttpContext.Current.Request.UrlReferrer == null) return message;

            var localPath = HttpContext.Current.Request.UrlReferrer.LocalPath.ToLower();

            if (localPath.Contains("/agency/region/edit"))
                message = "This Sub-State Region has successfully been edited.";
            else if (localPath.Contains("/agency/region/add"))
                message = "This Sub-State Region has been successfully added.";

            return message;
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) OnViewInitialized();

            OnViewLoaded();
        }

        protected void buttonDeleteSubStateRegion_Click(object sender, EventArgs e)
        {
            Logic.DeleteSubStateRegion(Id.GetValueOrDefault(0));
            RouteController.RouteTo(RouteController.AgencySearch());
        }

        protected void buttonEditSubStateRegion_Click(object sender, EventArgs e)
        {
            RouteController.RouteTo(RouteController.AgencyRegionEdit(Id.GetValueOrDefault(0)));
        }

        protected void dataSourceViewSubStateRegion_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceViewSubStateRegion.DataSource = ViewData;
        }

        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion
    }
}
