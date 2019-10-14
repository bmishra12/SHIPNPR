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
using ShiptalkWeb.Routing;
using System.Web.Routing;
using System.Transactions;
using Microsoft.Practices.Web.UI.WebControls;

using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkLogic.BusinessLayer;
using ShiptalkCommon;



namespace ShiptalkWeb
{
    public partial class UserView : System.Web.UI.Page, IRouteDataPage
    {


        #region Properties
        private int UserId { get { return this.AccountInfo.UserId; } }
        public UserViewData userProfileViewData { get; set; }
        private const string DISPLAY_MESSAGE_PLACEHOLDER_ID = "plhMessage";
        private const string DISPLAY_MESSAGE_LABEL_ID = "lblMessage";
        //public RegionalProfileViewData regionalProfileViewData { get; set; }

        private int _UserProfileUserId;
        protected int UserProfileUserId
        {
            get
            {
                if (RouteData.Values[UserProfileUserIdKey] == null) throw new ShiptalkException("User View requested without UserId in the Route Data.", false, new ArgumentNullException("RouteData.UserId"));
                return int.Parse(RouteData.Values[UserProfileUserIdKey].ToString());
            }
            set
            {
                _UserProfileUserId = value;
            }
        }

        protected bool IsEditAccessAllowed()
        {
            //While User is visiting his own profile, deny edit, else check for access rights
            if (UserProfileUserId != this.AccountInfo.UserId)
            {
                return AccessRulesBLL.CanEditUserProfile(userProfileViewData, AdminViewData);
            }
                
            return false;
                
        }
        protected bool IsDeleteAccessAllowed()
        {
            //While User is visiting his own profile, deny edit, else check for access rights
            if (UserProfileUserId != this.AccountInfo.UserId)
            {
                return AdminViewData.IsCMSAdmin || AdminViewData.IsStateAdmin;
            }

            return false;

        }
        protected bool IsUnlockAccessAllowed()
        {
            //While User is visiting his own profile, deny edit, else check for access rights
            if (UserProfileUserId != this.AccountInfo.UserId)
            {
                return AdminViewData.IsCMSAdmin ;
            }

            return false;

        }
        
        protected bool IsEditAccessAllowed(int RegionId)
        {
            bool IsAllowed = false;

            //return AccessRulesBLL.CanEditUserProfile(this.userProfileViewData, UserBLL.GetUser(this.AccountInfo.UserId));
            if (userProfileViewData.IsUserSubStateRegionalScope)
            {
                UserRegionalAccessProfile subStateProfile = userProfileViewData.RegionalProfiles.Where( p=> p.RegionId == RegionId).FirstOrDefault();
                if (subStateProfile != null)
                {
                    IsAllowed = AccessRulesBLL.CanEditSubStateUser(subStateProfile.RegionId,
                        subStateProfile.IsAdmin,
                        userProfileViewData.StateFIPS,
                        subStateProfile.IsApproverDesignate,
                        AdminViewData);
                }
            }
            else if (userProfileViewData.IsUserAgencyScope)
            {
                UserRegionalAccessProfile AgencyProfile = userProfileViewData.RegionalProfiles.Where(p => p.RegionId == RegionId).FirstOrDefault();
                if (AgencyProfile != null)
                {
                    IsAllowed = AccessRulesBLL.CanEditAgencyUser(AgencyProfile.RegionId,
                        AgencyProfile.IsAdmin,
                        userProfileViewData.StateFIPS,
                        AgencyProfile.IsApproverDesignate,
                        AdminViewData);
                }
                
            }
            else if (userProfileViewData.IsUserCMSRegionalScope)
            {
                UserRegionalAccessProfile CMSRegionalProfile = userProfileViewData.RegionalProfiles.Where(p => p.RegionId == RegionId).FirstOrDefault();
                if (CMSRegionalProfile != null)
                {
                    IsAllowed = AccessRulesBLL.CanEditUserProfile(userProfileViewData, AdminViewData);
                }
            }

            return IsAllowed;
        }

