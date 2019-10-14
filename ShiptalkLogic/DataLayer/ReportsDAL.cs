using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using System.Data;
using System.Data.Common;

using ShiptalkLogic.BusinessObjects.UI;

namespace ShiptalkLogic.DataLayer
{
    internal static class ReportsDAL
    {
        #region Add/Update Operations
        /// <summary>
        /// Updates an existing Resource Report
        /// </summary>
        /// <param name="RptData">ResourceReport</param>
        /// <param name="outResourceReportId"></param>
        /// <returns>outResourceReportId or -1 if update failed.</returns>
        public static int UpdateResourceReport(ViewResourceReportViewData RptData)
        {
            try
            {
                int ReportId = int.Parse(RptData.ResourceReportId);
                SaveResourceReport(RptData, out ReportId);
                return ReportId;
            }
            catch(System.Exception exFail)
            {
                //TODO: Log error and report error
                return -1;
            }
        }

        /// <summary>
        /// Adds a new Resource Report record 
        /// </summary>
        /// <param name="RptData">ResourceReport</param>
        /// <returns>ResourceReportId</returns>
        public static int AddResourceReport(ViewResourceReportViewData RptData)
        {
            try
            {
                int ResourceReportId = 0;
                SaveResourceReport(RptData, out ResourceReportId);
                return ResourceReportId;
            }
            catch (System.Exception exFail)
            {
                //TODO: Log error and report error
                return -1;
            }
            
        }

