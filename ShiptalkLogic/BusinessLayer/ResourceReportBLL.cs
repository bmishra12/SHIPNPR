using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkLogic.DataLayer;
using System.Data;
namespace ShiptalkLogic.BusinessLayer
{
    public class ResourceReportBLL
    {

        public ViewResourceReportViewData GetViewResourceReport(int ResourceReportId)
        {
            ViewResourceReportViewData vwData = new ViewResourceReportViewData();
            if (ResourceReportId == 0)
            {
                vwData.ReviewerID = "0";
                vwData.SubmitterID = "0";
                vwData.NoOfStateVolunteerCounselors = "0";
                vwData.NoOfOtherVolunteerCounselors = "0";
                vwData.NoOfStateShipPaidCounselors = "0";
                vwData.NoOfOtherShipPaidCounselors = "0";
                vwData.NoOfStateInKindPaidCounselors = "0";
                vwData.NoOfOtherInKindPaidCounselors = "0";
                vwData.NoOfStateVolunteerCounselorsHrs = "0";
                vwData.NoOfOtherVolunteerCounselorsHrs = "0";
                vwData.NoOfStateShipPaidCounselorsHrs = "0";
                vwData.NoOfOtherShipPaidCounselorsHrs = "0";
                vwData.NoOfStateInKindPaidCounselorsHrs = "0";
                vwData.NoOfOtherInKindPaidCounselorsHrs = "0";
                vwData.NoOfUnpaidVolunteerCoordinators = "0";
                vwData.NoOfSHIPPaidCoordinators = "0";
                vwData.NoOfInKindPaidCoordinators = "0";
                vwData.NoOfUnpaidVolunteerCoordinatorsHrs = "0";
                vwData.NoOfSHIPPaidCoordinatorsHrs = "0";
                vwData.NoOfInKindPaidCoordinatorsHrs = "0";
                vwData.NoOfStateOtherStaff = "0";
                vwData.NoOfOtherOtherStaff = "0";
                vwData.NoOfStateShipPaidOtherStaff = "0";
                vwData.NoOfOtherShipPaidOtherStaff = "0";
                vwData.NoOfStateInKindPaidOtherStaff = "0";
                vwData.NoOfOtherInKindPaidOtherStaff = "0";
                vwData.NoOfStateVolunteerOtherStaffHrs = "0";
                vwData.NoOfStateVolunteerOtherStaff = "0";
                vwData.NoOfStateVolunteerOtherStaff = "0";
                vwData.NoOfOtherVolunteerOtherStaffHrs = "0";
                vwData.NoOfOtherVolunteerOtherStaff = "0";
                vwData.NoOfStateShipPaidOtherStaffHrs = "0";
                //vwData.NoOfSHIPPaidOtherStaffHrs = "0";
                vwData.NoOfOtherShipPaidOtherStaffHrs = "0";
                vwData.NoOfStateInKindPaidOtherStaffHrs = "0";
                vwData.NoOfOtherInKindPaidOtherStaffHrs = "0";
                vwData.NoOfInitialTrainings = "0";
                vwData.NoOfInitialTrainingsAttend = "0";
                vwData.NoOfInitTrainingsTotalHrs = "0";
                vwData.NoOfUpdateTrainings = "0";
                vwData.NoOfUpdateTrainingsAttend = "0";
                vwData.NoOfUpdateTrainingsTotalHrs = "0";
                vwData.NoOfYrsServiceLessThan1 = "0";
                vwData.NoOfYrsService1To3 = "0";
                vwData.NoOfYrsService3To5 = "0";
                vwData.NoOfYrsServiceOver5 = "0";
                vwData.NoOfYrsServiceNotCol = "0";
                vwData.NoOfDisabled = "0";
                vwData.NoOfNotDisabled = "0";
                vwData.NoOfEthnicityHispanic = "0";
                vwData.NoOfEthnicityWhite = "0";
                vwData.NoOfEthnicityAfricanAmerican = "0";
                vwData.NoOfEthnicityAmericanIndian = "0";
                vwData.NoOfEthnicityAsianIndian = "0";
                vwData.NoOfEthnicityChinese = "0";
                vwData.NoOfEthnicityFilipino = "0";
                vwData.NoOfEthnicityJapanese = "0";
                vwData.NoOfEthnicityKorean = "0";
                vwData.NoOfEthnicityVietnamese = "0";
                vwData.NoOfEthnicityNativeHawaiian = "0";
                vwData.NoOfEthnicityGuamanian = "0";
                vwData.NoOfEthnicitySamoan = "0";
                vwData.NoOfEthnicityOtherAsian = "0";
                vwData.NoOfEthnicityOthherPacificIslander = "0";
                vwData.NoOfEthnicitySomeOtherRace = "0";
                vwData.NoOfEthnicityMoreThanOneRaceEthnicity = "0";
                vwData.NoOfEthnicityNotCollected = "0";
                vwData.NoOfAgeLessThan65 = "0";
                vwData.NoOfAgeOver65 = "0";
                vwData.NoOfAgeNotCollected = "0";
                vwData.NoOfGenderFemale = "0";
                vwData.NoOfGenderMale = "0";
                vwData.NoOfGenderNotCollected = "0";
                vwData.NoOfSpeaksAnotherLanguageOtherThanEnglish = "0";
                vwData.NoOfEnglishSpeakerOnly = "0";
                vwData.NoOfSpeaksAnotherLanguageNotCollected = "0";
                vwData.ResourceReportId = "0";

            }
            else
            {
                //Initialize Object and return it.
                ResourceReportBLL ReportLogic = new ResourceReportBLL();
                DataTable ReportData = ReportLogic.GetResourceReportByReportId(ResourceReportId);
                DataRow rw = ReportData.Rows[0];
                vwData.RepYrFrom = rw["RepYrFrom"].ToString();
                vwData.RepYrTo = rw["RepYrTo"].ToString();
                vwData.ReviewerID = rw["ReviewerID"].ToString();
                vwData.SubmitterID = rw["SubmitterID"].ToString();
                vwData.StateFIPSCode = rw["StateFIPSCode"].ToString();
                vwData.NoOfStateVolunteerCounselors = rw["NoOfStateVolunteerCounselors"].ToString(); ;
                vwData.NoOfOtherVolunteerCounselors = rw["NoOfOtherVolunteerCounselors"].ToString(); ;
                vwData.NoOfStateShipPaidCounselors = rw["NoOfStateShipPaidCounselors"].ToString(); ;
                vwData.NoOfOtherShipPaidCounselors = rw["NoOfOtherShipPaidCounselors"].ToString(); ;
                vwData.NoOfStateInKindPaidCounselors = rw["NoOfStateInKindPaidCounselors"].ToString(); ;
                vwData.NoOfOtherInKindPaidCounselors = rw["NoOfOtherInKindPaidCounselors"].ToString(); ;
                vwData.NoOfStateVolunteerCounselorsHrs = rw["NoOfStateVolunteerCounselorsHrs"].ToString(); ;
                vwData.NoOfOtherVolunteerCounselorsHrs = rw["NoOfOtherVolunteerCounselorsHrs"].ToString(); ;
                vwData.NoOfStateShipPaidCounselorsHrs = rw["NoOfStateShipPaidCounselorsHrs"].ToString(); ;
                vwData.NoOfOtherShipPaidCounselorsHrs = rw["NoOfOtherShipPaidCounselorsHrs"].ToString(); ;
                vwData.NoOfStateInKindPaidCounselorsHrs = rw["NoOfStateInKindPaidCounselorsHrs"].ToString(); ;
                vwData.NoOfOtherInKindPaidCounselorsHrs = rw["NoOfOtherInKindPaidCounselorsHrs"].ToString(); ;
                vwData.NoOfUnpaidVolunteerCoordinators = rw["NoOfUnpaidVolunteerCoordinators"].ToString(); ;
                vwData.NoOfSHIPPaidCoordinators = rw["NoOfSHIPPaidCoordinators"].ToString(); ;
                vwData.NoOfInKindPaidCoordinators = rw["NoOfInKindPaidCoordinators"].ToString(); ;
                vwData.NoOfUnpaidVolunteerCoordinatorsHrs = rw["NoOfUnpaidVolunteerCoordinatorsHrs"].ToString(); ;
                vwData.NoOfSHIPPaidCoordinatorsHrs = rw["NoOfSHIPPaidCoordinatorsHrs"].ToString(); ;
                vwData.NoOfInKindPaidCoordinatorsHrs = rw["NoOfInKindPaidCoordinatorsHrs"].ToString(); ;
                vwData.NoOfStateShipPaidOtherStaff = rw["NoOfStateShipPaidOtherStaff"].ToString(); ;
                vwData.NoOfOtherShipPaidOtherStaff = rw["NoOfOtherShipPaidOtherStaff"].ToString(); ;
                vwData.NoOfStateInKindPaidOtherStaff = rw["NoOfStateInKindPaidOtherStaff"].ToString(); ;
                vwData.NoOfOtherInKindPaidOtherStaff = rw["NoOfOtherInKindPaidOtherStaff"].ToString(); ;

                vwData.NoOfStateVolunteerOtherStaffHrs = rw["NoOfStateVolunteerOtherStaffHrs"].ToString(); ;
                vwData.NoOfStateVolunteerOtherStaff = rw["NoOfStateVolunteerOtherStaff"].ToString(); ;
                       
                vwData.NoOfStateVolunteerOtherStaff = rw["NoOfStateVolunteerOtherStaff"].ToString(); ;
                vwData.NoOfOtherVolunteerOtherStaffHrs = rw["NoOfOtherVolunteerOtherStaffHrs"].ToString(); ;
                vwData.NoOfOtherVolunteerOtherStaff = rw["NoOfOtherVolunteerOtherStaff"].ToString(); ;
                vwData.NoOfStateShipPaidOtherStaffHrs = rw["NoOfStateShipPaidOtherStaffHrs"].ToString(); ;
                vwData.NoOfOtherShipPaidOtherStaffHrs = rw["NoOfOtherShipPaidOtherStaffHrs"].ToString(); ;
                vwData.NoOfStateInKindPaidOtherStaffHrs = rw["NoOfStateInKindPaidOtherStaffHrs"].ToString(); ;
                vwData.NoOfOtherInKindPaidOtherStaffHrs = rw["NoOfOtherInKindPaidOtherStaffHrs"].ToString(); ;
                vwData.NoOfInitialTrainings = rw["NoOfInitialTrainings"].ToString(); ;
                vwData.NoOfInitialTrainingsAttend = rw["NoOfInitialTrainingsAttend"].ToString(); ;
                vwData.NoOfInitTrainingsTotalHrs = rw["NoOfInitTrainingsTotalHrs"].ToString(); ;
                vwData.NoOfUpdateTrainings = rw["NoOfUpdateTrainings"].ToString(); ;
                vwData.NoOfUpdateTrainingsAttend = rw["NoOfUpdateTrainingsAttend"].ToString(); ;
                vwData.NoOfUpdateTrainingsTotalHrs = rw["NoOfUpdateTrainingsTotalHrs"].ToString(); ;
                vwData.NoOfYrsServiceLessThan1 = rw["NoOfYrsServiceLessThan1"].ToString(); ;
                vwData.NoOfYrsService1To3 = rw["NoOfYrsService1To3"].ToString(); ;
                vwData.NoOfYrsService3To5 = rw["NoOfYrsService3To5"].ToString(); ;
                vwData.NoOfYrsServiceOver5 = rw["NoOfYrsServiceOver5"].ToString(); ;
                vwData.NoOfYrsServiceNotCol = rw["NoOfYrsServiceNotCol"].ToString(); ;
                vwData.NoOfDisabled = rw["NoOfDisabled"].ToString(); ;
                vwData.NoOfNotDisabled = rw["NoOfNotDisabled"].ToString();
                vwData.NoOfDisabledNotCollected = rw["NoOfDisabledNotCollected"].ToString();
                vwData.NoOfEthnicityHispanic = rw["NoOfEthnicityHispanic"].ToString(); ;
                vwData.NoOfEthnicityWhite = rw["NoOfEthnicityWhite"].ToString(); ;
                vwData.NoOfEthnicityAfricanAmerican = rw["NoOfEthnicityAfricanAmerican"].ToString(); ;
                vwData.NoOfEthnicityAmericanIndian = rw["NoOfEthnicityAmericanIndian"].ToString(); ;
                vwData.NoOfEthnicityAsianIndian = rw["NoOfEthnicityAsianIndian"].ToString(); ;
                vwData.NoOfEthnicityChinese = rw["NoOfEthnicityChinese"].ToString(); ;
                vwData.NoOfEthnicityFilipino = rw["NoOfEthnicityFilipino"].ToString(); ;
                vwData.NoOfEthnicityJapanese = rw["NoOfEthnicityJapanese"].ToString(); ;
                vwData.NoOfEthnicityKorean = rw["NoOfEthnicityKorean"].ToString(); ;
                vwData.NoOfEthnicityVietnamese = rw["NoOfEthnicityVietnamese"].ToString(); ;
                vwData.NoOfEthnicityNativeHawaiian = rw["NoOfEthnicityNativeHawaiian"].ToString(); ;
                vwData.NoOfEthnicityGuamanian = rw["NoOfEthnicityGuamanian"].ToString(); ;
                vwData.NoOfEthnicitySamoan = rw["NoOfEthnicitySamoan"].ToString(); ;
                vwData.NoOfEthnicityOtherAsian = rw["NoOfEthnicityOtherAsian"].ToString(); ;
                vwData.NoOfEthnicityOthherPacificIslander = rw["NoOfEthnicityOthherPacificIslander"].ToString(); ;
                vwData.NoOfEthnicitySomeOtherRace = rw["NoOfEthnicitySomeOtherRace"].ToString(); ;
                vwData.NoOfEthnicityMoreThanOneRaceEthnicity = rw["NoOfEthnicityMoreThanOneRaceEthnicity"].ToString(); ;
                vwData.NoOfEthnicityNotCollected = rw["NoOfEthnicityNotCollected"].ToString(); ;
                vwData.NoOfAgeLessThan65 = rw["NoOfAgeLessThan65"].ToString(); ;
                vwData.NoOfAgeOver65 = rw["NoOfAgeOver65"].ToString(); ;
                vwData.NoOfAgeNotCollected = rw["NoOfAgeNotCollected"].ToString(); ;
                vwData.NoOfGenderFemale = rw["NoOfGenderFemale"].ToString(); ;
                vwData.NoOfGenderMale = rw["NoOfGenderMale"].ToString(); ;
                vwData.NoOfGenderNotCollected = rw["NoOfGenderNotCollected"].ToString(); ;
                vwData.NoOfSpeaksAnotherLanguageOtherThanEnglish = rw["NoOfSpeaksAnotherLanguageOtherThanEnglish"].ToString(); ;
                vwData.NoOfEnglishSpeakerOnly = rw["NoOfEnglishSpeakerOnly"].ToString(); ;
                vwData.NoOfSpeaksAnotherLanguageNotCollected = rw["NoOfSpeaksAnotherLanguageNotCollected"].ToString(); ;
                vwData.ResourceReportId = rw["ResourceReportId"].ToString(); ;
                vwData.Title = rw["Title"].ToString();
                vwData.StateGranteeName = rw["StateGranteeName"].ToString();
            }
            return vwData;
        }


        
        /// <summary>
        /// Adds a new resource report 
        /// </summary>
        /// <param name="RptData"></param>
        /// <returns>ResourceReportId</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public int AddReport(ViewResourceReportViewData RptData)
        {

            // Ensure the fields that are required contain values
            if(RptData.SubmitterID == "-1")
            {
                throw( new ArgumentNullException("SubmitterId"));
            }

            if((string.IsNullOrEmpty(RptData.StateFIPSCode)))
            {
                throw (new ArgumentNullException("StateFIPSCode"));
            }
            return ReportsDAL.AddResourceReport(RptData);
        }

