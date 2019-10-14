using System;
using System.Collections.Generic;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;
using ShiptalkWeb;
using System.Linq;



namespace NPRRebuild.ShiptalkWeb.NPRReports
{
    public partial class SubStateRegionAddForReport : Page, IRouteDataPage
    {
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";
        private const string ScopeKey = "Scope";
        private const string ReportSubStateReportTypeKey = "ReportSubStateReportType";
        private const string ReportFromeTypeKey = "ReportFromeType";

        private ReportSubStateRegionBLL _logic;

        #region Properties


        public IEnumerable<KeyValuePair<int, string>> Agencies { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Counties { get; set; }
        public IEnumerable<KeyValuePair<string, string>> ZipCodes { get; set; }

        public IEnumerable<KeyValuePair<int, string>> ReportSubStateReportType { get { return  (IEnumerable<KeyValuePair<int, string>>)ViewState[ReportSubStateReportTypeKey] ; } set { ViewState[ReportSubStateReportTypeKey] = value; } }


        public IEnumerable<KeyValuePair<int, string>> ReportFromeType { get { return (IEnumerable<KeyValuePair<int, string>>)ViewState[ReportFromeTypeKey]; } set { ViewState[ReportFromeTypeKey] = value; } }
        //Logic.GetFromeType();
        
        public IEnumerable<KeyValuePair<string, string>> DynamicSubStateRegions { get; set; }


        public State DefaultState { get { return (State)ViewState[DefaultStateKey]; } set { ViewState[DefaultStateKey] = value; } }
        public bool IsAdmin { get { return (bool)ViewState[IsAdminKey]; } set { ViewState[IsAdminKey] = value; } }


        private AgencyBLL _agencyLogic;

        public AgencyBLL AgencyLogic
        {
            get
            {
                if (_agencyLogic == null) _agencyLogic = new AgencyBLL();

                return _agencyLogic;
            }
        }

        public ReportSubStateRegionBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new ReportSubStateRegionBLL();

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

            DynamicSubStateRegions = null;
            States = AgencyLogic.GetStates();

            DropDownList ctrlForm = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListFormType");
            int _formType = Convert.ToInt32(ctrlForm.SelectedValue);


            DropDownList ctrlGroup = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListGroupype");
            int _groupType = Convert.ToInt32(ctrlGroup.SelectedValue);
            if (_groupType > 0 && _formType > 0)
            {
                FormType formType = (FormType)_formType;
                SubStateReportType subStateType = (SubStateReportType)_groupType;


                if ((subStateType == SubStateReportType.Agency || subStateType ==SubStateReportType.AgencyPam) 
                    && (formType == FormType.ClientContact || formType == FormType.PublicMediaActivity))
                {
                    Agencies = AgencyLogic.GetAgencies(DefaultState.Code);
                    var _agencies = new List<KeyValuePair<string, string>>();
                    _agencies.AddRange(Agencies.Select((pair => (new KeyValuePair<string, string>(pair.Key.ToString(), pair.Value)))));
                    DynamicSubStateRegions = _agencies;
                }

                else if ( (subStateType == SubStateReportType.CountycodeOfClientRes || subStateType == SubStateReportType.CountyOfCounselorLocation || subStateType == SubStateReportType.CountycodeOfEvent )
                        && (formType == FormType.ClientContact || formType == FormType.PublicMediaActivity))
                    DynamicSubStateRegions = AgencyLogic.GetCounties(DefaultState.Code);


                else if ((subStateType == SubStateReportType.ZIPCodeOfClientRes || subStateType == SubStateReportType.ZIPCodeOfCounselorLocation)
                        && (formType == FormType.ClientContact))
                {
                    IEnumerable<ZipCountyView> zipCode = LookupBLL.GetZipCodesAndCountyFIPSForState(DefaultState.Code);
                    var _zips = new List<KeyValuePair<string, string>>();
                    _zips.AddRange(zipCode.Select((pair => (new KeyValuePair<string, string>(pair.Zipcode.ToString(), pair.Zipcode)))));
                    DynamicSubStateRegions = _zips;
                }

            }

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

            StateBindCall();
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

        protected void proxyValidatorState_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            e.ConvertedValue = e.ValueConvertState();
        }


