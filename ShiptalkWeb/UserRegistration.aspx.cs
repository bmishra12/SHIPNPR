using System;
using System.Collections;
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
using System.Security.Principal;
using System.Collections.Generic;

using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessLayer;
using ShiptalkCommon;

using System.Text;
using System.IO;

using RegistrationObject = ShiptalkLogic.BusinessObjects.UserRegistration;
using System.Text.RegularExpressions;

namespace ShiptalkWeb
{

    public partial class UserRegistration : System.Web.UI.Page
    {


        private enum DropDowns
        {
            CMSRegions = 1,
            States = 2,
            SubStateRegions = 3,
            Agencies = 4
        }

        private Role _selectedRole = null;
        private bool IsRoleBlankPostBack = false;
        private int? OldShipUserId = null;

        private string SelectedCheckboxListValues = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Display the States only if the Scope of the User who has 
            //logged in is a CMS Admin.'

            if (!IsPostBack)
            {
                //PopulateOldShipInfo();  //**NeW
                ResetDropdownViews();
                PopulateRoles();
            }
            else
            {
                //When the Login form is submitted, this page gets called too (Login in UserRegistration.aspx).
                //On successful registration, we hide the dv3colFormContent. However, we need to ensure that,
                //it is visible when Login page creates an error. The page otherwise looks ugly. The following
                //fixes that - ensures the dv3colFormContent which was turned off is now being turned back on.
                if (dv3colFormContent.Visible != true)
                    dv3colFormContent.Visible = !dv3colFormContent.Visible;

                if (dvCblDescriptors.Visible)
                {
                    foreach (ListItem li in cblDescriptors.Items)
                    {
                        if (li.Selected)
                        {
                            if (SelectedCheckboxListValues == string.Empty)
                                SelectedCheckboxListValues = li.Value;
                            else
                                SelectedCheckboxListValues = "|" + li.Value;
                        }
                    }
                }

            }
            AddLoadingClientScriptForRoles();
            //EmailFmtValidate.DataBind();
            //SecondaryEmailFmtValidate.DataBind();
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (SelectedCheckboxListValues != string.Empty)
            {
                string[] selectedItems = SelectedCheckboxListValues.Split('|');
                if (selectedItems.Contains("7"))
                {
                    foreach (ListItem li in cblDescriptors.Items)
                    {
                        if (li.Value != "7")
                        {
                            li.Selected = false;
                            //li.Enabled = false;
                        }
                    }
                }
                else if (selectedItems.Contains("6"))
                {
                    foreach (ListItem li in cblDescriptors.Items)
                    {
                        if (li.Value != "6" && li.Value != "7")
                        {
                            li.Selected = false;
                            //li.Enabled = false;
                        }
                    }
                }
                //else
                //{
                //    bool UncheckReadOnlyItems = false;
                //    foreach (string li in selectedItems)
                //    {
                //        int val;
                //        if (int.TryParse(li, out val))
                //        {
                //            if (val >= 1 && val <= 6)
                //            {
                //                UncheckReadOnlyItems = true;
                //                break;
                //            }
                //        }
                //    }
                //    if (UncheckReadOnlyItems)
                //    {
                //        IEnumerable<ListItem> listItems = (from ListItem li in cblDescriptors.Items where li.Value == "7" | li.Value == "6" select li);
                //        foreach (ListItem item in listItems)
                //        {
                //            item.Selected = false;
                //            item.Enabled = false;
                //        }
                //    }
                //}
            }
        }

        #region "Postback event handlers"

