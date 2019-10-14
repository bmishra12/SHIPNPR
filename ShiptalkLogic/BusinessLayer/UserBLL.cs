using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Web;
using System.Data;
using ShiptalkLogic.DataLayer;
using ShiptalkCommon;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using AutoMapper;

using System.Transactions;

namespace ShiptalkLogic.BusinessLayer
{


    public class UserBLL
    {

        /// <summary>
        /// Authenticate User. If Login fails - Get Error message related to the failure.
        /// The Validate will accept a plain text password and encrypted it before calling the DAL.
        /// Since Authentication is the most important piece of all, the BLL must verify password and handle encryption
        /// and not rely on the caller.
        /// </summary>
        /// <param name="Username">string</param>
        /// <param name="PlainTextPassword">string</param>
        /// <param name="UserId">int</param>
        /// <param name="ErrorMessage">output parameter string</param>
        /// <returns>bool indicating true or false; Also sets ErrorMessage if return value is false.</returns>
        //public  bool AuthenticateUser(string Username, string ClearTextPassword, out int UserId, out FailedLoginReason failedLoginReasonObj)
        //{

        //    bool result = false;
        //    FailedLoginReason failedReason;
        //    string encryptedPassword  = string.Empty;
        //    int tempUserId;

        //    //Ensure that the password is not too lengthy.
        //    //The encrypted password is always lengthy and we can ensure that the BL is the one 
        //    //that accepts a clear text password and encrypts it.
        //    if (ShiptalkCommon.PasswordUtil.VerifyClearTextPasswordLength(ClearTextPassword))
        //        //Encrypt the password.
        //        encryptedPassword = GetEncryptedPassword(ClearTextPassword);
        //    else
        //        throw new ShiptalkException("The password does not meet password length requirements.", false);

        //    if (string.IsNullOrEmpty(encryptedPassword))
        //        throw new ShiptalkException("Unable to encrypt password", true, "Error in authentication process. Please contact support for assistance.");
        //    else
        //        result = UserDAL.AuthenticateUser(Username, encryptedPassword, out tempUserId, out failedReason);


        //    UserId = tempUserId;
        //    failedLoginReasonObj = failedReason;

        //    return result;

        //}
        public static bool AuthenticateUser(string Username, string ClearTextPassword, out int UserId, out string ErrorMessage)
        {

            bool result = false;
            string tempErrorMessage;
            string encryptedPassword = string.Empty;
            int tempUserId;

            //Ensure that the password is not too lengthy.
            //The encrypted password is always lengthy and we can ensure that the BL is the one 
            //that accepts a clear text password and encrypts it.
            if (ShiptalkCommon.PasswordUtil.VerifyClearTextPasswordLength(ClearTextPassword))
                //Encrypt the password.
                encryptedPassword = GetEncryptedPassword(ClearTextPassword);
            else
                throw new ShiptalkException("The password does not meet password length requirements.", false);

            if (string.IsNullOrEmpty(encryptedPassword))
                throw new ShiptalkException("Unable to encrypt password", true, "Error in authentication process. Please contact support for assistance.");
            else
                //result = UserDAL.AuthenticateUser(Username, ClearTextPassword, out tempUserId, out tempErrorMessage);
                result = UserDAL.AuthenticateUser(Username, encryptedPassword, out tempUserId, out tempErrorMessage);

            UserId = tempUserId;
            ErrorMessage = tempErrorMessage;

            return result;

        }

        public static bool UpdateUserSessionToken(int UserId, Guid? SessionToken) 
        {
            return UserDAL.UpdateUserSessionToken(UserId, SessionToken);
        }

        private static string GetEncryptedPassword(string clearTextPassword)
        {
            Encryptor enc = EncryptorFactory.CreateEncryptor(EncryptionType.TripleDES);
            return enc.Encrypt(clearTextPassword);
        }


        /// <summary>
        /// Called when an Admin needs to create a new User account for the persons Jurisdiction. 
        /// </summary>
        /// <param name="profileObj">UserProfile</param>
        /// <param name="OldUserId">int?</param>
        /// <param name="IsRegistrationRequest">bool</param>
        /// <param name="RequestedAgencyId">int</param>
        /// <param name="CreatedBy">int?</param>
        /// <returns>bool</returns>
        /// <returns>out int? UserId</returns>
        //private static bool CreateUser(UserRegistration regObj, bool IsRegistrationRequest, int? CreatedBy, out int? UserId)
        //{
        //    regObj.Password = GetEncryptedPassword(regObj.Password);
        //    return UserDAL.CreateUser(regObj, IsRegistrationRequest, CreatedBy, out UserId);
        //}


