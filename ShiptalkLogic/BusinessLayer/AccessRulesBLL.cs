using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkCommon;

namespace ShiptalkLogic.BusinessLayer
{
    public class AccessRulesBLL
    {
        #region "Edit methods : If a person can edit another person's record."

        private static bool CanEditCMSUser(UserViewData CMSUserData, UserViewData EditorData)
        {
            if (CMSUserData.IsCMSAdmin)
                return EditorData.IsCMSAdmin && EditorData.IsCMSApproverDesignate;
            else
                return EditorData.IsCMSAdmin;
        }
        private static bool CanEditCMSRegionalUser(UserViewData CMSRegionalUserData, UserViewData EditorData)
        {
            return EditorData.IsCMSAdmin;
        }
        private static bool CanEditStateUser(UserViewData StateUserData, UserViewData EditorData)
        {
            if (EditorData.IsCMSLevel)
                return EditorData.IsCMSAdmin;   //Will approve all State Level users
            else if (EditorData.IsUserStateScope && (EditorData.StateFIPS == StateUserData.StateFIPS))
            {
                //Ship directors are only edited by CMS Admins
                if (StateUserData.IsShipDirector)
                    return false;
                else if (StateUserData.IsStateAdmin)
                {
                    if (StateUserData.IsStateApproverDesignate)
                        return EditorData.IsShipDirector;
                    else
                        return EditorData.IsShipDirector || EditorData.IsStateApproverDesignate;
                }
                else
                    return EditorData.IsStateAdmin;
            }

            return false;
        }
        private static bool CanEditMultiSubStateUser(UserViewData SubStateUserData, UserViewData EditorData)
        {
            if (EditorData.IsCMSLevel)
            {
                return EditorData.IsCMSAdmin;
            }
            else //State Level Users follow
            {
                //Kick out other state users
                if ((EditorData.StateFIPS != SubStateUserData.StateFIPS))
                    return false;

                //State Scope Users must be Admins
                if (EditorData.Scope.IsEqual(Scope.State))
                    return EditorData.IsStateAdmin;

                //Kickout agency users.
                if (EditorData.Scope.IsEqual(Scope.Agency))
                    return false;

                //The only possibility from now on, is Editor is also a  Sub State Scope user..
                IEnumerable<UserRegionalAccessProfile> EditorAdminProfiles = EditorData.RegionalProfiles.Where(p => p.IsAdmin == true);

                //Iterate each Admin profile of Editor
                foreach (UserRegionalAccessProfile editorUserProfile in EditorAdminProfiles)
                {
                    //For each maching sub state of editor/substate user
                    foreach (UserRegionalAccessProfile subStateUserProfile in SubStateUserData.RegionalProfiles.Where(p => p.RegionId == editorUserProfile.RegionId))
                    {
                        //Sub State Approvers can be edited by state approvers or state admins only.
                        if (!subStateUserProfile.IsApproverDesignate)
                        {
                            //sub state admins can be edited by same sub state approvers
                            if (subStateUserProfile.IsAdmin && editorUserProfile.IsApproverDesignate)
                                return true;
                            else if (!subStateUserProfile.IsAdmin)
                                return true;
                        }
                    }
                }
            }


            return false;

        }
        private static bool CanEditMultiAgencyUser(UserViewData AgencyUserData, UserViewData EditorData)
        {
            if (EditorData.IsCMSLevel)
            {
                //At CMS/CMSRegional level, only CMSAdmin is allowed.
                return EditorData.IsCMSAdmin;
            }
            else //State Level Users follow
            {
                //Kick out other state users
                if ((EditorData.StateFIPS != AgencyUserData.StateFIPS))
                    return false;

                //State Scope Users must be Admins
                if (EditorData.Scope.IsEqual(Scope.State))
                    return EditorData.IsStateAdmin;

                //Sub State User requesting Edit access on an Agency User.
                else if (EditorData.Scope.IsEqual(Scope.SubStateRegion))
                {
                    //The Agency of the account requested must be part of Editor's Sub State region.
                    IEnumerable<UserRegionalAccessProfile> EditorSubStateProfiles = EditorData.RegionalProfiles.Where(p => p.IsAdmin == true);
                    IEnumerable<UserRegionalAccessProfile> AgencyUserProfiles = AgencyUserData.RegionalProfiles;

                    foreach (UserRegionalAccessProfile adminSubStProfile in EditorSubStateProfiles)
                    {
                        IEnumerable<Agency> AgenciesForSubState = LookupBLL.GetAgenciesForSubStateRegion(adminSubStProfile.RegionId);
                        foreach (UserRegionalAccessProfile agencyprofile in AgencyUserProfiles)
                        {
                            //Find a match between one of the agencies of the User Vs Agencies of Sub States where Admin has access.
                            Agency matchingAgency = AgenciesForSubState.Where(ag => (ag.Id == agencyprofile.RegionId) && (ag.IsActive == true)).FirstOrDefault();
                            if (matchingAgency != null)
                            {
                                return true;
                            }
                        }
                    }
                }
                //Agency level User requesting Edit access on an Agency level User. 
                //It will be Agency level still I will put the If condition because I just like it :-,
                else if (EditorData.Scope.IsEqual(Scope.Agency))
                {
                    //The Agency of the account requested must be part of Editor's Sub State region.
                    IEnumerable<UserRegionalAccessProfile> EditorAgencyProfiles = EditorData.RegionalProfiles.Where(p => p.IsAdmin == true);
                    IEnumerable<UserRegionalAccessProfile> UserAgencyProfiles = AgencyUserData.RegionalProfiles;
                    foreach (UserRegionalAccessProfile agencyProfile in UserAgencyProfiles)
                    {
                        //Approvers can be edited only by higher scope Admins.
                        if (!agencyProfile.IsApproverDesignate)
                        {
                            foreach (UserRegionalAccessProfile editorAgency in EditorAgencyProfiles)
                            {
                                //User and Editor have access to same Agency
                                if (editorAgency.RegionId == agencyProfile.RegionId)
                                {
                                    //We know editor is already admin from the filter we did to EditorData.RegionalProfiles.Where(...)
                                    //Since admins cannot edit other admins, we ignore that condition as well
                                    if (agencyProfile.IsAdmin)
                                    {
                                        if (editorAgency.IsApproverDesignate) return true;
                                    }
                                    else
                                        return true;
                                }
                            }
                        }
                    }
                }

            }

            return false;

        }

