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
using System.ComponentModel;

using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkLogic.BusinessLayer;
using ShiptalkCommon;



using RegistrationObject = ShiptalkLogic.BusinessObjects.UserRegistration;

namespace ShiptalkWeb
{
    public partial class PendingRegistrationUserView : System.Web.UI.Page, IRouteDataPage
    {

        private const string VIEWSTATE_KEY_IS_RESEND_REQ = "is_resend_request";
        private const string VIEWSTATE_KEY_REG_USERID = "registered_userid";
        
        #region Private properties
        private int UserId { get { return this.AccountInfo.UserId; } }
        protected bool IsEmailResendRequest = false;
       
        private int _RegisteredUserId;
        private int RegisteredUserId
        {
            get
            {
                if (RouteData.Values[RegisteredUserIdKey] == null) throw new ShiptalkException("User View requested without UserId in the Route Data.", false, new ArgumentNullException("RouteData.UserId"));
                return int.Parse(RouteData.Values[RegisteredUserIdKey].ToString());
            }
            set
            {
                _RegisteredUserId = value;
            }
        }
        #endregion

        #region Private Constants
        private const string RegisteredUserIdKey = "Id";
        protected enum PostBackCommands
        {
            [Description("APPROVE")]
            APPROVE = 1,
            [Description("DENY")]
            DENY = 2,
            [Description("RESEND")]
            RESEND = 3
        }
        //private const string APPROVE_CMD = "approve";
        //private const string Deny_CMD = "deny";
        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {
            //Authorization will also use the Fetched RegistrationData
            //FetchRegistrationData();

            if (!IsAuthorized())
                throw new ShiptalkException("You are not authorized to View the User.", false, "You are not authorized to view this page.");

            InitializeView();
        }

        private void InitializeView()
        {
            if (!IsPostBack)
            {
                //If Email Token is still present in database, it means, the Uses not yet verified the token.
                //Such Users cannot be approved. Instead, send the 'Email Verification mail' to feature must be enabled.
                //This should use the appropriate RegisterUserImplBase's email verification mailer to send email.
                //  If USERBLL.IsAccountCreatedViaUserRegistration is true, 
                //          Use :   SendUserRegistrationVerificationEmail : CreatedBy is null && IsRegistrationRequest = 1
                //  Else 
                //          Use :   SendAddUserVerificationEmail : CreatedBy is not null && IsRegistrationRequest = 0
                dataSourceUserView.DataSource = ViewData;

                if (UserBLL.GetEmailVerificationTokenForUser(RegisteredUserId) != Guid.Empty)
                    IsEmailResendRequest = true;

                Page.DataBind();
                BindDescriptors();
                SetSelectedDescriptors();
            }
        }




        #region Page wired events
        protected void dataSourceUserView_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
        }
        protected void PostBackUserCommand(object sender, CommandEventArgs e)
        {
            PostBackCommands Cmd = (PostBackCommands)Enum.Parse(typeof(PostBackCommands), e.CommandName, true);
            switch (Cmd)
            {
                case PostBackCommands.APPROVE:
                    Action_ApproveUser();
                    break;
                case PostBackCommands.DENY:
                    Action_DenyUser();
                    break;
                case PostBackCommands.RESEND:
                    Action_ResendEmail();
                    break;
                default:
                    throw new ShiptalkException("Unknown command sent during postback in Approval page.", false);
            }
        }
        #endregion




        #region UI Processing Logic
        protected void BindDescriptors()
        {
            CheckBoxList DescriptorsObj = (CheckBoxList)formView.FindControl("Descriptors");
            IEnumerable<KeyValuePair<int, string>> DescriptorsForScope = LookupBLL.GetDescriptorsForScope(ViewData.Scope);
            if (ViewData.IsUserStateScope)
            {
                bool RemoveShipDirectorDescriptor = true;
                if (ViewData.IsAdmin)
                {
                    //If a Ship Director does not exist for the State, the Ship Director Descriptor can be shown
                    if (!LookupBLL.GetShipDirectorForState(ViewData.StateFIPS).HasValue)
                        RemoveShipDirectorDescriptor = false;
                }

                //Ship Director already exists for the State. Need not show it for State Level Admins
                if (RemoveShipDirectorDescriptor)
                    DescriptorsObj.DataSource = (from scope in DescriptorsForScope where scope.Key != Descriptor.ShipDirector.EnumValue<int>() select scope);
            }
            else
            {
                DescriptorsObj.DataSource = DescriptorsForScope;
            }

            DescriptorsObj.DataTextField = "Value";
            DescriptorsObj.DataValueField = "Key";
            DescriptorsObj.DataBind();

            foreach (ListItem item in DescriptorsObj.Items)
            {
                if (item.Attributes["CustomValue"] == null)
                    item.Attributes.Add("CustomValue", item.Value);
                else
                    item.Attributes["CustomValue"] = item.Value;

                if (item.Attributes["onClick"] == null)
                    item.Attributes.Add("onClick", string.Format("HandleDescriptorlist('{0}', {1})", DescriptorsObj.ClientID, item.Value));
                else
                    item.Attributes["onClick"] = string.Format("HandleDescriptorlist('{0}', {1})", DescriptorsObj.ClientID, item.Value);
            }

        }
        private List<int> GetDescriptorsSelectedByApprover()
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
        private void SetSelectedDescriptors()
        {
            CheckBoxList DescriptorsObj = (CheckBoxList)formView.FindControl("Descriptors");
            var SelectedList = ViewData.GetAllDescriptorsForUser;
            foreach (ListItem item in DescriptorsObj.Items)
            {
                foreach (KeyValuePair<int, string> pair in SelectedList)
                {
                    //Set Selected here
                    if (pair.Key == int.Parse(item.Value))
                        item.Selected = true;
                }
            }
        }
        protected void formView_PreRender(object sender, EventArgs e)
        {
            //Cannot update descriptors while resending verification email
            if (IsEmailResendRequest)
            {
                CheckBoxList DescriptorsObj = (CheckBoxList)formView.FindControl("Descriptors");
                if (DescriptorsObj != null) DescriptorsObj.Enabled = false;
           }
        }

       
        #endregion