        /// <summary>
        ///     Requests for ErrorMessage by both Old SHIPtalk Users who login with old accounts
        ///     as well as Anonymous requests for User account.
        /// </summary>
        /// <param name="profileObj">UserProfile</param>
        /// <param name="OldUserId">int?</param>
        /// <param name="RequestedAgencyId">int</param>
        /// <returns>bool</returns>
        /// <returns>out int? UserId</returns>
        public static bool RegisterUser(UserRegistration regObj, out int? UserId)
        {
            regObj.Password = GetEncryptedPassword(regObj.Password);
            return UserDAL.CreateUser(regObj, out UserId);
        }



        /// <summary>
        /// Returns UserProfile. The AccountInfo will be retreived if ReturnAccountInfo is set to true.
        /// Will throw an exception if all the required data cannot be retrieved.
        /// </summary>
        /// <param name="UserId">int</param>
        /// <param name="ReturnAccountInfo">bool</param>
        /// <returns>UserProfile</returns>
        public static UserProfile GetUserProfile(int UserId)
        {
            //Fill UserProfile here.
            return UserDAL.GetUserProfile(UserId);
        }


        /// <summary>
        /// Returns UserAccount information.
        /// </summary>
        /// <param name="UserId">int</param>
        /// <returns>UserAccount</returns>
        public static UserAccount GetUserAccount(int UserId)
        {
            return UserDAL.GetUserAccount(UserId);
        }



        /// <summary>
        /// Update a User's Profile. The User account part of the Profile can be included or excluded for updation.
        /// </summary>
        /// <param name="profileObj">UserProfile</param>
        /// <param name="UpdatedBy">int</param>
        /// <param name="UpdateAccountInfo">bool</param>
        /// <returns></returns>
        public static bool UpdateUserProfile(UserProfile profileObj, int UpdatedBy)
        {
            return UserDAL.UpdateUserProfile(profileObj, UpdatedBy);

        }



        /// <summary>
        /// Update a User's account.
        /// </summary>
        /// <param name="userAcctObj">UserAccount</param>
        /// <param name="UpdatedBy">int</param>
        /// <returns>bool</returns>
        public static bool UpdateUserAccount(UserAccount userAcctObj, int UpdatedBy)
        {
            return UserDAL.UpdateUserAccount(userAcctObj, UpdatedBy);
        }



        /// <summary>
        /// Set User status to Active.
        /// </summary>
        /// <param name="UserId">int</param>
        /// <param name="UpdatedBy">int</param>
        /// <returns>bool</returns>
        public static bool ActivateUser(int UserId, int UpdatedBy)
        {
            return UserDAL.SetUserAccountStatus(UserId, true, UpdatedBy);
        }

        /// <summary>
        /// Search Users who are accessible to a User.
        /// Returns Search results in a summary View.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IEnumerable<UserSearchViewData> SearchUsers(UserSearchSimpleParams param)
        {
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserSearchViewData>>(UserDAL.SearchUsers(param));

        }

        public static IEnumerable<UserSearchViewData> SearchUsersFor180dInactivity(UserSearchSimpleParams param)
        {
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserSearchViewData>>(UserDAL.SearchUsersFor180dInactivity(param));

        }


        public static IEnumerable<UserSearchViewData> GetUsersFor180dInactivity(int usrId)
        {
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserSearchViewData>>(UserDAL.GetUsersFor180dInactivity(usrId));

        }


        

        /// <summary>
        /// For only users of State/CMS Scope, the User table will be updated for the IsApproverDesignate status.
        /// For all other users, an exception will be raised at the database level.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="IsApproverDesignate"></param>
        /// <param name="UpdatedBy"></param>
        /// <returns></returns>
        public static bool UpdateApproverDesignate(int UserId, bool IsApproverDesignate, int UpdatedBy)
        {
            return UserDAL.UpdateApproverDesignate(UserId, IsApproverDesignate, UpdatedBy);
        }


