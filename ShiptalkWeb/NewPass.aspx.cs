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
using System.Transactions;

using ShiptalkLogic.BusinessLayer;
using ShiptalkCommon;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using ShiptalkLogic.BusinessObjects;

namespace ShiptalkWeb
{
    public partial class NewPass : System.Web.UI.Page
    {
        string PasswordResetValueString = string.Empty;
        int GuidSize = 36;

        string VIEW_STATE_KEY_TOKEN = "GD";
        string VIEW_STATE_KEY_USERNAME = "UN";

        string UserName = string.Empty;
        Guid Token = Guid.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            //Retreive Query String parameter using QueryStringUtil
            //The Query string must be split prior to the Guid. 
            //First piece is Username or PrimaryEmail(both same), the rest of the string is VerificationToken (36 length)

            //Call SP. SP will return true or false indicating the validity of the VerificationToken for the User.
            //The SP will do the following:
            //  The EmailVerificationTimeStamp will be recorded in the Users table, only if the Usename and EmailVerification match.
            //  The EmailVerificationToken column will be set to null by SP in the Users table.
            //If the EmailVerificationToken/Username is not found, then manipulated QueryString var. Ignore the call. 
            if (!IsPostBack)
            {
                PasswordResetValueString = QueryStringHelper.PasswordResetTokenString;
                if (PasswordResetValueString == string.Empty)
                {
                    //If EmailVerificationValue is blank, indicate that a bad link was clicked.
                    DisplayMessage("Error", "You selected a bad hyperlink. Please contact support if you need assistance");
                }
                UserName = GetUserNameFromQueryString();
                Token = GetVerificationTokenFromQueryString();

                //NOTE: NO MATTER POSTBACK OR NOT, WE MUST VALIDATE THE TOKEN, EMAIL - FOR SECURITY REASONS.
                //OTHERWISE MANIPULATED OR ARTIFICIALLY CREATED POSTBACKS CAN HELP RESET PASSWORD.
                if (UserBLL.IsPasswordResetTokenValid(Token, UserName))
                {
                    DisplayMessage("Thank you!", "You may create your new password in the box below.");
                    PasswordChangePanel.Visible = true;
                }
                else
                    DisplayMessage("Sorry,", "The reset password email is only valid for 24 hours. You either followed an old email or used a bad link to get to this page. Please request password reset again or contact support center if you need further assistance.");
            }
            


        }

        protected void ChangePasswordCommand(object sender, EventArgs e)
        {
            //TODO: Validation to be replaced with ProxyValidator
            Page.Validate("ChangePasswordGroup");
            if (Page.IsValid)
            {
                //IMPORTANT! IMPORTANT! IMPORTANT!
                //NOTE: NO MATTER POSTBACK OR NOT, WE MUST VALIDATE THE TOKEN, EMAIL - FOR SECURITY REASONS.
                //OTHERWISE MANIPULATED OR ARTIFICIALLY CREATED POSTBACKS CAN HELP RESET PASSWORD.
                if (!UserBLL.IsPasswordResetTokenValid(Token, UserName))
                    DisplayMessage("Sorry,", "An error occured. Your action was not recognized. Please contact support center if you need further assistance.");

                string ErrorMessage;
                int? UserId = UserBLL.GetUserIdForUserName(UserName);
                if (UserId.HasValue)
                {
                    //sammit-start
                    if (PasswordValidation.DoesTextContainsWord(Password.Text.Trim().ToLower()))
                    {
                        DisplayMessage("The entered password contains a dictionary word and is not allowed.", true);
                        return;
                    }

                    UserProfile userProf = UserBLL.GetUserProfile((int)UserId);
                    UserAccount userAcc = UserBLL.GetUserAccount((int)UserId);

                    if (PasswordValidation.DoesTextContainsFirstLastName(Password.Text.Trim().ToLower(), userProf.FirstName.Trim().ToLower(), userProf.LastName.Trim().ToLower(), userProf.MiddleName.Trim().ToLower()))
                    {
                        DisplayMessage("The entered password contains either FirstName/MiddleName/LastName and is not allowed.", true);
                        return;
                    }

                    if (PasswordValidation.DoesPassWordContainsEmail(userAcc.PrimaryEmail.Trim().ToLower(), Password.Text.Trim().ToLower()))
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

                    //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    //{
                        if (UserBLL.ChangePassword(UserId.Value, Password.Text.Trim(), out ErrorMessage))
                        {
                            if (!SendEmailToUserAboutPasswordChange())
                            {
                                DisplayMessage("Sorry. We were unable to complete the password change because we were unable to send confirmation email to your email address on record. If the problem persists, please contact support.", true);
                                return;
                            }

                            //DisplayMessage("Your password has been changed successfully. You may login with your new password any time.", false);
                            DisplayMessage("Success!", "Your password has been successfully changed.  You may login any time using the new password. <br> <br><a href='https://shipnpr.shiptalk.org/default.aspx'> Click here to login </a> ");
                            PasswordChangePanel.Visible = false;
                            //scope.Complete();
                        }
                        else

                        {
                            // DisplayMessage("Sorry. Unable to change password. Please contact support for assistance.", false);

                            //sammit show the error message
                            DisplayMessage(ErrorMessage, true);
                            return;
                        }
                    //}
                }
                else
                    DisplayMessage("Sorry. Unable to change password. Please contact support for assistance.", false);
            }

        }


