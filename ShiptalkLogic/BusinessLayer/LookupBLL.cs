using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkLogic.DataLayer;
using ShiptalkCommon;


namespace ShiptalkLogic.BusinessLayer
{

    public static class LookupBLL
    {

        private static string Key_List_Of_Roles_In_Cache = "List_Of_Roles";

        public static List<Role> Roles
        {
            get
            {
                List<Role> roles = new List<Role>();
                if (HttpRuntime.Cache[Key_List_Of_Roles_In_Cache] == null)
                {
                    roles = LookupDAL.GetRoles();
                    HttpRuntime.Cache[Key_List_Of_Roles_In_Cache] = roles;
                }
                else
                    roles = (List<Role>)HttpRuntime.Cache[Key_List_Of_Roles_In_Cache];

                return roles;
            }
        }

        public static Role GetRole(Scope scope, bool IsAdmin)
        {
            Role role = null;
            if (scope == Scope.CMS && IsAdmin)
                role = new Role(1, "CMS Admin", "CMS administrator", true, Scope.CMS);
            else
                role = Roles.Where(r => r.scope == scope && r.IsAdmin == IsAdmin)
                    .FirstOrDefault();

            if(role == null)
                throw new ShiptalkException("Role not found for Scope: " + scope.Description() + "; IsAdmin: " + IsAdmin.ToString(), false, new ArgumentNullException("Role"));

            return role;
        }

        public static string GetRoleNameUsingScope(Scope scope, bool IsAdmin, Descriptor? descr)
        {
            string RoleName = string.Empty;
            Role r = GetRole(scope, IsAdmin);

            if (r != null)
            {
                RoleName = r.Name;

                if (descr.HasValue && r.Compare(Scope.State, ComparisonCriteria.IsEqual) && r.IsAdmin)
                {
                    //Set special description for Ship Director.
                    if (descr.Value == Descriptor.ShipDirector)
                        RoleName = "State SHIP Director";
                }
            }

            return RoleName;
        }

        public static string GetRoleDescriptionUsingScope(Scope scope, bool IsAdmin)
        {
            string RoleDescription = string.Empty;

            Role r = GetRole(scope, IsAdmin);

            if (r != null)
                RoleDescription = r.Description;

            return RoleDescription;

        }




        /// <summary>
        /// Returns the Sub State Regions for State, returned by Data Layer.
        /// Caching is currently not supported by this method due to pressing importance of doing it.
        /// It is to be noted that each State has its own Sub State Region list and so, once the 
        /// Caching requirement hits the priority list, this method will be modified to support it.
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <returns></returns>
        public static IDictionary<int, string> GetSubStateRegionsForState(string StateFIPS)
        {
            return LookupDAL.GetSubStateRegionsForState(StateFIPS);
        }

        /// <summary>
        /// Returns the CMS Regions, returned by Data Layer.
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <returns></returns>
        public static IDictionary<int, string> GetCMSRegions()
        {
            return LookupDAL.GetCMSRegions();
        }

        /// <summary>
        /// Get StateFIPS of all States that belong to a CMS Region
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetStatesForCMSRegion(int CMSRegionId)
        {
            return LookupDAL.GetStatesForCMSRegion(CMSRegionId);
        }

        public static int GetAgencyID(string AgencyCode, string StateFIPS)
        {
            IDataReader AgencyData = LookupDAL.GetAgencyByCodeState(AgencyCode, StateFIPS);
            try
            {
                AgencyData.Read();
                int AgencyID = int.Parse(AgencyData["AgencyID"].ToString());
                AgencyData.Close();
                return AgencyID;
            }
            finally
            {
                AgencyData.Close();
            }

        }

       

        /// <summary>
        /// Returns the Agencies for a given state; which is returned by calling Data Layer.
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <returns></returns>
        public static IDictionary<Int32, string> GetAgenciesForState(string StateFIPS)
        {
            return LookupDAL.GetAgenciesForStateLookup(StateFIPS);
        }
        public static IDictionary<Int32, string> GetAgenciesForState(string StateFIPS, bool Inactive)
        {
            return LookupDAL.GetAgenciesForStateLookup(StateFIPS,Inactive);
        }
        /// <summary>
        /// Returns ShipDirectorID for a given state.
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <returns></returns>
        public static int? GetShipDirectorForState(string StateFIPS)
        {
            return LookupDAL.GetShipDirectorForState(StateFIPS);
        }

