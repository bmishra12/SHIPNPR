using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Web.Routing;
using Microsoft.Practices.Web.UI.WebControls;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkWeb.Routing;

using ShiptalkLogic.BusinessObjects.UI;
using System.Web.UI.WebControls;


namespace ShiptalkWeb
{
    public partial class Inactivity180 : Page, IRouteDataPage
    {
        protected string SearchTerm = string.Empty;
        protected string SearchDisplayTerm = string.Empty;
        private const string VIEWSTATE_KEY_SearchTerm = "Search_Term";
        private const string usrSearchTypeKey = "usrSearchType";

        #region Properties

        protected SearchType usrSearchType { get { return (SearchType)ViewState[usrSearchTypeKey]; } set { ViewState[usrSearchTypeKey] = value; } }

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
            usrSearchType = SearchType.Recent;
 
            //ShowHideStateSelection();
            SetupAdminLinksPanel();

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




        protected void listViewUsers_OnItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            if (String.Equals(e.CommandName, "Allow"))
            {
                // Verify that the employee ID is not already in the list. If not, add the
                // employee to the list.
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                string employeeID =
                  listViewUsers.DataKeys[dataItem.DisplayIndex].Value.ToString();

                if (UserBLL.ActivateUserWith180DaysInacitvity(Int32.Parse(employeeID), this.AccountInfo.UserId) )
                  {
                      usrSearchType = SearchType.Recent;

                      pager.SetPageProperties(0, pager.PageSize, false);


                      //Page.DataBind();
                      listViewUsers.DataBind();
                  }

            }

            if (String.Equals(e.CommandName, "Deny"))
            {
                // Verify that the employee ID is not already in the list. If not, add the
                // employee to the list.
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                string employeeID =
                  listViewUsers.DataKeys[dataItem.DisplayIndex].Value.ToString();

                //if (SelectedEmployeesListBox.Items.FindByValue(employeeID) == null)
                //{
                //    ListItem item = new ListItem(e.CommandArgument.ToString(), employeeID);
                //    SelectedEmployeesListBox.Items.Add(item);
                //}
            }
        }

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

            usrSearchType = SearchType.Keyword;

            pager.SetPageProperties(0, pager.PageSize, false);
               
               
            //Page.DataBind();
            listViewUsers.DataBind();
        }



        protected bool IsCMSLevelUser
        {
            get
            {
                return this.AccountInfo.Scope.IsHigher(Scope.State);
            }
        }


        //Prakash 01/11/2013  : State ADMIN can See the 180 Days Inactive Link and Activate the Inactive Users


        private void SetupAdminLinksPanel()
        {
            //Approval by All Admins; At State Scope : Ship Directors - This is DefaultAdminRights
            
            FilterByStates.Visible = IsCMSLevelUser;
            Inactivity180Link.Visible = IsAnAdminUser;
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
                  IList<UserSearchViewData> usrs = null;

            switch (usrSearchType)
            {
                case SearchType.Keyword:
                    {
                             usrs = new List<UserSearchViewData>(  GetCleanData(UserBLL.SearchUsersFor180dInactivity(CreateSearchParameters()))
                                                                            );
                        //}
                        dataSourceUsers.DataSource = usrs;

                        break;
                    }
                case SearchType.Recent:
                    {
                        usrs = new List<UserSearchViewData>(UserBLL.GetUsersFor180dInactivity(UserId));
                        dataSourceUsers.DataSource = usrs;

                        break;
                    }
            }

            NoSearchResultsMessage.Visible = (usrs.Count == 0);


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

        #region Sub Types

        protected enum SearchType
        {
            Keyword,
            Recent
        }

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