        public static bool UpdateStateSuperDataEditor(int UserId, bool IsSuperDataEditor, int UpdatedBy)
        {
            return UserDAL.UpdateStateSuperDataEditor(UserId, IsSuperDataEditor, UpdatedBy);
        }

        /// <summary>
        /// Gets list of accessible Users.
        /// Specify StateFIPS for CMS Users to filter results by that State.
        /// </summary>
        /// <param name="AdminUserId"></param>
        /// <param name="filterByStateFIPS"></param>
        /// <returns></returns>
        public static IEnumerable<UserSearchViewData> GetAllUsers(int RequestingUserId, string filterByStateFIPS)
        {
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserSearchViewData>>(UserDAL.GetAllUsers(RequestingUserId, filterByStateFIPS));
        }

        public static IEnumerable<UserSearchViewData> GetAllInactiveUsers(int RequestingUserId, string filterByStateFIPS)
        {
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserSearchViewData>>(UserDAL.GetAllInactiveUsers(RequestingUserId, filterByStateFIPS));
        }
        

        //private static UserRegionalAccessProfile MakeRegionalProfile(string RegionName)
        //{
        //    UserRegionalAccessProfile profile = new UserRegionalAccessProfile();
        //    profile.RegionName = RegionName;
        //    return profile;
        //}


        /// <summary>
        /// Set User status to InActive.
        /// </summary>
        /// <param name="UserId">int</param>
        /// <param name="UpdatedBy">int</param>
        /// <returns>bool</returns>
        public static bool DeActivateUser(int UserId, int UpdatedBy)
        {
            return UserDAL.SetUserAccountStatus(UserId, false, UpdatedBy);
        }

        /// <summary>
        /// Approve a registered User
        /// </summary>
        /// <param name="UserId">int</param>
        /// <param name="UpdatedBy">int</param>
        /// <returns>bool</returns>
        public static bool ApproveUser(int UserId, int ApprovedBy)
        {
            return UserDAL.ApproveUser(UserId, ApprovedBy);
        }



        /// <summary>
        /// ActivateUserWith180DaysInacitvity
        /// </summary>
        /// <param name="UserId">int</param>
        /// <param name="UpdatedBy">int</param>
        /// <returns>bool</returns>
        public static bool ActivateUserWith180DaysInacitvity(int UserId, int ApprovedBy)
        {
            return UserDAL.ActivateUserWith180DaysInacitvity(UserId, ApprovedBy);
        }

        /// <summary>
        /// Add one or more Descriptors for a User.
        /// If Agency is not passed, default Agency ID of 0 will be used [AgencyID 0 means, non-Agency level User]
        /// For non-Agency Users, the AgencyID must be 0 or null.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserDescriptorList"></param>
        /// <param name="AgencyId"></param>
        /// <param name="AddedBy"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public static bool AddDescriptorsForUser(int UserId, IEnumerable<int> UserDescriptorList, int? AgencyId, int AddedBy, out string ErrorMessage)
        {
            string message = string.Empty;
            bool result = true;

            int _AgencyID = AgencyId.HasValue ? AgencyId.Value : Constants.Defaults.DefaultValues.AgencyIdForNonAgencyUsers;
            foreach (int DescriptorID in UserDescriptorList)
            {
                int outUserDescriptorID;
                if (!UserDAL.AddDescriptorForUser(UserId, DescriptorID, _AgencyID, AddedBy, out outUserDescriptorID))
                    result = false;
            }
            if (result == false)
            {
                if (UserDescriptorList.Count() > 1)
                    message = "Unable to add one or more descriptors for the user.";
                else
                    message = "Unable to add the descriptor for user.";
            }
            ErrorMessage = message;
            return result;
        }



        /// <summary>
        /// Delete one or more descriptors for a User within an Agency.
        /// To delete descriptors in multiple agencies, this method must be called multiple times.
        /// </summary>
        /// <param name="UserDescriptorIDList">List</param>
        /// <param name="ErrorMessage">out string</param>
        /// <returns>bool</returns>
        public static bool DeleteDescriptorsForUser(int UserId, IEnumerable<int> UserDescriptorList, int? AgencyId, out string ErrorMessage)
        {
            string message = string.Empty;
            bool result = true;

            int _AgencyID = AgencyId.HasValue ? AgencyId.Value : Constants.Defaults.DefaultValues.AgencyIdForNonAgencyUsers;
            foreach (int DescriptorID in UserDescriptorList)
            {
                if (!UserDAL.DeleteDescriptorForUser(UserId, DescriptorID, _AgencyID))
                    result = false;
            }
            if (result == false)
            {
                if (UserDescriptorList.Count() > 1)
                    message = "Failed to remove one or more descriptors for the User.";
                else
                    message = "Failed to remove descriptor for the User.";
            }
            ErrorMessage = message;
            return result;
        }


