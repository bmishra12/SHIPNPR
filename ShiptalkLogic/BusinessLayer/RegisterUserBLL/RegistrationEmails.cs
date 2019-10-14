using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkCommon;

namespace ShiptalkLogic.BusinessLayer
{
    public static class RegistrationEmails
    {

        public static bool SendEmailVerificationNotification(bool IsUserCreatedByRegistration, int UserId, int? RegisteredByUserId, out string ErrorMessage)
        {
           
            bool mailSent = IsUserCreatedByRegistration
               ? SendUserRegistrationVerificationEmail(UserId, out ErrorMessage)
               : SendAddUserVerificationEmail(UserId,  out ErrorMessage);

            return mailSent;
        }


        private static bool SendUserRegistrationVerificationEmail(int UserId, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            UserViewData UserRegistrationData = UserBLL.GetUser(UserId);

            ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
            mailMessage.ToList.Add(UserRegistrationData.PrimaryEmail);

            if (ConfigUtil.WebEnvironment != "prod")
            {
                mailMessage.Subject = "Your shipnpr.shiptalk.org registration(" + ConfigUtil.WebEnvironment + ")";

            }
            else
            {
                mailMessage.Subject = "Your shipnpr.shiptalk.org registration";
            }

            Guid VerificationToken = DataLayer.UserDAL.GetEmailVerificationTokenForUser(UserRegistrationData.UserId);
            string sVerificationToken = string.Empty;
            if (VerificationToken == Guid.Empty)
                return false;
            else
                sVerificationToken = VerificationToken.ToString();

            string linkFormat = "<a href='" + ConfigUtil.EmailConfirmationUrl + "?evt={0}'>Follow this link</a>";
            string confirmLinkParam = UserRegistrationData.PrimaryEmail + sVerificationToken;
            string confirmLink = string.Format(linkFormat, confirmLinkParam);
            string textlink = ConfigUtil.EmailConfirmationUrl + "?evt={0}" ;
            textlink = string.Format(textlink, confirmLinkParam);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (ConfigUtil.WebEnvironment != "prod")
            {
                sb.AppendFormat("-----------TEST  User Registration DEV site------------"); sb.AddNewHtmlLines(2);
                sb.AppendFormat("-----------Created from DEV environment: {0}------------", ConfigUtil.WebEnvironment); sb.AddNewHtmlLines(2); ;
            }
            sb.AppendFormat("Dear {0} {1},", UserRegistrationData.FirstName.ToCamelCasing(), UserRegistrationData.LastName.ToCamelCasing()); 
            sb.AddNewHtmlLines(2);
            sb.Append("Thank you for registering at shipnpr.shiptalk.org.");
            sb.AddNewHtmlLines(3);
            sb.AppendFormat(confirmLink);
            sb.Append(" to verify your email address and submit your registration for approval. If you have difficulties accessing the link, copy and paste the link below to your browser to verify your email address.");
            sb.AddNewHtmlLines(2);
            sb.Append(textlink);
            sb.AddNewHtmlLines(3);
            sb.Append("You will receive an e-mail within a few days notifying you about the status of your registration request.");
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

        private static bool SendAddUserVerificationEmail(int UserId,  out string ErrorMessage)
        {
            ErrorMessage = string.Empty;

            UserViewData UserRegistrationData = UserBLL.GetUser(UserId);
            string EmailAddress = UserRegistrationData.PrimaryEmail;
            //UserViewData RegisteredByUserInfo = UserBLL.GetUser(RegisteredByUserId);

            Guid VerificationToken = UserBLL.GetEmailVerificationTokenForUser(UserId);
            string sVerificationToken = string.Empty;
            if (VerificationToken == Guid.Empty)
            {
                ErrorMessage = "An error occured while preparing content for email address verification procedure. Please contact support if this issue persists.";
                return false;
            }
            else
                sVerificationToken = VerificationToken.ToString();

            string linkFormat = "<a href='" + ConfigUtil.EmailConfirmationUrl + "?evt={0}'>Follow this link</a>";
            string confirmLinkParam = EmailAddress + sVerificationToken;
            string confirmLink = string.Format(linkFormat, confirmLinkParam);
            string textlink = ConfigUtil.EmailConfirmationUrl + "?evt={0}";
            textlink = string.Format(textlink, confirmLinkParam);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (ConfigUtil.WebEnvironment != "prod")
            {
                sb.AppendFormat("-----------TEST  User Account Registration DEV site------------"); sb.AddNewHtmlLines(2);
                sb.AppendFormat("-----------Created from DEV environment: {0}------------", ConfigUtil.WebEnvironment); sb.AddNewHtmlLines(2); ;
            }
            sb.AppendFormat("Dear {0} {1},", UserRegistrationData.FirstName.ToCamelCasing(), UserRegistrationData.LastName.ToCamelCasing());
            sb.AddNewHtmlLines(2);
            sb.Append("A new user account has been registered for you at the shipnpr.shiptalk.org.");
            sb.AddNewHtmlLines(2);
            sb.AppendFormat(confirmLink);
            sb.Append(" to verify your email address. If you have difficulties accessing the link, copy and paste the link below to your browser to verify your email address.");

            sb.AddNewHtmlLines(2);
            sb.Append(textlink);
            sb.AddNewHtmlLines(3);
            sb.Append("You will be able to login to the shipnpr.shiptalk.org upon successful verification of the email using the above link.");
            //sb.AddNewHtmlLine();
            //sb.AppendFormat("If you require assistance, please contact {0} at {1}.", RegisteredByUserInfo.FullName, RegisteredByUserInfo.PrimaryEmail);
            sb.AddNewHtmlLines(2);
            sb.Append("Thank you,");
            sb.AddNewHtmlLine();
            sb.Append("SHIP NPR Help Desk");
            sb.AddNewHtmlLine();
            sb.Append(ConfigUtil.ShiptalkSupportPhone);
            sb.AddNewHtmlLines(5);


            ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
            mailMessage.ToList.Add(EmailAddress);
           
            if (ConfigUtil.WebEnvironment != "prod")
            {
                mailMessage.Subject = "Welcome to shipnpr.shiptalk.org!(" + ConfigUtil.WebEnvironment + ")";

            }
            else
            {
                mailMessage.Subject = "Welcome to shipnpr.shiptalk.org!";
            }

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

    }
}
