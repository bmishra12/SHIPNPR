using System;
using System.Collections.Generic;
using System.Text;

namespace ShiptalkLogic.BusinessObjects
{

    public class UserAccount
    {
        public UserAccount() { }

        public UserAccount(int UserId) { }

        public int UserId { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }

        public string PrimaryEmail { get; set; }

        public short ScopeId { get; set; }

        public Scope Scope
        {
            get { return ScopeId.ToEnumObject<Scope>(); }
        }

        public string RegionName { get; set; }

        public int? PrimaryDescriptor { get; set; }

        public bool? IsApproverDesignate { get; set; }

        //Need to look for regional profiles for agency/substate for IsSuperDataEditor rights at agency/substate level
        public bool IsStateSuperDataEditor { get; set; }

        //public List<UserRegionalAccessProfile> UserRegionalAccessProfileList { get; set; }

        public string StateFIPS { get; set; }

        public string CounselingLocation { get; set; }
        
        public string CounselingCounty { get; set; }
        
        public bool IsShipDirector { get; set; }

        public string MedicareUniqueId { get; set; }


        public bool IsCMSScope
        {
            get
            {
                return Scope.IsEqual(Scope.CMS);
            }
        }
        
        public bool IsCMSRegionalScope
        {
            get
            {
                return Scope.IsEqual(Scope.CMSRegional);
            }
        }
        /// <summary>
        /// Level is used to indicate 2 levels: State Level and CMS Level
        /// StateLevel Users are Users of State Scope, Sub State Scope and Agency Scope
        /// CMS Level Users are Users of CMS Scope, CMS Regional Scope
        /// </summary>
        /// <returns></returns>
        public bool IsCMSLevel
        {
            get
            {
                return Scope.IsHigherOrEqualTo(Scope.CMSRegional);
            }
        }

        /// <summary>
        /// Level is used to indicate 2 levels: State Level and CMS Level
        /// StateLevel Users are Users of State Scope, Sub State Scope and Agency Scope
        /// CMS Level Users are Users of CMS Scope, CMS Regional Scope
        /// </summary>
        /// <returns></returns>
        public bool IsStateLevel
        {
            get
            {
                return Scope.IsLowerOrEqualTo(Scope.State);
            }
        }
        public bool IsStateScope
        {
            get
            {
                return Scope.IsEqual(Scope.State);
            }
        }
        public bool IsStateAdmin
        {
            get
            {
                return Scope.IsEqual(Scope.State) && IsAdmin;
            }
        }
        public bool IsStateUser
        {
            get
            {
                return Scope.IsEqual(Scope.State) && (!IsAdmin);
            }
        }


        //        public List<int> DescriptorList
        //        {
        //            get
        //            {
        //                return _DescriptorList;
        //}
        //            set { _DescriptorList = value; }
        //        }


    }

    //public class User
    //{

    //    public User() { }

    //    public UserAccount UserAccount { get; set; }

    //    public UserProfile UserProfile { get; set; }


    //    //        public List<int> DescriptorList
    //    //        {
    //    //            get
    //    //            {
    //    //                return _DescriptorList;
    //    //}
    //    //            set { _DescriptorList = value; }
    //    //        }


    //}
}
