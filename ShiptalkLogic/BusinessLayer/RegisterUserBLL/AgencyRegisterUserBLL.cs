using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Transactions;

using ShiptalkLogic.BusinessObjects;
using ShiptalkCommon;
using ShiptalkLogic.DataLayer;



namespace ShiptalkLogic.BusinessLayer
{

    internal class AgencyRegisterUserBLL : RegisterUserImplBase
    {
        private int AgencyId;
        public AgencyRegisterUserBLL(UserRegistration usrRegObj)
            : base(usrRegObj)
        {
            AgencyId = usrRegObj.UserRegionalAccessProfile.RegionId;
        }

        public override void Validate()
        {
            if (this.AgencyId == 0)
                throw new ShiptalkException("Agency must be captured for Agency User registration.", false, new ArgumentNullException());

        }

        protected override void GrantAccessToRegion()
        {
            //TODO: Need to refactor in future. Just use the UserRegionalAccessProfile object of UserRegistrationData

            UserRegionalAccessProfile AgencyProfile = new UserRegionalAccessProfile();
            AgencyProfile.UserId = UserId.Value;
            AgencyProfile.RegionId = this.AgencyId;
            AgencyProfile.IsAdmin = UserRegistrationData.RoleRequested.IsAdmin;
            AgencyProfile.IsDefaultRegion = true;       //First Agency set during registration is assumed default.
                        
            int AccessGrantedBy = -1;
            if (UserRegistrationData.IsRegistrationRequest)
            {
                AccessGrantedBy = UserId.Value;
                AgencyProfile.IsApproverDesignate = false;
            }
            else
            {
                AccessGrantedBy = UserRegistrationData.RegisteredByUserId.Value;
                AgencyProfile.IsApproverDesignate = UserRegistrationData.UserRegionalAccessProfile.IsApproverDesignate;
            }

            if (!UserAgencyDAL.GrantAgencyAccessToUser(AgencyProfile, AccessGrantedBy))
                this.ErrorMessage = "Sorry. We encountered an error while saving the agency information.";

            //if (!UserAgencyDAL.GrantAgencyAccessToUser(UserId.Value, this.AgencyId, UserRegistrationData.RoleRequested.IsAdmin, true, UserId.Value))
            //    this.ErrorMessage = "Sorry. We encountered an error while saving the agency information.";
        }

        protected override void DoPostProfileCreationTasks()
        {
        }

    }


}
