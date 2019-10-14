using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;
using ShiptalkLogic.BusinessObjects.UI;

namespace ShiptalkLogic.BusinessObjects
{
    public sealed class User : IModified, IIsActive
    {
        private UserAccount _UserAccount;
        private UserProfile _UserProfile;
        
        static User()
        {
            CreateMap_User_UserSummaryViewData();
            //CreateMap_User_PendingApprovalUserViewData();
            CreateMap_User_UserViewData();
            CreateMap_User_UserSearchViewData();
        }

        public UserAccount UserAccount { 
            get {
                if (_UserAccount == null)
                    _UserAccount = new UserAccount();
                return _UserAccount;
            }
            set
            {
                _UserAccount = value;
            }
        }

        public UserProfile UserProfile
        {
            get
            {
                if (_UserProfile == null)
                    _UserProfile = new UserProfile();
                return _UserProfile;
            }
            set
            {
                _UserProfile = value;
            }
        }

        private List<UserRegionalAccessProfile> _UserRegionalProfiles;
        public List<UserRegionalAccessProfile> UserRegionalProfiles
        {
            get
            {
                if (_UserRegionalProfiles == null)
                    _UserRegionalProfiles = new List<UserRegionalAccessProfile>();
                return _UserRegionalProfiles;
            }
            set
            {
                _UserRegionalProfiles = value;
            }
        }

        
        #region Mapper from/to Business object(s)
        public static void CreateMap_User_UserSummaryViewData()
        {
            Mapper.CreateMap<User, UserSummaryViewData>()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserAccount.UserId))
               .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.UserAccount.IsAdmin))
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.UserProfile.FirstName))
               .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.UserProfile.MiddleName))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.UserProfile.LastName))
               .ForMember(dest => dest.PrimaryEmail, opt => opt.MapFrom(src => src.UserAccount.PrimaryEmail))
               .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.UserAccount.IsActive))
               .ForMember(dest => dest.State, opt => opt.MapFrom(src => new State(src.UserAccount.StateFIPS)))
               .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src.UserAccount.Scope))
               .ForMember(dest => dest.MedicareUniqueId, opt => opt.MapFrom(src => src.UserAccount.MedicareUniqueId))

               .ForMember(dest => dest.IsShipDirector, opt => opt.MapFrom(src => src.UserAccount.IsShipDirector));
        }

        public static void CreateMap_User_UserSearchViewData()
        {
            Mapper.CreateMap<User, UserSearchViewData>()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserAccount.UserId))
               .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.UserAccount.IsAdmin))
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.UserProfile.FirstName))
               .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.UserProfile.MiddleName))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.UserProfile.LastName))
               .ForMember(dest => dest.PrimaryEmail, opt => opt.MapFrom(src => src.UserAccount.PrimaryEmail))
               .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.UserAccount.IsActive))
               .ForMember(dest => dest.StateFIPS, opt => opt.MapFrom(src => src.UserAccount.StateFIPS))
               .ForMember(dest => dest.MedicareUniqueId, opt => opt.MapFrom(src => src.UserAccount.MedicareUniqueId))
               .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src.UserAccount.Scope))
               .ForMember(dest => dest.IsShipDirector, opt => opt.MapFrom(src => src.UserAccount.IsShipDirector))
               .ForMember(dest => dest.UserRoleText, opt => opt.MapFrom(src => GetSpecialRoleText(src.UserAccount)))
               .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => (src.UserRegionalProfiles != null && src.UserRegionalProfiles.Count() > 0 ? src.UserRegionalProfiles[0].RegionName : string.Empty)));
        }

        private static string GetSpecialRoleText(UserAccount accountInfo)
        {
            if (accountInfo.IsShipDirector)
                return "Ship Director";
            else if (accountInfo.IsCMSLevel && accountInfo.IsApproverDesignate.HasValue)
                return "CMS Admin, Approver";
            else if (accountInfo.IsStateAdmin && accountInfo.IsApproverDesignate.HasValue)
                return "State Administrator, Approver";
            else
                return string.Empty;
        }


        public static void CreateMap_User_UserViewData()
        {
            Mapper.CreateMap<User, UserViewData>()
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserAccount.UserId))
              .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.UserAccount.IsAdmin))
              .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.UserAccount.IsActive))
              .ForMember(dest => dest.ScopeId, opt => opt.MapFrom(src => src.UserAccount.ScopeId))
              .ForMember(dest => dest.StateFIPS, opt => opt.MapFrom(src => src.UserAccount.StateFIPS))
              .ForMember(dest => dest.CounselingLocation, opt => opt.MapFrom(src => src.UserAccount.CounselingLocation))
              .ForMember(dest => dest.PrimaryEmail, opt => opt.MapFrom(src => src.UserAccount.PrimaryEmail))
              .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.UserProfile.FirstName))
              .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.UserProfile.MiddleName))
              .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.UserProfile.LastName))
              .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.UserProfile.NickName))
              .ForMember(dest => dest.Honorifics, opt => opt.MapFrom(src => src.UserProfile.Honorifics))
              .ForMember(dest => dest.Suffix, opt => opt.MapFrom(src => src.UserProfile.Suffix))
              .ForMember(dest => dest.PrimaryPhone, opt => opt.MapFrom(src => src.UserProfile.PrimaryPhone))
              .ForMember(dest => dest.SecondaryEmail, opt => opt.MapFrom(src => src.UserProfile.SecondaryEmail))
              .ForMember(dest => dest.SecondaryPhone, opt => opt.MapFrom(src => src.UserProfile.SecondaryPhone))
              //Included by Lavanya
              .ForMember(dest => dest.TempPrimaryEmail, opt => opt.MapFrom(src => src.UserProfile.TempPrimaryEmail))
              .ForMember(dest => dest.EmailChangeRequestDate, opt => opt.MapFrom(src => src.UserProfile.EmailChangeRequestDate))
              //end
              .ForMember(dest => dest.IsShipDirector, opt => opt.MapFrom(src => src.UserAccount.IsShipDirector))
              .ForMember(dest => dest.RegionalProfiles, opt => opt.MapFrom(src => src.UserRegionalProfiles))
              .ForMember(dest => dest.CounselingCounty, opt => opt.MapFrom(src => src.UserAccount.CounselingCounty))
              .ForMember(dest => dest.IsStateApproverDesignate, opt => opt.MapFrom(src => (src.UserAccount.IsStateScope ? src.UserAccount.IsApproverDesignate : false) ))
              .ForMember(dest => dest.IsCMSApproverDesignate, opt => opt.MapFrom(src => (src.UserAccount.IsCMSScope ? src.UserAccount.IsApproverDesignate : false) ))
              .ForMember(dest => dest.IsStateSuperDataEditor , opt => opt.MapFrom(src => (src.UserAccount.IsStateScope ? src.UserAccount.IsStateSuperDataEditor : false)));
        }

        

        #endregion

      
        #region Implementation of IModified

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        #endregion

        #region Implementation of IIsActive

        public bool IsActive { get; set; }
        public DateTime? ActiveInactiveDate { get; set; }

        #endregion














        //private string GetPrimaryRegionName()
        //{
        //    if (_UserRegionalProfiles != null)
        //    {
        //        UserRegionalAccessProfile userProfile = (from profile in _UserRegionalProfiles where profile.IsDefaultRegion == true select profile).FirstOrDefault();
        //        if (userProfile == null)
        //        {
        //            if (_UserRegionalProfiles.Count() > 0)
        //                userProfile = _UserRegionalProfiles.First();
        //        }

        //        if (userProfile != null)
        //            return userProfile.RegionName;
        //    }

        //    return string.Empty;
        //}


        //public static void CreateMap_User_PendingApprovalUserViewData()
        //{
        //    Mapper.CreateMap<User, PendingApprovalUserViewData>()
        //      .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserAccount.UserId))
        //      .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.UserAccount.IsAdmin))
        //      .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.UserAccount.IsActive))
        //      .ForMember(dest => dest.ScopeId, opt => opt.MapFrom(src => src.UserAccount.ScopeId))
        //      .ForMember(dest => dest.StateFIPS, opt => opt.MapFrom(src => src.UserAccount.StateFIPS))
        //      .ForMember(dest => dest.CounselingLocation, opt => opt.MapFrom(src => src.UserAccount.CounselingLocation))
        //      .ForMember(dest => dest.PrimaryEmail, opt => opt.MapFrom(src => src.UserAccount.PrimaryEmail))
        //      .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.UserProfile.FirstName))
        //      .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.UserProfile.MiddleName))
        //      .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.UserProfile.LastName))
        //      .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.UserProfile.NickName))
        //      .ForMember(dest => dest.Honorifics, opt => opt.MapFrom(src => src.UserProfile.Honorifics))
        //      .ForMember(dest => dest.Suffix, opt => opt.MapFrom(src => src.UserProfile.Suffix))
        //      .ForMember(dest => dest.PrimaryPhone, opt => opt.MapFrom(src => src.UserProfile.PrimaryPhone))
        //      .ForMember(dest => dest.SecondaryEmail, opt => opt.MapFrom(src => src.UserProfile.SecondaryEmail))
        //      .ForMember(dest => dest.SecondaryPhone, opt => opt.MapFrom(src => src.UserProfile.SecondaryPhone))
        //      .ForMember(dest => dest.PrimaryRegionName, opt => opt.MapFrom(src => src.GetPrimaryRegionName()));
        //}
    }
}
