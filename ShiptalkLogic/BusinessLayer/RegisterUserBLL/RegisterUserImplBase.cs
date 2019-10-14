
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Transactions;

using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkCommon;
using ShiptalkLogic.DataLayer;


namespace ShiptalkLogic.BusinessLayer
{

    public interface IRegisterUser
    {
        bool Save();
        void ValidateData();
        string ErrorMessage { get; set; }
        bool IsValid { get; }
        int? UserId { get; set; }
    }

    public abstract class RegisterUserImplBase : IRegisterUser
    {

        protected UserRegistration UserRegistrationData { get; private set; }

        public int? UserId { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsValid
        {
            get
            {
                return string.IsNullOrEmpty(ErrorMessage);
            }
        }

        private bool _IsDirtyData = true;
        private bool IsDirtyData { get { return _IsDirtyData; } set { _IsDirtyData = value; } }

        private int? _UserId;


        protected RegisterUserImplBase(UserRegistration userRegObj)
        {
            //Assign the object 
            this.UserRegistrationData = userRegObj;
        }

        public static IRegisterUser CreateObject(UserRegistration userRegObj)
        {
            IRegisterUser registrationProviderObj = null;
            switch (userRegObj.RoleRequested.scope)
            {
                case Scope.CMS:
                case Scope.CMSRegional:
                    registrationProviderObj = new CMSRegisterUserBLL(userRegObj);
                    break;
                case Scope.State:
                    registrationProviderObj = new StateRegisterUserBLL(userRegObj);
                    break;
                case Scope.SubStateRegion:
                    registrationProviderObj = new SubStateRegisterUserBLL(userRegObj);
                    break;
                case Scope.Agency:
                    registrationProviderObj = new AgencyRegisterUserBLL(userRegObj);
                    break;
                default:
                    throw new NotImplementedException("Unknown registration scope requested.");
            }
            return registrationProviderObj;
        }

        protected void SendVerifyEmailNotification(bool IsCreatedByUserRegistration)
        {
            string ErrorMessage;
            bool mailSent = RegistrationEmails.SendEmailVerificationNotification(
                IsCreatedByUserRegistration, 
                this.UserId.Value, 
                UserRegistrationData.RegisteredByUserId, 
                out ErrorMessage
                );

            if (!mailSent)
                this.ErrorMessage = ErrorMessage;
        }


        public bool Save()
        {
            bool IsSuccess = false;
            //If Validation was successful this.IsValid will be true.
            if (!this.IsDirtyData)
            {
                //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                //{
                    //if ( UserDAL.RegisterUser(UserRegistrationData, null, out _UserId))
                    if (UserBLL.RegisterUser(UserRegistrationData, out _UserId))
                    {
                        //Important : Set the UserId here before verifying email.
                        this.UserId = _UserId;
                        this.GrantAccessToRegion();

                        if (this.IsValid)
                        {
                            if (this.UserRegistrationData.IsRegistrationRequest)
                                this.SaveDescriptors(this.UserId.Value);
                            else
                                this.SaveDescriptors(this.UserRegistrationData.RegisteredByUserId.Value);
                        }

                        if (this.IsValid)
                            this.DoPostProfileCreationTasks();

                        if (this.IsValid)
                            SendVerifyEmailNotification(UserRegistrationData.IsRegistrationRequest);

                        if (this.IsValid)
                        {
                            //scope.Complete();
                            IsSuccess = true;
                        }
                    }
                //}
            }
            else
                this.ErrorMessage = "Attempt to save an user profile that was not validated.";

            return IsSuccess;
        }


        protected abstract void GrantAccessToRegion();

        protected abstract void DoPostProfileCreationTasks();

        //protected virtual void SaveDescriptors()
        //{
        //    foreach (UserDescriptor descr in this.UserRegistrationData.UserRegionalProfile.DescriptorIDList)
        //    {
        //        //TODO: Can be replaced by LINQ in 1 line.
        //        descr.UserId = this.UserId.Value;
        //        if (!SaveDescriptor(descr))
        //        {
        //            this.ErrorMessage = "Error occured while saving " + descr.DescriptorId.ToEnumObject<Descriptor>().Description() + " descriptor information.";
        //            break;
        //        }
        //    }

        //}

        public void ValidateData()
        {
            this.IsDirtyData = false;

            //RegistrationRequests Vs AddUser request.
            //Add User must have a valid RegisteredByUserId value; Reg requests won't have it anyway.
            if (!this.UserRegistrationData.IsRegistrationRequest)
            {
                if (!this.UserRegistrationData.RegisteredByUserId.HasValue)
                    throw new ShiptalkException("For Registration Requests, the RegisteredByUserID must be provided.", false, new ArgumentNullException("UserRegistrationRequestData.RegisteredByUserId"));
            }

            if (this.UserRegistrationData.RoleRequested.scope.IsEqual(Scope.Agency) || this.UserRegistrationData.RoleRequested.scope.IsEqual(Scope.SubStateRegion))
            {
                if (this.UserRegistrationData.IsApproverDesignate)
                {
                    throw new ShiptalkException("For Agency/SubState Users, approver designates must be set for the appropriate regional profile.", false, "An error occured during data validation. Please contact support for assistance.");
                }
            }

            //Ensure that the Approver Designates are Admins
            switch(this.UserRegistrationData.RoleRequested.scope)
            {
                case Scope.CMS:
                case Scope.State:
                    if(!this.UserRegistrationData.RoleRequested.IsAdmin && this.UserRegistrationData.IsApproverDesignate)
                        throw new ShiptalkException("Approver Designates must be Admins", false, "An error occured during data validation. Please contact support for assistance.");
                    break;
                case Scope.Agency:
                case Scope.SubStateRegion:
                    if (!this.UserRegistrationData.RoleRequested.IsAdmin && this.UserRegistrationData.UserRegionalAccessProfile.IsApproverDesignate)
                        throw new ShiptalkException("Approver Designates must be Admins", false, "An error occured during data validation. Please contact support for assistance.");
                    break;
                case Scope.CMSRegional:
                    if(this.UserRegistrationData.UserRegionalAccessProfile.IsAdmin)
                        throw new ShiptalkException("CMS Regional Users cannot be Admins", false, "An error occured during data validation. Please contact support for assistance.");
                    break;
                default:
                    break;
            }

            this.Validate();
        }

        public abstract void Validate();

        //protected virtual bool SaveDescriptor(UserDescriptor descriptorObj)
        //{
        //    int NewUserDescriptorId;
        //    int DescriptorAddedBy = this.UserId.Value;

        //    return UserAgencyDAL.AddDescriptorForUser(descriptorObj, DescriptorAddedBy, out NewUserDescriptorId);
        //}

        protected virtual bool SaveDescriptor(int DescriptorId, int RegionId, int AddedBy)
        {
            int NewUserDescriptorId;

            return UserDAL.AddDescriptorForUser(this.UserId.Value,
                        DescriptorId,
                        RegionId,
                        AddedBy, out NewUserDescriptorId);
        }

        protected virtual void SaveDescriptors(int AddedBy)
        {
            //SubState and Agency Users use the Actual SubState/AgencyID; others use the constant
            int RegionId = (this.UserRegistrationData.RoleRequested.scope.IsLowerOrEqualTo(Scope.SubStateRegion)) ? this.UserRegistrationData.UserRegionalAccessProfile.RegionId : Constants.Defaults.DefaultValues.AgencyIdForNonAgencyUsers;
            foreach (int DescriptorId in this.UserRegistrationData.UserRegionalAccessProfile.DescriptorIDList)
            {
                //TODO: Can be replaced by LINQ in 1 line.

                if (!SaveDescriptor(DescriptorId, RegionId, AddedBy))
                {
                    this.ErrorMessage = "Error occured while saving " + DescriptorId.ToEnumObject<Descriptor>().Description() + " descriptor information.";
                    break;
                }
            }
        }



    }
}