        protected bool IsViewAccessAllowed(int RegionId)
        {
            bool IsAllowed = false;

            //return AccessRulesBLL.CanEditUserProfile(this.userProfileViewData, UserBLL.GetUser(this.AccountInfo.UserId));
            if (userProfileViewData.IsUserSubStateRegionalScope)
            {
                UserRegionalAccessProfile subStateProfile = userProfileViewData.RegionalProfiles.Where(p => p.RegionId == RegionId).FirstOrDefault();
                if (subStateProfile != null)
                {
                    IsAllowed = AccessRulesBLL.CanViewSubStateUser(subStateProfile.RegionId,
                        subStateProfile.IsAdmin,
                        userProfileViewData.StateFIPS,
                        AdminViewData);
                }
            }
            else if (userProfileViewData.IsUserAgencyScope)
            {
                UserRegionalAccessProfile AgencyProfile = userProfileViewData.RegionalProfiles.Where(p => p.RegionId == RegionId).FirstOrDefault();
                if (AgencyProfile != null)
                {
                    IsAllowed = AccessRulesBLL.CanViewAgencyUser(AgencyProfile.RegionId,
                        AgencyProfile.IsAdmin,
                        userProfileViewData.StateFIPS,
                        AdminViewData);
                }
            }
            else if (userProfileViewData.IsUserCMSRegionalScope)
            {
                UserRegionalAccessProfile CMSRegionalProfile = userProfileViewData.RegionalProfiles.Where(p => p.RegionId == RegionId).FirstOrDefault();
                if (CMSRegionalProfile != null)
                {
                    IsAllowed = AccessRulesBLL.CanViewUserProfile(userProfileViewData, AdminViewData);
                }
            }

            return IsAllowed;

        }

        private bool? _IsAddAccessAllowed = null;
        protected bool IsAddAccessAllowed
        {
            get
            {
                if (!_IsAddAccessAllowed.HasValue)
                {
                    if (UserProfileUserId != this.AccountInfo.UserId)
                    {
                        _IsAddAccessAllowed = AccessRulesBLL.CanAddUser(AdminViewData, this.userProfileViewData.Scope, this.userProfileViewData.StateFIPS);
                    }
                    
                    //User visiting their own profile.
                    _IsAddAccessAllowed = false;
                }
                //_IsEditAccessAllowed = AccessRulesBLL.IsProfileEditable(this.userProfileViewData, this.AccountInfo);

                return _IsAddAccessAllowed.Value;
            }
        }

        private UserViewData _AdminViewData = null;
        private UserViewData AdminViewData
        {
            get
            {
                if (_AdminViewData == null)
                {
                    _AdminViewData = UserBLL.GetUser(this.AccountInfo.UserId);
                }

                return _AdminViewData;
            }
        }

        public string RegionListViewRegionName
        {
            get
            {
                return userProfileViewData.Scope.Description();
            }
        }

        #endregion


        #region Private Constants
        private const string UserProfileUserIdKey = "Id";
        private const string EDIT_CMD = "edit";
        private const string DELETE_CMD = "delete";
        private const string UNLOCK_CMD = "unlock";
        #endregion




        protected void Page_Load(object sender, EventArgs e)
        {
            FetchUserData();

            if (!IsAuthorized())
                throw new ShiptalkException(string.Format("User {0} is not authorized to view User information of {1}", this.AccountInfo.UserId, UserProfileUserId), false, "You are not authorized to view the User information.");

            InitializeView();
        }

        private void InitializeView()
        {
            Page.DataBind();
        }

        
        private void FetchUserData()
        {
            userProfileViewData = UserBLL.GetUser(UserProfileUserId);
        }

        private UserViewData _ViewerUserData = null;
        public UserViewData ViewerUserData
        {
            get
            {
                if (_ViewerUserData == null)
                    _ViewerUserData = UserBLL.GetUser(this.AccountInfo.UserId);

                return _ViewerUserData;
            }
        }


        private bool IsAuthorized()
        {
            return AccessRulesBLL.CanViewUserProfile(userProfileViewData, ViewerUserData);
            //return AccessRulesBLL.CheckReadOnlyAccess(this.AccountInfo, this.userProfileViewData);
        }

