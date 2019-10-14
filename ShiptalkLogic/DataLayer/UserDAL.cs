using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;

using System.Data.Common;
using System.Data;

using ShiptalkLogic.BusinessObjects;

namespace ShiptalkLogic.DataLayer
{




    /// <summary>
    /// The Data Access Layer class provides operations on User specific information.
    /// </summary>
    internal static class UserDAL
    {


        /// <summary>
        /// Authenticate User. If Login fails - Get Error message related to the failure.
        /// Boolean true is returned by AuthenticateUser for Successful Login; Boolean False for Failure.
        /// The input password must be an encrypted password.
        /// 
        /// </summary>
        /// <param name="Username">string</param>
        /// <param name="EncryptedPassword">string</param>
        /// <param name="UserId">output parameter int</param>
        /// <param name="ErrorMessage">output parameter string</param>
        /// <returns>bool indicating true or false; Also sets ErrorMessage if return value is false.</returns>
        public static bool AuthenticateUser(string Username, string EncryptedPassword, out int UserId, out string ErrorMessage)
        {
            int tempUserId = int.MinValue;
            string tempErrorMessage = null;

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.AuthenticateUser.Description()))
            {
                db.AddInParameter(dbCmd, "@LoginID", DbType.String, Username);
                db.AddInParameter(dbCmd, "@Password", DbType.String, EncryptedPassword);

                db.AddOutParameter(dbCmd, "@UserID", DbType.Int32, 4);
                db.AddOutParameter(dbCmd, "@FailureReason", DbType.String, 250);

                db.ExecuteNonQuery(dbCmd);

                if (db.GetParameterValue(dbCmd, "@UserID") != null)
                {
                    if (db.GetParameterValue(dbCmd, "@UserID") != DBNull.Value)
                        tempUserId = (int)db.GetParameterValue(dbCmd, "@UserID");
                }

                if (db.GetParameterValue(dbCmd, "@FailureReason") != null)
                    if (db.GetParameterValue(dbCmd, "@FailureReason") != DBNull.Value)
                        tempErrorMessage = Convert.ToString(db.GetParameterValue(dbCmd, "@FailureReason"));
            }

            UserId = tempUserId;
            ErrorMessage = tempErrorMessage;