        //Save DescriptorId, AgencyId for User.
        public static bool SaveDescriptors(int UserId, IEnumerable<int> NewDescriptorIds, int AgencyId, int UpdatedBy, out string ErrorMessage)
        {
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            //{
                //If the User in context is not Supervisor/Editor then remove all references where the User was assigned as 
                //a Supervisor/Editor for other users. In other words, the Users for whom the current User was a Supervisor
                //will not be supervisor anymore.
                if (!NewDescriptorIds.Contains(Descriptor.DataEditor_Reviewer.EnumValue<int>()))
                {
                    //Removes Supervisor/Reviewer/Editor access and all Users for whom the person is Supervisor/Reviewer/Editor
                    UserDAL.DeleteDescriptorForUser(UserId, Descriptor.DataEditor_Reviewer.EnumValue<int>(), AgencyId);
                }

                //Delete old descriptors
                if (!DeleteAllDescriptorsForUser(UserId, AgencyId, out ErrorMessage))
                {
                    return false;
                }

                //Add all new descriptors
                if (NewDescriptorIds != null && NewDescriptorIds.Count() > 0)
                {
                    if (!AddDescriptorsForUser(UserId, NewDescriptorIds, (int?)AgencyId, UpdatedBy, out ErrorMessage))
                        return false;
                }

                //scope.Complete();

                ErrorMessage = string.Empty;
                return true;
            //}
        }


        /// <summary>
        /// Save Supervisors(Reviewers) for User
        /// For State User, pass null for AgencyId
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="NewReviewerIds"></param>
        /// <param name="AgencyId"></param>
        /// <param name="UpdatedBy"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public static bool SaveSupervisorForUser(int UserId, int SupervisorId, int? UserRegionId, int UpdatedBy)
        {
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            //{
                //Delete old Supervisors
                DeleteAllSupervisorsForUser(UserId, UserRegionId);

                bool IsSuccess = true;
                if (SupervisorId > 0)
                {
                    //Add new Supervisor; Future will support multiple supervisors.
                    IsSuccess = AddSupervisorForUser(UserId, SupervisorId, UserRegionId, UpdatedBy);
                }

                //if (IsSuccess)
                //    scope.Complete();

                return IsSuccess;
            //}
        }



        /// <summary>
        /// Delete all Supervisors (Reviewers) for User
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="AgencyId"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public static void DeleteAllSupervisorsForUser(int UserId, int? UserRegionId)
        {
            UserDAL.DeleteAllReviewersForUser(UserId, UserRegionId);
        }


        /// <summary>
        /// Add a Supervisor for a User. Use UserRegionId for Agency/SubStateID
        /// For State Users pass null for UserRegionId
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ReviewerId"></param>
        /// <param name="UserRegionId"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public static bool AddSupervisorForUser(int UserId, int ReviewerId, int? UserRegionId, int CreatedBy)
        {
            return UserDAL.AddReviewerForUser(UserId, ReviewerId, UserRegionId, CreatedBy);
        }



        /// <summary>
        /// Delete all for a User within an Agency [For non-Agency Users, AgencyID is 0.
        /// To delete descriptors in multiple agencies, this method must be called multiple times.
        /// </summary>
        /// <param name="UserDescriptorIDList">List</param>
        /// <param name="ErrorMessage">out string</param>
        /// <returns>bool</returns>
        public static bool DeleteAllDescriptorsForUser(int UserId, int? AgencyId, out string ErrorMessage)
        {
            string message = string.Empty;
            bool result = true;

            int _AgencyID = AgencyId.HasValue ? AgencyId.Value : Constants.Defaults.DefaultValues.AgencyIdForNonAgencyUsers;
            UserDAL.DeleteAllDescriptorsForUser(UserId, _AgencyID);

            if (result == false)
            {
                message = "Failed to remove one or more descriptors for the User.";
            }
            ErrorMessage = message;
            return result;
        }






