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

using System.Text;

using ShiptalkLogic.BusinessLayer;
using ShiptalkCommon;
using ShiptalkLogic.BusinessObjects;

namespace ShiptalkWeb
{
    public partial class ResetPassword : System.Web.UI.Page
    {

        public string EmailAddress { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //EmailFmtValidate.DataBind();
        }

        protected void ResetPasswordCommand(object sender, EventArgs e)
        {
            //if(valid)
            Page.Validate("ResetPassword");

            if (!IsFormValid())
            {
                DisplayMessage("Please fix the error(s) displayed in red and submit again", true);
                return;
            }

            DisplayMessage("Thank You! Your request has been submitted. If the email address you entered is in our system you will receive an email shortly. If you do not receive an email after a while, please verify that you entered the registered email associated with your SHIP NPR account. Please check your Junk mail folder as well. If you have additional questions, please contact the SHIP NPR Help Desk at 1-800-253-7154, option 1.", false);

            EmailAddress = Email.Text.Trim();

            if (!RegisterUserBLL.DoesUserNameExist(EmailAddress))
            {
                DisplayMessage("Thank You! Your request has been submitted. If the email address you entered is in our system you will receive an email shortly. If you do not receive an email after a while, please verify that you entered the registered email associated with your SHIP NPR account. Please check your Junk mail folder as well. If you have additional questions, please contact the SHIP NPR Help Desk at 1-800-253-7154, option 1.", false);
                return;
            }
            else
            {

             //sammit: Check if the user exist and Is he trying to change the password in the sameday
                // if he is trying to do so Send an e-mail with the warning message
             int? UserId = UserBLL.GetUserIdForUserName(EmailAddress);
             if (UserId.HasValue)
             {
                 UserProfile userProf = UserBLL.GetUserProfile((int)UserId);
                 if (userProf.LastPasswordChangeDate != null && ((DateTime)userProf.LastPasswordChangeDate).Date == System.DateTime.Today)
                 {
                     //Send Email to User with warning.
                     if (SendPasswordCanNotBeChangedEmail())
                         DisplayMessage("Thank You! Your request has been submitted. If the email address you entered is in our system you will receive an email shortly. If you do not receive an email after a while, please verify that you entered the registered email associated with your SHIP NPR account. Please check your Junk mail folder as well. If you have additional questions, please contact the SHIP NPR Help Desk at 1-800-253-7154, option 1.", false);
                     else
                         DisplayMessage("Sorry. We were unable to send password reset instructions to your mail. Please contact help desk if the problem persists.", true);

                     return;
                 }

             }

                //Send Email to User for verification.
                if (SendPasswordResetEmail())
                    DisplayMessage("Thank You! Your request has been submitted. If the email address you entered is in our system you will receive an email shortly. If you do not receive an email after a while, please verify that you entered the registered email associated with your SHIP NPR account. Please check your Junk mail folder as well. If you have additional questions, please contact the SHIP NPR Help Desk at 1-800-253-7154, option 1.", false);
                else
                    DisplayMessage("Sorry. We were unable to send password reset instructions to your mail. Please contact help desk if the problem persists.", true);
            }
        }

        private void DisplayMessage(string ErrorMessage, bool IsError)
        {
           
            MessageArea.Visible = true;
            Message.Text = ErrorMessage;

            if (IsError)

                Message.CssClass = "required";
            else
            {
                Message.CssClass = "Notrequired";
                email1.Visible = false;
                test12.Visible = false;
               
                
            }
        }

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

        private bool SendPasswordResetEmail()
        {
            ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
            mailMessage.ToList.Add(EmailAddress);

            if (ConfigUtil.WebEnvironment != "prod")
            {
                mailMessage.Subject = "Your request at the shipnpr.shiptalk.org(" + ConfigUtil.WebEnvironment + ")";

            }
            else
            {
                mailMessage.Subject = "Your request at the shipnpr.shiptalk.org";
            }

            mailMessage.Body = CreateEmailBodyForResetPassword();
            ShiptalkMail mail = new ShiptalkMail(mailMessage);
            if (!mail.SendMail())
            {
                DisplayMessage("An error occured while sending password reset instructions by email. If the problem persists, please contact SHIP NPR Help Desk.", true);
                return false;
            }
            else
                return true;
        }

