using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using ShiptalkLogic.DataLayer;
using ShiptalkLogic.BusinessObjects;

namespace ShiptalkLogic.BusinessLayer
{


    public sealed class UserCMSBLL
    {

        //Add Delete Operations - Deals with adding/deleting User's agency level attributes.
        #region "Add/Delete Operations"
        #endregion



        //Deals with access to an agency location or forms in an agency.
        #region "Grant/Revoke Operations"
        #endregion



        //Get Operations
        #region "Get Operations"

        public static IEnumerable<UserRegionalAccessProfile> GetUserCMSRegionalProfiles(int UserId, bool OnlyAdminProfiles)
        {
            if(OnlyAdminProfiles)
                return (from profile in UserCMSDAL.GetUserCMSRegionalProfiles(UserId) where profile.IsAdmin == true select profile);
            else
                return UserCMSDAL.GetUserCMSRegionalProfiles(UserId);
        }

        #endregion


       

    }



}
