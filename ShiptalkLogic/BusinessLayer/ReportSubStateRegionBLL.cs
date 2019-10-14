using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkLogic.DataLayer;

namespace ShiptalkLogic.BusinessLayer
{
    public class ReportSubStateRegionBLL
    {
        public ReportSubStateRegionBLL()
        {
            CreateMaps();
        }

        private SubStateRegionForReportDAL _data;

        #region Properties

        private SubStateRegionForReportDAL Data
        {
            get
            {
                if (_data == null) _data = new SubStateRegionForReportDAL();

                return _data;
            }
        }

        #endregion


        public IEnumerable<SearchSubStateRegionForReportViewData> ListAllSubStates()
        {
            return Mapper.Map<IEnumerable<SubStateRegionForReport>, IEnumerable<SearchSubStateRegionForReportViewData>>(Data.ListAllSubStates());
        }

        public IEnumerable<SearchSubStateRegionForReportViewData> ListSubStatesForState(string stateFips)
        {
            return Mapper.Map<IEnumerable<SubStateRegionForReport>, IEnumerable<SearchSubStateRegionForReportViewData>>(Data.ListSubStatesForState(stateFips));
        }

        

        public ViewSubStateRegionForReportViewData GetViewSubStateRegionForReport(int id)
        {
            return Mapper.Map<SubStateRegionForReport, ViewSubStateRegionForReportViewData>(Data.GetSubStateRegionForReport(id));
        }


        public EditSubStateRegionForReportViewData GetEditSubStateRegionForReport(int id)
        {
            return Mapper.Map<SubStateRegionForReport, EditSubStateRegionForReportViewData>(Data.GetSubStateRegionForReport(id));
        }


        public int CreateSubStateRegionForReport(AddSubStateRegionForReportViewData viewData)
        {
            if (viewData == null)
                throw new ArgumentNullException("viewData");

            return Data.CreateSubStateRegionForReport(Mapper.Map<AddSubStateRegionForReportViewData, SubStateRegionForReport>(viewData));
        }

        public void UpdateSubStateRegionForReport(EditSubStateRegionForReportViewData viewData)
        {
            Data.UpdateSubStateRegionForReport(Mapper.Map<EditSubStateRegionForReportViewData, SubStateRegionForReport>(viewData));
        }


        public void DeleteSubStateRegionForReport(int id)
        {
            Data.DeleteSubStateRegionForReport(id);
        }



        public bool DoesSubStateRegionForReportNameExist(string SubStateRegionForReportName)
        {
            return Data.DoesSubStateRegionForReportNameExist(SubStateRegionForReportName);
        }


        public string GetSubStateRegionAgencyNameForReport(int SubStateRegionID)
        {
            return Data.GetSubStateRegionAgencyNameForReport(SubStateRegionID);

        }

        public string GetSubStateRegionCountyNameForReport(int SubStateRegionID)
        {
            return Data.GetSubStateRegionCountyNameForReport(SubStateRegionID);

        }
        
        public string GetSubStateRegionZipCodeForReport(int SubStateRegionID)
        {
            return Data.GetSubStateRegionZipCodeForReport(SubStateRegionID);

        }

        //Returns "Group types " by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetSubStateReportType(FormType formType)
        {
            if (formType == FormType.ClientContact)
                return typeof(SubStateReportTypeCCF).Descriptions();
            else if (formType == FormType.PublicMediaActivity)

                return typeof(SubStateReportTypePAM).Descriptions();
            else
                return null;

        }

        //Returns FormType
        public IEnumerable<KeyValuePair<int, string>> GetFromeType()
        {
            return typeof(FormType).Descriptions();

        }

        private void CreateMaps()
        {
            Mapper.CreateMap<SubStateRegionForReport, SearchSubStateRegionForReportViewData>()
                ;

            Mapper.CreateMap<SubStateRegionForReport, ViewSubStateRegionForReportViewData>()
                ;

            Mapper.CreateMap<AddSubStateRegionForReportViewData, SubStateRegionForReport>()
                .ForMember(dest => dest.ServiceAreas, opt => opt.MapFrom(src => src.ServiceAreas.Split(',').ToSubStateRegionZIPFIPSForReportList()));

            Mapper.CreateMap<SubStateRegionForReport, EditSubStateRegionForReportViewData>()
            .ForMember(dest => dest.ServiceAreas, opt => opt.MapFrom(src =>
                                        string.Join(",", (from p in src.ServiceAreas select p.ZIPFIPSCountyCode.ToString()).ToArray())));


            Mapper.CreateMap<EditSubStateRegionForReportViewData, SubStateRegionForReport>()
                .ForMember(dest => dest.ServiceAreas, opt => opt.MapFrom(src => src.ServiceAreas.Split(',').ToSubStateRegionZIPFIPSForReportList()));


            Mapper.CreateMap<SubStateRegionZIPFIPSForReport, KeyValuePair<string, string>>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.ZIPFIPSCountyCode))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name));


        }




    }
}