using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web;

namespace ShiptalkLogic.BusinessObjects
{
    //class PAMBO
    //{
        //  Public & Media Activity Form tables begin here

        public enum PAMAudiance
        {
            [Description("Medicare Pre-Enrollees - Age 45-64")]
            MedicarePreEnrolleesAge45to64 = 1,
            [Description("Medicare Beneficiaries")]
            MedicareBeneficiaries = 2,
            [Description("Family Members - Caregivers of Medicare Beneficiaries")]
            FamilyMembersCaregiversOfMedicareBeneficiaries = 3,
            [Description("Low-Income")]
            LowIncome = 4,
            [Description("Hispanic, Latino, or Spanish Origin")]
            HispanicLatinoOrSpanishOrigin = 5,
            [Description("White, Non-Hispanic")]
            White_NonHispanic = 6,
            [Description("Black or African-American")]
            BlackOrAfricanAmerican = 7,
            [Description("American Indian or Alaska Native")]
            AmericanIndianOrAlaskaNative = 8,
            [Description("Asian Indian")]
            AsianIndian = 9,
            [Description("Chinese")]
            Chinese = 10,
            [Description("Filipino")]
            Filipino = 11,
            [Description("Japanese")]
            Japanese = 12,
            [Description("Korean")]
            Korean = 13,
            [Description("Vietnamese")]
            Vietnamese = 14,
            [Description("Native Hawaiian")]
            NativeHawaiian = 15,
            [Description("Guamanian or Chamorro")]
            GuamanianOrChamorro = 16,
            [Description("Samoan")]
            Samoan = 17,
            [Description("Other Asian")]
            OtherAsian = 18,
            [Description("Other Pacific Islander")]
            OtherPacificIslander = 19,
            [Description("Some Other Race-Ethnicity")]
            SomeOtherRaceEthnicity = 20,
            [Description("Disabled")]
            Disabled = 21,
            [Description("Rural")]
            Rural = 22,
            [Description("Employer-Related Groups")]
            EmployerRelatedGroups = 23,
            [Description("Mental Health Professionals")]
            MentalHealthProfessionals = 24,
            [Description("Social Work Professionals")]
            SocialWorkProfessionals = 25,
            [Description("Dual-Eligible Groups")]
            DualEligibleGroups = 26,
            [Description("Partnership Outreach")]
            PartnershipOutreach = 27,
            [Description("Presentations to Groups in Languages Other Than English")]
            PresentationsToGroupsInLanguagesOtherThanEnglish = 28,
            [Description("Other Audiences")]
            OtherAudiences = 29

        }

        public enum PAMTopic
        {
            [Description("Medicare Parts A and B")]
            MedicarePartsAandB = 1,
            [Description("Plan Issues - Non-Renewal, Termination, Employer-COBRA")]
            PlanIssues_NonRenewal_Termination_EmployerCOBRA = 2,
            [Description("Long-Term Care")]
            LongTermCare = 3,
            [Description("Medigap - Medicare Supplements")]
            MedigapMedicareSupplements = 4,
            [Description("Medicare Fraud and Abuse")]
            MedicareFraudAndAbuse = 5,
            [Description("Medicare Prescription Drug Coverage - PDP / MA-PD")]
            MedicarePrescriptionDrugCoverage_PDP_MAPD = 6,
            [Description("Other Prescription Drug Coverage - Assistance")]
            OtherPrescriptionDrugCoverageAssistance = 7,
            [Description("Medicare Advantage")]
            MedicareAdvantage = 8,
            [Description("QMB - SLMB - QI")]
            QMB_SLMB_QI = 9,
            [Description("Other Medicaid")]
            OtherMedicaid = 10,
            [Description("General SHIP Program Information")]
            GeneralSHIPProgramInformation = 11,
            [Description("Medicare Preventive Services")]
            MedicarePreventiveServices = 12,
            [Description("Low-Income Assistance")]
            LowIncomeAssistance = 13,
            [Description("Dual Eligible with Mental Illness Mental Disability")]
            DualEligiblewithMentalIllnessMentalDisability = 14,
            [Description("Volunteer Recruitment")]
            VolunteerRecruitment = 15,
            [Description("Partnership Recruitment")]
            PartnershipRecruitment = 16,
            [Description("Other Topics")]
            OtherTopics = 17
        }

        //  Public & Media Activity Form tables end here
    //}


}