        #region Page wired events
        protected void EditUserCommand(object sender, CommandEventArgs e)
        {
            switch (e.CommandArgument.ToString().ToLower().Trim())
            {
                case EDIT_CMD:
                    RouteController.RouteTo(RouteController.UserEdit(UserProfileUserId), true);
                    break;
                case DELETE_CMD:
                    HandleDeleteUser();
                    break;
                case UNLOCK_CMD:
                    HandleUnlockUser();
                    break;
                default:
                    throw new ShiptalkException("Unknown command sent during postback in Approval page.", false);
            }
        }

        protected string GetStateName()
        {
            return userProfileViewData.StateName;
        }

        protected string GetUserScopeForRegionalUser(bool IsAdmin)
        {
            return LookupBLL.GetRoleNameUsingScope(userProfileViewData.Scope, IsAdmin, (Descriptor?)null);
        }
        protected string GetEditRoute(int RegionId)
        {
            if (userProfileViewData.IsUserSubStateRegionalScope)
            {
                return RouteController.UserSubStateProfileEdit(UserProfileUserId, RegionId);
            }
            else if (userProfileViewData.IsUserAgencyScope)
            {
                return RouteController.UserAgencyProfileEdit(UserProfileUserId, RegionId);
            }
            else
                return string.Empty;
        }
        protected string GetAddRoute()
        {
            if (userProfileViewData.IsUserSubStateRegionalScope)
            {
                return RouteController.UserSubStateProfileAdd(UserProfileUserId);
            }
            else if (userProfileViewData.IsUserAgencyScope)
            {
                return RouteController.UserAgencyProfileAdd(UserProfileUserId);
            }
            else
                return string.Empty;
        }
        protected string GetViewRoute(int RegionId)
        {
            if (userProfileViewData.IsUserSubStateRegionalScope)
            {
                return RouteController.UserSubStateProfileView(UserProfileUserId, RegionId);
            }
            else if (userProfileViewData.IsUserAgencyScope)
            {
                return RouteController.UserAgencyProfileView(UserProfileUserId, RegionId);
            }
            else
                return string.Empty;
        }
        protected bool IsMultiAgencyAndDefaultAgencyExist
        {
            get
            {
                if (userProfileViewData.Scope.IsEqual(Scope.Agency))
                {
                    if (userProfileViewData.IsMultiAgencyUser)
                    {
                        return (userProfileViewData.DefaultAgencyIdOfUser > 0);
                    }
                    else
                        return true;

                }
                else
                    return false;
            }
        }

