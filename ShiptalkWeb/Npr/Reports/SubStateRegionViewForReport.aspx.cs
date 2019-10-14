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

namespace NPRRebuild.ShiptalkWeb.NPRReports
{
    public partial class SubStateRegionViewForReport : Page, IRouteDataPage, IAuthorize
    {
        private const string IdKey = "Id";
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";
        private const string ScopeKey = "Scope";


        private ReportSubStateRegionBLL _logic;
        private ViewSubStateRegionForReportViewData _viewData;

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
        public Scope Scope { get { return (Scope)ViewState[ScopeKey]; } set { ViewState[ScopeKey] = value; } }

        public ReportSubStateRegionBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new ReportSubStateRegionBLL();

                return _logic;
            }
        }

        private ViewSubStateRegionForReportViewData ViewData
        {
            get
            {
                if (_viewData == null)
                    _viewData = Logic.GetViewSubStateRegionForReport(Id.GetValueOrDefault(0));

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
            if (HttpContext.Current.Request.UrlReferrer == null) return false;

            var localPath = HttpContext.Current.Request.UrlReferrer.LocalPath.ToLower();

            return (localPath.Contains("reports/substate/edit")
                    || localPath.Contains("reports/substate/add")
                        );
        }

        protected string GetSuccessMessage()
        {
            var message = string.Empty;

            if (HttpContext.Current.Request.UrlReferrer == null) return message;

            var localPath = HttpContext.Current.Request.UrlReferrer.LocalPath.ToLower();

            if (localPath.Contains("reports/substate/edit"))
                message = "This Substate for Report has successfully been edited.";
            else if (localPath.Contains("reports/substate/add"))
                message = "This Substate for Report has been successfully added.";

            return message;
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) OnViewInitialized(); 

            OnViewLoaded();
        }

        protected void dataSourceViewSubStateRegion_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceViewSubStateRegion.DataSource = ViewData;
        }

        protected void buttonEditSubStateRegion_Click(object sender, EventArgs e)
        {
            RouteController.RouteTo(RouteController.ReportSubstateEdit(Id.Value));
        }

        protected void buttonDeleteSubStateRegion_Click(object sender, EventArgs e)
        {
           Logic.DeleteSubStateRegionForReport(Id.Value);
            RouteController.RouteTo(RouteController.ReportSubstateSearch());
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
            return (AccountInfo.Scope == Scope.CMS || AccountInfo.StateFIPS == ViewData.State.Code);
        }

        #endregion
    }
}
