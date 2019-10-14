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

using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;

namespace ShiptalkWeb
{
    public partial class EmailConf : System.Web.UI.Page
    {
        string EmailVerificationValueString = string.Empty;
        int GuidSize = 36;


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
                EmailVerificationValueString = QueryStringHelper.EmailVerificationTokenString;
                if (EmailVerificationValueString == string.Empty)
                {
                    //If EmailVerificationValue is blank, indicate that a bad link was clicked.
                    DisplayMessage("Error", "You selected a bad hyperlink. Please contact support if you need assistance");
                }
                else
                {
                    //if this email verification is for Registration
                    if (String.IsNullOrEmpty(QueryStringHelper.EmailVerificationTypeString))
                    {

                        if (RegisterUserBLL.VerifyEmailToken(GetVerificationToken(), GetUserName()))
                        {
                            //LOGIC: If User was added, then we need not notify Approver.
                            //However, if the User was registered and then is verifying the email address, then we must notify admin about it.
                            bool IsRegisteredAccount = UserBLL.IsAccountCreatedViaUserRegistration(GetUserName());
                            if (IsRegisteredAccount)
                            {
                                if (RegisterUserBLL.NotifyDesignatedRegistrationNotificationRecipientForUser(GetUserName()))
                                    DisplayMessage("Thank you!", "You have successfully verified your email address. Your account is now waiting for approval. You will receive an email notification when a decision is made.");
                                else
                                    DisplayMessage("Unable to notify approver", "You have successfully verified your email address. However, please note that we encountered a problem while notifying the approver about your registration. Please contact support center about this problem.");
                            }
                            else
                            {
                                DisplayMessage("Thank you!", "You have successfully verified your email address. Your account is now active. You may login any time.");
                            }

                            ////TODO: Get designated email and dispatch it.
                            //if (IsRegisteredAccount && !RegisterUserBLL.NotifyDesignatedRegistrationNotificationRecipientForUser(GetUserName()))
                            //{
                            //    DisplayMessage("Unable to notify approver", "You have successfully verified your email address. However, please note that we encountered a problem while notifying the approver about your registration. Please contact support center about this problem.");
                            //}
                            //else
                            //    DisplayMessage("Thank you!", "You have successfully verified your email address. Your account is now waiting for approval. You will receive an email notification when a decision is made.");

                        }
                        else
                            DisplayMessage("Email confirmation", string.Format("New email verification requirement was not found for {0}. Either the email address was already verified, or you followed a bad link. Please contact the SHIP NPR Help Desk at 1-800-253-7154, option 1 or SHIPNPRHelp@air.org if you need further assistance.", GetUserName()));
                    }
                        // if this email verification is for email change
                    else
                        if (QueryStringHelper.EmailVerificationTypeString != null && QueryStringHelper.EmailVerificationTypeString != string.Empty)
                        {
                            //if it is for 'Email change Accept'
                            if (QueryStringHelper.EmailVerificationTypeString == "eca")
                            {
                                // EMail verification link is valid only for 24 hours. otherwise it should be expired.
                                if (UserBLL.IsEmailVerificationTokenValid(GetVerificationToken(), GetUserName()))
                                {
                                    //if the Email verification link is valid, change the user email. 
                                    if (RegisterUserBLL.AcceptOrRejectEmailChange(GetVerificationToken(), GetUserName(), "Accept"))
                                    {
                                        if (!RegisterUserBLL.SendEmailToUserAboutEmailChangeEmailVerification(GetUserName()))
                                        {
                                            DisplayMessage("Unable to notify user", "You have successfully verified your email address. However, please note that we encountered a problem while notifying you about your Email change. Please contact support center about this problem.");
                                            return;
                                        }

                                        else
                                        {

                                            DisplayMessage("Thank you!", "You have successfully verified your email address. You can login using your new email any time.");
                                        }
                                    }
                                    else
                                        DisplayMessage("Sorry", string.Format("New email verification requirement was not found for {0}. Either the email address was already verified, or you followed a bad link. Please contact the SHIP NPR Help Desk at 1-800-253-7154, option 1 or SHIPNPRHelp@air.org if you need further assistance.", GetUserName()));
                                }

                                else
                                    DisplayMessage("Sorry,", "The Email Verification link is only valid for 24 hours. You either followed an old email or used a bad link to get to this page. Please contact support center for further assistance.");
                            }

                            else   //if it is for 'Email change Reject'
                                if (QueryStringHelper.EmailVerificationTypeString == "ecr")
                                { 
                                     // EMail verification link is valid only for 24 hours. otherwise it should be expired.
                                    if (UserBLL.IsEmailVerificationTokenValid(GetVerificationToken(), GetUserName()))
                                    {
                                        //if the Email verification link is valid and the request is 'Reject', dont change the email
                                        if (RegisterUserBLL.AcceptOrRejectEmailChange(GetVerificationToken(), GetUserName(), "Reject"))
                                        {
                                             DisplayMessage("Thank you!", "We have cancelled your email change request.");                                          
                                        }
                                        else
                                            DisplayMessage("Sorry", "We could not complete you request. Either this request has already been completed, or you followed a bad link. Please contact the SHIP NPR Help Desk at 1-800-253-7154, option 1 or SHIPNPRHelp@air.org if you need further assistance.");
                                    }
                                    else
                                        DisplayMessage("Sorry,", "The Reject link is only valid for 24 hours. You either followed an old email or used a bad link to get to this page. Please contact support center for further assistance.");
                                }
                        }

                }
            }
        }


        private void DisplayMessage(string Title, string Descr)
        {
            colTitle.Text = Title;
            colTitle.Visible = true;

            dv3colSuccessMessage.Text = Descr;
            dv3colSuccessMessage.Visible = true;
        }

        /// <summary>
        /// Parse the QueryString Value for VerificationToken
        /// </summary>
        /// <returns></returns>
        private Guid GetVerificationToken()
        {
            int fullLength = EmailVerificationValueString.Length;
            int startIndex = (fullLength - GuidSize);
            return new Guid(EmailVerificationValueString.Substring(startIndex, GuidSize));
        }

        /// <summary>
        /// Parse the QueryString Value for user email (Username)
        /// </summary>
        /// <returns></returns>
        private string GetUserName()
        {
            int fullLength = EmailVerificationValueString.Length;
            return EmailVerificationValueString.Substring(0, fullLength - GuidSize);
        }

       


    }
}