        protected bool IsMultiSubStateAndDefaultSubStateExist
        {
            get
            {
                if (userProfileViewData.Scope.IsEqual(Scope.SubStateRegion))
                {
                    if (userProfileViewData.IsMultiSubStateUser)
                    {
                        return (userProfileViewData.DefaultSubStateRegionIdOfUser > 0);
                    }
                    else
                        return true;
                }
                else
                    return false;
            }
        }
        protected bool ShowRoleInformation
        {
            get
            {
                if (userProfileViewData.IsCMSLevel || userProfileViewData.IsUserStateScope)
                    return true;
                else if (userProfileViewData.IsUserSubStateRegionalScope)
                {
                    return (!userProfileViewData.IsMultiSubStateUser);
                }
                else //Agency 
                {
                    return (!userProfileViewData.IsMultiAgencyUser);
                }
            }
        }
        protected string UserProfileRoleTitle
        {
            get
            {
                //For Agency and Sub State Users, ShowRoleInformation is called by the ASPX page.
                //When ShowRoleInformation is true, the User Profile level Title is displayed.
                //For Non-multi agency or multi sub state Users, the User Profile Level title is applicable.
                //For multi Agency or multi Sub State Users, Profile Level title is not available because they could be admin in one where as User in another region.
                if (userProfileViewData.IsUserAgencyScope || userProfileViewData.IsUserSubStateRegionalScope)
                {
                    Scope scope = userProfileViewData.Scope;
                    if (userProfileViewData.RegionalProfiles != null && userProfileViewData.RegionalProfiles.Count > 0)
                        return LookupBLL.GetRoleNameUsingScope(scope, userProfileViewData.RegionalProfiles[0].IsAdmin, (Descriptor?)null);
                    else
                        return string.Empty;
                }
                else
                {
                    return userProfileViewData.RoleTitle;
                }
            }
        }
        protected string UserProfileRoleDescription
        {
            get
            {
                //For Agency and Sub State Users, ShowRoleInformation is called by the ASPX page.
                //When ShowRoleInformation is true, the User Profile level Description is displayed.
                //For Non-multi agency or multi sub state Users, the User Profile Level description is applicable.
                //For multi Agency or multi Sub State Users, Profile Level description is not available because they could be admin in one where as User in another region.
                if (userProfileViewData.IsUserAgencyScope || userProfileViewData.IsUserSubStateRegionalScope)
                {
                    Scope scope = userProfileViewData.Scope;
                    if (userProfileViewData.RegionalProfiles != null && userProfileViewData.RegionalProfiles.Count > 0)
                        return LookupBLL.GetRoleDescriptionUsingScope(scope, userProfileViewData.IsAdmin);
                    else
                        return string.Empty;
                }
                else
                {
                    return userProfileViewData.RoleDescription;
                }
            }
        }
        protected string GetScopeDisplayText
        {
            get
            {
                if (userProfileViewData.IsUserAgencyScope)
                    return "Agency";
                else if (userProfileViewData.IsUserSubStateRegionalScope)
                    return "Sub State Region";
                else
                    return string.Empty;
            }

        }
        protected string CanApproveUsersString
        {
            get
            {
                bool? CanApprove = null;

                //Only admins are approvers.
                if (!userProfileViewData.IsAdmin)
                    return "No";

                if (userProfileViewData.IsShipDirector || userProfileViewData.IsCMSAdmin)
                    CanApprove = true;
                else if (userProfileViewData.IsUserStateScope)
                    CanApprove = userProfileViewData.IsStateApproverDesignate;
                else if (userProfileViewData.IsUserCMSScope)
                    CanApprove = userProfileViewData.IsCMSApproverDesignate;

                if (CanApprove.HasValue)
                    return CanApprove.Value ? "Yes" : "No";
                else
                    return string.Empty;
            }

        }

        protected void dataSourceUserView_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceUserView.DataSource = userProfileViewData;
        }