        public static bool CanEditUserProfile(UserViewData UserToEdit, UserViewData Editor)
        {
            switch (UserToEdit.Scope)
            {
                case Scope.CMS:
                    return CanEditCMSUser(UserToEdit, Editor);
                case Scope.CMSRegional:
                    return CanEditCMSRegionalUser(UserToEdit, Editor);
                case Scope.State:
                    return CanEditStateUser(UserToEdit, Editor);
                case Scope.SubStateRegion:
                    return CanEditMultiSubStateUser(UserToEdit, Editor);
                case Scope.Agency:
                    return CanEditMultiAgencyUser(UserToEdit, Editor);
                default:
                    throw new ShiptalkException("Unknown User Scope.", false);
            }
        }
        #endregion


        #region "Add methods: If a person can add another person to a state or sub state or agency"
        public static bool CanAddUser(UserViewData AdminData, Scope ScopeOfNewUser, string NewUserStateFIPS)
        {
            switch (ScopeOfNewUser)
            {
                case Scope.CMS:
                    return CanAddUserToCMSScope(AdminData);
                case Scope.CMSRegional:
                    return CanAddUserToCMSRegion(AdminData);
                case Scope.State:
                    return CanAddUserToStateScope(AdminData, NewUserStateFIPS);
                case Scope.SubStateRegion:
                    return CanAddUserToSubStateScope(AdminData, NewUserStateFIPS);
                case Scope.Agency:
                    return CanAddUserToAgencyScope(AdminData, NewUserStateFIPS);
                default:
                    throw new ShiptalkException("Unknown User Scope.", false);
            }
        }

        /// <summary>
        /// Add Users to CMS Scope
        /// #) CMS Admins can add Users at CMS Scope
        /// </summary>
        /// <param name="AdminData"></param>
        /// <returns></returns>
        private static bool CanAddUserToCMSScope(UserViewData AdminData)
        {
            return AdminData.IsCMSAdmin;
        }

        /// <summary>
        /// To Add CMS Admins 
        /// #) CMS Admins can add Users at CMS Scope
        /// #) CMS Admin must be approver delegate to add a CMS Admin.
        /// </summary>
        /// <param name="AdminData"></param>
        /// <param name="NewUserRoleIsAdmin"></param>
        /// <returns></returns>
        public static bool CanAddUserToCMSRole(UserViewData AdminData, bool NewUserRoleIsAdmin)
        {
            if (NewUserRoleIsAdmin)
                return AdminData.IsCMSAdmin && AdminData.IsCMSApproverDesignate;
            else
                return AdminData.IsCMSAdmin;
        }

