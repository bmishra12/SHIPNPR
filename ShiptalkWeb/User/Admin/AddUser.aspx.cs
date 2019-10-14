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

using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessLayer;
using ShiptalkCommon;

using RegistrationObject = ShiptalkLogic.BusinessObjects.UserRegistration;

namespace ShiptalkWeb
{
    public partial class AddUser : System.Web.UI.Page, IRouteDataPage
    {

        private enum DDLRegionSelection
        {
            CMSRegions,
            States,
            SubStateRegions,
            Agencies
        }


        private Role _selectedRole = null;

        private int UserId { get { return this.AccountInfo.UserId; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsAuthorized())
                throw new ShiptalkException("Non Admin Users are not authorized to Add Users.", false, "You are not authorized to view this page.");

            if (!IsPostBack)
            {
                ResetDropdownViews();
                PopulateRoles();
            }
            else
            {
                if (_selectedRole == null)
                    InferRoleSelection();

                //if (dv3colFormContent.Visible != true)
                //    dv3colFormContent.Visible = !dv3colFormContent.Visible;
            }
            
            

        }

        private bool IsAuthorized()
        {
            if (this.AccountInfo.ScopeId.ToEnumObject<Scope>() == Scope.CMS)
                return true;
            else
                return this.AccountInfo.IsAdmin;
        }

        private IEnumerable<Role> GetAvailableNewUserRolesForUserScope()
        {

            return (
                    from role in LookupBLL.Roles
                    where IsUserShipDirector && role.scope == Scope.State
                    && role.IsAdmin == true
                    select role
                   )
                   .Union
                   (
                    from role in LookupBLL.Roles
                    where role.Compare(this.AccountInfo.Scope, ComparisonCriteria.IsLower) == true
                    |
                    (role.Compare(this.AccountInfo.Scope, ComparisonCriteria.IsEqual) == true
                    && role.IsAdmin == false)
                    select role
                   );
        }

        private bool IsUserShipDirector
        {
            get
            {
                int? ShipDirectorId = LookupBLL.GetShipDirectorForState(this.AccountInfo.StateFIPS);
                if (ShipDirectorId.HasValue)
                    return (this.AccountInfo.UserId == ShipDirectorId.Value);

                return false;
            }
        }

        private Role GetStateAdminRoleForShipDirectors
        {
            get
            {
                if (IsUserShipDirector)
                    return (from role in LookupBLL.Roles where role.scope == Scope.State && role.IsAdmin == true select role).First();

                return null;
            }
        }


        private bool IsFormValid()
        {
            if (!Page.IsValid)
                return false;

            if (!VerifyCaptchaText())
            {
                Control ctrl = this.Master.FindControl("body1").FindControl("cvCustomValidator");
                CustomValidator cvCtrl = (CustomValidator)ctrl;
                cvCtrl.ErrorMessage = "The text entered must match the image text. Please try again.";
                cvCtrl.Text = "<BR />" + cvCtrl.ErrorMessage;
                cvCtrl.IsValid = false;
                cvCtrl.EnableViewState = false;

                return false;
            }

            return true;

        }

        private void ShowSuccess()
        {
            colTitle.Text = "Success!";
            dv3colFormContent.Visible = false;
            dv3colDefaultDescription.Visible = false;
            dv3colSuccessMessage.Visible = !dv3colDefaultDescription.Visible;
        }

        private void ClearForm()
        {
            Control control = null;
            foreach (string ctrl in this.Page.Request.Form)
            {
                control = this.Page.FindControl(ctrl);
                if (control != null)
                {
                    switch (control.GetType().Name)
                    {
                        case "TextBox":
                            TextBox txtBox = (TextBox)control;
                            txtBox.Text = "";
                            break;
                        case "DropDownList":
                            DropDownList ddl = (DropDownList)control;
                            ddl.SelectedIndex = 0;
                            break;
                        case "CheckBox":
                            CheckBox chk = (CheckBox)control;
                            chk.Checked = false;
                            break;
                        case "CheckBoxList":
                            CheckBoxList chkList = (CheckBoxList)control;
                            foreach (ListItem li in chkList.Items)
                                li.Selected = false;
                            break;
                        //case "Panel":
                        //    ClearFields((Panel)control);
                        //    break;
                        case "RadioButtonList":
                            RadioButtonList rbl = (RadioButtonList)control;
                            rbl.SelectedIndex = -1;
                            break;
                    }
                }
            }

        }



