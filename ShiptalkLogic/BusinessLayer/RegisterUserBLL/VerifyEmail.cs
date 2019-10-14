using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkCommon;

namespace ShiptalkLogic.BusinessLayer
{
    public static class VerifyEmail
    {

        public static bool SendEmailVerificationNotificationforEmailChange(bool IsUserCreatedByRegistration, int UserId, int? RegisteredByUserId, string NewEmail, out string ErrorMessage)
        {
            bool mailSent = false;

            //First send email to the old email about this request
            if (SendUserAboutEmailChangeRequest(UserId, out ErrorMessage) )                
            {
                //if above mail is successfully sent, then send an email to new email for email verification
                if(SendUserEmailChangeVerificationEmail(UserId, NewEmail, out ErrorMessage))
                    mailSent = true;         
            }               

          
            return mailSent;
        }

        private static bool SendUserEmailChangeVerificationEmail(int UserId, string NewEmail, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            UserViewData UserRegistrationData = UserBLL.GetUser(UserId);

            ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
            mailMessage.ToList.Add(NewEmail);
            mailMessage.Subject = "Your shipnpr.shiptalk.org Email Verification";

            Guid VerificationToken = DataLayer.UserDAL.GetEmailVerificationTokenForUser(UserRegistrationData.UserId);
            string sVerificationToken = string.Empty;
            if (VerificationToken == Guid.Empty)
                return false;
            else
                sVerificationToken = VerificationToken.ToString();

            string AcceptLinkFormat = "<a href='" + ConfigUtil.EmailConfirmationUrl + "?evty=eca&evt={0}'>Accept</a>";
            string RejectLinkFormat = "<a href='" + ConfigUtil.EmailConfirmationUrl + "?evty=ecr&evt={0}'>Reject</a>";

            string confirmLinkParam = NewEmail + sVerificationToken;

            string AcceptLink = string.Format(AcceptLinkFormat, confirmLinkParam);
            string RejectLink = string.Format(RejectLinkFormat, confirmLinkParam);

            string AcceptTextlink = ConfigUtil.EmailConfirmationUrl + "?evty=eca&evt={0}";
            string RejectTextlink = ConfigUtil.EmailConfirmationUrl + "?evty=ecr&evt={0}";

            AcceptTextlink = string.Format(AcceptTextlink, confirmLinkParam);
            RejectTextlink = string.Format(RejectTextlink, confirmLinkParam);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("Dear {0} {1},", UserRegistrationData.FirstName.ToCamelCasing(), UserRegistrationData.LastName.ToCamelCasing());
            sb.AddNewHtmlLines(2);
            sb.Append("You have requested to change your Email at shipnpr.shiptalk.org.");
            sb.AddNewHtmlLines(3);           
            sb.Append("Click on ");
            sb.AppendFormat(AcceptLink);
             sb.Append(" to verify your email address. If you have difficulties accessing the link, copy and paste the link below to your browser to verify your email address.");
            sb.AddNewHtmlLines(2);
            sb.Append(AcceptTextlink);
            sb.AddNewHtmlLines(2);
            sb.Append("If you didn't request this email change or if you don't want to change your email, click on ");
            sb.AppendFormat(RejectLink);
            sb.Append(" to cancel this request. If you have difficulties accessing the link, copy and paste the link below to your browser to cancel this request.");
            sb.AddNewHtmlLines(2);
            sb.Append(RejectTextlink);
            sb.AddNewHtmlLines(2);
            sb.Append("These links will expire in 24 hours. Please make sure you Accept or Reject this change within 24 hours.");
            sb.AddNewHtmlLines(3);
            sb.Append("Thank you,");
            sb.AddNewHtmlLines(2);
            sb.Append("SHIP NPR Help Desk");
            sb.AddNewHtmlLine();
            sb.Append(ConfigUtil.ShiptalkSupportPhone);
            sb.AddNewHtmlLine();
            sb.Append(ConfigUtil.EmailOfResourceCenter);
            sb.AddNewHtmlLines(5);

            mailMessage.Body = sb.ToString();
            ShiptalkMail mail = new ShiptalkMail(mailMessage);
            if (!mail.SendMail())
            {
                ErrorMessage = string.Format("An error occured while sending email to {0}.", NewEmail);
                return false;
            }
            else
                return true;
        }

