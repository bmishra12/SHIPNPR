using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using ShiptalkLogic.DataLayer;
using ShiptalkLogic.BusinessObjects;

namespace ShiptalkLogic.BusinessLayer
{


    public sealed class UserAgencyBLL
    {

        //Add Delete Operations - Deals with adding/deleting User's agency level attributes.
        #region "Add/Delete Operations"
        /// <summary>
        /// Adds descriptors to User in an Agency. Users can belong to multiple agencies.
        /// This method does not support updating Descriptors to multiple agencies.
        /// This method only supports adding multiple descriptors for a User in One agency.
        /// </summary>
        /// <param name="UserDescriptorList">List Of UserDescriptor</param>
        /// <param name="AddedBy">int</param>
        /// <param name="ErrorMessage">out string</param>
        /// <returns>bool</returns>
        ////public bool AddDescriptorsForUser(ref List<UserDescriptor> UserDescriptorList, int AddedBy, out string ErrorMessage) 
        ////{
        ////    string message = string.Empty;
        ////    bool result = true;

        ////    foreach (UserDescriptor descrObj in UserDescriptorList)
        ////    {
        ////        int outUserDescriptorID;
        ////        if (UserAgencyDAL.AddDescriptorForUser(descrObj, AddedBy, out outUserDescriptorID))
        ////        {
        ////            descrObj.UserDescriptorId = outUserDescriptorID;
        ////        }
        ////        else
        ////            result = false;
        ////    }
        ////    if (result == false)
        ////    {
        ////        if (UserDescriptorList.Count > 1)
        ////            message = "Unable to add one or more descriptors for the user.";
        ////        else
        ////            message = "Unable to add the descriptor for user.";
        ////    }
        ////    ErrorMessage = message;
        ////    return result;
        ////}


        /// <summary>
        /// Adds User to an existing agency and saves descriptors for the User in that agency.
        /// </summary>
        /// <param name="UserAgencyProfile"></param>
        /// <param name="UpdatedBy"></param>
        /// <returns></returns>
        public static bool AddUserAgency(UserRegionalAccessProfile UserAgencyProfile, int UpdatedBy)
        {
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
                //Save the Descriptors for User and then Approve.
                if (UserAgencyDAL.GrantAgencyAccessToUser(UserAgencyProfile, UpdatedBy))
                {
                    IEnumerable<int> NewDescriptorIds = UserAgencyProfile.DescriptorIDList;
                    int UserId = UserAgencyProfile.UserId;
                    int AgencyId = UserAgencyProfile.RegionId;
                    string ErrorMessage;
                    if (!UserBLL.SaveDescriptors(UserId, NewDescriptorIds, AgencyId, UpdatedBy, out ErrorMessage))
                        return false;

                    //scope.Complete();
                    return true;
                }
                else
                    return false;
            //}
        }



        /// <summary>
        /// Update Agency profile for User
        /// </summary>
        /// <param name="UserSubStateRegionProfile"></param>
        /// <param name="UpdatedBy"></param>
        /// <returns></returns>
        public static bool UpdateUserAgency(UserRegionalAccessProfile UserAgencyProfile, int UpdatedBy)
        {
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
                //Save the Descriptors for User and then Approve.
                IEnumerable<int> NewDescriptorIds = UserAgencyProfile.DescriptorIDList;
                int UserId = UserAgencyProfile.UserId;
                int AgencyId = UserAgencyProfile.RegionId;
                string ErrorMessage;

                if (UserAgencyDAL.UpdateUserAgency(UserAgencyProfile, UpdatedBy))
                {
                    if (UserBLL.SaveDescriptors(UserId, NewDescriptorIds, AgencyId, UpdatedBy, out ErrorMessage))
                    {
                        //scope.Complete();
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            //}
        }




        /// <summary>
        /// Add Reviewers to a User in an Agency. Users can belong to multiple agencies.
        /// This method does not support adding Reviewers to multiple agencies.
        /// This method only supports adding multiple Reviwers for an Agency User.
        /// </summary>
        /// <param name="UserReviewerList">ref List</param>
        /// <param name="AddedBy">int</param>
        /// <param name="ErrorMessage">out string</param>
        /// <returns>bool</returns>
        //public bool AddReviewerForUser(ref List<UserReviewer> UserReviewerList, int AddedBy, out string ErrorMessage)
        //{
        //    string message = string.Empty;
        //    bool result = true;

        //    foreach (UserReviewer usrReviewerObj in UserReviewerList)
        //    {
        //        int outUserReviewerID;
        //        if (UserAgencyDAL.AddReviewerForUser(usrReviewerObj, out outUserReviewerID))
        //        {
        //            usrReviewerObj.UserReviewerId = outUserReviewerID;
        //        }
        //        else
        //            result = false;
        //    }
        //    if (result == false)
        //    {
        //        if (UserReviewerList.Count > 1)
        //            message = "Unable to add one or more Reviewers for the user.";
        //        else
        //            message = "Unable to add Reviewer for the user.";
        //    }
        //    ErrorMessage = message;
        //    return result;
        //}


        /// <summary>
        /// Delete one or more Reviewers for an Agency User.
        /// To delete Reviewerss in multiple agencies, this method must be called multiple times.
        /// </summary>
        /// <param name="UserReviewerIDList">List</param>
        /// <param name="ErrorMessage">out string</param>
        /// <returns>bool</returns>
        //public bool DeleteReviewerForUser(List<int> UserReviewerIDList, out string ErrorMessage)
        //{
        //    bool result = true;
        //    string message = string.Empty;
        //    foreach (int UserReviewerID in UserReviewerIDList)
        //    {
        //        if (!UserAgencyDAL.DeleteReviewerForUser(UserReviewerID))
        //            result = false;
        //    }
        //    if (result == false)
        //    {
        //        if (UserReviewerIDList.Count > 1)
        //            message = "Failed to remove one or more Reviewers for the User.";
        //        else
        //            message = "Failed to remove the Reviewer for the User.";
        //    }
        //    ErrorMessage = message;
        //    return result;
        //}
        #endregion



        //Deals with access to an agency location or forms in an agency.
        #region "Grant/Revoke Operations"

        public bool GrantAgencyAccessToUser() { throw new NotImplementedException(); }

        public bool UpdateAgencyAccessForUser() { throw new NotImplementedException(); }

        public bool RevokeAgencyAccessForUser() { throw new NotImplementedException(); }

        public bool GrantUserAction() { throw new NotImplementedException(); }

        public bool RevokeUserAction() { throw new NotImplementedException(); }
        #endregion



        //Get Operations
        #region "Gets"

        public static IEnumerable<UserRegionalAccessProfile> GetUserAgencyProfiles(int UserId, bool OnlyAdminProfiles)
        {
            if (OnlyAdminProfiles)
                return (from profile in UserAgencyDAL.GetUserAgencyProfiles(UserId) where profile.IsAdmin == true select profile);
            else
                return UserAgencyDAL.GetUserAgencyProfiles(UserId);
        }

        #endregion






    }



}
