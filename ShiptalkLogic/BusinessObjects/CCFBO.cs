using System.ComponentModel;

namespace ShiptalkLogic.BusinessObjects
{
    // CCF Enumerations
    public enum ClientAgeGroup
    {
        [Description("64 or Younger")]
        Age64OrYounger = 1,
        [Description("65-74")]
        Age65To74 = 2,
        [Description("75-84")]
        Age75To84 = 3,
        [Description("85 or Older")]
        Age85OrOlder = 4,
        [Description("Not Collected")]
        NotCollected = 9
    }

    public enum ClientAssets
    {
        [Description("Below LIS Asset Limits")]
        BelowLISAssetLimits = 1,
        [Description("Above LIS Asset Limits")]
        AboveLISAssetLimits = 2,
        [Description("Not Collected")]
        NotCollected = 9
    }

    public enum ClientDualEligble
    {
        [Description("Yes")]
        Yes = 1,
        [Description("No")]
        No = 2,
        [Description("Not Collected")]
        NotCollected = 9
    }

    public enum ClientFirstVsContinuingContact
    {
        [Description("First Contact for Issue")]
        FirstContactForIssue = 1,
        [Description("Continuing Contacts for Issue")]
        ContinuingContactsForIssue = 2
    }

    public enum ClientGender
    {
        [Description("Female")]
        Female = 1,
        [Description("Male")]
        Male = 2,
        [Description("Not Collected")]
        NotCollected = 9
    }

    public enum ClientLearnedAboutSHIP
    {
        [Description("Previous Contact")]
        PreviousContact = 1,
        [Description("CMS / Medicare")]
        CMSorMedicare = 2,
        [Description("Presentations")]
        Presentations = 3,
        [Description("Mailings")]
        Mailings = 4,
        [Description("Another Agency")]
        Agency = 5,
        [Description("Friend or Relative")]
        FriendOrRelative = 6,
        [Description("Media")]
        Media = 7,
        [Description("State Website")]
        StateWebsite = 8,
        [Description("Other")]
        Other = 9,
        [Description("Not Collected")]
        NotCollected = 99
    }

    public enum ClientMethodOfContact
    {
        [Description("Phone Call")]
        PhoneCall = 1,
        [Description("Face to Face at Counseling Location or Event Site")]
        FaceToFaceAtCounselingLocationOrEventSite = 2,
        [Description("Face to Face at Client's Home or Facility")]
        FaceToFaceAtClientHomeOrFacility = 3,
        [Description("EMail")]
        Email = 4,
        [Description("Postal Mail or Fax")]
        PostalMailOrFax = 5
    }

    public enum ClientMonthlyIncome
    {
        [Description("Below 150% FPL")]
        Below150PercentFPL = 1,
        [Description("At or Above 150% FPL")]
        AtOrAbove150PercentFPL = 2,
        [Description("Not Collected")]
        NotCollected = 9
    }

    public enum ClientPrimaryLanguageOtherThanEnglish
    {
        [Description("Primary Language Other Than English")]
        PrimaryLanguageOtherThanEnglish = 1,
        [Description("English is Client's Primary Language")]
        EnglishIsClientPrimaryLanguage = 2,
        [Description("Not Collected")]
        NotCollected = 9
    }

    public enum ClientRaceDescription
    {
        [Description("Hispanic, Latino, or Spanish Origin")]
        Hispanic_Latino_SpanishOrigin = 1,
        [Description("White, Non-Hispanic")]
        White_NonHispanic = 2,
        [Description("Black, African American")]
        Black_AfricanAmerican = 3,
        [Description("American Indian or Alaska Native")]
        AmericanIndianOrAlaskaNative = 4,
        [Description("Asian Indian")]
        AsianIndian = 5,
        [Description("Chinese")]
        Chinese = 6,
        [Description("Filipino")]
        Filipino = 7,
        [Description("Japanese")]
        Japanese = 8,
        [Description("Korean")]
        Korean = 9,
        [Description("Vietnamese")]
        Vietnamese = 10,
        [Description("Native Hawaiian")]
        NativeHawaiian = 11,
        [Description("Guamanian or Chamorro")]
        GuamanianOrChamorro = 12,
        [Description("Samoan")]
        Samoan = 13,
        [Description("Other Asian")]
        OtherAsian = 14,
        [Description("Other Pacific Islander")]
        OtherPacificIslander = 15,
        [Description("Some Other Race-Ethnicity")]
        SomeOtherRace = 16,
        [Description("Not Collected")]
        NotCollected = 99
    }