        /// <summary>
        /// To add User to CMS Region
        /// #) User must be CMS Admin, at a minimum
        /// </summary>
        /// <param name="AdminData"></param>
        /// <returns></returns>
        public static bool CanAddUserToCMSRegion(UserViewData AdminData)
        {
            return AdminData.IsCMSAdmin;
        }


        /// <summary>
        /// To add User to State Scope
        /// #) User must be CMS Admin Or
        /// #) User must be minimum of State Admin if not ship director
        /// #) States must match if Admin is State Admin or Ship Director
        /// </summary>
        /// <param name="AdminData"></param>
        /// <param name="StateFIPS"></param>
        /// <returns></returns>
        private static bool CanAddUserToStateScope(UserViewData AdminData, string StateFIPS)
        {
            if (AdminData.IsStateAdmin && AdminData.StateFIPS == StateFIPS) return true;
            else if (AdminData.IsCMSAdmin) return true;

            return false;
        }


        /// <summary>
        /// Minimum requirements:
        ///     To Add State Ship Director
        ///         #) Admin must be CMS Admin
        ///     To Add a State Admin
        ///         #) Admin must be Ship Director or State Admin PLUS with Approver Delegate rights        
        ///     To Add a State User
        ///         #) Admin must be minimum of a State Admin
        /// </summary>
        /// <param name="AdminData"></param>
        /// <param name="StateFIPS"></param>
        /// <param name="NewUserRoleIsAdmin"></param>
        /// <returns></returns>
        public static bool CanAddUserToStateRole(UserViewData AdminData, string StateFIPS, bool NewUserRoleIsAdmin, bool ShipDirectorRoleRequested)
        {
            //CMS Admins can create users of all state roles
            if (AdminData.IsCMSAdmin)
                return true;
            else
            {
                //Ship directors are also state admins
                if (AdminData.IsStateAdmin && (AdminData.StateFIPS == StateFIPS))
                {
                    //Ship directors can only be created by CMS Admins
                    if (ShipDirectorRoleRequested)
                        return false;

                    //If State Admin is requested, creating Admin must be State Admin PLUS Approver [or Ship Director]
                    if (NewUserRoleIsAdmin)
                    {
                        return (AdminData.IsShipDirector || AdminData.IsStateApproverDesignate);
                    }
                    else
                    {
                        //New State User Role requests can be served by State Admins
                        return true;
                    }
                }

            }
            return false;
        }


        private static bool CanAddUserToSubStateScope(UserViewData AdminData, string StateFIPS)
        {
            //CMS Admins can create users of all state roles
            if (AdminData.IsCMSAdmin)
                return true;
            else if (AdminData.StateFIPS == StateFIPS)
            {
                if (AdminData.IsStateAdmin) return true;
                else if (AdminData.IsUserSubStateRegionalScope)
                {
                    //The only possibility from now on, is Editor is a Sub State User.
                    foreach (UserRegionalAccessProfile subStateUserProfile in AdminData.RegionalProfiles)
                    {
                        if (subStateUserProfile.IsAdmin)
                            return true;
                    }
                }
            }

            return false;
        }