        public static DataTable GetResourceReportByStateFipCode(string StateFipsCode, int FilterSelectedYear)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Reports.GetResourceReportByStateFipCode.Description()))
            {
                dbCmd.Connection = db.CreateConnection();
                dbCmd.Connection.Open();
                db.AddInParameter(dbCmd, "@StateFipsCodes ", DbType.String, StateFipsCode);
                db.AddInParameter(dbCmd, "@FilterSelectedYear", DbType.Int32, FilterSelectedYear);
                DataSet dsRecords = db.ExecuteDataSet(dbCmd);

                return dsRecords.Tables[0];
            }
            

        }

        public static DataTable GetResourceReportByReportId(int ReportId)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Reports.GetResourceReportByReportId.Description()))
            {
                dbCmd.Connection = db.CreateConnection();
                dbCmd.Connection.Open();
                db.AddInParameter(dbCmd, "@ReportId", DbType.Int32, ReportId);
                DataSet dsRecords = db.ExecuteDataSet(dbCmd);
                return dsRecords.Tables[0];
            }

        }

        public static void SetResourceReportActiveFlag(int ReportId, int Active)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Reports.SetActivateResourceReport.Description()))
            {
                dbCmd.Connection = db.CreateConnection();
                dbCmd.Connection.Open();
                db.AddInParameter(dbCmd, "@ReportId", DbType.Int32, ReportId);
                db.AddInParameter(dbCmd, "@Activation", DbType.Boolean, Convert.ToBoolean(Active));
                db.ExecuteNonQuery(dbCmd);
            }

        }

        private static bool SaveResourceReport(ViewResourceReportViewData RptData, out int outResourceReportId)
        {
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            outResourceReportId = -1;
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Reports.SaveResourceReport.Description()))
            {
                dbCmd.Connection = db.CreateConnection();
                dbCmd.Connection.Open();
                db.AddInParameter(dbCmd, "@SubmitterID", DbType.Int32, int.Parse(RptData.SubmitterID));
	            db.AddInParameter(dbCmd, "@ReviewerID", DbType.Int32, int.Parse(RptData.ReviewerID));
	            db.AddInParameter(dbCmd, "@RepYrFrom", DbType.Date, Convert.ToDateTime(RptData.RepYrFrom));
	            db.AddInParameter(dbCmd, "@RepYrTo", DbType.Date, Convert.ToDateTime(RptData.RepYrTo));
	            db.AddInParameter(dbCmd, "@StateFIPSCode", DbType.String, RptData.StateFIPSCode);
	            db.AddInParameter(dbCmd, "@StateGranteeName", DbType.String, RptData.StateGranteeName);
	            db.AddInParameter(dbCmd, "@PersonCompletingReportName", DbType.String, RptData.PersonCompletingReportName);
	            db.AddInParameter(dbCmd, "@PersonCompletingReportTitle", DbType.String, RptData.PersonCompletingReportTitle);
                db.AddInParameter(dbCmd, "@PersonCompletingReportTel", DbType.String, RptData.PersonCompletingReportTel);
                db.AddInParameter(dbCmd, "@NoOfStateVolunteerCounselors", DbType.Int32, int.Parse(RptData.NoOfStateVolunteerCounselors));

                db.AddInParameter(dbCmd, "@NoOfOtherVolunteerCounselors", DbType.Int32, int.Parse(RptData.NoOfOtherVolunteerCounselors));
                db.AddInParameter(dbCmd, "@NoOfStateShipPaidCounselors", DbType.Int32, int.Parse(RptData.@NoOfStateShipPaidCounselors));
                db.AddInParameter(dbCmd, "@NoOfOtherShipPaidCounselors", DbType.Int32, int.Parse(RptData.NoOfOtherShipPaidCounselors));
                db.AddInParameter(dbCmd, "@NoOfStateInKindPaidCounselors", DbType.Int32, int.Parse(RptData.@NoOfStateInKindPaidCounselors));
                db.AddInParameter(dbCmd, "@NoOfOtherInKindPaidCounselors", DbType.Int32, int.Parse(RptData.NoOfOtherInKindPaidCounselors));
                db.AddInParameter(dbCmd, "@NoOfStateVolunteerCounselorsHrs", DbType.Int32, int.Parse(RptData.NoOfStateVolunteerCounselorsHrs));
                db.AddInParameter(dbCmd, "@NoOfOtherVolunteerCounselorsHrs", DbType.Int32, int.Parse(RptData.NoOfOtherVolunteerCounselorsHrs));
                db.AddInParameter(dbCmd, "@NoOfStateShipPaidCounselorsHrs", DbType.Int32, int.Parse(RptData.NoOfStateShipPaidCounselorsHrs));
                db.AddInParameter(dbCmd, "@NoOfOtherShipPaidCounselorsHrs", DbType.Int32, int.Parse(RptData.NoOfOtherShipPaidCounselorsHrs));
                db.AddInParameter(dbCmd, "@NoOfStateInKindPaidCounselorsHrs", DbType.Int32, int.Parse(RptData.NoOfStateInKindPaidCounselorsHrs));
                db.AddInParameter(dbCmd, "@NoOfOtherInKindPaidCounselorsHrs", DbType.Int32, int.Parse(RptData.NoOfOtherInKindPaidCounselorsHrs));

                db.AddInParameter(dbCmd, "@NoOfUnpaidVolunteerCoordinators", DbType.Int32, int.Parse(RptData.NoOfUnpaidVolunteerCoordinators));
                db.AddInParameter(dbCmd, "@NoOfSHIPPaidCoordinators", DbType.Int32, int.Parse(RptData.NoOfSHIPPaidCoordinators));
                db.AddInParameter(dbCmd, "@NoOfInKindPaidCoordinators", DbType.Int32, int.Parse(RptData.NoOfInKindPaidCoordinators));
                db.AddInParameter(dbCmd, "@NoOfUnpaidVolunteerCoordinatorsHrs", DbType.Int32, int.Parse(RptData.NoOfUnpaidVolunteerCoordinatorsHrs));
                db.AddInParameter(dbCmd, "@NoOfSHIPPaidCoordinatorsHrs", DbType.Int32, int.Parse(RptData.@NoOfSHIPPaidCoordinatorsHrs));
                db.AddInParameter(dbCmd, "@NoOfInKindPaidCoordinatorsHrs", DbType.Int32, int.Parse(RptData.NoOfInKindPaidCoordinatorsHrs));
                db.AddInParameter(dbCmd, "@NoOfStateShipPaidOtherStaff", DbType.Int32, int.Parse(RptData.NoOfStateShipPaidOtherStaff));
                db.AddInParameter(dbCmd, "@NoOfOtherShipPaidOtherStaff", DbType.Int32, int.Parse(RptData.NoOfOtherShipPaidOtherStaff));
                db.AddInParameter(dbCmd, "@NoOfStateInKindPaidOtherStaff", DbType.Int32, int.Parse(RptData.NoOfStateInKindPaidOtherStaff));

                db.AddInParameter(dbCmd, "@NoOfOtherInKindPaidOtherStaff", DbType.Int32, int.Parse(RptData.NoOfOtherInKindPaidOtherStaff));
                db.AddInParameter(dbCmd, "@NoOfStateVolunteerOtherStaffHrs", DbType.Int32, int.Parse(RptData.NoOfStateVolunteerOtherStaffHrs));
                db.AddInParameter(dbCmd, "@NoOfOtherVolunteerOtherStaffHrs", DbType.Int32, int.Parse(RptData.NoOfOtherVolunteerOtherStaffHrs));
                db.AddInParameter(dbCmd, "@NoOfStateShipPaidOtherStaffHrs", DbType.Int32, int.Parse(RptData.NoOfStateShipPaidOtherStaffHrs));
                db.AddInParameter(dbCmd, "@NoOfOtherShipPaidOtherStaffHrs", DbType.Int32, int.Parse(RptData.NoOfOtherShipPaidOtherStaffHrs));
                db.AddInParameter(dbCmd, "@NoOfStateInKindPaidOtherStaffHrs", DbType.Int32, int.Parse(RptData.NoOfStateInKindPaidOtherStaffHrs));
                db.AddInParameter(dbCmd, "@NoOfOtherInKindPaidOtherStaffHrs", DbType.Int32, int.Parse(RptData.NoOfOtherInKindPaidOtherStaffHrs));
                db.AddInParameter(dbCmd, "@NoOfInitialTrainings", DbType.Int32, int.Parse(RptData.NoOfInitialTrainings));
                db.AddInParameter(dbCmd, "@NoOfInitialTrainingsAttend", DbType.Int32, int.Parse(RptData.NoOfInitialTrainingsAttend));
                db.AddInParameter(dbCmd, "@NoOfInitTrainingsTotalHrs", DbType.Int32, int.Parse(RptData.NoOfInitTrainingsTotalHrs));
                db.AddInParameter(dbCmd, "@NoOfUpdateTrainings", DbType.Int32, int.Parse(RptData.NoOfUpdateTrainings));

                db.AddInParameter(dbCmd, "@NoOfUpdateTrainingsAttend", DbType.Int32, int.Parse(RptData.NoOfUpdateTrainingsAttend));
                db.AddInParameter(dbCmd, "@NoOfUpdateTrainingsTotalHrs", DbType.Int32, int.Parse(RptData.NoOfUpdateTrainingsTotalHrs));
                db.AddInParameter(dbCmd, "@NoOfYrsServiceLessThan1", DbType.Int32, int.Parse(RptData.NoOfYrsServiceLessThan1));
                db.AddInParameter(dbCmd, "@NoOfYrsService1To3", DbType.Int32, int.Parse(RptData.NoOfYrsService1To3));
                db.AddInParameter(dbCmd, "@NoOfYrsService3To5", DbType.Int32, int.Parse(RptData.NoOfYrsService3To5));
                db.AddInParameter(dbCmd, "@NoOfYrsServiceOver5", DbType.Int32, int.Parse(RptData.NoOfYrsServiceOver5));
                db.AddInParameter(dbCmd, "@NoOfYrsServiceNotCol", DbType.Int32, int.Parse(RptData.NoOfYrsServiceNotCol));
                db.AddInParameter(dbCmd, "@NoOfDisabled", DbType.Int32, int.Parse(RptData.NoOfDisabled));
                db.AddInParameter(dbCmd, "@NoOfNotDisabled", DbType.Int32, int.Parse(RptData.NoOfNotDisabled));
                db.AddInParameter(dbCmd, "@NoOfDisabledNotCollected", DbType.Int32, int.Parse(RptData.NoOfDisabledNotCollected));
                db.AddInParameter(dbCmd, "@NoOfEthnicityHispanic", DbType.Int32, int.Parse(RptData.NoOfEthnicityHispanic));
                db.AddInParameter(dbCmd, "@NoOfEthnicityWhite", DbType.Int32, int.Parse(RptData.NoOfEthnicityWhite));

                db.AddInParameter(dbCmd, "@NoOfEthnicityAfricanAmerican", DbType.Int32, int.Parse(RptData.NoOfEthnicityAfricanAmerican));
                db.AddInParameter(dbCmd, "@NoOfEthnicityAmericanIndian", DbType.Int32, int.Parse(RptData.NoOfEthnicityAmericanIndian));
                db.AddInParameter(dbCmd, "@NoOfEthnicityAsianIndian", DbType.Int32, int.Parse(RptData.NoOfEthnicityAsianIndian));
                db.AddInParameter(dbCmd, "@NoOfEthnicityChinese", DbType.Int32, int.Parse(RptData.NoOfEthnicityChinese));
                db.AddInParameter(dbCmd, "@NoOfEthnicityFilipino", DbType.Int32, int.Parse(RptData.NoOfEthnicityFilipino));
                db.AddInParameter(dbCmd, "@NoOfEthnicityJapanese", DbType.Int32, int.Parse(RptData.NoOfEthnicityJapanese));
                db.AddInParameter(dbCmd, "@NoOfEthnicityKorean", DbType.Int32, int.Parse(RptData.NoOfEthnicityKorean));
                db.AddInParameter(dbCmd, "@NoOfEthnicityVietnamese", DbType.Int32, int.Parse(RptData.NoOfEthnicityVietnamese));
                db.AddInParameter(dbCmd, "@NoOfEthnicityNativeHawaiian", DbType.Int32, int.Parse(RptData.NoOfEthnicityNativeHawaiian));
                db.AddInParameter(dbCmd, "@NoOfEthnicityGuamanian", DbType.Int32, int.Parse(RptData.NoOfEthnicityGuamanian));
                db.AddInParameter(dbCmd, "@NoOfEthnicitySamoan", DbType.Int32, int.Parse(RptData.NoOfEthnicitySamoan));

                db.AddInParameter(dbCmd, "@NoOfEthnicityOtherAsian", DbType.Int32, int.Parse(RptData.NoOfEthnicityOtherAsian));
                db.AddInParameter(dbCmd, "@NoOfEthnicityOthherPacificIslander", DbType.Int32, int.Parse(RptData.NoOfEthnicityOthherPacificIslander));
                db.AddInParameter(dbCmd, "@NoOfEthnicitySomeOtherRace", DbType.Int32, int.Parse(RptData.NoOfEthnicitySomeOtherRace));
                db.AddInParameter(dbCmd, "@NoOfEthnicityMoreThanOneRaceEthnicity", DbType.Int32, int.Parse(RptData.NoOfEthnicityMoreThanOneRaceEthnicity));
                db.AddInParameter(dbCmd, "@NoOfEthnicityNotCollected", DbType.Int32, int.Parse(RptData.NoOfEthnicityNotCollected));
                db.AddInParameter(dbCmd, "@NoOfAgeLessThan65", DbType.Int32, int.Parse(RptData.NoOfAgeLessThan65));
                db.AddInParameter(dbCmd, "@NoOfAgeOver65", DbType.Int32, int.Parse(RptData.NoOfAgeOver65));
                db.AddInParameter(dbCmd, "@NoOfAgeNotCollected", DbType.Int32, int.Parse(RptData.NoOfAgeNotCollected));
                db.AddInParameter(dbCmd, "@NoOfGenderFemale", DbType.Int32, int.Parse(RptData.NoOfGenderFemale));
                db.AddInParameter(dbCmd, "@NoOfGenderMale", DbType.Int32, int.Parse(RptData.NoOfGenderMale));
                db.AddInParameter(dbCmd, "@NoOfGenderNotCollected", DbType.Int32, int.Parse(RptData.NoOfGenderNotCollected));

                db.AddInParameter(dbCmd, "@NoOfSpeaksAnotherLanguageOtherThanEnglish", DbType.Int32, int.Parse(RptData.NoOfSpeaksAnotherLanguageOtherThanEnglish));
                db.AddInParameter(dbCmd, "@NoOfEnglishSpeakerOnly", DbType.Int32, int.Parse(RptData.NoOfEnglishSpeakerOnly));
                db.AddInParameter(dbCmd, "@NoOfSpeaksAnotherLanguageNotCollected", DbType.Int32, int.Parse(RptData.NoOfSpeaksAnotherLanguageNotCollected));
                db.AddInParameter(dbCmd, "@LastUpdatedBy", DbType.Int32, RptData.LastUpdatedBy.Value);

                db.AddInParameter(dbCmd, "@ResourceReportId", DbType.Int32, int.Parse(RptData.ResourceReportId));
                db.AddInParameter(dbCmd, "@NoOfStateVolunteerOtherStaff", DbType.Int32, int.Parse(RptData.NoOfStateVolunteerOtherStaff));
                db.AddInParameter(dbCmd, "@NoOfOtherVolunteerOtherStaff", DbType.Int32, int.Parse(RptData.NoOfOtherVolunteerOtherStaff));
                db.AddInParameter(dbCmd, "@Title", DbType.String, RptData.Title);
                db.AddOutParameter(dbCmd, "@NewReportId", DbType.Int32, 4);
                dbCmd.ExecuteNonQuery();

                if (dbCmd.Parameters["@ResourceReportId"].Value != null)
                {
                    outResourceReportId = int.Parse(db.GetParameterValue(dbCmd, "@NewReportId").ToString());
                    result = true;
                }
            }
            return result;

        }
        #endregion

    }
}
