using System;
using System.Security;
using System.Web.Routing;
using System.Web.UI;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;
using System.Web;

namespace ShiptalkWeb.Agency
{
    public partial class AgencyView : Page, IRouteDataPage, IAuthorize
    {
        private const string IdKey = "Id";
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";

        private AgencyBLL _logic;
        private ViewAgencyViewData _viewData;

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

        private ViewAgencyViewData ViewData
        {
            get
            {
                if (_viewData == null)
                    _viewData = Logic.GetViewAgency(Id.GetValueOrDefault(0), true);

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
            if (HttpContext.Current.Request.UrlReferrer == null) return false;
            
            var localPath = HttpContext.Current.Request.UrlReferrer.LocalPath.ToLower();

            return (localPath.Contains("/agency/edit")
                    || localPath.Contains("/agency/register")
                    || localPath.Contains("agency/location/add")
                    || localPath.Contains("agency/location/edit"));
        }

        protected string GetSuccessMessage()
        {
            var message = string.Empty;

            if (HttpContext.Current.Request.UrlReferrer == null) return message;

            var localPath = HttpContext.Current.Request.UrlReferrer.LocalPath.ToLower();

            if (localPath.Contains("/agency/edit"))
                message = "This Agency has successfully been edited.";
            else if (localPath.Contains("/agency/register"))
                message = "This Agency has been successfully added.";
            else if (localPath.Contains("agency/location/add"))
                message = "The Location has been successfully added to this Agency.";
            else if (localPath.Contains("agency/location/edit"))
                message = "The Location has been successfully edited for this Agency.";

            return message;
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) OnViewInitialized(); 

            OnViewLoaded();
        }

        protected void dataSourceViewAgency_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceViewAgency.DataSource = ViewData;
        }

        protected void buttonEditAgency_Click(object sender, EventArgs e)
        {
            RouteController.RouteTo(RouteController.AgencyEdit(Id.Value));
        }

        protected void buttonDeleteAgency_Click(object sender, EventArgs e)
        {
            Logic.DeleteAgency(Id.Value);
            RouteController.RouteTo(RouteController.AgencySearch());
        }

        protected void buttonAddLocation_Click(object sender, EventArgs e)
        {
            RouteController.RouteTo(RouteController.AgencyLocationAdd(Id.Value));
        }

        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }
        
        public RouteData RouteData { get; set; }

        #endregion

        #region Implementation of IAuthorize

        public bool IsAuthorized()
        {
            return ((AccountInfo.Scope == Scope.CMS || AccountInfo.Scope == Scope.CMSRegional)
                        || AccountInfo.StateFIPS == ViewData.State.Code);

        }

        #endregion
    }
}