        /// <summary>
        /// Returns complete User profile of a User who is pending approval. The profile includes Account info, Personal profile and Regional profiles (agencies, sub state regions etc).
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        //public static UserViewData GetPendingUser(int UserId)
        //{
        //    User userObj = new User();
        //    userObj.UserAccount = UserBLL.GetUserAccount(UserId);
        //    if (userObj.UserAccount != null)
        //    {
        //        userObj.UserProfile = UserBLL.GetUserProfile(UserId);

        //        if (userObj.UserAccount.Scope.CompareTo(Scope.Agency, ComparisonCriteria.IsEqual))
        //            userObj.UserRegionalProfiles = new List<UserRegionalAccessProfile>(UserAgencyBLL.GetUserAgencyProfiles(UserId, false));
        //        else if (userObj.UserAccount.Scope.CompareTo(Scope.SubStateRegion, ComparisonCriteria.IsEqual))
        //            userObj.UserRegionalProfiles = new List<UserRegionalAccessProfile>(UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(UserId, false));
        //        else if (userObj.UserAccount.Scope.CompareTo(Scope.CMSRegional, ComparisonCriteria.IsEqual))
        //            userObj.UserRegionalProfiles = new List<UserRegionalAccessProfile>(UserCMSBLL.GetUserCMSRegionalProfiles(UserId, false));
        //    }
        //    else
        //        return null;

        //    UserViewData viewData = Mapper.Map<User, UserViewData>(userObj);

        //    StepUpViewData(ref viewData);
        //    return viewData;

        //}


        /// <summary>
        /// Returns complete User profile that includes Account info, Personal profile and Regional profiles (agencies, sub state regions etc).
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static UserViewData GetUser(int UserId)
        {
            User userObj = new User();
            userObj.UserAccount = UserBLL.GetUserAccount(UserId);
            if (userObj.UserAccount != null && (userObj.UserAccount.UserId > 0))
            {
                userObj.UserProfile = UserBLL.GetUserProfile(UserId);                

                if (userObj.UserAccount.Scope.IsEqual(Scope.Agency))
                    userObj.UserRegionalProfiles = new List<UserRegionalAccessProfile>(UserAgencyBLL.GetUserAgencyProfiles(UserId, false));
                else if (userObj.UserAccount.Scope.IsEqual(Scope.SubStateRegion))
                    userObj.UserRegionalProfiles = new List<UserRegionalAccessProfile>(UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(UserId, false));
                else if (userObj.UserAccount.Scope.IsEqual(Scope.CMSRegional))
                    userObj.UserRegionalProfiles = new List<UserRegionalAccessProfile>(UserCMSBLL.GetUserCMSRegionalProfiles(UserId, false));
                else
                    userObj.UserRegionalProfiles = new List<UserRegionalAccessProfile>();
            }
            else
                return null;

            UserViewData viewData = Mapper.Map<User, UserViewData>(userObj);
            StepUpViewData(ref viewData);

            return viewData;

        }

        /// <summary>
        /// To avoid the Business Object refer back to UserBLL, we would like to do the processing here,
        /// rather than User.cs where mapping is done.
        /// </summary>
        /// <param name="viewData"></param>
        /// <param name="accountInfo"></param>
        private static void StepUpViewData(ref UserViewData viewData)
        {
            string description = string.Empty;
            string title = string.Empty;
            //bool IsShipDirector = false;
            //Get the Role that match user account information

            Role r = LookupBLL.GetRole(viewData.Scope, viewData.IsAdmin);
            title = r.Name;

            //Set special description for Ship Director.
            if (r.IsStateAdmin)
            {
                //IsShipDirector = LookupBLL.IsShipDirector(viewData.UserId, viewData.StateFIPS);
                if (viewData.IsShipDirector)
                {
                    description = "State SHIP Director";
                    title = "Ship Director";
                }
            }

            //Set descriptors here for State/CMS. Other Users have it in their UserRegionalProfiles.
            if (r.scope.IsEqual(Scope.State) || r.scope.IsEqual(Scope.CMS))
            {
                viewData.DescriptorIds = GetDescriptorsForUser(viewData.UserId, Constants.Defaults.DefaultValues.AgencyIdForNonAgencyUsers);
            }

            //Add Supervisor data for State and CMS level users.
            if (viewData.IsUserStateScope)
            {
                var reviewers = UserBLL.GetReviewersForUser(viewData.UserId, null);
                if (reviewers != null && reviewers.Count() > 0)
                {
                    viewData.SupervisorIdForStateUser = reviewers.First().Key;
                    viewData.SupervisorNameForStateUser = reviewers.First().Value;
                }

            }

            description = (description == string.Empty) ? r.Description : description;


            if (viewData.RegionalProfiles != null)
            {
                viewData.RegionalProfiles.ForEach(prof => prof.RegionScope = (Scope?)r.scope);
            }


            viewData.RoleDescription = description;
            viewData.RoleTitle = title;
            //viewData.IsShipDirector = IsShipDirector;
        }