        protected void PropertyProxyValidatorReprotType_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            if (e.ValueToConvert.ToString()== "0")
                e.ConvertedValue = null;
            else
            {
                e.ConvertedValue = e.ValueToConvert.ToString();
            }
        }

        protected void dataSourceSubStateRegion_Inserted(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            var viewData = (AddSubStateRegionForReportViewData)e.Instance;
            viewData.SetIsActive(true);
            viewData.SetCreated(AccountInfo.UserId);
            viewData.SetLastUpdated(AccountInfo.UserId);

            RouteController.RouteTo(RouteController.ReportSubstateView(Logic.CreateSubStateRegionForReport(viewData)));
        }

        protected void dataSourceSubStateRegion_Inserting(object sender, ObjectContainerDataSourceInsertingEventArgs e)
        {


            e.NewValues["State"] = DefaultState;
        }


        protected void validatorServiceAreas_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !string.IsNullOrEmpty(((HiddenField)formViewSubStateRegionForReport.FindControl("hiddenServiceAreas")).Value);
        }

        protected void dropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefaultState = new State(((DropDownList) sender).SelectedValue);

            StateBindCall();

        }

        private void StateBindCall()
        {
            DropDownList ctrlFormtype = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListFormType");

            if (DefaultState.Code == null)
                ReportFromeType = null;
            else
                ReportFromeType = Logic.GetFromeType();

            ctrlFormtype.Focus();

            //bind group type with null
            ReportSubStateReportType = null;

            DynamicSubStateRegions = null;

            States = AgencyLogic.GetStates();

            BindFormView();
        }

        protected void dropDownListFormType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //change the ccf/pam type for the type dropdown
            int _formType = Convert.ToInt32(((DropDownList)sender).SelectedValue);
            FormType formType = (FormType)_formType;
            ReportSubStateReportType = Logic.GetSubStateReportType(formType);

            DynamicSubStateRegions = null;

            States = AgencyLogic.GetStates();

            BindFormView();

        }




        protected void dropDownListGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDependentData();
            BindFormView();

        }

        private void BindFormView()
        {
            //preserve the before values
            DropDownList ctrlForm = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListFormType");
            string _formType = ctrlForm.SelectedValue;

            DropDownList ctrlGroup = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListGroupype");
            string _groupType = ctrlGroup.SelectedValue;

            TextBox ctrlName = (TextBox)formViewSubStateRegionForReport.FindControl("textBoxName");
            string _reportName = ctrlName.Text;

            formViewSubStateRegionForReport.DataBind();

            //Aplly the before values
            DropDownList ctrlFormAfter = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListFormType");
            DropDownList ctrlGroupAfter = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListGroupype");
            TextBox ctrlNameAfter = (TextBox)formViewSubStateRegionForReport.FindControl("textBoxName");

            ctrlFormAfter.SelectedValue = _formType;
            ctrlGroupAfter.SelectedValue = _groupType;
            ctrlNameAfter.Text = _reportName;
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {

            TextBox ctrlName = (TextBox)formViewSubStateRegionForReport.FindControl("textBoxName");
            string reportName = ctrlName.Text;

            Label ctrlErrName = (Label)formViewSubStateRegionForReport.FindControl("lblErrorMsg");
            ctrlErrName.Visible = false;

            if (reportName.Length > 0 && Logic.DoesSubStateRegionForReportNameExist(reportName))
            {
                //display error message.
                ctrlErrName.Visible = true;
                ctrlErrName.Text = "The Substate Region Report Name already exists. Please use a different name.";
                
                return;
            }

            Validate();
            formViewSubStateRegionForReport.InsertItem(true);
        }

        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion
    }
}
