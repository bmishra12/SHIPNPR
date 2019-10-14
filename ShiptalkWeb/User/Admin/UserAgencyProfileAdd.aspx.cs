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
    public partial class UserAgencyProfileAdd : System.Web.UI.Page, IRouteDataPage
    {

        #region Private Constants
        private const string UserAgencyProfileQueryStringKey = "id";
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

            //Set base for UserAgencyRegionData which will be manually populated by User
            UserAgencyRegionData = new UserRegionalAccessProfile();
            UserAgencyRegionData.UserId = UserData.UserId;
        }
        private void InitializeView()
        {
            if (!IsPostBack)
            {
                if (AgencyList != null && AgencyList.Count() > 0)
                {
                    BindData();
                }
                else
                {
                    DisplayMessage("Cannot add to another Agency. The User has been already assigned to all existing Agencies.", true);
                    EndRequest = true;
                    Page.DataBind();
                }
                
            }
        }

        private void BindData()
        {
            Page.DataBind();
            BindDescriptors();
            BindAgenciesForUser();
        }




        #region Page wired events
        protected void dataSourceAgencyUserAdd_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
            //if (!EndRequest)
            //{
                dataSourceAgencyUserAdd.DataSource = UserAgencyRegionData;
             //   EndRequest = true;
            //}
            
        }
        protected void dataSourceAgencyUserAdd_Updated(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            UserRegionalAccessProfile ChangedUserAgencyRegionData = (UserRegionalAccessProfile)e.Instance;
            UpdateUserAgencyData(ChangedUserAgencyRegionData);
            if (UserAgencyRegionData.RegionId != 0)
            {
                if (UserAgencyBLL.AddUserAgency(UserAgencyRegionData, this.AccountInfo.UserId))
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
                lblMessage.Text = "An agency must be selected.";
                lblMessage.CssClass = "required";
                
                hlBackToEdit.EnableViewState = false;
                hlBackToEdit.Visible = false;
                hlBackToEdit.NavigateUrl = RouteController.UserEdit(UserProfileUserId);
            }

            Page.DataBind();

        }

        private void UpdateUserAgencyData(UserRegionalAccessProfile ChangedProfile)
        {
            UserAgencyRegionData.RegionId = ChangedProfile.RegionId;
            UserAgencyRegionData.DescriptorIDList = ChangedProfile.DescriptorIDList;
            UserAgencyRegionData.IsAdmin = ChangedProfile.IsAdmin;
            UserAgencyRegionData.IsDefaultRegion = ChangedProfile.IsDefaultRegion;
            UserAgencyRegionData.IsApproverDesignate = ChangedProfile.IsApproverDesignate;
            UserAgencyRegionData.IsSuperDataEditor = ChangedProfile.IsSuperDataEditor;
        }

        protected void dataSourceAgencyUserAdd_Updating(object sender, ObjectContainerDataSourceUpdatingEventArgs e)
        {
            //Update UserID
            e.NewValues["UserId"] = UserProfileUserId;
            e.NewValues["DescriptorIDList"] = GetDescriptorsSelectedByAdmin();
            e.NewValues["RegionId"] = GetSelectedAgencyRegionId();
            

        }
        protected void UserCommand(object sender, CommandEventArgs e)
        {
            //Validate
            Page.Validate("UserAgencyProfile");

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
                ReBindAgencyList();

                BindDescriptors();
                SetSelectedDescriptorsForUser();
            }
        }
       
        
        #endregion



        #region UI Processing Logic
        private void ReBindAgencyList()
        {
            string PreviousSelection = ((DropDownList)formView.FindControl("ddlAgencies")).SelectedValue;

            BindAgenciesForUser();
            DropDownList AgenciesDLLObj = (DropDownList)formView.FindControl("ddlAgencies");
            ListItem li = AgenciesDLLObj.Items.FindByValue(PreviousSelection);
            if(li != null)
                li.Selected = true;
        }

        private void SetSelectedDescriptorsForUser()
        {
            CheckBoxList DescriptorsCblObj = formView.FindControl("Descriptors") as CheckBoxList;
            if (DescriptorsCblObj != null)
            {
                var SelectedList = UserAgencyRegionData.DescriptorIDList;
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
        private void BindAgenciesForUser()
        {
            DropDownList AgenciesDLLObj = (DropDownList)formView.FindControl("ddlAgencies");
            AgenciesDLLObj.DataSource = AgencyList;
            
            AgenciesDLLObj.DataTextField = "Value";
            AgenciesDLLObj.DataValueField = "Key";
            AgenciesDLLObj.DataBind();

            if(AgencyList == null || AgencyList.Count() == 0)
                AgenciesDLLObj.Items.Add(new ListItem("No agencies available","0"));
            else
                AgenciesDLLObj.Items.Insert(0, new ListItem("-- Select agency --", "0"));
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
                //The permission is same to add Add Users as well as Edit Users
                bool AuthResult = AccessRulesBLL.CanEditUserProfile(UserData, AdminUserData);
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
                throw new ShiptalkException(string.Format("Profile of User: {0} not found; Add User Agency Profile Requested by User ID {1}.", UserProfileUserId, this.AccountInfo.UserId), false, "Not enough information is available to process the request. Please check the URL you typed or contact support for assistance.");

            PopulateAgencyList();
        }


        private void PopulateParamsFromRouteData()
        {
            //Get UserID of the User's Profile being currently Added.
            int ProfileUserId;
            if (!int.TryParse(RouteData.Values[UserAgencyProfileQueryStringKey].ToString(), out ProfileUserId))
                throw new ShiptalkException("User Agency profile requested without proper UserID parameter in the Route Data.", false, new ArgumentNullException("RouteData.Id"));

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
            hlBackToEdit.NavigateUrl = RouteController.UserEdit(UserProfileUserId);

            hlBackToEdit.Visible = !IsError;

            if (!IsError || !IsPostBack)
            {
                //Solution for now - Hide main panel
                MainPanel.Visible = false;
                formView.Visible = false;
            }
            
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
        protected int GetSelectedAgencyId()
        {
            DropDownList AgenciesDDL = (DropDownList)formView.FindControl("ddlAgencies");
            return int.Parse(AgenciesDDL.SelectedValue);
        }
        protected int GetSelectedAgencyRegionId()
        {
            DropDownList AgenciesDDL = (DropDownList)formView.FindControl("ddlAgencies");
            return int.Parse(AgenciesDDL.SelectedValue);
        }

        #region protected/private properties
        protected int UserProfileUserId { get; set; }

        private UserRegionalAccessProfile _UserAgencyRegionData = null;
        private UserViewData _UserData = null;
        private IEnumerable<KeyValuePair<int, string>> _AgencyList = null;
        public IEnumerable<KeyValuePair<int, string>> AgencyList
        {
            get
            {
                if (_AgencyList == null)
                    PopulateAgencyList();

                return _AgencyList;
            }
            set
            {
                _AgencyList = value;
            }
        }

        protected void PopulateAgencyList()
        {
            List<KeyValuePair<int, string>> AgencyListObj = new List<KeyValuePair<int, string>>(); 

            //All Agencies where User is Admin.
            IEnumerable<UserRegionalAccessProfile> AgenciesWhereUserIsAdmin = null;

            IDictionary<int, string> AllAgenciesInState = LookupBLL.GetAgenciesForState(UserData.StateFIPS);


            if (IsCreatorAgencyScope)
            {
                AgenciesWhereUserIsAdmin = UserAgencyBLL.GetUserAgencyProfiles(this.AccountInfo.UserId, true);

                KeyValuePair<int, string>? matchingAgency = null;
                //get KeyValue Pair of all agencies in state where the Creator[the person who is adding the User] is Admin
                foreach (UserRegionalAccessProfile AgencyAdminProfile in AgenciesWhereUserIsAdmin)
                {
                    matchingAgency = AllAgenciesInState.Where(p => p.Key == AgencyAdminProfile.RegionId).FirstOrDefault();
                    if (matchingAgency.HasValue)
                    {
                        AgencyListObj.Add(matchingAgency.Value);
                        matchingAgency = null;
                    }
                }
            }
            else if (IsCreatorSubStateScope)
            {
                IEnumerable<UserRegionalAccessProfile> SubStatesWhereUserIsAdmin = 
                                    UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(this.AccountInfo.UserId, true);

                if (SubStatesWhereUserIsAdmin != null && SubStatesWhereUserIsAdmin.Count() > 0)
                {
                    //Collect Agencies that are part of Creator's Sub State regions.
                    foreach (UserRegionalAccessProfile subStateprofile in SubStatesWhereUserIsAdmin)
                    {
                        //Get Agencies for substate
                        IEnumerable<ShiptalkLogic.BusinessObjects.Agency> agencyProfiles = LookupBLL.GetAgenciesForSubStateRegion(subStateprofile.RegionId);
                        if(agencyProfiles != null && agencyProfiles.Count() > 0)
                            AgencyListObj.AddRange(agencyProfiles.Select(p => new KeyValuePair<int, string>(p.Id.Value, p.Name)));
                    }
                }
            }
            else
            {
                AgencyListObj = AllAgenciesInState.ToList<KeyValuePair<int, string>>();
            }

            //Before we return the list of Agencies we need to remove the Agencies that the User is already assigned to,
            //so that the user is not added to the same Agency again.
            if (AgencyListObj != null && AgencyListObj.Count > 0)
            {
                IEnumerable<UserRegionalAccessProfile> ExistingAgencies = null;
                ExistingAgencies = UserAgencyBLL.GetUserAgencyProfiles(UserProfileUserId, false);

                if (ExistingAgencies != null && ExistingAgencies.Count() > 0)
                {
                    KeyValuePair<int, string>? existAgency = null;
                    foreach (UserRegionalAccessProfile ExistingAgency in ExistingAgencies)
                    {
                        existAgency = AgencyListObj.Where(p => p.Key == ExistingAgency.RegionId).FirstOrDefault();
                        if (existAgency.HasValue)
                        {
                            AgencyListObj.Remove(existAgency.Value);
                            existAgency = null;
                        }
                    }
                }
            }

            this.AgencyList = AgencyListObj;
        }

       

        private bool IsCreatorAgencyScope
        {
            get{
                //The Agency Users cannot Add Users.
                //The other possibilities are Agency Admins or [State Level Admins or CMS Level Admins]
                //If not [State Level Admins or CMS Level Admins], then Creator is AgencyAdmin
                return (this.AccountInfo.Scope.IsEqual(Scope.Agency));
            }
        }
        private bool IsCreatorSubStateScope
        {
            get
            {
                return (this.AccountInfo.Scope.IsEqual(Scope.SubStateRegion));
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
        private UserRegionalAccessProfile UserAgencyRegionData
        {
            get
            {
                if (_UserAgencyRegionData == null)
                {
                    _UserAgencyRegionData = UserData.RegionalProfiles[0];
                    if (_UserAgencyRegionData == null)
                        throw new ShiptalkException(string.Format("Error initializing data for new User Agency functionality. User:{0}, Admin: {1}", UserProfileUserId, this.AccountInfo.UserId), false, "An error occured on server while initializing the add feature. Please contact support for assistance.");
                }
                return _UserAgencyRegionData;
            }
            set
            {
                _UserAgencyRegionData = value;
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
                var AgenciesDDL = formView.FindControl("ddlAgencies") as DropDownList;
                if (AgenciesDDL.SelectedValue != "0")
                {
                    if (!AccessRulesBLL.CanAddUserToAgency(AdminUserData, UserData.StateFIPS, GetSelectedAgencyId(), IsAdminCheckbox.Checked, IsApproverDesignateChecked))
                    {
                        IsValid = false;
                        if (IsAdminCheckbox.Checked)
                        {
                            if (IsApproverDesignateChecked)
                            {
                                approverError.ErrorMessage = "You are not an approver in the chosen agency. You cannot make another person an approver in the agency.";
                                approverError.IsValid = IsValid;
                            }
                            else
                            {
                                adminError.ErrorMessage = "You are not authorized to create an admin user account in the chosen agency.";
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
