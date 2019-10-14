using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShiptalkLogic.BusinessObjects
{
    public sealed class PAMSummaryReport : IModified
    {
        public string StateName { get; set; }
        public int? TotalEventsAndActivities { get; set; }

        public int? InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents { get; set; }
        public int? InteractivePresentationstoPublicEstimatedNumberofAttendees { get; set; }
        public int? InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance { get; set; }

        public int? BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents { get; set; }
        public int? BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees { get; set; }
        public int? BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance { get; set; }

        public int? DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents{ get; set; }
        public int? DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance { get; set; }
        public int? DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance { get; set; }
        public int? DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD { get; set; }
        public int? DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS { get; set; }
        public int? DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP { get; set; }
        public int? DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram { get; set; }

        public int? RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents { get; set; }
        public int? RadioShowLiveorTapedEstimatedNumberofListenersReached { get; set; }

        public int? TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents { get; set; }
        public int? TVorCableShowLiveorTapedEstimatedNumberofViewersReached { get; set; }

        public int? ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents { get; set; }
        public int? ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign { get; set; }

        public int? PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents { get; set; }
        public int? PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign { get; set; }

        public int? NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations { get; set; }
        public string TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic { get; set; }

        public int? NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits { get; set; }
        public string TotalPersonHoursofEffortSpentonBoothsandExhibits { get; set; }

        public int? NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents { get; set; }
        public string TotalPersonHoursofEffortSpentonEnrollmentEvents { get; set; }

        public int? NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents { get; set; }
        public string TotalPersonHoursofEffortSpentonRadioEvents { get; set; }

        public int? NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents { get; set; }
        public string TotalPersonHoursofEffortSpentonTelevisionEvents { get; set; }

        public int? NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities { get; set; }
        public string TotalPersonHoursofEffortSpentonElectronicOtherActivities { get; set; }

        public int? NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities { get; set; }
        public string TotalPersonHoursofEffortSpentonPrintOtherActivities { get; set; }

        public int? NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities { get; set; }
        public string TotalPersonHoursofEffortSpentonAllEventsActivities { get; set; }

        public int? MedicarePartsAandBTopicFocus { get; set; }
        public int? PlanIssuesNonRenewalTerminationEmployerCOBRA { get; set; }
        public int? LongTermCare { get; set; }
        public int? MedigapMedicareSupplements { get; set; }
        public int? MedicareFraudandAbuse { get; set; }
        public int? MedicarePrescriptionDrugCoveragePDPMAPD { get; set; }
        public int? OtherPrescriptionDrugCoverageAssistance { get; set; }
        public int? MedicareAdvantageHealthPlans { get; set; }
        public int? QMBSLMBQI { get; set; }
        public int? OtherMedicaid { get; set; }
        public int? GeneralSHIPProgramInformation { get; set; }
        public int? MedicarePreventiveServices { get; set; }
        public int? LowIncomeAssistance { get; set; }
        public int? DualEligiblewithMentalIllnessMentalDisability { get; set; }
        public int? VolunteerRecruitment { get; set; }
        public int? PartnershipRecruitment { get; set; }
        public int? OtherTopics { get; set; }

        public int? MedicarePreEnrolleesAge4564TargetAudience { get; set; }
        public int? MedicareBeneficiaries { get; set; }
        public int? FamilyMembersCaregiversofMedicareBeneficiaries { get; set; }
        public int? LowIncome { get; set; }
        public int? HispanicLatinoorSpanishOrigin { get; set; }
        public int? WhiteNonHispanic { get; set; }
        public int? BlackorAfricanAmerican { get; set; }
        public int? AmericanIndianorAlaskaNative { get; set; }
        public int? AsianIndian { get; set; }
        public int? Chinese { get; set; }
        public int? Filipino { get; set; }
        public int? Japanese { get; set; }
        public int? Korean { get; set; }
        public int? Vietnamese { get; set; }
        public int? NativeHawaiian { get; set; }
        public int? GuamanianorChamorro { get; set; }
        public int? Samoan { get; set; }
        public int? OtherAsian { get; set; }
        public int? OtherPacificIslander { get; set; }
        public int? SomeOtherRaceEthnicity { get; set; }
        public int? Disabled { get; set; }
        public int? Rural { get; set; }
        public int? EmployerRelatedGroups { get; set; }
        public int? MentalHealthProfessionals { get; set; }
        public int? SocialWorkProfessionals { get; set; }
        public int? DualEligibleGroups { get; set; }
        public int? PartnershipOutreach { get; set; }
        public int? PresentationstoGroupsinLanguagesOtherThanEnglish { get; set; }
        public int? OtherAudiences { get; set; }
        public int? OldMedicareBeneficiariesandorPreEnrollees { get; set; }
        public int? OldAsian { get; set; }
        public int? OldNativeHawaiianorOtherPacificIslander { get; set; }
        
        #region Implementation of IModified

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        #endregion
    }
}
