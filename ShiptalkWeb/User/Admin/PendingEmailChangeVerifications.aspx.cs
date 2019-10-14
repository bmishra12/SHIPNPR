using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.Routing;
using System.ComponentModel;
using Microsoft.Practices.Web.UI.WebControls;

using ShiptalkWeb.Routing;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;


namespace ShiptalkWeb
{
    public partial class PendingEmailChangeVerifications : System.Web.UI.Page, IRouteDataPage
    {

        private bool IsViewDataLoaded = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            pager.PagedControlID = listViewPendingEmailVerifications.UniqueID;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsAuthorized)
                    DisplayMessage("You are not authorized to view this page.", true);
                else
                {
                    pager.SetPageProperties(0, pager.PageSize, false);
                    RetrieveAndBindData();
                    Page.DataBind();
                }
            }
        }



        private void RetrieveAndBindData()
        {
            RetrievePendingEmailVerifications();
            dataSourcePendingEmailVerifications.DataSource = ViewData;

            //if (ViewData == null && ViewData.Count() == 0)
            if (ViewData == null || ViewData.Count() == 0)
            {
                NoSearchResultsMessage.Visible = true;
            }
        }

        private bool IsAuthorized
        {
            get
            {
                return ApproverRulesBLL.IsApprover(this.AccountInfo);
            }
        }


        #region Postback event handlers
        protected void dataSourcePendingEmailVerifications_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            if (IsPostBack)
            {
                RetrieveAndBindData();
            }
        }

        //protected enum ShowDataCommands
        //{
        //    [Description("SHOW_PENDING_EMAILS")]
        //    SHOW_PENDING_EMAILS = 1,
        //    [Description("SHOW_PENDING_APPROVALS")]
        //    SHOW_PENDING_APPROVALS = 2
        //}

        //protected void btnShowData_Cmd(object sender, CommandEventArgs e)
        //{

        //    if (!string.IsNullOrEmpty(e.CommandName))
        //    {
        //        ShowDataCommands Cmd = (ShowDataCommands)Enum.Parse(typeof(ShowDataCommands), e.CommandName, true);
        //        if (Cmd == ShowDataCommands.SHOW_PENDING_APPROVALS || Cmd == ShowDataCommands.SHOW_PENDING_EMAILS)
        //        {
        //            IncludeAllPendingEmailVerifications = (Cmd == ShowDataCommands.SHOW_PENDING_EMAILS);
        //            btnShowData.CommandName = (
        //                Cmd == ShowDataCommands.SHOW_PENDING_EMAILS ? ShowDataCommands.SHOW_PENDING_APPROVALS.ToString() : ShowDataCommands.SHOW_PENDING_EMAILS.ToString());
        //            btnShowData.Text = (
        //                Cmd == ShowDataCommands.SHOW_PENDING_EMAILS ? "Show pending approval requests" : "Show pending email verifications");
        //            pager.SetPageProperties(0, pager.PageSize, false);
        //            RetrievePendingUserRegistrations();
        //            Page.DataBind();
        //        }
        //    }
        //}

        #endregion


        #region User Access rights related


        #endregion


        #region UI related
        private void DisplayMessage(string Message, bool IsError)
        {
            plhMessage.Visible = true;
            lblMessage.Text = Message;
        }
        #endregion




        #region Business Logic only
        private void RetrievePendingEmailVerifications()
        {
            if (!IsViewDataLoaded)
            {
               //IsPostback won't help - the ObjectContainerDataSource_Selecting is called twice and the second time, it says 'IsPostBack = false'
                ViewData = RegisterUserBLL.GetPendingEmailChangeVerifications(this.AccountInfo);
                IsViewDataLoaded = true;
            }
        }

        #endregion


        #region Capture data From UI
        #endregion


        #region Private Properties
        private int UserId
        {
            get
            {
                return this.AccountInfo.UserId;
            }
        }

        private string StateFIPS
        {
            get { return AccountInfo.StateFIPS; }
        }

        private bool IsAdmin
        {
            get { return AccountInfo.IsAdmin; }
        }

        private Scope UserScope
        {
            get
            {
                return AccountInfo.Scope;
            }
        }
        private int UserScopeId
        {
            get
            {
                return AccountInfo.ScopeId;
            }
        }


        private bool? _IsShipDirector = null;
        private bool IsShipDirector
        {
            get
            {
                if (!_IsShipDirector.HasValue)
                    _IsShipDirector = LookupBLL.IsShipDirector(UserId, StateFIPS);

                return _IsShipDirector.Value;
            }
        }

        public IEnumerable<UserSummaryViewData> ViewData { get; set; }

        public bool ViewDataHasRows
        {
            get
            {
                return (ViewData != null && ViewData.Count() > 0);
            }
        }

        #endregion


        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        
        #region "View state events"
        //protected override void LoadViewState(object savedState)
        //{
        //    base.LoadViewState(savedState);
        //    IncludeAllPendingEmailVerifications = (bool)ViewState[VIEWSTATE_KEY_INCL_PENDING_EMAILS];
        //}

        //protected override object SaveViewState()
        //{
        //    ViewState[VIEWSTATE_KEY_INCL_PENDING_EMAILS] = IncludeAllPendingEmailVerifications;
        //    return base.SaveViewState();
        //}
        #endregion

    }
}


