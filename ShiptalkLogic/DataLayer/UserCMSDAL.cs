using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ShiptalkLogic.BusinessObjects;

namespace ShiptalkLogic.DataLayer
{
    class UserCMSDAL
    {

        public static IEnumerable<UserRegionalAccessProfile> GetUserCMSRegionalProfiles(int UserId)
        {
            return UserDAL.GetUserRegionalProfiles(UserId, Scope.CMSRegional);
        }
    }
}
