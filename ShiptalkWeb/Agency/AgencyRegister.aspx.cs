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

namespace ShiptalkWeb.Agency
{
    public partial class AgencyRegister : Page, IRouteDataPage
    {
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";
        private const string MailingStateKey = "MailingState";
        private const string PrimaryPhoneKey = "PrimaryPhone";
        private const string SecondaryPhoneKey = "SecondaryPhone";
        private const string TDDKey = "TDD";
        private const string TollFreeTDDKey = "TollFreeTDD";
        private const string FaxKey = "Fax";
        private const string PhysicalZIPKey = "PhysicalZIP";
        private const string MailingZIPKey = "MailingZIP";
        private const string ScopeKey = "Scope";

        private AgencyBLL _logic;

        #region Properties

        public IEnumerable<KeyValuePair<int, string>> AgencyTypes { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Counties { get; set; }

        public State DefaultState { get { return (State)ViewState[DefaultStateKey]; } set { ViewState[DefaultStateKey] = value; } }

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

        public IEnumerable<KeyValuePair<string, string>> States { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Binds the dependent data for the form.
        /// </summary>
        protected void BindDependentData()
        {
            Counties = Logic.GetCounties(DefaultState.Code);
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

        protected void dataSourceAgeny_Inserted(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            var viewData = (RegisterAgencyViewData)e.Instance;
            viewData.SetIsActive(true);
            viewData.SetCreated(AccountInfo.UserId);
            viewData.SetLastUpdated(AccountInfo.UserId);
            RouteController.RouteTo(RouteController.AgencyRegisterSuccess(Logic.RegisterAgency(viewData)));
        }

        protected void dataSourceAgency_Inserting(object sender, ObjectContainerDataSourceInsertingEventArgs e)
        {
            var mailingStateValue = e.NewValues[MailingStateKey].ToString();
            e.NewValues[MailingStateKey] = (!string.IsNullOrEmpty(mailingStateValue))
                                              ? new State(mailingStateValue)
                                              : (State?)null;
            e.NewValues[PrimaryPhoneKey] = e.NewValues[PrimaryPhoneKey].ToString().FormatPhoneNumber();
            e.NewValues[SecondaryPhoneKey] = e.NewValues[SecondaryPhoneKey].ToString().FormatPhoneNumber();
            e.NewValues[TDDKey] = e.NewValues[TDDKey].ToString().FormatPhoneNumber();
            e.NewValues[TollFreeTDDKey] = e.NewValues[TollFreeTDDKey].ToString().FormatPhoneNumber();
            e.NewValues[FaxKey] = e.NewValues[FaxKey].ToString().FormatPhoneNumber();
            e.NewValues[PhysicalZIPKey] = e.NewValues[PhysicalZIPKey].ToString().FormatZip();
            e.NewValues[MailingZIPKey] = e.NewValues[MailingZIPKey].ToString().FormatZip();
            e.NewValues["State"] = DefaultState;
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

        protected void dropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefaultState = new State(((DropDownList) sender).SelectedValue);
            BindDependentData();
            formViewAgency.DataBind();
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            Validate();
            formViewAgency.InsertItem(true);
        }

        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion
    }
}
