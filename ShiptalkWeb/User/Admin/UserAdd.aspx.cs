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
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkLogic.BusinessLayer;
using ShiptalkCommon;

using RegistrationObject = ShiptalkLogic.BusinessObjects.UserRegistration;
using System.Text.RegularExpressions;

namespace ShiptalkWeb
{
    public partial class UserAdd : System.Web.UI.Page, IRouteDataPage
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
                InitializeRoles();
            }
            else
            {
                //if (_selectedRole == null)
                //    InferRoleSelection();

                //if (dv3colFormContent.Visible != true)
                //    dv3colFormContent.Visible = !dv3colFormContent.Visible;
            }

            //EmailFmtValidate.DataBind();
            //SecondaryEmailFmtValidate.DataBind();
        }

       

        private bool IsAuthorized()
        {
            if (this.AccountInfo.IsCMSScope || this.AccountInfo.IsStateScope)
                return this.AccountInfo.IsAdmin;
            else
            {
                UserViewData viewData = UserBLL.GetUser(this.AccountInfo.UserId);
                if (viewData.RegionalProfiles != null && viewData.RegionalProfiles.Count > 0)
                {
                    if (viewData.RegionalProfiles.Count > 1)
                    {
                        foreach (UserRegionalAccessProfile profile in viewData.RegionalProfiles)
                        {
                            //Admin at, atleast 1 location
                            if (profile.IsAdmin)
                                return true;
                        }
                        //No admin found
                        return false;
                    }
                    else
                    {
                        //Admin at the single location?
                        return viewData.RegionalProfiles[0].IsAdmin;
                    }
                }
                else
                    return false;
            }
        }

        private IEnumerable<Role> GetAvailableNewUserRolesForUserScope()
        {
            //List<ShiptalkLogic.BusinessObjects.Role> roles = new List<Role>();

            //return State Admin only if User is current User is Ship Director.
            //roles.AddRange(
            return  
                GetSameScopeAdminRoleIfApprover
                .Union
                (
                from role in LookupBLL.Roles where role.scope == Scope.State && role.IsAdmin == true && IsUserShipDirector select role
                )
                .Union
                (//Return all User level roles in same scope PLUS all Roles in lower level scopes.
                    from role in LookupBLL.Roles
                    where role.Compare(this.AccountInfo.Scope, ComparisonCriteria.IsLower) == true
                    |
                    (role.Compare(this.AccountInfo.Scope, ComparisonCriteria.IsEqual) == true
                    && role.IsAdmin == false)
                    select role
                )
                ;
            
            //Added 04/30/2010 - Approver can add user of: same scope as him PLUS admin rights.
//            if (ApproverRulesBLL.IsApprover(this.AccountInfo))
 //               roles.Add((from role in LookupBLL.Roles where role.scope.IsEqual(this.AccountInfo.Scope) && role.IsAdmin == true select role).FirstOrDefault());

   //         return roles;
        }

        private IEnumerable<Role> GetSameScopeAdminRoleIfApprover
        {
            get
            {
                List<Role> AdminRoles = new List<Role>();

                if (this.AccountInfo.Scope.IsEqual(Scope.CMS) && ApproverRulesBLL.IsApprover(this.AccountInfo))
                    AdminRoles.Add(LookupBLL.GetRole(Scope.CMS, true));
                else if (ApproverRulesBLL.IsApprover(this.AccountInfo))
                    AdminRoles.AddRange(from role in LookupBLL.Roles where role.scope.IsEqual(this.AccountInfo.Scope) && role.IsAdmin == true select role);

                return AdminRoles;
            }
        }

        private bool IsUserShipDirector
        {
            get
            {
                return this.AccountInfo.IsShipDirector;
                //int? ShipDirectorId = LookupBLL.GetShipDirectorForState(this.AccountInfo.StateFIPS);
                //if (ShipDirectorId.HasValue)
                //    return (this.AccountInfo.UserId == ShipDirectorId.Value);

                //return false;
            }
        }

        private Role GetStateAdminRoleForShipDirectors
        {
            get
            {
                if (IsUserShipDirector)
                    return (from role in LookupBLL.Roles where role.scope == Scope.State && role.IsAdmin == true select role).FirstOrDefault();

                return null;
            }
        }


        private bool IsFormValid()
        {
            if (!_selectedRole.IsAdmin && cbIsApprover.Enabled && cbIsApprover.Checked)
            {
                cvIsApproverError.ErrorMessage = "Only Admins can be approvers. The role must be an administrator role";
                cvIsApproverError.IsValid = false;
            }
            else if (cbIsSuperEditor.Visible && cbIsSuperEditor.Checked && !chBoxIsShipDirector.Checked)
            {
                //If NPR ReadOnly Checkbox is checked, then Super Editor cannot be checked.
                if (dvCblDescriptors.Visible)
                {
                    if (cblDescriptors.Items != null && cblDescriptors.Items.Count > 0)
                    {
                        for (int i = 0; i < cblDescriptors.Items.Count; i++)
                        {
                            if (cblDescriptors.Items[i].Selected)
                            {
                                if (int.Parse(cblDescriptors.Items[i].Value) == Descriptor.OtherStaff_NPR.EnumValue<int>())
                                {
                                    cvIsSuperEditorError.ErrorMessage = "NPR Read Only Users cannot be super editors.";
                                    cvIsSuperEditorError.IsValid = false;
                                    break;
                                }
                            }

                        }
                    }
                }
            }
            else if (_selectedRole.scope.IsLowerOrEqualTo(Scope.SubStateRegion))
            {
                if (cbIsApprover.Enabled && cbIsApprover.Checked)
                {
                    if (_selectedRole.scope.IsEqual(Scope.SubStateRegion) && (ddlAgency.SelectedValue != "0"))
                    {
                        if (!IsAdminApproverForSelectedSubState(int.Parse(ddlSubStateRegion.SelectedValue)))
                        {
                            cvIsApproverError.ErrorMessage = "You are not an approver in the chosen sub state. You cannot make another person an approver in the sub state.";
                            cvIsApproverError.IsValid = false;
                        }
                    }
                    else if (_selectedRole.scope.IsEqual(Scope.Agency) && (ddlAgency.SelectedValue != "0"))
                    {
                            if (!IsAdminApproverForSelectedAgency(int.Parse(ddlAgency.SelectedValue)))
                            {
                                cvIsApproverError.ErrorMessage = "You are not an approver in the chosen agency. You cannot make another person an approver in the agency.";
                                cvIsApproverError.IsValid = false;
                            }
                    }
                }


            }

            if (!Page.IsValid)
                return false;

            //if (!VerifyCaptchaText())
            //{
            //    Control ctrl = this.Master.FindControl("body1").FindControl("cvCustomValidator");
            //    CustomValidator cvCtrl = (CustomValidator)ctrl;
            //    cvCtrl.ErrorMessage = "The text entered must match the image text. Please try again.";
            //    cvCtrl.Text = "<BR />" + cvCtrl.ErrorMessage;
            //    cvCtrl.IsValid = false;
            //    cvCtrl.EnableViewState = false;

            //    return false;
            //}

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

        private void InitializeRoles()
        {
            ddlRoles.DataSource = GetAvailableNewUserRolesForUserScope();
            ddlRoles.DataTextField = "Name";
            ddlRoles.DataValueField = "Id";
            ddlRoles.DataBind();

            if (ddlRoles.Items.Count > 1)
            {
                ddlRoles.Items.Insert(0, new ListItem("Select new user role", "0"));
                ddlRoles.SelectedValue = "0";
            }
            else if (ddlRoles.Items.Count == 1)
            {
                if (this.AccountInfo.Scope.IsEqual(Scope.Agency))
                {
                    ddlRoles.SelectedValue = ddlRoles.Items[0].Value;
                    InferRoleSelection();
                    SetupUIForSelectedRole();
                    //PopulateRegionData(DDLRegionSelection.Agencies);
                }
            }
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

        private void PopulateAgenciesForSubStateUser()
        {
            IEnumerable<UserRegionalAccessProfile> SubStateProfiles = UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(UserId, true);
            List<KeyValuePair<int, string>> Agencies = new List<KeyValuePair<int, string>>();
            foreach (UserRegionalAccessProfile subStProfile in SubStateProfiles)
            {
                IEnumerable<ShiptalkLogic.BusinessObjects.Agency> AgencyForSubState = LookupBLL.GetAgenciesForSubStateRegion(subStProfile.RegionId);
                Agencies.AddRange(AgencyForSubState.Where(elem => elem.IsActive == true).Select(p => (new KeyValuePair<int, string>(p.Id.Value, p.Name))));
            }

            if (Agencies != null && Agencies.Count > 1)
                ddlAgency.DataSource = Agencies.Distinct().OrderBy( p => p.Value);
            else
                ddlAgency.DataSource = Agencies;

            ddlAgency.DataTextField = "Value";
            ddlAgency.DataValueField = "Key";
            ddlAgency.DataBind();

            if (Agencies == null || Agencies.Count() == 0)
                ddlAgency.Items.Add(new ListItem("No agencies available", "0"));
            else if (Agencies.Count() > 1)
            {
                ddlAgency.Items.Insert(0, new ListItem("-- Select agency --", "0"));
                //ddlRoles.SelectedValue = "0";
            }
           
            

        }

        private void PopulateAgenciesForUser()
        {
            IEnumerable<UserRegionalAccessProfile> adminProfiles = UserAgencyBLL.GetUserAgencyProfiles(UserId, true);
            
            if (IsSelectedRoleApproverSpecial)
                adminProfiles = adminProfiles.Where(p => p.IsApproverDesignate == true);

            if (adminProfiles != null)
                adminProfiles = adminProfiles.OrderBy(p => p.RegionName);

            BindRegionalData(adminProfiles, ddlAgency);
            if (adminProfiles == null || adminProfiles.Count() == 0)
                ddlAgency.Items.Add(new ListItem("No agencies available", "0"));
            else if (ddlAgency.Items.Count > 1)
            {
                ddlAgency.Items.Insert(0, new ListItem("-- Select agency --", "0"));
               // ddlRoles.SelectedValue = "0";
            }
        }

        /// <summary>
        /// Not to read too  much into the word Special.
        /// If the role is of same scope as the logged in User and also the Role is for Admin User,
        /// it is possible that it is created by only Ship Director for State Level Users
        /// Or
        /// created by Approver Designates for users of other scopes.
        /// </summary>
        private bool IsSelectedRoleApproverSpecial
        {
            get
            {
                bool IsSpecialRole = false;
                
                if (_selectedRole.scope.IsEqual(this.AccountInfo.Scope) && _selectedRole.IsAdmin && !this.AccountInfo.IsShipDirector)
                    IsSpecialRole = true;

                return IsSpecialRole;
            }
        }

        private void PopulateSubStateRegionsForUser()
        {
            //A role was selected. Sub States need to be populated.
            //If the Role is of Admin type which requires the admin to be approver, then, display only those
            //sub states where User is approver.
            var adminProfiles = UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(UserId, true);

            if (adminProfiles != null)
            {
                if (IsSelectedRoleApproverSpecial)
                    adminProfiles = adminProfiles.Where(p => p.IsApproverDesignate == true);

                adminProfiles = adminProfiles.OrderBy(p => p.RegionName);
            }
            
            BindRegionalData(adminProfiles, ddlSubStateRegion);
            if (adminProfiles == null || adminProfiles.Count() == 0)
                ddlSubStateRegion.Items.Add(new ListItem("No substates available", "0"));
            else
            {
                ddlSubStateRegion.Items.Insert(0, new ListItem("-- Select substate region --", "0"));
               // ddlSubStateRegion.SelectedValue = "0";
            }
        }

        private void PopulateCMSRegionsForUser()
        {
            var adminProfiles = UserCMSBLL.GetUserCMSRegionalProfiles(UserId, true);
            if (adminProfiles != null)
                adminProfiles = adminProfiles.OrderBy(p => p.RegionName);

            BindRegionalData(adminProfiles, ddlCMSRegion);
            if (adminProfiles == null || adminProfiles.Count() == 0)
                ddlCMSRegion.Items.Add(new ListItem("No CMS region available", "0"));
            else
            {
                ddlCMSRegion.Items.Insert(0, new ListItem("-- Select CMS region --", "0"));
                //ddlCMSRegion.SelectedValue = "0";
            }
        }

        private void BindRegionalData(IEnumerable<UserRegionalAccessProfile> regionalProfiles, ListControl ddl)
        {
            ddl.DataSource = regionalProfiles;
            ddl.DataTextField = "RegionName";
            ddl.DataValueField = "RegionId";
            ddl.DataBind();
            //ddl.SelectedIndex = regionalProfiles.ToList<UserRegionalAccessProfile>().IndexOf((from profile in regionalProfiles where profile.IsDefaultRegion == true select profile).FirstOrDefault());
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
            {
                ShowDropdowns(DDLRegionSelection.CMSRegions);
            }
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

            //Added 04/29/2010 - Ensure descriptors are visible for all State Level Users but Ship Directors
            dvCblDescriptors.Visible = (_selectedRole.scope.IsLowerOrEqualTo(Scope.State) && !chBoxIsShipDirector.Checked);
            if (!_selectedRole.IsAdmin)
                SetApproverDesignateArea(false);
            else
            {
                SetApproverDesignateArea(ApproverRulesBLL.IsApproverForRole(this.AccountInfo, _selectedRole));
            }

            DisplayRoleDescription();
            //CMS Users - do not require any drop downs for selection

        }

        private void DisplayRoleDescription()
        {
            lblRoleDescription.Text = _selectedRole.Description;
            plhRoleDescription.Visible = true;
        }
        private void SetApproverDesignateArea(bool IsEnabled)
        {
            cbIsApprover.Enabled = IsEnabled;

            //When Approver area is disabled, uncheck it, so when it is enabled back again, it is not checked.
            if (!cbIsApprover.Enabled)
                cbIsApprover.Checked = false;
        }

        //private void HandleApproverDisplayLogic()
        //{
        //    if (!_selectedRole.IsAdmin)
        //        SetApproverDesignateArea(false);
        //    else
        //    {
        //        if (_selectedRole.scope.IsHigherOrEqualTo(Scope.CMSRegional))
        //            SetApproverDesignateArea(AccessRulesBLL.IsApproverAtCMS(this.AccountInfo));
        //        if (_selectedRole.scope.IsEqual(Scope.State))
        //            SetApproverDesignateArea(AccessRulesBLL.IsApproverForState(this.AccountInfo, this.AccountInfo.StateFIPS));
        //        if (_selectedRole.scope.IsEqual(Scope.SubStateRegion))
        //        {
        //            if (ddlSubStateRegion.Visible && ddlSubStateRegion.SelectedValue != "0")
        //                SetApproverDesignateArea(AccessRulesBLL.IsApproverForSubState(this.AccountInfo, int.Parse(ddlSubStateRegion.SelectedValue)));
        //            else
        //                SetApproverDesignateArea(AccessRulesBLL.IsApproverForState(this.AccountInfo, this.AccountInfo.StateFIPS));
        //        }
        //        if (_selectedRole.scope.IsEqual(Scope.Agency))
        //        {
        //            if (ddlAgency.Visible && ddlAgency.SelectedValue != "0")
        //                SetApproverDesignateArea(AccessRulesBLL.IsApproverForAgency(this.AccountInfo, int.Parse(ddlAgency.SelectedValue)));
        //            else
        //                SetApproverDesignateArea(AccessRulesBLL.IsApproverForState(this.AccountInfo, this.AccountInfo.StateFIPS));
        //        }
        //    }
        //}

       

        /// <summary>
        /// Check if User is approver at a specific State 
        /// </summary>
        /// <returns></returns>
        private bool IsAdminApproverForSelectedState(string StateFIPS)
        {
            return ApproverRulesBLL.IsApproverForState(this.AccountInfo, StateFIPS);
        }
        /// <summary>
        /// Check if User is approver at a specific SubState
        /// </summary>
        /// <param name="SubStateId"></param>
        /// <returns></returns>
        private bool IsAdminApproverForSelectedSubState(int SubStateId)
        {
            return ApproverRulesBLL.IsApproverForSubState(this.AccountInfo, SubStateId);
        }
        /// <summary>
        /// Check if User is approver at a specific agency
        /// </summary>
        /// <param name="AgencyId"></param>
        /// <returns></returns>
        private bool IsAdminApproverForSelectedAgency(int AgencyId)
        {
            return ApproverRulesBLL.IsApproverForAgency(this.AccountInfo, AgencyId);
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
                    if (this.AccountInfo.Scope.IsLowerOrEqualTo(Scope.State))
                        StateFIPS = this.AccountInfo.StateFIPS;
                    else
                        StateFIPS = ddlStates.SelectedValue;

                    HandleSubStateDisplayLogic(StateFIPS);
                    break;
                case DDLRegionSelection.Agencies:
                    if (this.AccountInfo.Scope.IsLowerOrEqualTo(Scope.State))
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
                var CMSRegions = LookupBLL.GetCMSRegions();
                if (CMSRegions != null)
                {
                    IEnumerable<KeyValuePair<int, string>> OrderedCMSRegions = CMSRegions.OrderBy(p => p.Value);
                    ddlCMSRegion.DataSource = OrderedCMSRegions;
                }
                else
                    ddlCMSRegion.DataSource = CMSRegions;

                ddlCMSRegion.DataTextField = "Value";
                ddlCMSRegion.DataValueField = "Key";
                ddlCMSRegion.DataBind();
            }

            if (ddlCMSRegion.Items.Count > 1)
                ddlCMSRegion.Items.Insert(0, new ListItem("Select CMS Region", "0"));
            else
                ddlCMSRegion.Items.Add(new ListItem("No CMS Regions available", "0"));
        }


        private void HandleStateDisplayLogic()
        {
            divStates.Visible = true;
            ddlStates.DataSource = State.GetStatesWithFIPSKey().OrderBy( st => st.Value);
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
                var SubStates = LookupBLL.GetSubStateRegionsForState(StateFIPS);
                if (SubStates != null)
                {
                    IEnumerable<KeyValuePair<int, string>> OrderedSubStates = SubStates.OrderBy(p => p.Value);
                    ddlSubStateRegion.DataSource = OrderedSubStates;
                }
                else
                    ddlSubStateRegion.DataSource = SubStates;

                ddlSubStateRegion.DataTextField = "Value";
                ddlSubStateRegion.DataValueField = "Key";
                ddlSubStateRegion.DataBind();

                if (ddlSubStateRegion.Items.Count > 1)
                    ddlSubStateRegion.Items.Insert(0, new ListItem("Select Sub State Region", "0"));
                else
                    ddlSubStateRegion.Items.Add(new ListItem("No Sub State Regions available", "0"));
            }
            else
                PopulateSubStateRegionsForUser();

           

        }


        private void HandleAgencyDisplayLogic(string StateFIPS)
        {
            plhAgencies.Visible = true;

            if (this.AccountInfo.Scope.IsHigherOrEqualTo(Scope.State))
            {
                var Agencies = LookupBLL.GetAgenciesForState(StateFIPS);
                if (Agencies != null)
                {
                    IEnumerable<KeyValuePair<int, string>> OrderedAgencies = Agencies.OrderBy(p => p.Value);
                    ddlAgency.DataSource = OrderedAgencies;
                }
                else
                    ddlAgency.DataSource = Agencies;
                
                ddlAgency.DataTextField = "Value";
                ddlAgency.DataValueField = "Key";
                ddlAgency.DataBind();

                if (ddlAgency.Items.Count > 1)
                    ddlAgency.Items.Insert(0, new ListItem("-- Select agency --", "0"));
                else
                    ddlAgency.Items.Add(new ListItem("No agencies available", "0"));
            }
            else if (this.AccountInfo.Scope.IsEqual(Scope.SubStateRegion))
            {
                PopulateAgenciesForSubStateUser();
            }
            else
            {
                PopulateAgenciesForUser();
            }

           
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
            dvCblDescriptors.Visible = !chBoxIsShipDirector.Checked;
            if (chBoxIsShipDirector.Checked)
                SetApproverDesignateArea(false);
            else
                SetApproverDesignateArea(IsAdminApproverForSelectedState(ddlStates.SelectedValue));
        }

        public string UserName { get; private set; }


        protected void AddUserCommand(object sender, EventArgs e)
        {
            //if(valid)
            Page.Validate("RegisterForm");

            bool FormIsValid = IsFormValid();

            //For some reason, CaptchaText EnableViewState=false isn't working. So doing this...
            //CaptchaText.Text = string.Empty;

            if (!FormIsValid)
            {
                DisplayMessage("Please fix the error(s) displayed in red and submit again", true);
                return;
            }

            //sammit-start
            if (PasswordValidation.DoesTextContainsWord(Password.Text.Trim().ToLower()))
            {
                DisplayMessage("The entered password contains a dictionary word and is not allowed.", true);
                return;
            }

            if (PasswordValidation.DoesTextContainsFirstLastName(Password.Text.Trim().ToLower(), FirstName.Text.Trim().ToLower(), LastName.Text.Trim().ToLower(), MiddleName.Text.Trim().ToLower()))
            {
                DisplayMessage("The entered password contains either FirstName/MiddleName/LastName and is not allowed.", true);
                return;
            }

            if (PasswordValidation.DoesPassWordContainsEmail(Email.Text.Trim().ToLower(), Password.Text.Trim().ToLower()))
            {
                DisplayMessage("The entered password contains your email-id and is not allowed.", true);
                return;
            }

            if (PasswordValidation.DoesContainFourConsecutive(Password.Text.Trim().ToLower()))
            {
                DisplayMessage("The entered password contains 4 consecutive letter/number and is not allowed.", true);
                return;
            }
            //sammit-end

            UserName = Email.Text.Trim();

            if (RegisterUserBLL.DoesUserNameExist(UserName))
            {
                DisplayMessage("The Primary Email address is already registered. Duplicates are not allowed.", true);
                return;
            }
            else
            {

                //if (!UserBLL.CreateUser(CreateRegistrationObject(), false, this.AccountInfo.UserId, out newUserId))
                IRegisterUser regBLL = RegisterUserBLL.CreateRegistrationProviderObject(CreateRegistrationObject());
                regBLL.ValidateData();
                if (regBLL.IsValid)
                {
                    if (regBLL.Save())
                    {
                        ShowSuccess();
                        ClearForm();
                    }
                    else
                    {
                        DisplayMessage("Unable to add user. Please contact support if the issue persists. Error: " + regBLL.ErrorMessage, true);
                        return;
                    }
                }
                else
                    DisplayMessage("Validation error occured while adding new User. Error: " + regBLL.ErrorMessage, true);

            }

        }


        private bool IsRoleSelectedWhereAdminIsApprover
        {
            get
            {
                bool FilterByApproverRole = false;
                if (_selectedRole.scope.IsEqual(this.AccountInfo.Scope) && _selectedRole.IsAdmin)
                    FilterByApproverRole = true;

                return FilterByApproverRole;
            }
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
            regObj.ClearPassword = Password.Text.Trim(); //sammit

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
                    regObj.UserRegionalAccessProfile.IsApproverDesignate = IsApproverDesignateRequested;
                    regObj.UserRegionalAccessProfile.IsSuperDataEditor = IsSuperEditorRequested;
                    break;
                case Scope.Agency:
                    regObj.UserRegionalAccessProfile.RegionId = GetAgencyIdForNewUser();
                    regObj.UserRegionalAccessProfile.IsApproverDesignate = IsApproverDesignateRequested;
                    regObj.UserRegionalAccessProfile.IsSuperDataEditor = IsSuperEditorRequested;
                    break;
                case Scope.State:
                    regObj.IsShipDirector = chBoxIsShipDirector.Checked;
                    regObj.IsApproverDesignate = IsApproverDesignateRequested;
                    regObj.IsStateSuperEditor = IsStateLevelRole;
                    break;
                case Scope.CMS:
                    regObj.IsApproverDesignate = IsApproverDesignateRequested;
                    break;
            }

            //Populate User Descriptors for the Regions other than Agencies
            PopulateUserDescriptors(ref regObj);

            regObj.IsRegistrationRequest = false;
            regObj.RegisteredByUserId = this.AccountInfo.UserId;
            return regObj;

        }

        private bool IsApproverDesignateRequested
        {
            get
            {
                return (cbIsApprover.Enabled && cbIsApprover.Checked);
            }
        }

        private bool IsSuperEditorRequested
        {
            get
            {
                return (cbIsSuperEditor.Enabled && cbIsSuperEditor.Checked);
            }
        }
        protected bool IsStateLevelRole
        {
            get
            {
                if (_selectedRole != null)
                {
                    return _selectedRole.scope.IsLowerOrEqualTo(Scope.State);
                }

                return false;
            }
        }


        private string GetStateFIPSForNewUser()
        {
            string StateFIPS = string.Empty;

            //Users at State level and below 
            if (_selectedRole.scope.IsLowerOrEqualTo(Scope.State))
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
            else
                StateFIPS = State.GetStateFIPSForCMS();

            return StateFIPS;
        }

        private int GetAgencyIdForNewUser()
        {
            string sID = string.Empty;

            if (ddlAgency.Items.Count == 0)
            {
                //if (ddlAgency.SelectedValue.Trim() == string.Empty)
                //    sID = UserAgencyBLL.GetUserAgencyProfiles(this.AccountInfo.UserId, true).First().RegionId.ToString();

                throw new ShiptalkException("A valid Agency must be selected to create a new Agency Level User", false, new ArgumentNullException("AgencyId"));
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
        //private void PopulateNonAgencyUserDescriptors(ref ShiptalkLogic.BusinessObjects.UserRegistration regObj)
        //{
        //    PopulateAgencyUserDescriptors(ref regObj, ShiptalkLogic.Constants.Defaults.DefaultValues.AgencyIdForNonAgencyUsers);
        //}

        /// <summary>
        /// Populate descriptors for Agency Users whose role is tied to an agency.
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="AgencyId"></param>
        private void PopulateUserDescriptors(ref ShiptalkLogic.BusinessObjects.UserRegistration regObj)
        {
            if (dvCblDescriptors.Visible)
            {
                if (cblDescriptors.Items != null && cblDescriptors.Items.Count > 0)
                {
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
            dvCblDescriptors.Visible = false;
            plhIsShipDirector.Visible = false;
        }

        private void InferRoleSelection()
        {

            Role CMSRole = LookupBLL.GetRole(Scope.CMS, true);
            if (Int16.Parse(ddlRoles.SelectedValue) == CMSRole.Id)
                _selectedRole = CMSRole;
            else
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
