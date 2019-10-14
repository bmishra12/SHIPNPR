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
    public partial class UserAgencyProfileEdit : System.Web.UI.Page, IRouteDataPage
    {

        #region Private Constants
        private const string UserAgencyProfileQueryStringKey = "params";
        private static readonly string[] UserID_AgencyID_Seperator = { "-" };
        private const string EDIT_CMD = "edit";
        private const string DEL_CMD = "delete";
        private const string VIEWSTATE_KEY_UserIdOfProfileToEdit = "UserIdOfProfileToEdit";
        private const string VIEWSTATE_KEY_UserIdOfOldReviewer = "UserIdOfOldReviewer";
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
                Page.DataBind();
                //BindDescriptors();
                //SetSelectedDescriptors();
                //BindReviewers();
                //SetSelectedReviewers();
            }
        }





        #region Page wired events
        protected void dataSourceAgencyUserEdit_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            //if (!IsPostBack)
            //{
            dataSourceAgencyUserEdit.DataSource = UserAgencyData;
            //}
        }
        protected void dataSourceAgencyUserEdit_Updated(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            //UserAgencyData = (UserRegionalAccessProfile)e.Instance;
            UserAgencyData = null;
            SynchronizeChangesToUserAgencyData((UserRegionalAccessProfile)e.Instance);

            //Readonly descriptors verification: 
            //  New logic to ensure that certain readonly descriptors are not saved along with non-readonly ones.
            bool ShowError = true;
            string Message = "Sorry. We were unable to save the information. Please contact support for assistance.";
            if (ValidateDescriptorsForUser(ref Message))
            {
                if (UserAgencyBLL.UpdateUserAgency(UserAgencyData, this.AccountInfo.UserId))
                {
                    //display success message
                    bool ReviewerUpdateFailed = false;

                    //Save the new ReviewerID (Supervisor)
                    if (NewSupervisorId != UserIdOfOldReviewer)
                        ReviewerUpdateFailed = !UserBLL.SaveSupervisorForUser(UserAgencyData.UserId, NewSupervisorId, AgencyId, this.AccountInfo.UserId);

                    if (ReviewerUpdateFailed)
                    {
                        DisplayMessage("The new supervisor was not saved. The rest of the submitted information has been saved successfully.", false);
                        ShowError = false;
                    }
                    else
                    {
                        DisplayMessage("The submitted information has been saved successfully.", false);
                        ShowError = false;
                    }
                }
            }

            //Added error for the readonly descriptors verification
            if (ShowError) DisplayMessage(Message, true);
        }
        protected void dataSourceAgencyUserEdit_Updating(object sender, ObjectContainerDataSourceUpdatingEventArgs e)
        {
            //Update UserID
            e.NewValues["UserId"] = UserProfileUserId;
            e.NewValues["DescriptorIDList"] = GetDescriptorsSelectedByAdmin();
            e.NewValues["RegionId"] = AgencyId;

            //Get the new supervisor ID
            DropDownList ReviewersDDL = (DropDownList)formView.FindControl("ddlReviewers");
            int newReviewerId;
            if (int.TryParse(ReviewersDDL.SelectedValue, out newReviewerId))
                NewSupervisorId = newReviewerId;
        }
        protected void UserCommand(object sender, CommandEventArgs e)
        {
        }
        protected void formView_PreRender(object sender, EventArgs e)
        {
            //Cannot update Approver status for 'Users' - need to be Admins.
            if (!UserAgencyData.IsAdmin)
            {
                CheckBox cb = formView.FindControl("cbIsApprover") as CheckBox;
                if (cb != null)
                    cb.Enabled = false;
            }

            BindDescriptors();
            BindReviewers();
            SetSelectedDescriptors();
            SetSelectedReviewers();
        }
        private void SynchronizeChangesToUserAgencyData(UserRegionalAccessProfile ChangedUserAgencyData)
        {
            UserAgencyData.DescriptorIDList = ChangedUserAgencyData.DescriptorIDList;
            UserAgencyData.IsActive = ChangedUserAgencyData.IsActive;
            UserAgencyData.IsAdmin = ChangedUserAgencyData.IsAdmin;
            UserAgencyData.IsApproverDesignate = ChangedUserAgencyData.IsApproverDesignate;
            UserAgencyData.IsDefaultRegion = ChangedUserAgencyData.IsDefaultRegion;
            UserAgencyData.IsSuperDataEditor = ChangedUserAgencyData.IsSuperDataEditor;
        }
        #endregion



        #region UI Processing Logic
        private bool ValidateDescriptorsForUser(ref string Message)
        {
            bool IsSuccess = true;
            int OtherStaffNPRReadonly_Descriptor = Descriptor.OtherStaff_NPR.EnumValue<int>();
            int OtherStaffSHIPReadOnly_Descriptor = Descriptor.OtherStaff_SHIP.EnumValue<int>();

            var Descriptors = UserAgencyData.DescriptorIDList;
            if (Descriptors.Contains(OtherStaffNPRReadonly_Descriptor) || Descriptors.Contains(OtherStaffSHIPReadOnly_Descriptor))
            {
                if (UserAgencyData.IsSuperDataEditor)
                {
                    Message = "The User cannot be SHIP ReadOnly/NPR Readonly staff and also be Super Editor. Please fix the issue";
                    IsSuccess = false;
                }
                else
                {
                    IList<int> ReadOnlyDescriptors = new List<int> { OtherStaffNPRReadonly_Descriptor, OtherStaffSHIPReadOnly_Descriptor };
                    IEnumerable<int> OtherDescriptors = Descriptors.Except(ReadOnlyDescriptors);
                    if (OtherDescriptors != null && OtherDescriptors.Count() > 0)
                    {
                        //At this point, Descritors selected contain both Readonly and Other descriptors; which is not allowed
                        Message = "SHIP ReadOnly and NPR Readonly staff task functions cannot be combined with other task/functions. Please fix the issue";
                        IsSuccess = false;
                    }
                }
            }

            return IsSuccess;
        }

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
            var SelectedList = UserAgencyData.DescriptorIDList;
            foreach (ListItem item in DescriptorsObj.Items)
            {
                foreach (var descriptorID in SelectedList)
                {
                    //Set Selected here
                    if (descriptorID == int.Parse(item.Value))
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

                var ReviewerItems = LookupBLL.GetReviewersByUserRegion(AgencyId, Scope.Agency);
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
                //At present, only one supervisor is assigned to a User.
                //Is possible to have multiple in future - the original idea. Code is good for all scenarios.
                DropDownList ReviewersDDL = (DropDownList)formView.FindControl("ddlReviewers");
                var SelectedReviewers = UserBLL.GetReviewersForUser(UserProfileUserId, AgencyId);

                if (ReviewersDDL.Items.Count == 0)
                    ReviewersDDL.Items.Add(new ListItem("No supervisors found for your Agency.", "0"));
                else
                    ReviewersDDL.Items.Insert(0, new ListItem("--Supervisor not selected--", "0"));

                //When post back occurs, we need to set the Reviewer that was chosen, but not saved as the selected Reviewer in DDL
                //This is important when the updated data is not saved in database due to error.
                if (IsPostBack)
                {
                    ListItem newSupervisorItem = ReviewersDDL.Items.FindByValue(NewSupervisorId.ToString());
                    if (newSupervisorItem != null)
                    {
                        newSupervisorItem.Selected = true;
                        UserIdOfOldReviewer = NewSupervisorId;
                    }
                }
                else
                {
                    bool ReviewerSelected = false;
                    foreach (ListItem item in ReviewersDDL.Items)
                    {
                        foreach (var pair in SelectedReviewers)
                        {
                            //Set Selected here
                            if (pair.Key == int.Parse(item.Value))
                            {
                                item.Selected = true;
                                UserIdOfOldReviewer = pair.Key;
                                ReviewerSelected = true;
                                break;
                            }
                        }

                        //Need to ensure that the above foreach break doesn't get into another loop.
                        if (ReviewerSelected) break;
                    }
                }
            }
        }
        #endregion


        #region "View state events"
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            UserProfileUserId = (int)ViewState[VIEWSTATE_KEY_UserIdOfProfileToEdit];
            UserIdOfOldReviewer = (int)ViewState[VIEWSTATE_KEY_UserIdOfOldReviewer];
        }

        protected override object SaveViewState()
        {
            ViewState[VIEWSTATE_KEY_UserIdOfProfileToEdit] = UserProfileUserId;
            ViewState[VIEWSTATE_KEY_UserIdOfOldReviewer] = UserIdOfOldReviewer;
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
                bool AuthResult = AccessRulesBLL.CanEditAgencyUser(UserAgencyData.RegionId, UserAgencyData.IsAdmin, UserData.StateFIPS, UserAgencyData.IsApproverDesignate, AdminUserData);
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
                throw new ShiptalkException(string.Format("Profile of UserID {0} not found for User Agency Profile Requested by User ID {1}.", UserProfileUserId, this.AccountInfo.UserId), false, "Not enough information is available to process the request. Please check the URL you typed or contact support for assistance.");

            UserAgencyData = UserData.RegionalProfiles.Where(prof => prof.RegionId == AgencyId).FirstOrDefault();
            if (UserAgencyData == null)
                throw new ShiptalkException(string.Format("User: {0} does not have a agency profile as requested by User: {1}.", UserProfileUserId, this.AccountInfo.UserId), false, "Not enough information is available to process the request. Please check the URL you typed or contact support for assistance.");

        }



        private void PopulateParamsFromRouteData()
        {
            if (RouteData.Values[UserAgencyProfileQueryStringKey] + string.Empty == string.Empty)
                throw new ShiptalkException("User Agency profile requested without proper AgencyID parameter in the Route Data.", false, new ArgumentNullException("RouteData.params"));

            //Get UserID of the User's Profile being currently edited.
            int ProfileUserId;
            if (!int.TryParse(RouteData.Values[UserAgencyProfileQueryStringKey].ToString().Split(UserID_AgencyID_Seperator, StringSplitOptions.None)[0], out ProfileUserId))
                throw new ShiptalkException("User Agency profile requested without proper UserID parameter in the Route Data.", false, new ArgumentNullException("RouteData.Id"));


            int AgencyId;
            if (!int.TryParse(RouteData.Values[UserAgencyProfileQueryStringKey].ToString().Split(UserID_AgencyID_Seperator, StringSplitOptions.None)[1], out AgencyId))
                throw new ShiptalkException("User Agency profile requested without proper AgencyID parameter in the Route Data.", false, new ArgumentNullException("RouteData.Id"));

            this.UserProfileUserId = ProfileUserId;
            this.AgencyId = AgencyId;
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

            //Hide the form if error.
            //MainPanel.Visible = IsError;
            //MainPanel.EnableViewState = IsError;
            //formView.Visible = MainPanel.Visible;
            //MainPanel.Visible = false;
            //MainPanel.EnableViewState = false;
            //formView.Visible = MainPanel.Visible;

            //Setup the back hyperlink if applicable.
            //if (!IsError)
            //{
                //hlBackToEdit.Visible = true;
                //hlBackToEdit.NavigateUrl = RouteController.UserEdit(UserProfileUserId);
                //hlBackToEdit.DataBind();
            //}
            EditPageMainLink.DataBind();
        }


        #region protected/private properties
        protected int UserProfileUserId { get; set; }
        protected int AgencyId { get; set; }
        private int UserIdOfOldReviewer { get; set; }
        private int NewSupervisorId { get; set; }
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
        //        int ApproverDescriptorID = Descriptor.UserRegistrations_Approver.EnumValue<int>();
        //        int? searchResult = (from DescriptorID in UserAgencyData.DescriptorIDList where DescriptorID == ApproverDescriptorID select DescriptorID).FirstOrDefault();
        //        return (searchResult.HasValue && searchResult.Value > 0);
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
        protected string GetAgencyName
        {
            get
            {
                return UserAgencyData.RegionName;
            }
        }
        protected IEnumerable<KeyValuePair<int, string>> GetDescriptors
        {
            get
            {
                return LookupBLL.GetDescriptorsForScope(UserData.Scope);
            }
        }
        protected bool EnableApproverCheckbox
        {
            get
            {
                if (UserAgencyData.IsAdmin)
                {
                    return ApproverRulesBLL.IsApproverForAgency(this.AccountInfo, AgencyId);
                }

                return false;
            }
        }


        protected IEnumerable<KeyValuePair<int, string>> PopulateReviewersForUser()
        {
            return UserBLL.GetReviewersForUser(UserProfileUserId, AgencyId);

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
