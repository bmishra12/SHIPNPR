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
using Microsoft.Practices.Web.UI.WebControls;

using ShiptalkWeb.Routing;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;


namespace ShiptalkWeb
{
    public partial class PendingUniqueIdRequests : System.Web.UI.Page, IRouteDataPage
    {

        
        public enum ViewType
        {
            ShowAll = 1,
            Pending = 2,
            Revoked = 3
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsAuthorized)
                {
                    DisplayMessage("You are not authorized to view this page.", true);
                }
                else
                {
                   // pager.SetPageProperties(0, pager.PageSize, false);

                    RetrieveAndBindData(ViewType.Pending);
                    Page.DataBind();

                }
            }
        }

        private void RetrieveAndBindData(ViewType viewType)
        {
            RetrievePendingUserRegistrations(viewType);
            dataSourcePendingUsers.DataSource = ViewData;

            if (!(ViewData != null && ViewData.Count() > 0))
            {
                NoSearchResultsMessage.Visible = true;
            }
            else
            {
                if (IsAuthorized)
                lbtnDownload.Visible = true;
            }

        }

        private bool IsAuthorized
        {
            get
            {
                return ( ApproverRulesBLL.IsApproverAtCMS(this.AccountInfo) 
                    || this.AccountInfo.IsShipDirector );
            }
        }


        #region Postback event handlers
        protected void dataSourcePendingUsers_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            //if (IsPostBack)
            //{
            //    RetrieveAndBindData(false);
            //}
        }
        #endregion


        #region User Access rights related


        #endregion


        #region UI related
        private void DisplayMessage(string Message, bool IsError)
        {
            plhMessage.Visible = true;
            lblMessage.Text = Message;
        }


        protected void ShowAllUserList_Click(object sender, EventArgs e)
        {
            lblHeaderTitle.Text = "CMS Unique IDs";

            RetrieveAndBindData(ViewType.ShowAll);

            lbtnShowAll.Visible = false;

        }


        protected void ShowRevokedUserList_Click(object sender, EventArgs e)
        {
            lblHeaderTitle.Text = "CMS Revoked Unique IDs";

            RetrieveAndBindData(ViewType.Revoked);

            lbtnRevoke.Visible = false;

        }

        

        protected void DownloadUserList_Click(object sender, EventArgs e)
        {
            //pager.SetPageProperties(0, pager.PageSize, false);
            WriteToStream();
            //Page.DataBind();
        }


        private void WriteToStream()
        {
            //The tab delimitations
            string TabDelimitedFmt = "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\n";

            //Set Headers using various Column names
            string Col1 = "User Role";
            string Col2 = "First name";
            string Col3 = "Last name";
            string Col4 = "Primary email";
            string Col5 = "User ID";
            string Col6 = "Agency";
            string Col7 = "State";

            string Headers = string.Empty;

            //Create filename using time/date stamp
            string timeStamp = DateTime.Now.ToString("h_mm_ss_tt");
            string dateStamp = DateTime.Now.ToString("d_MMM_yyyy");
            string fileName = "UserList-" + dateStamp + "-" + timeStamp + ".xls";

            bool ShowStateColumn = this.AccountInfo.IsCMSLevel;

            if (ShowStateColumn)
                Headers = string.Format(TabDelimitedFmt, Col1, Col2, Col3, Col4, Col5, Col6, Col7);
            else
                Headers = string.Format(TabDelimitedFmt, Col1, Col2, Col3, Col4, Col5, Col6, string.Empty);


            HttpResponse response = HttpContext.Current.Response;

            // make sure nothing is in response stream 
            response.Clear();
            response.Charset = "";

            // set MIME type to be Excel file. 
            response.ContentType = "application/vnd.ms-excel";

            // add a header to response to force download (specifying filename) 
            response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", fileName));

            //Write Column Headers
            response.Write(Headers);

            //Write Content
            //fill values
            string RowVal = string.Empty;
            foreach (UserSummaryViewData data in ViewData)
            {
                if (ShowStateColumn)
                    RowVal = string.Format(TabDelimitedFmt,
                                GetRoleText(data.Scope, data.IsAdmin, data.IsShipDirector).ToCamelCasing(),
                                data.FirstName.ToCamelCasing(),
                                data.LastName.ToCamelCasing(),
                                data.PrimaryEmail,
                                data.UserId,
                                data.RegionName,
                                data.StateName);
                else
                    RowVal = string.Format(TabDelimitedFmt,
                            GetRoleText(data.Scope, data.IsAdmin, data.IsShipDirector).ToCamelCasing(),
                            data.FirstName.ToCamelCasing(),
                            data.LastName.ToCamelCasing(),
                            data.PrimaryEmail,
                            data.UserId,
                            data.RegionName,
                            string.Empty);

                response.Write(RowVal);
            }

            // Close response stream. 
            response.End();
        }


        protected string GetRoleText(Scope scope, bool IsAdmin, bool IsShipDirector)
        {
            if (IsShipDirector)
                return "Ship Director";
            else
                return LookupBLL.GetRoleNameUsingScope(scope, IsAdmin, (Descriptor?)null);
        }





        private IEnumerable<UserSummaryViewData> _ViewData { get; set; }
        public IEnumerable<UserSummaryViewData> ViewData
        {
            get
            {
                if (_ViewData == null)
                    _ViewData = FetchData(ViewType.Pending);

                return _ViewData;
            }
            set
            {
                _ViewData = value;
            }
        }



        private IEnumerable<UserSummaryViewData> FetchData(ViewType viewType)
        {
            if (viewType == ViewType.ShowAll)
                return RegisterUserBLL.GetAllPendingUniqueIdRequestsByState(this.AccountInfo.StateFIPS);

            else if (viewType == ViewType.Pending)
                return RegisterUserBLL.GetPendingUniqueIdRequestsForState(this.AccountInfo.StateFIPS);

            else if (viewType == ViewType.Revoked)
                return RegisterUserBLL.GetRevokedPendingUniqueIdRequestsByState(this.AccountInfo.StateFIPS);

            else return null;

        }
        #endregion




        #region Business Logic only
        private void RetrievePendingUserRegistrations(ViewType viewType)
        {
            ViewData = FetchData(viewType);
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


        #endregion


        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion
    }
}


