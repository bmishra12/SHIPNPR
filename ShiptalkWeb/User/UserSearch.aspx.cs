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
    public partial class UserSearch : Page, IRouteDataPage
    {
        protected string SearchTerm = string.Empty;
        protected string SearchDisplayTerm = string.Empty;
        private const string VIEWSTATE_KEY_SearchTerm = "Search_Term";

        #region Properties


        #endregion



        #region Methods

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

                //if (StatesForUser != null)
                //{
                //    StatesForUser = StatesForUser.Union(AllStatesOption);
                //}

                ddlStates.DataSource = StatesForUser;
            }
        }

        /// <summary>
        /// Called when page is initialized for the first time.
        /// </summary>
        protected void InitializeView()
        {
            //Reset Search Textbox
            SearchTerm = string.Empty;

            SetupAdminLinksPanel();
            ShowHideStateSelection();

            Page.DataBind();
        }




        private bool IsAuthorizedForUniqueID
        {
            get
            {
                return (ApproverRulesBLL.IsApproverAtCMS(this.AccountInfo)
                    || ApproverRulesBLL.IsApproverForState(this.AccountInfo, this.AccountInfo.StateFIPS));
            }
        }

        private void SetupAdminLinksPanel()
        {
            //Approval by All Admins; At State Scope : Ship Directors - This is DefaultAdminRights
            //pendingRegistrations.Visible = AccessRulesBLL.HasDefaultAdminRights(this.AccountInfo);
            pendingRegistrations.Visible = ApproverRulesBLL.IsApprover(this.AccountInfo);

            pendingUniqueIds.Visible = IsAuthorizedForUniqueID;
            lbtnDownloadUniqueID.Visible = IsAuthorizedForUniqueID;

            //Add Users by all Admins, even at State Level.
            AddUserLink.Visible = IsAnAdminUser;
            Inactivity180.Visible = IsAnAdminUser;

            AdminLinksPanel.Visible = (pendingRegistrations.Visible | AddUserLink.Visible);
        }
        private bool IsAnAdminUser
        {
            get
            {
                //All Admins are Default Admins at their Scope Level.
                //However, for State Level, Ship Directors are Default Admins.
                if (AccountInfo.IsCMSLevel || AccountInfo.IsStateScope)
                {
                    return AccountInfo.IsAdmin;
                }
                else
                {
                    //For potential multi Regional Users such as Sub State and Agency Users 
                    //Atleast at one agency, they are admin. Thats all we can do for generalized IsAdmin search.
                    //For regional specific IsAdmin, this method must not be used.
                    UserViewData UserData = UserBLL.GetUser(AccountInfo.UserId);
                    foreach (UserRegionalAccessProfile regionalProfile in UserData.RegionalProfiles)
                    {
                        if (regionalProfile.IsAdmin)
                            return true;
                    }
                    return false;
                }
            }
        }

        protected bool ShowUserList
        {
            get
            {
                if (this.AccountInfo.IsStateScope | (this.AccountInfo.IsCMSScope))
                {
                    return this.AccountInfo.IsAdmin;
                }
                return false;
            }
        }



        //download the ApprovedUniquIdUserLists
        protected void DownloadApprovedUniquIdUserList_Click(object sender, EventArgs e)
        {
         IEnumerable<UserSummaryViewData> approvedList =   RegisterUserBLL.GetApprovedUniqueIdRequestsByStateFIPS(this.AccountInfo.StateFIPS);
         WriteToStream(approvedList);
        }

        //write the ApprovedUniquIdUserLists to excel sheet
        private void WriteToStream(IEnumerable<UserSummaryViewData>  approvedList)
        {
            //The tab delimitations
            string TabDelimitedFmt = "{0}\t{1}\t{2}\t{3}\n";

            //Set Headers using various Column names
            string Col1 = "State";
            string Col2 = "First name";
            string Col3 = "Last name";
            string Col4 = "CMS SHIP Unique ID";

            string Headers = string.Empty;

            //Create filename using time/date stamp
            string timeStamp = DateTime.Now.ToString("h_mm_ss_tt");
            string dateStamp = DateTime.Now.ToString("d_MMM_yyyy");
            string fileName = "UserList-" + dateStamp + "-" + timeStamp + ".xls";

            bool ShowStateColumn = this.AccountInfo.IsCMSLevel;

            Headers = string.Format(TabDelimitedFmt, Col1, Col2, Col3, Col4);
            
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
            foreach (UserSummaryViewData data in approvedList)
            {
                    RowVal = string.Format(TabDelimitedFmt,
                    data.StateName,
                    data.FirstName.ToCamelCasing(),
                    data.LastName.ToCamelCasing(),
                    "=concatenate(\"" + data.MedicareUniqueId + "\")"
                                );

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



        protected void Page_Init(object sender, EventArgs e)
        {
            pager.PagedControlID = listViewUsers.UniqueID;
        }
        #endregion



        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) InitializeView();

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchText.Text.Trim() == string.Empty)
            {
                NoSearchResultsMessage.Visible = true;
                SearchDisplayTerm = "BLANK text";
                return;
            }
            else
                SearchDisplayTerm = "&quot;" + SearchText.Text.Trim().EncodeHtml() + "&quot;";

            SearchTerm = SearchText.Text.Trim();

            pager.SetPageProperties(0, pager.PageSize, false);
               
               
            Page.DataBind();
        }



        protected bool IsCMSLevelUser
        {
            get
            {
                return this.AccountInfo.Scope.IsHigher(Scope.State);
            }
        }

        private void ShowHideStateSelection()
        {
            FilterByStates.Visible = IsCMSLevelUser;
            if (IsCMSLevelUser)
                PopulateStates();
        }


        private UserSearchSimpleParams CreateSearchParameters()
        {
            UserSearchSimpleParams searchParams = new UserSearchSimpleParams();
            //searchParams.FirstName = FirstName.Text.Trim();
            //searchParams.LastName = LastName.Text.Trim();
            searchParams.SearchText = SearchTerm;
            searchParams.SearchedById = UserId;

            if (SearchByState)
                searchParams.SearchByStateFIPS = ddlStates.SelectedValue;

            return searchParams;

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
            if (IsPostBack)
            {
                IEnumerable<UserSearchViewData> viewData = GetCleanData(UserBLL.SearchUsers(CreateSearchParameters()));
                if (viewData != null && viewData.Count() > 0)
                {
                    dataSourceUsers.DataSource = viewData;
                }
                else
                {
                    NoSearchResultsMessage.Visible = true;
                }
            }
        }

        #endregion

        private int UserId
        {
            get
            {
                return AccountInfo.UserId;
            }
        }


        protected IEnumerable<UserSearchViewData> GetCleanData(IEnumerable<UserSearchViewData> origViewData)
        {
            List<UserSearchViewData> newViewData = new List<UserSearchViewData>();
            origViewData.GroupBy(p => p.UserId).Where(w => w.Count() > 1).ToList().ForEach(f => { f.First().UserRoleText = GetMultiRegionalUserRoleText(f.First().Scope); f.First().RegionName = GetMultiRegionalUserRegionText(f.First().Scope, f.Count()); });
            origViewData.GroupBy(p => p.UserId).Where(w => w.Count() == 1).ToList().ForEach(f => { f.First().UserRoleText = GetRoleNameColumnValue(f.First()); });
            //origViewData.GroupBy(p => p.UserId).Where(w => w.Count() > 1).First().Single(s => (s.Scope.IsEqual(Scope.SubStateRegion) ? "Multi sub state user" : (s.Scope.IsEqual(Scope.Agency) ? "Multi agency user" : s.RegionName)));
            return origViewData.GroupBy(p => p.UserId).Select(x => x.First());


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

        //protected string GetRoleNameColumnValue(Scope scope, bool IsAdmin, bool IsShipDirector)
        //{
        //    if (IsShipDirector) return "Ship Director";
        //    else
        //        return LookupBLL.GetRoleNameUsingScope(scope, IsAdmin, (Descriptor?)null);
        //}

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion



        #region "View state events"
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            SearchTerm = (string)ViewState[VIEWSTATE_KEY_SearchTerm];
        }

        protected override object SaveViewState()
        {
            ViewState[VIEWSTATE_KEY_SearchTerm] = SearchTerm;
            return base.SaveViewState();
        }
        #endregion
    }
}