        /// <summary>
        /// Called primarily for State and CMS Users. CMSRegion/SubStateRegion/Agency have their descriptors as part of 
        /// their individual regions. For example: A User with multiple Agency or sub state profile will have
        /// the descriptors corresponding to each Agency or Sub state as part of the agency or sub state profile.
        /// For non-Agency Users, set AgencyId to zero. This is per DB settings.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="AgencyId"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetDescriptorsForUser(int UserId, int? AgencyId)
        {
            return UserDAL.GetDescriptorsForUser(UserId, AgencyId);
        }



        /// <summary>
        /// Pass in the UserId, then UserRegionId as explained below:
        /// UserRegionId: For CMS/State users pass null for UserRegionId
        /// UserRegionId: For persons with more than one regions such as Agency or SubState or even CMS Regional, pass in their AgencyId, Sub StateID etc.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserRegionId"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<int, string>> GetReviewersForUser(int UserId, int? UserRegionId)
        {
            return UserDAL.GetReviewersForUser(UserId, UserRegionId);
        }


        /// <summary>
        /// Returns the UserUniqueId object
        /// </summary>
        /// <param name="UserId"></param>
        public static UserUniqueID GetUniqueIDForUser(int UserId)
        {
            return UserDAL.GetUniqueIdForUser(UserId);
        }


        /// <summary>
        /// Generates a Unique Id which is a combination of StateFIPS and a Random number of 6 digits.
        /// </summary>
        /// <param name="UserId"></param>
        public static string GenerateMedicareUniqueIDForUser(int UserId, int ApproverUserID)
        {
            return UserDAL.GenerateMedicareUniqueID(UserId, ApproverUserID);
        }


        public static bool DoesMedicareUniqueIdExist(int UserId)
        {
            return UserDAL.DoesMedicareUniqueIdExist(UserId);
        }

        

        /// <summary>
        /// Generates a Unique Id which is a combination of StateFIPS and a Random number of 6 digits.
        /// </summary>
        /// <param name="AccountInfo"></param>
        /// <returns></returns>
        public static string GenerateUniqueIDForUser(UserAccount AccountInfo)
        {

            throw new NotImplementedException("Not implemented");
            //Generate one using StateFIPS PLUS random number of 6 characters
            //string StateFIPS = AccountInfo.StateFIPS;

            //Random gnr = new Random();
            //string newUniqueID = GenerateUniqueID(ref gnr);

            //while (!UniqueIDList.Contains(newUniqueID))
            //{
            //    newUniqueID = GenerateUniqueID(ref gnr);
            //}

            //return StateFIPS + newUniqueID;
        }

        private string GenerateUniqueID(ref Random gnr)
        {
            string randomNo = gnr.Next().ToString();
            if (randomNo.Length > 6)
                return randomNo.Substring(0, 6);
            else
                return randomNo.PadLeft(6, '0');
        }


        public static bool SaveUniqueIdForUser(int UserId, string UniqueId, int AddedBy)
        {
            return UserDAL.SaveUniqueIdFromUser(UserId, UniqueId, AddedBy);
        }

        /// <summary>
        /// Returns the active AgencyId, AgencyName enumeration for User
        /// </summary>
        /// <param name="UserId"></param>
        public static IEnumerable<KeyValuePair<int, string>> GetAllAgenciesForUser(int UserId, Scope scope, string StateFIPS)
        {
            return UserDAL.GetAllAgenciesForUser(UserId, scope.EnumValue<int>(), StateFIPS);
        }

        public static bool DeleteUserUniqueIDRequest(int UserId)
        {
            return UserDAL.DeleteUserUniqueIDRequest(UserId);
        }


