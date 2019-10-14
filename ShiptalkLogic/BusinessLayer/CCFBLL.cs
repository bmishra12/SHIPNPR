using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using AutoMapper;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkLogic.DataLayer;

namespace ShiptalkLogic.BusinessLayer
{
    public class CCFBLL
    {

        public struct CCCounselorsFilterCriteria
        {
            public int UserId { get; set; }
            public Scope scope { get; set; }
            public string StateFIPS { get; set; }
            public int? AgencyId { get; set; }
        }
        public struct CCSubmittersFilterCriteria
        {
            public int UserId { get; set; }
            public Scope scope { get; set; }
            public string StateFIPS { get; set; }
            public int? AgencyId { get; set; }
        }
        public struct CCSearchCriteria
        {
            public int UserId { get; set; }
            public Scope scope { get; set; }
            public string Keyword { get; set; }
            public int? AgencyId { get; set; }
            public string StateFIPS { get; set; }
            public int? CounselorId { get; set; }
            public int? SubmitterId { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }

        }

        public CCFBLL()
        {
            CreateMaps();
        }

        private CCFDAL _data;

        #region Properties

        private CCFDAL Data
        {
            get
            {
                if (_data == null) _data = new CCFDAL();

                return _data;
            }
        }

        #endregion

        #region Methods

        //Returns "First vs Continuing Contact" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetContactType()
        {
            return typeof(ClientFirstVsContinuingContact).Descriptions();
        }

        //Returns "Client Age Group" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetClientAgeGroup()
        {
            return typeof(ClientAgeGroup).Descriptions();
        }

        //Returns "Client Gender" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetClientGender()
        {
            return typeof(ClientGender).Descriptions();
        }

        //Returns "Client Language" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetClientLanguage()
        {
            return typeof(ClientPrimaryLanguageOtherThanEnglish).Descriptions();
        }

        //Returns "How Did Client Learn About SHIP" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetClientLearnedAboutSHIP()
        {
            return typeof(ClientLearnedAboutSHIP).Descriptions();
        }

        //Returns "Client Monthly Income" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetClientMonthlyIncome()
        {
            return typeof(ClientMonthlyIncome).Descriptions();
        }

        //Returns "Client Assets" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetClientAssets()
        {
            return typeof(ClientAssets).Descriptions();
        }

        //Returns "Method Of Contact" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetClientMethodOfContact()
        {
            return typeof(ClientMethodOfContact).Descriptions();
        }

        //Returns "Diability Factors" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetClientReceivingSSOrMedicareDisability()
        {
            return typeof(ClientReceivingSSOrMedicareDisability).Descriptions();
        }

        //Returns "Client Race" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetClientRace()
        {
            return typeof(ClientRaceDescription).Descriptions();
        }

        //Returns "Client Dual Eligble" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetClientDualEligble()
        {
            return typeof(ClientDualEligble).Descriptions();
        }

        //Returns "Client Status" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetClientStatus()
        {
            return typeof(ClientStatus).Descriptions();
        }

        //Returns "Topics: Medicare Prescription Drug Coverage (Part D)" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetMedicarePrescriptionDrugCoverage()
        {
            return typeof(Topic_MedicarePrescriptionDrugCoverage_PartD).Descriptions();
        }

        //Returns "Topics: Part D Low Income Subsidy (LIS/Extra Help)" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetPartDLowIncomeSubsidy()
        {
            return typeof(Topic_PartDLowIncomeSubsidy_LISOrExtraHelp).Descriptions();
        }

        //Returns "Topics: Other Prescription Assistance" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetOtherPrescriptionAssistance()
        {
            return typeof(Topic_OtherPrescriptionAssistance).Descriptions();
        }

        //Returns "Topics: MEDICARE (Parts A & B)" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetMedicarePartsAandB()
        {
            return typeof(Topic_MEDICARE_PartsAandB).Descriptions();
        }

        //Returns "Topics: Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost)" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetMedicareAdvantage()
        {
            return typeof(Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost).Descriptions();
        }

        //Returns "Topics: Medicare Supplement" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetMedicareSupplement()
        {
            return typeof(Topic_MedicareSupplementOrSelect).Descriptions();
        }