        public int UpdateReport(ViewResourceReportViewData RptData)
        {
            // Ensure the fields that are required contain values
            if (RptData.SubmitterID == "-1")
            {
                throw (new ArgumentNullException("SubmitterId"));
            }

            if ((string.IsNullOrEmpty(RptData.StateFIPSCode)))
            {
                throw (new ArgumentNullException("StateFIPSCode"));
            }

            int ReportId = int.Parse(RptData.ResourceReportId);
            return ReportsDAL.UpdateResourceReport(RptData);

        }

        public bool ReportExists(string Year, string StateFipsCode)
        {
           DataTable dtFoundReport =  GetResourceReportByStateFipCode(StateFipsCode, int.Parse(Year));
           return (dtFoundReport.Rows.Count > 0);
        }


        /// <summary>
        /// Returns all Resource Reports associated witha specific StateFIPS
        /// code.
        /// </summary>
        /// <param name="StateFipsCode"></param>
        /// <returns></returns>
        public DataTable GetResourceReportByStateFipCode(string StateFipsCode, int FilterYear)
        {
            return ReportsDAL.GetResourceReportByStateFipCode(StateFipsCode, FilterYear);
        }

        public DataTable GetResourceReportByReportId(int ReportId)
        {
            return ReportsDAL.GetResourceReportByReportId(ReportId);
        }

        /// <summary>
        /// Sets the active flag to false on a resource report.
        /// </summary>
        /// <param name="ReportId"></param>
        public void DeActivateReport(int ReportId)
        {
            ReportsDAL.SetResourceReportActiveFlag(ReportId, Convert.ToInt32(false));
        }

        /// <summary>
        /// Sets the active flag to true on a resource report.
        /// </summary>
        /// <param name="ReportId"></param>
        public void ActivateReport(int ReportId)
        {
            ReportsDAL.SetResourceReportActiveFlag(ReportId, Convert.ToInt32(true));
        }




        static public bool IsDateValid(DateTime FromDate, DateTime ToDate)
        {
            return (FromDate < ToDate);
        }

    }
}