        #region Call BLL here
        private void Action_ApproveUser()
        {
            string ErrorMessage;
            ViewData.DescriptorIds = GetDescriptorsSelectedByApprover();

            if (RegisterUserBLL.ApproveUser(ViewData, this.AccountInfo.UserId, out ErrorMessage))
                DisplayMessage("The User Approval was successful. An approval notification email has been sent to the User.", false);
            else
                DisplayMessage("The approval process failed with the following error message:" + "<br />" + ErrorMessage, true);

        }
        private void Action_DenyUser()
        {
            string ErrorMessage;
            if (RegisterUserBLL.DenyUser(ViewData, this.AccountInfo.UserId, out ErrorMessage))
                DisplayMessage("You have denied account for the registered User. Please note that the registration information has been deleted. An email confirmation has been sent to the User about your decision.", false);
            else
                DisplayMessage("An error occured while deleting User registration information. Error info:" + ErrorMessage, true);

        }
        private void Action_ResendEmail()
        {
            string ErrorMessage;
            bool mailSent = RegistrationEmails.SendEmailVerificationNotification(UserBLL.IsAccountCreatedViaUserRegistration(ViewData.PrimaryEmail),
                ViewData.UserId, ViewData.CreatedBy, out ErrorMessage);
            if(mailSent)
                DisplayMessage("The email has been sent successfully. <br />", false);
            else
                DisplayMessage("The email attempt failed with the following error message:" + "<br />" + ErrorMessage, true);
        }
        private void FetchRegistrationData()
        {
            
        }

        private UserViewData _viewData = null;
        protected UserViewData ViewData { 
            get {
                if (_viewData == null)
                    _viewData = UserBLL.GetUser(RegisteredUserId);

                    return _viewData;
                } 
            set {
                _viewData = value;
            } 
        }
        #endregion



        #region UI message display related
        private void DisplayMessage(string message, bool IsError)
        {
            if (!IsError)
            {
                lblTitle.Text = "Success!";
                ((Panel)formView.FindControl("RegistrationDataPanel")).Visible = false;
                dv3colMessage.Text = message;
            }
            else
            {
                ((PlaceHolder)formView.FindControl("plhMessage")).Visible = true;
                ((Label)formView.FindControl("lblMessage")).Text = message;
            }
        }
        #endregion

