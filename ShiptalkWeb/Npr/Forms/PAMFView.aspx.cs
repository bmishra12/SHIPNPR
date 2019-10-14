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
using System.Collections.Generic;
using ShiptalkWebControls;

namespace NPRRebuild.ShiptalkWeb.PAMF
{
    public partial class PAMFView : Page, IRouteDataPage, IAuthorize
    {
        private const string IdKey = "Id";
        private const string DefaultStateKey = "DefaultState";
        private const string IsAdminKey = "IsAdmin";

        private PamBLL _pamLogic;

        #region Properties

        public IEnumerable<KeyValuePair<int, string>> PamFocus { get { return Logic.GetPamTopics(); } }
        public IEnumerable<KeyValuePair<int, string>> PamAudiences { get { return Logic.GetPamTargetAudience(); } }

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



        public PamBLL Logic
        {
            get
            {
                if (_pamLogic == null) _pamLogic = new PamBLL();

                return _pamLogic;
            }
        }


        private ViewPublicMediaEventViewData _viewData;

        private ViewPublicMediaEventViewData ViewData
        {
            get
            {
                if (_viewData == null)
                    _viewData = Logic.GetViewPAM(Id.GetValueOrDefault(0));

                return _viewData;
            }
        }


        public bool isShowDelete
        {
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

            returnValue = (localPath.Contains("/pam/edit")
                            || localPath.Contains("/pam/add") );

            if (returnValue == true) literalSuccessHeader.Text = "Success!";
            return returnValue;

        }

        protected string GetSuccessMessage()
        {
            var message = string.Empty;

            if (HttpContext.Current.Request.UrlReferrer == null) return message;

            var localPath = HttpContext.Current.Request.UrlReferrer.LocalPath.ToLower();

            if (localPath.Contains("/pam/edit"))
                message = "This Agency has successfully been edited.";
            else if (localPath.Contains("/pam/add"))
                message = "This Agency has been successfully added.";

            return message;
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) OnViewInitialized(); 

            OnViewLoaded();
        }

        protected void dataSourceViewPAM_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceViewPAM.DataSource = ViewData;
        }

        protected void buttonEditPam_Click(object sender, EventArgs e)
        {
            RouteController.RouteTo(RouteController.PamEdit(Id.Value));
        }

        protected void buttonDeletePam_Click(object sender, EventArgs e)
        {
            int pamID = Id.GetValueOrDefault(0);


            string FailureReason;
            if (Logic.DeletePamByPamID(pamID, out FailureReason))
            {
                literalSuccessHeader.Text = "Success";
                literalSuccessMessage.Text = "This PAM Record has been Deleted successfully!";

                addInSameAgency.Visible = false;

                panelSuccess.Visible = true;
                panelForm.Visible = false;
            }
            else
            {
                literalSuccessHeader.Text = "Failed!";

                FailureReason = "This PAM Record could not be deleted. Reason: " + FailureReason;
                literalSuccessMessage.Text = FailureReason;

                addInSameAgency.Visible = false;

                panelSuccess.Visible = true;
                panelForm.Visible = false;

            }
        }


        protected void buttonAddLocation_Click(object sender, EventArgs e)
        {
            RouteController.RouteTo(RouteController.AgencyLocationAdd(Id.Value));
        }



        protected void formViewPAM_OnPreRender(object sender, EventArgs e)
        {
            //bind it only once at the initilization of form..
            if (!IsPostBack)
            {

                //set the value for topic
                CheckBoxList chkPamTopic = (CheckBoxList)formViewPAM.FindControl("checkBoxListPamTopics");
                for (int i = 0; i < ViewData.PAMSelectedTopics.Count; i++)
                {
                    int chkArrayNo = Convert.ToInt32(ViewData.PAMSelectedTopics[i].Key) - 1;
                    chkPamTopic.Items[chkArrayNo].Selected = true;
                }

                CheckBoxList chkPamAudience = (CheckBoxList)formViewPAM.FindControl("checkBoxListPamAudience");
                for (int i = 0; i < ViewData.PAMSelectedAudiences.Count; i++)
                {
                    int chkArrayNo = Convert.ToInt32(ViewData.PAMSelectedAudiences[i].Key) - 1;
                    chkPamAudience.Items[chkArrayNo].Selected = true;
                }
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
            
          //  return (AccountInfo.Scope == Scope.CMS || AccountInfo.StateFIPS == ViewData.State.Code);
            return true;
        }

        #endregion
    }
}
