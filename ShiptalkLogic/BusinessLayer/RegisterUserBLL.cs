using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkCommon;
using ShiptalkLogic.DataLayer;

using System.Linq.Expressions;

using AutoMapper;


namespace ShiptalkLogic.BusinessLayer
{

    public class RegisterUserBLL
    {

        public static bool VerifyEmailToken(Guid VerificationToken, string UserName)
        {
            return UserDAL.VerifyEmailToken(VerificationToken, UserName);
        }


        public static bool AcceptOrRejectEmailChange(Guid VerificationToken, string NewEmal, string Request)
        {
            return UserDAL.AcceptOrRejectEmailChange(VerificationToken, NewEmal, Request);
        }


        public static IRegisterUser CreateRegistrationProviderObject(UserRegistration _UserRegObj)
        {
            return RegisterUserImplBase.CreateObject(_UserRegObj);
        }


        public static bool DoesUserNameExist(string UserName)
        {
            return UserDAL.DoesUserNameExist(UserName);
        }


        /// <summary>
        /// Returns the email address of Designated Approvers for the User Scope.
        /// If any approvers are unavailable for current scope, Approver from higher scope will be used.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        private static IEnumerable<KeyValuePair<int, string>> GetApproversForUser(int UserId)
        {
            IEnumerable<KeyValuePair<int, string>> Approvers = UserBLL.GetApproversForUser(UserId);
            if (Approvers != null && Approvers.Count() > 0)
                return Approvers;
            else
            {
                List<KeyValuePair<int, string>> defaultVal = new List<KeyValuePair<int, string>>();
                defaultVal.Add(new KeyValuePair<int, string>(-1, ShiptalkCommon.ConfigUtil.EmailOfTechSupport));
                return defaultVal.AsEnumerable();
            }
        }


        public static bool NotifyDesignatedRegistrationNotificationRecipientForUser(string UserName)
        {

            int? UserId = UserDAL.GetUserIdForUserName(UserName);
            if (UserId.HasValue)
            {
                //Prepare Mail Object
                ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
                UserProfile userProfile = UserBLL.GetUserProfile(UserId.Value);
                UserAccount userAccount = UserBLL.GetUserAccount(UserId.Value);

                var Approvers = GetApproversForUser(UserId.Value);
                foreach (var approver in Approvers) mailMessage.ToList.Add(approver.Value);


                if (ConfigUtil.WebEnvironment != "prod")
                {
                    mailMessage.Subject = "New shipnpr.shiptalk.org(" + ConfigUtil.WebEnvironment + ")Registration";

                }
                else
                {
                    mailMessage.Subject = "New shipnpr.shiptalk.org Registration";
                }
                mailMessage.Body = CreateEmailBodyForRegistrationNotification(userProfile, userAccount);

                //Send Mail here
                ShiptalkMail mail = new ShiptalkMail(mailMessage);
                return mail.SendMail();
            }
            else
                throw new ShiptalkException("Unable to find UserId for Username: " + UserName, false);

        }


        private static string CreateEmailBodyForRegistrationNotification(UserProfile userProfile, UserAccount userAccount)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (ConfigUtil.WebEnvironment != "prod")
            {
                sb.AppendFormat("-----------TEST User Registration from DEV site------------"); sb.Append(AddLines(2));
                sb.AppendFormat("-----------Created from DEV environment: {0}------------", ConfigUtil.WebEnvironment);sb.Append(AddLines(2));
            }
            sb.AppendFormat("-----------New User Registration Notification------------"); sb.Append(AddLines(2));


            sb.Append("A new user has registered at shipnpr.shiptalk.org."); sb.Append(AddLine());
            sb.Append("The account is currently inactive and awaiting your approval.");
            sb.Append(AddLines(3));


            sb.Append("Here is a summary of the registration:"); sb.Append(AddLines(2));

