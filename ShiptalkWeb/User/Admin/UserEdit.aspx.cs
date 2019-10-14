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
    public partial class UserEdit : System.Web.UI.Page, IRouteDataPage
    {

        protected UserViewData _UserData = null;

        private IEnumerable<KeyValuePair<int, string>> _Supervisors = null;
        private const string UserProfileUserIdKey = "Id";
        private const string VIEWSTATE_KEY_UserIdOfProfileToEdit = "UserIdOfProfileToEdit";
        private const string VIEWSTATE_KEY_UserIdOldReviewer = "UserIdOfOldReviewer";
        private const string VIEWSTATE_KEY_ScopeIdOfUser = "ScopeIdOfUser";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsAuthorized())
                ShiptalkException.ThrowSecurityException(string.Format("Access denied. User :{0} cannot edit {1}.", this.AccountInfo.UserId, UserData.UserId), "You are not authorized to edit the User information.");

            InitializeView();

            //Pbattineni - 10/08/12
            oldEmailId = (formView.FindControl("oldEmail") as TextBox).Text.ToString();
       
        }

        private void InitializeView()
        {
            if (!IsPostBack)
            {
                Page.DataBind();
            }
        }

        private void PopulateDescriptorsForStateUsers()
        {
            IEnumerable<KeyValuePair<int, string>> DescriptorValues = GetDescriptors();
            CheckBoxList DescriptorsCblObj = formView.FindControl("Descriptors") as CheckBoxList;
            if (DescriptorsCblObj != null)
            {
                DescriptorsCblObj.DataSource = DescriptorValues;
                DescriptorsCblObj.DataTextField = "Value";
                DescriptorsCblObj.DataValueField = "Key";
                DescriptorsCblObj.DataBind();
            }
        }

        private IEnumerable<KeyValuePair<int, string>> GetDescriptors()
        {
            if (this.AccountInfo.IsCMSLevel && this.AccountInfo.IsAdmin)
                return LookupBLL.GetDescriptorsForScope(Scope.State);
            else
            {
                var DescriptorsList = LookupBLL.GetDescriptorsForScope(Scope.State);
                return DescriptorsList.Where(p => p.Key != Descriptor.ShipDirector.EnumValue<int>());
            }
        }

        private void SetSelectedDescriptorsForUser()
        {
            CheckBoxList DescriptorsCblObj = formView.FindControl("Descriptors") as CheckBoxList;
            if (DescriptorsCblObj != null)
            {
                var SelectedList = UserData.GetAllDescriptorsForUser;
                foreach (ListItem item in DescriptorsCblObj.Items)
                {
                    foreach (var pair in SelectedList)
                    {
                        //Set Selected here
                        if (pair.Key == int.Parse(item.Value))
                            item.Selected = true;
                    }
                }
            }
        }


        #region Page wired events
        protected void dataSourceUserView_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceUserView.DataSource = UserData;
        }


        private void FormatData()
        {
            UserData.FirstName = UserData.FirstName.ToCamelCasing();
            UserData.MiddleName = UserData.MiddleName.ToCamelCasing();
            UserData.LastName = UserData.LastName.ToCamelCasing();
            UserData.NickName = UserData.NickName.ToCamelCasing();
            UserData.Suffix = UserData.Suffix.ToCamelCasing();
            UserData.Honorifics = UserData.Honorifics.ToCamelCasing();
            UserData.PrimaryEmail = UserData.PrimaryEmail.ToCamelCasing();

            UserData.PrimaryPhone = UserData.PrimaryPhone.FormatPhoneNumber();
            UserData.SecondaryPhone = UserData.SecondaryPhone.FormatPhoneNumber();
        }



        protected void dataSourceUserView_Updated(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            Page.Validate("UserProfile");
            if (!Page.IsValid)
                return;

            UserViewData ChangedUserData = (UserViewData)e.Instance;

            try
            {
                if (IsCMSOrStateScope)
                    UserBLL.UpdateIsAdminStatus(ChangedUserData.UserId, ChangedUserData.IsAdmin);
            }
            catch (Exception UpdateAccountEx)
            {
                DisplayMessage("Failed while saving administrator status. Please try later or contact support for assistance.", true);
                return;
            }

            //Update IsActive of UserData with what we got from Updating method.
            //ChangedUserData.IsActive = IsActiveUser;
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            //{
            if (UserBLL.UpdateUserProfile(CreateUserProfile(ChangedUserData), this.AccountInfo.UserId))
            {
                if (!UserData.IsShipDirector && UserData.IsUserStateScope)
                {
                    string ErrorMessage;
                    if (!UserBLL.SaveDescriptors(ChangedUserData.UserId, GetDescriptorsSelectedByAdmin(), ShiptalkLogic.Constants.Defaults.DefaultValues.AgencyIdForNonAgencyUsers, this.AccountInfo.UserId, out ErrorMessage))
                    {
                        DisplayMessage(ErrorMessage, true);
                        return;
                    }

                    if (!UserBLL.UpdateStateSuperDataEditor(ChangedUserData.UserId, ChangedUserData.IsStateSuperDataEditor, this.AccountInfo.UserId))
                    {
                        DisplayMessage("An error occured while updating the super data editor status", true);
                        return;
                    }
                }

                //Update the Is Approver Designate status for CMS/State scope Admins alone.
                //Ignore Ship directors and CMS Admins.
                if (UserData.IsAdmin)
                {
                    if ((!UserData.IsCMSAdmin && UserData.IsUserCMSScope) || (!UserData.IsShipDirector && UserData.IsUserStateScope))
                    {
                        if (!UserBLL.UpdateApproverDesignate(UserData.UserId, GetApproverDesignateSelection(), this.AccountInfo.UserId))
                        {
                            DisplayMessage("Failed while updating approver designate status. Please try later or contact support for assistance.", true);
                            return;
                        }
                    }
                }

                bool ReviewerUpdateFailed = false;
                if (ScopeIdOfUser == Scope.State.EnumValue<int>())
                {
                    if (NewSupervisorId == -1)
                        NewSupervisorId = 0;

                    if (NewSupervisorId != UserIdOfOldReviewer)
                    {
                        //Save the new ReviewerID as Supervisor
                        if (!UserBLL.SaveSupervisorForUser(ChangedUserData.UserId, NewSupervisorId, null, this.AccountInfo.UserId))
                        {
                            //If update failed, then 
                            ReviewerUpdateFailed = true;
                        }
                    }
                }
                //Lavnaya: Included Change Email process : 09/02/2012
                //If Primary Email is changed, then the email should be sent to user's old email id and as well as to new email id to notify them.
                // Email verification link will be sen to the user's new email id. Users can login using their old email id till they vefy the new email. this process is same as "Edit my Email" functionality.
                //If the Primary EMail id is not changed, then we dont change it in database.

                bool DoCommit = false;
                string ErrorMsg;
                string NewEmail = string.Empty;

                TextBox Email = formView.FindControl("Email") as TextBox;

                NewEmail = Email.Text;

                if (NewEmail != UserData.PrimaryEmail)
                {
                    if (NewEmail != "" && NewEmail != null)
                    {
                        //Check if the Username already exists
                        if (RegisterUserBLL.DoesUserNameExist(NewEmail))
                        {
                            DisplayMessage("The Primary Email address is already registered. Duplicates are not allowed.", true);
                            return;
                        }
                        else
                        {

                            if (UserBLL.ChangeEmail(ChangedUserData.UserId, UserData.PrimaryEmail, NewEmail, this.AccountInfo.UserId, out ErrorMsg))
                            {
                                if (VerifyEmail.SendEmailVerificationNotificationforEmailChange(false, ChangedUserData.UserId, ChangedUserData.UserId, NewEmail, out ErrorMsg))
                                    DoCommit = true;
                            }

                            //if (DoCommit)
                            //{
                            //    //scope.Complete();
                            //    DisplayMessage("Your request has been submitted. You will receive this Change Email request information email at your old Email address and  'Email verification' email at your new Email address shortly. Please follow the instruction to complete the Email verification process . If you do not receive an email after a while, please contact the help desk.", false);
                            //}

                            //}
                        }
                    }
                }
               

                //Lavnaya: Included Change Email process : 09/02/2012 -- End

                if (ReviewerUpdateFailed)
                    DisplayMessage("Unable to save new supervisor. Please try later or contact support for assistance.", true);


                    //else if (!DoCommit)Pbattineni: If (!DoCommit) & and New email is not equal to Primary email then Throw error Message- 10/08/12
                    else if (!DoCommit && NewEmail != UserData.PrimaryEmail)

                    DisplayMessage("Sorry. Unable to change your email. Please contact support for assistance.", true);
                else
                {
                    //scope.Complete();
                    DisplayMessage("The submitted information has been saved successfully.", false);
                }
                //formView.DataBind();
                //listViewUserRegionalProfiles.DataBind();
                //Page.DataBind();

            }
            else
                DisplayMessage("Sorry. We were unable to save the information. Please contact support for assistance.", true);
            //}

            //Get Fresh data for rebind.
            FetchUserData();

        }

        protected void dataSourceUserView_Updating(object sender, ObjectContainerDataSourceUpdatingEventArgs e)
        {
            RadioButton rb = ((RadioButton)formView.FindControl("rbActive"));
            IsActiveUser = rb.Checked;

            if (ScopeIdOfUser == Scope.State.EnumValue<int>())
            {
                if (formView.FindControl("ddlReviewers") != null)
                {
                    DropDownList ReviewersDDL = (DropDownList)formView.FindControl("ddlReviewers");
                    int _newReviewerId;
                    if (int.TryParse(ReviewersDDL.SelectedValue, out _newReviewerId))
                    {
                        //Need to save the new ReviewerID as Supervisor
                        NewSupervisorId = _newReviewerId;
                    }
                }
            }

        }
        protected void dataSourceRegionalProfiles_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            dataSourceRegionalProfiles.DataSource = GetEditableProfiles();
        }

        protected void formView_PreRender(object sender, EventArgs e)
        {
            if (UserData.IsUserStateScope && !UserData.IsShipDirector)
            {
                PopulateDescriptorsForStateUsers();
                SetSelectedDescriptorsForUser();
            }

            //Cannot update Approver status for 'Users' - need to be Admins.
            //Ship Directors and CMS Admins also do not need the approver bit set - they are approvers by default.
            if (UserData.IsShipDirector || UserData.IsCMSAdmin || !UserData.IsAdmin)
            {
                CheckBox cb = formView.FindControl("cbIsApprover") as CheckBox;
                if (cb != null)
                    cb.Enabled = false;
            }

        }


        #endregion


        private void DisplayMessage(string message, bool IsError)
        {
            plhMessage.Visible = true;
            lblMessage.Text = message;
            lblTitleMessage.Text = IsError ? "Error" : "Success!";
            lblMessage.CssClass = IsError ? "required" : "info";
        }




        private UserProfile CreateUserProfile(UserViewData ChangedData)
        {
            UserProfile profile = new UserProfile();
            profile.UserId = UserIdOfProfileToEdit;
            profile.FirstName = ChangedData.FirstName;
            profile.MiddleName = ChangedData.MiddleName;
            profile.LastName = ChangedData.LastName;
            profile.NickName = ChangedData.NickName;
            profile.Suffix = ChangedData.Suffix;
            profile.Honorifics = ChangedData.Honorifics;

            profile.SecondaryEmail = ChangedData.SecondaryEmail;
            profile.PrimaryPhone = ChangedData.PrimaryPhone;
            profile.SecondaryPhone = ChangedData.SecondaryPhone;
            profile.IsActive = IsActiveUser;

            return profile;
        }

        private IEnumerable<UserRegionalAccessProfile> GetEditableProfiles()
        {
            List<UserRegionalAccessProfile> editableProfiles = new List<UserRegionalAccessProfile>();
            foreach (UserRegionalAccessProfile profile in UserData.RegionalProfiles)
            {
                if (UserData.Scope.IsEqual(Scope.SubStateRegion))
                {
                    if (AccessRulesBLL.CanEditSubStateUser(profile.RegionId, profile.IsAdmin, UserData.StateFIPS, profile.IsApproverDesignate, AdminViewData))
                        editableProfiles.Add(profile);
                }
                else if (UserData.Scope.IsEqual(Scope.Agency))
                {
                    if (AccessRulesBLL.CanEditAgencyUser(profile.RegionId, profile.IsAdmin, UserData.StateFIPS, profile.IsApproverDesignate, AdminViewData))
                        editableProfiles.Add(profile);
                }
                else if (UserData.Scope.IsEqual(Scope.CMSRegional))
                {
                    if (AccessRulesBLL.CanEditUserProfile(UserData, AdminViewData))
                        editableProfiles.Add(profile);
                }
            }

            if (editableProfiles == null || editableProfiles.Count == 0)
                return null;
            else
                return editableProfiles;
        }

        private string GetFormViewTextBoxValue(string ControlName)
        {
            return ((TextBox)formView.FindControl(ControlName)).Text.Trim();
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
        private bool GetApproverDesignateSelection()
        {
            bool selectedValue = false;

            CheckBox cbl = formView.FindControl("cbIsApprover") as CheckBox;
            if (cbl != null)
                selectedValue = cbl.Checked;

            return selectedValue;
        }


        #region Call BLLs here
        //private void FetchData()
        //{
        //    //UserData = UserBLL.GetUser(UserIdOfProfileToEdit);
        //    Supervisors = LookupBLL.GetReviewersForStateScope(UserData.StateFIPS);
        //    ScopeIdOfUser = UserData.ScopeId;
        //}

        private void FetchUserData()
        {
            UserData = UserBLL.GetUser(UserIdOfProfileToEdit);
        }

        private void BindSupervisors()
        {
            if (UserData.IsUserStateScope)
            {
                if (formView.FindControl("ddlReviewers") != null)
                {
                    DropDownList ReviewerDDL = (DropDownList)formView.FindControl("ddlReviewers");
                    ReviewerDDL.DataSource = Supervisors;
                    ReviewerDDL.DataTextField = "Value";
                    ReviewerDDL.DataValueField = "Key";
                    ReviewerDDL.DataBind();

                    if (ReviewerDDL.Items.Count == 0)
                        ReviewerDDL.Items.Add(new ListItem("No supervisors found for your state.", "-1"));
                    else
                        ReviewerDDL.Items.Insert(0, new ListItem("--Supervisor not selected--", "-1"));
                }
            }
        }
        private void SetSelectedSupervisors()
        {
            if (UserData.IsUserStateScope)
            {
                if (formView.FindControl("ddlReviewers") != null)
                {
                    DropDownList ReviewerDDL = (DropDownList)formView.FindControl("ddlReviewers");

                    if (!IsPostBack)
                    {
                        //Get the current supervisor and set as selected from list of Reviewers.
                        //Per business rule, currently only one Supervisor is allowed. Thats why we use dropdownlist.
                        //Lets pick the first and assign to user.
                        var Reviewer = UserBLL.GetReviewersForUser(UserData.UserId, null);
                        if (Reviewer != null && Reviewer.Count() > 0)
                        {
                            ReviewerDDL.SelectedValue = Reviewer.ElementAt(0).Key.ToString();
                            UserIdOfOldReviewer = Reviewer.ElementAt(0).Key;
                        }
                        else
                            UserIdOfOldReviewer = 0;
                    }
                    else
                    {
                        ReviewerDDL.SelectedValue = NewSupervisorId.ToString();
                    }
                }
            }
        }

        #endregion


        #region protected/private properties
        private int _UserIdOfProfileToEdit;
        protected int UserIdOfProfileToEdit
        {
            get
            {
                if (RouteData.Values[UserProfileUserIdKey] == null) throw new ShiptalkException("User Edit requested without UserId in the Route Data.", false, new ArgumentNullException("RouteData.UserId"));
                return int.Parse(RouteData.Values[UserProfileUserIdKey].ToString());
            }
            set
            {
                _UserIdOfProfileToEdit = value;
            }
        }
        private int UserIdOfOldReviewer { get; set; }
        private Int16 ScopeIdOfUser { get; set; }
        private int NewSupervisorId { get; set; }
        protected UserViewData UserData
        {
            get
            {
                if (_UserData == null)
                {
                    FetchUserData();
                    ScopeIdOfUser = _UserData.ScopeId;
                }
                return _UserData;
            }
            set
            {
                _UserData = value;
            }
        }
        protected bool IsSingleProfileUser
        {
            get
            {
                if (UserData.IsCMSLevel || UserData.IsUserStateScope)
                    return true;
                else if (UserData.IsUserSubStateRegionalScope)
                {
                    return (!UserData.IsMultiSubStateUser);
                }
                else //Agency 
                {
                    return (!UserData.IsMultiAgencyUser);
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
                if (UserData.IsUserAgencyScope || UserData.IsUserSubStateRegionalScope)
                {
                    Scope scope = UserData.Scope;
                    if (UserData.RegionalProfiles != null && UserData.RegionalProfiles.Count > 0)
                        return LookupBLL.GetRoleNameUsingScope(scope, UserData.RegionalProfiles[0].IsAdmin, (Descriptor?)null);
                    else
                        return string.Empty;
                }
                else
                {
                    return UserData.RoleTitle;
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
                if (UserData.IsUserAgencyScope || UserData.IsUserSubStateRegionalScope)
                {
                    Scope scope = UserData.Scope;
                    if (UserData.RegionalProfiles != null && UserData.RegionalProfiles.Count > 0)
                        return LookupBLL.GetRoleDescriptionUsingScope(scope, UserData.IsAdmin);
                    else
                        return string.Empty;
                }
                else
                {
                    return UserData.RoleDescription;
                }
            }
        }
        protected string GetScopeDisplayText
        {
            get
            {
                if (UserData.IsUserAgencyScope)
                    return "Agency";
                else if (UserData.IsUserSubStateRegionalScope)
                    return "Sub State Region";
                else
                    return string.Empty;
            }

        }

        //IMPORTANT: Applies to only State Scope and CMS Users
        //For other scopes the regional profiles will hold values that will be shown in appropriate pages.
        protected bool CanapproveUsers
        {
            get
            {
                bool? CanApprove = null;

                //Only Admins can approve.
                if (!UserData.IsAdmin)
                    return false;

                if (UserData.IsShipDirector || UserData.IsCMSAdmin)
                    CanApprove = true;
                else if (UserData.IsUserStateScope)
                    CanApprove = UserData.IsStateApproverDesignate;
                else if (UserData.IsUserCMSScope)
                    CanApprove = UserData.IsCMSApproverDesignate;

                if (CanApprove.HasValue)
                    return CanApprove.Value;
                else
                    return false;
            }

        }


        protected bool EnableApproverCheckbox
        {
            get
            {
                if (UserData.IsStateAdmin)
                {
                    return ApproverRulesBLL.IsApproverForState(this.AccountInfo, UserData.StateFIPS);
                }
                else if (UserData.IsCMSAdmin)
                {
                    return ApproverRulesBLL.IsApproverAtCMS(this.AccountInfo);
                }

                return false;
            }
        }

        private bool? _IsActiveUser = null;
        public bool IsActiveUser
        {
            get
            {
                if (!_IsActiveUser.HasValue)
                    _IsActiveUser = UserData.IsActive;

                return _IsActiveUser.Value;
            }
            private set
            {
                _IsActiveUser = value;
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


        protected string GetUserScopeForRegionalUser(bool IsAdmin)
        {
            return LookupBLL.GetRoleNameUsingScope(UserData.Scope, IsAdmin, (Descriptor?)null);
        }
        protected string GetEditRoute(int RegionId)
        {
            if (UserData.IsUserSubStateRegionalScope)
            {
                return RouteController.UserSubStateProfileEdit(UserIdOfProfileToEdit, RegionId);
            }
            else if (UserData.IsUserAgencyScope)
            {
                return RouteController.UserAgencyProfileEdit(UserIdOfProfileToEdit, RegionId);
            }
            else
                return string.Empty;
        }
        protected string GetAddRoute()
        {
            if (UserData.IsUserSubStateRegionalScope)
            {
                return RouteController.UserSubStateProfileAdd(UserIdOfProfileToEdit);
            }
            else if (UserData.IsUserAgencyScope)
            {
                return RouteController.UserAgencyProfileAdd(UserIdOfProfileToEdit);
            }
            else
                return string.Empty;
        }
        protected string GetViewRoute(int RegionId)
        {
            if (UserData.IsUserSubStateRegionalScope)
            {
                return RouteController.UserSubStateProfileView(UserIdOfProfileToEdit, RegionId);
            }
            else if (UserData.IsUserAgencyScope)
            {
                return RouteController.UserAgencyProfileView(UserIdOfProfileToEdit, RegionId);
            }
            else
                return string.Empty;
        }
        protected IEnumerable<KeyValuePair<int, string>> Supervisors
        {
            get
            {
                if (_Supervisors == null)
                {
                    var val = new List<KeyValuePair<int, string>>(LookupBLL.GetReviewersForStateScope(UserData.StateFIPS));
                    if (val != null && val.Count() > 0)
                    {
                        val.Insert(0, new KeyValuePair<int, string>(0, "--Supervisor not selected--"));
                        _Supervisors = val.Where(item => item.Key != UserIdOfProfileToEdit);
                    }
                    else
                    {
                        if (val != null)
                            val.Insert(0, new KeyValuePair<int, string>(0, "No supervisors found for the state."));
                        else
                            val.Add(new KeyValuePair<int, string>(0, "No supervisors found for the state."));

                        _Supervisors = val;
                    }
                }
                return _Supervisors;
            }
            set
            {
                _Supervisors = value;
            }
        }
        protected string CurrentSupervisor
        {
            get
            {
                if (!IsPostBack)
                {
                    if (UserData.IsUserStateScope)
                    {
                        //Get the current supervisor and set as selected from list of Reviewers.
                        //Per business rule, currently only one Supervisor is allowed. Thats why we use dropdownlist.
                        //Lets pick the first and assign to user.
                        var Reviewer = new List<KeyValuePair<int, string>>(UserBLL.GetReviewersForUser(UserData.UserId, null));
                        if (Reviewer != null && Reviewer.Count() > 0)
                        {
                            UserIdOfOldReviewer = Reviewer.ElementAt(0).Key;
                            Reviewer.Insert(0, new KeyValuePair<int, string>(0, "--Supervisor not selected--"));
                            return UserIdOfOldReviewer.ToString();
                        }
                        else
                        {
                            Reviewer.Insert(0, new KeyValuePair<int, string>(0, "No supervisors assigned"));
                            UserIdOfOldReviewer = 0;
                            return "0";
                        }
                    }
                    else
                        return "0";
                }
                else
                {
                    return NewSupervisorId.ToString();
                }
            }
        }
        public bool IsCMSOrStateScope
        {
            get
            {
                return this.AccountInfo.Scope.IsEqual(Scope.CMS) || this.AccountInfo.Scope.IsEqual(Scope.State);
            }
        }
        #endregion


        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion



        #region "View state events"
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            UserIdOfProfileToEdit = (int)ViewState[VIEWSTATE_KEY_UserIdOfProfileToEdit];
            UserIdOfOldReviewer = (int)ViewState[VIEWSTATE_KEY_UserIdOldReviewer];
            ScopeIdOfUser = (Int16)ViewState[VIEWSTATE_KEY_ScopeIdOfUser];
        }

        protected override object SaveViewState()
        {
            ViewState[VIEWSTATE_KEY_UserIdOfProfileToEdit] = UserIdOfProfileToEdit;
            ViewState[VIEWSTATE_KEY_UserIdOldReviewer] = UserIdOfOldReviewer;
            ViewState[VIEWSTATE_KEY_ScopeIdOfUser] = ScopeIdOfUser;
            return base.SaveViewState();
        }
        #endregion



        #region Authorization

        public bool IsAuthorized()
        {
            //Get ViewData Of Logged In User if User is potential multi regional user[Agency/SubState].
            return AccessRulesBLL.CanEditUserProfile(UserData, AdminViewData);
        }

        #endregion




        /// <summary>
        /// If we turn on client side script, Display="Dynamic" and SetFocusOnError doesnt seem to go well.
        /// This is a work around I'm giving to identify the first error control and set its focus.
        /// If no errors, still the first valid control will get its focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_PreRender(object sender, System.EventArgs e)
        {
            try
            {
                if (Page.Validators != null && Page.Validators.Count > 0)
                {
                    string firstNonErrorControlName = string.Empty;
                    bool bErrorValidatorFound = false;
                    foreach (IValidator validator in Page.Validators)
                    {
                        if (!validator.IsValid)
                        {
                            BaseValidator field = validator as BaseValidator;
                            Control c = formView.FindControl(field.ControlToValidate);
                            c.Focus();
                            bErrorValidatorFound = true;
                            break;
                        }
                        else if (firstNonErrorControlName == string.Empty)
                        {
                            BaseValidator field = validator as BaseValidator;
                            Control c = formView.FindControl(field.ControlToValidate);

                            if (c.Visible == true)
                                firstNonErrorControlName = c.ID;
                        }
                    }
                    if (!bErrorValidatorFound && (firstNonErrorControlName != string.Empty))
                    {
                        Control c = formView.FindControl(firstNonErrorControlName);
                        c.Focus();
                    }
                }
            }
            catch
            {
                //Do nothing; least important.
            }

        }

        //pbattineni
        // If Primary Email is changed,we Need to throw a Error Message. - 10/08/12

        private string oldEmailId;
        private string newEmailId;
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            newEmailId = (formView.FindControl("Email") as TextBox).Text.ToString();

            if (newEmailId != oldEmailId)
            {
                string strErrorDesc = "You are about to change the Primary Email Address of this user. An email will be sent to this user and this change will not be effective until the user accepts this change by clicking the accept link. If you are sure about this change, Click OK to proceed.";
                Response.Write(@"<script language='javascript'>alert('" + strErrorDesc + " ');</script>");
            }
        }

    }
}
