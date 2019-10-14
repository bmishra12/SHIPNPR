using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using System.Data;
using System.Data.Common;

using ShiptalkLogic.BusinessObjects;


namespace ShiptalkLogic.DataLayer
{
 
    internal static class UserAgencyDAL
    {

        //Add Delete Operations - Deals with adding/deleting User's agency level attributes.
        #region "Add/Delete Operations"
        /// <summary>
        /// Adds one descriptor to a User in an Agency. 
        /// </summary>
        /// <param name="UserDescriptorObj">UserDescriptor</param>
        /// <param name="AddedBy">int</param>
        /// <param name="UserDescriptorId">out int</param>
        /// <returns>bool</returns>
        ////public static bool AddDescriptorForUser(UserDescriptor UserDescriptorObj, int AddedBy, out int UserDescriptorId) {

        ////    bool result = false;

        ////    Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
        ////    int userDescriptorIdOut = -1;

        ////    using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.UserAgency.AddDescriptorsForUser.Description()))
        ////    {
        ////        db.AddInParameter(dbCmd, "@UserID", DbType.Int32, UserDescriptorObj.UserId);
        ////        db.AddInParameter(dbCmd, "@DescriptorID", DbType.Int32, UserDescriptorObj.DescriptorId);
        ////        db.AddInParameter(dbCmd, "@AgencyID", DbType.Int32, UserDescriptorObj.AgencyId);
        ////        db.AddInParameter(dbCmd, "@CreatedBy", DbType.Int32, AddedBy);
        ////        db.AddOutParameter(dbCmd, "@UserDescriptorID", DbType.Int32, 4);

        ////        db.ExecuteNonQuery(dbCmd);
                
        ////        if (dbCmd.Parameters["@UserDescriptorID"].Value != null)
        ////        {
        ////            userDescriptorIdOut = (int)dbCmd.Parameters["@UserDescriptorID"].Value;
        ////            result = true;
        ////        }
        ////    }

        ////    UserDescriptorId = userDescriptorIdOut;
        ////    return result;
        ////}


       

       


        /// <summary>
        /// Add a reviewer for an AgencyUser.
        /// For multiple descriptors Or multiple agencies, this method must be called multiple times.
        /// </summary>
        /// <param name="userReviewerObj">UserReviewer</param>
        /// <param name="outUserReviewerId">out int</param>
        /// <returns>bool</returns>
        //public static bool AddReviewerForUser(UserReviewer userReviewerObj, out int outUserReviewerId) 
        //{
        //    bool result = false;

        //    Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
        //    int userReviewerIdOut = -1;

        //    using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.UserAgency.AddReviewerForUser.Description()))
        //    {
        //        db.AddInParameter(dbCmd, "@UserAgencyID", DbType.Int32, userReviewerObj.UserAgencyId );
        //        db.AddInParameter(dbCmd, "@ReviewerID", DbType.Int32, userReviewerObj.ReviewerId);
        //        db.AddInParameter(dbCmd, "@FormID", DbType.Int16, userReviewerObj.FormId);
        //        db.AddInParameter(dbCmd, "@ActionID", DbType.Int16, userReviewerObj.ActionId);
        //        db.AddInParameter(dbCmd, "@CreatedBy", DbType.Int32, userReviewerObj.CreatedBy);

        //        db.AddOutParameter(dbCmd, "@UserReviewerID", DbType.Int32, 4);

        //        dbCmd.ExecuteNonQuery();

        //        if (dbCmd.Parameters["@UserReviewerID"].Value != null)
        //        {
        //            userReviewerIdOut = (int)dbCmd.Parameters["@UserReviewerID"].Value;
        //            result = true;
        //        }
        //    }
        //    outUserReviewerId = userReviewerIdOut;
        //    return result;
        //}