        public static bool CanAddUserToSubState(UserViewData AdminData, string StateFIPS, int SubStateIDRequested, bool NewUserRoleIsAdmin, bool ApproverRightsRequested)
        {
            if (ApproverRightsRequested)
            {
                //Approver rights request must be accompanied by Admin request
                //because only Admins can be approvers
                if (!NewUserRoleIsAdmin)
                    return false;


                if (AdminData.IsCMSAdmin && AdminData.IsCMSApproverDesignate)
                    return true;
                else
                {
                    if (AdminData.StateFIPS == StateFIPS)
                    {
                        if (AdminData.IsShipDirector)
                            return true;
                        else if (AdminData.IsStateAdmin)
                            return AdminData.IsStateApproverDesignate;
                        else if (AdminData.IsUserSubStateRegionalScope)
                        {
                            //The only possibility from now on, is Editor is a Sub State User.
                            foreach (UserRegionalAccessProfile adminSubStateProfile in AdminData.RegionalProfiles)
                            {
                                if (adminSubStateProfile.RegionId == SubStateIDRequested)
                                {
                                    if (adminSubStateProfile.IsAdmin && adminSubStateProfile.IsApproverDesignate)
                                        return true;
                                }
                            }
                        }

                        return false;
                    }
                    else
                        return false;
                }
            }
            else
            {
                if (AdminData.IsCMSAdmin)
                    return true;
                else
                {
                    if (AdminData.StateFIPS == StateFIPS)
                    {
                        if (AdminData.IsStateAdmin)     //includes ship director
                            return true;
                        else if (AdminData.IsUserSubStateRegionalScope)
                        {
                            //The only possibility from now on, is Editor is a Sub State User.
                            foreach (UserRegionalAccessProfile adminSubStateProfile in AdminData.RegionalProfiles)
                            {
                                if (adminSubStateProfile.RegionId == SubStateIDRequested)
                                {
                                    if (NewUserRoleIsAdmin)
                                        return adminSubStateProfile.IsAdmin && adminSubStateProfile.IsApproverDesignate;
                                    else
                                        return adminSubStateProfile.IsAdmin;
                                }
                            }
                        }

                        return false;
                    }
                    else
                        return false;
                }
            }
        }




        private static bool CanAddUserToAgencyScope(UserViewData AdminData, string StateFIPS)
        {
            //CMS Admins can create users of all state roles
            if (AdminData.IsCMSAdmin)
                return true;
            else if (AdminData.StateFIPS == StateFIPS)
            {
                if (AdminData.IsStateAdmin) return true;
                else if (AdminData.IsUserSubStateRegionalScope)
                {
                    //Admin is a Sub State User.
                    foreach (UserRegionalAccessProfile subStateUserProfile in AdminData.RegionalProfiles)
                    {
                        if (subStateUserProfile.IsAdmin)
                            return true;
                    }
                }
                else if (AdminData.IsUserSubStateRegionalScope)
                {
                    //Admin is an agency user.
                    foreach (UserRegionalAccessProfile agencyUserProfile in AdminData.RegionalProfiles)
                    {
                        if (agencyUserProfile.IsAdmin)
                            return true;
                    }
                }
            }

            return false;
        }




        public static bool CanAddUserToAgency(UserViewData AdminData, string StateFIPS, int AgencyIDRequested, bool NewUserRoleIsAdmin, bool ApproverRightsRequested)
        {
            if (ApproverRightsRequested)
            {
                //Approver rights request must be accompanied by Admin request
                //because only Admins can be approvers
                if (!NewUserRoleIsAdmin)
                    return false;


                if (AdminData.IsCMSAdmin && AdminData.IsCMSApproverDesignate)
                    return true;
                else
                {
                    if (AdminData.StateFIPS == StateFIPS)
                    {
                        if (AdminData.IsShipDirector)
                            return true;
                        else if (AdminData.IsStateAdmin)
                            return AdminData.IsStateApproverDesignate;
                        else if (AdminData.IsUserSubStateRegionalScope)
                        {
                            //The Agency of the account requested must be part of Editor's Sub State region.
                            IEnumerable<UserRegionalAccessProfile> AdminSubStateProfiles = AdminData.RegionalProfiles;
                            foreach (UserRegionalAccessProfile subStateProfile in AdminSubStateProfiles)
                            {
                                if (subStateProfile.IsAdmin)
                                {
                                    IEnumerable<Agency> AgenciesForSubState = LookupBLL.GetAgenciesForSubStateRegion(subStateProfile.RegionId);
                                    foreach (Agency agency in AgenciesForSubState)
                                    {
                                        if (agency.Id == AgencyIDRequested)
                                        {
                                            return subStateProfile.IsApproverDesignate;
                                        }
                                    }
                                }
                            }
                        }

                        return false;
                    }
                    else
                        return false;
                }
            }
            else
            {
                if (AdminData.IsCMSAdmin)
                    return true;
                else
                {
                    if (AdminData.StateFIPS == StateFIPS)
                    {
                        if (AdminData.IsStateAdmin)     //includes ship director
                            return true;
                        else if (AdminData.IsUserSubStateRegionalScope)
                        {
                            //The only possibility from now on, is Editor is a Sub State User.
                            foreach (UserRegionalAccessProfile adminSubStateProfile in AdminData.RegionalProfiles)
                            {
                                if (adminSubStateProfile.IsAdmin)
                                {
                                    IEnumerable<Agency> AgenciesForSubState = LookupBLL.GetAgenciesForSubStateRegion(adminSubStateProfile.RegionId);
                                    foreach (Agency agency in AgenciesForSubState)
                                    {
                                        if (agency.Id == AgencyIDRequested)
                                            return true;
                                    }
                                }
                            }
                        }
                        else if (AdminData.IsUserAgencyScope)
                        {
                            IEnumerable<UserRegionalAccessProfile> adminAgencyProfiles = AdminData.RegionalProfiles.Where(p => p.IsAdmin == true && p.RegionId == AgencyIDRequested);
                            //Expecting only one item after adding the Where filter to RegionalProfiles; still moving with existing code.
                            foreach (UserRegionalAccessProfile adminProfile in adminAgencyProfiles)
                            {
                                if (NewUserRoleIsAdmin) return adminProfile.IsApproverDesignate;
                                else return true;

                            }
                        }

                        return false;
                    }
                    else
                        return false;
                }
            }
        }