            sb.Append("<strong>Personal Information:</strong>"); sb.Append(AddLine());
            sb.AppendFormat("Name: {0}", userProfile.FirstName); sb.Append(AddLine());
            sb.AppendFormat("Middle Name: {0}", userProfile.MiddleName); sb.Append(AddLine());
            sb.AppendFormat("Last Name: {0}", userProfile.LastName); sb.Append(AddLine());
            sb.AppendFormat("Nick Name: {0}", userProfile.NickName); sb.Append(AddLines(2));

            sb.Append("<strong>Contact Information:</strong>"); sb.Append(AddLine());
            sb.AppendFormat("Primary Phone: {0}", userProfile.PrimaryPhone); sb.Append(AddLine());
            sb.AppendFormat("Primary Email: {0}", userAccount.PrimaryEmail); sb.Append(AddLine());
            sb.AppendFormat("Secondary Email: {0}", userProfile.SecondaryEmail); sb.Append(AddLine());
            sb.AppendFormat("Primary Phone: {0}", userProfile.PrimaryPhone); sb.Append(AddLines(2));

            sb.Append("<strong>Requested Account Information:</strong>"); sb.Append(AddLine());
            sb.AppendFormat("State: {0}", State.GetStateName(userAccount.StateFIPS)); sb.Append(AddLine());

            string role = userAccount.ScopeId.ToEnumObject<Scope>().Description() + (userAccount.IsAdmin ? " Admin access" : " User access");
            sb.AppendFormat("Role: {0}", role); sb.Append(AddLines(3));

            //Need to add User Descriptor and Regional Access profile by verifying what role or agency was selected.

            sb.Append("Please login to https://SHIPNPR.SHIPTalk.org to approve or deny this registration request.");
            sb.Append(AddLines(2));
            sb.Append("Thank you,");
            sb.Append(AddLine());
            sb.Append("SHIP NPR Help Desk");
            sb.AddNewHtmlLine();
            sb.Append(ConfigUtil.ShiptalkSupportPhone);
            sb.AddNewHtmlLine();
            sb.Append(ConfigUtil.EmailOfResourceCenter);
            sb.AddNewHtmlLines(5);


            return sb.ToString();
        }

        //Added Lavanya
        public static bool SendEmailToUserAboutEmailChangeEmailVerification(string UserName)
        {
            string EmailAddress = UserName;

            ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
            mailMessage.ToList.Add(EmailAddress);
            mailMessage.Subject = "Changes to your account at shipnpr.shiptalk.org";

            mailMessage.Body = CreateEmailBodyForEmailChangeConfirmation();
            ShiptalkMail mail = new ShiptalkMail(mailMessage);
            return mail.SendMail();

        }

        private static string CreateEmailBodyForEmailChangeConfirmation()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (ConfigUtil.WebEnvironment != "prod")
            {
                sb.AppendFormat("-----------TEST Confirm Your Email from DEV site------------"); sb.AddNewHtmlLines(2);
                sb.AppendFormat("-----------Created from DEV environment: {0}------------", ConfigUtil.WebEnvironment); sb.AddNewHtmlLines(2); ;
            }
            sb.Append("Hello,");
            sb.AddNewHtmlLines(3);
            sb.Append("This email is to confirm that your Email of shipnpr.shiptalk.org account has been changed successfully.");
            sb.AddNewHtmlLines(2);
            sb.Append("If you did not change your Email, please contact SHIP NPR Help Desk immediately.");
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
        // end : Lavanya

        private static string AddLines(Int16 NoOfLines)
        {
            string line = "<BR />";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < NoOfLines; i++)
                sb.Append(line);