        protected void dataSourceRegionalProfiles_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceRegionalProfiles.DataSource = userProfileViewData.RegionalProfiles;
        }

        protected void listViewUserRegionalProfiles_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //if (!IsAddRouteSet)
            //{
            //    HtmlAnchor link = listViewUserRegionalProfiles.FindControl("AddNewRegionLink") as HtmlAnchor;
            //    link.HRef = GetAddRoute();
            //    link.Title = "Add User to a " + GetScopeDisplayText;
            //    link.InnerText = link.Title;
            //    IsAddRouteSet = true;
            //}
        }
        private bool _IsAddRouteSet = false;
        protected bool IsAddRouteSet { get { return _IsAddRouteSet; } set { _IsAddRouteSet = value; } }
        #endregion



        #region "View/Edit Rights used by aspx page"
        List<KeyValuePair<int, KeyValuePair<bool, bool>>> _ViewEditColl = null;
        protected List<KeyValuePair<int, KeyValuePair<bool, bool>>> ViewEditColl
        {
            get
            {
                if (_ViewEditColl == null)
                {
                    _ViewEditColl = new List<KeyValuePair<int, KeyValuePair<bool, bool>>>();
                    PopulateViewEditAccess(ref _ViewEditColl);
                }

                return _ViewEditColl;
            }
        }
        protected bool CanViewRegion(int RegionId)
        {
            KeyValuePair<bool, bool>? KeyPairOfRegion = (from match in ViewEditColl where match.Key == RegionId select match.Value).FirstOrDefault();
            if (KeyPairOfRegion.HasValue)
            {
                return KeyPairOfRegion.Value.Key;
            }
            return false;
        }
        protected bool CanEditRegion(int RegionId)
        {
            KeyValuePair<bool, bool>? KeyPairOfRegion = (from match in ViewEditColl where match.Key == RegionId select match.Value).FirstOrDefault();
            if (KeyPairOfRegion.HasValue)
            {
                return KeyPairOfRegion.Value.Value;
            }
            return false;
        }

        private void PopulateViewEditAccess(ref List<KeyValuePair<int, KeyValuePair<bool, bool>>> ViewEditColl)
        {
            string StateFIPS = userProfileViewData.StateFIPS;
            foreach (UserRegionalAccessProfile profile in userProfileViewData.RegionalProfiles)
            {
                if (userProfileViewData.IsUserAgencyScope)
                {
                    var CanView = AccessRulesBLL.CanViewAgencyUser(profile.RegionId, profile.IsAdmin, StateFIPS, AdminViewData);
                    var CanEdit = AccessRulesBLL.CanEditAgencyUser(profile.RegionId, profile.IsAdmin, StateFIPS, profile.IsApproverDesignate, AdminViewData);
                    ViewEditColl.Add(new KeyValuePair<int, KeyValuePair<bool, bool>>(profile.RegionId, new KeyValuePair<bool, bool>(CanView, CanEdit)));
                }
                else if (userProfileViewData.IsUserSubStateRegionalScope)
                {
                    var CanView = AccessRulesBLL.CanViewSubStateUser(profile.RegionId, profile.IsAdmin, StateFIPS, AdminViewData);
                    var CanEdit = AccessRulesBLL.CanEditSubStateUser(profile.RegionId, profile.IsAdmin, StateFIPS, profile.IsApproverDesignate, AdminViewData);
                    ViewEditColl.Add(new KeyValuePair<int, KeyValuePair<bool, bool>>(profile.RegionId, new KeyValuePair<bool, bool>(CanView, CanEdit)));
                }
                else if (userProfileViewData.IsUserCMSRegionalScope)
                {
                    var CanView = AccessRulesBLL.CanViewUserProfile(userProfileViewData, AdminViewData);
                    var CanEdit = AccessRulesBLL.CanEditUserProfile(userProfileViewData, AdminViewData);
                    ViewEditColl.Add(new KeyValuePair<int, KeyValuePair<bool, bool>>(profile.RegionId, new KeyValuePair<bool, bool>(CanView, CanEdit)));
                }
            }
        }

        private void HandleDeleteUser()
        {
            string FailureReason;
            if (UserBLL.DeleteUser(UserProfileUserId, out FailureReason))
            {
                lblTitle.Text = "Success";
                dv3colMessage.Text = "The User has been deleted successfully!";
                BackToUserSearch.Visible = true;
                BackToUserSearch.DataBind();
                UserDataPanel.Visible = false;
            }
            else
            {
                formView.DataBind();
                FailureReason = "Unable to delete User. Reason: " + FailureReason;
                DisplayMessage(FailureReason, true);
            }


        }

        //Prakash 11/13/2012 - UnlockUser , if the user gets Locked by Changing their Password more than once in 24 Hours..

        private void HandleUnlockUser()
        {
            string FailureReason;
            if (UserBLL.UnlockUser(UserProfileUserId, out FailureReason))
            {
                lblTitle.Text = "Success";
                dv3colMessage.Text = "The User has been unlocked successfully!";
                BackToUserSearch.Visible = true;
                BackToUserSearch.DataBind();
                UserDataPanel.Visible = false;
            }
            else
            {
                formView.DataBind();
                FailureReason = "Unable to unlock User. Reason: " + FailureReason;
                DisplayMessage(FailureReason, true);
            }            
        }
        #endregion


        #region "Display message"
        private void DisplayMessage(string message, bool IsError)
        {
            var messagepanel = formView.FindControl(DISPLAY_MESSAGE_PLACEHOLDER_ID) as PlaceHolder;
            if(messagepanel != null)
            {
                messagepanel.Visible = true;

                var messageLabel = formView.FindControl(DISPLAY_MESSAGE_LABEL_ID) as Label;
                if (messageLabel != null)
                {
                    messageLabel.Text = message;

                    if (IsError)
                        messageLabel.CssClass = "required";
                    else
                        messageLabel.CssClass = "info";
                }
            }
        }
        #endregion

        #region "View state events"
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            UserProfileUserId = (int)ViewState["UserProfileUserId"];
        }

        protected override object SaveViewState()
        {
            ViewState["UserProfileUserId"] = UserProfileUserId;
            return base.SaveViewState();
        }
        #endregion


        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion
    }
}