        #endregion











        #region "View methods: If a person can view another person's view page."

        private static bool CanViewCMSUser(UserViewData CMSUserData, UserViewData ViewerData)
        {
            //CMSAdmins can be viewed by other CMSAdmins.
            if (CMSUserData.IsCMSAdmin) return ViewerData.IsCMSAdmin;
            else
            //CMSUsers can be viewed by both CMSAdmins and CMSUsers.
            { return ViewerData.IsUserCMSScope; }
        }
        private static bool CanViewCMSRegionalUser(UserViewData CMSRegionalUserData, UserViewData ViewerData)
        {
            //All CMS Scope Users and
            //CMS Regional User of same Region can view.
            if (ViewerData.IsUserCMSScope) return true;
            else if (ViewerData.IsUserCMSRegionalScope)
            {
                //CMS Regional Users have only 1 regional profile. However, we can loop to accomodate any future expectations.
                foreach (UserRegionalAccessProfile userProfile in CMSRegionalUserData.RegionalProfiles)
                {
                    if (ViewerData.RegionalProfiles.Where(p => p.RegionId == userProfile.RegionId).FirstOrDefault() != null)
                        return true;
                }
            }

            //All other scopes fail.
            return false;
        }
        private static bool CanViewStateUser(UserViewData StateUserData, UserViewData ViewerData)
        {

            if (ViewerData.IsUserCMSScope) return true;
            else if (ViewerData.IsUserCMSRegionalScope)
            {
                //CMS Regional Users have only 1 regional profile. However, we can loop to accomodate any future expectations.
                foreach (UserRegionalAccessProfile profile in ViewerData.RegionalProfiles)
                {
                    IEnumerable<string> StatesForCMSRegion = LookupBLL.GetStatesForCMSRegion(profile.RegionId);
                    if (StatesForCMSRegion.Contains(StateUserData.StateFIPS))
                        return true;
                }
            }
            else if (ViewerData.IsUserStateScope && (ViewerData.StateFIPS == StateUserData.StateFIPS))
            {
                //States can have only 1 ship director. 
                //However, this logic will ensure that users viewing their own profile are allowed to do so.
                if (StateUserData.IsShipDirector)
                    return ViewerData.IsShipDirector;
                else if (StateUserData.IsStateAdmin)
                    return ViewerData.IsStateAdmin;
                else if (StateUserData.IsUserStateScope) //State User. This is obvious, still, will have it here so bad calls are caught.
                    return true;
            }
            //All others get knocked out.
            return false;
        }

