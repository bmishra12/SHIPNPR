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
    class UserSubStateRegionDAL
    {

        public static IEnumerable<UserRegionalAccessProfile> GetUserSubStateRegionalProfiles(int UserId)
        {
            return UserDAL.GetUserRegionalProfiles(UserId, Scope.SubStateRegion);
        }

        /// <summary>
        /// Update a User's account in a Sub State Region.
        /// </summary>
        /// <param name="userAcctObj">UserRegionalAccessProfile</param>
        /// <param name="UpdatedBy">int</param>
        /// <returns>bool</returns>
        public static bool UpdateUserSubState(UserRegionalAccessProfile UserRegionalProfile, int UpdatedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.UserSubStateRegion.UpdateUserSubStateRegion.Description()))
            {
                db.AddInParameter(dbCmd, "@UserSubStateRegionId", DbType.Int32, UserRegionalProfile.Id);
                db.AddInParameter(dbCmd, "@IsAdmin", DbType.Boolean, UserRegionalProfile.IsAdmin);
                db.AddInParameter(dbCmd, "@IsDefaultSubState", DbType.Boolean, UserRegionalProfile.IsDefaultRegion);
                db.AddInParameter(dbCmd, "@IsActive", DbType.Boolean, UserRegionalProfile.IsActive);
                db.AddInParameter(dbCmd, "@UpdatedBy", DbType.Int32, UpdatedBy);
                db.AddInParameter(dbCmd, "@IsApproverDesignate", DbType.Boolean, UserRegionalProfile.IsApproverDesignate);
                db.AddInParameter(dbCmd, "@IsSuperDataEditor", DbType.Boolean, UserRegionalProfile.IsSuperDataEditor);

                return (db.ExecuteNonQuery(dbCmd) > 0);
            }
        }



        //public static bool GrantSubStateRegionAccessToUser(int UserId, int SubStateRegionId, bool IsAdmin, int GrantedBy)
        public static bool GrantSubStateRegionAccessToUser(UserRegionalAccessProfile SubStateProfile, int GrantedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.UserAccess.GrantSubStateRegionAccessToUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, SubStateProfile.UserId);
                db.AddInParameter(dbCmd, "@SubStateRegionId", DbType.Int32, SubStateProfile.RegionId);
                db.AddInParameter(dbCmd, "@IsAdmin", DbType.Boolean, SubStateProfile.IsAdmin);
                db.AddInParameter(dbCmd, "@GrantedBy", DbType.Int32, GrantedBy);
                db.AddInParameter(dbCmd, "@IsApproverDesignate", DbType.Boolean, SubStateProfile.IsApproverDesignate);
                db.AddInParameter(dbCmd, "@IsSuperDataEditor", DbType.Boolean, SubStateProfile.IsSuperDataEditor);
                db.AddInParameter(dbCmd, "@IsDefaultSubStateRegion", DbType.Boolean, SubStateProfile.IsDefaultRegion);

                db.AddOutParameter(dbCmd, "@UserSubStateRegionID", DbType.Int32, 4);

                return (db.ExecuteNonQuery(dbCmd) > 0);
            }
        }



    }
}