        public static IEnumerable<Agency> GetAgenciesForSubStateRegion(int SubStateRegionId)
        {
            return LookupDAL.GetAgenciesForSubStateRegion(SubStateRegionId);
        }


        public static IEnumerable<KeyValuePair<int, string>> GetPresentorsForState(string StateFIPS, bool IsActive)
        {
            return LookupDAL.GetPresentorsForState(StateFIPS, IsActive);
        }

        public static IEnumerable<KeyValuePair<int, string>> GetPAMPresentorsForState(string StateFIPS)
        {
            return LookupDAL.GetPAMPresentorsForState(StateFIPS);
        }

        public static IEnumerable<KeyValuePair<int, string>> GetSubmittersForState(string StateFIPS)
        {
            return LookupDAL.GetSubmittersForState(StateFIPS);
        }

        public static bool IsShipDirector(int UserId, string StateFIPS)
        {
            bool? _IsShipDirector = null;

            int? ShipDirectorId = GetShipDirectorForState(StateFIPS);

            if (ShipDirectorId.HasValue)
                _IsShipDirector = (ShipDirectorId.Value == UserId);
            else
                _IsShipDirector = false;

            return _IsShipDirector.Value;
        }

        public static IEnumerable<KeyValuePair<int, string>> GetDescriptorsForScope(Scope scope)
        {
            var DescriptorsList = new List<KeyValuePair<int, string>>();
            DescriptorsList.Add(new KeyValuePair<int, string>(Descriptor.Counselor.EnumValue<int>(), Descriptor.Counselor.Description()));
            DescriptorsList.Add(new KeyValuePair<int, string>(Descriptor.DataSubmitter.EnumValue<int>(), Descriptor.DataSubmitter.Description()));
            DescriptorsList.Add(new KeyValuePair<int, string>(Descriptor.PresentationAndMediaStaff.EnumValue<int>(), Descriptor.PresentationAndMediaStaff.Description()));
            DescriptorsList.Add(new KeyValuePair<int, string>(Descriptor.DataEditor_Reviewer.EnumValue<int>(), Descriptor.DataEditor_Reviewer.Description()));
            DescriptorsList.Add(new KeyValuePair<int, string>(Descriptor.OtherStaff_NPR.EnumValue<int>(), Descriptor.OtherStaff_NPR.Description()));
            DescriptorsList.Add(new KeyValuePair<int, string>(Descriptor.OtherStaff_SHIP.EnumValue<int>(), Descriptor.OtherStaff_SHIP.Description()));

            

            //Dictionary<int, string> Descriptors = new Dictionary<int, string>();
            switch (scope)
            {
                case Scope.CMS:
                    break;
                case Scope.CMSRegional:
                    break;
                case Scope.State:
                    DescriptorsList.Add(new KeyValuePair<int, string>(Descriptor.ShipDirector.EnumValue<int>(), Descriptor.ShipDirector.Description()));
                    break;
                case Scope.SubStateRegion:
                    break;
                case Scope.Agency:
                    break;

            }
            return DescriptorsList;
        }

        public static string GetDescriptorName(int DescriptorId)
        {
            return DescriptorId.ToEnumObject<Descriptor>().Description();
        }

        public static int GetDescriptorId(Descriptor enumObj)
        {
            return enumObj.EnumValue<int>();
        }

        public static Descriptor GetDescriptorEnumeration(int DescriptorId)
        {
            return DescriptorId.ToEnumObject<Descriptor>();
        }

        /// <summary>
        /// Get Supervisors(Reviewers) for State Users in a state.
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<int, string>> GetReviewersForStateScope(string StateFIPS)
        {
            return LookupDAL.GetReviewersForStateScope(StateFIPS);
        }