        #region "View state events"
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            RegisteredUserId = (int)ViewState[VIEWSTATE_KEY_REG_USERID];
            IsEmailResendRequest = (bool)ViewState[VIEWSTATE_KEY_IS_RESEND_REQ];
        }

        protected override object SaveViewState()
        {
            ViewState[VIEWSTATE_KEY_REG_USERID] = RegisteredUserId;
            ViewState[VIEWSTATE_KEY_IS_RESEND_REQ] = IsEmailResendRequest;
            return base.SaveViewState();
        }
        #endregion

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion

        #region Authorization
        private bool IsAuthorized()
        {
            /*  Approval Data Authorization Logic is written as follows:
             *      A: The Approver must be within the same State as the User whose profile is pending approval.
             *      B: The Approver must be an Admin ANDALSO an Approver Designate of Same Scope or Higher scope.
             *      C: If the account pending approval is for State Admin, then the Approver can be be a SHIP director or State Admin with Approver Designate rights.
             *      D: If the Approver is a CMS Level User, the Approver must be a CMS Admin with Approver Designate rights.
             *      E:  Introduced 03/07/2010 - User who is Admin with Descriptor ID 8 [Approver] can approve within A-D rule context.
             */

            //Lets Gather parameters here for verifying the business logic.
            string requestedState = ViewData.StateFIPS;
            Scope requestedScope = ViewData.Scope;
            bool IsCMSRegionAccountRequested = ViewData.IsUserCMSRegionalScope;
            bool IsCMSAccountRequested = ViewData.IsUserCMSScope;
            bool IsAdminAccountRequested = ViewData.IsAdmin;

            string ApproverState = this.AccountInfo.StateFIPS;
            Scope ApproverScope = this.AccountInfo.Scope;
            int ApproverUserId = AccountInfo.UserId;
            bool ApproverIsAdmin = this.AccountInfo.IsAdmin;
            bool IsApproverShipDirector = this.AccountInfo.IsShipDirector;
            bool IsApproverStateApprover = this.AccountInfo.IsStateAdmin && this.AccountInfo.IsApproverDesignate.HasValue
                                                && this.AccountInfo.IsApproverDesignate.Value;
            bool IsApproverCMSApprover = this.AccountInfo.IsAdmin && this.AccountInfo.IsCMSScope
                                            && this.AccountInfo.IsApproverDesignate.HasValue
                                            && this.AccountInfo.IsApproverDesignate.Value;


            //General Rule: Admins of lower scope cannot approve Admins of higher scope.
            if (ApproverScope.IsLower(requestedScope))
                return false;

            //Approve must always be Admin of some scope
            if(!ApproverRulesBLL.IsApprover(this.AccountInfo))
            //if (!ApproverIsAdmin)
                return false;

            //CMS Admins can be approved by only approver designates.
            if (IsApproverCMSApprover)
                return true;

            //CMS User/CMS Admin and CMS Regional requests can be approved only by a CMS Admin
            if (IsCMSRegionAccountRequested || IsCMSAccountRequested)
                return false;


            //Move on to State Level Users Approval Request
            //For Non-CMS Users: States must match
            if (requestedState != ApproverState)
                return false;
            else
            {
                //State Ship Directors can approve any one in the State.
                if (IsApproverShipDirector || IsApproverStateApprover)
                    return true;

                if (requestedScope.IsEqual(Scope.State))
                {
                    //State Users can be approved by only State Directors or State Approvers.
                    //State Director and State approver rights were already checked, so need to reject all State requests.
                    return false;
                }

                //For verifying Sub State access rights, lets get Sub State profiles of Approver
                IEnumerable<UserRegionalAccessProfile> approverSubStateAdminProfiles =
                            UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(ApproverUserId, true);
                if (requestedScope.IsEqual(Scope.SubStateRegion))
                {
                    //Approvers of Sub State Users and Admins can be the following:
                    // CMS Approver - which we already checked
                    // Ship Director & State Approvers - which we already checked.
                    //Now check for Sub State Approver of the Sub State ID requested.

                    //Approve must be Sub State Admin to approve a Sub State User
                    if (approverSubStateAdminProfiles != null && approverSubStateAdminProfiles.Count() > 0)
                    {
                        //Sub State User can be approved by Sub State Admin of Same Sub State Region
                        int SubStateRegionIDOfRequestor = ViewData.RegionalProfiles[0].RegionId;
                        foreach (UserRegionalAccessProfile approverSubState in approverSubStateAdminProfiles)
                        {
                            if (approverSubState.RegionId == SubStateRegionIDOfRequestor)
                            {
                                return approverSubState.IsApproverDesignate;
                            }
                        }
                    }
                    return false;
                }

                //Logic below is for Agency Scope account approval requests.
                //Agency Scope requests can be approved by Sub State Approvers as well as Agency Approvers.
                if (ApproverScope.IsEqual(Scope.SubStateRegion))
                {
                    if (approverSubStateAdminProfiles != null && approverSubStateAdminProfiles.Count() > 0)
                    {
                        //The Agency of the account requested must be part of Approver's Sub State region.
                        int AgencyOfAccountRequested = ViewData.RegionalProfiles[0].RegionId;
                        foreach (UserRegionalAccessProfile subStateprofile in approverSubStateAdminProfiles)
                        {
                            //Get Agencies for substate
                            IEnumerable<ShiptalkLogic.BusinessObjects.Agency> agencyProfiles = LookupBLL.GetAgenciesForSubStateRegion(subStateprofile.RegionId);
                            foreach (ShiptalkLogic.BusinessObjects.Agency agency in agencyProfiles)
                            {
                                if (agency.Id == AgencyOfAccountRequested)
                                {
                                    return subStateprofile.IsApproverDesignate;
                                }
                            }
                        }
                    }
                    return false;
                }
                else
                {
                    //Here, it is evident that Approver is an Agency Level person and also account Request is for agency scope.
                    //All Agency requests could be approved by Agency Approvers, SubState approvers or Ship Director or CMS approvers
                    int AgencyOfAccountRequested = ViewData.RegionalProfiles[0].RegionId;
                    IEnumerable<UserRegionalAccessProfile> approverAgencyAdminProfiles =
                        UserAgencyBLL.GetUserAgencyProfiles(ApproverUserId, true);

                    foreach (UserRegionalAccessProfile approverAgencyprofile in approverAgencyAdminProfiles)
                    {
                        if (approverAgencyprofile.RegionId == AgencyOfAccountRequested)
                        {
                            return approverAgencyprofile.IsApproverDesignate;
                        }
                    }
                    return false;
                }

            }

        }
        #endregion

      
       

    }
}
