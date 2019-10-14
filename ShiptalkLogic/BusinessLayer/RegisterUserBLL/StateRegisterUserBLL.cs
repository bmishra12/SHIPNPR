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

    internal class StateRegisterUserBLL : RegisterUserImplBase
    {
        public StateRegisterUserBLL(UserRegistration usrRegObj) : base(usrRegObj) { }

        public override void Validate()
        {
            this.ErrorMessage = string.Empty;

            if (string.IsNullOrEmpty(UserRegistrationData.StateFIPS))
                throw new ShiptalkException("State must be captured for State level User registration.", false, new ArgumentNullException());
        }


        protected override void GrantAccessToRegion()
        {
        }

        protected override void DoPostProfileCreationTasks()
        {
            if (UserRegistrationData.IsShipDirector)
            {
                int AgencyId = Constants.Defaults.DefaultValues.AgencyIdForNonAgencyUsers;
                int DescriptorId = Descriptor.ShipDirector.EnumValue<int>();

                int AddedBy = -1;
                if (this.UserRegistrationData.IsRegistrationRequest)
                    AddedBy = UserId.Value;
                else
                    AddedBy = this.UserRegistrationData.RegisteredByUserId.Value;
                
                if (!base.SaveDescriptor(DescriptorId, AgencyId, AddedBy))
                    this.ErrorMessage = "An error occured while saving the SHIP Director permissions for User. Please contact support for assistance.";
            }
        }

    }


  
}
