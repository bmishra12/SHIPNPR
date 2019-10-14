using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkLogic.DataLayer;
using AutoMapper;

namespace ShiptalkLogic.BusinessLayer
{
    public class PAMSummaryReportBLL
    {
        public PAMSummaryReportBLL()
        {
            CreateMaps();
        }
        private void CreateMaps()
        {
            Mapper.CreateMap<PAMSummaryReport, ViewPAMSummaryReportViewData>();
        }
        public ViewPAMSummaryReportViewData GetPAMSummaryReportByState(DateTime DOCStartDate, DateTime DOCEndDate, string StateFIPSCode)
        {
            var pamSummaryReport = new PAMSummaryReportDAL().GetPAMSummaryReportByState(DOCStartDate, DOCEndDate, StateFIPSCode);

            return Mapper.Map<PAMSummaryReport,ViewPAMSummaryReportViewData>(pamSummaryReport);

        }
        public ViewPAMSummaryReportViewData GetPAMSummaryReportByAgency(DateTime DOCStartDate, DateTime DOCEndDate, string AgencyId)
        {
            var pamSummaryReport = new PAMSummaryReportDAL().GetPAMSummaryReportByAgency(DOCStartDate, DOCEndDate, AgencyId);

            return Mapper.Map<PAMSummaryReport, ViewPAMSummaryReportViewData>(pamSummaryReport);

        }
        public ViewPAMSummaryReportViewData GetPAMSummaryReportByCountyOfActivityEvent(DateTime DOCStartDate, DateTime DOCEndDate, string CountyOfActivityEvent, int ScopeId, int UserId, int AgencyId, string StateFIPSCode)
        {
            var pamSummaryReport = new PAMSummaryReportDAL().GetPAMSummaryReportByCountyOfActivityEvent(DOCStartDate, DOCEndDate, CountyOfActivityEvent, ScopeId, UserId, AgencyId,  StateFIPSCode);

            return Mapper.Map<PAMSummaryReport, ViewPAMSummaryReportViewData>(pamSummaryReport);

        }
        public ViewPAMSummaryReportViewData GetPAMSummaryReportByZipCodeOfActivityEvent(DateTime DOCStartDate, DateTime DOCEndDate, string ZipCodeOfActivityEvent, int ScopeId, int UserId, int AgencyId)
        {
            var pamSummaryReport = new PAMSummaryReportDAL().GetPAMSummaryReportByZipCodeOfActivityEvent(DOCStartDate, DOCEndDate, ZipCodeOfActivityEvent, ScopeId, UserId, AgencyId);

            return Mapper.Map<PAMSummaryReport, ViewPAMSummaryReportViewData>(pamSummaryReport);
        }
        public ViewPAMSummaryReportViewData GetPAMSummaryReportBySubStateRegionOnAgency(DateTime DOCStartDate, DateTime DOCEndDate, int RegionId)
        {
            var PAMSummaryReport = new PAMSummaryReportDAL().GetPAMSummaryReportBySubStateRegionOnAgency(DOCStartDate, DOCEndDate, RegionId);

            return Mapper.Map<PAMSummaryReport, ViewPAMSummaryReportViewData>(PAMSummaryReport);
        }
        public ViewPAMSummaryReportViewData GetPAMSummaryReportBySubStateRegionCountiesEvent(DateTime DOCStartDate, DateTime DOCEndDate, int RegionId)
        {
            var PAMSummaryReport = new PAMSummaryReportDAL().GetPAMSummaryReportBySubStateRegionCountiesEvent(DOCStartDate, DOCEndDate, RegionId);

            return Mapper.Map<PAMSummaryReport, ViewPAMSummaryReportViewData>(PAMSummaryReport);
        }
        public ViewPAMSummaryReportViewData GetPAMSummaryReportByNational(DateTime DOCStartDate, DateTime DOCEndDate)
        {
            var PAMSummaryReport = new PAMSummaryReportDAL().GetPAMSummaryReportByNational(DOCStartDate, DOCEndDate);

            return Mapper.Map<PAMSummaryReport, ViewPAMSummaryReportViewData>(PAMSummaryReport);

        }
        public ViewPAMSummaryReportViewData GetPAMSummaryReportByPresenterContributor(DateTime DOCStartDate, DateTime DOCEndDate, int AgecyID, int PresenterContributorUserId)
        {
            var PAMSummaryReport = new PAMSummaryReportDAL().GetPAMSummaryReportByPresenterContributor(DOCStartDate, DOCEndDate, AgecyID, PresenterContributorUserId);

            return Mapper.Map<PAMSummaryReport, ViewPAMSummaryReportViewData>(PAMSummaryReport);

        }
        public ViewPAMSummaryReportViewData GetPAMSummaryReportBySubmitter(DateTime DOCStartDate, DateTime DOCEndDate, int AgecyID, int submitterUserID)
        {
            var PAMSummaryReport = new PAMSummaryReportDAL().GetPAMSummaryReportBySubmitter(DOCStartDate, DOCEndDate, AgecyID, submitterUserID);

            return Mapper.Map<PAMSummaryReport, ViewPAMSummaryReportViewData>(PAMSummaryReport);

        }
    }
}
