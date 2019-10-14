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

    internal class SubStateRegisterUserBLL : RegisterUserImplBase
    {
        private int SubStateRegionId;

        public SubStateRegisterUserBLL(UserRegistration usrRegObj) : base(usrRegObj) {
            SubStateRegionId = usrRegObj.UserRegionalAccessProfile.RegionId;
        }

        public override void Validate()
        {
            if (this.SubStateRegionId == 0)
                throw new ShiptalkException("Sub State Region must be captured for Sub State Regional User registration.", false, new ArgumentNullException());
        }

        protected override void GrantAccessToRegion()
        {
            //TODO: Need to refactor in future. Just use the UserRegionalAccessProfile object of UserRegistrationData
            UserRegionalAccessProfile UserSubStateProfile = new UserRegionalAccessProfile();
            UserSubStateProfile.UserId = UserId.Value;
            UserSubStateProfile.RegionId = this.SubStateRegionId;
            UserSubStateProfile.IsAdmin = UserRegistrationData.RoleRequested.IsAdmin;
            UserSubStateProfile.IsDefaultRegion = true;
            
            int GrantedBy = 0;
            if (UserRegistrationData.IsRegistrationRequest)
            {
                GrantedBy = UserId.Value;
                UserSubStateProfile.IsApproverDesignate = false;
            }
            else
            {
                GrantedBy = UserRegistrationData.RegisteredByUserId.Value;
                UserSubStateProfile.IsApproverDesignate = UserRegistrationData.UserRegionalAccessProfile.IsApproverDesignate;
            }

            //if (!UserSubStateRegionBLL.GrantSubStateRegionAccessToUser(UserId.Value, this.SubStateRegionId, UserRegistrationData.RoleRequested.IsAdmin, UserId.Value))
            if(!UserSubStateRegionBLL.GrantSubStateRegionAccessToUser(UserSubStateProfile, GrantedBy))
                this.ErrorMessage = "Sorry. We encountered an error while saving the Sub State Region information.";
        }

        protected override void DoPostProfileCreationTasks()
        {
        }

        

    }


   
}
