using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Collections.Generic;
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
    public partial class UserSubStateProfileView : System.Web.UI.Page, IRouteDataPage
    {

        #region Private Constants
        private const string QueryStringKey = "params";
        private static readonly string[] UserID_SubStateRegionID_Seperator = { "-" };
        private const string EDIT_CMD = "edit";
        private const string DEL_CMD = "delete";
        #endregion

        #region Private/Protected properties

        protected int ProfileUserId { get; set; }
        protected int ProfileSubStateRegionId { get; set; }


        
        private UserViewData _UserData = null;
        private UserViewData UserData
        {
            get
            {
                if (_UserData == null)
                    FetchData();
                return _UserData;
            }
            set
            {
                _UserData = value;
            }
        }

        private UserRegionalAccessProfile _UserSubStateRegionData = null;
        private UserRegionalAccessProfile UserSubStateRegionData
        {
            get
            {
                if (_UserSubStateRegionData == null)
                    FetchData();

                return _UserSubStateRegionData;
            }
            set
            {
                _UserSubStateRegionData = value;
            }
        }


        private bool? _IsEditAccessAllowed = null;
        protected bool IsEditAccessAllowed
        {
            get
            {
                if (!_IsEditAccessAllowed.HasValue)
                {
                    _IsEditAccessAllowed = AccessRulesBLL.CanEditSubStateUser(UserSubStateRegionData.RegionId, UserSubStateRegionData.IsAdmin, UserData.StateFIPS, UserSubStateRegionData.IsApproverDesignate, ViewerUserData);
                    //_IsEditAccessAllowed = AccessRulesBLL.IsProfileEditable(this.UserData, this.AccountInfo);
                }

                return _IsEditAccessAllowed.Value;
            }
        }
        protected string GetSubStateRegionName
        {
            get
            {
                return UserSubStateRegionData.RegionName;
            }
        }
        protected string GetStateName
        {
            get
            {
                return UserData.StateName;
            }
        }
        protected string GetRoleName
        {
            get
            {
                return LookupBLL.GetRoleNameUsingScope(UserData.Scope, UserSubStateRegionData.IsAdmin, (Descriptor?)null);
            }
        }
        protected string IsDefaultRegionText
        {
            get
            {
                return (UserSubStateRegionData.IsDefaultRegion ? "Yes" : "No");
            }
        }
        protected string GetUserFullName
        {
            get
            {
                return UserData.FullName;
            }
        }
        protected IEnumerable<string> GetDescriptorNames
        {
            get
            {
                var DescriptorIds = UserSubStateRegionData.DescriptorIDList;
                List<string> DescriptorNames = new List<string>();
                foreach (int DescriptorId in DescriptorIds)
                    DescriptorNames.Add(LookupBLL.GetDescriptorName(DescriptorId));
                
                return DescriptorNames;
            }
        }
        protected bool CanApproveUserRegistrations
        {
            get
            {
                //int ApproverDescriptorID = Descriptor.UserRegistrations_Approver.EnumValue<int>();
                //int? searchResult = (from DescriptorID in UserSubStateRegionData.DescriptorIDList where DescriptorID == ApproverDescriptorID select DescriptorID).FirstOrDefault();
                //return (searchResult.HasValue && searchResult.Value > 0);
                return UserSubStateRegionData.IsApproverDesignate;
            }
        }
        protected bool IsMultiSubStateUser
        {
            get
            {
                return UserData.IsMultiSubStateUser;
            }
        }
        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {
                InitializeData();
                if (IsAuthorized())
                {
                    InitializeView();
                }
        }

        private void InitializeData()
        {
            if (!IsPostBack)
            {
                ProfileUserId = GetUserIdFromRouteRequest();
                ProfileSubStateRegionId = GetSubStateRegionIdFromRouteRequest();
                FetchData();
            }
        }
        private void InitializeView()
        {
            if (!IsPostBack)
            {
                Page.DataBind();
            }
        }

        private int GetUserIdFromRouteRequest()
        {
            if (RouteData.Values[QueryStringKey] + string.Empty == string.Empty)
                throw new ShiptalkException("User Sub State profile requested without proper UserID parameter in the Route Data.", false, new ArgumentNullException("RouteData.params"));

            int profileUserId;
            if (int.TryParse(RouteData.Values[QueryStringKey].ToString().Split(UserID_SubStateRegionID_Seperator, StringSplitOptions.None)[0], out profileUserId))
                return profileUserId;
            else
                throw new ShiptalkException("User Sub State profile requested without proper UserID parameter in the Route Data.", false, new ArgumentNullException("RouteData.params"));
        }

        private int GetSubStateRegionIdFromRouteRequest()
        {
            if (RouteData.Values[QueryStringKey] + string.Empty == string.Empty)
                throw new ShiptalkException("User Sub State profile requested without proper SubStateRegionID parameter in the Route Data.", false, new ArgumentNullException("RouteData.params"));

            int SubStateRegionId;
            if (int.TryParse(RouteData.Values[QueryStringKey].ToString().Split(UserID_SubStateRegionID_Seperator, StringSplitOptions.None)[1], out SubStateRegionId))
                return SubStateRegionId;
            else
                throw new ShiptalkException("User Sub State profile requested without proper SubStateRegionID parameter in the Route Data.", false, new ArgumentNullException("RouteData.params"));

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


        private void FetchData()
        {
            UserData = UserBLL.GetUser(ProfileUserId);
            if (UserData == null)
                throw new ShiptalkException(string.Format("Profile of UserID {0} not found for User Sub State Regional Profile Requested by User ID {1}.", ProfileUserId, this.AccountInfo.UserId), false, "Not enough information is available to process the request. Please check the URL you typed or contact support for assistance.");

            UserSubStateRegionData = UserData.RegionalProfiles.Where(prof => prof.RegionId == ProfileSubStateRegionId).FirstOrDefault();
            if (UserSubStateRegionData == null)
                throw new ShiptalkException(string.Format("User: {0} does not have a Sub State profile as requested by User: {1}.", ProfileUserId, this.AccountInfo.UserId), false, "Not enough information is available to process the request. Please check the URL you typed or contact support for assistance.");
        }



        #region Page wired events
        protected void UserCommand(object sender, CommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case EDIT_CMD:
                    RouteController.RouteTo(RouteController.UserSubStateProfileEdit(ProfileUserId, ProfileSubStateRegionId), true);
                    break;
                case DEL_CMD:
                    break;
                default:
                    throw new ShiptalkException(string.Format("Uknown UserCommand for Sub State Region postback. Command: {0}, UserId: {1}", e.CommandName, ProfileUserId), false, "Sorry. We're unable to process your request. Please contact support for assistance.");
            }
        }
        protected void dataSourceSubStateUserView_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceSubStateUserView.DataSource = UserSubStateRegionData;
        }
        #endregion




        #region "View state events"
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            ProfileUserId = (int)ViewState["UserProfileUserId"];
            ProfileSubStateRegionId = (int)ViewState["UserSubStateRegionId"];
        }

        protected override object SaveViewState()
        {
            ViewState["UserProfileUserId"] = ProfileUserId;
            ViewState["UserSubStateRegionId"] = ProfileSubStateRegionId;
            return base.SaveViewState();
        }
        #endregion


        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion


        #region Authorization happens here
        private bool IsAuthorized()
        {
            //return AccessRulesBLL.CheckReadOnlyAccess(this.AccountInfo, UserData);
            bool AuthResult = AccessRulesBLL.CanViewSubStateUser(UserSubStateRegionData.RegionId, UserSubStateRegionData.IsAdmin, UserData.StateFIPS, ViewerUserData);

            if (!AuthResult)
                ShiptalkException.ThrowSecurityException(string.Format("Access denied. User :{0} cannot view {1}.", this.AccountInfo.UserId, UserData.UserId), "You are not authorized to view the User information.");

            return AuthResult;

        }
        #endregion

    }
}