        #region Initialize Drop downs based on Scope/IsAdmin

        private void PopulateRoles()
        {
            ddlRoles.DataSource = GetAvailableNewUserRolesForUserScope();
            ddlRoles.DataTextField = "Name";
            ddlRoles.DataValueField = "Id";
            ddlRoles.DataBind();

            if (ddlRoles.Items.Count > 1)
                ddlRoles.Items.Insert(0, new ListItem("Select new user role", "0"));
            else if (ddlRoles.Items.Count == 0)
                ddlRoles.Items.Add(new ListItem("--Not available--", "0"));

        }

        private void InitializeUserLocationSpecificViews()
        {
            switch (this.AccountInfo.ScopeId.ToEnumObject<Scope>())
            {
                case Scope.Agency:
                    //Agency Admin can add Users to any Agency where User is an Admin.
                    PopulateRegionData(DDLRegionSelection.Agencies);
                    break;
                case Scope.SubStateRegion:
                    PopulateRegionData(DDLRegionSelection.SubStateRegions);
                    break;
                case Scope.State:
                    //PopulateStatesForUser();
                    break;
                case Scope.CMSRegional:
                    PopulateRegionData(DDLRegionSelection.CMSRegions);
                    break;
                case Scope.CMS:
                    break;
                default:
                    throw new ShiptalkException("ScopeId is not recognized.", false, new ArgumentOutOfRangeException("UserAccount.ScopeId"));
            }
        }

        private void PopulateRegionData(DDLRegionSelection sel)
        {
            switch (sel)
            {
                case DDLRegionSelection.Agencies:
                    PopulateAgenciesForUser();
                    break;
                case DDLRegionSelection.SubStateRegions:
                    PopulateSubStateRegionsForUser();
                    break;
                case DDLRegionSelection.CMSRegions:
                    PopulateCMSRegionsForUser();
                    break;
                default:
                    return;
            }

        }

        private void PopulateAgenciesForUser()
        {
            BindRegionalData(UserAgencyBLL.GetUserAgencyProfiles(UserId, true), ddlAgency);
        }

        private void PopulateSubStateRegionsForUser()
        {
            BindRegionalData(UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(UserId, true), ddlSubStateRegion);
        }

        private void PopulateCMSRegionsForUser()
        {
            BindRegionalData(UserCMSBLL.GetUserCMSRegionalProfiles(UserId, true), ddlCMSRegion);
        }

        private void BindRegionalData(IEnumerable<UserRegionalAccessProfile> regionalProfiles, ListControl ddl)
        {
            ddl.DataSource = regionalProfiles;
            ddl.DataTextField = "RegionName";
            ddl.DataValueField = "RegionId";
            ddl.DataBind();
            ddl.SelectedIndex = regionalProfiles.ToList<UserRegionalAccessProfile>().IndexOf((from profile in regionalProfiles where profile.IsDefaultRegion == true select profile).FirstOrDefault());
            ddl.Focus();
        }
        #endregion


        /// <summary>
        /// Need to show or hide dropdowns or descriptors? 
        /// Do it here. The business logic that drives UI for selected roles is here.
        /// </summary>
        private void SetupUIForSelectedRole()
        {
            if (_selectedRole.Compare(Scope.CMSRegional, ComparisonCriteria.IsEqual))
                ShowDropdowns(DDLRegionSelection.CMSRegions);
            else if (_selectedRole.Compare(Scope.State, ComparisonCriteria.IsEqual))
            {
                if (this.AccountInfo.Scope != Scope.State)
                    ShowDropdowns(DDLRegionSelection.States);
            }
            else if (_selectedRole.Compare(Scope.SubStateRegion, ComparisonCriteria.IsEqual))
            {
                if (this.AccountInfo.Scope.CompareTo(Scope.State, ComparisonCriteria.IsHigher))
                    ShowDropdowns(DDLRegionSelection.States);
                else
                    ShowDropdowns(DDLRegionSelection.SubStateRegions);
            }
            else if (_selectedRole.Compare(Scope.Agency, ComparisonCriteria.IsEqual))
            {
                if (this.AccountInfo.Scope.CompareTo(Scope.State, ComparisonCriteria.IsHigher))
                    ShowDropdowns(DDLRegionSelection.States);
                else
                    ShowDropdowns(DDLRegionSelection.Agencies);
            }


            DisplayRoleDescription();
            //CMS Users - do not require any drop downs for selection

        }

