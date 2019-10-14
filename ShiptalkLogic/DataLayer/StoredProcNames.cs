using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;



namespace ShiptalkLogic.DataLayer
{

    internal static class StoredProcNames
    {

        public enum User
        {
            /*UserDAL*/
            [Description("CreateUser")]
            CreateUser,

            [Description("AuthenticateUser")]
            AuthenticateUser,

            [Description("UpdateUserSessionToken")]
            UpdateUserSessionToken,

            [Description("GetUserProfile")]
            GetUserProfile,

            [Description("GetUserAccountInfo")]
            GetUserAccountInfo,

            [Description("ChangePassword")]
            ChangePassword,

            [Description("ChangeEmail")]
            ChangeEmail,

            [Description("ActivateDeActivateUser")]
            ActivateDeActivateUser,

            [Description("GetDescriptorsForUser")]
            GetDescriptorsForUser,

            [Description("GetReviewersForUser")]
            GetReviewersForUser,

            [Description("GetApproversForUser")]
            GetApproversForUser,

            [Description("ApproveUser")]
            ApproveUser,

            [Description("ActivateUserWith180DaysInacitvity")]
            ActivateUserWith180DaysInacitvity,

            [Description("GetEmailVerificationTokenForUser")]
            GetEmailVerificationTokenForUser,

            [Description("VerifyEmailToken")]
            VerifyEmailToken,

            [Description("GetPasswordResetTokenForUser")]
            GetPasswordResetVerificationToken,

            [Description("IsPasswordResetTokenValid")]
            IsPasswordResetTokenValid,

            [Description("IsEmailVerificationTokenValid")]
            IsEmailVerificationTokenValid,

            [Description("ResetEmailChangeRequestDate")]
            ResetEmailChangeRequestDate,

            [Description("GetUserChangeEmailRequestDetails")]
            GetUserChangeEmailRequestDetails,

            [Description("AcceptOrRejectEmailChange")]
            AcceptOrRejectEmailChange,

            [Description("DoesUserNameExist")]
            DoesUserNameExist,

            [Description("GetUserIdForUserName")]
            GetUserIdForUserName,

            [Description("SearchUsers")]
            SearchUsers,

            [Description("SearchUsersFor180dInactivity")]
            SearchUsersFor180dInactivity,

            [Description("GetUsersFor180dInactivity")]
            GetUsersFor180dInactivity,

            [Description("GetUsersFor180dInActiveUserList")]
            GetUsersFor180dInActiveUserList,
            

            [Description("GetPendingRegistrationRequestsByStateFIPS")]
            GetPendingUsersByState,

            [Description("GetPendingUserRegistrationsForSubStateRegion")]
            GetPendingUsersBySubStateRegion,

            [Description("GetPendingUserRegistrationsForAgency")]
            GetPendingUsersForAgency,

            [Description("AddDescriptorForUser")]
            AddDescriptorForUser,

            [Description("DeleteDescriptorForUser")]
            DeleteDescriptorForUser,

            [Description("DeleteAllDescriptorsForUser")]
            DeleteAllDescriptorsForUser,

            [Description("DeleteUserRegistration")]
            DeleteUserRegistration,
            
            [Description("UnlockUserRegistration")]
            UnlockUserRegistration,

            [Description("UpdateUserProfile")]
            UpdateUserProfile,

            [Description("UpdateUserAccount")]
            UpdateUserAccount,

            [Description("DeleteAllReviewersForUser")]
            DeleteAllReviewersForUser,

            [Description("AddReviewerForUser")]
            AddReviewerForUser,

            [Description("GetAllUsers")]
            GetAllUsers,

            [Description("UpdateApproverDesignate")]
            UpdateApproverDesignate,

            [Description("AddUniqueIdRequest")]
            AddUniqueIdRequest,

            [Description("GetUserUniqueId")]
            GetUserUniqueId,

