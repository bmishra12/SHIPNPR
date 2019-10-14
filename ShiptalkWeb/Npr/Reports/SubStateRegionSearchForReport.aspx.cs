using System;
using System.Collections.Generic;
using System.Web.Routing;
using System.Web.UI;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;
using System.Web.UI.WebControls;

namespace NPRRebuild.ShiptalkWeb.NPRReports
{
    public partial class SubStateRegionSearchForReport : Page, IRouteDataPage
    {
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";
        private const string ScopeKey = "Scope";


        #region Properties



        public State DefaultState { get { return (State)ViewState[DefaultStateKey]; } set { ViewState[DefaultStateKey] = value; } }

        public bool IsAdmin { get { return (bool)ViewState[IsAdminKey]; } set { ViewState[IsAdminKey] = value; } }

        public Scope Scope { get { return (Scope)ViewState[ScopeKey]; } set { ViewState[ScopeKey] = value; } }


        private ReportSubStateRegionBLL _Logic;

        public ReportSubStateRegionBLL Logic
        {
            get
            {
                if (_Logic == null) _Logic = new ReportSubStateRegionBLL();

                return _Logic;
            }
        }



        #endregion

        #region Methods

        /// <summary>
        /// Binds the dependent data for the form.
        /// </summary>
        protected void BindDependentData()
        {


            aAddReport.DataBind();


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




        protected void listViewSubStates_Sorting(object sender, ListViewSortEventArgs e)
        {
            // Check the sort direction to set the image URL accordingly.
            string imgUrl;
            if (e.SortDirection == SortDirection.Ascending)
                imgUrl = "~/images/ascending.gif";
            else
                imgUrl = "~/images/descending.gif";

            // Check which field is being sorted
            // to set the visibility of the image controls.
            Image sortImage1 = (Image)listViewSubStates.FindControl("SortImage1");
            Image sortImage2 = (Image)listViewSubStates.FindControl("SortImage2");
            switch (e.SortExpression)
            {
                case "SubStateRegionName":
                    sortImage1.Visible = true;
                    sortImage1.ImageUrl = imgUrl;
                    sortImage2.Visible = false;
                    break;
                case "Type":
                    sortImage1.Visible = false;
                    sortImage2.Visible = true;
                    sortImage2.ImageUrl = imgUrl;
                    break;

            }
        }

        protected void buttonSearch_Click(object sender, EventArgs e)
        {

            panelNoResults.Visible = false;

            if (Page.IsValid)
            {


                pager.SetPageProperties(0, pager.PageSize, false);
                listViewSubStates.DataBind();

            }
            else
                return;
        }


        protected void dataSourceSubStateRegion_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
           // if (!IsPostBack) return;

            IList<SearchSubStateRegionForReportViewData> substates = null;

            if (DefaultState.Code == "99")
                substates = new List<SearchSubStateRegionForReportViewData>(Logic.ListAllSubStates());
            else
                substates = new List<SearchSubStateRegionForReportViewData>(Logic.ListSubStatesForState(DefaultState.Code));

            dataSourceSubStateRegion.DataSource = substates;


            panelNoResults.Visible = (substates.Count == 0);

        }

        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion


    }


}