        //Returns "Topics: MEDICAID" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetMedicaid()
        {
            return typeof(Topic_MEDICAID).Descriptions();
        }

        //Returns "Topics: Other" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetOtherDrug()
        {
            return typeof(Topic_OTHER).Descriptions();
        }

        public string GetNextAutoAssignedClientID(string agencyCode)
        {
            return (!string.IsNullOrEmpty(agencyCode)) ? Data.GetNextAutoAssignedClientID(agencyCode) : string.Empty;
        }

        public IEnumerable<SpecialField> GetStateSpecialFields(State state)
        {
            return Data.GetSpecialFields(FormType.ClientContact, state, true);
        }

        public IEnumerable<SpecialField> GetStateSpecialFields(State state, bool restrictDate)
        {
            return Data.GetSpecialFields(FormType.ClientContact, state, restrictDate);
        }

        public IEnumerable<SpecialField> GetCMSSpecialFields()
        {
            return GetCMSSpecialFields(true);
        }

        public IEnumerable<SpecialField> GetCMSSpecialFields(bool restrictDate)
        {
         return Data.GetSpecialFields(FormType.ClientContact, new State("99"), restrictDate);
        }

        public int AddClientContact(AddClientContactViewData viewData)
        {
            return Data.CreateClientContact(Mapper.Map<AddClientContactViewData, ClientContact>(viewData));
        }

        public ViewClientContactViewData GetViewClientContact(int id)
        {
            return Mapper.Map<ClientContact, ViewClientContactViewData>(Data.GetClientContact(id));
        }

        public EditClientContactViewData GetEditClientContact(int id)
        {
            return  Mapper.Map<ClientContact, EditClientContactViewData>(Data.GetClientContact(id));
        }

        public void UpdateClientContact(EditClientContactViewData viewData)
        {
            Data.UpdateClientContact(Mapper.Map<EditClientContactViewData, ClientContact>(viewData));
        }

        public IEnumerable<SearchClientContactsViewData> GetRecentClientContacts(int userId)
        {
            //return Mapper.Map <IEnumerable<ClientContact>, IEnumerable<SearchClientContactsViewData>>(Data.GetRecentClientContacts(userId));

            return Data.GetRecentClientContacts(userId);
        }

        //public IEnumerable<SearchClientContactsViewData> SearchClientContacts(string keywords, DateTime? fromContactDate, DateTime? toContactDate, State state, int? counselorId, int? submitterId, int scopeId, int userID )
        //{
        //    return Mapper.Map<IEnumerable<ClientContact>, IEnumerable<SearchClientContactsViewData>>(Data.SearchClientContacts(keywords, fromContactDate, toContactDate, state, counselorId, submitterId, scopeId, userID));
        //}

        public IEnumerable<SearchClientContactsViewData> SearchClientContacts(CCSearchCriteria criteria)
        {
            //return Mapper.Map<IEnumerable<ClientContact>, IEnumerable<SearchClientContactsViewData>>(
            //    Data.SearchClientContacts(
            //                                criteria.UserId, 
            //                                criteria.scope.EnumValue<int>(), 
            //                                criteria.StateFIPS, 
            //                                criteria.Keyword,
            //                                criteria.FromDate, 
            //                                criteria.ToDate, 
            //                                criteria.CounselorId, 
            //                                criteria.SubmitterId, 
            //                                criteria.AgencyId
            //                            )
            //    );


            return Data.SearchClientContacts(
                                           criteria.UserId,
                                           criteria.scope.EnumValue<int>(),
                                           criteria.StateFIPS,
                                           criteria.Keyword,
                                           criteria.FromDate,
                                           criteria.ToDate,
                                           criteria.CounselorId,
                                           criteria.SubmitterId,
                                           criteria.AgencyId
                                       );

        }