        public static bool RevokeMedicareUniqueIdForUser(int UserId)
        {
            return UserDAL.RevokeMedicareUniqueIdForUser(UserId);
        }


        public static bool InvokeMedicareUniqueIdForUser(int UserId)
        {
            return UserDAL.InvokeMedicareUniqueIdForUser(UserId);
        }

        
        public static bool HandleUniqueIDRequest(int UserId, out string ErrorMessage)
        {
            string error = string.Empty;
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
                if (UserDAL.AddUniqueIdRequestFromUser(UserId))
                {
                    try
                    {
                        EmailNotifyUniqueIDApprovers(UserId);
                        //scope.Complete();
                    }
                    catch(Exception mailEx)
                    {
                        ErrorMessage = "Unable to notify approvers. Unique ID cannot be requested at this time.";
                        return false;
                    }
                }
                else
                {
                    ErrorMessage = "You have already requested Unique ID. Duplicates not allowed.";
                    return false;
                }
            //}
            ErrorMessage = error;
            return true;
        }


        private static bool EmailNotifyUniqueIDApprovers(int RequestedUserId)
        {
            //Prepare Mail Object
            ShiptalkMailMessage mailMessage = new ShiptalkMailMessage(true, ShiptalkMailMessage.MailFrom.ShiptalkResourceCenter);
            UserViewData userData = UserBLL.GetUser(RequestedUserId);

            var Approvers = GetApproversForUser(RequestedUserId);
            foreach (var approver in Approvers) mailMessage.ToList.Add(approver.Value);

            mailMessage.Subject = "New CMS SHIP Unique ID request";

            //PREPARE BODY OF EMAIL
            StringBuilder sbMailBody = new StringBuilder();
            sbMailBody.Append("A SHIP User has requested a new CMS SHIP Unique ID. Please login to <a href='https://shipnpr.shiptalk.org'>https://shipnpr.shiptalk.org</a> to respond the request.");
            sbMailBody.Append(" Go to the 'User' screen and click on 'Review, Approve, Deny CMS SHIP Unique ID Requests' to view and approve/deny this request.");
            sbMailBody.AddNewHtmlLines(3);
            sbMailBody.Append("A brief snapshot of the user profile is provided below:");
            sbMailBody.AddNewHtmlLines(2);
            sbMailBody.AppendFormat("Name: {0}{1} {2}", userData.FirstName, 
                                        string.IsNullOrEmpty(userData.MiddleName) ? string.Empty : " " + userData.MiddleName , 
                                        userData.LastName);
            sbMailBody.AddNewHtmlLine();
            sbMailBody.AppendFormat("Primary Email: {0}", userData.PrimaryEmail);
            sbMailBody.AddNewHtmlLine();
            sbMailBody.AppendFormat("Primary Phone: {0}", userData.PrimaryPhone);
            sbMailBody.AddNewHtmlLines(3);
            mailMessage.Body = sbMailBody.ToString();

            //Send Mail here
            ShiptalkMail mail = new ShiptalkMail(mailMessage);
            return mail.SendMail();
        }


        /// <summary>
        /// Change Password for all types of scenarios. Refer to overloads and choose the best one.
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <param name="OldPassword">OldPassword</param>
        /// <param name="NewPassword">NewPassword</param>
        /// <param name="ResetBy">Admin User who is resetting the password</param>
        /// <returns>bool</returns>
        /// <returns>out string ErrorMessage</returns>
        public static bool ChangePassword(int UserId, string OldPassword, string NewPassword, int? ResetBy, out string ErrorMessage)
        {
            return UserDAL.ChangePassword(UserId, OldPassword, GetEncryptedPassword(NewPassword), NewPassword, ResetBy, out ErrorMessage);
        }
        /// <summary>
        /// Change email for all types of scenarios. Refer to overloads and choose the best one.
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <param name="OldPassword">OldMail</param>
        /// <param name="NewPassword">NewMail</param>
        /// <param name="ResetBy">Admin User who is resetting the Mail</param>
        /// <returns>bool</returns>
        /// <returns>out string ErrorMessage</returns>
        public static bool ChangeEmail(int UserId, string oldemail, string newemail, int? ResetBy, out string ErrorMessage)
        {
            return UserDAL.ChangeEmail(UserId, oldemail, newemail, ResetBy, out ErrorMessage);
        }

