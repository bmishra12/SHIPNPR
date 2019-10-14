using System;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkWeb.Routing;


namespace ShiptalkWeb.Npr.Forms
{
    public partial class CCFView : Page, IRouteDataPage, IAuthorize
    {
        private const string IdKey = "Id";
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";

        private AgencyBLL _agencyLogic;
        private CCFBLL _logic;
        private ViewClientContactViewData _viewData;
        private const string ReviewerUserIdKey = "ReviewerUserId";

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

        public AgencyBLL AgencyLogic
        {
            get
            {
                if (_agencyLogic == null) _agencyLogic = new AgencyBLL();

                return _agencyLogic;
            }
        }

        public CCFBLL Logic
        {
            get
            {
                if (_logic == null) _logic = new CCFBLL();

                return _logic;
            }
        }

        private ViewClientContactViewData ViewData
        {
            get
            {
                if (_viewData == null)
                    _viewData = Logic.GetViewClientContact(Id.GetValueOrDefault(0));

                return _viewData;
            }
        }

        public int? ReviewerUserId { get { return (ViewState[ReviewerUserIdKey] != null) ? (int?)Convert.ToInt32(ViewState[ReviewerUserIdKey]) : null; } set { ViewState[ReviewerUserIdKey] = value; } }

        public bool isShowDelete{
            get
            {
                return (ApproverRulesBLL.IsApproverAtCMS(this.AccountInfo)
            || this.AccountInfo.IsShipDirector);
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
            bool returnValue = false;
            if (HttpContext.Current.Request.UrlReferrer == null) return false;

            var localPath = HttpContext.Current.Request.UrlReferrer.LocalPath.ToLower();

            returnValue = (localPath.Contains("/forms/ccf/add")
                    || localPath.Contains("/forms/ccf/edit"));

            if (returnValue == true) literalSuccessHeader.Text = "Success!";
            return returnValue;
        }

        protected string GetSuccessMessage()
        {
            var message = string.Empty;

            if (HttpContext.Current.Request.UrlReferrer == null) return message;

            var localPath = HttpContext.Current.Request.UrlReferrer.LocalPath.ToLower();

            if (localPath.Contains("/forms/ccf/add"))
                message = "This Client Contact has successfully been saved.";
            else if (localPath.Contains("/forms/ccf/edit"))
                message = "This Client Contact has successfully been edited.";

            return message;
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) OnViewInitialized();

            OnViewLoaded();
        }

        protected void dataSourceViewClientContact_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceViewClientContact.DataSource = ViewData;
        }

        protected void buttonEditClientContact_Click(object sender, EventArgs e)
        {
            RouteController.RouteTo(RouteController.CcfEdit(Id.GetValueOrDefault(0)));
        }

        //protected void buttonAddSimilarClientContact_Click(object sender, EventArgs e)
        //{
           //RouteController.RouteTo(RouteController.CcfAddSimilarContact(Id.GetValueOrDefault(0)));
        //}


        protected void buttonDeleteClientContact_Click(object sender, EventArgs e)
        {
            int clientContactID = Id.GetValueOrDefault(0);


            string FailureReason;
            if (Logic.DeleteClientContact(clientContactID, out FailureReason))
            {

                literalSuccessHeader.Text = "Success";
                literalSuccessMessage.Text = "This Client Contact Record has been Deleted successfully!";

                addInSameAgency.Visible = false;
                panelSuccess.Visible = true;
                panelForm.Visible = false;
            }
            else
            {
                literalSuccessHeader.Text = "Failed!";

                FailureReason = "This Client Contact Record could not be deleted. Reason: " + FailureReason;
                literalSuccessMessage.Text = FailureReason;
                addInSameAgency.Visible = false;
                panelSuccess.Visible = true;
                panelForm.Visible = false;

            }
        }


        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        #region Implementation of IAuthorize

        public bool IsAuthorized()
        {
            if (AccountInfo.IsCMSLevel == true) return true;

            //for cms or shipDirector return true so that they can see the delete button
            if (ApproverRulesBLL.IsApproverAtCMS(this.AccountInfo) || this.AccountInfo.IsShipDirector)
                return true;

            if (!AgencyLogic.IsAgencyUserActive(ViewData.AgencyId, AccountInfo.UserId))
                return false;

            ReviewerUserId = (Logic.IsUserClientContactReviewer(ViewData.Id, AccountInfo.UserId))
                                ? (int?)AccountInfo.UserId
                                : null;

            if (ViewData.SubmitterUserId == AccountInfo.UserId || ViewData.CounselorUserId == AccountInfo.UserId)
                return true;

            var descriptors = UserBLL.GetDescriptorsForUser(AccountInfo.UserId, ViewData.AgencyId);

            foreach (var descriptor in descriptors)
            {
                if (descriptor == (int)Descriptor.DataSubmitter
                    || descriptor == (int)Descriptor.ShipDirector)
                    return true;
            }

            return (ReviewerUserId.HasValue);
        }

        #endregion
    }
}