        private void CreateMaps()
        {
            Mapper.CreateMap<AddClientContactViewData, ClientContact>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.AutoAssignedClientId))
                .ForMember(dest => dest.StateSpecialUseFields,
                           opt => opt.MapFrom(src => src.StateSpecialUseFields.ToSpecialFieldValueList()))
                .ForMember(dest => dest.CMSSpecialUseFields,
                           opt => opt.MapFrom(src => src.CMSSpecialUseFields.ToSpecialFieldValueList()))
                .ForMember(dest => dest.ClientRaceDescriptions,
                           opt => opt.MapFrom(src => src.ClientRaceDescriptions.ToIdList<ClientRaceDescription>()))
                .ForMember(dest => dest.MedicaidTopics,
                           opt => opt.MapFrom(src => src.MedicaidTopics.ToIdList<Topic_MEDICAID>()))
                .ForMember(dest => dest.MedicareAdvantageTopics,
                           opt => opt.MapFrom(src => src.MedicareAdvantageTopics.ToIdList<Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost>()))
                .ForMember(dest => dest.MedicarePartsAandBTopics,
                           opt => opt.MapFrom(src => src.MedicarePartsAandBTopics.ToIdList<Topic_MEDICARE_PartsAandB>()))
                .ForMember(dest => dest.MedicarePrescriptionDrugCoverageTopics,
                           opt => opt.MapFrom(src => src.MedicarePrescriptionDrugCoverageTopics.ToIdList<Topic_MedicarePrescriptionDrugCoverage_PartD>()))
                .ForMember(dest => dest.MedicareSupplementTopics,
                           opt => opt.MapFrom(src => src.MedicareSupplementTopics.ToIdList<Topic_MedicareSupplementOrSelect>()))
                .ForMember(dest => dest.OtherDrugTopics,
                           opt => opt.MapFrom(src => src.OtherDrugTopics.ToIdList<Topic_OTHER>()))
                .ForMember(dest => dest.OtherPrescriptionAssistanceTopics,
                           opt => opt.MapFrom(src => src.OtherPrescriptionAssistanceTopics.ToIdList<Topic_OtherPrescriptionAssistance>()))
                .ForMember(dest => dest.PartDLowIncomeSubsidyTopics,
                           opt => opt.MapFrom(src => src.PartDLowIncomeSubsidyTopics.ToIdList<Topic_PartDLowIncomeSubsidy_LISOrExtraHelp>()));