            [Description("SaveUniqueIdForUser")]
            SaveUniqueIdForUser,

            [Description("IsAccountCreatedViaUserRegistration")]
            IsAccountCreatedViaUserRegistration,

            [Description("GetLastLoginDetailsForUser")]
            GetLastLoginDetailsForUser,

            [Description("UpdateUserIsAdmin")]
            UpdateUserIsAdmin,

             [Description("GetClientContactCounselorForState")]
            GetClientContactCounselorForState,

             [Description("GetClientContactSubmitterForState")]
            GetClientContactSubmitterForState,

             [Description("GetPresenterForState")]
             GetPresenterForState,

             [Description("UpdateStateSuperDataEditor")]
             UpdateStateSuperDataEditor,

             [Description("GetAllAgenciesForUser")]
             GetAllAgenciesForUser,

             [Description("GetPendingEmailChangeVerifications")]
             GetPendingEmailChangeVerifications,
        }


        public enum UserAgency
        {
            /*UserAgencyDAL*/
            //[Description("AddReviewerForUser")]
            //AddReviewerForUser,
            //[Description("DeleteReviewerForUser")]
            //DeleteReviewerForUser,
            [Description("GetUserAgencyProfiles")]
            GetUserAgencyProfiles,
            [Description("UpdateUserAgency")]
            UpdateUserAgency
        }

        public enum UserSubStateRegion
        {
            /*UserSubStateRegionDAL*/
            [Description("GetUserSubStateRegionProfiles")]
            GetUserSubStateRegionProfiles,

            [Description("UpdateUserSubStateRegion")]
            UpdateUserSubStateRegion,
        }

        public enum UserCMSRegion
        {
            /*UserCMSRegionDAL*/
            [Description("GetUserCMSRegionalProfiles")]
            GetUserCMSRegionalProfiles
        }

        public enum UserAccess
        {
            [Description("GrantCMSRegionAccessToUser")]
            GrantCMSRegionAccessToUser = 1,
            [Description("GrantSubStateRegionAccessToUser")]
            GrantSubStateRegionAccessToUser = 2,
            [Description("GrantAgencyAccessToUser")]
            GrantAgencyAccessToUser = 3
        }

        public enum Lookup
        {
            /*LookupDAL*/
            [Description("GetSubStateRegionsForState")]
            GetSubStateRegionsForState = 1,
            [Description("GetCMSRegions")]
            GetCMSRegions = 2,
            [Description("GetAgenciesForState_Lookup")]
            GetAgenciesForStateLookup = 3,
			[Description("GetCountiesByStateFIPS")]
            GetCountiesByStateFIPS = 4,
            [Description("GetShipDirector")]
            GetShipDirector,
            [Description("GetStatesForCMSRegion")]
            GetStatesForCMSRegion,
            [Description("GetAgenciesForSubStateRegion")]
            GetAgenciesForSubStateRegion,
            [Description("GetAgenciesForState")]
            GetAgenciesForState,
            [Description("GetZipCodeForStateFips")]
            GetZipCodeForStateFips,
            [Description("GetCountiesByZip")]
            GetCountiesByZip,
            [Description("GetCountyByZipCode")]
            GetCountyByZipCode,
            [Description("GetReviewersForStateScope")]
            GetReviewersForStateScope,
            [Description("GetReviewersByUserRegion")]
            GetReviewersByUserRegion,
            [Description("GetStateFipCodeByShortName")]
            GetStateFipCodeByShortName,
            [Description("GetZipCodeForCountyFips")]
            GetZipCodeForCountyFips,
            [Description("GetPAMSubmittersForScope")]
            GetPAMSubmittersForScope,
            [Description("GetSubmittersForState")]
            GetSubmittersForState,
            [Description("GetPresentorsForState")]
            GetPresentorsForState,
            [Description("GetPAMPresentersForScope")]
            GetPAMPresentersForScope,
            [Description("IsZipCodeValidForState")]
            GetPAMPresentorsForState,
            [Description("IsZipCodeValidForState")]
            IsZipCodeValidForState,
            [Description("IsZipCodeValidForCounty")]
            IsZipCodeValidForCounty,
            [Description("IsAgencyValidForState")]
            IsAgencyValidForState,
            [Description("IsStateFipCodeValid")]
            IsStateFipCodeValid,
            [Description("IsClientContactRecordUploaded")]
            IsClientContactRecordUploaded,
            [Description("GetAgencyByCodeState")]
            GetAgencyByCodeState,
            [Description("GetCountyOfCounselorLocationByAgencyId")]
            GetCountyOfCounselorLocationByAgencyId,
             [Description("GetCountyOfCounselorLocationByState")]
            GetCountyOfCounselorLocationByState,
             [Description("GetZipCodeOfCounselorLocationByState")]
             GetZipCodeOfCounselorLocationByState,
             [Description("GetCountyOfClientResidenceByState")]
             GetCountyOfClientResidenceByState,
             [Description("GetZipCodeOfClientResidenceByState")]
             GetZipCodeOfClientResidenceByState
        }

