using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkWeb;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;

namespace ShiptalkWeb.Npr.Forms
{
    public partial class ResourceReportFView : Page , IRouteDataPage
    {
        private const string IdKey = "id";
        private const string IsAdminKey = "IsAdmin";
        private const string ScopeKey = "Scope";


        public bool IsAdmin { get { return (bool)ViewState[IsAdminKey]; } set { ViewState[IsAdminKey] = value; } }
        public Scope Scope { get { return (Scope)ViewState[ScopeKey]; } set { ViewState[ScopeKey] = value; } }


        #region Event Handlers

        UserProfile prof = null;
        UserProfile Submitter = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            prof = UserBLL.GetUserProfile(AccountInfo.UserId);
            if (!IsPostBack) OnViewInitialized();
            OnViewLoaded();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                formViewResourceReport.ChangeMode(FormViewMode.Insert);
                formViewResourceReport.InsertItem(false);
            }
            catch (ArgumentException exNull)
            {
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            RouteController.RouteTo(RouteController.ResourceReportEdit(ReportId.Value));
        }


        protected void proxyValidatorPhoneNumber_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            e.ConvertedValue = (!string.IsNullOrEmpty(e.ValueToConvert.ToString())) ? e.ValueConvertPhoneNumber() : null;
        }


        protected void dataSourceViewResourceReport_Inserted(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {

            ViewResourceReportViewData RptData = (ViewResourceReportViewData)e.Instance;
            RptData.SubmitterID = AccountInfo.UserId.ToString();
            Logic.AddReport(RptData);
        }

        protected void dataSourceViewResourceReport_Inserting(object sender, ObjectContainerDataSourceInsertingEventArgs e)
        {
            //Check for blank values and set the value to 0;
            IDictionaryEnumerator myEnumerator =
                e.NewValues.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                if (string.IsNullOrEmpty(myEnumerator.Value.ToString()))
                {
                    e.NewValues[myEnumerator.Key] = "0";
                }

            }
            e.NewValues["LastUpdatedBy"] = AccountInfo.UserId;
            e.NewValues["StateFIPSCode"] = AccountInfo.StateFIPS;
            e.NewValues["RepYrFrom"] = new DateTime(int.Parse(e.NewValues["RepYrFrom"].ToString()), DateTime.Now.Month, DateTime.Now.Day);
            e.NewValues["RepYrTo"] = new DateTime(int.Parse(e.NewValues["RepYrTo"].ToString()), DateTime.Now.Month, DateTime.Now.Day);
            e.NewValues["LastUpdatedBy"] = AccountInfo.UserId;


        }


        #endregion


        #region Methods
        /// <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void OnViewInitialized()
        {

            IsAdmin = AccountInfo.IsAdmin;
            Scope = (Scope)AccountInfo.ScopeId;

            dataSourceViewResourceReport.DataSource = DataView;
            Submitter = UserBLL.GetUserProfile(int.Parse(DataView.SubmitterID));
            BindDependentData();
            
            dataSourceViewResourceReport.DataBind();
            formViewResourceReport.DataBind();

        }

        /// <summary>
        /// Called when page is loaded.
        /// </summary>
        protected void OnViewLoaded()
        {

        }

        /// <summary>
        /// Binds the dependent data for the form.
        /// </summary>
        protected void BindDependentData()
        {
            if (Submitter == null)
            {
                dataSourceViewResourceReport.DataSource = DataView;
                Submitter = UserBLL.GetUserProfile(int.Parse(DataView.SubmitterID));
            }
            
            
                string FullName = Submitter.FirstName + " " + Submitter.LastName;
                DataView.PersonCompletingReportName = FullName;
                DataView.PersonCompletingReportTel = Submitter.PrimaryPhone;
                DataView.PersonCompletingReportTitle = FullName;
               
        }
        #endregion

        #region Properties
        /// <summary>
        /// Business Logic object
        /// </summary>
        private ResourceReportBLL _logic;
        public ResourceReportBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new ResourceReportBLL();

                return _logic;
            }
        }

        /// <summary>
        /// View of Data
        /// </summary>
        ViewResourceReportViewData _View = null;
        public ViewResourceReportViewData DataView
        {
            get
            {
                if (_View == null)
                {
                                        
                    _View = Logic.GetViewResourceReport(ReportId.Value);
                   // if ((_View.StateFIPSCode == AccountInfo.StateFIPS) || AccountInfo.IsAdmin)
                    //{
                        if (string.IsNullOrEmpty(_View.StateGranteeName) || _View.StateGranteeName == "0")
                        {
                            _View.StateGranteeName = string.Empty;
                        }
                        if (string.IsNullOrEmpty(_View.Title) || _View.Title == "0")
                        {
                            _View.Title = string.Empty;
                        }
                        return _View;
                    //}
                   // else
                    //{
                    //    _View = null;
                   // }
                }
                return _View;

            }
        }

        private int? ReportId
        {
            get
            {
                if (RouteData.Values[IdKey] == null) return null;
                
                    int ResourceReportId = 0;
                    int.TryParse(RouteData.Values[IdKey].ToString(), out ResourceReportId);
                    return ResourceReportId;
            }

        }
        #endregion


        
        
        #region Implementation of IRouteDataPage


            public RouteData RouteData { get; set; }
            public UserAccount AccountInfo { get; set; }


        #endregion


        #region Implementation of IAuthorize
            public bool IsAuthorized()
            {
                return true;
            }
        #endregion




        #region Events

        
        #endregion
    }
}