    public enum ClientReceivingSSOrMedicareDisability
    {
        [Description("Yes")]
        Yes = 1,
        [Description("No")]
        No = 2,
        [Description("Not Collected")]
        NotCollected = 9
    }

    public enum ClientStatus
    {
        [Description("General Information and Referral")]
        GeneralInformationAndReferral = 1,
        [Description("Detailed Assistance - In Progress")]
        DetailedAssistanceInProgress = 2,
        [Description("Detailed Assistance - Fully Completed")]
        DetailedAssistanceFullyCompleted = 3,
        [Description("Problem Solving / Problem Resolution - In Progress")]
        ProblemSolvingOrProblemResolutionInProgress = 4,
        [Description("Problem Solving / Problem Resolution - Fully Completed")]
        ProblemSolvingOrProblemResolutionFullyCompleted = 5
    }

    public enum ClientTopicGroups
    {
        [Description("Medicare Prescription Drug Coverage (Part D)")]
        Topic_MedicarePrescriptionDrugCoverage_PartD = 1,
        [Description("Part D Low Income Subsidy (LIS/Extra Help)")]
        Topic_PartDLowIncomeSubsidy_LISOrExtraHelp = 2,
        [Description("Other Prescription Assistance")]
        Topic_OtherPrescriptionAssistance = 3,
        [Description("MEDICARE (Parts A & B)")]
        Topic_MEDICARE_PartsAandB = 4,
        [Description("MEDICARE ADVANTAGE (HMO, POS, PPO, PFFS, SNP,MSA, Cost)")]
        Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost = 5, 
        [Description("MEDICARE SUPPLEMENT/SELECT")]
        Topic_MedicareSupplementOrSelect = 6,
        [Description("MEDICAID")]
        Topic_Medicaid = 7, 
        [Description("OTHER")]
        Topic_Other = 8 
    }

    public enum Topic_MedicarePrescriptionDrugCoverage_PartD
    {
        [Description("Eligibility/Screening")]
        EligibilityOrScreening = 1,
        [Description("Benefit Explanation")]
        BenefitExplanation = 2,
        [Description("Plans Comparison")]
        PlansComparison = 3,
        [Description("Plan Enrollment/Disenrollment")]
        PlanEnrollmentOrDisenrollment = 4,
        [Description("Claims/Billing")]
        ClaimsOrBilling = 5,
        [Description("Appeals/Grievances")]
        AppealsOrGrievances = 6,
        [Description("Fraud and Abuse")]
        FraudAndAbuse = 7,
        [Description("Marketing/Sales Complaints or Issues")]
        MarketingOrSalesComplaintsOrIssues = 8,
        [Description("Quality of Care")]
        QualityOfCare = 9,
        [Description("Plan Non-Renewal")]
        PlanNonRenewal = 10
    }

    public enum Topic_PartDLowIncomeSubsidy_LISOrExtraHelp
    {
        [Description("Eligibility/Screening")]
        EligibilityOrScreening = 11,
        [Description("Benefit Explanation")]
        BenefitExplanation = 12,
        [Description("Application Assistance")]
        ApplicationAssistance = 13,
        [Description("Claims/Billing")]
        ClaimsOrBilling = 14,
        [Description("Appeals/Grievances")]
        AppealsOrGrievances = 15
    }

