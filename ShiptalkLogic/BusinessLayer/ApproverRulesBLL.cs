using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;

namespace ShiptalkLogic.BusinessLayer
{
    public class ApproverRulesBLL
    {

        /// <summary>
        /// If a specific agency or sub state or state is unknown, use role to find if approver at a higher role irrelevant of specific region.
        /// </summary>
        /// <returns></returns>
        public static bool IsApproverForRole(UserAccount AdminAccountInfo, Role RoleToCheck)
        {
            //If requested role is CMS/CMSRegional Scope, Admin must be CMS Admin
            if (RoleToCheck.scope.IsHigherOrEqualTo(Scope.CMSRegional))
                return IsApproverAtCMS(AdminAccountInfo);
            //If requested role is State Scope, Admin must be approver at State or CMS Level
            if (RoleToCheck.scope.IsEqual(Scope.State))
                return IsApproverForState(AdminAccountInfo, AdminAccountInfo.StateFIPS);
            //If requested role is SubState, Admin must be State level approver.
            //Since SubState Admins cannot create other Admins, Can Approve show/hide question does not arise.
            if (RoleToCheck.scope.IsEqual(Scope.SubStateRegion))
            {
                if (AdminAccountInfo.Scope.IsHigherOrEqualTo(Scope.State))
                    return IsApproverForState(AdminAccountInfo, AdminAccountInfo.StateFIPS);
                else if (AdminAccountInfo.Scope.IsEqual(Scope.SubStateRegion))
                {
                    //[Approver]Approvers at Sub States can approve Sub State Admins/Users.
                    var SubStateProfiles = UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(AdminAccountInfo.UserId, true).Where(p => p.IsApproverDesignate);
                    if (SubStateProfiles != null && SubStateProfiles.Count() > 0)
                        return true;

                }
                return false;
            }
            if (RoleToCheck.scope.IsEqual(Scope.Agency))
            {
                if (AdminAccountInfo.Scope.IsHigherOrEqualTo(Scope.State))
                    return IsApproverForState(AdminAccountInfo, AdminAccountInfo.StateFIPS);
                else if (AdminAccountInfo.Scope.IsEqual(Scope.SubStateRegion))
                {
                    //Approvers for Sub States are approvers for agencies
                    var SubstateProfiles = UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(AdminAccountInfo.UserId, true).Where(p => p.IsApproverDesignate == true);
                    if (SubstateProfiles != null && SubstateProfiles.Count() > 0)
                        return true;
                }
                else if (AdminAccountInfo.Scope.IsEqual(Scope.Agency))
                {
                    //Approvers for Sub States are approvers for agencies
                    var agencyProfiles = UserAgencyBLL.GetUserAgencyProfiles(AdminAccountInfo.UserId, true).Where(p => p.IsApproverDesignate == true);
                    if (agencyProfiles != null && agencyProfiles.Count() > 0)
                        return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Is Approver at CMS or State or atleast one of the agencies/regions.
        /// </summary>
        /// <param name="AccountInfo"></param>
        /// <returns></returns>
        public static bool IsApprover(UserAccount AccountInfo)
        {
            //All Admins are Default Admins at their Scope Level.
            //However, for State Level, Ship Directors are Default Admins.
            if (AccountInfo.IsCMSLevel)
            {
                if (AccountInfo.IsAdmin && AccountInfo.IsCMSScope)
                {
                    if (AccountInfo.IsApproverDesignate.HasValue)
                        return AccountInfo.IsApproverDesignate.Value;
                }
                return false;

            }
            else if (AccountInfo.IsStateScope)
            {
                if (AccountInfo.IsShipDirector)
                    return true;
                else
                {
                    if (AccountInfo.IsApproverDesignate.HasValue) return AccountInfo.IsApproverDesignate.Value;

                    return false;
                }
            }
            else
            {
                //For potential multi Regional Users such as Sub State and Agency Users 
                //Atleast at one agency, they are admin. Thats all we can do for generalized IsAdmin search.
                //For regional specific IsAdmin, this method must not be used.
                UserViewData UserData = UserBLL.GetUser(AccountInfo.UserId);
                foreach (UserRegionalAccessProfile regionalProfile in UserData.RegionalProfiles)
                {
                    if (regionalProfile.IsApproverDesignate && regionalProfile.IsAdmin)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Can approve CMS Users if they are CMS Approver Designates
        /// </summary>
        /// <param name="AccountInfo"></param>
        /// <returns></returns>
        public static bool IsApproverAtCMS(UserAccount AccountInfo)
        {
            if (AccountInfo.IsCMSLevel)
            {
                return (AccountInfo.IsCMSScope &&
                        AccountInfo.IsAdmin &&
                        AccountInfo.IsApproverDesignate.HasValue &&
                        AccountInfo.IsApproverDesignate.Value);

            }
            return false;
        }

        /// <summary>
        /// Can Approve State Users if they are:
        /// #) CMS Approvers
        /// #) Same state Ship Directors
        /// #) Same state Approver Designates
        /// </summary>
        /// <param name="AccountInfo"></param>
        /// <param name="StateFIPS"></param>
        /// <returns></returns>
        public static bool IsApproverForState(UserAccount AccountInfo, string StateFIPS)
        {
            bool IsApprover = false;
            if (AccountInfo.Scope.IsEqual(Scope.CMS))
                return IsApproverAtCMS(AccountInfo);
            else if (AccountInfo.Scope.IsEqual(Scope.State))
            {
                if (AccountInfo.StateFIPS == StateFIPS)
                {
                    if (AccountInfo.IsShipDirector)
                        return true;
                    else
                        return AccountInfo.IsStateAdmin &&
                            AccountInfo.IsApproverDesignate.HasValue && AccountInfo.IsApproverDesignate.Value;
                }
            }
            return IsApprover;
        }

        /// <summary>
        /// Can Approve Sub State User if:
        /// #) CMS Approvers
        /// #) Same State Ship Director
        /// #) Same State Approver Designate
        /// #) Same Sub State Approver Designates
        /// </summary>
        /// <param name="AccountInfo"></param>
        /// <param name="SubStateRegionId"></param>
        /// <returns></returns>
        public static bool IsApproverForSubState(UserAccount AccountInfo, int SubStateRegionId)
        {
            bool IsApprover = false;

            if (AccountInfo.Scope.IsEqual(Scope.CMS))
                return IsApproverAtCMS(AccountInfo);
            else if (AccountInfo.Scope.IsEqual(Scope.State))
            {
                var SubStates = LookupBLL.GetSubStateRegionsForState(AccountInfo.StateFIPS);
                if (SubStates != null && SubStates.Count > 0)
                {
                    //Check if Sub State is part of Admin State [Same State check]
                    if (SubStates.Keys.Contains(SubStateRegionId))
                    {
                        return IsApproverForState(AccountInfo, AccountInfo.StateFIPS);
                    }
                }
            }
            else if (AccountInfo.Scope.IsEqual(Scope.SubStateRegion))    //for clarity
            {
                var SubStateAdminProfiles = UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(AccountInfo.UserId, true);
                foreach (UserRegionalAccessProfile SubStateProfile in SubStateAdminProfiles)
                {
                    //Is admin is already checked while retrieving SubState profiles. Added for clarity.
                    if (SubStateProfile.RegionId == SubStateRegionId)
                        return (SubStateProfile.IsAdmin && SubStateProfile.IsApproverDesignate);
                }
            }
            return IsApprover;
        }

        /// <summary>
        /// Can Approve Agency if Approver is one of the following:
        /// #) CMS Approvers
        /// #) Ship Director or State Approver
        /// #) Sub State Approver, where User Agency is part of Approver Sub State
        /// #) Same Agency Approver
        /// </summary>
        /// <param name="AccountInfo"></param>
        /// <param name="AgencyId"></param>
        /// <returns></returns>
        public static bool IsApproverForAgency(UserAccount AccountInfo, int AgencyId)
        {

            bool IsApprover = false;

            if (AccountInfo.Scope.IsEqual(Scope.CMS))
                return IsApproverAtCMS(AccountInfo);
            else if (AccountInfo.Scope.IsEqual(Scope.State))
                return IsApproverForState(AccountInfo, AccountInfo.StateFIPS);
            else if (AccountInfo.Scope.IsEqual(Scope.SubStateRegion))    //for clarity
            {
                var SubStateAdminProfiles = UserSubStateRegionBLL.GetUserSubStateRegionalProfiles(AccountInfo.UserId, true);
                foreach (UserRegionalAccessProfile SubStateProfile in SubStateAdminProfiles)
                {
                    var AgenciesForSubState = LookupBLL.GetAgenciesForSubStateRegion(SubStateProfile.RegionId);
                    if (AgenciesForSubState != null && AgenciesForSubState.Count() > 0)
                    {
                        foreach (Agency ag in AgenciesForSubState)
                        {
                            //If agency is part of the Admin Sub State 
                            if (ag.Id == AgencyId)
                            {
                                //Is admin is already checked while retrieving SubState profiles. Added for clarity.
                                return (SubStateProfile.IsAdmin && SubStateProfile.IsApproverDesignate);
                            }
                        }
                    }
                }
            }
            else if (AccountInfo.Scope.IsEqual(Scope.Agency))
            {
                var AdminAgencies = UserAgencyBLL.GetUserAgencyProfiles(AccountInfo.UserId, true);
                foreach (UserRegionalAccessProfile agencyProfile in AdminAgencies)
                {
                    //Is admin is already checked while retrieving Agency profiles. Added for clarity.
                    if (agencyProfile.RegionId == AgencyId)
                        return agencyProfile.IsAdmin && agencyProfile.IsApproverDesignate;
                }
            }

            return IsApprover;


        }


    }
}
