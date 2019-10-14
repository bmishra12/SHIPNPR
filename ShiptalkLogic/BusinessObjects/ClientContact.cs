using System;
using System.Collections.Generic;

namespace ShiptalkLogic.BusinessObjects
{
    public sealed class ClientContact : IModified
    {
        #region Properties

        #region Batch Upload Members.

        public string Action { get; set; } //TODO: Consider using an enum to constrain the types of action.
        public string RecordId { get; set; }

        #endregion

        public int? Id { get; set; }
        public int UserId { get; set; }
        public State AgencyState { get; set; }
        public string AgencyCode { get; set; }
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string ClientId { get; set; }
        public ClientAgeGroup ClientAgeGroup { get; set; }
        public ClientAssets ClientAssets { get; set; }
        public string ClientCountyCode { get; set; }
        public string ClientCountyName { get; set; }
        public ClientDualEligble ClientDualEligble { get; set; }
        public string ClientFirstName { get; set; }
        public ClientFirstVsContinuingContact ClientFirstVsContinuingContact { get; set; }
        public ClientGender ClientGender { get; set; }
        public string ClientLastName { get; set; }
        public ClientLearnedAboutSHIP ClientLearnedAboutSHIP { get; set; }
        public ClientMethodOfContact ClientMethodOfContact { get; set; }
        public ClientMonthlyIncome ClientMonthlyIncome { get; set; }
        public string ClientPhoneNumber { get; set; }
        public ClientPrimaryLanguageOtherThanEnglish ClientPrimaryLanguageOtherThanEnglish { get; set; }
        public IList<ClientRaceDescription> ClientRaceDescriptions { get; set; }
        public ClientReceivingSSOrMedicareDisability ClientReceivingSSOrMedicareDisability { get; set; }
        public ClientStatus ClientStatus { get; set; }
        public string ClientZIPCode { get; set; }
        public IList<SpecialFieldValue> CMSSpecialUseFields { get; set; }
        public string Comments { get; set; }
        public string CounselorCountyCode { get; set; }
        public string CounselorCountyName { get; set; }
        public int CounselorUserId { get; set; }
        public UserProfile Counselor { get; set; }
        public string CounselorZIPCode { get; set; }
        public string BatchStateUniqueID { get; set; }
        public DateTime DateOfContact { get; set; }
        public int HoursSpent { get; set; }
        public bool IsBatchUploadData { get; set; }
        public IList<Topic_MEDICAID> MedicaidTopics { get; set; }
        public IList<Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost> MedicareAdvantageTopics { get; set; }
        public IList<Topic_MEDICARE_PartsAandB> MedicarePartsAandBTopics { get; set; }
        public IList<Topic_MedicarePrescriptionDrugCoverage_PartD> MedicarePrescriptionDrugCoverageTopics { get; set; }
        public IList<Topic_MedicareSupplementOrSelect> MedicareSupplementTopics { get; set; }
        public int MinutesSpent { get; set; }
        public IList<Topic_OTHER> OtherDrugTopics { get; set; }
        public string OtherDrugTopicsSpecified { get; set; }
        public IList<Topic_OtherPrescriptionAssistance> OtherPrescriptionAssistanceTopics { get; set; }
        public string OtherPrescriptionAssitanceSpecified { get; set; }
        public IList<Topic_PartDLowIncomeSubsidy_LISOrExtraHelp> PartDLowIncomeSubsidyTopics { get; set; }
        public UserProfile Reviewer { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string RepresentativeFirstName { get; set; }
        public string RepresentativeLastName { get; set; }
        public UserProfile Submitter { get; set; }
        public IList<SpecialFieldValue> StateSpecialUseFields { get; set; }
        public string StateSpecificClientId { get; set; }

        #region Implementation of IModified

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        #endregion

        #endregion

    #region Methods

        internal static Type GetTopicType(int groupId)
        {
            switch (groupId)
            {
                case 1:
                    return typeof (Topic_MedicarePrescriptionDrugCoverage_PartD);
                case 2:
                    return typeof(Topic_PartDLowIncomeSubsidy_LISOrExtraHelp);
                case 3:
                    return typeof(Topic_OtherPrescriptionAssistance);
                case 4:
                    return typeof(Topic_MEDICARE_PartsAandB);
                case 5:
                    return typeof(Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost);
                case 6:
                    return typeof(Topic_MedicareSupplementOrSelect);
                case 7:
                    return typeof(Topic_MEDICAID);
                case 8:
                    return typeof(Topic_OTHER);
                default:
                    return null;
            }

        }

    #endregion
    }
}
