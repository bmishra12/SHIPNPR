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



//using RegistrationObject = ShiptalkLogic.BusinessObjects.UserRegistration;

namespace ShiptalkWeb
{
    public partial class PendingEmailChangeUserView : System.Web.UI.Page, IRouteDataPage
    {

                
        #region Private properties
        private int UserId { get { return this.AccountInfo.UserId; } }
        public UserViewData userProfileViewData { get; set; }
             
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
        private const string EDIT_CMD = "edit";
        private const string RESENDEMAIL_CMD = "resendemail";
             
        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {
            //Authorization will also use the Fetched RegistrationData         

            if (!IsAuthorized())
                throw new ShiptalkException("You are not authorized to View the User.", false, "You are not authorized to view this page.");

            InitializeView();            
        }

        private void InitializeView()
        {
            if (!IsPostBack)
            {
                //If Email Token is still present in database, it means, the Uses not yet verified the token. Then need to resend the emal.                
                dataSourceUserView.DataSource = ViewData;               

                if (UserBLL.GetEmailVerificationTokenForUser(RegisteredUserId) != Guid.Empty)                    

                Page.DataBind();     
               
            }
        }




        #region Page wired events
        protected void dataSourceUserView_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
        {
        }
        protected void PostBackUserCommand(object sender, CommandEventArgs e)
        {

            switch (e.CommandArgument.ToString().ToLower().Trim())
            {
                case EDIT_CMD:
                    RouteController.RouteTo(RouteController.PendingEmailChangeEdit(RegisteredUserId), true);
                    break;
                case RESENDEMAIL_CMD:
                    Action_ResendEmail();
                    break;
                default:
                    throw new ShiptalkException("Unknown command sent during postback in Approval page.", false);
            }

            //try
            //{
            //    Action_ResendEmail();
            //}
            //catch
            //{
            //    throw new ShiptalkException("Unknown command sent during postback on this page.", false);
            //}
          
        }

        protected bool IsMultiAgencyAndDefaultAgencyExist
        {
            get
            {
                if (ViewData.Scope.IsEqual(Scope.Agency))
                {
                    if (ViewData.IsMultiAgencyUser)
                    {
                        return (ViewData.DefaultAgencyIdOfUser > 0);
                    }
                    else
                        return true;

                }
                else
                    return false;
            }
        }

        protected bool IsMultiSubStateAndDefaultSubStateExist
        {
            get
            {
                if (ViewData.Scope.IsEqual(Scope.SubStateRegion))
                {
                    if (ViewData.IsMultiSubStateUser)
                    {
                        return (ViewData.DefaultSubStateRegionIdOfUser > 0);
                    }
                    else
                        return true;
                }
                else
                    return false;
            }
        }
       

        #endregion



        #region Call BLL here
       
        private void Action_ResendEmail()
        {
            string ErrorMessage;
            Label TempPrimaryEmail = (Label)formView.FindControl("TempPrimaryEmail");

            //When the Email verification for Change email is resent, need to reset EmailChangeRequestDate to getdate()
            if (UserBLL.ResetEmailChangeRequestDate(ViewData.UserId, TempPrimaryEmail.Text))
            {
                bool mailSent = VerifyEmail.SendEmailVerificationNotificationforEmailChange(false, ViewData.UserId, ViewData.CreatedBy, TempPrimaryEmail.Text, out ErrorMessage);
                if (mailSent)
                    DisplayMessage("The email has been sent successfully. <br />", false);
                else
                    DisplayMessage("The email attempt failed with the following error message:" + "<br />" + ErrorMessage, true);
            }
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
