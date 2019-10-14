using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ShiptalkLogic.BusinessObjects;
using T = ShiptalkLogic.Constants.Tables;
using SP = ShiptalkLogic.Constants.StoredProcs;

namespace ShiptalkLogic.DataLayer
{
    internal class PAMSummaryReportDAL : DALBase
    {
        #region GetPAMSummaryReportByState

        public PAMSummaryReport GetPAMSummaryReportByState(DateTime DOCStartDate, DateTime DOCEndDate, string StateFIPSCode)
        {
            PAMSummaryReport pamSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.GetPAMSummaryReportByState"))
            {
                database.AddInParameter(command, SP.GetPAMSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.EndDate, DbType.DateTime, DOCEndDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.StateFIPS, DbType.String, StateFIPSCode);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        pamSummaryReport = new PAMSummaryReport
                        {
                            TotalEventsAndActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalEventsAndActivities, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedNumberofAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedNumberofAttendees, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.RadioShowLiveorTapedEstimatedNumberofListenersReached = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedEstimatedNumberofListenersReached, GetNullableInt32, null);
                            pamSummaryReport.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.TVorCableShowLiveorTapedEstimatedNumberofViewersReached = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedEstimatedNumberofViewersReached, GetNullableInt32, null);
                            pamSummaryReport.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign, GetNullableInt32, null);
                            pamSummaryReport.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign, GetNullableInt32, null);
                            pamSummaryReport.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonBoothsandExhibits, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonEnrollmentEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonRadioEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonTelevisionEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonElectronicOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonPrintOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonAllEventsActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePartsAandBTopicFocus = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePartsAandBTopicFocus, GetNullableInt32, null);
                            pamSummaryReport.PlanIssuesNonRenewalTerminationEmployerCOBRA = reader.GetDefaultIfDBNull(T.PAMSummary.PlanIssuesNonRenewalTerminationEmployerCOBRA, GetNullableInt32, null);
                            pamSummaryReport.LongTermCare = reader.GetDefaultIfDBNull(T.PAMSummary.LongTermCare, GetNullableInt32, null);
                            pamSummaryReport.MedigapMedicareSupplements = reader.GetDefaultIfDBNull(T.PAMSummary.MedigapMedicareSupplements, GetNullableInt32, null);
                            pamSummaryReport.MedicareFraudandAbuse = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareFraudandAbuse, GetNullableInt32, null);
                            pamSummaryReport.MedicarePrescriptionDrugCoveragePDPMAPD = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePrescriptionDrugCoveragePDPMAPD, GetNullableInt32, null);
                            pamSummaryReport.OtherPrescriptionDrugCoverageAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPrescriptionDrugCoverageAssistance, GetNullableInt32, null);
                            pamSummaryReport.MedicareAdvantageHealthPlans = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareAdvantageHealthPlans, GetNullableInt32, null);
                            pamSummaryReport.QMBSLMBQI = reader.GetDefaultIfDBNull(T.PAMSummary.QMBSLMBQI, GetNullableInt32, null);
                            pamSummaryReport.OtherMedicaid = reader.GetDefaultIfDBNull(T.PAMSummary.OtherMedicaid, GetNullableInt32, null);
                            pamSummaryReport.GeneralSHIPProgramInformation = reader.GetDefaultIfDBNull(T.PAMSummary.GeneralSHIPProgramInformation, GetNullableInt32, null);
                            pamSummaryReport.MedicarePreventiveServices = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreventiveServices, GetNullableInt32, null);
                            pamSummaryReport.LowIncomeAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncomeAssistance, GetNullableInt32, null);
                            pamSummaryReport.DualEligiblewithMentalIllnessMentalDisability = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligiblewithMentalIllnessMentalDisability, GetNullableInt32, null);
                            pamSummaryReport.VolunteerRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.VolunteerRecruitment, GetNullableInt32, null);
                            pamSummaryReport.PartnershipRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipRecruitment, GetNullableInt32, null);
                            pamSummaryReport.OtherTopics = reader.GetDefaultIfDBNull(T.PAMSummary.OtherTopics, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePreEnrolleesAge4564TargetAudience = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreEnrolleesAge4564TargetAudience, GetNullableInt32, null);
                            pamSummaryReport.MedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.FamilyMembersCaregiversofMedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.FamilyMembersCaregiversofMedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.LowIncome = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncome, GetNullableInt32, null);
                            pamSummaryReport.HispanicLatinoorSpanishOrigin = reader.GetDefaultIfDBNull(T.PAMSummary.HispanicLatinoorSpanishOrigin, GetNullableInt32, null);
                            pamSummaryReport.WhiteNonHispanic = reader.GetDefaultIfDBNull(T.PAMSummary.WhiteNonHispanic, GetNullableInt32, null);
                            pamSummaryReport.BlackorAfricanAmerican = reader.GetDefaultIfDBNull(T.PAMSummary.BlackorAfricanAmerican, GetNullableInt32, null);
                            pamSummaryReport.AmericanIndianorAlaskaNative = reader.GetDefaultIfDBNull(T.PAMSummary.AmericanIndianorAlaskaNative, GetNullableInt32, null);
                            pamSummaryReport.AsianIndian = reader.GetDefaultIfDBNull(T.PAMSummary.AsianIndian, GetNullableInt32, null);
                            pamSummaryReport.Chinese = reader.GetDefaultIfDBNull(T.PAMSummary.Chinese, GetNullableInt32, null);
                            pamSummaryReport.Filipino = reader.GetDefaultIfDBNull(T.PAMSummary.Filipino, GetNullableInt32, null);
                            pamSummaryReport.Japanese = reader.GetDefaultIfDBNull(T.PAMSummary.Japanese, GetNullableInt32, null);
                            pamSummaryReport.Korean = reader.GetDefaultIfDBNull(T.PAMSummary.Korean, GetNullableInt32, null);
                            pamSummaryReport.Vietnamese = reader.GetDefaultIfDBNull(T.PAMSummary.Vietnamese, GetNullableInt32, null);
                            pamSummaryReport.NativeHawaiian = reader.GetDefaultIfDBNull(T.PAMSummary.NativeHawaiian, GetNullableInt32, null);
                            pamSummaryReport.GuamanianorChamorro = reader.GetDefaultIfDBNull(T.PAMSummary.GuamanianorChamorro, GetNullableInt32, null);
                            pamSummaryReport.Samoan = reader.GetDefaultIfDBNull(T.PAMSummary.Samoan, GetNullableInt32, null);
                            pamSummaryReport.OtherAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAsian, GetNullableInt32, null);
                            pamSummaryReport.OtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPacificIslander, GetNullableInt32, null);
                            pamSummaryReport.SomeOtherRaceEthnicity = reader.GetDefaultIfDBNull(T.PAMSummary.SomeOtherRaceEthnicity, GetNullableInt32, null);
                            pamSummaryReport.Disabled = reader.GetDefaultIfDBNull(T.PAMSummary.Disabled, GetNullableInt32, null);
                            pamSummaryReport.Rural = reader.GetDefaultIfDBNull(T.PAMSummary.Rural, GetNullableInt32, null);
                            pamSummaryReport.EmployerRelatedGroups = reader.GetDefaultIfDBNull(T.PAMSummary.EmployerRelatedGroups, GetNullableInt32, null);
                            pamSummaryReport.MentalHealthProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.MentalHealthProfessionals, GetNullableInt32, null);
                            pamSummaryReport.SocialWorkProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.SocialWorkProfessionals, GetNullableInt32, null);
                            pamSummaryReport.DualEligibleGroups = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligibleGroups, GetNullableInt32, null);
                            pamSummaryReport.PartnershipOutreach = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipOutreach, GetNullableInt32, null);
                            pamSummaryReport.PresentationstoGroupsinLanguagesOtherThanEnglish = reader.GetDefaultIfDBNull(T.PAMSummary.PresentationstoGroupsinLanguagesOtherThanEnglish, GetNullableInt32, null);
                            pamSummaryReport.OtherAudiences = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAudiences, GetNullableInt32, null);
                            pamSummaryReport.OldMedicareBeneficiariesandorPreEnrollees = reader.GetDefaultIfDBNull(T.PAMSummary.OldMedicareBeneficiariesandorPreEnrollees, GetNullableInt32, null);
                            pamSummaryReport.OldAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OldAsian, GetNullableInt32, null);
                            pamSummaryReport.OldNativeHawaiianorOtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OldNativeHawaiianorOtherPacificIslander, GetNullableInt32, null);
                        }
                    }
                }
            }
            return pamSummaryReport;
        }
        #endregion
        
        #region GetPAMSummaryReportByAgency

        public PAMSummaryReport GetPAMSummaryReportByAgency(DateTime DOCStartDate, DateTime DOCEndDate, string AgencyId)
        {
            PAMSummaryReport pamSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.GetPAMSummaryReportByAgency"))
            {
                database.AddInParameter(command, SP.GetPAMSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.EndDate, DbType.DateTime, DOCEndDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.AgencyId, DbType.String, AgencyId);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        pamSummaryReport = new PAMSummaryReport
                        {
                            TotalEventsAndActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalEventsAndActivities, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedNumberofAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedNumberofAttendees, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.RadioShowLiveorTapedEstimatedNumberofListenersReached = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedEstimatedNumberofListenersReached, GetNullableInt32, null);
                            pamSummaryReport.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.TVorCableShowLiveorTapedEstimatedNumberofViewersReached = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedEstimatedNumberofViewersReached, GetNullableInt32, null);
                            pamSummaryReport.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign, GetNullableInt32, null);
                            pamSummaryReport.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign, GetNullableInt32, null);
                            pamSummaryReport.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonBoothsandExhibits, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonEnrollmentEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonRadioEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonTelevisionEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonElectronicOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonPrintOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonAllEventsActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePartsAandBTopicFocus = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePartsAandBTopicFocus, GetNullableInt32, null);
                            pamSummaryReport.PlanIssuesNonRenewalTerminationEmployerCOBRA = reader.GetDefaultIfDBNull(T.PAMSummary.PlanIssuesNonRenewalTerminationEmployerCOBRA, GetNullableInt32, null);
                            pamSummaryReport.LongTermCare = reader.GetDefaultIfDBNull(T.PAMSummary.LongTermCare, GetNullableInt32, null);
                            pamSummaryReport.MedigapMedicareSupplements = reader.GetDefaultIfDBNull(T.PAMSummary.MedigapMedicareSupplements, GetNullableInt32, null);
                            pamSummaryReport.MedicareFraudandAbuse = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareFraudandAbuse, GetNullableInt32, null);
                            pamSummaryReport.MedicarePrescriptionDrugCoveragePDPMAPD = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePrescriptionDrugCoveragePDPMAPD, GetNullableInt32, null);
                            pamSummaryReport.OtherPrescriptionDrugCoverageAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPrescriptionDrugCoverageAssistance, GetNullableInt32, null);
                            pamSummaryReport.MedicareAdvantageHealthPlans = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareAdvantageHealthPlans, GetNullableInt32, null);
                            pamSummaryReport.QMBSLMBQI = reader.GetDefaultIfDBNull(T.PAMSummary.QMBSLMBQI, GetNullableInt32, null);
                            pamSummaryReport.OtherMedicaid = reader.GetDefaultIfDBNull(T.PAMSummary.OtherMedicaid, GetNullableInt32, null);
                            pamSummaryReport.GeneralSHIPProgramInformation = reader.GetDefaultIfDBNull(T.PAMSummary.GeneralSHIPProgramInformation, GetNullableInt32, null);
                            pamSummaryReport.MedicarePreventiveServices = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreventiveServices, GetNullableInt32, null);
                            pamSummaryReport.LowIncomeAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncomeAssistance, GetNullableInt32, null);
                            pamSummaryReport.DualEligiblewithMentalIllnessMentalDisability = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligiblewithMentalIllnessMentalDisability, GetNullableInt32, null);
                            pamSummaryReport.VolunteerRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.VolunteerRecruitment, GetNullableInt32, null);
                            pamSummaryReport.PartnershipRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipRecruitment, GetNullableInt32, null);
                            pamSummaryReport.OtherTopics = reader.GetDefaultIfDBNull(T.PAMSummary.OtherTopics, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePreEnrolleesAge4564TargetAudience = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreEnrolleesAge4564TargetAudience, GetNullableInt32, null);
                            pamSummaryReport.MedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.FamilyMembersCaregiversofMedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.FamilyMembersCaregiversofMedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.LowIncome = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncome, GetNullableInt32, null);
                            pamSummaryReport.HispanicLatinoorSpanishOrigin = reader.GetDefaultIfDBNull(T.PAMSummary.HispanicLatinoorSpanishOrigin, GetNullableInt32, null);
                            pamSummaryReport.WhiteNonHispanic = reader.GetDefaultIfDBNull(T.PAMSummary.WhiteNonHispanic, GetNullableInt32, null);
                            pamSummaryReport.BlackorAfricanAmerican = reader.GetDefaultIfDBNull(T.PAMSummary.BlackorAfricanAmerican, GetNullableInt32, null);
                            pamSummaryReport.AmericanIndianorAlaskaNative = reader.GetDefaultIfDBNull(T.PAMSummary.AmericanIndianorAlaskaNative, GetNullableInt32, null);
                            pamSummaryReport.AsianIndian = reader.GetDefaultIfDBNull(T.PAMSummary.AsianIndian, GetNullableInt32, null);
                            pamSummaryReport.Chinese = reader.GetDefaultIfDBNull(T.PAMSummary.Chinese, GetNullableInt32, null);
                            pamSummaryReport.Filipino = reader.GetDefaultIfDBNull(T.PAMSummary.Filipino, GetNullableInt32, null);
                            pamSummaryReport.Japanese = reader.GetDefaultIfDBNull(T.PAMSummary.Japanese, GetNullableInt32, null);
                            pamSummaryReport.Korean = reader.GetDefaultIfDBNull(T.PAMSummary.Korean, GetNullableInt32, null);
                            pamSummaryReport.Vietnamese = reader.GetDefaultIfDBNull(T.PAMSummary.Vietnamese, GetNullableInt32, null);
                            pamSummaryReport.NativeHawaiian = reader.GetDefaultIfDBNull(T.PAMSummary.NativeHawaiian, GetNullableInt32, null);
                            pamSummaryReport.GuamanianorChamorro = reader.GetDefaultIfDBNull(T.PAMSummary.GuamanianorChamorro, GetNullableInt32, null);
                            pamSummaryReport.Samoan = reader.GetDefaultIfDBNull(T.PAMSummary.Samoan, GetNullableInt32, null);
                            pamSummaryReport.OtherAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAsian, GetNullableInt32, null);
                            pamSummaryReport.OtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPacificIslander, GetNullableInt32, null);
                            pamSummaryReport.SomeOtherRaceEthnicity = reader.GetDefaultIfDBNull(T.PAMSummary.SomeOtherRaceEthnicity, GetNullableInt32, null);
                            pamSummaryReport.Disabled = reader.GetDefaultIfDBNull(T.PAMSummary.Disabled, GetNullableInt32, null);
                            pamSummaryReport.Rural = reader.GetDefaultIfDBNull(T.PAMSummary.Rural, GetNullableInt32, null);
                            pamSummaryReport.EmployerRelatedGroups = reader.GetDefaultIfDBNull(T.PAMSummary.EmployerRelatedGroups, GetNullableInt32, null);
                            pamSummaryReport.MentalHealthProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.MentalHealthProfessionals, GetNullableInt32, null);
                            pamSummaryReport.SocialWorkProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.SocialWorkProfessionals, GetNullableInt32, null);
                            pamSummaryReport.DualEligibleGroups = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligibleGroups, GetNullableInt32, null);
                            pamSummaryReport.PartnershipOutreach = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipOutreach, GetNullableInt32, null);
                            pamSummaryReport.PresentationstoGroupsinLanguagesOtherThanEnglish = reader.GetDefaultIfDBNull(T.PAMSummary.PresentationstoGroupsinLanguagesOtherThanEnglish, GetNullableInt32, null);
                            pamSummaryReport.OtherAudiences = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAudiences, GetNullableInt32, null);
                            pamSummaryReport.OldMedicareBeneficiariesandorPreEnrollees = reader.GetDefaultIfDBNull(T.PAMSummary.OldMedicareBeneficiariesandorPreEnrollees, GetNullableInt32, null);
                            pamSummaryReport.OldAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OldAsian, GetNullableInt32, null);
                            pamSummaryReport.OldNativeHawaiianorOtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OldNativeHawaiianorOtherPacificIslander, GetNullableInt32, null);
                        }
                    }
                }
            }
            return pamSummaryReport;
        }
        #endregion


        #region GetPAMSummaryReportByCountyOfActivityEvent

        public PAMSummaryReport GetPAMSummaryReportByCountyOfActivityEvent(DateTime DOCStartDate, DateTime DOCEndDate, string CountyOfActivityEvent, int ScopeId, int UserId, int AgencyId, string StateFIPSCode)
        {
            PAMSummaryReport pamSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.GetPAMSummaryReportByCounty"))
            {
                database.AddInParameter(command, SP.GetPAMSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.EndDate, DbType.DateTime, DOCEndDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.CountyOfActivityEvent, DbType.String, CountyOfActivityEvent);
                database.AddInParameter(command, SP.GetPAMSummaryReport.ScopeId, DbType.Int32, ScopeId);
                database.AddInParameter(command, SP.GetPAMSummaryReport.UserId, DbType.Int32, UserId);
                database.AddInParameter(command, SP.GetPAMSummaryReport.AgencyId, DbType.Int32, AgencyId);
                database.AddInParameter(command, SP.GetPAMSummaryReport.StateFIPSCode, DbType.String, StateFIPSCode); 

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        pamSummaryReport = new PAMSummaryReport
                        {
                            TotalEventsAndActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalEventsAndActivities, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedNumberofAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedNumberofAttendees, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.RadioShowLiveorTapedEstimatedNumberofListenersReached = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedEstimatedNumberofListenersReached, GetNullableInt32, null);
                            pamSummaryReport.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.TVorCableShowLiveorTapedEstimatedNumberofViewersReached = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedEstimatedNumberofViewersReached, GetNullableInt32, null);
                            pamSummaryReport.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign, GetNullableInt32, null);
                            pamSummaryReport.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign, GetNullableInt32, null);
                            pamSummaryReport.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonBoothsandExhibits, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonEnrollmentEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonRadioEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonTelevisionEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonElectronicOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonPrintOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonAllEventsActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePartsAandBTopicFocus = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePartsAandBTopicFocus, GetNullableInt32, null);
                            pamSummaryReport.PlanIssuesNonRenewalTerminationEmployerCOBRA = reader.GetDefaultIfDBNull(T.PAMSummary.PlanIssuesNonRenewalTerminationEmployerCOBRA, GetNullableInt32, null);
                            pamSummaryReport.LongTermCare = reader.GetDefaultIfDBNull(T.PAMSummary.LongTermCare, GetNullableInt32, null);
                            pamSummaryReport.MedigapMedicareSupplements = reader.GetDefaultIfDBNull(T.PAMSummary.MedigapMedicareSupplements, GetNullableInt32, null);
                            pamSummaryReport.MedicareFraudandAbuse = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareFraudandAbuse, GetNullableInt32, null);
                            pamSummaryReport.MedicarePrescriptionDrugCoveragePDPMAPD = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePrescriptionDrugCoveragePDPMAPD, GetNullableInt32, null);
                            pamSummaryReport.OtherPrescriptionDrugCoverageAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPrescriptionDrugCoverageAssistance, GetNullableInt32, null);
                            pamSummaryReport.MedicareAdvantageHealthPlans = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareAdvantageHealthPlans, GetNullableInt32, null);
                            pamSummaryReport.QMBSLMBQI = reader.GetDefaultIfDBNull(T.PAMSummary.QMBSLMBQI, GetNullableInt32, null);
                            pamSummaryReport.OtherMedicaid = reader.GetDefaultIfDBNull(T.PAMSummary.OtherMedicaid, GetNullableInt32, null);
                            pamSummaryReport.GeneralSHIPProgramInformation = reader.GetDefaultIfDBNull(T.PAMSummary.GeneralSHIPProgramInformation, GetNullableInt32, null);
                            pamSummaryReport.MedicarePreventiveServices = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreventiveServices, GetNullableInt32, null);
                            pamSummaryReport.LowIncomeAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncomeAssistance, GetNullableInt32, null);
                            pamSummaryReport.DualEligiblewithMentalIllnessMentalDisability = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligiblewithMentalIllnessMentalDisability, GetNullableInt32, null);
                            pamSummaryReport.VolunteerRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.VolunteerRecruitment, GetNullableInt32, null);
                            pamSummaryReport.PartnershipRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipRecruitment, GetNullableInt32, null);
                            pamSummaryReport.OtherTopics = reader.GetDefaultIfDBNull(T.PAMSummary.OtherTopics, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePreEnrolleesAge4564TargetAudience = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreEnrolleesAge4564TargetAudience, GetNullableInt32, null);
                            pamSummaryReport.MedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.FamilyMembersCaregiversofMedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.FamilyMembersCaregiversofMedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.LowIncome = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncome, GetNullableInt32, null);
                            pamSummaryReport.HispanicLatinoorSpanishOrigin = reader.GetDefaultIfDBNull(T.PAMSummary.HispanicLatinoorSpanishOrigin, GetNullableInt32, null);
                            pamSummaryReport.WhiteNonHispanic = reader.GetDefaultIfDBNull(T.PAMSummary.WhiteNonHispanic, GetNullableInt32, null);
                            pamSummaryReport.BlackorAfricanAmerican = reader.GetDefaultIfDBNull(T.PAMSummary.BlackorAfricanAmerican, GetNullableInt32, null);
                            pamSummaryReport.AmericanIndianorAlaskaNative = reader.GetDefaultIfDBNull(T.PAMSummary.AmericanIndianorAlaskaNative, GetNullableInt32, null);
                            pamSummaryReport.AsianIndian = reader.GetDefaultIfDBNull(T.PAMSummary.AsianIndian, GetNullableInt32, null);
                            pamSummaryReport.Chinese = reader.GetDefaultIfDBNull(T.PAMSummary.Chinese, GetNullableInt32, null);
                            pamSummaryReport.Filipino = reader.GetDefaultIfDBNull(T.PAMSummary.Filipino, GetNullableInt32, null);
                            pamSummaryReport.Japanese = reader.GetDefaultIfDBNull(T.PAMSummary.Japanese, GetNullableInt32, null);
                            pamSummaryReport.Korean = reader.GetDefaultIfDBNull(T.PAMSummary.Korean, GetNullableInt32, null);
                            pamSummaryReport.Vietnamese = reader.GetDefaultIfDBNull(T.PAMSummary.Vietnamese, GetNullableInt32, null);
                            pamSummaryReport.NativeHawaiian = reader.GetDefaultIfDBNull(T.PAMSummary.NativeHawaiian, GetNullableInt32, null);
                            pamSummaryReport.GuamanianorChamorro = reader.GetDefaultIfDBNull(T.PAMSummary.GuamanianorChamorro, GetNullableInt32, null);
                            pamSummaryReport.Samoan = reader.GetDefaultIfDBNull(T.PAMSummary.Samoan, GetNullableInt32, null);
                            pamSummaryReport.OtherAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAsian, GetNullableInt32, null);
                            pamSummaryReport.OtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPacificIslander, GetNullableInt32, null);
                            pamSummaryReport.SomeOtherRaceEthnicity = reader.GetDefaultIfDBNull(T.PAMSummary.SomeOtherRaceEthnicity, GetNullableInt32, null);
                            pamSummaryReport.Disabled = reader.GetDefaultIfDBNull(T.PAMSummary.Disabled, GetNullableInt32, null);
                            pamSummaryReport.Rural = reader.GetDefaultIfDBNull(T.PAMSummary.Rural, GetNullableInt32, null);
                            pamSummaryReport.EmployerRelatedGroups = reader.GetDefaultIfDBNull(T.PAMSummary.EmployerRelatedGroups, GetNullableInt32, null);
                            pamSummaryReport.MentalHealthProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.MentalHealthProfessionals, GetNullableInt32, null);
                            pamSummaryReport.SocialWorkProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.SocialWorkProfessionals, GetNullableInt32, null);
                            pamSummaryReport.DualEligibleGroups = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligibleGroups, GetNullableInt32, null);
                            pamSummaryReport.PartnershipOutreach = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipOutreach, GetNullableInt32, null);
                            pamSummaryReport.PresentationstoGroupsinLanguagesOtherThanEnglish = reader.GetDefaultIfDBNull(T.PAMSummary.PresentationstoGroupsinLanguagesOtherThanEnglish, GetNullableInt32, null);
                            pamSummaryReport.OtherAudiences = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAudiences, GetNullableInt32, null);
                            pamSummaryReport.OldMedicareBeneficiariesandorPreEnrollees = reader.GetDefaultIfDBNull(T.PAMSummary.OldMedicareBeneficiariesandorPreEnrollees, GetNullableInt32, null);
                            pamSummaryReport.OldAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OldAsian, GetNullableInt32, null);
                            pamSummaryReport.OldNativeHawaiianorOtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OldNativeHawaiianorOtherPacificIslander, GetNullableInt32, null);
                        }
                    }
                }
            }
            return pamSummaryReport;
        }
        #endregion


        #region GetPAMSummaryReportByZipCodeOfActivityEvent

        public PAMSummaryReport GetPAMSummaryReportByZipCodeOfActivityEvent(DateTime DOCStartDate, DateTime DOCEndDate, string ZipCodeOfActivityEvent, int ScopeId, int UserId, int AgencyId)
        {
            PAMSummaryReport pamSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.GetPAMSummaryReportByZipCode"))
            {
                database.AddInParameter(command, SP.GetPAMSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.EndDate, DbType.DateTime, DOCEndDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.ZipCodeOfActivityEvent, DbType.String, ZipCodeOfActivityEvent);
                database.AddInParameter(command, SP.GetPAMSummaryReport.ScopeId, DbType.Int32, ScopeId);
                database.AddInParameter(command, SP.GetPAMSummaryReport.UserId, DbType.Int32, UserId);
                database.AddInParameter(command, SP.GetPAMSummaryReport.AgencyId, DbType.Int32, AgencyId);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        pamSummaryReport = new PAMSummaryReport
                        {
                            TotalEventsAndActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalEventsAndActivities, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedNumberofAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedNumberofAttendees, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.RadioShowLiveorTapedEstimatedNumberofListenersReached = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedEstimatedNumberofListenersReached, GetNullableInt32, null);
                            pamSummaryReport.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.TVorCableShowLiveorTapedEstimatedNumberofViewersReached = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedEstimatedNumberofViewersReached, GetNullableInt32, null);
                            pamSummaryReport.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign, GetNullableInt32, null);
                            pamSummaryReport.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign, GetNullableInt32, null);
                            pamSummaryReport.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonBoothsandExhibits, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonEnrollmentEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonRadioEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonTelevisionEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonElectronicOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonPrintOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonAllEventsActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePartsAandBTopicFocus = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePartsAandBTopicFocus, GetNullableInt32, null);
                            pamSummaryReport.PlanIssuesNonRenewalTerminationEmployerCOBRA = reader.GetDefaultIfDBNull(T.PAMSummary.PlanIssuesNonRenewalTerminationEmployerCOBRA, GetNullableInt32, null);
                            pamSummaryReport.LongTermCare = reader.GetDefaultIfDBNull(T.PAMSummary.LongTermCare, GetNullableInt32, null);
                            pamSummaryReport.MedigapMedicareSupplements = reader.GetDefaultIfDBNull(T.PAMSummary.MedigapMedicareSupplements, GetNullableInt32, null);
                            pamSummaryReport.MedicareFraudandAbuse = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareFraudandAbuse, GetNullableInt32, null);
                            pamSummaryReport.MedicarePrescriptionDrugCoveragePDPMAPD = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePrescriptionDrugCoveragePDPMAPD, GetNullableInt32, null);
                            pamSummaryReport.OtherPrescriptionDrugCoverageAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPrescriptionDrugCoverageAssistance, GetNullableInt32, null);
                            pamSummaryReport.MedicareAdvantageHealthPlans = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareAdvantageHealthPlans, GetNullableInt32, null);
                            pamSummaryReport.QMBSLMBQI = reader.GetDefaultIfDBNull(T.PAMSummary.QMBSLMBQI, GetNullableInt32, null);
                            pamSummaryReport.OtherMedicaid = reader.GetDefaultIfDBNull(T.PAMSummary.OtherMedicaid, GetNullableInt32, null);
                            pamSummaryReport.GeneralSHIPProgramInformation = reader.GetDefaultIfDBNull(T.PAMSummary.GeneralSHIPProgramInformation, GetNullableInt32, null);
                            pamSummaryReport.MedicarePreventiveServices = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreventiveServices, GetNullableInt32, null);
                            pamSummaryReport.LowIncomeAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncomeAssistance, GetNullableInt32, null);
                            pamSummaryReport.DualEligiblewithMentalIllnessMentalDisability = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligiblewithMentalIllnessMentalDisability, GetNullableInt32, null);
                            pamSummaryReport.VolunteerRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.VolunteerRecruitment, GetNullableInt32, null);
                            pamSummaryReport.PartnershipRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipRecruitment, GetNullableInt32, null);
                            pamSummaryReport.OtherTopics = reader.GetDefaultIfDBNull(T.PAMSummary.OtherTopics, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePreEnrolleesAge4564TargetAudience = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreEnrolleesAge4564TargetAudience, GetNullableInt32, null);
                            pamSummaryReport.MedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.FamilyMembersCaregiversofMedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.FamilyMembersCaregiversofMedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.LowIncome = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncome, GetNullableInt32, null);
                            pamSummaryReport.HispanicLatinoorSpanishOrigin = reader.GetDefaultIfDBNull(T.PAMSummary.HispanicLatinoorSpanishOrigin, GetNullableInt32, null);
                            pamSummaryReport.WhiteNonHispanic = reader.GetDefaultIfDBNull(T.PAMSummary.WhiteNonHispanic, GetNullableInt32, null);
                            pamSummaryReport.BlackorAfricanAmerican = reader.GetDefaultIfDBNull(T.PAMSummary.BlackorAfricanAmerican, GetNullableInt32, null);
                            pamSummaryReport.AmericanIndianorAlaskaNative = reader.GetDefaultIfDBNull(T.PAMSummary.AmericanIndianorAlaskaNative, GetNullableInt32, null);
                            pamSummaryReport.AsianIndian = reader.GetDefaultIfDBNull(T.PAMSummary.AsianIndian, GetNullableInt32, null);
                            pamSummaryReport.Chinese = reader.GetDefaultIfDBNull(T.PAMSummary.Chinese, GetNullableInt32, null);
                            pamSummaryReport.Filipino = reader.GetDefaultIfDBNull(T.PAMSummary.Filipino, GetNullableInt32, null);
                            pamSummaryReport.Japanese = reader.GetDefaultIfDBNull(T.PAMSummary.Japanese, GetNullableInt32, null);
                            pamSummaryReport.Korean = reader.GetDefaultIfDBNull(T.PAMSummary.Korean, GetNullableInt32, null);
                            pamSummaryReport.Vietnamese = reader.GetDefaultIfDBNull(T.PAMSummary.Vietnamese, GetNullableInt32, null);
                            pamSummaryReport.NativeHawaiian = reader.GetDefaultIfDBNull(T.PAMSummary.NativeHawaiian, GetNullableInt32, null);
                            pamSummaryReport.GuamanianorChamorro = reader.GetDefaultIfDBNull(T.PAMSummary.GuamanianorChamorro, GetNullableInt32, null);
                            pamSummaryReport.Samoan = reader.GetDefaultIfDBNull(T.PAMSummary.Samoan, GetNullableInt32, null);
                            pamSummaryReport.OtherAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAsian, GetNullableInt32, null);
                            pamSummaryReport.OtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPacificIslander, GetNullableInt32, null);
                            pamSummaryReport.SomeOtherRaceEthnicity = reader.GetDefaultIfDBNull(T.PAMSummary.SomeOtherRaceEthnicity, GetNullableInt32, null);
                            pamSummaryReport.Disabled = reader.GetDefaultIfDBNull(T.PAMSummary.Disabled, GetNullableInt32, null);
                            pamSummaryReport.Rural = reader.GetDefaultIfDBNull(T.PAMSummary.Rural, GetNullableInt32, null);
                            pamSummaryReport.EmployerRelatedGroups = reader.GetDefaultIfDBNull(T.PAMSummary.EmployerRelatedGroups, GetNullableInt32, null);
                            pamSummaryReport.MentalHealthProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.MentalHealthProfessionals, GetNullableInt32, null);
                            pamSummaryReport.SocialWorkProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.SocialWorkProfessionals, GetNullableInt32, null);
                            pamSummaryReport.DualEligibleGroups = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligibleGroups, GetNullableInt32, null);
                            pamSummaryReport.PartnershipOutreach = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipOutreach, GetNullableInt32, null);
                            pamSummaryReport.PresentationstoGroupsinLanguagesOtherThanEnglish = reader.GetDefaultIfDBNull(T.PAMSummary.PresentationstoGroupsinLanguagesOtherThanEnglish, GetNullableInt32, null);
                            pamSummaryReport.OtherAudiences = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAudiences, GetNullableInt32, null);
                            pamSummaryReport.OldMedicareBeneficiariesandorPreEnrollees = reader.GetDefaultIfDBNull(T.PAMSummary.OldMedicareBeneficiariesandorPreEnrollees, GetNullableInt32, null);
                            pamSummaryReport.OldAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OldAsian, GetNullableInt32, null);
                            pamSummaryReport.OldNativeHawaiianorOtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OldNativeHawaiianorOtherPacificIslander, GetNullableInt32, null);
                        }
                    }
                }
            }
            return pamSummaryReport;
        }
        #endregion

        #region GetPAMSummaryReportByNational

        public PAMSummaryReport GetPAMSummaryReportByNational(DateTime DOCStartDate, DateTime DOCEndDate)
        {
            PAMSummaryReport pamSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.GetPAMSummaryReportByNational"))
            {
                database.AddInParameter(command, SP.GetPAMSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.EndDate, DbType.DateTime, DOCEndDate);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        pamSummaryReport = new PAMSummaryReport
                        {
                            TotalEventsAndActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalEventsAndActivities, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedNumberofAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedNumberofAttendees, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.RadioShowLiveorTapedEstimatedNumberofListenersReached = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedEstimatedNumberofListenersReached, GetNullableInt32, null);
                            pamSummaryReport.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.TVorCableShowLiveorTapedEstimatedNumberofViewersReached = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedEstimatedNumberofViewersReached, GetNullableInt32, null);
                            pamSummaryReport.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign, GetNullableInt32, null);
                            pamSummaryReport.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign, GetNullableInt32, null);
                            pamSummaryReport.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonBoothsandExhibits, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonEnrollmentEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonRadioEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonTelevisionEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonElectronicOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonPrintOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonAllEventsActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePartsAandBTopicFocus = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePartsAandBTopicFocus, GetNullableInt32, null);
                            pamSummaryReport.PlanIssuesNonRenewalTerminationEmployerCOBRA = reader.GetDefaultIfDBNull(T.PAMSummary.PlanIssuesNonRenewalTerminationEmployerCOBRA, GetNullableInt32, null);
                            pamSummaryReport.LongTermCare = reader.GetDefaultIfDBNull(T.PAMSummary.LongTermCare, GetNullableInt32, null);
                            pamSummaryReport.MedigapMedicareSupplements = reader.GetDefaultIfDBNull(T.PAMSummary.MedigapMedicareSupplements, GetNullableInt32, null);
                            pamSummaryReport.MedicareFraudandAbuse = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareFraudandAbuse, GetNullableInt32, null);
                            pamSummaryReport.MedicarePrescriptionDrugCoveragePDPMAPD = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePrescriptionDrugCoveragePDPMAPD, GetNullableInt32, null);
                            pamSummaryReport.OtherPrescriptionDrugCoverageAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPrescriptionDrugCoverageAssistance, GetNullableInt32, null);
                            pamSummaryReport.MedicareAdvantageHealthPlans = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareAdvantageHealthPlans, GetNullableInt32, null);
                            pamSummaryReport.QMBSLMBQI = reader.GetDefaultIfDBNull(T.PAMSummary.QMBSLMBQI, GetNullableInt32, null);
                            pamSummaryReport.OtherMedicaid = reader.GetDefaultIfDBNull(T.PAMSummary.OtherMedicaid, GetNullableInt32, null);
                            pamSummaryReport.GeneralSHIPProgramInformation = reader.GetDefaultIfDBNull(T.PAMSummary.GeneralSHIPProgramInformation, GetNullableInt32, null);
                            pamSummaryReport.MedicarePreventiveServices = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreventiveServices, GetNullableInt32, null);
                            pamSummaryReport.LowIncomeAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncomeAssistance, GetNullableInt32, null);
                            pamSummaryReport.DualEligiblewithMentalIllnessMentalDisability = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligiblewithMentalIllnessMentalDisability, GetNullableInt32, null);
                            pamSummaryReport.VolunteerRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.VolunteerRecruitment, GetNullableInt32, null);
                            pamSummaryReport.PartnershipRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipRecruitment, GetNullableInt32, null);
                            pamSummaryReport.OtherTopics = reader.GetDefaultIfDBNull(T.PAMSummary.OtherTopics, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePreEnrolleesAge4564TargetAudience = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreEnrolleesAge4564TargetAudience, GetNullableInt32, null);
                            pamSummaryReport.MedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.FamilyMembersCaregiversofMedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.FamilyMembersCaregiversofMedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.LowIncome = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncome, GetNullableInt32, null);
                            pamSummaryReport.HispanicLatinoorSpanishOrigin = reader.GetDefaultIfDBNull(T.PAMSummary.HispanicLatinoorSpanishOrigin, GetNullableInt32, null);
                            pamSummaryReport.WhiteNonHispanic = reader.GetDefaultIfDBNull(T.PAMSummary.WhiteNonHispanic, GetNullableInt32, null);
                            pamSummaryReport.BlackorAfricanAmerican = reader.GetDefaultIfDBNull(T.PAMSummary.BlackorAfricanAmerican, GetNullableInt32, null);
                            pamSummaryReport.AmericanIndianorAlaskaNative = reader.GetDefaultIfDBNull(T.PAMSummary.AmericanIndianorAlaskaNative, GetNullableInt32, null);
                            pamSummaryReport.AsianIndian = reader.GetDefaultIfDBNull(T.PAMSummary.AsianIndian, GetNullableInt32, null);
                            pamSummaryReport.Chinese = reader.GetDefaultIfDBNull(T.PAMSummary.Chinese, GetNullableInt32, null);
                            pamSummaryReport.Filipino = reader.GetDefaultIfDBNull(T.PAMSummary.Filipino, GetNullableInt32, null);
                            pamSummaryReport.Japanese = reader.GetDefaultIfDBNull(T.PAMSummary.Japanese, GetNullableInt32, null);
                            pamSummaryReport.Korean = reader.GetDefaultIfDBNull(T.PAMSummary.Korean, GetNullableInt32, null);
                            pamSummaryReport.Vietnamese = reader.GetDefaultIfDBNull(T.PAMSummary.Vietnamese, GetNullableInt32, null);
                            pamSummaryReport.NativeHawaiian = reader.GetDefaultIfDBNull(T.PAMSummary.NativeHawaiian, GetNullableInt32, null);
                            pamSummaryReport.GuamanianorChamorro = reader.GetDefaultIfDBNull(T.PAMSummary.GuamanianorChamorro, GetNullableInt32, null);
                            pamSummaryReport.Samoan = reader.GetDefaultIfDBNull(T.PAMSummary.Samoan, GetNullableInt32, null);
                            pamSummaryReport.OtherAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAsian, GetNullableInt32, null);
                            pamSummaryReport.OtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPacificIslander, GetNullableInt32, null);
                            pamSummaryReport.SomeOtherRaceEthnicity = reader.GetDefaultIfDBNull(T.PAMSummary.SomeOtherRaceEthnicity, GetNullableInt32, null);
                            pamSummaryReport.Disabled = reader.GetDefaultIfDBNull(T.PAMSummary.Disabled, GetNullableInt32, null);
                            pamSummaryReport.Rural = reader.GetDefaultIfDBNull(T.PAMSummary.Rural, GetNullableInt32, null);
                            pamSummaryReport.EmployerRelatedGroups = reader.GetDefaultIfDBNull(T.PAMSummary.EmployerRelatedGroups, GetNullableInt32, null);
                            pamSummaryReport.MentalHealthProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.MentalHealthProfessionals, GetNullableInt32, null);
                            pamSummaryReport.SocialWorkProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.SocialWorkProfessionals, GetNullableInt32, null);
                            pamSummaryReport.DualEligibleGroups = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligibleGroups, GetNullableInt32, null);
                            pamSummaryReport.PartnershipOutreach = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipOutreach, GetNullableInt32, null);
                            pamSummaryReport.PresentationstoGroupsinLanguagesOtherThanEnglish = reader.GetDefaultIfDBNull(T.PAMSummary.PresentationstoGroupsinLanguagesOtherThanEnglish, GetNullableInt32, null);
                            pamSummaryReport.OtherAudiences = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAudiences, GetNullableInt32, null);
                            pamSummaryReport.OldMedicareBeneficiariesandorPreEnrollees = reader.GetDefaultIfDBNull(T.PAMSummary.OldMedicareBeneficiariesandorPreEnrollees, GetNullableInt32, null);
                            pamSummaryReport.OldAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OldAsian, GetNullableInt32, null);
                            pamSummaryReport.OldNativeHawaiianorOtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OldNativeHawaiianorOtherPacificIslander, GetNullableInt32, null);
                        }
                    }
                }
            }
            return pamSummaryReport;
        }
        #endregion


        #region GetPAMSummaryReportByPresenterContributor

        public PAMSummaryReport GetPAMSummaryReportByPresenterContributor(DateTime DOCStartDate, DateTime DOCEndDate, int AgencyId, int PresenterContributorId)
        {
            PAMSummaryReport pamSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.GetPAMSummaryReportByPresenter"))
            {
                database.AddInParameter(command, SP.GetPAMSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.EndDate, DbType.DateTime, DOCEndDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.AgencyId, DbType.Int32, AgencyId);

                database.AddInParameter(command, SP.GetPAMSummaryReport.PresenterContributorId, DbType.Int32, PresenterContributorId);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        pamSummaryReport = new PAMSummaryReport
                        {
                            TotalEventsAndActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalEventsAndActivities, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedNumberofAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedNumberofAttendees, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.RadioShowLiveorTapedEstimatedNumberofListenersReached = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedEstimatedNumberofListenersReached, GetNullableInt32, null);
                            pamSummaryReport.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.TVorCableShowLiveorTapedEstimatedNumberofViewersReached = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedEstimatedNumberofViewersReached, GetNullableInt32, null);
                            pamSummaryReport.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign, GetNullableInt32, null);
                            pamSummaryReport.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign, GetNullableInt32, null);
                            pamSummaryReport.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonBoothsandExhibits, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonEnrollmentEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonRadioEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonTelevisionEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonElectronicOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonPrintOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonAllEventsActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePartsAandBTopicFocus = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePartsAandBTopicFocus, GetNullableInt32, null);
                            pamSummaryReport.PlanIssuesNonRenewalTerminationEmployerCOBRA = reader.GetDefaultIfDBNull(T.PAMSummary.PlanIssuesNonRenewalTerminationEmployerCOBRA, GetNullableInt32, null);
                            pamSummaryReport.LongTermCare = reader.GetDefaultIfDBNull(T.PAMSummary.LongTermCare, GetNullableInt32, null);
                            pamSummaryReport.MedigapMedicareSupplements = reader.GetDefaultIfDBNull(T.PAMSummary.MedigapMedicareSupplements, GetNullableInt32, null);
                            pamSummaryReport.MedicareFraudandAbuse = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareFraudandAbuse, GetNullableInt32, null);
                            pamSummaryReport.MedicarePrescriptionDrugCoveragePDPMAPD = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePrescriptionDrugCoveragePDPMAPD, GetNullableInt32, null);
                            pamSummaryReport.OtherPrescriptionDrugCoverageAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPrescriptionDrugCoverageAssistance, GetNullableInt32, null);
                            pamSummaryReport.MedicareAdvantageHealthPlans = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareAdvantageHealthPlans, GetNullableInt32, null);
                            pamSummaryReport.QMBSLMBQI = reader.GetDefaultIfDBNull(T.PAMSummary.QMBSLMBQI, GetNullableInt32, null);
                            pamSummaryReport.OtherMedicaid = reader.GetDefaultIfDBNull(T.PAMSummary.OtherMedicaid, GetNullableInt32, null);
                            pamSummaryReport.GeneralSHIPProgramInformation = reader.GetDefaultIfDBNull(T.PAMSummary.GeneralSHIPProgramInformation, GetNullableInt32, null);
                            pamSummaryReport.MedicarePreventiveServices = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreventiveServices, GetNullableInt32, null);
                            pamSummaryReport.LowIncomeAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncomeAssistance, GetNullableInt32, null);
                            pamSummaryReport.DualEligiblewithMentalIllnessMentalDisability = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligiblewithMentalIllnessMentalDisability, GetNullableInt32, null);
                            pamSummaryReport.VolunteerRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.VolunteerRecruitment, GetNullableInt32, null);
                            pamSummaryReport.PartnershipRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipRecruitment, GetNullableInt32, null);
                            pamSummaryReport.OtherTopics = reader.GetDefaultIfDBNull(T.PAMSummary.OtherTopics, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePreEnrolleesAge4564TargetAudience = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreEnrolleesAge4564TargetAudience, GetNullableInt32, null);
                            pamSummaryReport.MedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.FamilyMembersCaregiversofMedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.FamilyMembersCaregiversofMedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.LowIncome = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncome, GetNullableInt32, null);
                            pamSummaryReport.HispanicLatinoorSpanishOrigin = reader.GetDefaultIfDBNull(T.PAMSummary.HispanicLatinoorSpanishOrigin, GetNullableInt32, null);
                            pamSummaryReport.WhiteNonHispanic = reader.GetDefaultIfDBNull(T.PAMSummary.WhiteNonHispanic, GetNullableInt32, null);
                            pamSummaryReport.BlackorAfricanAmerican = reader.GetDefaultIfDBNull(T.PAMSummary.BlackorAfricanAmerican, GetNullableInt32, null);
                            pamSummaryReport.AmericanIndianorAlaskaNative = reader.GetDefaultIfDBNull(T.PAMSummary.AmericanIndianorAlaskaNative, GetNullableInt32, null);
                            pamSummaryReport.AsianIndian = reader.GetDefaultIfDBNull(T.PAMSummary.AsianIndian, GetNullableInt32, null);
                            pamSummaryReport.Chinese = reader.GetDefaultIfDBNull(T.PAMSummary.Chinese, GetNullableInt32, null);
                            pamSummaryReport.Filipino = reader.GetDefaultIfDBNull(T.PAMSummary.Filipino, GetNullableInt32, null);
                            pamSummaryReport.Japanese = reader.GetDefaultIfDBNull(T.PAMSummary.Japanese, GetNullableInt32, null);
                            pamSummaryReport.Korean = reader.GetDefaultIfDBNull(T.PAMSummary.Korean, GetNullableInt32, null);
                            pamSummaryReport.Vietnamese = reader.GetDefaultIfDBNull(T.PAMSummary.Vietnamese, GetNullableInt32, null);
                            pamSummaryReport.NativeHawaiian = reader.GetDefaultIfDBNull(T.PAMSummary.NativeHawaiian, GetNullableInt32, null);
                            pamSummaryReport.GuamanianorChamorro = reader.GetDefaultIfDBNull(T.PAMSummary.GuamanianorChamorro, GetNullableInt32, null);
                            pamSummaryReport.Samoan = reader.GetDefaultIfDBNull(T.PAMSummary.Samoan, GetNullableInt32, null);
                            pamSummaryReport.OtherAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAsian, GetNullableInt32, null);
                            pamSummaryReport.OtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPacificIslander, GetNullableInt32, null);
                            pamSummaryReport.SomeOtherRaceEthnicity = reader.GetDefaultIfDBNull(T.PAMSummary.SomeOtherRaceEthnicity, GetNullableInt32, null);
                            pamSummaryReport.Disabled = reader.GetDefaultIfDBNull(T.PAMSummary.Disabled, GetNullableInt32, null);
                            pamSummaryReport.Rural = reader.GetDefaultIfDBNull(T.PAMSummary.Rural, GetNullableInt32, null);
                            pamSummaryReport.EmployerRelatedGroups = reader.GetDefaultIfDBNull(T.PAMSummary.EmployerRelatedGroups, GetNullableInt32, null);
                            pamSummaryReport.MentalHealthProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.MentalHealthProfessionals, GetNullableInt32, null);
                            pamSummaryReport.SocialWorkProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.SocialWorkProfessionals, GetNullableInt32, null);
                            pamSummaryReport.DualEligibleGroups = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligibleGroups, GetNullableInt32, null);
                            pamSummaryReport.PartnershipOutreach = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipOutreach, GetNullableInt32, null);
                            pamSummaryReport.PresentationstoGroupsinLanguagesOtherThanEnglish = reader.GetDefaultIfDBNull(T.PAMSummary.PresentationstoGroupsinLanguagesOtherThanEnglish, GetNullableInt32, null);
                            pamSummaryReport.OtherAudiences = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAudiences, GetNullableInt32, null);
                            pamSummaryReport.OldMedicareBeneficiariesandorPreEnrollees = reader.GetDefaultIfDBNull(T.PAMSummary.OldMedicareBeneficiariesandorPreEnrollees, GetNullableInt32, null);
                            pamSummaryReport.OldAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OldAsian, GetNullableInt32, null);
                            pamSummaryReport.OldNativeHawaiianorOtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OldNativeHawaiianorOtherPacificIslander, GetNullableInt32, null);
                        }
                    }
                }
            }
            return pamSummaryReport;
        }
        #endregion


        #region GetPAMSummaryReportBySubmitter

        public PAMSummaryReport GetPAMSummaryReportBySubmitter(DateTime DOCStartDate, DateTime DOCEndDate,int AgencyId, int SubmitterId)
        {
            PAMSummaryReport pamSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.GetPAMSummaryReportBySubmitter"))
            {
                database.AddInParameter(command, SP.GetPAMSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.EndDate, DbType.DateTime, DOCEndDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.AgencyId, DbType.Int32, AgencyId);

                database.AddInParameter(command, SP.GetPAMSummaryReport.SubmitterId, DbType.Int32, SubmitterId);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        pamSummaryReport = new PAMSummaryReport
                        {
                            TotalEventsAndActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalEventsAndActivities, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedNumberofAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedNumberofAttendees, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.RadioShowLiveorTapedEstimatedNumberofListenersReached = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedEstimatedNumberofListenersReached, GetNullableInt32, null);
                            pamSummaryReport.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.TVorCableShowLiveorTapedEstimatedNumberofViewersReached = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedEstimatedNumberofViewersReached, GetNullableInt32, null);
                            pamSummaryReport.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign, GetNullableInt32, null);
                            pamSummaryReport.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign, GetNullableInt32, null);
                            pamSummaryReport.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonBoothsandExhibits, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonEnrollmentEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonRadioEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonTelevisionEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonElectronicOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonPrintOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonAllEventsActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePartsAandBTopicFocus = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePartsAandBTopicFocus, GetNullableInt32, null);
                            pamSummaryReport.PlanIssuesNonRenewalTerminationEmployerCOBRA = reader.GetDefaultIfDBNull(T.PAMSummary.PlanIssuesNonRenewalTerminationEmployerCOBRA, GetNullableInt32, null);
                            pamSummaryReport.LongTermCare = reader.GetDefaultIfDBNull(T.PAMSummary.LongTermCare, GetNullableInt32, null);
                            pamSummaryReport.MedigapMedicareSupplements = reader.GetDefaultIfDBNull(T.PAMSummary.MedigapMedicareSupplements, GetNullableInt32, null);
                            pamSummaryReport.MedicareFraudandAbuse = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareFraudandAbuse, GetNullableInt32, null);
                            pamSummaryReport.MedicarePrescriptionDrugCoveragePDPMAPD = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePrescriptionDrugCoveragePDPMAPD, GetNullableInt32, null);
                            pamSummaryReport.OtherPrescriptionDrugCoverageAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPrescriptionDrugCoverageAssistance, GetNullableInt32, null);
                            pamSummaryReport.MedicareAdvantageHealthPlans = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareAdvantageHealthPlans, GetNullableInt32, null);
                            pamSummaryReport.QMBSLMBQI = reader.GetDefaultIfDBNull(T.PAMSummary.QMBSLMBQI, GetNullableInt32, null);
                            pamSummaryReport.OtherMedicaid = reader.GetDefaultIfDBNull(T.PAMSummary.OtherMedicaid, GetNullableInt32, null);
                            pamSummaryReport.GeneralSHIPProgramInformation = reader.GetDefaultIfDBNull(T.PAMSummary.GeneralSHIPProgramInformation, GetNullableInt32, null);
                            pamSummaryReport.MedicarePreventiveServices = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreventiveServices, GetNullableInt32, null);
                            pamSummaryReport.LowIncomeAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncomeAssistance, GetNullableInt32, null);
                            pamSummaryReport.DualEligiblewithMentalIllnessMentalDisability = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligiblewithMentalIllnessMentalDisability, GetNullableInt32, null);
                            pamSummaryReport.VolunteerRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.VolunteerRecruitment, GetNullableInt32, null);
                            pamSummaryReport.PartnershipRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipRecruitment, GetNullableInt32, null);
                            pamSummaryReport.OtherTopics = reader.GetDefaultIfDBNull(T.PAMSummary.OtherTopics, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePreEnrolleesAge4564TargetAudience = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreEnrolleesAge4564TargetAudience, GetNullableInt32, null);
                            pamSummaryReport.MedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.FamilyMembersCaregiversofMedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.FamilyMembersCaregiversofMedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.LowIncome = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncome, GetNullableInt32, null);
                            pamSummaryReport.HispanicLatinoorSpanishOrigin = reader.GetDefaultIfDBNull(T.PAMSummary.HispanicLatinoorSpanishOrigin, GetNullableInt32, null);
                            pamSummaryReport.WhiteNonHispanic = reader.GetDefaultIfDBNull(T.PAMSummary.WhiteNonHispanic, GetNullableInt32, null);
                            pamSummaryReport.BlackorAfricanAmerican = reader.GetDefaultIfDBNull(T.PAMSummary.BlackorAfricanAmerican, GetNullableInt32, null);
                            pamSummaryReport.AmericanIndianorAlaskaNative = reader.GetDefaultIfDBNull(T.PAMSummary.AmericanIndianorAlaskaNative, GetNullableInt32, null);
                            pamSummaryReport.AsianIndian = reader.GetDefaultIfDBNull(T.PAMSummary.AsianIndian, GetNullableInt32, null);
                            pamSummaryReport.Chinese = reader.GetDefaultIfDBNull(T.PAMSummary.Chinese, GetNullableInt32, null);
                            pamSummaryReport.Filipino = reader.GetDefaultIfDBNull(T.PAMSummary.Filipino, GetNullableInt32, null);
                            pamSummaryReport.Japanese = reader.GetDefaultIfDBNull(T.PAMSummary.Japanese, GetNullableInt32, null);
                            pamSummaryReport.Korean = reader.GetDefaultIfDBNull(T.PAMSummary.Korean, GetNullableInt32, null);
                            pamSummaryReport.Vietnamese = reader.GetDefaultIfDBNull(T.PAMSummary.Vietnamese, GetNullableInt32, null);
                            pamSummaryReport.NativeHawaiian = reader.GetDefaultIfDBNull(T.PAMSummary.NativeHawaiian, GetNullableInt32, null);
                            pamSummaryReport.GuamanianorChamorro = reader.GetDefaultIfDBNull(T.PAMSummary.GuamanianorChamorro, GetNullableInt32, null);
                            pamSummaryReport.Samoan = reader.GetDefaultIfDBNull(T.PAMSummary.Samoan, GetNullableInt32, null);
                            pamSummaryReport.OtherAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAsian, GetNullableInt32, null);
                            pamSummaryReport.OtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPacificIslander, GetNullableInt32, null);
                            pamSummaryReport.SomeOtherRaceEthnicity = reader.GetDefaultIfDBNull(T.PAMSummary.SomeOtherRaceEthnicity, GetNullableInt32, null);
                            pamSummaryReport.Disabled = reader.GetDefaultIfDBNull(T.PAMSummary.Disabled, GetNullableInt32, null);
                            pamSummaryReport.Rural = reader.GetDefaultIfDBNull(T.PAMSummary.Rural, GetNullableInt32, null);
                            pamSummaryReport.EmployerRelatedGroups = reader.GetDefaultIfDBNull(T.PAMSummary.EmployerRelatedGroups, GetNullableInt32, null);
                            pamSummaryReport.MentalHealthProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.MentalHealthProfessionals, GetNullableInt32, null);
                            pamSummaryReport.SocialWorkProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.SocialWorkProfessionals, GetNullableInt32, null);
                            pamSummaryReport.DualEligibleGroups = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligibleGroups, GetNullableInt32, null);
                            pamSummaryReport.PartnershipOutreach = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipOutreach, GetNullableInt32, null);
                            pamSummaryReport.PresentationstoGroupsinLanguagesOtherThanEnglish = reader.GetDefaultIfDBNull(T.PAMSummary.PresentationstoGroupsinLanguagesOtherThanEnglish, GetNullableInt32, null);
                            pamSummaryReport.OtherAudiences = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAudiences, GetNullableInt32, null);
                            pamSummaryReport.OldMedicareBeneficiariesandorPreEnrollees = reader.GetDefaultIfDBNull(T.PAMSummary.OldMedicareBeneficiariesandorPreEnrollees, GetNullableInt32, null);
                            pamSummaryReport.OldAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OldAsian, GetNullableInt32, null);
                            pamSummaryReport.OldNativeHawaiianorOtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OldNativeHawaiianorOtherPacificIslander, GetNullableInt32, null);
                        }
                    }
                }
            }
            return pamSummaryReport;
        }
        #endregion


        #region GetPAMSummaryReportBySubStateRegionOnAgency

        public PAMSummaryReport GetPAMSummaryReportBySubStateRegionOnAgency(DateTime DOCStartDate, DateTime DOCEndDate, int SubStateRegionID)
        {
            PAMSummaryReport pamSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.GetPAMSummaryReportBySubStateRegionAgency"))
            {
                database.AddInParameter(command, SP.GetPAMSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.EndDate, DbType.DateTime, DOCEndDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.SubStateRegionID, DbType.Int32, SubStateRegionID);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        pamSummaryReport = new PAMSummaryReport
                        {
                            TotalEventsAndActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalEventsAndActivities, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedNumberofAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedNumberofAttendees, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.RadioShowLiveorTapedEstimatedNumberofListenersReached = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedEstimatedNumberofListenersReached, GetNullableInt32, null);
                            pamSummaryReport.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.TVorCableShowLiveorTapedEstimatedNumberofViewersReached = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedEstimatedNumberofViewersReached, GetNullableInt32, null);
                            pamSummaryReport.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign, GetNullableInt32, null);
                            pamSummaryReport.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign, GetNullableInt32, null);
                            pamSummaryReport.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonBoothsandExhibits, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonEnrollmentEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonRadioEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonTelevisionEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonElectronicOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonPrintOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonAllEventsActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePartsAandBTopicFocus = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePartsAandBTopicFocus, GetNullableInt32, null);
                            pamSummaryReport.PlanIssuesNonRenewalTerminationEmployerCOBRA = reader.GetDefaultIfDBNull(T.PAMSummary.PlanIssuesNonRenewalTerminationEmployerCOBRA, GetNullableInt32, null);
                            pamSummaryReport.LongTermCare = reader.GetDefaultIfDBNull(T.PAMSummary.LongTermCare, GetNullableInt32, null);
                            pamSummaryReport.MedigapMedicareSupplements = reader.GetDefaultIfDBNull(T.PAMSummary.MedigapMedicareSupplements, GetNullableInt32, null);
                            pamSummaryReport.MedicareFraudandAbuse = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareFraudandAbuse, GetNullableInt32, null);
                            pamSummaryReport.MedicarePrescriptionDrugCoveragePDPMAPD = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePrescriptionDrugCoveragePDPMAPD, GetNullableInt32, null);
                            pamSummaryReport.OtherPrescriptionDrugCoverageAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPrescriptionDrugCoverageAssistance, GetNullableInt32, null);
                            pamSummaryReport.MedicareAdvantageHealthPlans = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareAdvantageHealthPlans, GetNullableInt32, null);
                            pamSummaryReport.QMBSLMBQI = reader.GetDefaultIfDBNull(T.PAMSummary.QMBSLMBQI, GetNullableInt32, null);
                            pamSummaryReport.OtherMedicaid = reader.GetDefaultIfDBNull(T.PAMSummary.OtherMedicaid, GetNullableInt32, null);
                            pamSummaryReport.GeneralSHIPProgramInformation = reader.GetDefaultIfDBNull(T.PAMSummary.GeneralSHIPProgramInformation, GetNullableInt32, null);
                            pamSummaryReport.MedicarePreventiveServices = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreventiveServices, GetNullableInt32, null);
                            pamSummaryReport.LowIncomeAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncomeAssistance, GetNullableInt32, null);
                            pamSummaryReport.DualEligiblewithMentalIllnessMentalDisability = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligiblewithMentalIllnessMentalDisability, GetNullableInt32, null);
                            pamSummaryReport.VolunteerRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.VolunteerRecruitment, GetNullableInt32, null);
                            pamSummaryReport.PartnershipRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipRecruitment, GetNullableInt32, null);
                            pamSummaryReport.OtherTopics = reader.GetDefaultIfDBNull(T.PAMSummary.OtherTopics, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePreEnrolleesAge4564TargetAudience = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreEnrolleesAge4564TargetAudience, GetNullableInt32, null);
                            pamSummaryReport.MedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.FamilyMembersCaregiversofMedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.FamilyMembersCaregiversofMedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.LowIncome = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncome, GetNullableInt32, null);
                            pamSummaryReport.HispanicLatinoorSpanishOrigin = reader.GetDefaultIfDBNull(T.PAMSummary.HispanicLatinoorSpanishOrigin, GetNullableInt32, null);
                            pamSummaryReport.WhiteNonHispanic = reader.GetDefaultIfDBNull(T.PAMSummary.WhiteNonHispanic, GetNullableInt32, null);
                            pamSummaryReport.BlackorAfricanAmerican = reader.GetDefaultIfDBNull(T.PAMSummary.BlackorAfricanAmerican, GetNullableInt32, null);
                            pamSummaryReport.AmericanIndianorAlaskaNative = reader.GetDefaultIfDBNull(T.PAMSummary.AmericanIndianorAlaskaNative, GetNullableInt32, null);
                            pamSummaryReport.AsianIndian = reader.GetDefaultIfDBNull(T.PAMSummary.AsianIndian, GetNullableInt32, null);
                            pamSummaryReport.Chinese = reader.GetDefaultIfDBNull(T.PAMSummary.Chinese, GetNullableInt32, null);
                            pamSummaryReport.Filipino = reader.GetDefaultIfDBNull(T.PAMSummary.Filipino, GetNullableInt32, null);
                            pamSummaryReport.Japanese = reader.GetDefaultIfDBNull(T.PAMSummary.Japanese, GetNullableInt32, null);
                            pamSummaryReport.Korean = reader.GetDefaultIfDBNull(T.PAMSummary.Korean, GetNullableInt32, null);
                            pamSummaryReport.Vietnamese = reader.GetDefaultIfDBNull(T.PAMSummary.Vietnamese, GetNullableInt32, null);
                            pamSummaryReport.NativeHawaiian = reader.GetDefaultIfDBNull(T.PAMSummary.NativeHawaiian, GetNullableInt32, null);
                            pamSummaryReport.GuamanianorChamorro = reader.GetDefaultIfDBNull(T.PAMSummary.GuamanianorChamorro, GetNullableInt32, null);
                            pamSummaryReport.Samoan = reader.GetDefaultIfDBNull(T.PAMSummary.Samoan, GetNullableInt32, null);
                            pamSummaryReport.OtherAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAsian, GetNullableInt32, null);
                            pamSummaryReport.OtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPacificIslander, GetNullableInt32, null);
                            pamSummaryReport.SomeOtherRaceEthnicity = reader.GetDefaultIfDBNull(T.PAMSummary.SomeOtherRaceEthnicity, GetNullableInt32, null);
                            pamSummaryReport.Disabled = reader.GetDefaultIfDBNull(T.PAMSummary.Disabled, GetNullableInt32, null);
                            pamSummaryReport.Rural = reader.GetDefaultIfDBNull(T.PAMSummary.Rural, GetNullableInt32, null);
                            pamSummaryReport.EmployerRelatedGroups = reader.GetDefaultIfDBNull(T.PAMSummary.EmployerRelatedGroups, GetNullableInt32, null);
                            pamSummaryReport.MentalHealthProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.MentalHealthProfessionals, GetNullableInt32, null);
                            pamSummaryReport.SocialWorkProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.SocialWorkProfessionals, GetNullableInt32, null);
                            pamSummaryReport.DualEligibleGroups = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligibleGroups, GetNullableInt32, null);
                            pamSummaryReport.PartnershipOutreach = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipOutreach, GetNullableInt32, null);
                            pamSummaryReport.PresentationstoGroupsinLanguagesOtherThanEnglish = reader.GetDefaultIfDBNull(T.PAMSummary.PresentationstoGroupsinLanguagesOtherThanEnglish, GetNullableInt32, null);
                            pamSummaryReport.OtherAudiences = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAudiences, GetNullableInt32, null);
                            pamSummaryReport.OldMedicareBeneficiariesandorPreEnrollees = reader.GetDefaultIfDBNull(T.PAMSummary.OldMedicareBeneficiariesandorPreEnrollees, GetNullableInt32, null);
                            pamSummaryReport.OldAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OldAsian, GetNullableInt32, null);
                            pamSummaryReport.OldNativeHawaiianorOtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OldNativeHawaiianorOtherPacificIslander, GetNullableInt32, null);
                        }
                    }
                }
            }
            return pamSummaryReport;
        }
        #endregion

        #region GetPAMSummaryReportBySubStateRegionCountiesEvent

        public PAMSummaryReport GetPAMSummaryReportBySubStateRegionCountiesEvent(DateTime DOCStartDate, DateTime DOCEndDate, int SubStateRegionID)
        {
            PAMSummaryReport pamSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.GetPAMSummaryReportBySubStateRegionCountiesEvent"))
            {
                database.AddInParameter(command, SP.GetPAMSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.EndDate, DbType.DateTime, DOCEndDate);
                database.AddInParameter(command, SP.GetPAMSummaryReport.SubStateRegionID, DbType.Int32, SubStateRegionID);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        pamSummaryReport = new PAMSummaryReport
                        {
                            TotalEventsAndActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalEventsAndActivities, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedNumberofAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedNumberofAttendees, GetNullableInt32, null);
                            pamSummaryReport.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees, GetNullableInt32, null);
                            pamSummaryReport.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP, GetNullableInt32, null);
                            pamSummaryReport.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram = reader.GetDefaultIfDBNull(T.PAMSummary.DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.RadioShowLiveorTapedEstimatedNumberofListenersReached = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedEstimatedNumberofListenersReached, GetNullableInt32, null);
                            pamSummaryReport.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.TVorCableShowLiveorTapedEstimatedNumberofViewersReached = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedEstimatedNumberofViewersReached, GetNullableInt32, null);
                            pamSummaryReport.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign, GetNullableInt32, null);
                            pamSummaryReport.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents = reader.GetDefaultIfDBNull(T.PAMSummary.ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign, GetNullableInt32, null);
                            pamSummaryReport.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents = reader.GetDefaultIfDBNull(T.PAMSummary.PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonBoothsandExhibits = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonBoothsandExhibits, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonEnrollmentEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonEnrollmentEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonRadioEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonRadioEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonTelevisionEvents = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonTelevisionEvents, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonElectronicOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonElectronicOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonPrintOtherActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonPrintOtherActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities, GetNullableInt32, null);
                            pamSummaryReport.TotalPersonHoursofEffortSpentonAllEventsActivities = reader.GetDefaultIfDBNull(T.PAMSummary.TotalPersonHoursofEffortSpentonAllEventsActivities, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePartsAandBTopicFocus = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePartsAandBTopicFocus, GetNullableInt32, null);
                            pamSummaryReport.PlanIssuesNonRenewalTerminationEmployerCOBRA = reader.GetDefaultIfDBNull(T.PAMSummary.PlanIssuesNonRenewalTerminationEmployerCOBRA, GetNullableInt32, null);
                            pamSummaryReport.LongTermCare = reader.GetDefaultIfDBNull(T.PAMSummary.LongTermCare, GetNullableInt32, null);
                            pamSummaryReport.MedigapMedicareSupplements = reader.GetDefaultIfDBNull(T.PAMSummary.MedigapMedicareSupplements, GetNullableInt32, null);
                            pamSummaryReport.MedicareFraudandAbuse = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareFraudandAbuse, GetNullableInt32, null);
                            pamSummaryReport.MedicarePrescriptionDrugCoveragePDPMAPD = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePrescriptionDrugCoveragePDPMAPD, GetNullableInt32, null);
                            pamSummaryReport.OtherPrescriptionDrugCoverageAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPrescriptionDrugCoverageAssistance, GetNullableInt32, null);
                            pamSummaryReport.MedicareAdvantageHealthPlans = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareAdvantageHealthPlans, GetNullableInt32, null);
                            pamSummaryReport.QMBSLMBQI = reader.GetDefaultIfDBNull(T.PAMSummary.QMBSLMBQI, GetNullableInt32, null);
                            pamSummaryReport.OtherMedicaid = reader.GetDefaultIfDBNull(T.PAMSummary.OtherMedicaid, GetNullableInt32, null);
                            pamSummaryReport.GeneralSHIPProgramInformation = reader.GetDefaultIfDBNull(T.PAMSummary.GeneralSHIPProgramInformation, GetNullableInt32, null);
                            pamSummaryReport.MedicarePreventiveServices = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreventiveServices, GetNullableInt32, null);
                            pamSummaryReport.LowIncomeAssistance = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncomeAssistance, GetNullableInt32, null);
                            pamSummaryReport.DualEligiblewithMentalIllnessMentalDisability = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligiblewithMentalIllnessMentalDisability, GetNullableInt32, null);
                            pamSummaryReport.VolunteerRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.VolunteerRecruitment, GetNullableInt32, null);
                            pamSummaryReport.PartnershipRecruitment = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipRecruitment, GetNullableInt32, null);
                            pamSummaryReport.OtherTopics = reader.GetDefaultIfDBNull(T.PAMSummary.OtherTopics, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            pamSummaryReport.MedicarePreEnrolleesAge4564TargetAudience = reader.GetDefaultIfDBNull(T.PAMSummary.MedicarePreEnrolleesAge4564TargetAudience, GetNullableInt32, null);
                            pamSummaryReport.MedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.MedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.FamilyMembersCaregiversofMedicareBeneficiaries = reader.GetDefaultIfDBNull(T.PAMSummary.FamilyMembersCaregiversofMedicareBeneficiaries, GetNullableInt32, null);
                            pamSummaryReport.LowIncome = reader.GetDefaultIfDBNull(T.PAMSummary.LowIncome, GetNullableInt32, null);
                            pamSummaryReport.HispanicLatinoorSpanishOrigin = reader.GetDefaultIfDBNull(T.PAMSummary.HispanicLatinoorSpanishOrigin, GetNullableInt32, null);
                            pamSummaryReport.WhiteNonHispanic = reader.GetDefaultIfDBNull(T.PAMSummary.WhiteNonHispanic, GetNullableInt32, null);
                            pamSummaryReport.BlackorAfricanAmerican = reader.GetDefaultIfDBNull(T.PAMSummary.BlackorAfricanAmerican, GetNullableInt32, null);
                            pamSummaryReport.AmericanIndianorAlaskaNative = reader.GetDefaultIfDBNull(T.PAMSummary.AmericanIndianorAlaskaNative, GetNullableInt32, null);
                            pamSummaryReport.AsianIndian = reader.GetDefaultIfDBNull(T.PAMSummary.AsianIndian, GetNullableInt32, null);
                            pamSummaryReport.Chinese = reader.GetDefaultIfDBNull(T.PAMSummary.Chinese, GetNullableInt32, null);
                            pamSummaryReport.Filipino = reader.GetDefaultIfDBNull(T.PAMSummary.Filipino, GetNullableInt32, null);
                            pamSummaryReport.Japanese = reader.GetDefaultIfDBNull(T.PAMSummary.Japanese, GetNullableInt32, null);
                            pamSummaryReport.Korean = reader.GetDefaultIfDBNull(T.PAMSummary.Korean, GetNullableInt32, null);
                            pamSummaryReport.Vietnamese = reader.GetDefaultIfDBNull(T.PAMSummary.Vietnamese, GetNullableInt32, null);
                            pamSummaryReport.NativeHawaiian = reader.GetDefaultIfDBNull(T.PAMSummary.NativeHawaiian, GetNullableInt32, null);
                            pamSummaryReport.GuamanianorChamorro = reader.GetDefaultIfDBNull(T.PAMSummary.GuamanianorChamorro, GetNullableInt32, null);
                            pamSummaryReport.Samoan = reader.GetDefaultIfDBNull(T.PAMSummary.Samoan, GetNullableInt32, null);
                            pamSummaryReport.OtherAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAsian, GetNullableInt32, null);
                            pamSummaryReport.OtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OtherPacificIslander, GetNullableInt32, null);
                            pamSummaryReport.SomeOtherRaceEthnicity = reader.GetDefaultIfDBNull(T.PAMSummary.SomeOtherRaceEthnicity, GetNullableInt32, null);
                            pamSummaryReport.Disabled = reader.GetDefaultIfDBNull(T.PAMSummary.Disabled, GetNullableInt32, null);
                            pamSummaryReport.Rural = reader.GetDefaultIfDBNull(T.PAMSummary.Rural, GetNullableInt32, null);
                            pamSummaryReport.EmployerRelatedGroups = reader.GetDefaultIfDBNull(T.PAMSummary.EmployerRelatedGroups, GetNullableInt32, null);
                            pamSummaryReport.MentalHealthProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.MentalHealthProfessionals, GetNullableInt32, null);
                            pamSummaryReport.SocialWorkProfessionals = reader.GetDefaultIfDBNull(T.PAMSummary.SocialWorkProfessionals, GetNullableInt32, null);
                            pamSummaryReport.DualEligibleGroups = reader.GetDefaultIfDBNull(T.PAMSummary.DualEligibleGroups, GetNullableInt32, null);
                            pamSummaryReport.PartnershipOutreach = reader.GetDefaultIfDBNull(T.PAMSummary.PartnershipOutreach, GetNullableInt32, null);
                            pamSummaryReport.PresentationstoGroupsinLanguagesOtherThanEnglish = reader.GetDefaultIfDBNull(T.PAMSummary.PresentationstoGroupsinLanguagesOtherThanEnglish, GetNullableInt32, null);
                            pamSummaryReport.OtherAudiences = reader.GetDefaultIfDBNull(T.PAMSummary.OtherAudiences, GetNullableInt32, null);
                            pamSummaryReport.OldMedicareBeneficiariesandorPreEnrollees = reader.GetDefaultIfDBNull(T.PAMSummary.OldMedicareBeneficiariesandorPreEnrollees, GetNullableInt32, null);
                            pamSummaryReport.OldAsian = reader.GetDefaultIfDBNull(T.PAMSummary.OldAsian, GetNullableInt32, null);
                            pamSummaryReport.OldNativeHawaiianorOtherPacificIslander = reader.GetDefaultIfDBNull(T.PAMSummary.OldNativeHawaiianorOtherPacificIslander, GetNullableInt32, null);
                        }
                    }
                }
            }
            return pamSummaryReport;
        }
        #endregion
    }
}
