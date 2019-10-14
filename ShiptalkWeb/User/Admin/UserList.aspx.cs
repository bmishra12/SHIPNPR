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

using System.Web.Routing;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkWeb.Routing;

using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkCommon;



namespace ShiptalkWeb
{
    public partial class UserList : Page, IRouteDataPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsAuthorized())
                throw new ShiptalkException("Not authorized to view User list. You should be a CMS Admin/Ship Director/State Admin to view the user list.", false, "You are not authorized to view this page.");

            if (!IsPostBack) InitializeView();
        }



        protected void btnFilterByState_Click(object sender, EventArgs e)
        {
            pager.SetPageProperties(0, pager.PageSize, false);
            Page.DataBind();
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
            string Col7 = "CMS SHIP Unique ID";
            string Col8 = "State";

            string Headers = string.Empty;

            //Create filename using time/date stamp
            string timeStamp = DateTime.Now.ToString("h_mm_ss_tt");
            string dateStamp = DateTime.Now.ToString("d_MMM_yyyy");
            string fileName = "UserList-" + dateStamp + "-" + timeStamp + ".xls";

            bool ShowStateColumn = this.AccountInfo.IsCMSLevel;

            if (ShowStateColumn)
                Headers = string.Format(TabDelimitedFmt, Col1, Col2, Col3, Col4, Col5, Col6, Col7, Col8);
            else
                Headers = string.Format(TabDelimitedFmt, Col1, Col2, Col3, Col4, Col5, Col6,Col7, string.Empty);


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
            foreach (UserSearchViewData data in ViewData)
            {
                if (ShowStateColumn)
                    RowVal = string.Format(TabDelimitedFmt,
                                GetRoleText(data.Scope, data.IsAdmin, data.IsShipDirector).ToCamelCasing(),
                                data.FirstName.ToCamelCasing(),
                                data.LastName.ToCamelCasing(),
                                data.PrimaryEmail,
                                data.UserId,
                                data.RegionName,
                                "=concatenate(\"" + data.MedicareUniqueId + "\")",
                                data.StateName);
                else
                    RowVal = string.Format(TabDelimitedFmt,
                            GetRoleText(data.Scope, data.IsAdmin, data.IsShipDirector).ToCamelCasing(),
                            data.FirstName.ToCamelCasing(),
                            data.LastName.ToCamelCasing(),
                            data.PrimaryEmail,
                            data.UserId,
                            data.RegionName,
                            "=concatenate(\"" + data.MedicareUniqueId + "\")",
                            string.Empty
                            );

                response.Write(RowVal);
            }


            // Close response stream. 
            response.End();
        }


        /// <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void InitializeView()
        {
            ShowHideStateSelection();
            pager.SetPageProperties(0, pager.PageSize, false);
            Page.DataBind();
        }

        private bool SearchByState
        {
            get
            {
                if (ddlStates.Visible == true)
                {
                    return (ddlStates.SelectedIndex != 0);
                }
                else
                    return false;
            }
        }

        protected void dataSourceUsers_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            //if (IsPostBack)
            //{
            //IEnumerable<UserViewData> viewData = null;
            ViewData = FetchData();

            //if (SearchByState)
            //    viewData = UserBLL.GetAllUsers(UserId, ddlStates.SelectedValue);
            //else
            //    viewData = UserBLL.GetAllUsers(UserId, string.Empty);

            if (ViewData != null && ViewData.Count() > 0)
            {
                dataSourceUsers.DataSource = ViewData;
            }
            //}
            //else
            //{
            //    IEnumerable<UserViewData> viewData = UserBLL.GetAllUsers(UserId, string.Empty);
            //    if (viewData != null && viewData.Count() > 0)
            //    {
            //        dataSourceUsers.DataSource = viewData;
            //    }
            //}
        }

        private IEnumerable<UserSearchViewData> _ViewData { get; set; }
        private IEnumerable<UserSearchViewData> ViewData
        {
            get
            {
                if (_ViewData == null)
                    _ViewData = FetchData();

                return _ViewData;
            }
            set
            {
                _ViewData = value;
            }
        }


        protected void listViewUsers_PreRender(object sender, EventArgs e)
        {

            if (listViewUsers.Items.Count > 0)
            {
                lbtnDownload.Visible = true;
            }

        }

        private IEnumerable<UserSearchViewData> FetchData()
        {
            if (SearchByState)
                return StepUpData(UserBLL.GetAllUsers(UserId, ddlStates.SelectedValue));
            //return GetCleanData(UserBLL.GetAllUsers(UserId, ddlStates.SelectedValue));
            else
                return StepUpData(UserBLL.GetAllUsers(UserId, string.Empty));
            //return GetCleanData(UserBLL.GetAllUsers(UserId, string.Empty));
        }



        private int UserId
        {
            get
            {
                return AccountInfo.UserId;
            }
        }


        private void ShowHideStateSelection()
        {
            FilterByStatePanel.Visible = IsCMSLevelUser;
            if (IsCMSLevelUser)
                PopulateStates();
        }



        /// <summary>
        /// Populates states in the form
        /// </summary>
        protected void PopulateStates()
        {
            //Get all States but CMS State.
            if (IsCMSLevelUser)
            {
                string StateFIPSForCMS = State.GetStateFIPSForCMS();
                //List<KeyValuePair<string, string>> AllStatesOption = new List<KeyValuePair<string, string>>();
                //AllStatesOption.Add(new KeyValuePair<string,string>(StateFIPSForCMS, "All States"));


                IEnumerable<KeyValuePair<string, string>> StatesForUser = null;
                IEnumerable<KeyValuePair<string, string>> StatesData = State.GetStatesWithFIPSKey().Where(p => p.Key != StateFIPSForCMS);

                if (AccountInfo.Scope.IsEqual(Scope.CMSRegional))
                {
                    List<string> StateFIPSForCMSRegions = new List<string>();
                    IEnumerable<UserRegionalAccessProfile> profiles = UserCMSBLL.GetUserCMSRegionalProfiles(AccountInfo.UserId, false);
                    foreach (UserRegionalAccessProfile profile in profiles)
                    {
                        IEnumerable<string> CMSStateFIPS = LookupBLL.GetStatesForCMSRegion(profile.RegionId);
                        if (CMSStateFIPS != null)
                            StateFIPSForCMSRegions.AddRange(CMSStateFIPS);
                    }
                    if (StateFIPSForCMSRegions.Count > 0)
                    {
                        StatesForUser = (
                            from stFIPS in StatesData
                            from cmsStFIPS in StateFIPSForCMSRegions
                            where stFIPS.Key == cmsStFIPS
                            select stFIPS
                            );
                    }
                }
                else
                    StatesForUser = (from states in StatesData where (states.Key != State.GetStateFIPSForCMS()) select states);

                ddlStates.DataSource = StatesForUser;
            }
        }


        protected bool IsCMSLevelUser
        {
            get
            {
                return this.AccountInfo.Scope.IsHigher(Scope.State);
            }
        }


        protected string GetRoleText(Scope scope, bool IsAdmin, bool IsShipDirector)
        {
            if (IsShipDirector)
                return "Ship Director";
            else
                return LookupBLL.GetRoleNameUsingScope(scope, IsAdmin, (Descriptor?)null);
        }


        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion


        private bool IsAuthorized()
        {
            if (this.AccountInfo.IsCMSScope || this.AccountInfo.IsStateScope)
                return this.AccountInfo.IsAdmin;
            else
            {
                return false;
            }
        }


        #region Prevent Duplicates - clean up data
        protected IEnumerable<UserSearchViewData> StepUpData(IEnumerable<UserSearchViewData> origViewData)
        {
            if(origViewData != null && origViewData.Count() > 0)
                origViewData.ToList().ForEach(f => { f.UserRoleText = GetRoleText(f.Scope, f.IsAdmin, f.IsShipDirector); });

            return origViewData;
        }

        protected IEnumerable<UserSearchViewData> GetCleanData(IEnumerable<UserSearchViewData> origViewData)
        {
            if (origViewData != null && origViewData.Count() > 0)
            {
                origViewData.GroupBy(p => p.UserId).Where(w => w.Count() > 1).ToList().ForEach(f => { f.First().UserRoleText = GetMultiRegionalUserRoleText(f.First().Scope); f.First().RegionName = GetMultiRegionalUserRegionText(f.First().Scope, f.Count()); });
                origViewData.GroupBy(p => p.UserId).Where(w => w.Count() == 1).ToList().ForEach(f => { f.First().UserRoleText = GetRoleNameColumnValue(f.First()); });
                //origViewData.GroupBy(p => p.UserId).Where(w => w.Count() > 1).First().Single(s => (s.Scope.IsEqual(Scope.SubStateRegion) ? "Multi sub state user" : (s.Scope.IsEqual(Scope.Agency) ? "Multi agency user" : s.RegionName)));
                return origViewData.GroupBy(p => p.UserId).Select(x => x.First()).OrderBy(o => o.Scope.EnumValue<int>());
            }
            else
                return origViewData;
        }

        private string GetMultiRegionalUserRoleText(Scope scope)
        {
            if (scope.IsEqual(Scope.Agency))
                return "Multi agency user";
            else if (scope.IsEqual(Scope.SubStateRegion))
                return "Multi sub state user";
            else
                return string.Empty;
        }

        /// <summary>
        /// Returns number of available profiles as user friendly text
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="RecordCount"></param>
        /// <returns></returns>
        protected string GetMultiRegionalUserRegionText(Scope scope, int RecordCount)
        {
            if (scope.IsEqual(Scope.Agency))
                return string.Format("({0} agencies)", RecordCount);
            else if (scope.IsEqual(Scope.SubStateRegion))
                return string.Format("({0} sub states)", RecordCount);
            else
                return string.Empty;
        }

        protected string GetRoleNameColumnValue(UserSearchViewData viewData)
        {
            if (viewData.UserRoleText == string.Empty)
            {
                if (viewData.IsShipDirector) return "Ship Director";
                else
                    return LookupBLL.GetRoleNameUsingScope(viewData.Scope, viewData.IsAdmin, (Descriptor?)null);
            }
            else
                return viewData.UserRoleText;
        }

        #endregion

    }
}
