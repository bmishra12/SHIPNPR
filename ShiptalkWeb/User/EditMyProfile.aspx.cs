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
    public partial class EditMyProfile : System.Web.UI.Page, IRouteDataPage
    {

        private const string VIEWSTATE_KEY_UserIdOfProfileToEdit = "UserIdOfProfileToEdit";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsAuthorized())
                ShiptalkException.ThrowSecurityException(string.Format("Access denied. Profile of UserId:{0} must be edited only by the same user.", UserIdOfProfileToEdit), "The profile of the person editing must match the profile of the person requesting 'Edit my profile' access. Admins must use Edit User functionality.");

            InitializeView();
        }

        private void InitializeView()
        {
            if (!IsPostBack)
            {
                UserIdOfProfileToEdit = this.AccountInfo.UserId;

                SetupAdminLinksPanel();

                Page.DataBind();
            }
        }

        private void SetupAdminLinksPanel()
        {
           pendingEmailChangeVerifications.Visible = IsAuthorizedtoViewPendingEmailChangeVerifications;

            AdminLinksPanel.Visible = (pendingEmailChangeVerifications.Visible);
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

        #region Page wired events
        protected void dataSourceUserView_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {

            UserData.FirstName = UserData.FirstName.ToCamelCasing();
            UserData.MiddleName = UserData.MiddleName.ToCamelCasing();
            UserData.LastName = UserData.LastName.ToCamelCasing();
            UserData.NickName = UserData.NickName.ToCamelCasing();
            UserData.Suffix = UserData.Suffix.ToCamelCasing();
            UserData.Honorifics = UserData.Honorifics.ToCamelCasing();

            UserData.PrimaryPhone = UserData.PrimaryPhone.FormatPhoneNumber();
            UserData.SecondaryPhone = UserData.SecondaryPhone.FormatPhoneNumber();

            dataSourceUserView.DataSource = UserData;

        }

        protected void dataSourceUserView_Updated(object sender, ObjectContainerDataSourceStatusEventArgs e)
        {
            if (!IsValid())
                return;

            UserViewData ChangedUserData = (UserViewData)e.Instance;

            //Update IsActive of UserData with what we got from Updating method.
            //ChangedUserData.IsActive = IsActiveUser;

            if (UserBLL.UpdateUserProfile(GetChangedUserProfile(ChangedUserData), this.AccountInfo.UserId))
            {
                if (UserBLL.UpdateUserAccount(this.AccountInfo, this.AccountInfo.UserId))
                    DisplayMessage("The submitted information has been saved successfully.", false);
                else
                    DisplayMessage("Unable to save Zip and County of Counseling location. However, the rest of the profile information has been saved successfully.", true);
            }
            else
                DisplayMessage("Sorry. Unable to save the information. Please contact support for assistance.", true);

            FetchUserData();
        }

        protected void dataSourceUserView_Updating(object sender, ObjectContainerDataSourceUpdatingEventArgs e)
        {
            if (formView.FindControl("ddlZipcodes") != null)
            {
                DropDownList ZipcodesDDL = (DropDownList)formView.FindControl("ddlZipcodes");
                if ((!string.IsNullOrEmpty(ZipcodesDDL.SelectedValue)) && (ZipcodesDDL.SelectedValue != "0"))
                    this.AccountInfo.CounselingLocation = ZipcodesDDL.SelectedValue;

                DropDownList CountiesDDL = (DropDownList)formView.FindControl("ddlCounties");
                if ((!string.IsNullOrEmpty(CountiesDDL.SelectedValue)) && (CountiesDDL.SelectedValue != "0"))
                    this.AccountInfo.CounselingCounty = CountiesDDL.SelectedValue;
            }

        }


        protected void RequestUniqueId_Click(Object sender, CommandEventArgs e)
        {
            //Insert the request here.
            //Should save, email the designated person in one transaction and return success or failure
            string ErrorMessage;
            if (UserBLL.HandleUniqueIDRequest(this.AccountInfo.UserId, out ErrorMessage))
            {
                //For reloading new value, set UniqueIdData to null.
                UniqueIDData = null;
                DisplayMessage("The Unique ID request has been submitted successfully.", false);
                Page.DataBind();
            }
            else
                DisplayMessage(ErrorMessage + " Please contact support if this issue persists.", true);
        }





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

        #endregion


        private void DisplayMessage(string message, bool IsError)
        {
            plhMessage.Visible = true;
            lblMessage.Text = message;
            lblTitleMessage.Text = IsError ? "Error" : "Success!";
            lblMessage.CssClass = IsError ? "required" : "info";
        }




        private UserProfile GetChangedUserProfile(UserViewData ChangedData)
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

        private string GetFormViewTextBoxValue(string ControlName)
        {
            return ((TextBox)formView.FindControl(ControlName)).Text.Trim();
        }

        private bool IsValid()
        {
            Page.Validate("UserProfile");
            if (!Page.IsValid)
                return false;
            else
            {
                bool IsBadCountyZipMatch = false;
                //Ensure the selected Zip is part of selected county
                if (formView.FindControl("ddlZipcodes") != null)
                {
                    DropDownList zipDDL = (DropDownList)formView.FindControl("ddlZipcodes");
                    string selectedZip = zipDDL.SelectedValue;
                    if (selectedZip != "0")
                    {
                        IEnumerable<string> matchingCounties = (from record in ZipCountyForState
                                                                where record.Zipcode == selectedZip
                                                                select record.CountyFIPS);
                        DropDownList countyDDL = (DropDownList)formView.FindControl("ddlCounties");
                        string selectedCounty = countyDDL.SelectedValue;
                        bool IsMatchFound = false;
                        foreach (string county in matchingCounties)
                        {
                            if (county == selectedCounty)
                            {
                                IsMatchFound = true;
                                break;
                            }
                        }
                        if (!IsMatchFound)
                        {
                            IsBadCountyZipMatch = true;
                            DisplayMessage("The counseling location zip code does not belong to the selected County. Please try again.", true);
                        }
                    }

                }
                return !IsBadCountyZipMatch;
            }
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
            UserData = UserBLL.GetUser(this.AccountInfo.UserId);
        }



        #endregion


        #region protected/private properties
        private int _UserIdOfProfileToEdit;
        protected int UserIdOfProfileToEdit
        {
            get
            {
                return _UserIdOfProfileToEdit;
            }
            set
            {
                _UserIdOfProfileToEdit = value;
            }
        }

        private Int16 ScopeIdOfUser { get; set; }


        protected UserViewData _UserData = null;
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
        protected bool IsDataSubmitter
        {
            get
            {
                if (UserData.IsStateLevel)
                {
                    if (UserData.IsShipDirector) return true;
                    else
                        return UserData.GetAllDescriptorNamesForUser.Contains(Descriptor.DataSubmitter.Description());
                }
                else
                    return false;
            }
        }

        protected string GetUserScopeForRegionalUser(bool IsAdmin)
        {
            return LookupBLL.GetRoleNameUsingScope(UserData.Scope, IsAdmin, (Descriptor?)null);
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
        protected IEnumerable<string> Zipcodes
        {
            get
            {
                return (from zip in ZipCountyForState
                        orderby zip.Zipcode
                        select zip.Zipcode).Distinct();
            }
        }
        protected string CurrentZipcode
        {
            get
            {
                if (string.IsNullOrEmpty(UserData.CounselingLocation))
                    return "0";
                else
                {
                    //If Zipcode selected as default location is outside state
                    //then it will not be presented in Zipcodes. So set to blank.
                    if (Zipcodes.Contains(UserData.CounselingLocation))
                        return UserData.CounselingLocation;
                    else
                        return "0";
                }
            }
        }
        protected IEnumerable<KeyValuePair<string, string>> Counties
        {
            get
            {
                return (from county in ZipCountyForState
                        orderby county.CountyKVPair.Value
                        select county.CountyKVPair).Distinct();
            }
        }
        protected string CurrentCounty
        {
            get
            {
                if (string.IsNullOrEmpty(UserData.CounselingCounty))
                    return "0";
                else
                    return UserData.CounselingCounty;
            }
        }
        protected bool IsQualifiedForUniqueID
        {
            get
            {
                if (this.AccountInfo.IsStateLevel)
                {
                    if (this.AccountInfo.IsShipDirector)
                        return true;
                    else
                    {
                        KeyValuePair<int, string> kvPair = UserData.GetAllDescriptorsForUser.Where(p => p.Key == Descriptor.Counselor.EnumValue<int>()).FirstOrDefault();
                        return !kvPair.Value.IsNull();
                    }
                }

                return false;
            }
        }


        protected string UniqueIDValue
        {
            get
            {
                return UniqueIDData.UniqueID + string.Empty;
            }
        }

        protected bool HasUniqueID
        {
            get
            {
                return (UniqueIDValue != string.Empty);
            }
        }

        private UserUniqueID _UserUniqueID = null;
        protected UserUniqueID UniqueIDData
        {
            get
            {
                if (_UserUniqueID == null)
                {
                    _UserUniqueID = UserBLL.GetUniqueIDForUser(this.AccountInfo.UserId);
                    if (_UserUniqueID == null)
                        _UserUniqueID = new UserUniqueID();
                }

                return _UserUniqueID;
            }
            set
            {
                _UserUniqueID = value;
            }
        }


        private IEnumerable<ZipCountyView> _ZipCountyForState = null;
        public IEnumerable<ZipCountyView> ZipCountyForState
        {
            get
            {
                if (_ZipCountyForState == null)
                    _ZipCountyForState = LookupBLL.GetZipCodesAndCountyFIPSForState(this.AccountInfo.StateFIPS);

                return _ZipCountyForState;
            }
            set
            {
                _ZipCountyForState = value;
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
        }

        protected override object SaveViewState()
        {
            ViewState[VIEWSTATE_KEY_UserIdOfProfileToEdit] = UserIdOfProfileToEdit;
            return base.SaveViewState();
        }
        #endregion



        #region Authorization

        public bool IsAuthorized()
        {
            //On post back we see if the User who retrieved data is indeed the one who modified it.
            if (IsPostBack)
                return (this.AccountInfo.UserId == UserIdOfProfileToEdit);
            else
                return true;
        }

        //the link "Pending Email Change Verifications" should be displayed only for admins with approval permissions
        private bool IsAuthorizedtoViewPendingEmailChangeVerifications
        {
            get
            {
                return ApproverRulesBLL.IsApprover(this.AccountInfo);
            }
        }

        #endregion




    }
}
