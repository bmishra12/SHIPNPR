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
    public partial class PendingUniqueIdUserView : System.Web.UI.Page, IRouteDataPage
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




        private UserUniqueID _UserUniqueID = null;
        protected UserUniqueID UniqueIDData
        {
            get
            {
                if (_UserUniqueID == null)
                {
                    _UserUniqueID = UserBLL.GetUniqueIDForUser(RegisteredUserId);
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
        #endregion

        #region Private Constants
        private const string RegisteredUserIdKey = "Id";
        protected enum PostBackCommands
        {
            [Description("APPROVE")]
            APPROVE = 1,
            [Description("DENY")]
            DENY = 2,
            [Description("REVOKE")]
            REVOKE = 3,
            [Description("INVOKE")]
            INVOKE = 4
        }
        //private const string APPROVE_CMD = "approve";
        //private const string Deny_CMD = "deny";
        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsAuthorized)
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
                case PostBackCommands.REVOKE:
                    Action_RevokeUser();
                    break;
                case PostBackCommands.INVOKE:
                    Action_InvokeUser();
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

                CheckBoxList DescriptorsObj = (CheckBoxList)formView.FindControl("Descriptors");
                if (DescriptorsObj != null) DescriptorsObj.Enabled = false;
        }

       
        #endregion







        #region Call BLL here


        //this UserId is passed is the RegisteredUserId/requester UserId
        private static bool SendApprovedEmail(int UserId, string uniqueId)
        {
            UserViewData userView = UserBLL.GetUser(UserId);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendFormat("Dear {0},", userView.FullName);
            sb.AddNewHtmlLines(2);
            sb.Append("Your request for a CMS SHIP Unique ID has been approved.");
            sb.AddNewHtmlLines(2);

            //sammit: new business rule do not show the unique ID in the e-mail
            //sb.Append("Your CMS SHIP Unique ID is: " + uniqueId);
            //sb.AddNewHtmlLines(2);

            sb.Append("You may login anytime to the SHIPtalk website and find 'CMS SHIP Unique ID' under 'My Profile'.");
            sb.AddNewHtmlLines(2);
            
            sb.Append("Note: The Unique ID will not be recognized by Customer Services Representatives until the 1st week of the following month.");
            sb.AddNewHtmlLines(2);
                 
            sb.Append("Thank you,");
            sb.AddNewHtmlLine();
        
            sb.Append("SHIP NPR Help Desk");
            sb.AddNewHtmlLine();

            sb.Append("<a href='https://shipnpr.shiptalk.org'>https://shipnpr.shiptalk.org</a>");
            sb.AddNewHtmlLine();
            sb.Append(ConfigUtil.ShiptalkSupportPhone);
            sb.AddNewHtmlLines(5);


            ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
            mailMessage.ToList.Add(userView.PrimaryEmail);
            mailMessage.Subject = "Your SHIPtalk.org request for CMS SHIP Unique ID";

            mailMessage.Body = sb.ToString();
            ShiptalkMail mail = new ShiptalkMail(mailMessage);


            try
            {
                mail.SendMail();
                return true;
            }
            catch { }

            return false;
        }



        private static bool SendDisapproveEmail(int RegisteredUserId)
        {


            UserViewData userView = UserBLL.GetUser(RegisteredUserId);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendFormat("Dear {0},", userView.FullName);
            sb.AddNewHtmlLines(2);
            sb.Append("Your request for a CMS SHIP Unique ID has been denied by the administrator.");
            sb.AddNewHtmlLines(2);

            sb.Append("Thank you,");
            sb.AddNewHtmlLine();

            sb.Append("SHIP NPR Help Desk");
            sb.AddNewHtmlLine();

            sb.Append("<a href='https://shipnpr.shiptalk.org'>https://shipnpr.shiptalk.org</a>");
            sb.AddNewHtmlLine();
            sb.Append(ConfigUtil.ShiptalkSupportPhone);
            sb.AddNewHtmlLines(5);


            ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
            mailMessage.ToList.Add(userView.PrimaryEmail);
            mailMessage.Subject = "Your SHIPtalk.org request for CMS SHIP Unique ID is denied.";

            mailMessage.Body = sb.ToString();
            ShiptalkMail mail = new ShiptalkMail(mailMessage);


            try
            {
                mail.SendMail();
                return true;
            }
            catch { }

            return false;
        }


        private static bool SendRevokeEmail(int RegisteredUserId)
        {


            UserViewData userView = UserBLL.GetUser(RegisteredUserId);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendFormat("Dear {0},", userView.FullName);
            sb.AddNewHtmlLines(2);
            sb.Append("Your CMS SHIP Unique ID has been revoked by the State SHIP Director or by the Administrator.");
            sb.AddNewHtmlLines(2);

            sb.Append("If you feel that your CMS SHIP Unique ID account has been deleted in error, please contact your State SHIP Director.");
            sb.AddNewHtmlLines(2);
            
            sb.Append("Thank you,");
            sb.AddNewHtmlLine();

            sb.Append("SHIP NPR Help Desk");
            sb.AddNewHtmlLine();

            sb.Append("<a href='https://shipnpr.shiptalk.org'>https://shipnpr.shiptalk.org</a>");
            sb.AddNewHtmlLine();
            sb.Append(ConfigUtil.ShiptalkSupportPhone);
            sb.AddNewHtmlLines(5);


            ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
            mailMessage.ToList.Add(userView.PrimaryEmail);
            mailMessage.Subject = "Your SHIPtalk.org request for CMS SHIP Unique ID is revoked.";

            mailMessage.Body = sb.ToString();
            ShiptalkMail mail = new ShiptalkMail(mailMessage);


            try
            {
                mail.SendMail();
                return true;
            }
            catch { }

            return false;
        }

        private static bool SendInvokeEmail(int RegisteredUserId)
        {


            UserViewData userView = UserBLL.GetUser(RegisteredUserId);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendFormat("Dear {0},", userView.FullName);
            sb.AddNewHtmlLines(2);
            sb.Append("Your CMS SHIP Unique ID has been reinstated by the State SHIP Director or by the Administrator.");
            sb.AddNewHtmlLines(2);

            sb.Append("You may login anytime to the SHIPtalk website and find 'CMS SHIP Unique ID' under 'My Profile'.");
            sb.AddNewHtmlLines(2);
            sb.Append("Thank you,");
            sb.AddNewHtmlLine();

            sb.Append("SHIP NPR Help Desk");
            sb.AddNewHtmlLine();

            sb.Append("<a href='https://shipnpr.shiptalk.org'>https://shipnpr.shiptalk.org</a>");
            sb.AddNewHtmlLine();
            sb.Append(ConfigUtil.ShiptalkSupportPhone);
            sb.AddNewHtmlLines(5);


            ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
            mailMessage.ToList.Add(userView.PrimaryEmail);
            mailMessage.Subject = "Your SHIPtalk.org request for CMS SHIP Unique ID is reinstated.";

            mailMessage.Body = sb.ToString();
            ShiptalkMail mail = new ShiptalkMail(mailMessage);


            try
            {
                mail.SendMail();
                return true;
            }
            catch { }

            return false;
        }
        private void Action_ApproveUser()
        {
            string ErrorMessage;

            //have a check here whether the uniquID is approved by somebody else..
            //false means uniqueId is not yet generated so go ahead in the rest of process.
            if (UserBLL.DoesMedicareUniqueIdExist(RegisteredUserId) == false)
            {
                //get the unique id...
                string uniqueId = UserBLL.GenerateMedicareUniqueIDForUser(RegisteredUserId, UserId);

                if (uniqueId.Length > 0)
                {
                    //then send the email to the RegisteredUserId...

                    if (SendApprovedEmail(RegisteredUserId, uniqueId))
                    {
                        DisplayMessage("The CMS SHIP Unique ID Approval was successful. An approval notification email has been sent to the User.", false);
                    }
                    else
                    {
                        ErrorMessage = "Attempt to send email notification to the User failed. Please try again, later.";
                        DisplayMessage("The approval process failed with the following error message:" + "<br />" + ErrorMessage, true);
                    }
                }
                else
                {
                    ErrorMessage = "Attempt to create CMS SHIP Unique ID failed. Please try again, later.";
                    DisplayMessage("The approval process failed with the following error message:" + "<br />" + ErrorMessage, true);

                }

            }
            else
            {
                ErrorMessage = "Attempt to create CMS SHIP Unique ID failed. The user has already have a CMS SHIP Unique ID.";
                DisplayMessage("The approval process failed with the following error message:" + "<br />" + ErrorMessage, true);

            }

        }


        private void Action_DenyUser()
        {
            string ErrorMessage;

             //have a check here whether the uniquID is approved by somebody else..
            //false means uniqueId is not yet generated so go ahead in the rest of process.
            if (UserBLL.DoesMedicareUniqueIdExist(RegisteredUserId) == false)
            {


                //delete the request.. from the database
                bool IsSuccess = UserBLL.DeleteUserUniqueIDRequest(RegisteredUserId);

                if (IsSuccess)
                {


                    if (SendDisapproveEmail(RegisteredUserId))
                        DisplayMessage("You have denied a CMS SHIP Unique ID. Please note that the CMS SHIP Unique ID request has been deleted. An email confirmation has been sent to the User about your decision.", false);
                    else
                    {
                        ErrorMessage = "Attempt to send email notification to the User failed. Please try again, later.";
                        DisplayMessage("An error occured while deleting User registration information. Error info:" + ErrorMessage, true);
                    }
                }
                else
                {
                    ErrorMessage = "Attempt to delete CMS SHIP Unique ID record failed. Please try again, later.";
                    DisplayMessage("The deny process failed with the following error message:" + "<br />" + ErrorMessage, true);

                }
            }
            else
            {
                ErrorMessage = "Attempt to delete CMS SHIP Unique ID record failed. The user has already have a CMS SHIP Unique ID.";
                DisplayMessage("The deny process failed with the following error message:" + "<br />" + ErrorMessage, true);

            }


        }

        private void Action_RevokeUser()
        {
            string ErrorMessage;

            //delete the request.. from the database
            bool IsSuccess = UserBLL.RevokeMedicareUniqueIdForUser(RegisteredUserId);

            if (IsSuccess)
            {


                if (SendRevokeEmail(RegisteredUserId))
                    DisplayMessage("You have revoked a CMS SHIP Unique ID. Please note that the CMS SHIP Unique ID request has been revoked. An email confirmation has been sent to the User about your decision.", false);
                else
                {
                    ErrorMessage = "Attempt to send email notification to the User failed. Please try again, later.";
                    DisplayMessage("An error occured while revoking User information. Error info:" + ErrorMessage, true);
                }
            }
            else
            {
                ErrorMessage = "Attempt to revoke CMS SHIP Unique ID record failed. Please try again, later.";
                DisplayMessage("The revoke process failed with the following error message:" + "<br />" + ErrorMessage, true);

            }



        }

        private void Action_InvokeUser()
        {
            string ErrorMessage;

            //delete the request.. from the database
            bool IsSuccess = UserBLL.InvokeMedicareUniqueIdForUser(RegisteredUserId);

            if (IsSuccess)
            {


                if (SendInvokeEmail(RegisteredUserId))
                    DisplayMessage("You have reinstated a CMS SHIP Unique ID. Please note that the CMS SHIP Unique ID request has been reinstated. An email confirmation has been sent to the User about your decision.", false);
                else
                {
                    ErrorMessage = "Attempt to send email notification to the User failed. Please try again, later.";
                    DisplayMessage("An error occured while revoking User information. Error info:" + ErrorMessage, true);
                }
            }
            else
            {
                ErrorMessage = "Attempt to reinstate CMS SHIP Unique ID record failed. Please try again, later.";
                DisplayMessage("The reinstated process failed with the following error message:" + "<br />" + ErrorMessage, true);

            }



        }

        private void Action_ResendEmail()
        {
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
        }

        protected override object SaveViewState()
        {
            ViewState[VIEWSTATE_KEY_REG_USERID] = RegisteredUserId;
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

                return (ApproverRulesBLL.IsApproverAtCMS(this.AccountInfo)
                    || this.AccountInfo.IsShipDirector);

            }
        }
        #endregion

      
       

    }
}