        /// <summary>
        /// The method passes null value for Old Password.
        /// If an Admin wants to reset password, the method will be useful. The NewPassword will be the newly
        /// generated password and the ResetBy will be the Admin's ID.
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <param name="OldPassword">OldPassword</param>
        /// <param name="NewPassword">NewPassword</param>
        /// <param name="ResetBy">Admin User who is resetting the password</param>
        /// <returns>bool</returns>
        /// <returns>out string ErrorMessage</returns>
        public static bool ChangePassword(int UserId, string NewPassword, int? ResetBy, out string ErrorMessage)
        {
            //Old Password is set to null
            return ChangePassword(UserId, null, NewPassword, ResetBy, out ErrorMessage);
        }


        /// <summary>
        /// The method internally uses UserId for ResetBy.
        /// This method is useful when the users change their own passwords. Typically, users who have logged in,
        /// would like to change their new password.
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <param name="NewPassword">NewPassword</param>
        /// <returns>bool</returns>
        /// <returns>out string ErrorMessage</returns>
        public static bool ChangePassword(int UserId, string NewPassword, out string ErrorMessage)
        {
            return ChangePassword(UserId, NewPassword, UserId, out ErrorMessage);
        }

       

        /// <summary>
        /// The method internally uses UserId for ResetBy.
        /// This method is useful when the users change their own passwords and the system requires them to
        /// type their Old password as well. This could be useful when a User is resetting their account
        /// using a temp password being emailed out. In that case, the old password would be the temp password.
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <param name="OldPassword">OldPassword</param>
        /// <param name="NewPassword">NewPassword</param>
        /// <returns>bool</returns>
        /// <returns>out string ErrorMessage</returns>
        public static bool ChangePassword(int UserId, string OldPassword, string NewPassword, out string ErrorMessage)
        {
            return ChangePassword(UserId, OldPassword, NewPassword, out ErrorMessage);
        }

        public static bool ChangeEmail(int UserId, string OldPassword, string NewPassword, out string ErrorMessage)
        {
            return ChangeEmail(UserId, OldPassword, NewPassword, UserId, out ErrorMessage);
        }

        public static Guid GetEmailVerificationTokenForUser(int UserId)
        {
            return UserDAL.GetEmailVerificationTokenForUser(UserId);
        }


        public static int GetUserId()
        {
            return UserDAL.GetUserIdForUserName(HttpContext.Current.User.Identity.Name).GetValueOrDefault(0);
        }

        public static int? GetUserIdForUserName(string UserName)
        {
            return UserDAL.GetUserIdForUserName(UserName);
        }
        
        public static LastLoginInfo GetLastLoginInfo(string UserName)
        {
            return UserDAL.GetLastLoginDetailsForUser(UserName);
        }


        public static Guid GetPasswordResetVerificationTokenForUser(string UserName)
        {
            return UserDAL.GetPasswordResetVerificationTokenForUser(UserName);
        }

        public static bool IsAccountCreatedViaUserRegistration(string UserName)
        {
            return UserDAL.IsAccountCreatedViaUserRegistration(UserName);
        }


        public static bool IsPasswordResetTokenValid(Guid Token, string UserName)
        {
            return UserDAL.IsPasswordResetTokenValid(Token, UserName);
        }

        public static bool DeleteUser(int UserId, out string FailureReason)
        {
            return UserDAL.DeleteUserRegistration(UserId, out FailureReason);
        }

        public static bool UnlockUser(int UserId, out string FailureReason)
        {
            return UserDAL.UnlockUserRegistration(UserId, out FailureReason);
        }


        public static IEnumerable<KeyValuePair<int, string>> GetApproversForUser(int UserId)
        {
            return UserDAL.GetApproversForUser(UserId);
        }

        public static void UpdateIsAdminStatus(int UserId, bool IsAdmin)
        {
            UserDAL.UpdateUserIsAdminStatus(UserId, IsAdmin);
        }

        public static bool IsEmailVerificationTokenValid(Guid Token, string UserName)
        {
            return UserDAL.IsEmailVerificationTokenValid(Token, UserName);
        }

        public static bool ResetEmailChangeRequestDate(int UserId, string TempPrimaryEmail)
        {
            return UserDAL.ResetEmailChangeRequestDate(UserId, TempPrimaryEmail);
        }

       
    }
}