        private void DisplayMessage(string Title, string Descr)
        {
            ResetResultPanel.Visible = true;
            ResetResultTitle.Text = Title;
            ResetResultDescription.Text = Descr;
        }

        private void DisplayMessage(string ErrorMessage, bool IsError)
        {
            MessageArea.Visible = true;
            Message.Text = ErrorMessage;
        }


        /// <summary>
        /// Parse the QueryString Value for VerificationToken
        /// </summary>
        /// <returns></returns>
        private Guid GetVerificationTokenFromQueryString()
        {
            int fullLength = PasswordResetValueString.Length;
            int startIndex = (fullLength - GuidSize);
            return new Guid(PasswordResetValueString.Substring(startIndex, GuidSize));
        }

        /// <summary>
        /// Parse the QueryString Value for user email (Username)
        /// </summary>
        /// <returns></returns>
        private string GetUserNameFromQueryString()
        {
            int fullLength = PasswordResetValueString.Length;
            return PasswordResetValueString.Substring(0, fullLength - GuidSize);
        }


        private bool SendEmailToUserAboutPasswordChange()
        {
            string EmailAddress = UserName;

            ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
            mailMessage.ToList.Add(EmailAddress);

            if (ConfigUtil.WebEnvironment != "prod")
            {
                mailMessage.Subject = "Changes to your account at shipnpr.shiptalk.org(" + ConfigUtil.WebEnvironment + ")";

            }
            else
            {

                mailMessage.Subject = "Changes to your account at shipnpr.shiptalk.org";
            }

            mailMessage.Body = CreateEmailBodyForPasswordChangeConfirmation();
            ShiptalkMail mail = new ShiptalkMail(mailMessage);
            return mail.SendMail();

        }

        private string CreateEmailBodyForPasswordChangeConfirmation()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (ConfigUtil.WebEnvironment != "prod")
            {
                sb.AppendFormat("-----------TEST Confirm Your Password from DEV site------------"); sb.AddNewHtmlLines(2);
                sb.AppendFormat("-----------Created from DEV environment: {0}------------", ConfigUtil.WebEnvironment); sb.AddNewHtmlLines(2); ;
            }
            sb.Append("Hello,");
            sb.AddNewHtmlLines(3);
            sb.Append("This email is to confirm that your password to shipnpr.shiptalk.org account has been changed successfully.");
            sb.AddNewHtmlLines(2);
            sb.Append("If you did not change your password, please contact SHIP NPR Help Desk immediately.");
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



        #region "View state events"
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            Token = (Guid)ViewState[VIEW_STATE_KEY_TOKEN];
            UserName = (string)ViewState[VIEW_STATE_KEY_USERNAME];
        }

        protected override object SaveViewState()
        {
            ViewState[VIEW_STATE_KEY_TOKEN] = Token;
            ViewState[VIEW_STATE_KEY_USERNAME] = UserName;
            return base.SaveViewState();
        }

        #endregion

    }
}
