using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using ShiptalkLogic.DataLayer;
using ShiptalkLogic.BusinessObjects;

namespace ShiptalkLogic.BusinessLayer
{


    public sealed class UserSubStateRegionBLL
    {

        //Add Delete Operations - Deals with adding/deleting User's CMS level functions.
        #region "Add/Update/Delete Operations"
        public static bool UpdateUserSubState(UserRegionalAccessProfile UserSubStateRegionProfile, int UpdatedBy)
        {
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
                //Save the Descriptors for User and then Approve.
                string ErrorMessage;

                if (UserSubStateRegionDAL.UpdateUserSubState(UserSubStateRegionProfile, UpdatedBy))
                {
                    int UserId = UserSubStateRegionProfile.UserId;
                    IEnumerable<int> NewDescriptorIds = UserSubStateRegionProfile.DescriptorIDList;
                    int AgencyId = UserSubStateRegionProfile.RegionId;
                    if (!UserBLL.SaveDescriptors(UserId, NewDescriptorIds, AgencyId, UpdatedBy, out ErrorMessage))
                        return false;

                    //scope.Complete();
                    return true;
                }
                else
                    return false;
            //}
        }
        public static bool AddUserSubStateRegionalProfile(UserRegionalAccessProfile UserSubStateRegionProfile, int CreatedBy)
        {
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
                //Save the Descriptors for User and then Approve.
                string ErrorMessage;

                if (GrantSubStateRegionAccessToUser(UserSubStateRegionProfile, CreatedBy))
                {
                    if (UserSubStateRegionProfile.DescriptorIDList != null && UserSubStateRegionProfile.DescriptorIDList.Count > 0)
                    {
                        int UserId = UserSubStateRegionProfile.UserId;
                        IEnumerable<int> NewDescriptorIds = UserSubStateRegionProfile.DescriptorIDList;
                        int AgencyId = UserSubStateRegionProfile.RegionId;

                        if (!UserBLL.SaveDescriptors(UserId, NewDescriptorIds, AgencyId, CreatedBy, out ErrorMessage))
                            return false;
                    }

                    //scope.Complete();
                    return true;
                }
                else
                    return false;
            //}
        }
        #endregion


        //Deals with access to an CMS level access
        #region "Grant/Revoke Operations"
        //public static bool GrantSubStateRegionAccessToUser(int UserId, int SubStateRegionId, bool IsAdmin, int GrantedById)
        public static bool GrantSubStateRegionAccessToUser(UserRegionalAccessProfile UserSubStateProfile, int GrantedById)
        {
            return UserSubStateRegionDAL.GrantSubStateRegionAccessToUser(UserSubStateProfile, GrantedById);
        }
        #endregion



        //Get Operations
        #region "Get Operations"
        public static IEnumerable<UserRegionalAccessProfile> GetUserSubStateRegionalProfiles(int UserId, bool OnlyAdminProfiles)
        {
            if (OnlyAdminProfiles)
                return (from profile in UserSubStateRegionDAL.GetUserSubStateRegionalProfiles(UserId) where profile.IsAdmin == true select profile);
            else
                return UserSubStateRegionDAL.GetUserSubStateRegionalProfiles(UserId);
        }
        #endregion




    }



}
