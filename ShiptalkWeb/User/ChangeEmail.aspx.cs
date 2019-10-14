using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Transactions;

using ShiptalkWeb.Routing;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;

using ShiptalkCommon;

namespace ShiptalkWeb
{
    //TODO: Include Routing Info
    public partial class ChangeEmail : System.Web.UI.Page, IRouteDataPage
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
                Page.DataBind();
            }
        }



        protected void ChangeEmailCommand(object sender, EventArgs e)
        {
            //TODO: Validation to be replaced with ProxyValidator
            Page.Validate("ChangeEmail");
            if (Page.IsValid)
            {
                //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                //{
                bool DoCommit = false;
                string ErrorMessage;
                string NewEmail = Email.Text.Trim();

                //Check if the Username already exists
                if (RegisterUserBLL.DoesUserNameExist(NewEmail))
                {
                    DisplayMessage("The Primary Email address is already registered. Duplicates are not allowed.", true);
                    return;
                }
                else
                {
                    if (UserBLL.ChangeEmail(ShiptalkPrincipal.UserId, ShiptalkPrincipal.Username, NewEmail, ShiptalkPrincipal.UserId, out ErrorMessage))
                    {
                        if (VerifyEmail.SendEmailVerificationNotificationforEmailChange(false, ShiptalkPrincipal.UserId, ShiptalkPrincipal.UserId, NewEmail, out ErrorMessage))
                            DoCommit = true;
                    }

                    if (DoCommit)
                    {
                        //scope.Complete();
                        DisplayMessage("Your request has been submitted. You will receive this Change Email request information email at your old Email address and  'Email verification' email at your new Email address shortly. Please follow the instruction to complete the Email verification process . If you do not receive an email after a while, please contact the help desk.", false);
                    }
                    else
                        DisplayMessage("Sorry. Unable to change your email. Please contact support for assistance.", false);
                    //}
                }
            }
        }

        private string UserName { get { return this.AccountInfo.PrimaryEmail; } }
        //private string EmailAddress { get { return UserName;} }


        //private bool SendEmailToUserAboutPasswordChange()
        //{
        //    string EmailAddress = UserName;

        //    ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
        //    mailMessage.ToList.Add(EmailAddress);
        //    mailMessage.Subject = "Changes to your account at shipnpr.shiptalk.org";

        //    mailMessage.Body = CreateEmailBodyForPasswordChangeConfirmation();
        //    ShiptalkMail mail = new ShiptalkMail(mailMessage);
        //    return mail.SendMail();

        //}

        //private string CreateEmailBodyForPasswordChangeConfirmation()
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    sb.Append("Hello,");
        //    sb.AddNewHtmlLines(2);
        //    sb.Append("This email is to confirm that your password at shipnpr.shiptalk.org has been changed successfully.");
        //    sb.AddNewHtmlLines(2);
        //    sb.Append("If you did not change the password, please contact SHIP NPR Help Desk immediately.");
        //    sb.AddNewHtmlLines(3);
        //    sb.Append("Thank you,");
        //    sb.AddNewHtmlLine();
        //    sb.Append("SHIP NPR Help Desk");
        //    sb.AddNewHtmlLine();
        //    sb.Append("<a href='https://shipnpr.shiptalk.org'>https://shipnpr.shiptalk.org</a>");
        //    sb.AddNewHtmlLine();
        //    sb.Append(ConfigUtil.ShiptalkSupportPhone);
        //    sb.AddNewHtmlLines(5);

        //    return sb.ToString();
        //}



        private void DisplayMessage(string ErrorMessage, bool IsError)
        {
            MessageArea.Visible = true;
            Message.Text = ErrorMessage;

            if (IsError == true)
                Message.ForeColor = System.Drawing.Color.Red;
            else
                Message.ForeColor = System.Drawing.Color.Blue;
        }

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

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
        #endregion
    }
}
