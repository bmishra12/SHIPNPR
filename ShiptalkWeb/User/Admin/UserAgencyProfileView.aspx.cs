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
    public partial class UserAgencyProfileView : System.Web.UI.Page, IRouteDataPage
    {

        #region Private Constants
        private const string QueryStringKey = "params";
        private static readonly string[] UserID_AgencyID_Seperator = { "-" };

        private const string EDIT_CMD = "edit";
        private const string DEL_CMD = "delete";
        #endregion

        #region Private/Protected properties

        protected int ProfileUserId { get; set; }
        protected int ProfileAgencyId { get; set; }
        
        private UserViewData _ViewerUserData = null;
        private UserViewData _UserData = null;
        private bool? _IsEditAccessAllowed = null;
        
        protected string GetAgencyName
        {
            get
            {
                return UserAgencyData.RegionName;
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
                return LookupBLL.GetRoleNameUsingScope(UserData.Scope, UserAgencyData.IsAdmin, (Descriptor?)null);
            }
        }
        protected string IsDefaultRegionText
        {
            get
            {
                return (UserAgencyData.IsDefaultRegion ? "Yes" : "No");
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
                var DescriptorIds = UserAgencyData.DescriptorIDList;
                List<string> DescriptorNames = new List<string>();
                foreach (int DescriptorId in DescriptorIds)
                    DescriptorNames.Add(LookupBLL.GetDescriptorName(DescriptorId));

                return DescriptorNames;
            }
        }
        protected bool IsMultiAgencyUser
        {
            get
            {
                return UserData.IsMultiAgencyUser;
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
                ProfileAgencyId = GetAgencyIdFromRouteRequest();
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
       


        private void FetchData()
        {
            UserData = UserBLL.GetUser(ProfileUserId);
            if (UserData == null)
                throw new ShiptalkException(string.Format("Profile of UserID {0} not found for User Agency Profile Requested by User ID {1}.", ProfileUserId, this.AccountInfo.UserId), false, "Not enough information is available to process the request. Please check the URL you typed or contact support for assistance.");

            UserAgencyData = UserData.RegionalProfiles.Where(prof => prof.RegionId == ProfileAgencyId).FirstOrDefault();
            if (UserAgencyData == null)
                throw new ShiptalkException(string.Format("User: {0} does not have a Agency profile as requested by User: {1}.", ProfileUserId, this.AccountInfo.UserId), false, "Not enough information is available to process the request. Please check the URL you typed or contact support for assistance.");
        }





        public UserViewData ViewerUserData
        {
            get
            {
                if (_ViewerUserData == null)
                    _ViewerUserData = UserBLL.GetUser(this.AccountInfo.UserId);

                return _ViewerUserData;
            }
        }

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

        private UserRegionalAccessProfile _UserAgencyData = null;
        private UserRegionalAccessProfile UserAgencyData
        {
            get
            {
                if (_UserAgencyData == null)
                    FetchData();
                return _UserAgencyData;
            }
            set
            {
                _UserAgencyData = value;
            }
        }

        protected bool IsEditAccessAllowed
        {
            get
            {
                if (!_IsEditAccessAllowed.HasValue)
                {
                    _IsEditAccessAllowed = AccessRulesBLL.CanEditAgencyUser(UserAgencyData.RegionId, UserAgencyData.IsAdmin, UserData.StateFIPS, UserAgencyData.IsApproverDesignate, ViewerUserData);
                    //_IsEditAccessAllowed = AccessRulesBLL.IsProfileEditable(this.UserData, this.AccountInfo);
                }

                return _IsEditAccessAllowed.Value;
            }
        }
        protected bool CanApproveUserRegistrations
        {
            get
            {
                //int ApproverDescriptorID = Descriptor.UserRegistrations_Approver.EnumValue<int>();
                //int? searchResult = (from DescriptorID in UserAgencyData.DescriptorIDList where DescriptorID == ApproverDescriptorID select DescriptorID).FirstOrDefault();
                //return (searchResult.HasValue && searchResult.Value > 0);
                return UserAgencyData.IsApproverDesignate;
            }
        }



        #region Page wired events
        protected void UserCommand(object sender, CommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case EDIT_CMD:
                    RouteController.RouteTo(RouteController.UserAgencyProfileEdit(ProfileUserId, ProfileAgencyId), true);
                    break;
                case DEL_CMD:
                    break;
                default:
                    throw new ShiptalkException(string.Format("Uknown UserCommand for Agency postback. Command: {0}, UserId: {1}", e.CommandName, ProfileUserId), false, "Sorry. We're unable to process your request. Please contact support for assistance.");
            }
        }
        protected void dataSourceAgencyUserView_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceAgencyUserView.DataSource = UserAgencyData;
        }
        #endregion




        #region "View state events"
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            ProfileUserId = (int)ViewState["UserProfileUserId"];
            ProfileAgencyId = (int)ViewState["UserAgencyId"];
        }

        protected override object SaveViewState()
        {
            ViewState["UserProfileUserId"] = ProfileUserId;
            ViewState["UserAgencyId"] = ProfileAgencyId;
            return base.SaveViewState();
        }
        #endregion


        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        #region "Parse route data"
        private int GetUserIdFromRouteRequest()
        {
            if (RouteData.Values[QueryStringKey] + string.Empty == string.Empty)
                throw new ShiptalkException("Agency profile requested without proper UserID parameter in the Route Data.", false, new ArgumentNullException("RouteData.params"));

            int ProfileUserId;
            if (int.TryParse(RouteData.Values[QueryStringKey].ToString().Split(UserID_AgencyID_Seperator, StringSplitOptions.None)[0], out ProfileUserId))
                return ProfileUserId;
            else
                throw new ShiptalkException("Agency profile requested without proper UserID parameter in the Route Data.", false, new ArgumentNullException("RouteData.params"));
        }

        private int GetAgencyIdFromRouteRequest()
        {
            if (RouteData.Values[QueryStringKey] + string.Empty == string.Empty)
                throw new ShiptalkException("Agency profile requested without proper AgencyID parameter in the Route Data.", false, new ArgumentNullException("RouteData.params"));

            int AgencyId;
            if (int.TryParse(RouteData.Values[QueryStringKey].ToString().Split(UserID_AgencyID_Seperator, StringSplitOptions.None)[1], out AgencyId))
                return AgencyId;
            else
                throw new ShiptalkException("Agency profile requested without proper AgencyID parameter in the Route Data.", false, new ArgumentNullException("RouteData.params"));

        }
        #endregion


     


        #region Authorization happens here
        private bool IsAuthorized()
        {
            bool AuthResult = AccessRulesBLL.CanViewAgencyUser(UserAgencyData.RegionId, UserAgencyData.IsAdmin, UserData.StateFIPS, ViewerUserData);
            //return AccessRulesBLL.CheckReadOnlyAccess(this.AccountInfo, UserData);

            //bool AuthResult = AccessRulesBLL.IsProfileEditable(UserData, this.AccountInfo);
            if (!AuthResult)
                ShiptalkException.ThrowSecurityException(string.Format("Access denied. User :{0} cannot view {1}.", this.AccountInfo.UserId, UserData.UserId), "You are not authorized to view the User information.");

            return AuthResult;
        }
        #endregion

    }
}
