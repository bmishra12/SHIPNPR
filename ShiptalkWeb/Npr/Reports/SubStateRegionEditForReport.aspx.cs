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
    public partial class SubStateRegionEditForReport : Page, IRouteDataPage
    {
        private const string IdKey = "Id";

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


        private EditSubStateRegionForReportViewData _viewData;

        private EditSubStateRegionForReportViewData ViewData
        {
            get
            {
                if (_viewData == null)
                    _viewData = Logic.GetEditSubStateRegionForReport(Id.GetValueOrDefault(0));

                return _viewData;
            }
        }

        #endregion

        #region Methods



                    


        protected void BindFormData()
        {
            States = AgencyLogic.GetStates();

            DefaultState = ViewData.State;
            DropDownList ctrlState = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListState");
            var state = ctrlState.Items.FindByValue(DefaultState.StateAbbr);
            if (state != null)
                state.Selected = true;


            ReportFromeType = Logic.GetFromeType();

            DropDownList ctrlForm = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListFormType");
            ctrlForm.DataSource = ReportFromeType;
            ctrlForm.DataBind();
            ctrlForm.Items.Insert(0, "-- Select Form Type --");

            var reportType = ctrlForm.Items.FindByValue(((int)ViewData.ReprotFormType).ToString());
            if (reportType != null)
                reportType.Selected = true;


            FormType _formType = (FormType)ViewData.ReprotFormType;
            ReportSubStateReportType = Logic.GetSubStateReportType(_formType);


            DropDownList ctrlGroup = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListGroupype");
            ctrlGroup.DataSource = ReportSubStateReportType;
            ctrlGroup.DataBind();
            ctrlGroup.Items.Insert(0, "-- Select Report Group Type --");

            var groupType = ctrlGroup.Items.FindByValue(((int)ViewData.Type).ToString());
            if (groupType != null)
                groupType.Selected = true;




            SubStateReportType _type = (SubStateReportType)ViewData.Type;
            GetSelectionArea(_formType, _type);


            ListBox ctrlCounties = (ListBox)formViewSubStateRegionForReport.FindControl("listBoxCounties");
            ListBox ctrlSvc = (ListBox)formViewSubStateRegionForReport.FindControl("listBoxServiceAreas");
            ctrlCounties.DataSource = DynamicSubStateRegions;
            ctrlSvc.DataSource = DynamicSubStateRegions;
            ctrlCounties.DataBind();
            ctrlSvc.DataBind();
        }


        /// <summary>
        /// Binds the dependent data for the form.
        /// </summary>
        protected void BindDependentDataCHANGE()
        {

            DynamicSubStateRegions = null;

            DropDownList ctrlForm = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListFormType");
            int _formType = Convert.ToInt32(ctrlForm.SelectedValue);


            DropDownList ctrlGroup = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListGroupype");
            int _groupType = Convert.ToInt32(ctrlGroup.SelectedValue);
            if (_groupType > 0 && _formType > 0)
            {
                FormType formType = (FormType)_formType;
                SubStateReportType subStateType = (SubStateReportType)_groupType;


               GetSelectionArea(formType, subStateType);


            }

            ListBox ctrlCounties = (ListBox)formViewSubStateRegionForReport.FindControl("listBoxCounties");
            ListBox ctrlSvc = (ListBox)formViewSubStateRegionForReport.FindControl("listBoxServiceAreas");
            ctrlCounties.DataSource = DynamicSubStateRegions;
            ctrlSvc.DataSource = DynamicSubStateRegions;
            ctrlCounties.DataBind();
            ctrlSvc.DataBind();
        }

        private void GetSelectionArea(FormType formType, SubStateReportType subStateType)
        {

            if ((subStateType == SubStateReportType.Agency || subStateType == SubStateReportType.AgencyPam)
                && (formType == FormType.ClientContact || formType == FormType.PublicMediaActivity))
            {
                Agencies = AgencyLogic.GetAgencies(DefaultState.Code);
                var _agencies = new List<KeyValuePair<string, string>>();
                _agencies.AddRange(Agencies.Select((pair => (new KeyValuePair<string, string>(pair.Key.ToString(), pair.Value)))));
                DynamicSubStateRegions = _agencies;
            }

            else if ((subStateType == SubStateReportType.CountycodeOfClientRes || subStateType == SubStateReportType.CountyOfCounselorLocation || subStateType == SubStateReportType.CountycodeOfEvent)
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


        /// <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void OnViewInitialized()
        {
            DefaultState = new State(AccountInfo.StateFIPS);
            IsAdmin = AccountInfo.IsAdmin;
            Scope = (Scope)AccountInfo.ScopeId;
            BindFormData();
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


        protected void dataSourceEditSubStateRegionForReport_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {

            dataSourceSubStateRegionForReport.DataSource = ViewData;

        }


        protected void dataSourceEditSubStateRegionForReport_Updated(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            var viewData = (EditSubStateRegionForReportViewData)e.Instance;
            viewData.SetIsActive(viewData.IsActive);
            viewData.SetCreated(AccountInfo.UserId);
            viewData.SetLastUpdated(AccountInfo.UserId);

            Logic.UpdateSubStateRegionForReport(viewData);

            RouteController.RouteTo(RouteController.ReportSubstateView(Id.GetValueOrDefault(0)));

        }



        protected void dataSourceEditSubStateRegionForReport_Updating(object sender, ObjectContainerDataSourceUpdatingEventArgs e)
        {
            DropDownList ctrlForm = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListFormType");
            DropDownList ctrlGroup = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListGroupype");


            e.NewValues["State"] = DefaultState;
            e.NewValues["ReprotFormType"] = (FormType)Convert.ToInt32(ctrlForm.SelectedValue);
            e.NewValues["Type"] = (SubStateReportType)Convert.ToInt32(ctrlGroup.SelectedValue);
        }



        protected void validatorServiceAreas_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !string.IsNullOrEmpty(((HiddenField)formViewSubStateRegionForReport.FindControl("hiddenServiceAreas")).Value);
        }

        protected void dropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefaultState = new State(((DropDownList) sender).SelectedValue);

            DropDownList ctrlForm = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListFormType");

            if (DefaultState.Code == null)
                ReportFromeType = null;

            else
               ReportFromeType= Logic.GetFromeType();

            if (ReportFromeType != null)
            {

                ctrlForm.DataSource = ReportFromeType;
                ctrlForm.DataBind();
                ctrlForm.Items.Insert(0, "-- Select Form Type --");

                ctrlForm.Focus();
            }
            else
            {
                ctrlForm.Items.Clear();
                ctrlForm.Items.Insert(0, "-- Select Form Type --");

            }


            //bind group type with null
            ReportSubStateReportType = null;
            DropDownList ctrlGroup = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListGroupype");
            ctrlGroup.Items.Clear();
            ctrlGroup.Items.Insert(0, "-- Select Report Group Type --");


            DynamicSubStateRegions = null;
            ListBox ctrlCounties = (ListBox)formViewSubStateRegionForReport.FindControl("listBoxCounties");
            ListBox ctrlSvc = (ListBox)formViewSubStateRegionForReport.FindControl("listBoxServiceAreas");
            ctrlCounties.Items.Clear();
            ctrlSvc.Items.Clear();

        }

        protected void dropDownListFormType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //change the ccf/pam type for the type dropdown
            int _formType = Convert.ToInt32(((DropDownList)sender).SelectedValue);

            FormType formType = (FormType)_formType;
            ReportSubStateReportType = Logic.GetSubStateReportType(formType); //this return null if the formtype not selected.

            DropDownList ctrlGroup = (DropDownList)formViewSubStateRegionForReport.FindControl("dropDownListGroupype");

            if (ReportSubStateReportType != null)
            {
                ctrlGroup.DataSource = ReportSubStateReportType;
                ctrlGroup.DataBind();
                ctrlGroup.Items.Insert(0, "-- Select Report Group Type --");

            }
            else
            {
                ctrlGroup.Items.Clear();
                ctrlGroup.Items.Insert(0, "-- Select Report Group Type --");
            }


            DynamicSubStateRegions = null;
            ListBox ctrlCounties = (ListBox)formViewSubStateRegionForReport.FindControl("listBoxCounties");
            ListBox ctrlSvc = (ListBox)formViewSubStateRegionForReport.FindControl("listBoxServiceAreas");
            ctrlCounties.DataSource = DynamicSubStateRegions;
            ctrlSvc.DataSource = DynamicSubStateRegions;
            ctrlCounties.DataBind();
            ctrlSvc.DataBind();

        }




        protected void dropDownListGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindDependentDataCHANGE();

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


            Validate();
            formViewSubStateRegionForReport.UpdateItem(true);
        }

        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion
    }
}
