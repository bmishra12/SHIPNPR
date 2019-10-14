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

namespace ShiptalkWeb.SHIP
{
    public partial class SHIPProfileEdit : Page, IRouteDataPage, IAuthorize
    {
        private const string IdKey = "Id";
        private const string IsAdminKey = "IsAdmin";
        private SHIPProfileBLL _logic;
        private const string DefaultStateKey = "DefaultState";
        private const string PrimaryPhoneKey = "PrimaryPhone";
        private const string SecondaryPhoneKey = "SecondaryPhone";
        private const string TDDKey = "TDD";
        private const string TollFreeTDDKey = "TollFreeTDD";
        private const string FaxKey = "Fax";
        private const string PhysicalZipKey = "PhysicalZip";
        private const string MailingZipKey = "MailingZip";

        #region Properties
        public State DefaultState { get { return (State)ViewState[DefaultStateKey]; } set { ViewState[DefaultStateKey] = value; } }

        public string Id
        {
            get
            {
                if (RouteData.Values[IdKey] == null) return null;

                return RouteData.Values[IdKey].ToString();

            }
        }

        public bool IsAdmin { get { return (bool)ViewState[IsAdminKey]; } set { ViewState[IsAdminKey] = value; } }
        public SHIPProfileBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new SHIPProfileBLL();

                return _logic;
            }
        }

        private EditShipProfileViewData _viewData;

        private EditShipProfileViewData ViewData
        {
            get
            {
                if (_viewData == null)
                    _viewData = Logic.GetEditSHIPProfile(Id);

                return _viewData;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                OnViewInitialized();
               // PopulateStates();
            }
        }
        /// <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void OnViewInitialized()
        {
            DefaultState = new State(AccountInfo.StateFIPS);
            IsAdmin = AccountInfo.IsAdmin;
        }

        protected void PopulateStates()
        {
            ddlStates.DataSource = State.GetStatesWithFIPSKey();
            ddlStates.DataTextField = "Value";
            ddlStates.DataValueField = "Key";
            ddlStates.DataBind();

            if (AccountInfo.IsStateAdmin)
            {
                ddlStates.Items.FindByValue(AccountInfo.StateFIPS).Selected = true;
                ddlStates.Enabled = false;
                if (ViewData != null)
                {
                    dataSourceEditSHIP.DataSource = ViewData;
                }
            }
        }
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            MessageBox.Visible = false;
            if (Page.IsValid)
            {
                Validate();
                formView.UpdateItem(false);
            }
        }
        protected void dataSourceEditSHIP_Updated(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            var viewData = (EditShipProfileViewData)e.Instance;
            //viewData.SetLastUpdated(AccountInfo.UserId);
            Logic.UpdateShipProfile(viewData);

            dataSourceEditSHIP.DataSource = viewData;
            MessageBox.Visible = true;
            MessageBox.Text = "Record updated successfull!.";
            //RouteController.RouteTo(RouteController.ShipProfileEdit());
        }


        protected void dataSourceEditSHIP_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceEditSHIP.DataSource = ViewData;
        }
        protected void dataSourceEditSHIP_Updating(object sender, ObjectContainerDataSourceUpdatingEventArgs e)
        {
            //e.NewValues[PrimaryPhoneKey] = e.NewValues[PrimaryPhoneKey].ToString().FormatPhoneNumber();
            //e.NewValues[SecondaryPhoneKey] = e.NewValues[SecondaryPhoneKey].ToString().FormatPhoneNumber();
            //e.NewValues[TDDKey] = e.NewValues[TDDKey].ToString().FormatPhoneNumber();
            //e.NewValues[TollFreeTDDKey] = e.NewValues[TollFreeTDDKey].ToString().FormatPhoneNumber();
            //e.NewValues[FaxKey] = e.NewValues[FaxKey].ToString().FormatPhoneNumber();
            //e.NewValues[PhysicalZipKey] = e.NewValues[PhysicalZipKey].ToString().FormatZip();
            //e.NewValues[MailingZipKey] = e.NewValues[MailingZipKey].ToString().FormatZip();
            //e.NewValues["StateValue"] = DefaultState.StateAbbr;
        }
        protected void ddlStates_SelectedChanged(object sender, EventArgs e)
        {
            MessageBox.Visible = false;
            if (ddlStates.SelectedValue != "0")
            {
                if (ViewData != null)
                {
                    dataSourceEditSHIP.DataSource = ViewData;
                }
                else
                {
                    dataSourceEditSHIP.DataSource = null;
                    formView.DataSource = null;
                }
            }
            else
            {
                dataSourceEditSHIP.DataSource = null;
                formView.DataSource = null;
            }
        }

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        #region Implementation of IAuthorize

        public bool IsAuthorized()
        {

              if (AccountInfo.Scope == Scope.CMS && AccountInfo.IsAdmin == true)
                return true;

              if (AccountInfo.Scope == Scope.State && AccountInfo.IsShipDirector == true)
                return true;

            //for all other case:
              return false;
        }

        #endregion

        
    }
}
