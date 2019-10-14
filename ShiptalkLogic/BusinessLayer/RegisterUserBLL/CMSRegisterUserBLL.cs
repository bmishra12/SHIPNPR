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
    internal class CMSRegisterUserBLL : RegisterUserImplBase
    {

        private int CMSRegionId;

        public CMSRegisterUserBLL(UserRegistration usrRegObj) : base(usrRegObj) {
            if(usrRegObj.RoleRequested.scope == Scope.CMSRegional)
                CMSRegionId = usrRegObj.UserRegionalAccessProfile.RegionId;
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(UserRegistrationData.StateFIPS))
                UserRegistrationData.StateFIPS = State.GetStateFIPSForCMS();

            if (UserRegistrationData.RoleRequested.scope == Scope.CMSRegional)
            {
                if (this.CMSRegionId == 0)
                    throw new ShiptalkException("CMS Region must be captured for CMS Regional registration.", false, new ArgumentNullException());
            }
        }


        protected override void GrantAccessToRegion()
        {
            if (this.UserRegistrationData.RoleRequested.scope == Scope.CMSRegional)
            {
                int GrantedBy = -1;
                if (this.UserRegistrationData.IsRegistrationRequest)
                    GrantedBy = UserId.Value;
                else
                    GrantedBy = this.UserRegistrationData.RegisteredByUserId.Value;

                if(!UserDAL.GrantCMSRegionAccessToUser(UserId.Value, this.CMSRegionId, GrantedBy))
                    this.ErrorMessage = "Sorry. We encountered an error while saving the CMS Region information.";
            }
        }

        protected override void DoPostProfileCreationTasks()
        {
        }

        

    }
}