        public static IEnumerable<KeyValuePair<int, string>> GetClientContactCounselorForState(string StateFIPS)
        {
            return LookupDAL.GetClientContactCounselorForState(StateFIPS);
        }
        public static IEnumerable<KeyValuePair<int, string>> GetClientContactSubmitterForState(string StateFIPS)
        {
            return LookupDAL.GetClientContactSubmitterForState(StateFIPS);
        }
        public static IEnumerable<KeyValuePair<int, string>> GetPresenterForState(string StateFIPS)
        {
            return LookupDAL.GetPresenterForState(StateFIPS);
        }

        /// <summary>
        /// Returns Supervisors(Reviewers) for an Agency or Sub State
        /// For State Users, refer to GetReviewersForStateScope.
        /// </summary>
        /// <param name="UserRegionId"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<int, string>> GetReviewersByUserRegion(int UserRegionId, Scope UserScope)
        {
            return LookupDAL.GetReviewersByUserRegion(UserRegionId, UserScope);
        }

        public static IEnumerable<ZipCountyView> GetZipCodesAndCountyFIPSForState(string StateFIPS)
        {
            List<ZipCountyView> ZipCodesAndCounties = new List<ZipCountyView>();
            System.Data.IDataReader reader = LookupDAL.GetZipCodeForStateFips(StateFIPS);
            //string CountyFIPS = string.Empty;
            while (reader.Read())
            {
                ZipCodesAndCounties.Add(new ZipCountyView(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
            }
            return ZipCodesAndCounties;
        }

        public static IEnumerable<KeyValuePair<int, string>> GetZipCodeForCountyFips(string CountyFips)
        {
            var _zipCodes = new List<KeyValuePair<int, string>>();

            IDataReader rdrZipCodes = LookupDAL.GetZipCodeForCountyFips(CountyFips);

            while (rdrZipCodes.Read())
            {
                _zipCodes.Add(new KeyValuePair<int, string>(rdrZipCodes.GetInt32(0), rdrZipCodes.GetString(1)));
            }
            return _zipCodes;
        }

        //TODO: This is a quick fix to a problem I was having on CCF/Add refactor asap. JP
        public static IEnumerable<KeyValuePair<string, string>> GetZipCodeForCountyFips2(string CountyFips)
        {
            var _zipCodes = new List<KeyValuePair<string, string>>();

            IDataReader rdrZipCodes = LookupDAL.GetZipCodeForCountyFips(CountyFips);

            while (rdrZipCodes.Read())
            {
                _zipCodes.Add(new KeyValuePair<string, string>(rdrZipCodes.GetInt32(0).ToString(), rdrZipCodes.GetString(1)));
            }
            return _zipCodes;
        }

        public static IEnumerable<KeyValuePair<string, string>> GetCountiesForZipCode(string ZipCode)
        {
            var _CountyCodes = new List<KeyValuePair<string, string>>();

            IDataReader rdrCounty = LookupDAL.GetCountiesByZipCode(ZipCode);

            while (rdrCounty.Read())
            {
                _CountyCodes.Add(new KeyValuePair<string, string>(rdrCounty.GetString(0), rdrCounty.GetString(1)));
            }
            return _CountyCodes;
        }
        

        public static IEnumerable<KeyValuePair<string, string>> GetCountyForAgency(int AgencyId)
        {
            return LookupDAL.GetCountyForAgency(AgencyId);
        }

    

        //public static IEnumerable<ZipCountyView> GetSubStateRegionForState(string StateFIPS)
        //{
        //    List<ZipCountyView> ZipCodes = new List<ZipCountyView>();
        //    System.Data.IDataReader reader = LookupDAL.GetZipCodeOfCountyLocationForStateFips(StateFIPS);
        //    while (reader.Read())
        //    {
        //        ZipCodes.Add(new ZipCountyView(reader.GetString(0)));
        //    }
        //    return ZipCodes;
        //}
        
        public static string GetStateFipsCodeByShortName(string ShortName)
        {
            return LookupDAL.GetStateFipsCodeByShortName(ShortName);
        }


        public static IEnumerable<KeyValuePair<string, int>> GetAllScopes()
        {
            var vals = Enum.GetValues(typeof(Scope)).AsQueryable();
            foreach (int val in vals)
            {
                yield return new KeyValuePair<string, int>(Enum.GetName(typeof(Scope), val), val);
            }
        }

    }
}