        private static bool SendUserAboutEmailChangeRequest(int UserId, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            UserViewData UserRegistrationData = UserBLL.GetUser(UserId);

            ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
            mailMessage.ToList.Add(UserRegistrationData.PrimaryEmail);
            mailMessage.Subject = "Your shipnpr.shiptalk.org Email Change request";            

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("Dear {0} {1},", UserRegistrationData.FirstName.ToCamelCasing(), UserRegistrationData.LastName.ToCamelCasing());
            sb.AddNewHtmlLines(2);
            sb.Append("You have requested to change your Email at shipnpr.shiptalk.org.");
            sb.AddNewHtmlLines(2);
            sb.Append("The Verification email has been sent to your new email id. Please check the email and follow the instructions to verify your email address. You can continue using Shipnpr website with your old email id until the new email id is being verified.");
            sb.AddNewHtmlLines(2);
            sb.Append("If you have not requested for Email change, please contact SHIP NPR Help Desk at 1-800-253-7154, option 1 or <a href='mailto:SHIPNPRHelp@air.org'>SHIPNPRHelp@air.org</a> immediately.");            
            sb.AddNewHtmlLines(3);            
            sb.Append("Thank you,");
            sb.AddNewHtmlLines(2);
            sb.Append("SHIP NPR Help Desk");
            sb.AddNewHtmlLine();
            sb.Append(ConfigUtil.ShiptalkSupportPhone);
            sb.AddNewHtmlLine();
            sb.Append(ConfigUtil.EmailOfResourceCenter);
            sb.AddNewHtmlLines(5);

            mailMessage.Body = sb.ToString();
            ShiptalkMail mail = new ShiptalkMail(mailMessage);
            if (!mail.SendMail())
            {
                ErrorMessage = string.Format("An error occured while sending email to {0}.", UserRegistrationData.PrimaryEmail);
                return false;
            }
            else
                return true;
        }

        //private static bool SendUserRegistrationVerificationEmail(int UserId, out string ErrorMessage)
        //{
        //    ErrorMessage = string.Empty;
        //    UserViewData UserRegistrationData = UserBLL.GetUser(UserId);

        //    ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
        //    mailMessage.ToList.Add(UserRegistrationData.PrimaryEmail);
        //    mailMessage.Subject = "Your shipnpr.shiptalk.org Email Verification";

        //    Guid VerificationToken = DataLayer.UserDAL.GetEmailVerificationTokenForUser(UserRegistrationData.UserId);
        //    string sVerificationToken = string.Empty;
        //    if (VerificationToken == Guid.Empty)
        //        return false;
        //    else
        //        sVerificationToken = VerificationToken.ToString();

        //    string linkFormat = "<a href='" + ConfigUtil.EmailConfirmationUrl + "?evt={0}'>Follow this link</a>";
        //    string confirmLinkParam = UserRegistrationData.PrimaryEmail + sVerificationToken;
        //    string confirmLink = string.Format(linkFormat, confirmLinkParam);
        //    string textlink = ConfigUtil.EmailConfirmationUrl + "?evt={0}";
        //    textlink = string.Format(textlink, confirmLinkParam);

        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    sb.AppendFormat("Dear {0} {1},", UserRegistrationData.FirstName.ToCamelCasing(), UserRegistrationData.LastName.ToCamelCasing());
        //    sb.AddNewHtmlLines(2);
        //    sb.Append("You have requested to change your email at shipnpr.shiptalk.org.");
        //    sb.AddNewHtmlLines(3);
        //    sb.AppendFormat(confirmLink);
        //    sb.Append(" Click on the below link to verify your email address. If you have difficulties accessing the link, copy and paste the link below to your browser to verify your email address.");
        //    sb.AddNewHtmlLines(2);
        //    sb.Append(textlink);
        //    sb.AddNewHtmlLines(3);
        //     sb.Append("Thank you,");
        //    sb.AddNewHtmlLines(2);
        //    sb.Append("SHIP NPR Help Desk");
        //    sb.AddNewHtmlLine();
        //    sb.Append(ConfigUtil.ShiptalkSupportPhone);
        //    sb.AddNewHtmlLine();
        //    sb.Append(ConfigUtil.EmailOfResourceCenter);
        //    sb.AddNewHtmlLines(5);

        //    mailMessage.Body = sb.ToString();
        //    ShiptalkMail mail = new ShiptalkMail(mailMessage);
        //    if (!mail.SendMail())
        //    {
        //        ErrorMessage = string.Format("An error occured while sending email to {0}.", UserRegistrationData.PrimaryEmail);
        //        return false;
        //    }
        //    else
        //        return true;
        //}

      

    }
}