        private void DisplayRoleDescription()
        {
            lblRoleDescription.Text = _selectedRole.Description;
            plhRoleDescription.Visible = true;
        }

        /// <summary>
        /// Route calls to methods that perform action for the selected drop down.
        /// </summary>
        /// <param name="dropdown"></param>
        private void ShowDropdowns(DDLRegionSelection dropdown)
        {

            string StateFIPS = string.Empty;
            switch (dropdown)
            {
                case DDLRegionSelection.CMSRegions:
                    HandleCMSRegionDisplayLogic();
                    break;
                case DDLRegionSelection.States:
                    HandleStateDisplayLogic();
                    break;
                case DDLRegionSelection.SubStateRegions:
                    if (this.AccountInfo.Scope == Scope.State)
                        StateFIPS = this.AccountInfo.StateFIPS;
                    else
                        StateFIPS = ddlStates.SelectedValue;

                    HandleSubStateDisplayLogic(StateFIPS);
                    break;
                case DDLRegionSelection.Agencies:
                    if (this.AccountInfo.Scope == Scope.State)
                        StateFIPS = this.AccountInfo.StateFIPS;
                    else
                        StateFIPS = ddlStates.SelectedValue;

                    HandleAgencyDisplayLogic(StateFIPS);
                    break;
            }
        }

        private void HandleCMSRegionDisplayLogic()
        {
            plhCMSRegion.Visible = true;
            if (this.AccountInfo.Scope == Scope.CMSRegional)
                PopulateCMSRegionsForUser();
            else
            {
                ddlCMSRegion.DataSource = LookupBLL.GetCMSRegions();
                ddlCMSRegion.DataTextField = "Value";
                ddlCMSRegion.DataValueField = "Key";
                ddlCMSRegion.DataBind();
            }

            if (ddlCMSRegion.Items.Count > 1)
                ddlCMSRegion.Items.Insert(0, new ListItem("Select CMS Region", "0"));
        }


        private void HandleStateDisplayLogic()
        {
            divStates.Visible = true;
            ddlStates.DataSource = State.GetStatesWithFIPSKey();
            ddlStates.DataTextField = "Value";
            ddlStates.DataValueField = "Key";
            ddlStates.DataBind();

            ddlStates.Items.Insert(0, new ListItem("Select a State", "0"));
        }


        private void HandleSubStateDisplayLogic(string StateFIPS)
        {
            plhSubStateRegion.Visible = true;
            if (this.AccountInfo.Scope != Scope.SubStateRegion)
            {
                ddlSubStateRegion.DataSource = LookupBLL.GetSubStateRegionsForState(StateFIPS);
                ddlSubStateRegion.DataTextField = "Value";
                ddlSubStateRegion.DataValueField = "Key";
                ddlSubStateRegion.DataBind();
            }
            else
                PopulateSubStateRegionsForUser();

            if (ddlSubStateRegion.Items.Count > 1)
                ddlSubStateRegion.Items.Insert(0, new ListItem("Select Sub State Region", "0"));
        }


        private void HandleAgencyDisplayLogic(string StateFIPS)
        {
            plhAgencies.Visible = true;

            if (this.AccountInfo.Scope != Scope.Agency)
            {
                ddlAgency.DataSource = LookupBLL.GetAgenciesForState(StateFIPS);
                ddlAgency.DataTextField = "Value";
                ddlAgency.DataValueField = "Key";
                ddlAgency.DataBind();
            }
            else
                PopulateAgenciesForUser();

            if (ddlAgency.Items.Count > 1)
                ddlAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
        }