        /// <summary>
        /// Update a User's account in a Sub State Region.
        /// </summary>
        /// <param name="userAcctObj">UserRegionalAccessProfile</param>
        /// <param name="UpdatedBy">int</param>
        /// <returns>bool</returns>
        public static bool UpdateUserAgency(UserRegionalAccessProfile UserRegionalProfile, int UpdatedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.UserAgency.UpdateUserAgency.Description()))
            {
                db.AddInParameter(dbCmd, "@UserAgencyId", DbType.Int32, UserRegionalProfile.Id);
                db.AddInParameter(dbCmd, "@IsAdmin", DbType.Boolean, UserRegionalProfile.IsAdmin);
                db.AddInParameter(dbCmd, "@IsDefaultAgency", DbType.Boolean, UserRegionalProfile.IsDefaultRegion);
                db.AddInParameter(dbCmd, "@IsActive", DbType.Boolean, UserRegionalProfile.IsActive);
                db.AddInParameter(dbCmd, "@UpdatedBy", DbType.Int32, UpdatedBy);
                db.AddInParameter(dbCmd, "@IsApproverDesignate", DbType.Boolean, UserRegionalProfile.IsApproverDesignate);
                db.AddInParameter(dbCmd, "@IsSuperDataEditor", DbType.Boolean, UserRegionalProfile.IsSuperDataEditor);
                return (db.ExecuteNonQuery(dbCmd) > 0);
            }
        }


        /// <summary>
        /// Delete a reviewer for a an AgencyUser.
        /// </summary>
        /// <param name="UserReviewerId">int</param>
        /// <returns>bool</returns>
        //public static bool DeleteReviewerForUser(int UserReviewerId)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
        //    int recsAffected = 0;

        //    using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.UserAgency.DeleteReviewerForUser.Description()))
        //    {
        //        db.AddInParameter(dbCmd, "@UserReviewerId", DbType.Int32, UserReviewerId);
        //        recsAffected = dbCmd.ExecuteNonQuery();
        //    }

        //    return (recsAffected > 0) ? true : false;
        //}
        #endregion



        #region Get User specific Agency Information
        public static IEnumerable<UserRegionalAccessProfile> GetUserAgencyProfiles(int UserId)
        {
            return UserDAL.GetUserRegionalProfiles(UserId, Scope.Agency);
        }

       
        #endregion


        //Deals with access to an agency location or forms in an agency.
        #region "Grant/Revoke Operations"

        public static bool GrantAgencyAccessToUser(UserRegionalAccessProfile AgencyRegionProfile, int GrantedBy)
        {
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.UserAccess.GrantAgencyAccessToUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, AgencyRegionProfile.UserId);
                db.AddInParameter(dbCmd, "@AgencyId", DbType.Int32, AgencyRegionProfile.RegionId);
                db.AddInParameter(dbCmd, "@IsAdmin", DbType.Boolean, AgencyRegionProfile.IsAdmin);
                db.AddInParameter(dbCmd, "@IsDefaultAgency", DbType.Boolean, AgencyRegionProfile.IsDefaultRegion);
                db.AddInParameter(dbCmd, "@GrantedBy", DbType.Int32, GrantedBy);
                db.AddInParameter(dbCmd, "@IsApproverDesignate", DbType.Boolean, AgencyRegionProfile.IsApproverDesignate);
                db.AddInParameter(dbCmd, "@IsSuperDataEditor", DbType.Boolean, AgencyRegionProfile.IsSuperDataEditor);

                db.AddOutParameter(dbCmd, "@UserAgencyID", DbType.Int32, 4);

                if (db.ExecuteNonQuery(dbCmd) > 0)
                    result = true;
            }

            return result;
        }

        public static bool UpdateAgencyAccessForUser() { throw new NotImplementedException(); }

        public static bool RevokeAgencyAccessForUser() { throw new NotImplementedException(); }

        public static bool GrantUserAction() { throw new NotImplementedException(); }

        public static bool RevokeUserAction() { throw new NotImplementedException(); }
        #endregion
    }
}