            return string.IsNullOrEmpty(ErrorMessage);

        }

        /// <summary>
        /// Updates the session token for the current user, thus making the user logged in/out
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="SessionToken"></param>
        /// <returns></returns>
        public static bool UpdateUserSessionToken(int UserId, Guid? SessionToken)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.UpdateUserSessionToken.Description()))
            {
                db.AddInParameter(dbCmd, "@UserID", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@SessionToken", DbType.Guid, SessionToken);
               
                int recsAffected = db.ExecuteNonQuery(dbCmd);
                return recsAffected > 0;
            }
        }


        /// <summary>
        /// Called when an 'Admin' needs to create a new 'User' account for the Admin's Jurisdiction. 
        /// </summary>
        /// <param name="profileObj">UserProfile</param>
        /// <param name="OldUserId">int?</param>
        /// <param name="IsRegistrationRequest">bool</param>
        /// <param name="CreatedBy">int?</param>
        /// <returns>bool</returns>
        /// <returns>out int? UserId</returns>
        public static bool CreateUser(UserRegistration regObj, out int? UserId)
        {
            bool result = false;
            int? outparam = null;
            int? OldUserId = regObj.OldShipUserId;
            bool IsRegistrationRequest = regObj.IsRegistrationRequest;
            int? CreatedBy = regObj.RegisteredByUserId;

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.CreateUser.Description()))
            {
                db.AddInParameter(dbCmd, "@OldUserID", DbType.Int32, (OldUserId.HasValue ? OldUserId.Value : OldUserId));
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.String, regObj.StateFIPS);
                db.AddInParameter(dbCmd, "@ScopeID", DbType.Int16, regObj.RoleRequested.ScopeId);
                //db.AddInParameter(dbCmd, "@CounselingLocation", DbType.String, regObj.UserAccount.CounselingLocation);
                db.AddInParameter(dbCmd, "@IsAdmin", DbType.Boolean, regObj.RoleRequested.IsAdmin);
                db.AddInParameter(dbCmd, "@ClearPassword", DbType.String, regObj.ClearPassword); //sammit
                db.AddInParameter(dbCmd, "@Password", DbType.String, regObj.Password);
                db.AddInParameter(dbCmd, "@FirstName", DbType.String, regObj.FirstName);
                db.AddInParameter(dbCmd, "@MiddleName", DbType.String, regObj.MiddleName);
                db.AddInParameter(dbCmd, "@LastName", DbType.String, regObj.LastName);
                db.AddInParameter(dbCmd, "@Nickname", DbType.String, regObj.NickName);
                db.AddInParameter(dbCmd, "@Suffix", DbType.String, regObj.Suffix);
                db.AddInParameter(dbCmd, "@Honorifics", DbType.String, regObj.Honorifics);
                db.AddInParameter(dbCmd, "@PrimaryPhone", DbType.String, regObj.PrimaryPhone);
                db.AddInParameter(dbCmd, "@SecondaryPhone", DbType.String, regObj.SecondaryPhone);
                db.AddInParameter(dbCmd, "@PrimaryEmail", DbType.String, regObj.PrimaryEmail);
                db.AddInParameter(dbCmd, "@SecondaryEmail", DbType.String, regObj.SecondaryEmail);
                db.AddInParameter(dbCmd, "@IsRegistrationRequest", DbType.Boolean, IsRegistrationRequest);
                db.AddInParameter(dbCmd, "@CreatedBy", DbType.Int32, (CreatedBy.HasValue ? CreatedBy.Value : CreatedBy));
                db.AddInParameter(dbCmd, "@IsApproverDesignate", DbType.Boolean, regObj.IsApproverDesignate);
                db.AddInParameter(dbCmd, "@IsStateSuperEditor", DbType.Boolean, regObj.IsStateSuperEditor);

                db.AddOutParameter(dbCmd, "@UserId", DbType.Int32, 4);

                int recsAffected = db.ExecuteNonQuery(dbCmd);
                if (recsAffected > 0)
                {
                    Object o = dbCmd.Parameters["@UserId"].Value;
                    if (o != null && o != DBNull.Value)
                    {
                        outparam = (int)o;
                        result = true;
                    }
                }
            }
            UserId = outparam;
            return result;
        }


        /// <summary>
        /// Registration of Anonymous Users at the web site.
        /// </summary>
        /// <param name="profileObj">UserProfile</param>
        /// <param name="OldUserId">int?</param>
        /// <param name="RequestedAgencyId">int</param>
        /// <param name="UserId">out int?</param>
        /// <returns>bool</returns>
        /// <returns>out int? UserId</returns>
        //public static bool RegisterUser(UserRegistration regObj, out int? UserId)
        //{
        //    //return CreateUser(profileObj, OldUserId, true, RequestedAgencyId, null);
        //    return CreateUser(regObj, true, null, out UserId);
        //}


        public static bool DeleteUserRegistration(int UserId, out string FailureReason)
        {
            bool result = false;
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.DeleteUserRegistration.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);

                db.AddOutParameter(dbCmd, "@IsSuccess", DbType.Boolean, 1);
                db.AddOutParameter(dbCmd, "@FailureReason", DbType.String, 500);

                //return (db.ExecuteNonQuery(dbCmd) > 0);
                db.ExecuteNonQuery(dbCmd);

                Object outObj = dbCmd.Parameters["@FailureReason"].Value;

                string failureMessage = string.Empty;
                if (outObj != null && outObj != DBNull.Value)
                    failureMessage = (string)outObj;
                else
                    result = true;
                
                //Finally set the output param value
                FailureReason = failureMessage;
            }
            return result;
        }

        //Prakash 11/13/2012 - UnlockUser , if the User gets Locked by Changing their Password more than once in 24 Hours..

        public static bool UnlockUserRegistration(int UserId, out string FailureReason)
        {
            bool result = false;
            Database db = DatabaseFactory.CreateDatabase("db_ship-npr");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.UnlockUserRegistration.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);

                db.AddOutParameter(dbCmd, "@IsSuccess", DbType.Boolean, 1);
                db.AddOutParameter(dbCmd, "@FailureReason", DbType.String, 500);

                //return (db.ExecuteNonQuery(dbCmd) > 0);
                db.ExecuteNonQuery(dbCmd);

                Object outObj = dbCmd.Parameters["@FailureReason"].Value;

                string failureMessage = string.Empty;
                if (outObj != null && outObj != DBNull.Value)
                    failureMessage = (string)outObj;
                else
                    result = true;

                //Finally set the output param value
                FailureReason = failureMessage;
            }
            return result;
        }

        public static bool GrantCMSRegionAccessToUser(int UserId, int CMSRegionId, int GrantedBy)
        {
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.UserAccess.GrantCMSRegionAccessToUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@CMSRegionID", DbType.Int32, CMSRegionId);
                db.AddInParameter(dbCmd, "@GrantedBy", DbType.Int32, GrantedBy);

                db.AddOutParameter(dbCmd, "@CMSRegionUserID", DbType.Int32, 4);

                if (db.ExecuteNonQuery(dbCmd) > 0)
                    result = true;
            }

            return result;

        }

        /// <summary>
        /// For Users of State and CMS Scope, the User table will be updated with the IsApproverDesignate status.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="CMSRegionId"></param>
        /// <param name="GrantedBy"></param>
        /// <returns></returns>
        public static bool UpdateApproverDesignate(int UserId, bool IsApproverDesignate, int UpdatedBy)
        {
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.UpdateApproverDesignate.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@IsApproverDesignate", DbType.Boolean, IsApproverDesignate);
                db.AddInParameter(dbCmd, "@UpdatedBy", DbType.Int32, UpdatedBy);

                if (db.ExecuteNonQuery(dbCmd) > 0)
                    result = true;
            }

            return result;

        }

        /// <summary>
        /// Update Super Data Editor rights for a User at State Scope 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="IsSuperDataEditor"></param>
        /// <param name="UpdatedBy"></param>
        /// <returns></returns>
        public static bool UpdateStateSuperDataEditor(int UserId, bool IsSuperDataEditor, int UpdatedBy)
        {
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.UpdateStateSuperDataEditor.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@IsStateSuperDataEditor", DbType.Boolean, IsSuperDataEditor);
                db.AddInParameter(dbCmd, "@UpdatedBy", DbType.Int32, UpdatedBy);

                if (db.ExecuteNonQuery(dbCmd) > 0)
                    result = true;
            }

            return result;

        }

        /// <summary>
        /// Returns UserProfile. The AccountInfo will be retreived if ReturnAccountInfo is set to true.
        /// Will throw an exception if all the required data cannot be retrieved.
        /// </summary>
        /// <param name="UserId">int</param>
        /// <param name="IncludeAccountInfo">bool</param>
        /// <returns>UserProfile</returns>
        public static UserProfile GetUserProfile(int UserId)
        {
            UserProfile usrProfile = null;

            //Fill UserProfile here.
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetUserProfile.Description()))
            {

                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@IncludeUserAccount", DbType.Boolean, false);

                DataSet ds = db.ExecuteDataSet(dbCmd);

                //First table has atleast 1 row.
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //Fill User Profile object
                    usrProfile = new UserProfile();
                    usrProfile.UserId = ds.Tables[0].Rows[0].Field<int>("UserId");
                    usrProfile.FirstName = ds.Tables[0].Rows[0].Field<string>("FirstName");
                    usrProfile.MiddleName = ds.Tables[0].Rows[0].Field<string>("MiddleName");
                    usrProfile.LastName = ds.Tables[0].Rows[0].Field<string>("LastName");
                    usrProfile.NickName = ds.Tables[0].Rows[0].Field<string>("Nickname");
                    usrProfile.Suffix = ds.Tables[0].Rows[0].Field<string>("Suffix");
                    usrProfile.Honorifics = ds.Tables[0].Rows[0].Field<string>("Honorifics");
                    usrProfile.SecondaryEmail = ds.Tables[0].Rows[0].Field<string>("SecondaryEmail");
                    usrProfile.PrimaryPhone = ds.Tables[0].Rows[0].Field<string>("PrimaryPhone");
                    usrProfile.SecondaryPhone = ds.Tables[0].Rows[0].Field<string>("SecondaryPhone");
                    usrProfile.TempPrimaryEmail = ds.Tables[0].Rows[0].Field<string>("TempPrimaryEmail");
                    usrProfile.EmailChangeRequestDate = ds.Tables[0].Rows[0].Field<DateTime?>("EmailChangeRequestDate");
                    usrProfile.IsActive = ds.Tables[0].Rows[0].Field<bool>("IsActive");
                    usrProfile.LastPasswordChangeDate = ds.Tables[0].Rows[0].Field<DateTime?>("LastPasswordChangeDate");
                }

                //Fill UserAccount info
                //Result set has more than one table.
                //if (ds.Tables.Count > 1)
                //{
                //    ds.Tables.RemoveAt(0);
                //    ds.AcceptChanges();
                //    usrProfile.UserAccount = FillUserAccount(ds);
                //}
            }
            return usrProfile;
        }




        //private static List<UserRegionalAccessProfile> GetUserRegionalAccessProfiles(int UserId)
        //{
        //    //If UserRegionalAccess table is present...
        //    if (ds.Tables.Count > 1)
        //    {
        //        DataTable tblRegAccess = ds.Tables[1];
        //        if (tblRegAccess.Rows.Count > 0)
        //            usrAcct.UserRegionalAccessProfileList = FillUserRegionalAccessProfileList(tblRegAccess);
        //    }
        //}





        ////private static List<UserRegionalAccessProfile> FillUserRegionList(DataTable tblregObjess)
        ////{
        ////    List<UserRegion> regObjList = new List<UserRegion>();

        ////    //If UserRegionalAccess table is found...
        ////    if (tblregObjess.Rows.Count > 0)
        ////    {

        ////        UserRegion regObj = null;
        ////        foreach (DataRow dr in tblregObjess.Rows)
        ////        {
        ////            regObj = new UserRegion();
        ////            regObj.Id = dr.Field<int>("UserRegionalAccessID");
        ////            regObj.RegionId = dr.Field<int>("RegionID");
        ////            regObj.IsAdmin = dr.Field<bool>("IsAdmin");
        ////            regObj.IsDefaultRegion = dr.Field<bool>("IsDefaultRegion");
        ////            regObjList.Add(regObj);
        ////        }
        ////    }

        ////    return regObjList;
        ////}


        /// <summary>
        /// Returns UserAccount information.
        /// </summary>
        /// <param name="UserId">int</param>
        /// <returns>UserAccount</returns>
        public static UserAccount GetUserAccount(int UserId)
        {

            //Fill UserProfile here.
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetUserAccountInfo.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);

                DataSet ds = db.ExecuteDataSet(dbCmd);

                return FillUserAccount(ds);
            }
        }



        /// <summary>
        /// Returns UserAccount information.
        /// </summary>
        /// <param name="UserId">int</param>
        /// <returns>UserAccount</returns>
        public static List<int> GetDescriptorsForUser(int UserId, int? AgencyId)
        {
            List<int> Descriptors = new List<int>();
            //Fill UserProfile here.
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetDescriptorsForUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                if (AgencyId.HasValue)
                    db.AddInParameter(dbCmd, "@AgencyId", DbType.Int32, AgencyId.Value);

                DataSet ds = db.ExecuteDataSet(dbCmd);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Descriptors.Add(dr.Field<int>(1));
                        }
                    }
                }
            }
            return Descriptors;
        }


        public static List<KeyValuePair<int, string>> GetReviewersForUser(int UserId, int? UserRegionId)
        {
            List<KeyValuePair<int, string>> Reviewers = new List<KeyValuePair<int, string>>();

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetReviewersForUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                if (UserRegionId.HasValue)
                    db.AddInParameter(dbCmd, "@UserRegionID", DbType.Int32, UserRegionId.Value);
                else
                    db.AddInParameter(dbCmd, "@UserRegionID", DbType.Int32, DBNull.Value);

                DataSet ds = db.ExecuteDataSet(dbCmd);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Reviewers.Add(new KeyValuePair<int, string>(dr.Field<int>(0), dr.Field<string>(1)));
                        }
                    }
                }
            }
            return Reviewers;
        }


        public static List<KeyValuePair<int, string>> GetApproversForUser(int UserId)
        {
            List<KeyValuePair<int, string>> Approvers = new List<KeyValuePair<int, string>>();

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetApproversForUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                DataSet ds = db.ExecuteDataSet(dbCmd);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Approvers.Add(new KeyValuePair<int, string>(dr.Field<int>(0), dr.Field<string>(1)));
                        }
                    }
                }
            }
            return Approvers;
        }


        /// <summary>
        /// Update a User's Profile. The User account part of the Profile can be included or excluded for updation.
        /// </summary>
        /// <param name="profileObj">UserProfile</param>
        /// <param name="UpdatedBy">int</param>
        /// <param name="UpdateAccountInfo">bool</param>
        /// <returns></returns>
        public static bool UpdateUserProfile(UserProfile userObj, int UpdatedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.UpdateUserProfile.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, userObj.UserId);
                db.AddInParameter(dbCmd, "@FirstName", DbType.String, userObj.FirstName);
                db.AddInParameter(dbCmd, "@MiddleName", DbType.String, userObj.MiddleName);
                db.AddInParameter(dbCmd, "@LastName", DbType.String, userObj.LastName);
                db.AddInParameter(dbCmd, "@Nickname", DbType.String, userObj.NickName);

                db.AddInParameter(dbCmd, "@Suffix", DbType.String, userObj.Suffix);
                db.AddInParameter(dbCmd, "@Honorifics", DbType.String, userObj.Honorifics);
                db.AddInParameter(dbCmd, "@PrimaryPhone", DbType.String, userObj.PrimaryPhone);
                db.AddInParameter(dbCmd, "@SecondaryPhone", DbType.String, userObj.SecondaryPhone);
                db.AddInParameter(dbCmd, "@SecondaryEmail", DbType.String, userObj.SecondaryEmail);

                db.AddInParameter(dbCmd, "@MedicareUniqueID", DbType.StringFixedLength, DBNull.Value);
                db.AddInParameter(dbCmd, "@IsActive", DbType.Boolean, userObj.IsActive);
                db.AddInParameter(dbCmd, "@ActiveInActiveDate", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCmd, "@UpdatedBy", DbType.Int32, UpdatedBy);

                return (db.ExecuteNonQuery(dbCmd) > 0);
            }

        }



        /// <summary>
        /// Update a User's account.
        /// </summary>
        /// <param name="userAcctObj">UserAccount</param>
        /// <param name="UpdatedBy">int</param>
        /// <returns>bool</returns>
        public static bool UpdateUserAccount(UserAccount userAcctObj, int UpdatedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.UpdateUserAccount.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, userAcctObj.UserId);
                db.AddInParameter(dbCmd, "@CounselingLocation", DbType.StringFixedLength, userAcctObj.CounselingLocation);
                db.AddInParameter(dbCmd, "@CounselingCounty", DbType.StringFixedLength, userAcctObj.CounselingCounty);
                db.AddInParameter(dbCmd, "@IsAdmin", DbType.Boolean, userAcctObj.IsAdmin);
                db.AddInParameter(dbCmd, "@IsActive", DbType.Boolean, userAcctObj.IsActive);
                db.AddInParameter(dbCmd, "@UpdatedBy", DbType.Int32, UpdatedBy);
                return (db.ExecuteNonQuery(dbCmd) > 0);
            }

        }



        /// <summary>
        /// Private method called to Set or UnSet User Active Status.
        /// </summary>
        /// <param name="UserId">int</param>
        /// <param name="ActiveInActiveBool">bool</param>
        /// <param name="UpdatedBy">int</param>
        /// <returns>bool</returns>
        public static bool SetUserAccountStatus(int UserId, bool ActiveInActiveBool, int UpdatedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.ActivateDeActivateUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserID", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@ActiveOrInactive", DbType.Boolean, ActiveInActiveBool);
                db.AddInParameter(dbCmd, "@UpdatedBy", DbType.Int32, UpdatedBy);

                int recsAffected = dbCmd.ExecuteNonQuery();
                return (recsAffected > 0 ? true : false);
            }
        }



        /// <summary>
        /// Approve a registered user
        /// </summary>
        /// <param name="UserId">int</param>
        /// <param name="ActiveInActiveBool">bool</param>
        /// <param name="UpdatedBy">int</param>
        /// <returns>bool</returns>
        public static bool ApproveUser(int UserId, int ApprovedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.ApproveUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserID", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@ApproverId", DbType.Int32, ApprovedBy);

                int recsAffected = db.ExecuteNonQuery(dbCmd);
                return (recsAffected > 0 ? true : false);
            }
        }


        /// <summary>
        /// [ActivateUserWith180DaysInacitvity]
        /// </summary>
        /// <returns>bool</returns>
        public static bool ActivateUserWith180DaysInacitvity(int UserId, int ApprovedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.ActivateUserWith180DaysInacitvity.Description()))
            {
                db.AddInParameter(dbCmd, "@UserID", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@ApproverId", DbType.Int32, ApprovedBy);

                int recsAffected = db.ExecuteNonQuery(dbCmd);
                return (recsAffected > 0 ? true : false);
            }
        }


        /// <summary>
        /// Change Password for a SHIP User
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <param name="OldPassword">OldPassword</param>
        /// <param name="NewPassword">NewPassword</param>
        /// <returns></returns>
        public static bool ChangePassword(int UserId, string OldPassword, string NewPassword, string NewClearPassword,int? ResetBy, out string ErrorMessage)
        {
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.ChangePassword.Description()))
            {

                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                //OldPassword Password must be NULL or a valid value
                db.AddInParameter(dbCmd, "@OldPassword", DbType.String, OldPassword == string.Empty ? Convert.DBNull : OldPassword);
                db.AddInParameter(dbCmd, "@NewPassword", DbType.String, NewPassword);
                db.AddInParameter(dbCmd, "@NewClearPassword", DbType.String, NewClearPassword); //sammit
                
                //ResetBy Password must have NULL or a valid value
                db.AddInParameter(dbCmd, "@ResetBy", DbType.Int32, ResetBy.HasValue ? ResetBy.Value : Convert.DBNull);

                db.AddOutParameter(dbCmd, "@ErrorMessage", DbType.String, 250);
                db.ExecuteNonQuery(dbCmd);

                Object outObj = dbCmd.Parameters["@ErrorMessage"].Value;

                string errorMessageVal = string.Empty;
                if (outObj != null && outObj != DBNull.Value)
                    errorMessageVal = (string)outObj;
                else
                    result = true;
                //Finally set the output param value
                ErrorMessage = errorMessageVal;
            }
            return result;
        }


        public static bool ChangeEmail(int UserId, string OldEmail, string NewEmail, int? ResetBy, out string ErrorMessage)
        {
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.ChangeEmail.Description()))
            {

                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                //OldPassword Password must be NULL or a valid value
                db.AddInParameter(dbCmd, "@OldEmail", DbType.String, OldEmail == string.Empty ? Convert.DBNull : OldEmail);
                db.AddInParameter(dbCmd, "@NewEmail", DbType.String, NewEmail);
                //ResetBy Password must have NULL or a valid value
                db.AddInParameter(dbCmd, "@ResetBy", DbType.Int32, ResetBy.HasValue ? ResetBy.Value : Convert.DBNull);

                db.AddOutParameter(dbCmd, "@ErrorMessage", DbType.String, 120);
                db.ExecuteNonQuery(dbCmd);

                Object outObj = dbCmd.Parameters["@ErrorMessage"].Value;

                string errorMessageVal = string.Empty;
                if (outObj != null && outObj != DBNull.Value)
                    errorMessageVal = (string)outObj;
                else
                    result = true;
                //Finally set the output param value
                ErrorMessage = errorMessageVal;
            }
            return result;
        }


        public static bool VerifyEmailToken(Guid VerificationToken, string UserName)
        {
            bool IsSuccess = false;
            Guid _UserId = Guid.Empty;
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.VerifyEmailToken.Description()))
            {
                db.AddInParameter(dbCmd, "@VerificationToken", DbType.Guid, VerificationToken);
                db.AddInParameter(dbCmd, "@PrimaryEmail", DbType.String, UserName);

                Object o = db.ExecuteScalar(dbCmd);
                if (o != null)
                {
                    if (o != DBNull.Value)
                    {
                        IsSuccess = (bool)o;
                    }
                }
            }
            return IsSuccess;
        }


        public static bool DoesUserNameExist(string UserName)
        {
            bool IsSuccess = false;
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.DoesUserNameExist.Description()))
            {
                db.AddInParameter(dbCmd, "@UserName", DbType.String, UserName);

                Object o = db.ExecuteScalar(dbCmd);
                if (o != null)
                {
                    if (o != DBNull.Value)
                    {
                        IsSuccess = (bool)o;
                    }
                }
            }
            return IsSuccess;
        }

        public static int? GetUserIdForUserName(string UserName)
        {
            int? UserId = null;
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetUserIdForUserName.Description()))
            {
                db.AddInParameter(dbCmd, "@UserName", DbType.String, UserName);

                Object o = db.ExecuteScalar(dbCmd);
                if (o != null)
                {
                    if (o != DBNull.Value)
                    {
                        UserId = (int)o;
                    }
                }
            }

            return UserId;
        }

        public static bool IsAccountCreatedViaUserRegistration(string UserName)
        {
            bool IsRegistered = false;
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.IsAccountCreatedViaUserRegistration.Description()))
            {
                db.AddInParameter(dbCmd, "@UserName", DbType.String, UserName);
                Object o = db.ExecuteScalar(dbCmd);
                if (o != null && o != DBNull.Value)
                {
                        IsRegistered = (bool)o;
                }
            }
            return IsRegistered;
        }




        public static Guid GetEmailVerificationTokenForUser(int UserId)
        {
            Guid Token = Guid.Empty;
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetEmailVerificationTokenForUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                Object o = db.ExecuteScalar(dbCmd);
                if (o != null)
                {
                    if (o != DBNull.Value)
                        Token = (Guid)o;
                }
            }
            return Token;
        }


        public static Guid GetPasswordResetVerificationTokenForUser(string UserName)
        {
            Guid Token = Guid.Empty;
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetPasswordResetVerificationToken.Description()))
            {
                db.AddInParameter(dbCmd, "@UserName", DbType.String, UserName);
                Object o = db.ExecuteScalar(dbCmd);
                if (o != null)
                {
                    if (o != DBNull.Value)
                        Token = (Guid)o;
                }
            }
            return Token;
        }

        public static bool IsPasswordResetTokenValid(Guid Token, string UserName)
        {
            bool IsValid = false;
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.IsPasswordResetTokenValid.Description()))
            {
                db.AddInParameter(dbCmd, "@Token", DbType.Guid, Token);
                db.AddInParameter(dbCmd, "@UserName", DbType.String, UserName);
                Object o = db.ExecuteScalar(dbCmd);
                if (o != null)
                {
                    if (o != DBNull.Value)
                        IsValid = (bool)o;
                }
            }
            return IsValid;
        }

        public static bool AcceptOrRejectEmailChange(Guid VerificationToken, string NewEmail, string Request)
        {
            bool IsSuccess = false;
            Guid _UserId = Guid.Empty;
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.AcceptOrRejectEmailChange.Description()))
            {
                db.AddInParameter(dbCmd, "@VerificationToken", DbType.Guid, VerificationToken);
                db.AddInParameter(dbCmd, "@TempPrimaryEmail", DbType.String, NewEmail);
                db.AddInParameter(dbCmd, "@Request", DbType.String, Request);

                Object o = db.ExecuteScalar(dbCmd);
                if (o != null)
                {
                    if (o != DBNull.Value)
                    {
                        IsSuccess = (bool)o;
                    }
                }
            }
            return IsSuccess;
        }

        public static List<User> SearchUsers(UserSearchSimpleParams param)
        {
            List<User> userList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.SearchUsers.Description()))
            {
                db.AddInParameter(dbCmd, "@SearchText", DbType.String, param.SearchText);
                db.AddInParameter(dbCmd, "@SearchedById", DbType.Int32, param.SearchedById);
                if (string.IsNullOrEmpty(param.SearchByStateFIPS))
                    db.AddInParameter(dbCmd, "@FilterByStateFIPS", DbType.StringFixedLength, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@FilterByStateFIPS", DbType.StringFixedLength, param.SearchByStateFIPS);


                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    User usrObj = null;
                    while (reader.Read())
                    {
                        //Fill User Profile object
                        usrObj = new User();
                        usrObj.UserAccount.UserId = reader.GetInt32(0);
                        usrObj.UserAccount.IsAdmin = reader.GetBoolean(1);
                        usrObj.UserProfile.FirstName = reader.GetString(2);
                        usrObj.UserProfile.MiddleName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        usrObj.UserProfile.LastName = reader.GetString(4);
                        usrObj.UserAccount.PrimaryEmail = reader.GetString(5);
                        usrObj.UserAccount.IsActive = reader.GetBoolean(6);
                        usrObj.UserAccount.StateFIPS = reader.GetString(7);
                        usrObj.UserAccount.ScopeId = reader.GetInt16(8);
                        string RegionName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);

                        //CMS/State level i.e., non-regional users with single profiles.
                        if (RegionName != string.Empty)
                        {
                            UserRegionalAccessProfile profile = new UserRegionalAccessProfile();
                            profile.RegionName = RegionName;
                            usrObj.UserRegionalProfiles.Add(profile);
                        }
                        usrObj.UserAccount.IsShipDirector = reader.GetBoolean(10);

                        userList.Add(usrObj);
                    }
                }
            }

            return userList;
        }


        public static List<User> SearchUsersFor180dInactivity(UserSearchSimpleParams param)
        {
            List<User> userList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.SearchUsersFor180dInactivity.Description()))
            {
                db.AddInParameter(dbCmd, "@SearchText", DbType.String, param.SearchText);
                db.AddInParameter(dbCmd, "@SearchedById", DbType.Int32, param.SearchedById);
                if (string.IsNullOrEmpty(param.SearchByStateFIPS))
                    db.AddInParameter(dbCmd, "@FilterByStateFIPS", DbType.StringFixedLength, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@FilterByStateFIPS", DbType.StringFixedLength, param.SearchByStateFIPS);


                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    User usrObj = null;
                    while (reader.Read())
                    {
                        //Fill User Profile object
                        usrObj = new User();
                        usrObj.UserAccount.UserId = reader.GetInt32(0);
                        usrObj.UserAccount.IsAdmin = reader.GetBoolean(1);
                        usrObj.UserProfile.FirstName = reader.GetString(2);
                        usrObj.UserProfile.MiddleName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        usrObj.UserProfile.LastName = reader.GetString(4);
                        usrObj.UserAccount.PrimaryEmail = reader.GetString(5);
                        usrObj.UserAccount.IsActive = reader.GetBoolean(6);
                        usrObj.UserAccount.StateFIPS = reader.GetString(7);
                        usrObj.UserAccount.ScopeId = reader.GetInt16(8);
                        string RegionName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);

                        //CMS/State level i.e., non-regional users with single profiles.
                        if (RegionName != string.Empty)
                        {
                            UserRegionalAccessProfile profile = new UserRegionalAccessProfile();
                            profile.RegionName = RegionName;
                            usrObj.UserRegionalProfiles.Add(profile);
                        }
                        usrObj.UserAccount.IsShipDirector = reader.GetBoolean(10);

                        userList.Add(usrObj);
                    }
                }
            }

            return userList;
        }

        public static List<User> GetUsersFor180dInactivity(int RequestingUserId)
        {
            List<User> userList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetUsersFor180dInactivity.Description()))
            {
                db.AddInParameter(dbCmd, "@SearchedById", DbType.Int32, RequestingUserId);


                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    User usrObj = null;
                    while (reader.Read())
                    {
                        //Fill User Profile object
                        usrObj = new User();
                        usrObj.UserAccount.UserId = reader.GetInt32(0);
                        usrObj.UserAccount.IsAdmin = reader.GetBoolean(1);
                        usrObj.UserProfile.FirstName = reader.GetString(2);
                        usrObj.UserProfile.MiddleName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        usrObj.UserProfile.LastName = reader.GetString(4);
                        usrObj.UserAccount.PrimaryEmail = reader.GetString(5);
                        usrObj.UserAccount.IsActive = reader.GetBoolean(6);
                        usrObj.UserAccount.StateFIPS = reader.GetString(7);
                        usrObj.UserAccount.ScopeId = reader.GetInt16(8);
                        string RegionName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);

                        //CMS/State level i.e., non-regional users with single profiles.
                        if (RegionName != string.Empty)
                        {
                            UserRegionalAccessProfile profile = new UserRegionalAccessProfile();
                            profile.RegionName = RegionName;
                            usrObj.UserRegionalProfiles.Add(profile);
                        }
                        usrObj.UserAccount.IsShipDirector = reader.GetBoolean(10);

                        userList.Add(usrObj);
                    }
                }
            }

            return userList;
        }
        public static List<User> GetAllUsers(int RequestingUserId, string filterBySearchFIPS)
        {
            List<User> userList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetAllUsers.Description()))
            {
                db.AddInParameter(dbCmd, "@RequestingUserId", DbType.Int32, RequestingUserId);
                if (string.IsNullOrEmpty(filterBySearchFIPS))
                    db.AddInParameter(dbCmd, "@FilterByStateFIPS", DbType.StringFixedLength, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@FilterByStateFIPS", DbType.StringFixedLength, filterBySearchFIPS);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    User usrObj = null;
                    while (reader.Read())
                    {
                        //Fill User object
                        usrObj = new User();
                        usrObj.UserAccount.UserId = reader.GetInt32(0);
                        usrObj.UserAccount.IsAdmin = reader.GetBoolean(1);
                        usrObj.UserProfile.FirstName = reader.GetString(2);
                        usrObj.UserProfile.MiddleName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        usrObj.UserProfile.LastName = reader.GetString(4);
                        usrObj.UserAccount.PrimaryEmail = reader.GetString(5);
                        usrObj.UserAccount.IsActive = reader.GetBoolean(6);
                        usrObj.UserAccount.StateFIPS = reader.GetString(7);
                        usrObj.UserAccount.ScopeId = reader.GetInt16(8);
                        string RegionName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);

                        //CMS/State level i.e., non-regional users with single profiles.
                        if (RegionName != string.Empty)
                        {
                            UserRegionalAccessProfile profile = new UserRegionalAccessProfile();
                            profile.RegionName = RegionName;
                            usrObj.UserRegionalProfiles.Add(profile);
                        }
                        usrObj.UserAccount.IsShipDirector = reader.GetBoolean(10);
                        usrObj.UserAccount.MedicareUniqueId = reader.IsDBNull(11) ? string.Empty : reader.GetString(11); ;

                        userList.Add(usrObj);
                        //userObj.UserAccount.UserId = reader.GetInt32(0);     
                        //userObj.UserAccount.ScopeId = Int16.Parse(reader.GetInt32(1).ToString());
                        //userObj.UserAccount.IsAdmin = reader.GetBoolean(2);    
                        //userObj.UserAccount.IsShipDirector = reader.GetBoolean(3);
                        //userObj.UserAccount.StateFIPS = reader.GetString(4);    
                        //userObj.UserProfile.FirstName = reader.GetString(5);
                        //userObj.UserProfile.MiddleName = reader.GetString(6);
                        //userObj.UserProfile.LastName = reader.GetString(7);
                        //userObj.UserAccount.PrimaryEmail = reader.GetString(8);

                        //userList.Add(userObj);

                    }
                }
            }

            return userList;
        }

        //12/05/12 - Prakash - For Getting All the Inactive User list by each State(for InActiveUserList.aspx Page)
        public static List<User> GetAllInactiveUsers(int RequestingUserId, string filterBySearchFIPS)
        {
            List<User> userList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetUsersFor180dInActiveUserList.Description()))
            {
                db.AddInParameter(dbCmd, "@RequestingUserId", DbType.Int32, RequestingUserId);
                if (string.IsNullOrEmpty(filterBySearchFIPS))
                    db.AddInParameter(dbCmd, "@FilterByStateFIPS", DbType.StringFixedLength, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@FilterByStateFIPS", DbType.StringFixedLength, filterBySearchFIPS);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    User usrObj = null;
                    while (reader.Read())
                    {
                        //Fill User object
                        usrObj = new User();
                        usrObj.UserAccount.UserId = reader.GetInt32(0);
                        usrObj.UserAccount.IsAdmin = reader.GetBoolean(1);
                        usrObj.UserProfile.FirstName = reader.GetString(2);
                        usrObj.UserProfile.MiddleName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        usrObj.UserProfile.LastName = reader.GetString(4);
                        usrObj.UserAccount.PrimaryEmail = reader.GetString(5);
                        usrObj.UserAccount.IsActive = reader.GetBoolean(6);
                        usrObj.UserAccount.StateFIPS = reader.GetString(7);
                        usrObj.UserAccount.ScopeId = reader.GetInt16(8);
                        string RegionName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);

                        //CMS/State level i.e., non-regional users with single profiles.
                        if (RegionName != string.Empty)
                        {
                            UserRegionalAccessProfile profile = new UserRegionalAccessProfile();
                            profile.RegionName = RegionName;
                            usrObj.UserRegionalProfiles.Add(profile);
                        }
                        usrObj.UserAccount.IsShipDirector = reader.GetBoolean(10);
                        usrObj.UserAccount.MedicareUniqueId = reader.IsDBNull(11) ? string.Empty : reader.GetString(11); ;

                        userList.Add(usrObj);
                        //userObj.UserAccount.UserId = reader.GetInt32(0);     
                        //userObj.UserAccount.ScopeId = Int16.Parse(reader.GetInt32(1).ToString());
                        //userObj.UserAccount.IsAdmin = reader.GetBoolean(2);    
                        //userObj.UserAccount.IsShipDirector = reader.GetBoolean(3);
                        //userObj.UserAccount.StateFIPS = reader.GetString(4);    
                        //userObj.UserProfile.FirstName = reader.GetString(5);
                        //userObj.UserProfile.MiddleName = reader.GetString(6);
                        //userObj.UserProfile.LastName = reader.GetString(7);
                        //userObj.UserAccount.PrimaryEmail = reader.GetString(8);

                        //userList.Add(userObj);

                    }
                }
            }

            return userList;
        }


        /// <summary>
        /// This method is called by UnitTest code for pure DB connectivity test purpose.
        /// </summary>
        /// <returns>int</returns>
        public static int GetUserCount()
        {

            string sqlText = "SELECT count(*) FROM Agency";
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetSqlStringCommand(sqlText))
            {
                int i = (int)db.ExecuteScalar(dbCmd);
                System.Diagnostics.Trace.WriteLine(i.ToString());
                return i;
            }
        }



        /// <summary>
        /// Returns PendingUsers in SubStateRegion and its agencies.
        /// The Pending User Registrations in Agencies of Sub State Regions are not returned.
        /// </summary>
        /// <param name="SubStateRegionId"></param>
        /// <returns></returns>
        public static List<User> GetPendingUsersBySubStateRegionId(int SubStateRegionId, int PendingDaysCount, bool IncludePendingEmails)
        {
            List<User> userList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");

            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetPendingUsersBySubStateRegion.Description()))
            {
                db.AddInParameter(dbCmd, "@SubStateRegionId", DbType.Int32, SubStateRegionId);
                db.AddInParameter(dbCmd, "@PendingDaysCount", DbType.Int32, PendingDaysCount);
                db.AddInParameter(dbCmd, "@IncludePendingEmails", DbType.Boolean, IncludePendingEmails);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    User userObj = null;
                    while (reader.Read())
                    {
                        //Fill User Profile object
                        userObj = GetUserSummaryData(reader);
                        if (userObj != null)
                            userList.Add(userObj);
                    }
                }
            }

            return userList;

        }


        public static List<User> GetPendingUsersByAgencyId(int AgencyId, int PendingDaysCount, bool IncludePendingEmails)
        {
            List<User> userList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");

            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetPendingUsersForAgency.Description()))
            {
                db.AddInParameter(dbCmd, "@AgencyId", DbType.Int32, AgencyId);
                db.AddInParameter(dbCmd, "@PendingDaysCount", DbType.Int32, PendingDaysCount);
                db.AddInParameter(dbCmd, "@IncludePendingEmails", DbType.Boolean, IncludePendingEmails);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    User userObj = null;
                    while (reader.Read())
                    {
                        //Fill User Profile object
                        userObj = GetUserSummaryData(reader);
                        if (userObj != null)
                            userList.Add(userObj);
                    }
                }
            }

            return userList;

        }

        /// <summary>
        /// Use StateFIPS 99 to get PendingUsers for CMS and CMS Regions
        /// Rest of the StateFIPS are for real State Users.
        /// The resultset will contain all Users who match the StateFIPS irrespective of their ScopeId.
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <returns></returns>
        public static List<User> GetPendingUsersByState(string StateFIPS, int PendingDaysCount, bool IncludePendingEmails)
        {
            List<User> userList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");

            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetPendingUsersByState.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, StateFIPS);
                db.AddInParameter(dbCmd, "@PendingDaysCount", DbType.Int32, PendingDaysCount);
                db.AddInParameter(dbCmd, "@IncludePendingEmails", DbType.Boolean, IncludePendingEmails);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    User userObj = null;
                    while (reader.Read())
                    {
                        //Fill User Profile object
                        userObj = GetUserSummaryData(reader);
                        if (userObj != null)
                            userList.Add(userObj);
                    }
                }
            }

            return userList;
        }



        /// <summary>
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <returns></returns>
        public static List<User> GetPendingUniqueIdRequestsByState(string StateFIPS, int PendingDaysCount)
        {
            List<User> userList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");

            using (DbCommand dbCmd = db.GetStoredProcCommand("dbo.GetPendingUniqueIdRequestsByStateFIPS"))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, StateFIPS);
                db.AddInParameter(dbCmd, "@PendingDaysCount", DbType.Int32, PendingDaysCount);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    User userObj = null;
                    while (reader.Read())
                    {
                        //Fill User Profile object
                        userObj = GetUserSummaryData(reader);
                        if (userObj != null)
                            userList.Add(userObj);
                    }
                }
            }

            return userList;
        }

        public static List<User> GetAllPendingUniqueIdRequestsByState(string StateFIPS, int PendingDaysCount)
        {
            List<User> userList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");

            using (DbCommand dbCmd = db.GetStoredProcCommand("dbo.GetAllPendingUniqueIdRequestsByStateFIPS"))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, StateFIPS);
                db.AddInParameter(dbCmd, "@PendingDaysCount", DbType.Int32, PendingDaysCount);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    User userObj = null;
                    while (reader.Read())
                    {
                        //Fill User Profile object
                        userObj = GetUserSummaryData(reader);
                        if (userObj != null)
                            userList.Add(userObj);
                    }
                }
            }

            return userList;
        }

        public static List<User> GetRevokedPendingUniqueIdRequestsByState(string StateFIPS)
        {
            List<User> userList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");

            using (DbCommand dbCmd = db.GetStoredProcCommand("dbo.GetRevoekedUniqueIdRequestsByStateFIPS"))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, StateFIPS);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    User userObj = null;
                    while (reader.Read())
                    {
                        //Fill User Profile object
                        userObj = GetUserSummaryData(reader);
                        if (userObj != null)
                            userList.Add(userObj);
                    }
                }
            }

            return userList;
        }



        /// <summary>
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <returns></returns>
        public static List<User> GetApprovedUniqueIdRequestsByStateFIPS(string StateFIPS)
        {
            List<User> userList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");

            using (DbCommand dbCmd = db.GetStoredProcCommand("dbo.GetApprovedUniqueIdRequestsByStateFIPS"))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, StateFIPS);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    User userObj = null;
                    while (reader.Read())
                    {
                        //Fill User Profile object
                        userObj = new User();

                        //Fill User Profile object
                        userObj.UserAccount.UserId = reader.GetInt32(0);
                        userObj.UserAccount.IsAdmin = reader.GetBoolean(1);
                        userObj.UserProfile.FirstName = reader.GetString(2);

                        if (!reader.IsDBNull(3))
                        userObj.UserProfile.MiddleName = reader.GetString(3);

                        userObj.UserProfile.LastName = reader.GetString(4);
                        userObj.UserAccount.PrimaryEmail = reader.GetString(5);
                        userObj.UserAccount.IsActive = reader.GetBoolean(6);
                        userObj.UserAccount.StateFIPS = reader.GetString(7);

                        userObj.UserAccount.ScopeId = reader.GetInt16(8);

                        if (!reader.IsDBNull(9))
                        userObj.UserAccount.MedicareUniqueId = reader.GetString(9);

                        if (userObj != null)
                            userList.Add(userObj);
                    }
                }
            }

            return userList;
        }

        /// <summary>
        /// Deletes a descriptor for a User.
        /// Be careful to use this method. There could be same descriptor for same user but at different agencies.
        /// So it is important that the deletion takes into account all the three parameters.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="DescriptorId"></param>
        /// <param name="AgencyId"></param>
        /// <returns></returns>
        public static bool DeleteDescriptorForUser(int UserId, int DescriptorId, int AgencyId)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.DeleteDescriptorForUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserID", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@DescriptorID", DbType.Int32, DescriptorId);
                db.AddInParameter(dbCmd, "@AgencyID", DbType.Int32, AgencyId);

                int recsAffected = db.ExecuteNonQuery(dbCmd);
                return (recsAffected > 0 ? true : false);
            }

        }



        /// <summary>
        /// Deletes all descriptors for a User.
        /// Be careful to use this method. There could be same descriptor for same user but at different agencies.
        /// So it is important that the deletion takes into account all the three parameters.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="DescriptorId"></param>
        /// <param name="AgencyId"></param>
        /// <returns></returns>
        public static void DeleteAllDescriptorsForUser(int UserId, int AgencyId)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.DeleteAllDescriptorsForUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserID", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@AgencyID", DbType.Int32, AgencyId);

                db.ExecuteNonQuery(dbCmd);
            }
        }


        /// <summary>
        /// Delete all Supervisors(Reviewers) for the User for a given UserRegionId (AgencyId or SubStateId)
        /// If the User is State User, pass null for UserRegionId 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="AgencyId"></param>
        public static void DeleteAllReviewersForUser(int UserId, int? UserRegionId)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.DeleteAllReviewersForUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserID", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@UserRegionID", DbType.Int32, UserRegionId);

                db.ExecuteNonQuery(dbCmd);
            }
        }

        public static bool AddReviewerForUser(int UserId, int ReviewerId, int? UserRegionId, int CreatedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.AddReviewerForUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserID", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@ReviewerId", DbType.Int32, ReviewerId);
                db.AddInParameter(dbCmd, "@UserRegionID", DbType.Int32, UserRegionId);
                db.AddInParameter(dbCmd, "@CreatedBy", DbType.Int32, CreatedBy);

                return (db.ExecuteNonQuery(dbCmd) > 0);
            }
        }



        /// <summary>
        /// Add a Descriptor for a User.
        /// The same descriptor could be added to a User, but using a different AgencyID.
        /// The AgencyID must be 0 for non-Agency Users and the BLL must do its job for that.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="DescriptorId"></param>
        /// <param name="AgencyId"></param>
        /// <param name="AddedBy"></param>
        /// <param name="UserDescriptorId"></param>
        /// <returns></returns>
        public static bool AddDescriptorForUser(int UserId, int DescriptorId, int AgencyId, int AddedBy, out int UserDescriptorId)
        {
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            int userDescriptorIdOut = -1;

            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.AddDescriptorForUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserID", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@DescriptorID", DbType.Int32, DescriptorId);
                db.AddInParameter(dbCmd, "@AgencyID", DbType.Int32, AgencyId);
                db.AddInParameter(dbCmd, "@CreatedBy", DbType.Int32, AddedBy);
                db.AddOutParameter(dbCmd, "@UserDescriptorID", DbType.Int32, 4);

                db.ExecuteNonQuery(dbCmd);

                if (dbCmd.Parameters["@UserDescriptorID"].Value != null && dbCmd.Parameters["@UserDescriptorID"].Value != DBNull.Value)
                {
                    userDescriptorIdOut = (int)dbCmd.Parameters["@UserDescriptorID"].Value;
                    result = true;
                }
            }

            UserDescriptorId = userDescriptorIdOut;
            return result;
        }

        public static bool OldShipUserLogin(string Username, string EncryptedPassword)
        {
            Database db = DatabaseFactory.CreateDatabase("OldShipNPRConnString");
            using (DbCommand dbCmd = db.GetStoredProcCommand("Login"))
            {
                db.AddInParameter(dbCmd, "@User", DbType.String, Username);
                db.AddInParameter(dbCmd, "@pw", DbType.String, EncryptedPassword);

                DataSet ds = db.ExecuteDataSet(dbCmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return (ds.Tables[0].Rows.Count > 0);
                }
            }

            return false;
        }
        public static OldShipUserInfo GetOldShipUserInfo(string Username)
        {
            Database db = DatabaseFactory.CreateDatabase("OldShipNPRConnString");
            OldShipUserInfo oldInfo = new OldShipUserInfo();
            using (DbCommand dbCmd = db.GetStoredProcCommand("GetUser"))
            {
                db.AddInParameter(dbCmd, "@UserName", DbType.String, Username);
                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    if (reader.Read())
                    {
                        oldInfo.UserId = reader.GetInt32(0);
                        oldInfo.Email = reader.GetString(5);
                        oldInfo.FirstName = reader.GetString(7);
                        oldInfo.LastName = reader.GetString(8);
                        oldInfo.Phone = reader.GetString(9);
                    }
                }
            }

            return oldInfo;
        }

        /// <summary>
        /// Saves the Unique Medicare ID request; returns False if the request already exist.
        /// </summary>
        /// <returns></returns>
        public static bool AddUniqueIdRequestFromUser(int RequestedUserId)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            string PARM_AlreadyExist = "@AlreadyExist";
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.AddUniqueIdRequest.Description()))
            {
                db.AddInParameter(dbCmd, "@UserID", DbType.Int32, RequestedUserId);
                db.AddOutParameter(dbCmd, PARM_AlreadyExist, DbType.Boolean, 1);

                db.ExecuteNonQuery(dbCmd);

                bool AlreadyExist = false;
                if (db.GetParameterValue(dbCmd, PARM_AlreadyExist) != null && db.GetParameterValue(dbCmd, PARM_AlreadyExist) != DBNull.Value)
                    AlreadyExist = Convert.ToBoolean(db.GetParameterValue(dbCmd, PARM_AlreadyExist));

                return !AlreadyExist;
            }
        }


        public static bool DeleteUserUniqueIDRequest(int UserId)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand("DeleteUserUniqueIDRequest"))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                db.AddOutParameter(dbCmd, "@IsSuccess", DbType.Boolean, 1);
                db.ExecuteNonQuery(dbCmd);
                return (bool)(db.GetParameterValue(dbCmd, "@IsSuccess"));
            }
        }

        public static bool RevokeMedicareUniqueIdForUser(int UserId)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand("RevokeMedicareUniqueIdForUser"))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                db.AddOutParameter(dbCmd, "@IsSuccess", DbType.Boolean, 1);
                db.ExecuteNonQuery(dbCmd);
                return (bool)(db.GetParameterValue(dbCmd, "@IsSuccess"));
            }
        }


        public static bool InvokeMedicareUniqueIdForUser(int UserId)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand("InvokeMedicareUniqueIdForUser"))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                db.AddOutParameter(dbCmd, "@IsSuccess", DbType.Boolean, 1);
                db.ExecuteNonQuery(dbCmd);
                return (bool)(db.GetParameterValue(dbCmd, "@IsSuccess"));
            }
        }

        public static bool DoesMedicareUniqueIdExist(int UserId)
        {
            bool IsSuccess = false;
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand("DoesMedicareUniqueIdExist"))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);

                Object o = db.ExecuteScalar(dbCmd);
                if (o != null)
                {
                    if (o != DBNull.Value)
                    {
                        IsSuccess = (bool)o;
                    }
                }
            }
            return IsSuccess;
        }


        

        /// <summary>
        /// Returns the Unique Medicare ID that was generated earlier for User.
        /// </summary>
        /// <returns></returns>
        public static UserUniqueID GetUniqueIdForUser(int UserId)
        {
            UserUniqueID oUniqueID = null;
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetUserUniqueId.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                IDataReader reader = db.ExecuteReader(dbCmd);
                while (reader.Read())
                {
                    oUniqueID = new UserUniqueID()
                    {
                        Id = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        RequestedDate = reader.GetDateTime(3),
                    };

                    if (!reader.IsDBNull(2))
                        oUniqueID.UniqueID = reader.GetString(2);

                    if (!reader.IsDBNull(4))
                        oUniqueID.ApproverId = reader.GetInt32(4);

                    if (!reader.IsDBNull(5))
                        oUniqueID.ApprovedDate = reader.GetDateTime(5);
                    if (reader.IsDBNull(6))
                        oUniqueID.IsMedicareUniqueIdRevoked = false;
                    else
                        oUniqueID.IsMedicareUniqueIdRevoked = reader.GetBoolean(6);

                    break;
                }
            }
            return oUniqueID;
        }


        public static string GenerateMedicareUniqueID(int RequestedUserId, int ApproverId)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand("dbo.GenerateMedicareUniqueID"))
            {
                db.AddInParameter(dbCmd, "@UserID", DbType.Int32, RequestedUserId);
                db.AddInParameter(dbCmd, "@ApproverId", DbType.Int32, ApproverId);

                
                db.AddOutParameter(dbCmd, "@MedicareUniqueID", DbType.String, 100);

                db.ExecuteNonQuery(dbCmd);


                return  (string)(db.GetParameterValue(dbCmd, "@MedicareUniqueID"));

                 
            }
        }
        


        public static LastLoginInfo GetLastLoginDetailsForUser(string UserName)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetLastLoginDetailsForUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserName", DbType.String, UserName);
                IDataReader reader = db.ExecuteReader(dbCmd);
                
                while (reader.Read())
                {
                    return new LastLoginInfo
                    {
                        FirstFailedLoginAttempt = reader.IsDBNull(0) ? (DateTime?)null : reader.GetDateTime(0),
                        LastLoginAttempt = reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1),
                        LastFailedLoginAttempt = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                        FailedLoginAttemptsCount = reader.IsDBNull(3) ? (short)0 : reader.GetInt16(3),
                        UserId = reader.GetInt32(4),
                        SessionToken = reader.IsDBNull(5) ? (Guid?)null : reader.GetGuid(5),
                        LastPasswordChangeDate = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                    };
                }
            }
            return null;
        }


        public static IEnumerable<KeyValuePair<int, string>> GetAllAgenciesForUser(int UserId, int ScopeId, string StateFIPS)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetAllAgenciesForUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@ScopeId", DbType.Int32, ScopeId);
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, StateFIPS);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        yield return new KeyValuePair<int, string>(
                            reader.GetInt32(0), reader.GetString(1));
                    }
                }
            }
        }


        private static IEnumerable<DataColumn> getvals(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount - 1; i++)
            {
                yield return new DataColumn(reader.GetName(i), reader.GetFieldType(i));
            }
            
        }

        /// <summary>
        /// Saves the UniqueID that was generated for the User.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UniqueId"></param>
        /// <param name="AddedBy"></param>
        /// <returns></returns>
        public static bool SaveUniqueIdFromUser(int UserId, string UniqueId, int AddedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.SaveUniqueIdForUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@UniqueId", DbType.StringFixedLength, UniqueId);
                db.AddInParameter(dbCmd, "@UniqueIdApprover", DbType.Int32, AddedBy);
                return (db.ExecuteNonQuery(dbCmd) > 0);
            }
        }

        
        /// <summary>
        /// Updates IsAdmin bit for User
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="IsAdmin"></param>
        public static void UpdateUserIsAdminStatus(int UserId, bool IsAdmin)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.UpdateUserIsAdmin.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@IsAdmin", DbType.Boolean, IsAdmin);
                
                db.ExecuteNonQuery(dbCmd);
            }
        }


        /// <summary>
        /// Use StateFIPS 99 to get PendingUsers for CMS and CMS Regions
        /// Rest of the StateFIPS are for real State Users.
        /// The resultset will contain all Users who match the StateFIPS irrespective of their ScopeId.
        /// if the Scope is SubStateRegion, it returns Pending Email change verifications in SubStateRegion and its agencies.
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <returns></returns>
        public static List<User> GetPendingEmailChangeVerifications(string StateFIPS, int AgencyId, int SubStateRegionId, string Scope)
        {
            List<User> userList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");

            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetPendingEmailChangeVerifications.Description()))
            {

                if (string.IsNullOrEmpty(StateFIPS))
                    db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, DBNull.Value);
                else
                    db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, StateFIPS);
                
                db.AddInParameter(dbCmd, "@AgencyId", DbType.Int32, AgencyId);
                db.AddInParameter(dbCmd, "@SubStateRegionId", DbType.Int32, SubStateRegionId);
                db.AddInParameter(dbCmd, "@Scope", DbType.String, Scope);
               
                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    User userObj = null;
                    while (reader.Read())
                    {
                        //Fill User Profile object
                        userObj = GetUserSummaryData(reader);
                        if (userObj != null)
                            userList.Add(userObj);
                    }
                }
            }

            return userList;
        }

        #region Private utility methods
        internal static IEnumerable<UserRegionalAccessProfile> GetUserRegionalProfiles(int UserId, Scope scope)
        {
            List<UserRegionalAccessProfile> profileList = new List<UserRegionalAccessProfile>();
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");

            string storedProcName = string.Empty;
            switch (scope)
            {
                case Scope.Agency:
                    storedProcName = StoredProcNames.UserAgency.GetUserAgencyProfiles.Description();
                    break;
                case Scope.SubStateRegion:
                    storedProcName = StoredProcNames.UserSubStateRegion.GetUserSubStateRegionProfiles.Description();
                    break;
                case Scope.CMSRegional:
                    storedProcName = StoredProcNames.UserCMSRegion.GetUserCMSRegionalProfiles.Description();
                    break;
            }

            using (DbCommand dbCmd = db.GetStoredProcCommand(storedProcName))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    UserRegionalAccessProfile userProfileObj = null;
                    while (reader.Read())
                    {
                        //Fill User Profile object
                        userProfileObj = new UserRegionalAccessProfile();
                        userProfileObj.Id = reader.GetInt32(0);          //UserAgencyId, UserSubStateRegionId etc
                        userProfileObj.RegionId = reader.GetInt32(1);   //AgencyID, SubStateRegionID, CMSRegionId

                        userProfileObj.IsAdmin = reader.GetBoolean(2);
                        userProfileObj.IsDefaultRegion = reader.GetBoolean(3);  //Is Default Agency etc
                        userProfileObj.RegionName = reader.GetString(4);  //Agency name, Sub State Region name etc.
                        userProfileObj.IsApproverDesignate = reader.GetBoolean(5);  //Agency name, Sub State Region name etc.
                        userProfileObj.IsSuperDataEditor = reader.GetBoolean(6);
                        userProfileObj.IsActive = reader.GetBoolean(7);

                        userProfileObj.UserId = UserId;


                        profileList.Add(userProfileObj);
                    }
                }
            }


            if (profileList.Count > 0)
            {
                int profileAgencyId = 0;

                foreach (var obj in profileList)
                {
                    //Agency ID and Sub State ID are stored as is, in UserDescriptor table. 
                    //Added SubStateRegion condition: Apr 15/2010. Earlier it was Only Agency condition..
                    if (scope.IsEqual(Scope.Agency) || scope.IsEqual(Scope.SubStateRegion))
                        profileAgencyId = obj.RegionId;

                    obj.DescriptorIDList = GetDescriptorsForUser(UserId, profileAgencyId);
                    profileAgencyId = 0;
                }
            }

            return profileList;
        }

        private static User FillUserSummaryData(IDataReader reader)
        {
            User userObj = new User();
            //Fill User Profile object
            userObj.UserAccount.UserId = reader.GetInt32(0);
            userObj.UserAccount.IsAdmin = reader.GetBoolean(1);
            userObj.UserProfile.FirstName = reader.GetString(2);
            userObj.UserProfile.MiddleName = reader.IsDBNull(3) ? "" : reader.GetString(3);
            userObj.UserProfile.LastName = reader.GetString(4);
            userObj.UserAccount.PrimaryEmail = reader.GetString(5);
            userObj.UserAccount.IsActive = reader.GetBoolean(6);
            userObj.UserAccount.StateFIPS = reader.GetString(7);
            userObj.UserAccount.ScopeId = reader.GetInt16(8);

            return userObj;
        }

        private static UserAccount FillUserAccount(DataSet ds)
        {
            UserAccount usrAcct = null;

            DataTable tbl = ds.Tables[0];

            //Table has aleast one row.
            if (tbl.Rows.Count > 0)
            {
                usrAcct = new UserAccount();
                usrAcct.UserId = tbl.Rows[0].Field<int>("UserId");
                usrAcct.StateFIPS = tbl.Rows[0].Field<string>("StateFIPS");
                usrAcct.ScopeId = tbl.Rows[0].Field<Int16>("ScopeId");
                usrAcct.CounselingLocation = tbl.Rows[0].Field<string>("CounselingLocation");
                usrAcct.IsAdmin = tbl.Rows[0].Field<bool>("IsAdmin");
                usrAcct.IsActive = tbl.Rows[0].Field<bool>("IsActive");
                usrAcct.PrimaryEmail = tbl.Rows[0].Field<string>("PrimaryEmail");
                usrAcct.IsShipDirector = tbl.Rows[0].Field<bool>("IsShipDirector");
                usrAcct.CounselingCounty = tbl.Rows[0].Field<string>("CountyOfCounselingCounty");

                //Remember: ApproverDesignate is only available for State/CMS Users.
                //For ApproverDesignate for Agency/SubState Users, refer to UserRegionalAccessProfile of a User for an Agency.
                usrAcct.IsApproverDesignate = tbl.Rows[0].Field<bool>("IsApproverDesignate");
                usrAcct.IsStateSuperDataEditor = tbl.Rows[0].Field<bool>("IsSuperDataEditor");
            }

            //GetUserRegionalAccessProfiles(ds, usrAcct);

            return usrAcct;
        }


        public static bool IsEmailVerificationTokenValid(Guid Token, string UserName)
        {
            bool IsValid = false;
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.IsEmailVerificationTokenValid.Description()))
            {
                db.AddInParameter(dbCmd, "@Token", DbType.Guid, Token);
                db.AddInParameter(dbCmd, "@UserName", DbType.String, UserName);
                Object o = db.ExecuteScalar(dbCmd);
                if (o != null)
                {
                    if (o != DBNull.Value)
                        IsValid = (bool)o;
                }
            }
            return IsValid;
        }

        public static bool ResetEmailChangeRequestDate(int UserId, string TempPrimaryEmail)
        {
            bool IsValid = false;
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.ResetEmailChangeRequestDate.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                db.AddInParameter(dbCmd, "@NewEmail", DbType.String, TempPrimaryEmail);
                Object o = db.ExecuteScalar(dbCmd);
                if (o != null)
                {
                    if (o != DBNull.Value)
                        IsValid = (bool)o;
                }
            }
            return IsValid;
        }
       
        private static readonly Func<IDataReader, User> GetUserSummaryData = FillUserSummaryData;

        #endregion

    }
}