            return sb.ToString();
        }



        private static string AddLine()
        {
            return AddLines(1);
        }


        private static IEnumerable<UserSummaryViewData> GetPendingUserRegistrationsForCMS(bool IncludePendingEmailVerifications)
        {
            return GetPendingUserRegistrationsForState(State.GetStateFIPSForCMS(), IncludePendingEmailVerifications);
        }

        private static IEnumerable<UserSummaryViewData> GetPendingUserRegistrationsForState(string StateFIPS, bool IncludePendingEmailVerifications)
        {
            IEnumerable<User> userList = UserDAL.GetPendingUsersByState(StateFIPS, ConfigUtil.PendingUserRegistrationDays, IncludePendingEmailVerifications);
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserSummaryViewData>>(userList);
        }


        public static IEnumerable<UserSummaryViewData> GetPendingUniqueIdRequestsForState(string StateFIPS)
        {
            IEnumerable<User> userList = null;
            userList = UserDAL.GetPendingUniqueIdRequestsByState(StateFIPS, ConfigUtil.PendingUserRegistrationDays);

            return Mapper.Map<IEnumerable<User>, IEnumerable<UserSummaryViewData>>(userList);
        }

        public static IEnumerable<UserSummaryViewData> GetAllPendingUniqueIdRequestsByState(string StateFIPS)
        {
            IEnumerable<User> userList = null;
             userList = UserDAL.GetAllPendingUniqueIdRequestsByState(StateFIPS, ConfigUtil.PendingUserRegistrationDays);

            return Mapper.Map<IEnumerable<User>, IEnumerable<UserSummaryViewData>>(userList);
        }

        public static IEnumerable<UserSummaryViewData> GetRevokedPendingUniqueIdRequestsByState(string StateFIPS)
        {
            IEnumerable<User> userList = null;
            userList = UserDAL.GetRevokedPendingUniqueIdRequestsByState(StateFIPS);

            return Mapper.Map<IEnumerable<User>, IEnumerable<UserSummaryViewData>>(userList);
        }


        public static IEnumerable<UserSummaryViewData> GetApprovedUniqueIdRequestsByStateFIPS(string StateFIPS)
        {
            IEnumerable<User> userList = UserDAL.GetApprovedUniqueIdRequestsByStateFIPS(StateFIPS);
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserSummaryViewData>>(userList);
        }

        /// <summary>
        /// Returns all pending registrations where the Admin is an Approver designate.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        private static IEnumerable<UserSummaryViewData> GetPendingUserRegistrationsForSubStateAdmin(int SubStateAdminUserId, bool IncludePendingEmails)
        {
            IEnumerable<UserRegionalAccessProfile> profiles = UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(SubStateAdminUserId, true).Where(p => p.IsApproverDesignate == true);
            int Count = 0;
            if (profiles != null) Count = profiles.Count();

            if (Count == 0)
                return null;
            else if (Count == 1)
            {
                return GetPendingUsersBySubStateRegionId(profiles.First().RegionId, IncludePendingEmails);
            }
            else
            {
                List<UserSummaryViewData> userSummaryList = new List<UserSummaryViewData>();
                IEnumerable<UserSummaryViewData> tempList = null;
                foreach (UserRegionalAccessProfile profile in profiles)
                {
                    tempList = GetPendingUsersBySubStateRegionId(profiles.First().RegionId, IncludePendingEmails);
                    if (tempList != null)
                        userSummaryList.AddRange(tempList);
                }
                return userSummaryList;
            }
        }



        /// <summary>
        /// Returns all pending users in agencies where the Admin is approver.
        /// </summary>
        /// <param name="AgencyAdminUserId"></param>
        /// <returns></returns>

        private static IEnumerable<UserSummaryViewData> GetPendingUserRegistrationsForAgencyAdmin(int AgencyAdminUserId, bool IncludePendingEmails)
        {
            IEnumerable<UserRegionalAccessProfile> profiles = UserAgencyBLL.GetUserAgencyProfiles(AgencyAdminUserId, true).Where(p => p.IsApproverDesignate == true);
            int Count = 0;
            if (profiles != null)
                Count = profiles.Count();

            if (Count == 0)
                return null;
            else if (Count == 1)
            {
                return GetPendingUsersByAgencyId(profiles.First().RegionId, IncludePendingEmails);
            }
            else
            {
                List<UserSummaryViewData> userSummaryList = new List<UserSummaryViewData>();
                IEnumerable<UserSummaryViewData> tempList = null;
                foreach (UserRegionalAccessProfile profile in profiles)
                {
                    tempList = GetPendingUsersByAgencyId(profiles.First().RegionId, IncludePendingEmails);
                    if (tempList != null)
                        userSummaryList.AddRange(tempList);
                }
                return userSummaryList;
            }
        }

        /// <summary>
        /// Call only if the Admin who is requesting the info is a Designated Approver
        /// </summary>
        /// <param name="SubStateRegionId"></param>
        /// <returns></returns>
        private static IEnumerable<UserSummaryViewData> GetPendingUsersBySubStateRegionId(int SubStateRegionId, bool IncludePendingEmails)
        {
            List<User> userData = UserDAL.GetPendingUsersBySubStateRegionId(SubStateRegionId, ConfigUtil.PendingUserRegistrationDays, IncludePendingEmails);
            if (userData != null)
                return Mapper.Map<IEnumerable<User>, IEnumerable<UserSummaryViewData>>(userData);
            else
                return null;
        }

        /// <summary>
        /// Call only if the Admin who is requesting the info is a Designated Approver
        /// </summary>
        /// <param name="AgencyId"></param>
        /// <returns></returns>
        private static IEnumerable<UserSummaryViewData> GetPendingUsersByAgencyId(int AgencyId, bool IncludePendingEmails)
        {
            List<User> userData = UserDAL.GetPendingUsersByAgencyId(AgencyId, ConfigUtil.PendingUserRegistrationDays, IncludePendingEmails);
            if (userData != null)
                return Mapper.Map<IEnumerable<User>, IEnumerable<UserSummaryViewData>>(userData);
            else
                return null;
        }


        //public static bool ApproveUser(int UserId, int ApproverId, List<int> DescriptorIds, out string ErrorMessage)
        public static bool ApproveUser(UserViewData viewData, int ApproverId, out string ErrorMessage)
        {
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
                //IMPORTANT: FOR SHIP DIRECTORS, WE NEED TO ADD THE SHIP DIRECTOR DESCRIPTOR HERE.
                //IN GENERAL, WHEN DESCRIPTORS ARE ADDED FOR A USER, EXISTING ONES ARE DELETED AND NEW ONES ARE ADDED TO AVOID, OMISSIONS DURING UPDATE.
                //SINCE WE DON'T SHOW A SHIPDIRECTOR DESCRIPTOR DURING APPROVAL, THE DESCRIPTOR GOES AWAY DURING APPROVAL.
                //HENCE WE OFFSET IT HERE.
                if (viewData.IsShipDirector)
                {
                    List<int> DescriptorIDList = new List<int>(viewData.DescriptorIds);
                    DescriptorIDList.Add(Descriptor.ShipDirector.EnumValue<int>());
                    viewData.DescriptorIds = DescriptorIDList;
                }

                //Save the Descriptors for User and then Approve.
                IEnumerable<int> NewDescriptorIds = viewData.DescriptorIds;

                int AgencyId = 0;
                //For Agency Users and SubState Users, need to use the actual AgencyId
                //For other Users, a constant value of 0 is used.
                if (viewData.IsUserAgencyScope)
                    AgencyId = viewData.DefaultAgencyIdOfUser;
                else if (viewData.IsUserSubStateRegionalScope)
                    AgencyId = viewData.DefaultSubStateRegionIdOfUser;
                else
                    AgencyId = Constants.Defaults.DefaultValues.AgencyIdForNonAgencyUsers;

                if (!SaveApprovedDescriptors(viewData.UserId, NewDescriptorIds, AgencyId, ApproverId, out ErrorMessage))
                    return false;

                //Approve here.
                int UserId = viewData.UserId;
                if (UserBLL.ApproveUser(UserId, ApproverId))
                {
                    if (SendApprovedEmail(UserId))
                    {
                        //scope.Complete();
                        ErrorMessage = string.Empty;
                        return true;
                    }
                    else
                    {
                        ErrorMessage = "Attempt to send email notification to the User failed. Please try again, later.";
                        return false;
                    }
                }
                else
                {
                    ErrorMessage = "Approval process failed. Please try later or contact support if this issue persists.";
                    return false;
                }
            //}
        }

        //Save DescriptorId, AgencyId for User.
        private static bool SaveApprovedDescriptors(int UserId, IEnumerable<int> NewDescriptorIds, int AgencyId, int ApprovedBy, out string ErrorMessage)
        {
            return UserBLL.SaveDescriptors(UserId, NewDescriptorIds, AgencyId, ApprovedBy, out ErrorMessage);

            ////Delete old descriptors
            //if (!UserBLL.DeleteAllDescriptorsForUser(UserId, AgencyId, out ErrorMessage))
            //    return false;

            ////Add all new descriptors
            //if (NewDescriptorIds != null && NewDescriptorIds.Count() > 0)
            //{
            //    return UserBLL.AddDescriptorsForUser(UserId, NewDescriptorIds, (int?)AgencyId, ApprovedBy, out ErrorMessage);
            //}

            //ErrorMessage = string.Empty;
            //return true;
        }

        private static bool SendApprovedEmail(int UserId)
        {
            UserViewData userView = UserBLL.GetUser(UserId);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendFormat("Dear {0},", userView.FullName);
            sb.AddNewHtmlLines(2);
            sb.Append("Your request to shipnpr.shiptalk.org account has been approved.");
            sb.AddNewHtmlLines(2);
            sb.Append("You may login anytime using your registered information.");
            sb.AddNewHtmlLines(2);

            sb.Append("If you do not know your new SHIPtalk password, you can reset it by going to <a href='https://shipnpr.shiptalk.org'>https://shipnpr.shiptalk.org</a> and clicking 'Forgot password?' in the left of the screen. Follow the instructions to have the password reset instructions emailed to you. Once you reset your password, you should be able to log in to the website with your username (email address) and new password.");
            sb.AddNewHtmlLines(2);

            sb.Append("Submit your entire email address so the instructions to reset your password will be emailed to you.");
            sb.AddNewHtmlLine();

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
            mailMessage.Subject = "Your shipnpr.shiptalk.org account is approved";

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

        private static bool SendDisapproveEmail(UserViewData UserData)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendFormat("Dear {0},", UserData.FullName);
            sb.AddNewHtmlLines(2);
            sb.Append("Your request to shipnpr.shiptalk.org.account has been denied by the administrator.");
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
            mailMessage.ToList.Add(UserData.PrimaryEmail);
            mailMessage.Subject = "Your shipnpr.shiptalk.org account is denied.";

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

        public static bool DenyUser(UserViewData UserData, int ApproverId, out string ErrorMessage)
        {
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
            string FailureReason;
            if (UserBLL.DeleteUser(UserData.UserId, out FailureReason))
            {
                if (SendDisapproveEmail(UserData))
                {
                    //scope.Complete();
                    ErrorMessage = string.Empty;
                    return true;
                }
                else
                {
                    ErrorMessage = "Attempt to send email notification to the User failed. Please try later.";
                    return false;
                }
            }
            else
            {
                //This is old message. After we introduced Failure reason, we have not changed the message.
                ErrorMessage = "Deny process failed. Please try later or contact support if this issue persists.";
                return false;
            }
            //}
        }


        public static OldShipUserInfo GetOldShipUserInfo(string UserName, string ClearTextPassword)
        {
            string EncryptedPassword = EncryptorFactory.CreateEncryptor(EncryptionType.DES).Encrypt(ClearTextPassword);
            OldShipUserInfo oldInfo = null;
            //If User provided valid Login/Password, then get user info.
            if (UserDAL.OldShipUserLogin(UserName, EncryptedPassword))
            {
                oldInfo = UserDAL.GetOldShipUserInfo(UserName);
            }
            return oldInfo;
        }


        public static IEnumerable<UserSummaryViewData> GetPendingUserRegistrationsForApprover(UserAccount ApproverAccountInfo, bool IncludePendingEmails)
        {
            switch (ApproverAccountInfo.Scope)
            {
                case Scope.CMS:
                    if (ApproverAccountInfo.IsApproverDesignate.HasValue ? ApproverAccountInfo.IsApproverDesignate.Value : false)
                        return GetPendingUserRegistrationsForCMS(IncludePendingEmails);
                    break;
                case Scope.State:
                    //Only Ship Directors can view State Admins of their State in Pending Approval screen.
                    //if (LookupBLL.IsShipDirector(ApproverAccountInfo.UserId, ApproverAccountInfo.StateFIPS))
                    if (ApproverAccountInfo.IsShipDirector || (ApproverAccountInfo.IsApproverDesignate.HasValue ? ApproverAccountInfo.IsApproverDesignate.Value : false))
                        return (GetPendingUserRegistrationsForState(ApproverAccountInfo.StateFIPS, IncludePendingEmails));
                    //else
                    //{
                    //    //int? ShipDirectorRecordIndex = null;
                    //    IEnumerable<UserSummaryViewData> viewData = GetPendingUserRegistrationsForState(ApproverAccountInfo.StateFIPS);
                    //    return (from record in viewData
                    //            where !(record.Scope == Scope.State && record.IsAdmin == true)
                    //            select record);
                    //}
                    break;
                case Scope.SubStateRegion:
                    return GetPendingUserRegistrationsForSubStateAdmin(ApproverAccountInfo.UserId, IncludePendingEmails);
                    break;
                case Scope.Agency:
                    return GetPendingUserRegistrationsForAgencyAdmin(ApproverAccountInfo.UserId, IncludePendingEmails);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return null;
        }

       // Included by Lavanya on 07/19/2012
        public static IEnumerable<UserSummaryViewData> GetPendingEmailChangeVerifications(UserAccount ApproverAccountInfo)
        {
            switch (ApproverAccountInfo.Scope)
            {
                case Scope.CMS:
                    if (ApproverAccountInfo.IsApproverDesignate.HasValue ? ApproverAccountInfo.IsApproverDesignate.Value : false)
                        return GetPendingEmailChangeVerificationsForCMS("CMS");
                    break;
                case Scope.State:
                    //Only Ship Directors can view State Admins of their State in Pending Approval screen.
                    //if (LookupBLL.IsShipDirector(ApproverAccountInfo.UserId, ApproverAccountInfo.StateFIPS))
                    if (ApproverAccountInfo.IsShipDirector || (ApproverAccountInfo.IsApproverDesignate.HasValue ? ApproverAccountInfo.IsApproverDesignate.Value : false))
                        return (GetPendingEmailChangeVerificationsForState(ApproverAccountInfo.StateFIPS, "State"));                   
                    break;
                case Scope.SubStateRegion:
                    return GetPendingEmailChangeVerificationsForSubStateAdmin(ApproverAccountInfo.UserId, "SubStateRegion");
                    break;
                case Scope.Agency:
                    return GetPendingEmailChangeVerificationsForAgencyAdmin(ApproverAccountInfo.UserId, "Agency");
                    break;
                default:
                    throw new NotImplementedException();
            }
            return null;
        }

        private static IEnumerable<UserSummaryViewData> GetPendingEmailChangeVerificationsForCMS(string scope)
        {
            return GetPendingEmailChangeVerificationsForState(State.GetStateFIPSForCMS(), scope);
        }


        // For CMS, STATE, pass '0' to AgencyID and SubStateRegionId
        private static IEnumerable<UserSummaryViewData> GetPendingEmailChangeVerificationsForState(string StateFIPS, string scope)
        {
            IEnumerable<User> userList = UserDAL.GetPendingEmailChangeVerifications(StateFIPS, 0,0, scope);
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserSummaryViewData>>(userList);
        }

        /// <summary>
        /// Returns all pending EmailChangeVerifications where the Admin is an Approver designate.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        private static IEnumerable<UserSummaryViewData> GetPendingEmailChangeVerificationsForSubStateAdmin(int SubStateAdminUserId, string scope)
        {
            IEnumerable<UserRegionalAccessProfile> profiles = UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(SubStateAdminUserId, true).Where(p => p.IsApproverDesignate == true);
            int Count = 0;
            if (profiles != null) Count = profiles.Count();

            if (Count == 0)
                return null;
            else if (Count == 1)
            {
                return GetPendingEmailChangeVerificationsBySubStateRegionId(profiles.First().RegionId, scope);
            }
            else
            {
                List<UserSummaryViewData> userSummaryList = new List<UserSummaryViewData>();
                IEnumerable<UserSummaryViewData> tempList = null;
                foreach (UserRegionalAccessProfile profile in profiles)
                {
                    tempList = GetPendingEmailChangeVerificationsBySubStateRegionId(profiles.First().RegionId, scope);
                    if (tempList != null)
                        userSummaryList.AddRange(tempList);
                }
                return userSummaryList;
            }
        }
        
        /// <summary>
        /// Call only if the Admin who is requesting the info is a Designated Approver
        /// For SubStateRegion, pass empty value to StateFIPS and '0' to AgencyID 
        /// </summary>
        /// <param name="SubStateRegionId"></param>
        /// <returns></returns>
        private static IEnumerable<UserSummaryViewData> GetPendingEmailChangeVerificationsBySubStateRegionId(int SubStateRegionId, string scope)
        {
            List<User> userData = UserDAL.GetPendingEmailChangeVerifications("", 0, SubStateRegionId, scope);
            if (userData != null)
                return Mapper.Map<IEnumerable<User>, IEnumerable<UserSummaryViewData>>(userData);
            else
                return null;
        }

        /// <summary>
        /// Returns all pending EmailChangeVerifications in agencies where the Admin is approver.
        /// </summary>
        /// <param name="AgencyAdminUserId"></param>
        /// <returns></returns>

        private static IEnumerable<UserSummaryViewData> GetPendingEmailChangeVerificationsForAgencyAdmin(int AgencyAdminUserId, string scope)
        {
            IEnumerable<UserRegionalAccessProfile> profiles = UserAgencyBLL.GetUserAgencyProfiles(AgencyAdminUserId, true).Where(p => p.IsApproverDesignate == true);
            int Count = 0;
            if (profiles != null)
                Count = profiles.Count();

            if (Count == 0)
                return null;
            else if (Count == 1)
            {
                return GetPendingEmailChangeVerificationsByAgencyId(profiles.First().RegionId, scope);
            }
            else
            {
                List<UserSummaryViewData> userSummaryList = new List<UserSummaryViewData>();
                IEnumerable<UserSummaryViewData> tempList = null;
                foreach (UserRegionalAccessProfile profile in profiles)
                {
                    tempList = GetPendingEmailChangeVerificationsByAgencyId(profiles.First().RegionId, scope);
                    if (tempList != null)
                        userSummaryList.AddRange(tempList);
                }
                return userSummaryList;
            }
        }

        /// <summary>
        /// Call only if the Admin who is requesting the info is a Designated Approver
        /// For Agency,  pass empty value to StateFIPS and '0' to AgencyID and SubStateRegion
        /// </summary>
        /// <param name="AgencyId"></param>
        /// <returns></returns>
        private static IEnumerable<UserSummaryViewData> GetPendingEmailChangeVerificationsByAgencyId(int AgencyId, string scope)
        {
            List<User> userData = UserDAL.GetPendingEmailChangeVerifications("", AgencyId, 0, scope);
            if (userData != null)
                return Mapper.Map<IEnumerable<User>, IEnumerable<UserSummaryViewData>>(userData);
            else
                return null;
        }


    }


}

