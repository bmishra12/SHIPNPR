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
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ShiptalkWeb
{
    //TODO: Include Routing Info
    public partial class ChangePassword : System.Web.UI.Page, IRouteDataPage
    {
        string EmailAddress ;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.DataBind();
        }


        protected void ChangePasswordCommand(object sender, EventArgs e)
            
        {
            //TODO: Validation to be replaced with ProxyValidator
            Page.Validate("ChangePasswordGroup");
            if (Page.IsValid)
            {


                //sammit-start

                UserProfile userProf = UserBLL.GetUserProfile(ShiptalkPrincipal.UserId);
                if (userProf.LastPasswordChangeDate != null && ((DateTime)userProf.LastPasswordChangeDate).Date ==System.DateTime.Today )
                {

                    DisplayMessage("You are not allowed to change your password more than once in a day.", true);
                    return;
                }


                if (PasswordValidation.DoesTextContainsWord(Password.Text.Trim().ToLower()))
                {
                    DisplayMessage("The entered password contains a dictionary word and is not allowed.", true);
                    return;
                }


                UserAccount userAcc = UserBLL.GetUserAccount(ShiptalkPrincipal.UserId);
                EmailAddress = userAcc.PrimaryEmail;

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
                    bool DoCommit = false;
                    string ErrorMessage;
                    if (UserBLL.ChangePassword(ShiptalkPrincipal.UserId, Password.Text.Trim(), out ErrorMessage))
                    {
                        if (SendEmailToUserAboutPasswordChange())
                            DoCommit = true;

                    }
                    else
                    {
                        //sammit show the error message
                        DisplayMessage(ErrorMessage, true);
                        return;
                    }

                    if (DoCommit)
                    {
                        //scope.Complete();
                        DisplayMessage("Success!", "Your password has been changed successfully.");
                        ChangePasswordPanel.Visible = false;
                    }
                    else
                        DisplayMessage("Sorry. Unable to change your password. Please contact support for assistance.", false);
                //}
            }
           
        }

        private string UserName { get { return this.AccountInfo.PrimaryEmail; } }
        //private string EmailAddress { get { return UserName;} }


        private bool SendEmailToUserAboutPasswordChange()
        {
            //string EmailAddress = UserName; commented by BM, user not yest logged in so, account info does not have e-mail.
            // We will get e-mail from User account

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
                sb.AppendFormat("-----------TEST  Your Password Changed DEV site------------"); sb.AddNewHtmlLines(2);
                sb.AppendFormat("-----------Created from DEV environment: {0}------------", ConfigUtil.WebEnvironment); sb.AddNewHtmlLines(2); ;
            }
            sb.Append("Hello,");
            sb.AddNewHtmlLines(2);
            sb.Append("This email is to confirm that your password at shipnpr.shiptalk.org has been changed successfully.");
            sb.AddNewHtmlLines(2);
            sb.Append("If you did not change the password, please contact SHIP NPR Help Desk immediately.");
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

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion
    }
}