        private string CreateEmailBodyForResetPassword()
        {

            Guid VerificationToken = GetGuidForEmailReset();

            string sVerificationToken = string.Empty;
            if (VerificationToken == Guid.Empty)
                throw new ShiptalkException("Unable to obtain Token (Guid) for Email Reset process", true, "An error occured in the password reset process. Please contact support if the problem persists.");
            else
                sVerificationToken = VerificationToken.ToString();

            string linkFormat = "<a href='" + ConfigUtil.PasswordResetUrl + "?prt={0}'>Follow this link</a>";
            string ResetLink = string.Format(linkFormat, (EmailAddress + sVerificationToken));
            string textlink = ConfigUtil.PasswordResetUrl + "?prt={0}";
            textlink = string.Format(textlink, (EmailAddress + sVerificationToken));

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (ConfigUtil.WebEnvironment != "prod")
            {
                sb.AppendFormat("-----------TEST Reset Your Password from DEV site------------"); sb.AddNewHtmlLines(2);
                sb.AppendFormat("-----------Created from DEV environment: {0}------------", ConfigUtil.WebEnvironment); sb.AddNewHtmlLines(2); ;
            }
            sb.Append("Hello,");
            sb.AddNewHtmlLines(2);
            sb.Append("A request to reset your password was made at shipnpr.shiptalk.org.");
            sb.AddNewHtmlLine();
            sb.Append("If you did not request your password reset, please disregard this message.");
            sb.AddNewHtmlLines(2);


            sb.Append(ResetLink);//Follow this link ahref
            sb.Append(" to reset your password. If you have difficulties accessing the link, copy and paste the link below into your browser’s address bar to verify your email address.");
            sb.AddNewHtmlLines(2);
            sb.Append(textlink);
            sb.AddNewHtmlLines(3);
            sb.Append("Thank you,");
            sb.AddNewHtmlLine();
            sb.Append("SHIP NPR Help Desk");
            sb.AddNewHtmlLine();
            sb.Append("<a href='https://shipnpr.shiptalk.org'>https://shipnpr.shiptalk.org</a>");
            sb.AddNewHtmlLine();
            sb.Append(ConfigUtil.ShiptalkSupportPhone);
            sb.AddNewHtmlLines(5);

            return sb.ToString();
        }

        private bool SendPasswordCanNotBeChangedEmail()
        {
            ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
            mailMessage.ToList.Add(EmailAddress);
            mailMessage.Subject = "Your request at the shipnpr.shiptalk.org";

            mailMessage.Body = CreateEmailBodyForPasswordCanNotBeChanged();
            ShiptalkMail mail = new ShiptalkMail(mailMessage);
            if (!mail.SendMail())
            {
                DisplayMessage("An error occured while sending password reset instructions by email. If the problem persists, please contact SHIP NPR Help Desk.", true);
                return false;
            }
            else
                return true;
        }

        private string CreateEmailBodyForPasswordCanNotBeChanged()
        {


            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("You already changed your password. You are allowed to change your password once in a day");
            sb.AddNewHtmlLines(2);
            sb.Append("Contact help desk to unlock, verify your identity, and change password.");

            sb.AddNewHtmlLines(3);
            sb.Append("Thank you,");
            sb.AddNewHtmlLine();
            sb.Append("SHIPNPR Help Desk");
            sb.AddNewHtmlLine();
            sb.Append("<a href='https://shipnpr.shiptalk.org'>https://shipnpr.shiptalk.org</a>");
            sb.AddNewHtmlLine();
            sb.Append(ConfigUtil.ShiptalkSupportPhone);
            sb.AddNewHtmlLines(5);

            return sb.ToString();
        }


        private Guid GetGuidForEmailReset()
        {
            return UserBLL.GetPasswordResetVerificationTokenForUser(EmailAddress);
        }

        

    }
}