        private static bool CanViewMultiSubStateUser(UserViewData SubStateUserData, UserViewData ViewerData)
        {
            if (ViewerData.IsUserCMSScope) return true;
            else if (ViewerData.IsUserCMSRegionalScope)
            {
                //CMS Regional Users have only 1 regional profile. However, we can loop to accomodate any future expectations.
                foreach (UserRegionalAccessProfile profile in ViewerData.RegionalProfiles)
                {
                    IEnumerable<string> StatesForCMSRegion = LookupBLL.GetStatesForCMSRegion(profile.RegionId);
                    if (StatesForCMSRegion.Contains(SubStateUserData.StateFIPS))
                        return true;
                }
            }
            else //State Level Users follow
            {
                //Kick out other state users
                if ((ViewerData.StateFIPS != SubStateUserData.StateFIPS))
                    return false;

                //State Scope Users must be Admins
                if (ViewerData.IsUserStateScope)
                    return true;

                //Kickout agency users.
                if (ViewerData.IsUserSubStateRegionalScope)
                {
                    //The only possibility from now on, is Editor is a Sub State User.
                    foreach (UserRegionalAccessProfile subStateUserProfile in SubStateUserData.RegionalProfiles)
                    {
                        foreach (UserRegionalAccessProfile viewerUserProfile in ViewerData.RegionalProfiles)
                        {
                            if (viewerUserProfile.RegionId == subStateUserProfile.RegionId)
                            {
                                //This logic is not perfect but close to what is possible.
                                //If the SubStateUser is Admin, then we need to ensure that User cannot see it.
                                //However, if the Viewer is also Admin, he can see it. 
                                //If SubState User is a User, any one in same SubState can view.
                                if (subStateUserProfile.IsAdmin)
                                {
                                    if (viewerUserProfile.IsAdmin) return true;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            //All other users are out
            return false;
        }


        private static bool CanViewMultiAgencyUser(UserViewData AgencyUserData, UserViewData ViewerData)
        {
            if (ViewerData.IsUserCMSScope) return true;
            else if (ViewerData.IsUserCMSRegionalScope)
            {
                //CMS Regional Users have only 1 regional profile. However, we can loop to accomodate any future expectations.
                foreach (UserRegionalAccessProfile profile in ViewerData.RegionalProfiles)
                {
                    IEnumerable<string> StatesForCMSRegion = LookupBLL.GetStatesForCMSRegion(profile.RegionId);
                    if (StatesForCMSRegion.Contains(AgencyUserData.StateFIPS))
                        return true;
                }
            }
            else //State Level Users follow
            {
                //Kick out other state users
                if ((ViewerData.StateFIPS != AgencyUserData.StateFIPS))
                    return false;

                //State Scope Users must be Admins
                if (ViewerData.IsUserStateScope)
                    return true;

                //Find if Sub States fall under one of the Agency of the User.
                if (ViewerData.IsUserSubStateRegionalScope)
                {
                    IEnumerable<UserRegionalAccessProfile> viewerSubStateProfiles = ViewerData.RegionalProfiles;

                    IEnumerable<UserRegionalAccessProfile> userAgencyProfiles = AgencyUserData.RegionalProfiles;
                    foreach (UserRegionalAccessProfile viewerSubStateProfile in viewerSubStateProfiles)
                    {
                        IEnumerable<Agency> agenciesForSubState = LookupBLL.GetAgenciesForSubStateRegion(viewerSubStateProfile.RegionId);

                        foreach (UserRegionalAccessProfile userAgencyProfile in userAgencyProfiles)
                        {
                            IEnumerable<Agency> matchingAgencies = agenciesForSubState.Where(ag => ag.Id == userAgencyProfile.RegionId);
                            if (matchingAgencies != null && matchingAgencies.Count() > 0)
                            {
                                return true;
                            }
                        }
                    }
                }

                //Ofcourse, at this point, it get down to Agency User accessing Agency User
                //Still we will check the scope of the ViewerData so bad calls are caught.
                if (ViewerData.IsUserAgencyScope)
                {
                    IEnumerable<UserRegionalAccessProfile> viewerAgencyProfiles = ViewerData.RegionalProfiles;
                    IEnumerable<UserRegionalAccessProfile> userAgencyProfiles = AgencyUserData.RegionalProfiles;

                    foreach (UserRegionalAccessProfile viewerAgencyProfile in viewerAgencyProfiles)
                    {
                        foreach (UserRegionalAccessProfile userAgencyProfile in userAgencyProfiles)
                        {
                            if (userAgencyProfile.RegionId == viewerAgencyProfile.RegionId)
                            {
                                if (userAgencyProfile.IsAdmin)
                                {
                                    if (viewerAgencyProfile.IsAdmin) return true;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

            }

            //All other users are out
            return false;

        }


        public static bool CanViewUserProfile(UserViewData UserData, UserViewData ViewerData)
        {
            switch (UserData.Scope)
            {
                case Scope.CMS:
                    return CanViewCMSUser(UserData, ViewerData);
                case Scope.CMSRegional:
                    return CanViewCMSRegionalUser(UserData, ViewerData);
                case Scope.State:
                    return CanViewStateUser(UserData, ViewerData);
                case Scope.SubStateRegion:
                    return CanViewMultiSubStateUser(UserData, ViewerData);
                case Scope.Agency:
                    return CanViewMultiAgencyUser(UserData, ViewerData);
                default:
                    throw new ShiptalkException("Unknown User Scope.", false);
            }
        }
        #endregion




        #region "Can? Edit a particular User at a particular agency or sub state"
        public static bool CanEditSubStateUser(int SubStateId, bool UserIsAdmin, string StateFIPSOfSubState, bool UserIsApproverDesignate, UserViewData AdminData)
        {
            if (AdminData.IsCMSLevel)
            {
                return AdminData.IsCMSAdmin;
            }
            else //State Level Users follow
            {
                //Kick out other state users
                if ((AdminData.StateFIPS != StateFIPSOfSubState))
                    return false;

                //State Scope Users must be Admins
                if (AdminData.Scope.IsEqual(Scope.State))
                    return AdminData.IsStateAdmin;
                else if (AdminData.Scope.IsEqual(Scope.SubStateRegion))
                {
                    IEnumerable<UserRegionalAccessProfile> adminProfiles = AdminData.RegionalProfiles.Where(p => p.IsAdmin == true);
                    foreach (UserRegionalAccessProfile adminUserProfile in adminProfiles)
                    {
                        if (adminUserProfile.RegionId == SubStateId)
                        {
                            if (UserIsApproverDesignate) return false;
                            else if (UserIsAdmin) return adminUserProfile.IsApproverDesignate;
                            else return adminUserProfile.IsAdmin;  //Which is obvious as return true;
                        }
                    }
                }

            }

            return false;
        }


        public static bool CanEditAgencyUser(int AgencyId, bool UserIsAdmin, string StateFIPSOfAgency, bool UserIsApproverDesignate, UserViewData AdminData)
        {

            if (AdminData.IsCMSLevel)
            {
                return AdminData.IsCMSAdmin;
            }
            else //State Level Users follow
            {
                //Kick out other state users
                if ((AdminData.StateFIPS != StateFIPSOfAgency))
                    return false;

                //State Scope Users must be Admins
                if (AdminData.Scope.IsEqual(Scope.State))
                    return AdminData.IsStateAdmin;
                else if (AdminData.Scope.IsEqual(Scope.SubStateRegion))
                {
                    IEnumerable<UserRegionalAccessProfile> AdminSubStateProfiles = AdminData.RegionalProfiles.Where(p => p.IsAdmin == true);
                    foreach (UserRegionalAccessProfile adminProfile in AdminSubStateProfiles)
                    {
                        IEnumerable<Agency> AgenciesForSubState = LookupBLL.GetAgenciesForSubStateRegion(adminProfile.RegionId);
                        Agency matchingAgency = AgenciesForSubState.Where(ag => (ag.Id == AgencyId) && (ag.IsActive == true)).FirstOrDefault();
                        if (matchingAgency != null)
                        {
                            return true;
                        }
                    }
                }
                else if (AdminData.Scope.IsEqual(Scope.Agency))
                {
                    //The Agency of the account requested must be part of Editor's Sub State region.
                    IEnumerable<UserRegionalAccessProfile> AdminAgencyProfiles = AdminData.RegionalProfiles.Where(p => p.IsAdmin == true && p.RegionId == AgencyId);
                    //We expect only one item in AdminAgencyProfiles after applying Where filter to regional profiles, but still living with same code.
                    foreach (UserRegionalAccessProfile agencyProfile in AdminAgencyProfiles)
                    {
                        //Approvers can be edited by only higher scope admins.
                        if (UserIsApproverDesignate) return false;
                        else if (UserIsAdmin) return agencyProfile.IsApproverDesignate;
                        else return true;
                    }
                }

            }

            return false;
        }
        #endregion

        #region "Can? view a particular User at a particular agency or sub state"
        public static bool CanViewSubStateUser(int SubStateId, bool UserIsAdmin, string StateFIPSOfSubState, UserViewData ViewerData)
        {
            if (ViewerData.IsUserCMSScope) return true;
            else if (ViewerData.IsUserCMSRegionalScope)
            {
                //CMS Regional Users have only 1 regional profile. However, we can loop to accomodate any future expectations.
                foreach (UserRegionalAccessProfile profile in ViewerData.RegionalProfiles)
                {
                    IEnumerable<string> StatesForCMSRegion = LookupBLL.GetStatesForCMSRegion(profile.RegionId);
                    if (StatesForCMSRegion.Contains(StateFIPSOfSubState))
                        return true;
                }
            }
            else //State Level Users follow
            {
                //Kick out other state users
                if ((ViewerData.StateFIPS != StateFIPSOfSubState))
                    return false;

                //State Scope Users must be Admins
                if (ViewerData.IsUserStateScope)
                    return true;

                //Kickout agency users.
                if (ViewerData.IsUserSubStateRegionalScope)
                {
                    foreach (UserRegionalAccessProfile viewerUserProfile in ViewerData.RegionalProfiles)
                    {
                        if (viewerUserProfile.RegionId == SubStateId)
                        {
                            //This logic is not perfect but close to what is possible.
                            //If the SubStateUser is Admin, then we need to ensure that User cannot see it.
                            //However, if the Viewer is also Admin, he can see it. 
                            //If SubState User is a User, any one in same SubState can view.
                            if (UserIsAdmin)
                            {
                                if (viewerUserProfile.IsAdmin) return true;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            //All other users are out
            return false;
        }


        public static bool CanViewAgencyUser(int AgencyId, bool UserIsAdmin, string StateFIPSOfAgency, UserViewData ViewerData)
        {

            if (ViewerData.IsUserCMSScope) return true;
            else if (ViewerData.IsUserCMSRegionalScope)
            {
                //CMS Regional Users have only 1 regional profile. However, we can loop to accomodate any future expectations.
                foreach (UserRegionalAccessProfile profile in ViewerData.RegionalProfiles)
                {
                    IEnumerable<string> StatesForCMSRegion = LookupBLL.GetStatesForCMSRegion(profile.RegionId);
                    if (StatesForCMSRegion.Contains(StateFIPSOfAgency))
                        return true;
                }
            }
            else //State Level Users follow
            {
                //Kick out other state users
                if ((ViewerData.StateFIPS != StateFIPSOfAgency))
                    return false;

                //State Scope Users must be Admins
                if (ViewerData.IsUserStateScope)
                    return true;

                //Find if Sub States fall under one of the Agency of the User.
                if (ViewerData.IsUserSubStateRegionalScope)
                {
                    IEnumerable<UserRegionalAccessProfile> viewerSubStateProfiles = ViewerData.RegionalProfiles;
                    foreach (UserRegionalAccessProfile viewerSubStateProfile in viewerSubStateProfiles)
                    {
                        IEnumerable<Agency> agenciesForSubState = LookupBLL.GetAgenciesForSubStateRegion(viewerSubStateProfile.RegionId);
                        IEnumerable<Agency> matchingAgencies = agenciesForSubState.Where(ag => (ag.Id == AgencyId && ag.IsActive));
                        if (matchingAgencies != null && matchingAgencies.Count() > 0)
                        {
                            return true;
                        }

                    }
                }

                //Ofcourse, at this point, it get down to Agency User accessing Agency User
                //Still we will check the scope of the ViewerData so bad calls are caught.
                if (ViewerData.IsUserAgencyScope)
                {
                    IEnumerable<UserRegionalAccessProfile> viewerAgencyProfiles = ViewerData.RegionalProfiles;
                    foreach (UserRegionalAccessProfile viewerAgencyProfile in viewerAgencyProfiles)
                    {
                        if (viewerAgencyProfile.RegionId == AgencyId)
                        {
                            if (UserIsAdmin)
                            {
                                if (viewerAgencyProfile.IsAdmin) return true;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }

            }

            //All other users, if any, are out
            return false;
        }
        #endregion


        #region "IsAdmin at any region/scope"
        public static bool IsAdmin(UserViewData AdminViewData, State UserState)
        {
            if (AdminViewData.IsCMSLevel) return AdminViewData.IsCMSAdmin;
            else
            {
                if (AdminViewData.StateFIPS != UserState.Code)
                    return false;
                else
                {
                    if (AdminViewData.IsUserStateScope)
                        return AdminViewData.IsAdmin;
                    else
                    {
                        foreach (UserRegionalAccessProfile profile in AdminViewData.RegionalProfiles)
                        {
                            if (profile.IsAdmin && profile.IsActive)
                                return true;
                        }
                        return false;
                    }
                }
            }
        }
        #endregion
    }
}
