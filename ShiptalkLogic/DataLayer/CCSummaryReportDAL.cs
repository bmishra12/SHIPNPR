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
    internal class CCSummaryReportDAL : DALBase
    {
        public CCSummaryReport GetCCSummaryReportByState(DateTime DOCStartDate, DateTime DOCEndDate, string StateFIPSCode)
        {
            CCSummaryReport ccSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.CCSummaryReportByState"))
            {
                database.AddInParameter(command, SP.GetCCSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.EndDate, DbType.DateTime, DOCEndDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.StateFIPS, DbType.String, StateFIPSCode);
                
                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        ccSummaryReport = new CCSummaryReport
                        {
                            TotalClientContacts = reader.GetDefaultIfDBNull(T.CCSummaryReport.TotalClientContacts, GetNullableInt32, null),
                        };
                        reader.NextResult();
                        
                        while(reader.Read())
                        {
                            ccSummaryReport.ClientFirstVsContinuingContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact3, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientLearnedAboutSHIP1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP1, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP2, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP3, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP4, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP5, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP6, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP7, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP8, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP9, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP10, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMethodOfContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact3, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact4, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact5, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact6, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact7, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact8, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAgeGroup1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup1, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup2, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup3, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup4, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup5, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientGender1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender1, GetNullableInt32, null);
                            ccSummaryReport.ClientGender2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender2, GetNullableInt32, null);
                            ccSummaryReport.ClientGender3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactRace1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace6, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace7, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace8, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace9, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace10, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace11, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace12, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace13, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace14, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace15, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace16, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace17, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace18, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace19, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace20, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace21, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish1, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish2, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMonthlyIncome1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome1, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome2, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAssests1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests1, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests2, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability1, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability2, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientDualEligble1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble1, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble2, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID1, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID2, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID3, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID4, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID5, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID6, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID7, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID8, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID9, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID10, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID62 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID62, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID63 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID63, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID11, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID12, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID13, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID14, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID15, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID64 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID64, GetNullableInt32, null);
                            
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID16, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID17, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID18, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID19, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID20, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID65 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID65, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID66 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID66, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID21, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID22 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID22, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID23 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID23, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID24 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID24, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID25 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID25, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID26 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID26, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID60 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID60, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID61 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID61, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID27 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID27, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID28 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID28, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID29 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID29, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID30 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID30, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID31 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID31, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID32 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID32, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID33 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID33, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID34 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID34, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID35 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID35, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID36 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID36, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID67 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID67, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID68 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID68, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID37 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID37, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID38 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID38, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID39 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID39, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID40 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID40, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID41 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID41, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID42 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID42, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID43 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID43, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID44 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID44, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID45 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID45, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID70 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID70, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID71 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID71, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID72 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID72, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID46 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID46, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID47 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID47, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID48 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID48, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID49 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID49, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID50 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID50, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID51 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID51, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID69 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID69, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID52 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID52, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID53 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID53, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID54 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID54, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID55 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID55, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID56 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID56, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID57 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID57, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID58 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID58, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID59 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID59, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID73 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID73, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID74 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID74, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactHoursSpent1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent6, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactCurrentStatus1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus6, GetNullableInt32, null);
                        }
                    }
                }
            }
            return ccSummaryReport;
        }

        public CCSummaryReport GetCCSummaryReportByAgency(DateTime DOCStartDate, DateTime DOCEndDate, string AgencyId)
        {
            CCSummaryReport ccSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.CCSummaryReportByAgency"))
            {
                database.AddInParameter(command, SP.GetCCSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.EndDate, DbType.DateTime, DOCEndDate);

                database.AddInParameter(command, SP.GetCCSummaryReport.AgencyId, DbType.String, AgencyId);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        ccSummaryReport = new CCSummaryReport
                        {
                            TotalClientContacts = reader.GetDefaultIfDBNull(T.CCSummaryReport.TotalClientContacts, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientFirstVsContinuingContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact3, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientLearnedAboutSHIP1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP1, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP2, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP3, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP4, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP5, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP6, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP7, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP8, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP9, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP10, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMethodOfContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact3, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact4, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact5, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact6, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact7, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact8, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAgeGroup1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup1, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup2, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup3, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup4, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup5, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientGender1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender1, GetNullableInt32, null);
                            ccSummaryReport.ClientGender2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender2, GetNullableInt32, null);
                            ccSummaryReport.ClientGender3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactRace1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace6, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace7, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace8, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace9, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace10, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace11, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace12, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace13, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace14, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace15, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace16, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace17, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace18, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace19, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace20, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace21, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish1, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish2, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMonthlyIncome1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome1, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome2, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAssests1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests1, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests2, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability1, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability2, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientDualEligble1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble1, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble2, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID1, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID2, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID3, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID4, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID5, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID6, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID7, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID8, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID9, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID10, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID62 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID62, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID63 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID63, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID11, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID12, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID13, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID14, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID15, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID64 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID64, GetNullableInt32, null);

                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID16, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID17, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID18, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID19, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID20, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID65 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID65, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID66 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID66, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID21, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID22 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID22, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID23 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID23, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID24 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID24, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID25 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID25, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID26 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID26, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID60 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID60, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID61 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID61, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID27 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID27, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID28 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID28, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID29 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID29, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID30 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID30, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID31 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID31, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID32 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID32, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID33 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID33, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID34 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID34, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID35 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID35, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID36 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID36, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID67 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID67, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID68 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID68, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID37 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID37, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID38 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID38, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID39 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID39, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID40 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID40, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID41 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID41, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID42 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID42, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID43 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID43, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID44 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID44, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID45 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID45, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID70 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID70, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID71 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID71, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID72 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID72, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID46 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID46, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID47 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID47, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID48 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID48, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID49 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID49, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID50 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID50, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID51 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID51, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID69 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID69, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID52 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID52, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID53 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID53, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID54 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID54, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID55 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID55, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID56 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID56, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID57 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID57, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID58 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID58, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID59 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID59, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID73 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID73, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID74 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID74, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactHoursSpent1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent6, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactCurrentStatus1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus6, GetNullableInt32, null);
                        }
                    }
                }
            }
            return ccSummaryReport;
        }

        public CCSummaryReport GetCCSummaryReportByCountyOfCounselorLocation(DateTime DOCStartDate, DateTime DOCEndDate, string CountyCounselorLocation,int ScopeId, int UserId, int AgencyId)
        {
            CCSummaryReport ccSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.CCSummaryReportByCountyCounselorLocation"))
            {
                database.AddInParameter(command, SP.GetCCSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.EndDate, DbType.DateTime, DOCEndDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.CountyCounselorLocation, DbType.String, CountyCounselorLocation);
                database.AddInParameter(command, SP.GetCCSummaryReport.ScopeId, DbType.Int32, ScopeId);
                database.AddInParameter(command, SP.GetCCSummaryReport.UserId, DbType.Int32, UserId);
                database.AddInParameter(command, SP.GetCCSummaryReport.AgencyId, DbType.Int32, AgencyId);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        ccSummaryReport = new CCSummaryReport
                        {
                            TotalClientContacts = reader.GetDefaultIfDBNull(T.CCSummaryReport.TotalClientContacts, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientFirstVsContinuingContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact3, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientLearnedAboutSHIP1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP1, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP2, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP3, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP4, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP5, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP6, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP7, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP8, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP9, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP10, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMethodOfContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact3, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact4, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact5, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact6, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact7, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact8, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAgeGroup1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup1, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup2, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup3, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup4, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup5, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientGender1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender1, GetNullableInt32, null);
                            ccSummaryReport.ClientGender2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender2, GetNullableInt32, null);
                            ccSummaryReport.ClientGender3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactRace1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace6, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace7, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace8, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace9, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace10, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace11, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace12, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace13, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace14, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace15, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace16, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace17, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace18, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace19, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace20, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace21, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish1, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish2, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMonthlyIncome1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome1, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome2, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAssests1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests1, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests2, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability1, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability2, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientDualEligble1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble1, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble2, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID1, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID2, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID3, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID4, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID5, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID6, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID7, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID8, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID9, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID10, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID62 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID62, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID63 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID63, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID11, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID12, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID13, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID14, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID15, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID64 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID64, GetNullableInt32, null);

                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID16, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID17, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID18, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID19, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID20, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID65 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID65, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID66 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID66, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID21, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID22 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID22, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID23 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID23, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID24 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID24, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID25 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID25, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID26 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID26, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID60 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID60, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID61 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID61, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID27 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID27, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID28 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID28, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID29 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID29, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID30 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID30, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID31 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID31, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID32 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID32, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID33 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID33, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID34 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID34, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID35 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID35, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID36 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID36, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID67 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID67, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID68 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID68, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID37 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID37, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID38 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID38, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID39 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID39, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID40 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID40, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID41 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID41, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID42 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID42, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID43 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID43, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID44 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID44, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID45 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID45, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID70 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID70, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID71 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID71, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID72 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID72, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID46 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID46, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID47 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID47, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID48 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID48, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID49 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID49, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID50 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID50, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID51 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID51, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID69 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID69, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID52 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID52, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID53 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID53, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID54 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID54, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID55 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID55, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID56 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID56, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID57 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID57, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID58 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID58, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID59 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID59, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID73 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID73, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID74 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID74, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactHoursSpent1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent6, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactCurrentStatus1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus6, GetNullableInt32, null);
                        }
                    }
                }
            }
            return ccSummaryReport;
        }
        public CCSummaryReport GetCCSummaryReportByZipCodeOfCounselorLocation(DateTime DOCStartDate, DateTime DOCEndDate, string ZipCodeCounselorLocation, int ScopeId, int UserId, int AgencyId)
        {
            CCSummaryReport ccSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.CCSummaryReportByZipCodeCounselorLocation"))
            {
                database.AddInParameter(command, SP.GetCCSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.EndDate, DbType.DateTime, DOCEndDate);

                database.AddInParameter(command, SP.GetCCSummaryReport.ZipCodeCounselorLocation, DbType.String, ZipCodeCounselorLocation);
                database.AddInParameter(command, SP.GetCCSummaryReport.ScopeId, DbType.Int32, ScopeId);
                database.AddInParameter(command, SP.GetCCSummaryReport.UserId, DbType.Int32, UserId);
                database.AddInParameter(command, SP.GetCCSummaryReport.AgencyId, DbType.Int32, AgencyId);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        ccSummaryReport = new CCSummaryReport
                        {
                            TotalClientContacts = reader.GetDefaultIfDBNull(T.CCSummaryReport.TotalClientContacts, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientFirstVsContinuingContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact3, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientLearnedAboutSHIP1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP1, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP2, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP3, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP4, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP5, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP6, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP7, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP8, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP9, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP10, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMethodOfContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact3, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact4, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact5, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact6, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact7, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact8, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAgeGroup1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup1, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup2, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup3, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup4, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup5, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientGender1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender1, GetNullableInt32, null);
                            ccSummaryReport.ClientGender2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender2, GetNullableInt32, null);
                            ccSummaryReport.ClientGender3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactRace1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace6, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace7, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace8, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace9, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace10, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace11, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace12, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace13, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace14, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace15, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace16, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace17, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace18, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace19, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace20, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace21, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish1, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish2, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMonthlyIncome1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome1, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome2, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAssests1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests1, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests2, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability1, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability2, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientDualEligble1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble1, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble2, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID1, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID2, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID3, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID4, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID5, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID6, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID7, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID8, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID9, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID10, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID62 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID62, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID63 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID63, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID11, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID12, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID13, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID14, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID15, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID64 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID64, GetNullableInt32, null);

                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID16, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID17, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID18, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID19, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID20, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID65 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID65, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID66 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID66, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID21, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID22 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID22, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID23 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID23, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID24 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID24, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID25 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID25, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID26 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID26, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID60 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID60, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID61 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID61, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID27 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID27, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID28 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID28, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID29 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID29, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID30 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID30, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID31 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID31, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID32 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID32, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID33 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID33, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID34 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID34, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID35 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID35, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID36 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID36, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID67 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID67, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID68 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID68, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID37 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID37, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID38 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID38, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID39 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID39, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID40 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID40, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID41 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID41, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID42 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID42, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID43 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID43, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID44 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID44, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID45 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID45, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID70 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID70, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID71 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID71, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID72 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID72, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID46 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID46, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID47 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID47, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID48 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID48, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID49 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID49, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID50 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID50, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID51 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID51, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID69 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID69, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID52 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID52, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID53 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID53, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID54 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID54, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID55 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID55, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID56 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID56, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID57 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID57, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID58 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID58, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID59 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID59, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID73 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID73, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID74 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID74, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactHoursSpent1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent6, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactCurrentStatus1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus6, GetNullableInt32, null);
                        }
                    }
                }
            }
            return ccSummaryReport;
        }

        public CCSummaryReport GetCCSummaryReportByCountyOfClientResidence(DateTime DOCStartDate, DateTime DOCEndDate, string CountyCodeClientResidence, int ScopeId, int UserId, int AgencyId, string StateFIPSCode)
        {
            CCSummaryReport ccSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.CCSummaryReportByCountyClientResidence"))
            {
                database.AddInParameter(command, SP.GetCCSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.EndDate, DbType.DateTime, DOCEndDate);

                database.AddInParameter(command, SP.GetCCSummaryReport.CountyCodeClientResidence, DbType.String, CountyCodeClientResidence);
                database.AddInParameter(command, SP.GetCCSummaryReport.ScopeId, DbType.Int32, ScopeId);
                database.AddInParameter(command, SP.GetCCSummaryReport.UserId, DbType.Int32, UserId);
                database.AddInParameter(command, SP.GetCCSummaryReport.AgencyId, DbType.Int32, AgencyId);
                database.AddInParameter(command, SP.GetCCSummaryReport.StateFIPS, DbType.String, StateFIPSCode);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        ccSummaryReport = new CCSummaryReport
                        {
                            TotalClientContacts = reader.GetDefaultIfDBNull(T.CCSummaryReport.TotalClientContacts, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientFirstVsContinuingContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact3, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientLearnedAboutSHIP1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP1, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP2, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP3, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP4, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP5, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP6, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP7, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP8, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP9, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP10, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMethodOfContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact3, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact4, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact5, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact6, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact7, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact8, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAgeGroup1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup1, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup2, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup3, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup4, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup5, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientGender1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender1, GetNullableInt32, null);
                            ccSummaryReport.ClientGender2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender2, GetNullableInt32, null);
                            ccSummaryReport.ClientGender3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactRace1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace6, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace7, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace8, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace9, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace10, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace11, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace12, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace13, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace14, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace15, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace16, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace17, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace18, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace19, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace20, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace21, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish1, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish2, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMonthlyIncome1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome1, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome2, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAssests1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests1, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests2, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability1, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability2, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientDualEligble1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble1, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble2, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID1, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID2, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID3, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID4, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID5, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID6, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID7, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID8, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID9, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID10, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID62 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID62, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID63 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID63, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID11, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID12, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID13, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID14, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID15, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID64 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID64, GetNullableInt32, null);

                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID16, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID17, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID18, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID19, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID20, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID65 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID65, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID66 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID66, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID21, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID22 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID22, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID23 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID23, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID24 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID24, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID25 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID25, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID26 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID26, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID60 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID60, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID61 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID61, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID27 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID27, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID28 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID28, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID29 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID29, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID30 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID30, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID31 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID31, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID32 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID32, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID33 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID33, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID34 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID34, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID35 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID35, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID36 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID36, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID67 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID67, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID68 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID68, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID37 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID37, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID38 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID38, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID39 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID39, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID40 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID40, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID41 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID41, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID42 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID42, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID43 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID43, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID44 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID44, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID45 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID45, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID70 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID70, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID71 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID71, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID72 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID72, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID46 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID46, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID47 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID47, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID48 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID48, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID49 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID49, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID50 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID50, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID51 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID51, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID69 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID69, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID52 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID52, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID53 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID53, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID54 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID54, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID55 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID55, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID56 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID56, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID57 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID57, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID58 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID58, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID59 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID59, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID73 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID73, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID74 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID74, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactHoursSpent1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent6, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactCurrentStatus1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus6, GetNullableInt32, null);
                        }
                    }
                }
            }
            return ccSummaryReport;
        }

        public CCSummaryReport GetCCSummaryReportByZipCodeOfClientResidence(DateTime DOCStartDate, DateTime DOCEndDate, string ZIPCodeofClientResidence, int ScopeId, int UserId, int AgencyId)
        {
            CCSummaryReport ccSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.CCSummaryReportByZipCodeClientResidence"))
            {
                database.AddInParameter(command, SP.GetCCSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.EndDate, DbType.DateTime, DOCEndDate);

                database.AddInParameter(command, SP.GetCCSummaryReport.ZipCodeClientResidence, DbType.String, ZIPCodeofClientResidence);
                database.AddInParameter(command, SP.GetCCSummaryReport.ScopeId, DbType.Int32, ScopeId);
                database.AddInParameter(command, SP.GetCCSummaryReport.UserId, DbType.Int32, UserId);
                database.AddInParameter(command, SP.GetCCSummaryReport.AgencyId, DbType.Int32, AgencyId);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        ccSummaryReport = new CCSummaryReport
                        {
                            TotalClientContacts = reader.GetDefaultIfDBNull(T.CCSummaryReport.TotalClientContacts, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientFirstVsContinuingContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact3, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientLearnedAboutSHIP1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP1, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP2, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP3, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP4, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP5, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP6, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP7, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP8, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP9, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP10, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMethodOfContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact3, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact4, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact5, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact6, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact7, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact8, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAgeGroup1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup1, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup2, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup3, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup4, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup5, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientGender1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender1, GetNullableInt32, null);
                            ccSummaryReport.ClientGender2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender2, GetNullableInt32, null);
                            ccSummaryReport.ClientGender3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactRace1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace6, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace7, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace8, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace9, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace10, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace11, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace12, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace13, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace14, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace15, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace16, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace17, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace18, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace19, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace20, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace21, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish1, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish2, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMonthlyIncome1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome1, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome2, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAssests1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests1, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests2, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability1, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability2, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientDualEligble1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble1, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble2, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID1, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID2, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID3, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID4, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID5, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID6, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID7, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID8, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID9, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID10, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID62 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID62, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID63 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID63, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID11, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID12, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID13, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID14, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID15, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID64 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID64, GetNullableInt32, null);

                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID16, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID17, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID18, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID19, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID20, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID65 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID65, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID66 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID66, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID21, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID22 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID22, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID23 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID23, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID24 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID24, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID25 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID25, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID26 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID26, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID60 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID60, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID61 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID61, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID27 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID27, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID28 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID28, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID29 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID29, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID30 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID30, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID31 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID31, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID32 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID32, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID33 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID33, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID34 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID34, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID35 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID35, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID36 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID36, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID67 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID67, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID68 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID68, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID37 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID37, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID38 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID38, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID39 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID39, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID40 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID40, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID41 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID41, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID42 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID42, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID43 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID43, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID44 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID44, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID45 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID45, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID70 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID70, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID71 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID71, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID72 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID72, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID46 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID46, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID47 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID47, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID48 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID48, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID49 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID49, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID50 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID50, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID51 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID51, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID69 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID69, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID52 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID52, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID53 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID53, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID54 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID54, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID55 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID55, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID56 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID56, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID57 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID57, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID58 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID58, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID59 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID59, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID73 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID73, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID74 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID74, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactHoursSpent1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent6, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactCurrentStatus1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus6, GetNullableInt32, null);
                        }
                    }
                }
            }
            return ccSummaryReport;
        }

        public CCSummaryReport GetCCSummaryReportByContactsBySubStateRegionOnAgency(DateTime DOCStartDate, DateTime DOCEndDate, int SubStateRegionID)
        {
            CCSummaryReport ccSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.CCSummaryReportBySubstateRegionAgency"))
            {
                database.AddInParameter(command, SP.GetCCSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.EndDate, DbType.DateTime, DOCEndDate);

                database.AddInParameter(command, SP.GetCCSummaryReport.SubStateRegionID, DbType.Int32, SubStateRegionID);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        ccSummaryReport = new CCSummaryReport
                        {
                            TotalClientContacts = reader.GetDefaultIfDBNull(T.CCSummaryReport.TotalClientContacts, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientFirstVsContinuingContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact3, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientLearnedAboutSHIP1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP1, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP2, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP3, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP4, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP5, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP6, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP7, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP8, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP9, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP10, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMethodOfContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact3, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact4, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact5, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact6, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact7, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact8, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAgeGroup1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup1, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup2, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup3, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup4, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup5, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientGender1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender1, GetNullableInt32, null);
                            ccSummaryReport.ClientGender2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender2, GetNullableInt32, null);
                            ccSummaryReport.ClientGender3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactRace1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace6, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace7, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace8, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace9, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace10, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace11, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace12, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace13, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace14, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace15, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace16, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace17, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace18, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace19, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace20, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace21, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish1, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish2, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMonthlyIncome1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome1, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome2, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAssests1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests1, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests2, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability1, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability2, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientDualEligble1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble1, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble2, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID1, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID2, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID3, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID4, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID5, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID6, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID7, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID8, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID9, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID10, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID62 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID62, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID63 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID63, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID11, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID12, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID13, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID14, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID15, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID64 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID64, GetNullableInt32, null);

                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID16, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID17, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID18, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID19, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID20, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID65 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID65, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID66 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID66, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID21, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID22 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID22, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID23 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID23, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID24 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID24, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID25 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID25, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID26 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID26, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID60 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID60, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID61 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID61, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID27 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID27, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID28 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID28, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID29 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID29, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID30 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID30, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID31 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID31, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID32 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID32, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID33 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID33, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID34 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID34, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID35 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID35, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID36 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID36, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID67 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID67, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID68 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID68, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID37 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID37, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID38 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID38, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID39 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID39, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID40 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID40, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID41 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID41, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID42 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID42, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID43 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID43, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID44 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID44, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID45 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID45, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID70 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID70, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID71 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID71, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID72 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID72, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID46 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID46, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID47 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID47, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID48 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID48, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID49 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID49, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID50 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID50, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID51 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID51, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID69 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID69, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID52 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID52, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID53 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID53, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID54 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID54, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID55 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID55, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID56 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID56, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID57 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID57, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID58 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID58, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID59 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID59, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID73 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID73, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID74 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID74, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactHoursSpent1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent6, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactCurrentStatus1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus6, GetNullableInt32, null);
                        }
                    }
                }
            }
            return ccSummaryReport;
        }

        public CCSummaryReport GetCCSummaryReportByContactsBySubStateRegionOnCountiesoOfCounselorLocation(DateTime DOCStartDate, DateTime DOCEndDate, int SubStateRegionID)
        {
            CCSummaryReport ccSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.CCSummaryReportBySubstateRegionCountyCounselorLocation"))
            {
                database.AddInParameter(command, SP.GetCCSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.EndDate, DbType.DateTime, DOCEndDate);

                database.AddInParameter(command, SP.GetCCSummaryReport.SubStateRegionID, DbType.Int32, SubStateRegionID);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        ccSummaryReport = new CCSummaryReport
                        {
                            TotalClientContacts = reader.GetDefaultIfDBNull(T.CCSummaryReport.TotalClientContacts, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientFirstVsContinuingContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact3, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientLearnedAboutSHIP1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP1, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP2, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP3, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP4, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP5, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP6, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP7, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP8, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP9, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP10, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMethodOfContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact3, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact4, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact5, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact6, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact7, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact8, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAgeGroup1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup1, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup2, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup3, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup4, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup5, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientGender1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender1, GetNullableInt32, null);
                            ccSummaryReport.ClientGender2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender2, GetNullableInt32, null);
                            ccSummaryReport.ClientGender3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactRace1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace6, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace7, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace8, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace9, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace10, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace11, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace12, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace13, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace14, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace15, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace16, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace17, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace18, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace19, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace20, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace21, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish1, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish2, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMonthlyIncome1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome1, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome2, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAssests1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests1, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests2, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability1, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability2, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientDualEligble1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble1, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble2, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID1, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID2, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID3, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID4, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID5, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID6, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID7, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID8, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID9, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID10, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID62 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID62, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID63 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID63, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID11, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID12, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID13, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID14, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID15, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID64 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID64, GetNullableInt32, null);

                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID16, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID17, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID18, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID19, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID20, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID65 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID65, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID66 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID66, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID21, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID22 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID22, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID23 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID23, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID24 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID24, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID25 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID25, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID26 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID26, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID60 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID60, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID61 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID61, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID27 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID27, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID28 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID28, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID29 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID29, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID30 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID30, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID31 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID31, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID32 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID32, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID33 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID33, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID34 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID34, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID35 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID35, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID36 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID36, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID67 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID67, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID68 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID68, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID37 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID37, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID38 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID38, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID39 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID39, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID40 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID40, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID41 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID41, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID42 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID42, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID43 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID43, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID44 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID44, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID45 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID45, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID70 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID70, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID71 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID71, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID72 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID72, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID46 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID46, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID47 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID47, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID48 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID48, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID49 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID49, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID50 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID50, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID51 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID51, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID69 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID69, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID52 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID52, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID53 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID53, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID54 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID54, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID55 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID55, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID56 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID56, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID57 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID57, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID58 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID58, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID59 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID59, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID73 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID73, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID74 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID74, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactHoursSpent1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent6, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactCurrentStatus1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus6, GetNullableInt32, null);
                        }
                    }
                }
            }
            return ccSummaryReport;
        }

        public CCSummaryReport GetCCSummaryReportByContactsBySubStateRegionOnZipcodesOfCounselorLocation(DateTime DOCStartDate, DateTime DOCEndDate, int SubStateRegionID)
        {
            CCSummaryReport ccSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.CCSummaryReportBySubstateRegionZIPCodeCounselorLocation"))
            {
                database.AddInParameter(command, SP.GetCCSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.EndDate, DbType.DateTime, DOCEndDate);

                database.AddInParameter(command, SP.GetCCSummaryReport.SubStateRegionID, DbType.Int32, SubStateRegionID);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        ccSummaryReport = new CCSummaryReport
                        {
                            TotalClientContacts = reader.GetDefaultIfDBNull(T.CCSummaryReport.TotalClientContacts, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientFirstVsContinuingContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact3, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientLearnedAboutSHIP1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP1, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP2, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP3, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP4, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP5, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP6, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP7, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP8, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP9, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP10, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMethodOfContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact3, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact4, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact5, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact6, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact7, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact8, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAgeGroup1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup1, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup2, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup3, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup4, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup5, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientGender1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender1, GetNullableInt32, null);
                            ccSummaryReport.ClientGender2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender2, GetNullableInt32, null);
                            ccSummaryReport.ClientGender3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactRace1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace6, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace7, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace8, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace9, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace10, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace11, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace12, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace13, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace14, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace15, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace16, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace17, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace18, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace19, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace20, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace21, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish1, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish2, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMonthlyIncome1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome1, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome2, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAssests1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests1, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests2, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability1, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability2, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientDualEligble1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble1, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble2, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID1, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID2, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID3, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID4, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID5, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID6, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID7, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID8, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID9, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID10, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID62 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID62, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID63 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID63, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID11, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID12, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID13, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID14, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID15, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID64 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID64, GetNullableInt32, null);

                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID16, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID17, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID18, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID19, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID20, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID65 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID65, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID66 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID66, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID21, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID22 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID22, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID23 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID23, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID24 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID24, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID25 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID25, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID26 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID26, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID60 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID60, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID61 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID61, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID27 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID27, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID28 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID28, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID29 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID29, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID30 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID30, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID31 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID31, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID32 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID32, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID33 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID33, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID34 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID34, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID35 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID35, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID36 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID36, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID67 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID67, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID68 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID68, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID37 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID37, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID38 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID38, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID39 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID39, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID40 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID40, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID41 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID41, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID42 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID42, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID43 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID43, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID44 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID44, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID45 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID45, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID70 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID70, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID71 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID71, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID72 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID72, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID46 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID46, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID47 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID47, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID48 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID48, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID49 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID49, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID50 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID50, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID51 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID51, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID69 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID69, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID52 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID52, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID53 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID53, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID54 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID54, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID55 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID55, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID56 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID56, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID57 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID57, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID58 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID58, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID59 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID59, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID73 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID73, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID74 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID74, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactHoursSpent1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent6, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactCurrentStatus1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus6, GetNullableInt32, null);
                        }
                    }
                }
            }
            return ccSummaryReport;
        }

        public CCSummaryReport GetCCSummaryReportByContactsBySubStateRegionOnCountiesOfClientResidence(DateTime DOCStartDate, DateTime DOCEndDate, int SubStateRegionID)
        {
            CCSummaryReport ccSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.CCSummaryReportBySubstateRegionCountiesOfClientRes"))
            {
                database.AddInParameter(command, SP.GetCCSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.EndDate, DbType.DateTime, DOCEndDate);

                database.AddInParameter(command, SP.GetCCSummaryReport.SubStateRegionID, DbType.Int32, SubStateRegionID);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        ccSummaryReport = new CCSummaryReport
                        {
                            TotalClientContacts = reader.GetDefaultIfDBNull(T.CCSummaryReport.TotalClientContacts, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientFirstVsContinuingContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact3, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientLearnedAboutSHIP1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP1, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP2, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP3, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP4, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP5, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP6, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP7, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP8, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP9, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP10, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMethodOfContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact3, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact4, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact5, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact6, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact7, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact8, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAgeGroup1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup1, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup2, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup3, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup4, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup5, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientGender1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender1, GetNullableInt32, null);
                            ccSummaryReport.ClientGender2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender2, GetNullableInt32, null);
                            ccSummaryReport.ClientGender3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactRace1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace6, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace7, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace8, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace9, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace10, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace11, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace12, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace13, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace14, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace15, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace16, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace17, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace18, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace19, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace20, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace21, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish1, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish2, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMonthlyIncome1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome1, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome2, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAssests1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests1, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests2, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability1, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability2, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientDualEligble1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble1, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble2, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID1, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID2, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID3, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID4, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID5, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID6, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID7, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID8, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID9, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID10, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID62 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID62, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID63 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID63, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID11, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID12, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID13, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID14, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID15, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID64 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID64, GetNullableInt32, null);

                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID16, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID17, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID18, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID19, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID20, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID65 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID65, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID66 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID66, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID21, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID22 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID22, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID23 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID23, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID24 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID24, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID25 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID25, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID26 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID26, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID60 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID60, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID61 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID61, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID27 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID27, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID28 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID28, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID29 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID29, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID30 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID30, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID31 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID31, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID32 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID32, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID33 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID33, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID34 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID34, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID35 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID35, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID36 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID36, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID67 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID67, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID68 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID68, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID37 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID37, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID38 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID38, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID39 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID39, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID40 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID40, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID41 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID41, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID42 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID42, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID43 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID43, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID44 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID44, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID45 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID45, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID70 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID70, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID71 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID71, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID72 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID72, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID46 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID46, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID47 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID47, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID48 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID48, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID49 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID49, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID50 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID50, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID51 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID51, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID69 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID69, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID52 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID52, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID53 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID53, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID54 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID54, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID55 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID55, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID56 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID56, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID57 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID57, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID58 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID58, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID59 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID59, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID73 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID73, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID74 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID74, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactHoursSpent1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent6, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactCurrentStatus1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus6, GetNullableInt32, null);
                        }
                    }
                }
            }
            return ccSummaryReport;
        }

        public CCSummaryReport GetCCSummaryReportByContactsBySubStateRegionOnZipcodeOfClientResidence(DateTime DOCStartDate, DateTime DOCEndDate, int SubStateRegionID)
        {
            CCSummaryReport ccSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.CCSummaryReportBySubstateRegionZipCodesOfClientRes"))
            {
                database.AddInParameter(command, SP.GetCCSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.EndDate, DbType.DateTime, DOCEndDate);

                database.AddInParameter(command, SP.GetCCSummaryReport.SubStateRegionID, DbType.Int32, SubStateRegionID);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        ccSummaryReport = new CCSummaryReport
                        {
                            TotalClientContacts = reader.GetDefaultIfDBNull(T.CCSummaryReport.TotalClientContacts, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientFirstVsContinuingContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact3, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientLearnedAboutSHIP1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP1, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP2, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP3, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP4, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP5, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP6, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP7, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP8, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP9, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP10, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMethodOfContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact3, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact4, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact5, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact6, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact7, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact8, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAgeGroup1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup1, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup2, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup3, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup4, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup5, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientGender1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender1, GetNullableInt32, null);
                            ccSummaryReport.ClientGender2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender2, GetNullableInt32, null);
                            ccSummaryReport.ClientGender3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactRace1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace6, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace7, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace8, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace9, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace10, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace11, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace12, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace13, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace14, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace15, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace16, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace17, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace18, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace19, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace20, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace21, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish1, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish2, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMonthlyIncome1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome1, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome2, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAssests1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests1, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests2, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability1, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability2, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientDualEligble1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble1, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble2, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID1, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID2, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID3, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID4, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID5, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID6, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID7, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID8, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID9, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID10, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID62 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID62, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID63 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID63, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID11, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID12, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID13, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID14, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID15, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID64 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID64, GetNullableInt32, null);

                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID16, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID17, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID18, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID19, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID20, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID65 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID65, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID66 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID66, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID21, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID22 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID22, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID23 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID23, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID24 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID24, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID25 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID25, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID26 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID26, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID60 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID60, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID61 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID61, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID27 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID27, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID28 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID28, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID29 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID29, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID30 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID30, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID31 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID31, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID32 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID32, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID33 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID33, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID34 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID34, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID35 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID35, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID36 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID36, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID67 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID67, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID68 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID68, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID37 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID37, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID38 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID38, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID39 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID39, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID40 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID40, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID41 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID41, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID42 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID42, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID43 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID43, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID44 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID44, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID45 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID45, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID70 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID70, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID71 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID71, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID72 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID72, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID46 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID46, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID47 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID47, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID48 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID48, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID49 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID49, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID50 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID50, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID51 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID51, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID69 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID69, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID52 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID52, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID53 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID53, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID54 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID54, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID55 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID55, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID56 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID56, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID57 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID57, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID58 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID58, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID59 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID59, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID73 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID73, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID74 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID74, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactHoursSpent1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent6, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactCurrentStatus1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus6, GetNullableInt32, null);
                        }
                    }
                }
            }
            return ccSummaryReport;
        }


        public CCSummaryReport GetCCSummaryReportByNational(DateTime DOCStartDate, DateTime DOCEndDate)
        {
            CCSummaryReport ccSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.CCSummaryReportByNational"))
            {
                database.AddInParameter(command, SP.GetCCSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.EndDate, DbType.DateTime, DOCEndDate);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        ccSummaryReport = new CCSummaryReport
                        {
                            TotalClientContacts = reader.GetDefaultIfDBNull(T.CCSummaryReport.TotalClientContacts, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientFirstVsContinuingContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact3, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientLearnedAboutSHIP1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP1, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP2, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP3, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP4, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP5, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP6, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP7, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP8, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP9, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP10, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMethodOfContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact3, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact4, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact5, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact6, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact7, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact8, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAgeGroup1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup1, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup2, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup3, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup4, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup5, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientGender1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender1, GetNullableInt32, null);
                            ccSummaryReport.ClientGender2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender2, GetNullableInt32, null);
                            ccSummaryReport.ClientGender3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactRace1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace6, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace7, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace8, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace9, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace10, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace11, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace12, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace13, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace14, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace15, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace16, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace17, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace18, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace19, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace20, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace21, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish1, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish2, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMonthlyIncome1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome1, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome2, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAssests1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests1, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests2, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability1, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability2, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientDualEligble1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble1, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble2, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID1, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID2, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID3, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID4, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID5, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID6, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID7, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID8, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID9, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID10, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID62 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID62, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID63 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID63, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID11, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID12, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID13, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID14, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID15, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID64 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID64, GetNullableInt32, null);

                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID16, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID17, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID18, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID19, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID20, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID65 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID65, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID66 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID66, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID21, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID22 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID22, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID23 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID23, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID24 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID24, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID25 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID25, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID26 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID26, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID60 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID60, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID61 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID61, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID27 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID27, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID28 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID28, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID29 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID29, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID30 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID30, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID31 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID31, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID32 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID32, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID33 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID33, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID34 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID34, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID35 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID35, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID36 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID36, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID67 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID67, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID68 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID68, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID37 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID37, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID38 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID38, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID39 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID39, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID40 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID40, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID41 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID41, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID42 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID42, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID43 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID43, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID44 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID44, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID45 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID45, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID70 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID70, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID71 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID71, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID72 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID72, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID46 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID46, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID47 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID47, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID48 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID48, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID49 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID49, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID50 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID50, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID51 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID51, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID69 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID69, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID52 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID52, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID53 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID53, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID54 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID54, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID55 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID55, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID56 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID56, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID57 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID57, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID58 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID58, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID59 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID59, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID73 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID73, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID74 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID74, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactHoursSpent1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent6, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactCurrentStatus1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus6, GetNullableInt32, null);
                        }
                    }
                }
            }
            return ccSummaryReport;
        }
        public CCSummaryReport GetCCSummaryReportByCounselor(DateTime DOCStartDate, DateTime DOCEndDate,int AgencyId, int CounselorUserID)
        {
            CCSummaryReport ccSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.CCSummaryReportByCounselor"))
            {
                database.AddInParameter(command, SP.GetCCSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.EndDate, DbType.DateTime, DOCEndDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.AgencyId, DbType.Int32, AgencyId);
                database.AddInParameter(command, SP.GetCCSummaryReport.CounselorId, DbType.Int32, CounselorUserID);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        ccSummaryReport = new CCSummaryReport
                        {
                            TotalClientContacts = reader.GetDefaultIfDBNull(T.CCSummaryReport.TotalClientContacts, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientFirstVsContinuingContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact3, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientLearnedAboutSHIP1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP1, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP2, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP3, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP4, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP5, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP6, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP7, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP8, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP9, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP10, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMethodOfContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact3, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact4, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact5, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact6, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact7, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact8, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAgeGroup1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup1, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup2, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup3, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup4, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup5, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientGender1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender1, GetNullableInt32, null);
                            ccSummaryReport.ClientGender2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender2, GetNullableInt32, null);
                            ccSummaryReport.ClientGender3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactRace1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace6, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace7, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace8, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace9, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace10, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace11, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace12, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace13, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace14, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace15, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace16, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace17, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace18, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace19, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace20, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace21, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish1, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish2, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMonthlyIncome1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome1, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome2, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAssests1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests1, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests2, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability1, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability2, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientDualEligble1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble1, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble2, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID1, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID2, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID3, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID4, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID5, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID6, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID7, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID8, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID9, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID10, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID62 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID62, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID63 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID63, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID11, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID12, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID13, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID14, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID15, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID64 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID64, GetNullableInt32, null);

                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID16, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID17, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID18, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID19, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID20, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID65 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID65, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID66 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID66, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID21, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID22 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID22, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID23 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID23, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID24 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID24, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID25 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID25, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID26 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID26, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID60 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID60, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID61 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID61, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID27 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID27, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID28 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID28, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID29 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID29, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID30 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID30, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID31 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID31, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID32 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID32, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID33 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID33, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID34 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID34, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID35 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID35, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID36 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID36, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID67 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID67, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID68 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID68, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID37 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID37, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID38 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID38, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID39 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID39, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID40 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID40, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID41 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID41, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID42 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID42, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID43 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID43, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID44 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID44, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID45 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID45, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID70 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID70, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID71 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID71, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID72 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID72, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID46 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID46, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID47 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID47, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID48 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID48, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID49 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID49, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID50 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID50, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID51 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID51, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID69 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID69, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID52 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID52, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID53 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID53, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID54 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID54, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID55 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID55, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID56 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID56, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID57 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID57, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID58 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID58, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID59 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID59, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID73 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID73, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID74 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID74, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactHoursSpent1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent6, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactCurrentStatus1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus6, GetNullableInt32, null);
                        }
                    }
                }
            }
            return ccSummaryReport;
        }
        public CCSummaryReport GetCCSummaryReportBySubmitter(DateTime DOCStartDate, DateTime DOCEndDate, int AgencyID, int SubmitterUserID)
        {
            CCSummaryReport ccSummaryReport = null;

            using (var command = database.GetStoredProcCommand("dbo.CCSummaryReportBySubmitter"))
            {
                database.AddInParameter(command, SP.GetCCSummaryReport.StartDate, DbType.DateTime, DOCStartDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.EndDate, DbType.DateTime, DOCEndDate);
                database.AddInParameter(command, SP.GetCCSummaryReport.AgencyId, DbType.Int32, AgencyID);
                database.AddInParameter(command, SP.GetCCSummaryReport.SubmitterId, DbType.Int32, SubmitterUserID);
 
                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        ccSummaryReport = new CCSummaryReport
                        {
                            TotalClientContacts = reader.GetDefaultIfDBNull(T.CCSummaryReport.TotalClientContacts, GetNullableInt32, null),
                        };
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientFirstVsContinuingContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientFirstVsContinuingContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientFirstVsContinuingContact3, GetNullableInt32, null);
                        }
                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientLearnedAboutSHIP1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP1, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP2, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP3, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP4, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP5, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP6, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP7, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP8, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP9, GetNullableInt32, null);
                            ccSummaryReport.ClientLearnedAboutSHIP10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientLearnedAboutSHIP10, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMethodOfContact1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact1, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact2, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact3, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact4, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact5, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact6, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact7, GetNullableInt32, null);
                            ccSummaryReport.ClientMethodOfContact8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMethodOfContact8, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAgeGroup1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup1, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup2, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup3, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup4, GetNullableInt32, null);
                            ccSummaryReport.ClientAgeGroup5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAgeGroup5, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientGender1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender1, GetNullableInt32, null);
                            ccSummaryReport.ClientGender2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender2, GetNullableInt32, null);
                            ccSummaryReport.ClientGender3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientGender3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactRace1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace6, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace7, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace8, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace9, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace10, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace11, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace12, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace13, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace14, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace15, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace16, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace17, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace18, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace19, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace20, GetNullableInt32, null);
                            ccSummaryReport.ClientContactRace21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactRace21, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish1, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish2, GetNullableInt32, null);
                            ccSummaryReport.ClientPrimaryLanguageOtherThanEnglish3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientPrimaryLanguageOtherThanEnglish3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientMonthlyIncome1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome1, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome2, GetNullableInt32, null);
                            ccSummaryReport.ClientMonthlyIncome3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientMonthlyIncome3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientAssests1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests1, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests2, GetNullableInt32, null);
                            ccSummaryReport.ClientAssests3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientAssests3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability1, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability2, GetNullableInt32, null);
                            ccSummaryReport.ClientReceivingSSOrMedicareDisability3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientReceivingSSOrMedicareDisability3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientDualEligble1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble1, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble2, GetNullableInt32, null);
                            ccSummaryReport.ClientDualEligble3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientDualEligble3, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID1, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID2, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID3, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID4, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID5, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID6, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID7 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID7, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID8 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID8, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID9 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID9, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID10 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID10, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID62 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID62, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID63 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID63, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID11 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID11, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID12 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID12, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID13 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID13, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID14 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID14, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID15 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID15, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID64 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID64, GetNullableInt32, null);

                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID16 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID16, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID17 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID17, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID18 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID18, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID19 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID19, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID20 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID20, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID65 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID65, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID66 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID66, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID21 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID21, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID22 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID22, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID23 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID23, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID24 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID24, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID25 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID25, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID26 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID26, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID60 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID60, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID61 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID61, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID27 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID27, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID28 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID28, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID29 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID29, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID30 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID30, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID31 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID31, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID32 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID32, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID33 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID33, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID34 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID34, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID35 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID35, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID36 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID36, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID67 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID67, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID68 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID68, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID37 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID37, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID38 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID38, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID39 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID39, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID40 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID40, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID41 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID41, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID42 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID42, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID43 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID43, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID44 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID44, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID45 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID45, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID70 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID70, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID71 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID71, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID72 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID72, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID46 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID46, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID47 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID47, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID48 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID48, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID49 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID49, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID50 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID50, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID51 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID51, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID69 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID69, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientTopicID52 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID52, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID53 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID53, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID54 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID54, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID55 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID55, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID56 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID56, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID57 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID57, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID58 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID58, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID59 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID59, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID73 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID73, GetNullableInt32, null);
                            ccSummaryReport.ClientTopicID74 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientTopicID74, GetNullableInt32, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactHoursSpent1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactHoursSpent6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactHoursSpent6, GetString, null);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            ccSummaryReport.ClientContactCurrentStatus1 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus1, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus2 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus2, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus3 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus3, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus4 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus4, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus5 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus5, GetNullableInt32, null);
                            ccSummaryReport.ClientContactCurrentStatus6 = reader.GetDefaultIfDBNull(T.CCSummaryReport.ClientContactCurrentStatus6, GetNullableInt32, null);
                        }
                    }
                }
            }
            return ccSummaryReport;
        }
    }
}
