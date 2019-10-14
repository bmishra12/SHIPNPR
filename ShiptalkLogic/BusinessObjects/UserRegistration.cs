using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShiptalkLogic.BusinessObjects
{
    public sealed class UserRegistration : UserProfile
    {
        public string PrimaryEmail { get; set; }
        public string Password { get; set; }
        public string ClearPassword { get; set; } //sammit
        public Role RoleRequested { get; set; }
        public UserRegionalAccessProfile UserRegionalAccessProfile { get; set; }
        public int? OldShipUserId { get; set; }
        public bool IsRegistrationRequest { get; set; }
        public int? RegisteredByUserId { get; set; }

        public bool IsApproverDesignate { get; set; }
        public bool IsStateSuperEditor { get; set; }
        
        public UserRegistration()
        {
            UserRegionalAccessProfile = new UserRegionalAccessProfile();
            IsRegistrationRequest = false;      //Default
        }

        //public int CMSRegionId { get; set; }
        //public int AgencyId { get; set; }
        //public int SubStateRegionId { get; set; }
        public string StateFIPS { get; set; }
        public bool IsShipDirector { get; set; }
        //public List<UserDescriptor> UserDescriptorList = new List<UserDescriptor>();
        //public List<UserRegion> UserRegionalAccessProfileList { get; set; }
    }
}