        #region PostBack event handlers
        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Override the LoadViewState value here.
            _selectedRole = null;

            //Need to reset dropdowns elements that were shown earlier because Role changed.
            ResetDropdownViews();

            //If no role is selected, they must be forced to do so.
            if (ddlRoles.SelectedValue == "0")
            {
                plhRoleDescription.Visible = false;
                DisplayMessage("Please select a role before you provide further information", true);
                return;
            }
            else
                InferRoleSelection();

            //Keep this along with similar code in ddlStates_SelectedIndexChanged for better user experience.
            if (_selectedRole.Compare(Scope.State, ComparisonCriteria.IsLower))
                ddlStates.AutoPostBack = true;

            //Display UI objects for selected role.
            SetupUIForSelectedRole();
        }

        public void ddlStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStates.SelectedValue == "0")
            {
                DisplayMessage("Please select the State.", true);
                return;
            }
            else
            {
                //Once state is selected, no need to auto post back the next time ONLY if User is above State Level
                //For Sub State and Agency Level Users, we need to post back
                if (_selectedRole.Compare(Scope.State, ComparisonCriteria.IsHigherThanOrEqualTo))
                    ddlStates.AutoPostBack = false;
                else
                {
                    ddlStates.AutoPostBack = true;
                    //Show Sub State Regions if Role is for Sub State Regional User
                    //Show Agencies if Role is Agencies
                    if (_selectedRole.Compare(Scope.SubStateRegion, ComparisonCriteria.IsEqual))
                        ShowDropdowns(DDLRegionSelection.SubStateRegions);
                    else if (_selectedRole.Compare(Scope.Agency, ComparisonCriteria.IsEqual))
                        ShowDropdowns(DDLRegionSelection.Agencies);
                }
            }

        }


        public void OnShipDirectorCheckboxAction(object sender, EventArgs e)
        {
            cblDescriptors.Visible = !chBoxIsShipDirector.Checked;
        }

        public string UserName { get; private set; }


        protected void RegisterUserCommand(object sender, EventArgs e)
        {
            //if(valid)
            Page.Validate("RegisterForm");

            if (!IsFormValid())
            {
                DisplayMessage("Please fix the error(s) displayed in red and submit again", true);
                return;
            }

            UserName = Email.Text.Trim();

            if (RegisterUserBLL.DoesUserNameExist(UserName))
            {
                DisplayMessage("The Primary Email address is already registered. Duplicates are not allowed.", true);
                return;
            }
            else
            {

                //TODO: Add transaction scope, email verification logic and appropriate error messages.
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    int? newUserId;
                    if (!UserBLL.CreateUser(CreateRegistrationObject(), null, false, this.AccountInfo.UserId, out newUserId))
                    {
                        DisplayMessage("Unable to add user. Please contact support if the issue persists.", true);
                        return;
                    }
                    else
                    {
                        if (!SendVerificationEmail(newUserId.Value))
                            return;
                        else
                        {
                            scope.Complete();
                            ShowSuccess();
                            ClearForm();
                        }
                    }
                }

            }

        }


        protected bool SendVerificationEmail(int newUserId)
        {
            string EmailAddress = UserName;

            Guid VerificationToken = UserBLL.GetEmailVerificationTokenForUser(newUserId);
            string sVerificationToken = string.Empty;
            if (VerificationToken == Guid.Empty)
            {
                DisplayMessage("An error occured while preparing content for email address verification procedure. Please contact support if this issue persists.", true);
                return false;
            }
            else
                sVerificationToken = VerificationToken.ToString();

            string linkFormat = "<a href='" + ConfigUtil.EmailConfirmationUrl + "?evt={0}'>Follow this link</a>";
            string confirmLinkParam = EmailAddress  + sVerificationToken;
            string confirmLink = string.Format(linkFormat, confirmLinkParam);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.AppendFormat("Dear {0} {1},", FirstName.Text.Trim().ToCamelCasing(), LastName.Text.Trim().ToCamelCasing());
            sb.AddNewHtmlLines(2);
            sb.Append("A new user account has been registered for you, at the SHIPtalk.org web site.");
            sb.AddNewHtmlLines(2);
            sb.Append("It is required that you verify your email address using the following link:");
            sb.AddNewHtmlLine();
            sb.AppendFormat(confirmLink);
            sb.AddNewHtmlLines(2);
            sb.Append("You will be able to login to the SHIPtalk web site upon successful verification of the email using the above link.");
            sb.AddNewHtmlLine();
            sb.AppendFormat("If you would like to contact the person who created your account, please use the following email address: {0}", this.AccountInfo.PrimaryEmail);
            sb.AddNewHtmlLines(2);
            sb.Append("Thank you,");
            sb.AddNewHtmlLine();
            sb.Append("SHIPtalk.org");
            sb.AddNewHtmlLine();
            sb.Append(ConfigUtil.ShiptalkSupportPhone);
            sb.AddNewHtmlLines(5);
            

            ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
            mailMessage.ToList.Add(this.UserName);
            mailMessage.Subject = "Welcome to SHIPtalk.org!";

            mailMessage.Body = sb.ToString();
            ShiptalkMail mail = new ShiptalkMail(mailMessage);
            if (!mail.SendMail())
            {
                DisplayMessage("An error occured while sending email to the address provided.", true);
                return false;
            }
            else
                return true;


        }


        private RegistrationObject CreateRegistrationObject()
        {
            //Fill personal profile info here...
            RegistrationObject regObj = new RegistrationObject();
            regObj.FirstName = FirstName.Text.Trim();
            regObj.MiddleName = MiddleName.Text.Trim();
            regObj.LastName = LastName.Text.Trim();
            regObj.NickName = NickName.Text.Trim();
            regObj.Suffix = Suffix.Text.Trim();
            regObj.Honorifics = Honorifics.Text.Trim();
            regObj.SecondaryEmail = SecondaryEmail.Text.Trim();
            regObj.PrimaryPhone = PrimaryPhone.Text.Trim();
            regObj.SecondaryPhone = SecondaryPhone.Text.Trim();

            //Fill login info and Role
            regObj.PrimaryEmail = UserName;
            regObj.Password = Password.Text.Trim();
            regObj.RoleRequested = _selectedRole;

            regObj.StateFIPS = GetStateFIPSForNewUser();

            //Get regional IDs (AgencyID, Sub State Regional ID etc)
            switch (regObj.RoleRequested.scope)
            {
                case Scope.CMSRegional:
                    regObj.UserRegionalAccessProfile.RegionId = int.Parse(ddlCMSRegion.SelectedValue.Trim());
                    break;
                case Scope.SubStateRegion:
                    regObj.UserRegionalAccessProfile.RegionId = int.Parse(ddlSubStateRegion.SelectedValue.Trim());
                    break;
                case Scope.Agency:
                    regObj.UserRegionalAccessProfile.RegionId = GetAgencyIdForNewUser();
                    PopulateAgencyUserDescriptors(ref regObj, regObj.UserRegionalAccessProfile.RegionId);
                    break;
                case Scope.State:
                    regObj.IsShipDirector = chBoxIsShipDirector.Checked;
                    break;
            }

            //Populate User Descriptors for the Regions other than Agencies
            if (regObj.RoleRequested.Compare(Scope.Agency, ComparisonCriteria.IsHigher))
                PopulateNonAgencyUserDescriptors(ref regObj);

            return regObj;

        }

        private string GetStateFIPSForNewUser()
        {
            string StateFIPS = string.Empty;

            //Users at State level and below 
            if (_selectedRole.Compare(Scope.State, ComparisonCriteria.IsLowerThanOrEqualTo))
            {
                //On certain complex scenarios, States DDL is not displayed.
                //If the selected role is a non-CMS User, but the States DDL wasn't displayed,
                //then it is because, the User who is using the 'Add User' feature is a State Level Admin.
                //Use his StateFIPS.
                if (ddlStates.Items.Count == 0 || ddlStates.SelectedValue.Trim() == string.Empty)
                    StateFIPS = this.AccountInfo.StateFIPS;
                else
                    StateFIPS = ddlStates.SelectedValue.Trim();
            }
            return StateFIPS;
        }

        private int GetAgencyIdForNewUser()
        {
            string sID = string.Empty;

            if (ddlAgency.Items.Count == 0)
            {
                if (ddlAgency.SelectedValue.Trim() == string.Empty)
                    sID = UserAgencyBLL.GetUserAgencyProfiles(this.AccountInfo.UserId, true).First().RegionId.ToString();

                //throw new ShiptalkException("The Agency must be provided to create a new Agency Level User", false, new ArgumentNullException("AgencyId"));
            }
            else if (ddlAgency.Items.Count == 1)
                sID = ddlAgency.Items[0].Value;
            else
                sID = ddlAgency.SelectedValue.Trim();


            return int.Parse(sID);
        }


        /// <summary>
        /// Populate descriptors for State Users, Sub State Users etc who are not directly related to an Agency
        /// </summary>
        /// <param name="regObj"></param>
        private void PopulateNonAgencyUserDescriptors(ref ShiptalkLogic.BusinessObjects.UserRegistration regObj)
        {
            PopulateAgencyUserDescriptors(ref regObj, ShiptalkLogic.Constants.Defaults.DefaultValues.AgencyIdForNonAgencyUsers);
        }

        /// <summary>
        /// Populate descriptors for Agency Users whose role is tied to an agency.
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="AgencyId"></param>
        private void PopulateAgencyUserDescriptors(ref ShiptalkLogic.BusinessObjects.UserRegistration regObj, int AgencyId)
        {
            //UserDescriptor descr;
            for (int i = 0; i < cblDescriptors.Items.Count; i++)
            {
                if (cblDescriptors.Items[i].Selected)
                {
                    regObj.UserRegionalAccessProfile.DescriptorIDList.Add(int.Parse(cblDescriptors.Items[i].Value));
                    //descr = new UserDescriptor();
                    //descr.DescriptorId = int.Parse(cblDescriptors.Items[i].Value);
                    //descr.AgencyId = AgencyId;
                    //regObj.UserDescriptorList.Add(descr);
                }

            }
        }
        #endregion


        private void DisplayMessage(string message, bool IsError)
        {
            plhMessage.Visible = true;
            if (IsError)
                lblMessage.CssClass = "required";
            else
                lblMessage.CssClass = "info";
            lblMessage.Text = message;
        }


        protected void ResetDropdownViews()
        {
            plhCMSRegion.Visible = false;
            divStates.Visible = false;
            plhSubStateRegion.Visible = false;
            plhAgencies.Visible = false;
            cblDescriptors.Visible = false;
            plhIsShipDirector.Visible = false;
        }

        private void InferRoleSelection()
        {
            _selectedRole = LookupBLL.Roles.Find(p => p.Id == Int16.Parse(ddlRoles.SelectedValue));
            if (_selectedRole == null)
                ShiptalkException.ThrowSecurityException("Possible role list manipulation. The selected role was not found.", "Sorry. We're unable to serve your request. Please contact support for assistance");

        }

        #region "View state events"
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            _selectedRole = (Role)ViewState["RoleSelected"];
        }

        protected override object SaveViewState()
        {
            ViewState["RoleSelected"] = _selectedRole;
            return base.SaveViewState();
        }
        #endregion


        #region Lanap Captcha stuff
        private bool VerifyCaptchaText()
        {
            //To navigate to the ContentTemplate, use CreateUserWizard1.WizardSteps[0].Controls[0]
            Control ctrl = this.Master.FindControl("body1").FindControl("ctrlCaptcha");
            //I won't check for null. The page should work or the custom eror should catch it, so its fixed.
            //if (ctrl != null)
            Lanap.BotDetect.Captcha captchaControl = (Lanap.BotDetect.Captcha)ctrl;

            ctrl = this.Master.FindControl("body1").FindControl("CaptchaText");
            TextBox txtCaptchaText = (TextBox)ctrl;

            return captchaControl.Validate(txtCaptchaText.Text.Trim());

        }


        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion
    }
}
