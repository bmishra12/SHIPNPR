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
    public partial class UserSubStateProfileAdd : System.Web.UI.Page, IRouteDataPage
    {

        #region Private Constants
        private const string QueryStringKey = "id";
        private const string VIEWSTATE_KEY_UserIdOfProfileToAdd = "UserIdOfProfileToAdd";
        #endregion

        public bool EndRequest { get; set; }

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

            //Set base for UserSubStateRegionData which will be manually populated by User
            UserSubStateRegionData = new UserRegionalAccessProfile();
            UserSubStateRegionData.UserId = UserData.UserId;
        }
        private void InitializeView()
        {
            if (!IsPostBack)
            {
                if (SubStateList != null && SubStateList.Count() > 0)
                {
                    BindData();
                }
                else
                {
                    DisplayMessage("Cannot add to another sub state.The User has been already assigned to all existing Sub States.", true);
                    EndRequest = true;
                    Page.DataBind();
                }

            }

            hlViewUserProfileMain.NavigateUrl = RouteController.UserEdit(UserProfileUserId);
            hlEditUserProfileMain.NavigateUrl = RouteController.UserEdit(UserProfileUserId);
        }

        private void BindData()
        {
            Page.DataBind();
            BindDescriptors();
            BindSubStatesForUser();
        }




        #region Page wired events
        protected void dataSourceSubStateUserAdd_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            //if(!EndRequest)
            dataSourceSubStateUserAdd.DataSource = UserSubStateRegionData;
        }
        protected void dataSourceSubStateUserAdd_Updated(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            UserRegionalAccessProfile ChangedUserSubStateRegionData = (UserRegionalAccessProfile)e.Instance;
            UpdateUserSubStateRegionData(ChangedUserSubStateRegionData);

            if (UserSubStateRegionData.RegionId != 0)
            {

                if (UserSubStateRegionBLL.AddUserSubStateRegionalProfile(UserSubStateRegionData, this.AccountInfo.UserId))
                {
                    //display success message
                    DisplayMessage("The submitted information has been saved successfully.", false);
                }
                else
                    DisplayMessage("Sorry. We were unable to save the information. Please contact support for assistance.", true);
            }
            else
            {
                plhMessage.Visible = true;
                lblTitleMessage.Text = "Error";
                lblMessage.Text = "A Sub state must be selected.";
                lblMessage.CssClass = "required";

                hlBackToEdit.EnableViewState = false;
                hlBackToEdit.Visible = false;
                hlBackToEdit.NavigateUrl = RouteController.UserEdit(UserProfileUserId);
            }

            Page.DataBind();

        }

        private void UpdateUserSubStateRegionData(UserRegionalAccessProfile ChangedProfile)
        {
            UserSubStateRegionData.RegionId = ChangedProfile.RegionId;
            UserSubStateRegionData.DescriptorIDList = ChangedProfile.DescriptorIDList;
            UserSubStateRegionData.IsAdmin = ChangedProfile.IsAdmin;
            UserSubStateRegionData.IsDefaultRegion = ChangedProfile.IsDefaultRegion;
            UserSubStateRegionData.IsApproverDesignate = ChangedProfile.IsApproverDesignate;
            UserSubStateRegionData.IsSuperDataEditor = ChangedProfile.IsSuperDataEditor;
        }


        protected void dataSourceSubStateUserAdd_Updating(object sender, ObjectContainerDataSourceUpdatingEventArgs e)
        {
            //Update UserID
            e.NewValues["UserId"] = UserProfileUserId;
            e.NewValues["DescriptorIDList"] = GetDescriptorsSelectedByAdmin();
            e.NewValues["RegionId"] = GetSelectedSubStateRegionId();

        }
        protected void UserCommand(object sender, CommandEventArgs e)
        {
            //Validate
            Page.Validate("UserSubStateProfile");

            bool DataIsValid = IsDataValid();
            if (!DataIsValid)
                DisplayMessage("Please fix the error(s) displayed in red and submit again", true);


            if (!Page.IsValid)
                return;
        }
        protected void formView_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var cbApproverObj = formView.FindControl("cbIsApprover") as CheckBox;
                if (cbApproverObj != null)
                    cbApproverObj.Enabled = ApproverRulesBLL.IsApprover(this.AccountInfo);
            }
            else
            {
                ReBindSubStateList();

                BindDescriptors();
                SetSelectedDescriptorsForUser();
            }
        }

        #endregion



        #region UI Processing Logic
        private void ReBindSubStateList()
        {
            string PreviousSelection = ((DropDownList)formView.FindControl("ddlSubStates")).SelectedValue;
            
            BindSubStatesForUser();
            DropDownList SubStatesDLLObj = (DropDownList)formView.FindControl("ddlSubStates");
            ListItem li = SubStatesDLLObj.Items.FindByValue(PreviousSelection);
            if (li != null)
                li.Selected = true;
        }

        private void SetSelectedDescriptorsForUser()
        {
            CheckBoxList DescriptorsCblObj = formView.FindControl("Descriptors") as CheckBoxList;
            if (DescriptorsCblObj != null)
            {
                var SelectedList = UserSubStateRegionData.DescriptorIDList;
                foreach (ListItem item in DescriptorsCblObj.Items)
                {
                    foreach (var val in SelectedList)
                    {
                        //Set Selected here
                        if (val == int.Parse(item.Value))
                            item.Selected = true;
                    }
                }
            }
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

            return DescriptorIds;
        }


        private void BindDescriptors()
        {
            CheckBoxList DescriptorsObj = (CheckBoxList)formView.FindControl("Descriptors");
            DescriptorsObj.DataSource = GetDescriptors;

            DescriptorsObj.DataTextField = "Value";
            DescriptorsObj.DataValueField = "Key";
            DescriptorsObj.DataBind();
        }
        private void BindSubStatesForUser()
        {
            DropDownList SubStatesDLLObj = (DropDownList)formView.FindControl("ddlSubStates");
            SubStatesDLLObj.DataSource = SubStateList;

            SubStatesDLLObj.DataTextField = "Value";
            SubStatesDLLObj.DataValueField = "Key";
            SubStatesDLLObj.DataBind();

            if (SubStateList == null || SubStateList.Count() == 0)
                SubStatesDLLObj.Items.Add(new ListItem("No sub states available", "0"));
            else
                SubStatesDLLObj.Items.Insert(0, new ListItem("-- Select a substate region --", "0"));
        }
        #endregion


        #region "View state events"
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            UserProfileUserId = (int)ViewState[VIEWSTATE_KEY_UserIdOfProfileToAdd];
        }

        protected override object SaveViewState()
        {
            ViewState[VIEWSTATE_KEY_UserIdOfProfileToAdd] = UserProfileUserId;
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
                bool AuthResult = AccessRulesBLL.CanAddUser(AdminUserData, Scope.SubStateRegion, UserData.StateFIPS);


                //AuthResult = AccessRulesBLL.CanEditUserProfile(UserData, AdminUserData);
                //bool AuthResult = AccessRulesBLL.IsProfileEditable(UserData, this.AccountInfo);


                if (!AuthResult)
                    ShiptalkException.ThrowSecurityException(string.Format("Access denied. User :{0} cannot Add {1}.", this.AccountInfo.UserId, UserData.UserId), "You are not authorized to Add the User information.");

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
                throw new ShiptalkException(string.Format("Profile of UserID {0} not found; Add Sub State Regional Profile Requested by User ID {1}.", UserProfileUserId, this.AccountInfo.UserId), false, "Not enough information is available to process the request. Please check the URL you typed or contact support for assistance.");

            PopulateSubStateList();
        }


        private void PopulateParamsFromRouteData()
        {
            //if (RouteData.Values[UserSubStateProfileQueryStringKey] + string.Empty == string.Empty)
            //    throw new ShiptalkException("User Sub State profile requested without proper UserID parameter in the Route Data.", false, new ArgumentNullException("RouteData.params"));

            //Get UserID of the User's Profile being currently Added.
            int ProfileUserId;
            if (!int.TryParse(RouteData.Values[QueryStringKey].ToString(), out ProfileUserId))
                throw new ShiptalkException("User Sub State profile requested without proper UserID parameter in the Route Data.", false, new ArgumentNullException("RouteData.Id"));

            this.UserProfileUserId = ProfileUserId;
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
            
            //TODO: remove this link in the aspx page.
            hlBackToEdit.NavigateUrl = RouteController.UserEdit(UserProfileUserId);
            hlBackToEdit.Visible = !IsError;

            if (!IsError || !IsPostBack)
            {
                MainPanel.Visible = false;
                formView.Visible = false;
            }

            //Solution for now - Hide main panel
            //MainPanel.Visible = false;
            //formView.Visible = false;
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
        protected int GetSelectedSubStateRegionId()
        {
            DropDownList SubStatesDDL = (DropDownList)formView.FindControl("ddlSubStates");
            return int.Parse(SubStatesDDL.SelectedValue);
        }

        #region protected/private properties
        protected int UserProfileUserId { get; set; }

        private UserRegionalAccessProfile _UserSubStateRegionData = null;
        private UserViewData _UserData = null;
        private IEnumerable<KeyValuePair<int, string>> _SubStateList = null;
        public IEnumerable<KeyValuePair<int, string>> SubStateList
        {
            get
            {
                if (_SubStateList == null)
                    PopulateSubStateList();

                return _SubStateList;
            }
            set
            {
                _SubStateList = value;
            }
        }

        protected void PopulateSubStateList()
        {
            List<KeyValuePair<int, string>> SubStatesListObj = new List<KeyValuePair<int, string>>();

            //Get all Sub States where User is Admin.
            IEnumerable<UserRegionalAccessProfile> SubStatesWhereUserIsAdmin = null;
            IDictionary<int, string> AllSubStatesInState = LookupBLL.GetSubStateRegionsForState(UserData.StateFIPS);

            //If the Admin is a User of Sub State Scope, we need only those sub states
            if (IsCreatorSubStateScope)
            {
                SubStatesWhereUserIsAdmin = UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(this.AccountInfo.UserId, true);

                KeyValuePair<int, string>? matchingSubState = null;
                foreach (UserRegionalAccessProfile SubStateAdminProfile in SubStatesWhereUserIsAdmin)
                {
                    matchingSubState = AllSubStatesInState.Where(p => p.Key == SubStateAdminProfile.RegionId).FirstOrDefault();
                    if (matchingSubState.HasValue)
                    {
                        SubStatesListObj.Add(matchingSubState.Value);
                        matchingSubState = null;
                    }
                }
            }
            //else, lets use all sub states in the User's state.
            //Since a sub state user can only be created by user of sub state or higher scope, current 
            //user can add this new user to any sub state in the state.
            else
            {
                SubStatesListObj = AllSubStatesInState.ToList<KeyValuePair<int, string>>();
            }

            //Before we return the list of sub states we need to remove the substates that the User is already assigned to,
            //so that the user is not added to the same sub state again.
            if (SubStatesListObj != null && SubStatesListObj.Count > 0)
            {
                IEnumerable<UserRegionalAccessProfile> ExistingSubStates = null;
                ExistingSubStates = UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(UserProfileUserId, false);

                if (ExistingSubStates != null && ExistingSubStates.Count() > 0)
                {
                    KeyValuePair<int, string>? existSubState = null;
                    foreach (UserRegionalAccessProfile ExistingSubState in ExistingSubStates)
                    {
                        existSubState = SubStatesListObj.Where(p => p.Key == ExistingSubState.RegionId).FirstOrDefault();
                        if (existSubState.HasValue)
                        {
                            SubStatesListObj.Remove(existSubState.Value);
                            existSubState = null;
                        }
                    }
                }
            }

            this.SubStateList = SubStatesListObj;
        }

        private bool IsCreatorSubStateScope
        {
            get
            {
                //The Sub State Users cannot Add Users.
                //The other possibilities are Sub State Admins or [State Level Admins or CMS Level Admins]
                //If not [State Level Admins or CMS Level Admins], then Creator is SubStateAdmin
                //return (this.AccountInfo.Scope.IsEqual(Scope.SubStateRegion));
                return AdminUserData.IsUserSubStateRegionalScope;
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
        private UserRegionalAccessProfile UserSubStateRegionData
        {
            get
            {
                if (_UserSubStateRegionData == null)
                {
                    _UserSubStateRegionData = UserData.RegionalProfiles[0];
                    if (_UserSubStateRegionData == null)
                        throw new ShiptalkException(string.Format("Error initializing data for new User Sub State functionality. User:{0}, Admin: {1}", UserProfileUserId, this.AccountInfo.UserId), false, "An error occured on server while initializing the add feature. Please contact support for assistance.");
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
        protected string GetStateName
        {
            get
            {
                return UserData.StateName;
            }
        }

        protected IEnumerable<KeyValuePair<int, string>> GetDescriptors
        {
            get
            {
                return LookupBLL.GetDescriptorsForScope(UserData.Scope);
            }
        }
        protected bool IsSingleProfileUser
        {
            get
            {
                return UserData.IsSingleProfileUser;
            }
        }

        private bool IsDataValid()
        {

            bool IsValid = false;

            bool IsApproverDesignateChecked = false;

            IValidator approverError = formView.FindControl("cvIsApproverError") as CustomValidator;
            IValidator adminError = formView.FindControl("cvIsAdminError") as CustomValidator;
            CheckBox approverCheckbox = formView.FindControl("cbIsApprover") as CheckBox;
            CheckBox IsAdminCheckbox = formView.FindControl("cbIsAdmin") as CheckBox;
            

            if (approverCheckbox != null)
                IsApproverDesignateChecked = approverCheckbox.Checked;

            if (IsApproverDesignateChecked && !IsAdminCheckbox.Checked)
            {
                IsValid = false;
                approverError.ErrorMessage = "Only Admins can be approvers. The role must be an administrator role";
                approverError.IsValid = IsValid;
            }
            else
            {
                var SubStatesDDL = formView.FindControl("ddlSubStates") as DropDownList;
                if (SubStatesDDL.SelectedValue != "0")
                {
                    if (!AccessRulesBLL.CanAddUserToSubState(AdminUserData, UserData.StateFIPS, GetSelectedSubStateRegionId(), IsAdminCheckbox.Checked, IsApproverDesignateChecked))
                    {
                        IsValid = false;
                        if (IsAdminCheckbox.Checked)
                        {
                            if (IsApproverDesignateChecked)
                            {
                                approverError.ErrorMessage = "You are not an approver in the chosen sub state. You cannot make another person an approver in the sub state.";
                                approverError.IsValid = IsValid;
                            }
                            else
                            {
                                adminError.ErrorMessage = "You are not authorized to create an admin user account in the chosen sub state.";
                                adminError.IsValid = IsValid;
                            }
                        }

                        
                    }
                    else
                        IsValid = true;
                }
            }

            return IsValid;

        }
        #endregion

    }
}
