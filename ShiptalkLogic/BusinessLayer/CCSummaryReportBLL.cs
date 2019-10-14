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
    public class CCSummaryReportBLL
    {
        public CCSummaryReportBLL()
        {
            CreateMaps();
        }
        private void CreateMaps()
        {
            Mapper.CreateMap<CCSummaryReport, ViewCCSummaryReportViewData>();
        }
        public ViewCCSummaryReportViewData GetCCSummaryReportByState(DateTime DOCStartDate, DateTime DOCEndDate, string StateFIPSCode)
        {
            var CCSummaryReport = new CCSummaryReportDAL().GetCCSummaryReportByState(DOCStartDate, DOCEndDate, StateFIPSCode);

            return Mapper.Map<CCSummaryReport,ViewCCSummaryReportViewData>(CCSummaryReport);

        }
        public ViewCCSummaryReportViewData GetCCSummaryReportByAgency(DateTime DOCStartDate, DateTime DOCEndDate, string AgencyId)
        {
            var CCSummaryReport = new CCSummaryReportDAL().GetCCSummaryReportByAgency(DOCStartDate, DOCEndDate, AgencyId);

            return Mapper.Map<CCSummaryReport, ViewCCSummaryReportViewData>(CCSummaryReport);

        }
        public ViewCCSummaryReportViewData GetCCSummaryReportByCountyOfCounselorLocation(DateTime DOCStartDate, DateTime DOCEndDate, string CountyCounselorLocation, int ScopeId, int UserId, int AgencyId)
        {
            var CCSummaryReport = new CCSummaryReportDAL().GetCCSummaryReportByCountyOfCounselorLocation(DOCStartDate, DOCEndDate, CountyCounselorLocation, ScopeId, UserId, AgencyId);

            return Mapper.Map<CCSummaryReport, ViewCCSummaryReportViewData>(CCSummaryReport);

        }
        public ViewCCSummaryReportViewData GetCCSummaryReportByZipCodeOfCounselorLocation(DateTime DOCStartDate, DateTime DOCEndDate, string ZipCodeCounselorLocation, int ScopeId, int UserId, int AgencyId)
        {
            var CCSummaryReport = new CCSummaryReportDAL().GetCCSummaryReportByZipCodeOfCounselorLocation(DOCStartDate, DOCEndDate, ZipCodeCounselorLocation, ScopeId, UserId, AgencyId);

            return Mapper.Map<CCSummaryReport, ViewCCSummaryReportViewData>(CCSummaryReport);

        }
        public ViewCCSummaryReportViewData GetCCSummaryReportByCountyOfClientResidence(DateTime DOCStartDate, DateTime DOCEndDate, string CountyFipsCodeofClientResidence, int ScopeId, int UserId, int AgencyId, string StateFIPSCode)
        {
            var CCSummaryReport = new CCSummaryReportDAL().GetCCSummaryReportByCountyOfClientResidence(DOCStartDate, DOCEndDate, CountyFipsCodeofClientResidence, ScopeId, UserId, AgencyId, StateFIPSCode);

            return Mapper.Map<CCSummaryReport, ViewCCSummaryReportViewData>(CCSummaryReport);

        }
        public ViewCCSummaryReportViewData GetCCSummaryReportByZipCodeOfClientResidence(DateTime DOCStartDate, DateTime DOCEndDate, string ZIPCodeofClientResidence, int ScopeId, int UserId, int AgencyId)
        {
            var CCSummaryReport = new CCSummaryReportDAL().GetCCSummaryReportByZipCodeOfClientResidence(DOCStartDate, DOCEndDate, ZIPCodeofClientResidence, ScopeId, UserId, AgencyId);

            return Mapper.Map<CCSummaryReport, ViewCCSummaryReportViewData>(CCSummaryReport);

        }
        public ViewCCSummaryReportViewData GetCCSummaryReportByContactsBySubStateRegionOnAgency(DateTime DOCStartDate, DateTime DOCEndDate, int RegionId)
        {
            var CCSummaryReport = new CCSummaryReportDAL().GetCCSummaryReportByContactsBySubStateRegionOnAgency(DOCStartDate, DOCEndDate, RegionId);

            return Mapper.Map<CCSummaryReport, ViewCCSummaryReportViewData>(CCSummaryReport);

        }
        public ViewCCSummaryReportViewData GetCCSummaryReportByNational(DateTime DOCStartDate, DateTime DOCEndDate)
        {
            var CCSummaryReport = new CCSummaryReportDAL().GetCCSummaryReportByNational(DOCStartDate, DOCEndDate);

            return Mapper.Map<CCSummaryReport, ViewCCSummaryReportViewData>(CCSummaryReport);

        }
        public ViewCCSummaryReportViewData GetCCSummaryReportByCounselor(DateTime DOCStartDate, DateTime DOCEndDate, int AgecyID, int counselorUserID)
        {
            var CCSummaryReport = new CCSummaryReportDAL().GetCCSummaryReportByCounselor(DOCStartDate, DOCEndDate, AgecyID, counselorUserID);

            return Mapper.Map<CCSummaryReport, ViewCCSummaryReportViewData>(CCSummaryReport);

        }
        public ViewCCSummaryReportViewData GetCCSummaryReportBySubmitter(DateTime DOCStartDate, DateTime DOCEndDate, int AgecyID, int submitterUserID)
        {
            var CCSummaryReport = new CCSummaryReportDAL().GetCCSummaryReportBySubmitter(DOCStartDate, DOCEndDate,AgecyID, submitterUserID);

            return Mapper.Map<CCSummaryReport, ViewCCSummaryReportViewData>(CCSummaryReport);

        }


        //added

        public ViewCCSummaryReportViewData GetCCSummaryReportByContactsBySubStateRegionOnCountiesoOfCounselorLocation(DateTime DOCStartDate, DateTime DOCEndDate, int submitterUserID)
        {
            var CCSummaryReport = new CCSummaryReportDAL().GetCCSummaryReportByContactsBySubStateRegionOnCountiesoOfCounselorLocation(DOCStartDate, DOCEndDate, submitterUserID);

            return Mapper.Map<CCSummaryReport, ViewCCSummaryReportViewData>(CCSummaryReport);

        }
        public ViewCCSummaryReportViewData GetCCSummaryReportByContactsBySubStateRegionOnZipcodesOfCounselorLocation(DateTime DOCStartDate, DateTime DOCEndDate, int submitterUserID)
        {
            var CCSummaryReport = new CCSummaryReportDAL().GetCCSummaryReportByContactsBySubStateRegionOnZipcodesOfCounselorLocation(DOCStartDate, DOCEndDate, submitterUserID);

            return Mapper.Map<CCSummaryReport, ViewCCSummaryReportViewData>(CCSummaryReport);

        }
        public ViewCCSummaryReportViewData GetCCSummaryReportByContactsBySubStateRegionOnCountiesOfClientResidence(DateTime DOCStartDate, DateTime DOCEndDate, int submitterUserID)
        {
            var CCSummaryReport = new CCSummaryReportDAL().GetCCSummaryReportByContactsBySubStateRegionOnCountiesOfClientResidence(DOCStartDate, DOCEndDate, submitterUserID);

            return Mapper.Map<CCSummaryReport, ViewCCSummaryReportViewData>(CCSummaryReport);

        }

        public ViewCCSummaryReportViewData GetCCSummaryReportByContactsBySubStateRegionOnZipcodeOfClientResidence(DateTime DOCStartDate, DateTime DOCEndDate, int submitterUserID)
        {
            var CCSummaryReport = new CCSummaryReportDAL().GetCCSummaryReportByContactsBySubStateRegionOnZipcodeOfClientResidence(DOCStartDate, DOCEndDate, submitterUserID);

            return Mapper.Map<CCSummaryReport, ViewCCSummaryReportViewData>(CCSummaryReport);

        }
    }
}