        public enum BatchUpload
        {
            /*Batch File Upload*/
            [Description("AddBatchUploadStatus")]
            AddBatchUploadStatus,
            [Description("AddBatchUploadFile")]
            AddBatchUploadFile,
            [Description("UpdateFileUploadsRecordsProcessed")]
            UpdateFileUploadsRecordsProcessed,
            [Description("GetFileUploadStatusByUser")]
            GetFileUploadStatusByUser,
            [Description("DeleteUploadedPamRecord")]
            DeleteUploadedPamRecord,
            [Description("UpdateBatchUploadState")]
            UpdateBatchUploadState,
            [Description("GetExistingAutoAssignedClientID")]
            GetExistingAutoAssignedClientID


        }

        public enum SpecialFields
        {
            [Description("AddSpecialFields")]
            AddSpecialFields,
            [Description("UpdateSpecialField")]
            UpdateSpecialField,
            [Description("DeleteSpecialField")]
            DeleteSpecialField,
            [Description("GetSpecialFieldsById")]
            GetSpecialFieldsById,
            [Description("GetSpecialFieldsByDate")]
            GetSpecialFieldsByDate,
            [Description("GetSpecialFieldsValidStartEndDate")]
            GetSpecialFieldsValidStartEndDate,
            [Description("GetSpecialFieldName")]
            GetSpecialFieldName
            
        }

        public enum Reports
        {
            [Description("SaveResourceReport")]
            SaveResourceReport,
            [Description("GetResourceReportByStateFipCode")]
            GetResourceReportByStateFipCode,
            [Description("GetResourceReportByReportId")]
            GetResourceReportByReportId,
            [Description("SetActivateResourceReport")]
            SetActivateResourceReport

        }

        public enum InfoLib
        {
            [Description("AddInfoLibItem")]
            AddInfoLibItem,
            [Description("UpdateInfoLibItem")]
            UpdateInfoLibItem,
            [Description("DeleteInfoLibItem")]
            DeleteInfoLibItem,
            [Description("GetInfoLibItems")]
            GetInfoLibItems,
            [Description("GetInfoLibItemFile")]
            GetInfoLibItemFile,
            [Description("GetInfoLibItem")]
            GetInfoLibItem,
            [Description("GetInfoLibItemsTopLevel")]
            GetTopLevelInfoLibItems,
            [Description("GetInfoLibTopLevelParent")]
            GetInfoLibTopLevelParent,
            [Description("SearchInfoLibItems")]
            SearchInfoLibItems,
            [Description("GetInfoLibSpecialIdentifiers")]
            GetInfoLibSpecialIdentifiers,
            [Description("GetInfoLibForumCall")]
            GetInfoLibForumCall
        }

    }


}
