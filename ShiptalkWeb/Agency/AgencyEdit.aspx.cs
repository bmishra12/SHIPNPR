using System;
using System.Collections.Generic;
using System.Security;
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

namespace ShiptalkWeb.Agency
{
    public partial class AgencyEdit : Page, IRouteDataPage, IAuthorize
    {
        private const string IdKey = "Id";
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";
        private const string PrimaryPhoneKey = "PrimaryPhone";
        private const string SecondaryPhoneKey = "SecondaryPhone";
        private const string TDDKey = "TDD";
        private const string TollFreeTDDKey = "TollFreeTDD";
        private const string FaxKey = "Fax";
        private const string PhysicalZipKey = "PhysicalZip";
        private const string MailingZipKey = "MailingZip";
        
        private AgencyBLL _logic;

        #region Properties

        public IEnumerable<KeyValuePair<int, string>> AgencyTypes { get; set; }
        
        public IEnumerable<KeyValuePair<string, string>> Counties { get; set; }

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

        public IEnumerable<KeyValuePair<string, string>> States { get; set; }

        private EditAgencyViewData _viewData;

        private EditAgencyViewData ViewData 
        { 
            get
            {
                if (_viewData == null)
                    _viewData = Logic.GetEditAgency(Id.GetValueOrDefault(0), true);

                return _viewData;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Binds the dependent data for the form.
        /// </summary>
        protected void BindDependentData()
        {
            Counties = Logic.GetCounties(ViewData.PhysicalCountyFIPS);
            States = Logic.GetStates();
            AgencyTypes = Logic.GetAgencyTypes();
        }

        /// <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void OnViewInitialized()
        {
            DefaultState = new State(AccountInfo.StateFIPS);
            IsAdmin = AccountInfo.IsAdmin;
        }

        /// <summary>
        /// Called when page is loaded.
        /// </summary>
        protected void OnViewLoaded()
        {
            BindDependentData();
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack) OnViewInitialized();

            OnViewLoaded();
        }

        protected void dataSourceEditAgency_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceEditAgency.DataSource = ViewData;
        }

        protected void dataSourceEditAgency_Updated(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            var viewData = (EditAgencyViewData)e.Instance;
            viewData.SetLastUpdated(AccountInfo.UserId);
            Logic.UpdateAgency(viewData);
            RouteController.RouteTo(RouteController.AgencyView(Id.Value));
        }

        protected void dataSourceEditAgency_Updating(object sender, ObjectContainerDataSourceUpdatingEventArgs e)
        {
            e.NewValues[PrimaryPhoneKey] = e.NewValues[PrimaryPhoneKey].ToString().FormatPhoneNumber();
            e.NewValues[SecondaryPhoneKey] = e.NewValues[SecondaryPhoneKey].ToString().FormatPhoneNumber();
            e.NewValues[TDDKey] = e.NewValues[TDDKey].ToString().FormatPhoneNumber();
            e.NewValues[TollFreeTDDKey] = e.NewValues[TollFreeTDDKey].ToString().FormatPhoneNumber();
            e.NewValues[FaxKey] = e.NewValues[FaxKey].ToString().FormatPhoneNumber();
            e.NewValues[PhysicalZipKey] = e.NewValues[PhysicalZipKey].ToString().FormatZip();
            e.NewValues[MailingZipKey] = e.NewValues[MailingZipKey].ToString().FormatZip();
            e.NewValues["StateValue"] = DefaultState.StateAbbr;         

        }

        protected void proxyValidatorPhoneNumber_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            e.ConvertedValue = (!string.IsNullOrEmpty(e.ValueToConvert.ToString())) ? e.ValueConvertPhoneNumber() : null;
        }

        protected void proxyValidatorType_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            e.ConvertedValue = e.ValueConvertEnum<AgencyType?>();
        }

        protected void proxyValidatorMailingState_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            e.ConvertedValue = e.ValueConvertState();
        }

        protected void proxyValidatorEmptyStringToNull_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            e.ConvertedValue = (!string.IsNullOrEmpty(e.ValueToConvert.ToString())) ? e.ValueToConvert.ToString() : null;
        }

        protected void validatorServiceAreas_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !string.IsNullOrEmpty(((HiddenField)formViewAgency.FindControl("hiddenServiceAreas")).Value);
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            Validate();           
   
            formViewAgency.UpdateItem(true);
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
