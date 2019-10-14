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
using State=ShiptalkLogic.BusinessObjects.State;

namespace ShiptalkWeb.Agency
{
    public partial class LocationAdd : Page, IRouteDataPage, IAuthorize
    {
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";
        private const string MailingStateKey = "MailingState";
        private const string PrimaryPhoneKey = "PrimaryPhone";
        private const string SecondaryPhoneKey = "SecondaryPhone";
        private const string TDDKey = "TDD";
        private const string TollFreeTDDKey = "TollFreeTDD";
        private const string FaxKey = "Fax";
        private const string PhysicalZipKey = "PhysicalZip";
        private const string MailingZipKey = "MailingZip";
        private const string ScopeKey = "Scope";
        private const string AgencyIdKey = "agencyId";

        private AgencyBLL _logic;

        #region Properties

        public int? AgencyId
        {
            get
            {
                if (RouteData.Values[AgencyIdKey] == null) return null;

                int id;

                if (int.TryParse(RouteData.Values[AgencyIdKey].ToString(), out id))
                    return id;

                return null;
            }
        }

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

        protected void dataSourceAgencyLocation_Inserting(object sender, ObjectContainerDataSourceInsertingEventArgs e)
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
            e.NewValues[PhysicalZipKey] = e.NewValues[PhysicalZipKey].ToString().FormatZip();
            e.NewValues[MailingZipKey] = e.NewValues[MailingZipKey].ToString().FormatZip();
            e.NewValues["State"] = DefaultState;
            e.NewValues["AgencyId"] = AgencyId;
        }

        protected void dataSourceAgencyLocation_Inserted(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            var viewData = (AddAgencyLocationViewData)e.Instance;
            viewData.SetIsActive(true);
            viewData.SetCreated(AccountInfo.UserId);
            viewData.SetLastUpdated(AccountInfo.UserId);
            Logic.AddAgencyLocation(viewData);
            RouteController.RouteTo(RouteController.AgencyView(AgencyId.Value));
        }

        protected void proxyValidatorPhoneNumber_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            e.ConvertedValue = (!string.IsNullOrEmpty(e.ValueToConvert.ToString())) ? e.ValueConvertPhoneNumber() : null;
        }

        protected void proxyValidatorMailingState_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            e.ConvertedValue = e.ValueConvertState();
        }

        protected void proxyValidatorEmptyStringToNull_ValueConvert(object sender, ValueConvertEventArgs e)
        {
            e.ConvertedValue = (!string.IsNullOrEmpty(e.ValueToConvert.ToString())) ? e.ValueToConvert.ToString() : null;
        }

        protected void dropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefaultState = new State(((DropDownList)sender).SelectedValue);
            BindDependentData();
            formViewAgencyLocation.DataBind();
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            Validate();
            formViewAgencyLocation.InsertItem(true);
        }

        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        #region Implementation of IAuthorize

        public bool IsAuthorized()
        {
            return (AccountInfo.Scope == Scope.CMS || AccountInfo.StateFIPS == Logic.GetViewAgency(AgencyId.Value, false).State.Code);
        }

        #endregion
    }
}