    public enum Topic_OtherPrescriptionAssistance
    {
        [Description("Union/Employer Plan")]
        UnionOrEmployerPlan = 16,
        [Description("Military Drug Benefits")]
        MilitaryDrugBenefits = 17,
        [Description("Manufacturer Programs")]
        ManufacturerPrograms = 18,
        [Description("State Pharmaceutical Assistance Programs")]
        StatePharmaceuticalAssistancePrograms = 19,
        [Description("Other")]
        Other = 20
    }

    public enum Topic_MEDICARE_PartsAandB
    {
        [Description("Eligibility")]
        Eligibility = 21,
        [Description("Benefit Explanation")]
        BenefitExplanation = 22,
        [Description("Claims/Billing")]
        ClaimsOrBilling = 23,
        [Description("Appeals/Grievances")]
        AppealsOrGrievances = 24,
        [Description("Fraud and Abuse")]
        FraudAndAbuse = 25,
        [Description("Quality of Care")]
        QualityOfCare = 26
    }

    public enum Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost
    {
        [Description("Eligibility/Screening")]
        EligibilityOrScreening = 27,
        [Description("Benefit Explanation")]
        BenefitExplanation = 28,
        [Description("Plans Comparison")]
        PlansComparison = 29,
        [Description("Plan Enrollment/Disenrollment")]
        PlanEnrollmentOrDisenrollment = 30,
        [Description("Claims/Billing")]
        ClaimsOrBilling = 31,
        [Description("Appeals/Grievances")]
        AppealsOrGrievances = 32,
        [Description("Fraud and Abuse")]
        FraudAndAbuse = 33,
        [Description("Marketing/Sales Complaints or Issues")]
        MarketingOrSalesComplaintsOrIssues = 34,
        [Description("Quality of Care")]
        QualityOfCare = 35,
        [Description("Plan Non-Renewal")]
        PlanNonRenewal = 36
    }

    public enum Topic_MedicareSupplementOrSelect
    {
        [Description("Eligibility/Screening")]
        EligibilityOrScreening = 37,
        [Description("Benefit Explanation")]
        BenefitExplanation = 38,
        [Description("Plans Comparison")]
        PlansComparison = 39,
        [Description("Claims/Billing")]
        ClaimsOrBilling = 40,
        [Description("Appeals/Grievances")]
        AppealsOrGrievances = 41,
        [Description("Fraud and Abuse")]
        FraudAndAbuse = 42,
        [Description("Marketing/Sales Complaints or Issues")]
        MarketingOrSalesComplaintsOrIssues = 43,
        [Description("Quality of Care")]
        QualityOfCare = 44,
        [Description("Plan Non-Renewal")]
        PlanNonRenewal = 45
    }

    public enum Topic_MEDICAID
    {
        [Description("Medicare Savings Programs (MSP) Screening (QMB, SLMB, QI)")]
        MedicareSavingsProgramsScreening_QMB_SLMB_QI = 46,
        [Description("MSP Application Assistance")]
        MSPApplicationAssistance = 47,
        [Description("Medicaid (SSI, Nursing Home, MEPD, Elderly Waiver) Screening")]
        MedicaidScreening_SSI_NursingHome_MEPD_ElderlyWaiver = 48,
        [Description("Medicaid Application Assistance")]
        MedicaidApplicationAssistance = 49,
        [Description("Medicaid/QMB Claims")]
        MedicaidOrQMBClaims = 50,
        [Description("Fraud and Abuse")]
        FraudAndAbuse = 51
    }

    public enum Topic_OTHER
    {
        [Description("Long Term Care (LTC) Insurance")]
        LongTermCareInsurance = 52,
        [Description("LTC Partnership")]
        LTCPartnership = 53,
        [Description("LTC Other")]
        LTCOther = 54,
        [Description("Military Health Benefits")]
        MilitaryHealthBenefits = 55,
        [Description("Employer/Federal Employee Health Benefits (FEHB)")]
        EmployerOrFederalEmployeeHealthBenefits = 56,
        [Description("COBRA")]
        COBRA = 57,
        [Description("Other Health Insurance")]
        OtherHealthInsurance = 58,
        [Description("Other")]
        Other = 59
    }
}
