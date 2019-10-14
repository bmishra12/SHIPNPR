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
    public partial class UserSubStateProfileEdit : System.Web.UI.Page, IRouteDataPage
    {

        #region Private Constants
        private const string UserSubStateProfileQueryStringKey = "params";
        private static readonly string[] UserID_SubStateRegionID_Seperator = { "-" };

        private const string EDIT_CMD = "edit";
        private const string DEL_CMD = "delete";
        private const string VIEWSTATE_KEY_UserIdOfProfileToEdit = "UserIdOfProfileToEdit";
        private const string VIEWSTATE_KEY_UserIdOfOldReviewer = "UserIdOfOldReviewer";
        private const string VIEWSTATE_KEY_UserSubStateRegionId = "UserSubStateRegionId";

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeData();

            if (IsAuthorized)
                InitializeView();
        }

        private void InitializeData()
        {
            PopulateParamsFromRouteData();
            FetchData();
        }
        private void InitializeView()
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            Page.DataBind();
            BindDescriptors();
            SetSelectedDescriptors();
            BindReviewers();
            SetSelectedReviewers();
        }




        #region Page wired events
        protected void dataSourceSubStateUserEdit_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceSubStateUserEdit.DataSource = UserSubStateRegionData;
        }

        protected void dataSourceSubStateUserEdit_Updated(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            UserRegionalAccessProfile ChangedUserSubStateRegionData = (UserRegionalAccessProfile)e.Instance;
            if (UserSubStateRegionBLL.UpdateUserSubState(ChangedUserSubStateRegionData, this.AccountInfo.UserId))
            {

                bool ReviewerUpdateFailed = false;

                //Save the new ReviewerID (Supervisor)
                if (NewSupervisorId != UserIdOfOldReviewer)
                {
                    ReviewerUpdateFailed = !UserBLL.SaveSupervisorForUser(UserSubStateRegionData.UserId, NewSupervisorId, UserSubStateRegionId, this.AccountInfo.UserId);
                }


                if (ReviewerUpdateFailed)
                    DisplayMessage("The new supervisor was not saved. The rest of the submitted information has been saved successfully.", false);
                else
                    DisplayMessage("The submitted information has been saved successfully.", false);
            }
            else
                DisplayMessage("Sorry. We were unable to save the information. Please contact support for assistance.", true);

            UserData = null;

        }
        protected void dataSourceSubStateUserEdit_Updating(object sender, ObjectContainerDataSourceUpdatingEventArgs e)
        {
            //Update UserID
            e.NewValues["UserId"] = UserProfileUserId;
            e.NewValues["DescriptorIDList"] = GetDescriptorsSelectedByAdmin();
            e.NewValues["RegionId"] = UserSubStateRegionId;

            //Get the new supervisor ID
            DropDownList ReviewersDDL = (DropDownList)formView.FindControl("ddlReviewers");
            int newReviewerId;
            if (int.TryParse(ReviewersDDL.SelectedValue, out newReviewerId))
                NewSupervisorId = newReviewerId;

        }

        protected void formView_PreRender(object sender, EventArgs e)
        {
            BindDescriptors();
            SetSelectedDescriptors();
            BindReviewers();
            SetSelectedReviewers();

            //Cannot update Approver status for 'Users' - need to be Admins.
            if (!UserSubStateRegionData.IsAdmin)
            {
                CheckBox cb = formView.FindControl("cbIsApprover") as CheckBox;
                if (cb != null)
                    cb.Enabled = false;
            }
        }

        protected void UserCommand(object sender, CommandEventArgs e)
        {
            //switch (e.CommandName.ToLower().Trim())
            //{
            //    case APPROVE_CMD:
            //        ApproveUser();
            //        break;
            //    case Deny_CMD:
            //        DenyUser();
            //        break;
            //    default:
            //        throw new ShiptalkException("Unknown command sent during postback in Approval page.", false);
            //}
        }
        #endregion



        #region UI Processing Logic
        private List<int> GetDescriptorsSelectedByAdmin()
        {
            List<int> DescriptorIds = new List<int>();
            CheckBoxList DescriptorsObj = (CheckBoxList)formView.FindControl("Descriptors");
            foreach (ListItem item in DescriptorsObj.Items)
            {
                if (item.Selected)
                    DescriptorIds.Add(int.Parse(item.Value));
            }

            //if (HaveApproveDelegationRights)
            //    DescriptorIds.Add(Descriptor.UserRegistrations_Approver.EnumValue<int>());

            return DescriptorIds;
        }
        private bool HaveApproveDelegationRights
        {
            get
            {
                bool HasRights = false;
                //If Checked, include 'Can Approve User' Descriptor.
                if ((CheckBox)formView.FindControl("cbIsApprover") != null)
                {
                    CheckBox IsApproverControl = (CheckBox)formView.FindControl("cbIsApprover");
                    HasRights = IsApproverControl.Checked;
                }
                return HasRights;
            }
        }
        private void SetSelectedDescriptors()
        {
            CheckBoxList DescriptorsObj = (CheckBoxList)formView.FindControl("Descriptors");
            var SelectedList = UserSubStateRegionData.DescriptorIDList;
            foreach (ListItem item in DescriptorsObj.Items)
            {
                foreach (var DescriptorID in SelectedList)
                {
                    //Set Selected here
                    if (DescriptorID == int.Parse(item.Value))
                        item.Selected = true;
                }
            }
        }
        private void BindDescriptors()
        {
            CheckBoxList DescriptorsObj = (CheckBoxList)formView.FindControl("Descriptors");
            DescriptorsObj.DataSource = GetDescriptors;

            DescriptorsObj.DataTextField = "Value";
            DescriptorsObj.DataValueField = "Key";
            DescriptorsObj.DataBind();
        }

        private void BindReviewers()
        {
            if (formView.FindControl("ddlReviewers") != null)
            {
                DropDownList ReviewersDDL = (DropDownList)formView.FindControl("ddlReviewers");
                var ReviewerItems = LookupBLL.GetReviewersByUserRegion(UserSubStateRegionId, Scope.SubStateRegion);
                if (ReviewerItems != null && ReviewerItems.Count() > 0)
                    ReviewerItems = ReviewerItems.Where(item => item.Key != UserData.UserId);

                ReviewersDDL.DataSource = ReviewerItems;

                ReviewersDDL.DataTextField = "Value";
                ReviewersDDL.DataValueField = "Key";
                ReviewersDDL.DataBind();


            }
        }
        private void SetSelectedReviewers()
        {
            if (formView.FindControl("ddlReviewers") != null)
            {
                DropDownList ReviewersDDL = (DropDownList)formView.FindControl("ddlReviewers");
                var SelectedReviewers = UserBLL.GetReviewersForUser(UserProfileUserId, UserSubStateRegionId);
                foreach (ListItem item in ReviewersDDL.Items)
                {
                    foreach (var pair in SelectedReviewers)
                    {
                        //Set Selected here
                        if (pair.Key == int.Parse(item.Value))
                        {
                            item.Selected = true;
                            UserIdOfOldReviewer = pair.Key;
                        }
                    }
                }

                if (ReviewersDDL.Items.Count == 0)
                    ReviewersDDL.Items.Add(new ListItem("No supervisors found for your sub state.", "-1"));
                else
                    ReviewersDDL.Items.Insert(0, new ListItem("--Supervisor not selected--", "-1"));

            }
        }
        #endregion


        #region "View state events"
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            UserProfileUserId = (int)ViewState[VIEWSTATE_KEY_UserIdOfProfileToEdit];
            UserIdOfOldReviewer = (int)ViewState[VIEWSTATE_KEY_UserIdOfOldReviewer];
            UserSubStateRegionId = (int)ViewState[VIEWSTATE_KEY_UserSubStateRegionId];
        }

        protected override object SaveViewState()
        {
            ViewState[VIEWSTATE_KEY_UserIdOfProfileToEdit] = UserProfileUserId;
            ViewState[VIEWSTATE_KEY_UserIdOfOldReviewer] = UserIdOfOldReviewer;
            ViewState[VIEWSTATE_KEY_UserSubStateRegionId] = UserSubStateRegionId;
            return base.SaveViewState();
        }
        #endregion





        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion



        #region Authorization

        public bool IsAuthorized
        {
            get
            {
                //To edit others
                bool AuthResult = AccessRulesBLL.CanEditSubStateUser(UserSubStateRegionData.RegionId, UserSubStateRegionData.IsAdmin, UserData.StateFIPS, UserSubStateRegionData.IsApproverDesignate, AdminUserData);
                //bool AuthResult = AccessRulesBLL.IsProfileEditable(UserData, this.AccountInfo);
                if (!AuthResult)
                    ShiptalkException.ThrowSecurityException(string.Format("Access denied. User :{0} cannot edit {1}.", this.AccountInfo.UserId, UserData.UserId), "You are not authorized to edit the User information.");

                return AuthResult;
            }

        }

        #endregion


        private UserViewData _AdminUserData = null;
        public UserViewData AdminUserData
        {
            get
            {
                if (_AdminUserData == null)
                    _AdminUserData = UserBLL.GetUser(this.AccountInfo.UserId);

                return _AdminUserData;
            }
        }


        private void FetchData()
        {
            FetchUserData();
            if (UserData == null)
                throw new ShiptalkException(string.Format("Profile of UserID {0} not found for User Sub State Regional Profile Requested by User ID {1}.", UserProfileUserId, this.AccountInfo.UserId), false, "Not enough information is available to process the request. Please check the URL you typed or contact support for assistance.");

            UserSubStateRegionData = UserData.RegionalProfiles.Where(prof => prof.RegionId == UserSubStateRegionId).FirstOrDefault();
            if (UserSubStateRegionData == null)
                throw new ShiptalkException(string.Format("User: {0} does not have a Sub State profile as requested by User: {1}.", UserProfileUserId, this.AccountInfo.UserId), false, "Not enough information is available to process the request. Please check the URL you typed or contact support for assistance.");


        }


        private void PopulateParamsFromRouteData()
        {
            if (RouteData.Values[UserSubStateProfileQueryStringKey] + string.Empty == string.Empty)
                throw new ShiptalkException("User Sub State profile requested without proper SubStateRegionID parameter in the Route Data.", false, new ArgumentNullException("RouteData.params"));

            //Get UserID of the User's Profile being currently edited.
            int ProfileUserId;
            if (!int.TryParse(RouteData.Values[UserSubStateProfileQueryStringKey].ToString().Split(UserID_SubStateRegionID_Seperator, StringSplitOptions.None)[0], out ProfileUserId))
                throw new ShiptalkException("User Sub State profile requested without proper UserID parameter in the Route Data.", false, new ArgumentNullException("RouteData.Id"));


            int UserSubStateRegionId;
            if (!int.TryParse(RouteData.Values[UserSubStateProfileQueryStringKey].ToString().Split(UserID_SubStateRegionID_Seperator, StringSplitOptions.None)[1], out UserSubStateRegionId))
                throw new ShiptalkException("User Sub State profile requested without proper SubStateRegionID parameter in the Route Data.", false, new ArgumentNullException("RouteData.Id"));

            this.UserProfileUserId = ProfileUserId;
            this.UserSubStateRegionId = UserSubStateRegionId;
        }


        private void FetchUserData()
        {
            UserData = UserBLL.GetUser(UserProfileUserId);
            UserData.FirstName = UserData.FirstName.ToCamelCasing();
            UserData.MiddleName = UserData.MiddleName.ToCamelCasing();
            UserData.LastName = UserData.LastName.ToCamelCasing();
            UserData.NickName = UserData.NickName.ToCamelCasing();
            UserData.Suffix = UserData.Suffix.ToCamelCasing();
            UserData.Honorifics = UserData.Honorifics.ToCamelCasing();

            UserData.PrimaryPhone = UserData.PrimaryPhone.FormatPhoneNumber();
            UserData.SecondaryPhone = UserData.SecondaryPhone.FormatPhoneNumber();
        }
        private void DisplayMessage(string message, bool IsError)
        {
            //Set up title area
            plhMessage.Visible = true;
            lblTitleMessage.Text = (IsError ? "Error" : "Success!");
            lblMessage.Text = message;
            lblMessage.CssClass = (IsError ? "required" : "info");
            hlBackToEdit.NavigateUrl = RouteController.UserEdit(UserProfileUserId);

            //*****************If we need to show the form that was submitted on error (or) upon success
            //*****************we need to keep main panel open. There is an issue with Descriptors being populated after update.
            //*****************I don't have the time window left to work on it. So for now, closing the MainPanel in case of success or error.
            //The following code is required to keep the MainPanel visible on error and closed on success. Can be tweaked.
            //////////Hide the form if error.
            ////////MainPanel.Visible = IsError;
            ////////MainPanel.EnableViewState = IsError;
            ////////formView.Visible = MainPanel.Visible;

            //////////Setup the back hyperlink if applicable.
            ////////hlBackToEdit.Visible = true;
            ////////hlBackToEdit.NavigateUrl = RouteController.UserEdit(UserProfileUserId);

            //Solution for now - Hide main panel
            MainPanel.Visible = false;
            formView.Visible = false;
        }
        protected string IsAdminCheckboxClientId
        {
            get
            {
                return "'" + ((CheckBox)formView.FindControl("cbIsAdmin")).ClientID + "'";
            }
        }
        protected string ApproverPanelClientId
        {
            get
            {
                return "'" + ((Control)formView.FindControl("pnlCanApproveUsers")).ClientID + "'";
            }
        }

        protected bool EnableApproverCheckbox
        {
            get
            {
                if (UserSubStateRegionData.IsAdmin)
                {
                    return ApproverRulesBLL.IsApproverForSubState(this.AccountInfo, UserSubStateRegionId);
                }

                return false;
            }
        }


        #region protected/private properties
        protected int UserProfileUserId { get; set; }
        protected int UserSubStateRegionId { get; set; }
        private int UserIdOfOldReviewer { get; set; }
        private int NewSupervisorId { get; set; }

        private IEnumerable<KeyValuePair<int, string>> _Reviewers = null;
        protected IEnumerable<KeyValuePair<int, string>> Reviewers
        {
            get
            {
                if (_Reviewers == null)
                    PopulateReviewersForUser();

                return _Reviewers;
            }
            set { _Reviewers = value; }
        }
        private UserRegionalAccessProfile _UserSubStateRegionData = null;
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
        private UserRegionalAccessProfile UserSubStateRegionData
        {
            get
            {
                if (_UserSubStateRegionData == null)
                {
                    _UserSubStateRegionData = UserData.RegionalProfiles.Where(prof => prof.RegionId == UserSubStateRegionId).FirstOrDefault();
                    if (_UserSubStateRegionData == null)
                        throw new ShiptalkException(string.Format("User: {0} does not have a Sub State profile as requested by User: {1}.", UserProfileUserId, this.AccountInfo.UserId), false, "Not enough information is available to process the request. Please check the URL you typed or contact support for assistance.");
                }
                return _UserSubStateRegionData;
            }
            set
            {
                _UserSubStateRegionData = value;
            }
        }
        protected string GetUserFullName
        {
            get
            {
                return UserData.FullName;
            }
        }
        //protected bool CanApproveUserRegistrations
        //{
        //    get
        //    {
        //        //int ApproverDescriptorID = Descriptor.UserRegistrations_Approver.EnumValue<int>();
        //        //int? searchResult = (from DescriptorID in UserSubStateRegionData.DescriptorIDList where DescriptorID == ApproverDescriptorID select DescriptorID).FirstOrDefault();
        //        //return (searchResult.HasValue && searchResult.Value > 0);
        //    }
        //}
        protected bool IsSingleProfileUser
        {
            get
            {
                return UserData.IsSingleProfileUser;
            }
        }
        protected string GetStateName
        {
            get
            {
                return UserData.StateName;
            }
        }
        protected string GetSubStateRegionName
        {
            get
            {
                return UserSubStateRegionData.RegionName;
            }
        }
        protected IEnumerable<KeyValuePair<int, string>> GetDescriptors
        {
            get
            {
                return LookupBLL.GetDescriptorsForScope(UserData.Scope);
            }
        }
        protected IEnumerable<KeyValuePair<int, string>> PopulateReviewersForUser()
        {
            return UserBLL.GetReviewersForUser(UserProfileUserId, UserSubStateRegionId);

        }
        private bool IsApproverChecked { get; set; }
        protected int? Reviewer { get; set; }
        //protected string EditUserProfileMainPageUrl
        //{
        //    get
        //    {
        //        return RouteController.UserEdit(UserProfileUserId);
        //    }
        //}
        #endregion

    }
}