        /// <summary>
        /// The first drop down to be selected by the User.
        /// The remaining controls in the page are based off the Scope selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                IsRoleBlankPostBack = true;
                return;
            }
            else
                InferRoleSelection();

            //Keep this along with similar code in ddlStates_SelectedIndexChanged for better user experience.
            if (_selectedRole.Compare(Scope.State, ComparisonCriteria.IsLower))
                SetStatesAutoPostBackTrue();
            else
                SetStatesAutoPostBackFalse();

            //Display UI objects for selected role.
            SetupUIForSelectedRole();
        }



        public void ddlStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsRoleBlankPostBack)
                return;

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
                    SetStatesAutoPostBackFalse();
                else
                {
                    SetStatesAutoPostBackTrue();
                    //Show Sub State Regions if Role is for Sub State Regional User
                    //Show Agencies if Role is Agencies
                    if (_selectedRole.Compare(Scope.SubStateRegion, ComparisonCriteria.IsEqual))
                        ShowDropdowns(DropDowns.SubStateRegions);
                    else if (_selectedRole.Compare(Scope.Agency, ComparisonCriteria.IsEqual))
                        ShowDropdowns(DropDowns.Agencies);
                }
            }

        }


        public void OnShipDirectorCheckboxAction(object sender, EventArgs e)
        {
            dvCblDescriptors.Visible = !chBoxIsShipDirector.Checked;
            if (!dvCblDescriptors.Visible)
                cblDescriptors.ClearSelection();
        }


        protected void RegisterUserCommand(object sender, EventArgs e)
        {
            //if(valid)
            Page.Validate("RegisterForm");

            bool FormIsValid = IsFormValid();

            //For some reason, CaptchaText EnableViewState=false isn't working. So doing this...
            CaptchaText.Text = string.Empty;

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

            string UserName = Email.Text.Trim();

            if (RegisterUserBLL.DoesUserNameExist(UserName))
            {
                DisplayMessage("The Primary Email address is already registered. Duplicates are not allowed.", true);
                return;
            }
            else
            {

                //If Role selected is SHIP Admin, Check if a Ship Director already exists for the Chosen state
                if (_selectedRole.IsStateAdmin && chBoxIsShipDirector.Checked)
                {
                    if (LookupBLL.GetShipDirectorForState(ddlStates.SelectedValue.Trim()).HasValue)
                    {
                        DisplayMessage(string.Format("A SHIP Director already exists for state of {0}", State.GetStateName(StateFIPSSelected)), true);
                        return;
                    }
                }

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
                regObj.ClearPassword = Password.Text.Trim(); //sammit
                regObj.Password = Password.Text.Trim();
                regObj.RoleRequested = _selectedRole;

                regObj.OldShipUserId = OldShipUserId;
                regObj.IsRegistrationRequest = true;

                //GetStateFIPS (including CMS User)
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
                        regObj.UserRegionalAccessProfile.RegionId = int.Parse(ddlAgency.SelectedValue.Trim());
                        break;
                    case Scope.State:
                        regObj.IsShipDirector = chBoxIsShipDirector.Checked;
                        break;
                }

                //Populate User Descriptors for the Regions other than Agencies
                PopulateUserDescriptors(ref regObj);


                //Register
                IRegisterUser regBLL = RegisterUserBLL.CreateRegistrationProviderObject(regObj);
                regBLL.ValidateData();
                if (regBLL.IsValid)
                {
                    if (!regBLL.Save())
                        DisplayMessage("Unable to complete registration. " + regBLL.ErrorMessage, true);
                    else
                    {
                        ShowSuccess();
                        ClearForm();
                    }
                }
                else
                    DisplayMessage("Error. Validation error occured during registration. " + regBLL.ErrorMessage, true);
            }
        }

        //Similar to the one in Add User.
        private string GetStateFIPSForNewUser()
        {
            string StateFIPS = string.Empty;

            //Users at State level and below 
            if (_selectedRole.scope.IsLowerOrEqualTo(Scope.State))
                StateFIPS = ddlStates.SelectedValue.Trim();
            else
                StateFIPS = State.GetStateFIPSForCMS();

            return StateFIPS;
        }


        private string StateFIPSSelected
        {
            get
            {
                return ddlStates.SelectedValue;
            }
        }

        /// <summary>
        /// Populate descriptors for State Users, Sub State Users etc who are not directly related to an Agency
        /// </summary>
        /// <param name="regObj"></param>
        //private void PopulateNonAgencyUserDescriptors(ref ShiptalkLogic.BusinessObjects.UserRegistration regObj)
        //{
        //    if(regObj.RoleRequested.scope.IsEqual(Scope.SubStateRegion))
        //        PopulateAgencyUserDescriptors(ref regObj);
        //    else
        //        PopulateAgencyUserDescriptors(ref regObj);
        //}

        /// <summary>
        /// Populate descriptors for Agency Users whose role is tied to an agency.
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="AgencyId"></param>
        private void PopulateUserDescriptors(ref ShiptalkLogic.BusinessObjects.UserRegistration regObj)
        {
            //UserDescriptor descr;
            if (cblDescriptors.Items != null && cblDescriptors.Items.Count > 0)
            {
                for (int i = 0; i < cblDescriptors.Items.Count; i++)
                {
                    if (cblDescriptors.Items[i].Selected)
                    {
                        regObj.UserRegionalAccessProfile.DescriptorIDList.Add(int.Parse(cblDescriptors.Items[i].Value));
                    }

                }
            }
        }

        #endregion


        private void ShowSuccess()
        {
            colTitle.Text = "Success!";
            dv3colFormContent.Visible = false;
            dv3colDefaultDescription.Visible = false;
            dv3colSuccessMessage.Visible = !dv3colDefaultDescription.Visible;
           // OldShipUserLink.Visible = false;
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

        #region Populate Old Ship Info
        private void PopulateOldShipInfo()
        {
            OldShipUserInfo oldInfo = null;
            if (Session != null)
            {
                if (Session["OldShipUserInfo"] != null)
                {
                    oldInfo = Session["OldShipUserInfo"] as OldShipUserInfo;
                    OldShipUserId = oldInfo.UserId;
                    //The variable will be removed later from Session on postback.
                }
            }
            if (oldInfo == null)
                DisplayOldShipUserLoginLink();
            else
            {
                PopulateOldShipData(oldInfo);
                DisplayOldShipUserInfoPopulatedMessage();
            }
        }
        private void DisplayOldShipUserLoginLink()
        {
            //OldShipUserLoginPanel.Visible = true;
        }
        private void DisplayOldShipUserInfoPopulatedMessage()
        {
            OldShipInfoAvailableMesg.Visible = true;
        }
        private void PopulateOldShipData(OldShipUserInfo oldInfo)
        {
            OldShipUserId = oldInfo.UserId;
            FirstName.Text = oldInfo.FirstName.Trim();
            LastName.Text = oldInfo.LastName.Trim();
            Email.Text = oldInfo.Email.Trim();
            PrimaryPhone.Text = oldInfo.Phone.Trim();
        }
        #endregion


        #region "UI object processors"
        private void InferRoleSelection()
        {
            _selectedRole = LookupBLL.Roles.Find(p => p.Id == Int16.Parse(ddlRoles.SelectedValue));
            if (_selectedRole == null)
                ShiptalkException.ThrowSecurityException("Possible role list manipulation. The selected role was not found.", "Sorry. We're unable to serve your request. Please contact support for assistance");
            //DisplayMessage("Sorry. We're unable to serve your request. Please contact support for assistance.");

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
        #endregion


        #region "UI manipulation"

        /// <summary>
        /// Need to show or hide dropdowns or descriptors? 
        /// Do it here. The business logic that drives UI for selected roles is here.
        /// </summary>
        private void SetupUIForSelectedRole()
        {
            if (_selectedRole.Compare(Scope.State, ComparisonCriteria.IsLowerThanOrEqualTo))
            {
                ShowDropdowns(DropDowns.States);
                dvCblDescriptors.Visible = true;


                //For State Admins, 1) Show IsShipDirector Checkbox 
                //                  2) Hide Descriptors only if chBoxIsShipDirector is unchecked.
                if (_selectedRole.IsStateAdmin)
                {
                    plhIsShipDirector.Visible = true;
                }
            }
            else if (_selectedRole.Compare(Scope.CMSRegional, ComparisonCriteria.IsEqual))
                ShowDropdowns(DropDowns.CMSRegions);


            lblRoleDescription.Text = _selectedRole.Description;
            plhRoleDescription.Visible = true;
            //CMS Users - do not require any drop downs for selection

        }


        /// <summary>
        /// Route calls to methods that perform action for the selected drop down.
        /// </summary>
        /// <param name="dropdown"></param>
        private void ShowDropdowns(DropDowns dropdown)
        {

            switch (dropdown)
            {
                case DropDowns.CMSRegions:
                    HandleCMSRegionDisplayLogic();
                    break;
                case DropDowns.States:
                    HandleStateDisplayLogic();
                    break;
                case DropDowns.SubStateRegions:
                    HandleSubStateDisplayLogic(ddlStates.SelectedValue);
                    break;
                case DropDowns.Agencies:
                    HandleAgencyDisplayLogic(ddlStates.SelectedValue);
                    break;
            }
        }


        protected void ResetDropdownViews()
        {
            plhCMSRegion.Visible = false;
            divStates.Visible = false;
            plhSubStateRegion.Visible = false;
            plhAgencies.Visible = false;
            plhIsShipDirector.Visible = false;
            dvCblDescriptors.Visible = false;
            plhIsShipDirector.Visible = false;
            chBoxIsShipDirector.Checked = false;
            cblDescriptors.ClearSelection();
        }


        private void DisplayMessage(string message, bool IsError)
        {
            plhMessage.Visible = true;
            if (IsError)
                lblMessage.CssClass = "required";
            else
                lblMessage.CssClass = "info";
            lblMessage.Text = message;
        }


        private void PopulateRoles()
        {
            ddlRoles.DataSource = LookupBLL.Roles;
            ddlRoles.DataTextField = "Name";
            ddlRoles.DataValueField = "Id";
            ddlRoles.DataBind();
            ddlRoles.Items.Insert(0, new ListItem("Select a Role", "0"));
        }


        private void HandleCMSRegionDisplayLogic()
        {
            plhCMSRegion.Visible = true;
            ddlCMSRegion.DataSource = LookupBLL.GetCMSRegions();
            ddlCMSRegion.DataTextField = "Value";
            ddlCMSRegion.DataValueField = "Key";
            ddlCMSRegion.DataBind();
            ddlCMSRegion.Items.Insert(0, new ListItem("Select a CMS Region", "0"));
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
            ddlSubStateRegion.DataSource = LookupBLL.GetSubStateRegionsForState(StateFIPS);
            ddlSubStateRegion.DataTextField = "Value";
            ddlSubStateRegion.DataValueField = "Key";
            ddlSubStateRegion.DataBind();
            ddlSubStateRegion.Items.Insert(0, new ListItem("Select a Sub State Region", "0"));
        }


        private void HandleAgencyDisplayLogic(string StateFIPS)
        {
            plhAgencies.Visible = true;
            ddlAgency.DataSource = LookupBLL.GetAgenciesForState(StateFIPS);
            ddlAgency.DataTextField = "Value";
            ddlAgency.DataValueField = "Key";
            ddlAgency.DataBind();
            ddlAgency.Items.Insert(0, new ListItem("Select an Agency", "0"));
        }

        private void SetDescriptorsCheckboxList()
        {

        }

        
        #endregion


        #region "User input Validation"

        #endregion


        #region "View state events"
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            _selectedRole = (Role)ViewState["RoleSelected"];
            OldShipUserId = (int?)ViewState["OldShipUserId"];
        }

        protected override object SaveViewState()
        {
            ViewState["RoleSelected"] = _selectedRole;
            ViewState["OldShipUserId"] = OldShipUserId;
            return base.SaveViewState();
        }


        #endregion


        #region Client Script Injection
        private void AddLoadingClientScriptForRoles()
        {
            string Event_Name = "onchange";
            string Function_Name = "DoCustomStuff();";

            if (!Page.ClientScript.IsClientScriptBlockRegistered(typeof(UserRegistration), "PageScripts"))
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(UserRegistration), "PageScripts",
                          "function DoCustomStuff() { if (document.getElementById) { document.getElementById('LoadingImageDiv').style.display = 'block'; } " +
                          Page.ClientScript.GetPostBackEventReference(ddlRoles, string.Empty) + "; }", true);
            }
            if (ddlRoles.Attributes[Event_Name] != Function_Name)
            {
                ddlRoles.Attributes[Event_Name] = Function_Name;
            }
        }

        private void AddLoadingClientScriptForStates()
        {
            string Event_Name = "onchange";
            string Function_Name = "DoCustomStateStuff();";

            if (!Page.ClientScript.IsClientScriptBlockRegistered(typeof(UserRegistration), "PageStateScripts"))
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(UserRegistration), "PageStateScripts",
    "function DoCustomStateStuff() { if (document.getElementById) { document.getElementById('LoadingImageDiv').style.display = 'block'; } " +
    Page.ClientScript.GetPostBackEventReference(ddlStates, string.Empty) + "; }", true);
            }

            if (ddlStates.Attributes[Event_Name] != Function_Name)
            {
                ddlStates.Attributes[Event_Name] = Function_Name;
            }
        }

        private void SetStatesAutoPostBackFalse()
        {
            ddlStates.AutoPostBack = false;
            ddlStates.Attributes["onchange"] = string.Empty;
        }

        private void SetStatesAutoPostBackTrue()
        {
            ddlStates.AutoPostBack = true;
            AddLoadingClientScriptForStates();
        }

        #endregion


        #region Lanap Captcha stuff
        //protected void ctrlCaptcha_PreRender(object sender, EventArgs e)
        //{
        //    Lanap.BotDetect.Captcha captchaControl = (Lanap.BotDetect.Captcha)sender;
        //    StringBuilder sb = new StringBuilder();
        //    StringWriter tw = new StringWriter(sb);
        //    HtmlTextWriter hw = new HtmlTextWriter(tw);
        //    captchaControl.RenderControl(hw);
        //    tw.Flush();
        //    sb.Replace("this.blur();", "");
        //    captchaControl.Visible = false;

        //    string controlName = captchaControl.ID;

        //    Control c = TemplateControl.ParseControl(sb.ToString());
        //    this.Form.Controls.Remove(FindControl(controlName));
        //    this.Form.FindControl("Panel1").Controls.Add(c);

        //}


        private bool VerifyCaptchaText()
        {
            //To navigate to the ContentTemplate, use CreateUserWizard1.WizardSteps[0].Controls[0]
            Control ctrl = this.Master.FindControl("body1").FindControl("ctrlCaptcha");
            //I won't check for null. The page should work or the custom eror should catch it, so its fixed.
            //if (ctrl != null)
            Lanap.BotDetect.Captcha captchaControl = (Lanap.BotDetect.Captcha)ctrl;

            ctrl = this.Master.FindControl("body1").FindControl("CaptchaText");
            TextBox txtCaptchaText = (TextBox)ctrl;

            bool IsValid = captchaControl.Validate(txtCaptchaText.Text.Trim());
            //EnableViewState apparently doesnt work here. So, a fix to clear the previous captcha text.
            txtCaptchaText.Text = string.Empty;
            return IsValid;

        }


        #endregion

        #region "Commented code - Might come handy in future"
        //////string ctrlname = this.Page.Request.Params.Get("__EVENTTARGET");
        //////if (ctrlname != null && ctrlname != string.Empty)
        //////{
        //////    //For now, we don't need this.
        //////}
        //////else
        //////{
        //////        //foreach (string ctrl in this.Page.Request.Form)
        //////        //{
        //////        //    Control c = this.Page.FindControl(ctrl);
        //////        //    if (c is System.Web.UI.WebControls.Button)
        //////        //    {
        //////        //        Button btn = (Button)c;
        //////        //        if (btn.ID.Trim().ToLower() == "btnLogin".ToLower())
        //////        //            dv3colFormContent.Visible = true;
        //////        //    }
        //////        //}

        //////}
        #endregion
    }
}