            Mapper.CreateMap<ClientContact, ViewClientContactViewData>()
                .ForMember(dest => dest.AutoAssignedClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.ClientRaceDescriptions,
                           opt => opt.MapFrom(src => src.ClientRaceDescriptions.ToKeyValuePairList()))
                .ForMember(dest => dest.MedicaidTopics,
                           opt => opt.MapFrom(src => src.MedicaidTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.MedicareAdvantageTopics,
                           opt => opt.MapFrom(src => src.MedicareAdvantageTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.MedicarePartsAandBTopics,
                           opt => opt.MapFrom(src => src.MedicarePartsAandBTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.MedicarePrescriptionDrugCoverageTopics,
                           opt => opt.MapFrom(src => src.MedicarePrescriptionDrugCoverageTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.MedicareSupplementTopics,
                           opt => opt.MapFrom(src => src.MedicareSupplementTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.OtherDrugTopics,
                           opt => opt.MapFrom(src => src.OtherDrugTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.OtherPrescriptionAssistanceTopics,
                           opt => opt.MapFrom(src => src.OtherPrescriptionAssistanceTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.PartDLowIncomeSubsidyTopics,
                           opt => opt.MapFrom(src => src.PartDLowIncomeSubsidyTopics.ToKeyValuePairList()));

            Mapper.CreateMap<ClientContact, EditClientContactViewData>()
                .ForMember(dest => dest.AutoAssignedClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.ClientRaceDescriptions,
                           opt => opt.MapFrom(src => src.ClientRaceDescriptions.ToKeyValuePairList()))
                .ForMember(dest => dest.MedicaidTopics,
                           opt => opt.MapFrom(src => src.MedicaidTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.MedicareAdvantageTopics,
                           opt => opt.MapFrom(src => src.MedicareAdvantageTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.MedicarePartsAandBTopics,
                           opt => opt.MapFrom(src => src.MedicarePartsAandBTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.MedicarePrescriptionDrugCoverageTopics,
                           opt => opt.MapFrom(src => src.MedicarePrescriptionDrugCoverageTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.MedicareSupplementTopics,
                           opt => opt.MapFrom(src => src.MedicareSupplementTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.OtherDrugTopics,
                           opt => opt.MapFrom(src => src.OtherDrugTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.OtherPrescriptionAssistanceTopics,
                           opt => opt.MapFrom(src => src.OtherPrescriptionAssistanceTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.PartDLowIncomeSubsidyTopics,
                           opt => opt.MapFrom(src => src.PartDLowIncomeSubsidyTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.StateSpecialUseFields, 
                           opt => opt.MapFrom(src => src.StateSpecialUseFields.ToDictionary()))
                .ForMember(dest => dest.CMSSpecialUseFields, 
                           opt => opt.MapFrom(src => src.CMSSpecialUseFields.ToDictionary()));

            Mapper.CreateMap<EditClientContactViewData, ClientContact>()
               .ForMember(dest => dest.Counselor, opt => opt.MapFrom(src => new UserProfile { UserId = src.CounselorUserId }))
               .ForMember(dest => dest.Reviewer, opt => opt.MapFrom(src => new UserProfile { UserId = src.ReviewerUserId.GetValueOrDefault(0) }))
               .ForMember(dest => dest.Submitter, opt => opt.MapFrom(src => new UserProfile { UserId = src.SubmitterUserId }))
               .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.AutoAssignedClientId))
               .ForMember(dest => dest.StateSpecialUseFields,
                          opt => opt.MapFrom(src => src.StateSpecialUseFields.ToSpecialFieldValueList()))
               .ForMember(dest => dest.CMSSpecialUseFields,
                          opt => opt.MapFrom(src => src.CMSSpecialUseFields.ToSpecialFieldValueList()))
               .ForMember(dest => dest.ClientRaceDescriptions,
                          opt => opt.MapFrom(src => src.ClientRaceDescriptions.ToIdList<ClientRaceDescription>()))
               .ForMember(dest => dest.MedicaidTopics,
                          opt => opt.MapFrom(src => src.MedicaidTopics.ToIdList<Topic_MEDICAID>()))
               .ForMember(dest => dest.MedicareAdvantageTopics,
                          opt => opt.MapFrom(src => src.MedicareAdvantageTopics.ToIdList<Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost>()))
               .ForMember(dest => dest.MedicarePartsAandBTopics,
                          opt => opt.MapFrom(src => src.MedicarePartsAandBTopics.ToIdList<Topic_MEDICARE_PartsAandB>()))
               .ForMember(dest => dest.MedicarePrescriptionDrugCoverageTopics,
                          opt => opt.MapFrom(src => src.MedicarePrescriptionDrugCoverageTopics.ToIdList<Topic_MedicarePrescriptionDrugCoverage_PartD>()))
               .ForMember(dest => dest.MedicareSupplementTopics,
                          opt => opt.MapFrom(src => src.MedicareSupplementTopics.ToIdList<Topic_MedicareSupplementOrSelect>()))
               .ForMember(dest => dest.OtherDrugTopics,
                          opt => opt.MapFrom(src => src.OtherDrugTopics.ToIdList<Topic_OTHER>()))
               .ForMember(dest => dest.OtherPrescriptionAssistanceTopics,
                          opt => opt.MapFrom(src => src.OtherPrescriptionAssistanceTopics.ToIdList<Topic_OtherPrescriptionAssistance>()))
               .ForMember(dest => dest.PartDLowIncomeSubsidyTopics,
                          opt => opt.MapFrom(src => src.PartDLowIncomeSubsidyTopics.ToIdList<Topic_PartDLowIncomeSubsidy_LISOrExtraHelp>()));

            Mapper.CreateMap<ClientContact, SearchClientContactsViewData>()
                .ForMember(dest => dest.AutoAssignedClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.ClientPhone, opt => opt.MapFrom(src => src.ClientPhoneNumber))
                .ForMember(dest => dest.CounselorUserID,
                           opt => opt.MapFrom(src => (src.Counselor != null) ? src.Counselor.UserId : 0))
                .ForMember(dest => dest.SubmitterUserID,
                           opt => opt.MapFrom(src => (src.Submitter != null) ? src.Submitter.UserId : 0))
                .ForMember(dest => dest.ReviewerUserID,
                           opt => opt.MapFrom(src => (src.Reviewer != null) ? (int?)src.Reviewer.UserId : null));
        }

        public IEnumerable<KeyValuePair<int, string>> GetClientContactCounselors(CCCounselorsFilterCriteria CounselorFilters)
        {
            if(CounselorFilters.AgencyId.HasValue)
                return Data.GetClientContactCounselorsByAgency(CounselorFilters.UserId, CounselorFilters.StateFIPS, CounselorFilters.scope.EnumValue<int>(), CounselorFilters.AgencyId.Value);
            else
                return Data.GetAllClientContactCounselors(CounselorFilters.UserId, CounselorFilters.StateFIPS, CounselorFilters.scope.EnumValue<int>());
        }

        public IEnumerable<KeyValuePair<int, string>> GetClientContactCounselorsForCCForm(CCCounselorsFilterCriteria CounselorFilters, bool isActive)
        {
            return Data.GetClientContactCounselorsByAgencyActiveInactive(CounselorFilters.UserId, CounselorFilters.StateFIPS, CounselorFilters.scope.EnumValue<int>(), CounselorFilters.AgencyId.Value, isActive);
        }

        public IEnumerable<KeyValuePair<int, string>> GetClientContactSubmitters(CCSubmittersFilterCriteria SubmitterFilters)
        {
            if (SubmitterFilters.AgencyId.HasValue)
                return Data.GetClientContactSubmittersByAgency(SubmitterFilters.UserId, SubmitterFilters.StateFIPS, SubmitterFilters.scope.EnumValue<int>(), SubmitterFilters.AgencyId.Value);
            else
                return Data.GetAllClientContactSubmitters(SubmitterFilters.UserId, SubmitterFilters.StateFIPS, SubmitterFilters.scope.EnumValue<int>());
            
        }

        //public IEnumerable<KeyValuePair<int, string>> GetClientContactCounselors(State state)
        //{
        //    return Data.GetClientContactCounselors(state);
        //}

        //public IEnumerable<KeyValuePair<int, string>> GetClientContactSubmitters(State state)
        //{
        //    return Data.GetClientContactSubmitters(state);
        //}

        //public IEnumerable<KeyValuePair<int, string>> GetCounselorsForSuperDataEditor(int userId)
        //{
        //    return Data.GetCounselorsForSuperDataEditor(userId);
        //}

        //public IEnumerable<KeyValuePair<int, string>> GetSubmittersForSuperDataEditor(int userId)
        //{
        //    return Data.GetSubmittersForSuperDataEditor(userId);
        //}

        //public IEnumerable<KeyValuePair<int, string>> GetCounselors(Scope scope, IList<int> agencies)
        //{
        //    return Data.GetCounselors(scope, agencies);
        //}

        //public IEnumerable<KeyValuePair<int, string>> GetSubmitters(Scope scope, List<int> agencies)
        //{
        //    return Data.GetSubmitters(scope, agencies);
        //}

        //public IEnumerable<KeyValuePair<int, string>> GetReviewerSubmitters(Scope scope, List<int> agencies, int userId)
        //{
        //    return Data.GetReviewerSubmitters(scope, agencies, userId);
        //}

        public bool IsZIPCodeValid(string zipCode)
        {
            return Data.IsZIPCodeValid(zipCode);
        }

        public bool IsCounselingCountyCodeValid(int userId, string countyCode)
        {
            return Data.IsCounselingCountyCodeValid(userId, countyCode);
        }

        public bool IsCounselingZIPCodeValid(int userId, string zipCode)
        {
            return Data.IsCounselingZIPCodeValid(userId, zipCode);
        }

        public bool IsUserClientContactReviewer(int clientContactId, int userId)
        {
            return Data.IsUserClientContactReviewer(clientContactId, userId);
        }

        public bool IsDuplicateClientContact(DuplicateCheckType checkType, int agencyId, string stateSpecifiedClientId, string autoAssignedClientId, string clientFirstName, string clientLastName, DateTime dateOfContact, int CounselorID)
        {
            return Data.IsDuplicateClientContact(checkType, agencyId, stateSpecifiedClientId, autoAssignedClientId, clientFirstName, clientLastName, dateOfContact, CounselorID);
        }



        public bool DeleteClientContact(int clientContactID, out string FailureReason)
        {
            return Data.DeleteClientContact(clientContactID, out FailureReason);
        }

        public enum DuplicateCheckType
        {
            AddNewClientContact = 0,
            NewClientContact = 1
        }

        #endregion

        
    }
}