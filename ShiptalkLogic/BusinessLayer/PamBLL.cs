using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using AutoMapper;

using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.DataLayer;
using ShiptalkLogic.BusinessObjects.UI;
using System.Data;



namespace ShiptalkLogic.BusinessLayer
{
    public class PamBLL
    {
        public struct PamPresenterFilterCriteria
        {
            public int UserId { get; set; }
            public Scope scope { get; set; }
            public string StateFIPS { get; set; }
            public int? AgencyId { get; set; }
        }


        public struct PamSearchCriteria
        {
            public int UserId { get; set; }
            public Scope scope { get; set; }
            public int? AgencyId { get; set; }
            public string StateFIPS { get; set; }
            public int? PresenterId { get; set; }
            public int? SubmitterId { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }

        }

        public PamBLL()
        {
            CreateMaps();
        }


        private PAMDAL _data;

        #region Properties

        private PAMDAL Data
        {
            get
            {
                if (_data == null) _data = new PAMDAL();

                return _data;
            }
        }

        #endregion


        public  IEnumerable<KeyValuePair<int, string>> GetPAMSubmittersForScope(string StateFIPS, int ScopeId, int userId,  bool isActive)
        {
            return Data.GetPAMSubmittersForScope(StateFIPS, ScopeId, userId, isActive);
        }


        //not used
        public IEnumerable<KeyValuePair<int, string>> GetPAMPresentorsForScope(string StateFIPS, int ScopeId, int userId, bool isActive)
        {
            return Data.GetPAMPresentorsForScope(StateFIPS, ScopeId, userId, isActive);
        }

        public IEnumerable<KeyValuePair<int, string>> GetPamPresenters(PamPresenterFilterCriteria CounselorFilters)
        {
            if (CounselorFilters.AgencyId.HasValue)
                return Data.GetPamPresentersByAgency(CounselorFilters.UserId, CounselorFilters.StateFIPS, CounselorFilters.scope.EnumValue<int>(), CounselorFilters.AgencyId.Value);
            else
                return Data.GetAllPamPresenters(CounselorFilters.UserId, CounselorFilters.StateFIPS, CounselorFilters.scope.EnumValue<int>());
        }


        public IEnumerable<KeyValuePair<int, string>> GetPamPresentersForPamForm(PamPresenterFilterCriteria CounselorFilters, bool isActive)
        {
            return Data.GetPamPresentersByAgencyActiveInactive(CounselorFilters.UserId, CounselorFilters.StateFIPS, CounselorFilters.scope.EnumValue<int>(), CounselorFilters.AgencyId.Value, isActive);
        }

        public IEnumerable<SearchPublicMediaEventViewData> SearchPam(int AgencyId, DateTime? ActivityStartDateFrom, DateTime? ActivityStartDateTo)
        {
            //return Mapper.Map<IEnumerable<PublicMediaEvent>, IEnumerable<SearchPublicMediaEventViewData>>(Data.SearchPam(AgencyId, ActivityStartDateFrom, ActivityStartDateTo));

            return Data.SearchPam(AgencyId, ActivityStartDateFrom, ActivityStartDateTo);
        }

        
        public IEnumerable<SpecialField> GetStateSpecialFields(State state)
        {
            return Data.GetSpecialFields(FormType.PublicMediaActivity, state, true);
        }

        public IEnumerable<SpecialField> GetCMSSpecialFields()
        {
            return Data.GetSpecialFields(FormType.PublicMediaActivity, new State("99"), true);
        }

        public IEnumerable<SpecialField> GetStateSpecialFields(State state, bool restrictDate)
        {
            return Data.GetSpecialFields(FormType.PublicMediaActivity, state, restrictDate);
        }

        public IEnumerable<SpecialField> GetCMSSpecialFields(bool restrictDate)
        {
            return Data.GetSpecialFields(FormType.PublicMediaActivity, new State("99"), restrictDate);
        }

        public IEnumerable<KeyValuePair<int, string>> GetClientStatus()
        {
            return typeof(ClientStatus).Descriptions();
        }


        //Returns "PAM Topics" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetPamTopics()
        {
            return typeof(PAMTopic).Descriptions();

        }

        //Returns "PAM Target Audience" by calling BO layer.
        public IEnumerable<KeyValuePair<int, string>> GetPamTargetAudience()
        {
            return typeof(PAMAudiance).Descriptions();

        }


        public ViewPublicMediaEventViewData GetViewPAM(int id)
        {
            return Mapper.Map<PublicMediaEvent, ViewPublicMediaEventViewData>(Data.GetPam(id));
        }


        public EditPublicMediaEventViewData GetEditPAM(int id)
        {
            return Mapper.Map<PublicMediaEvent, EditPublicMediaEventViewData>(Data.GetPam(id));
        }

        public bool IsUserIdInPresenters(int pamId, int userId)
        {

            return Data.IsUserIdInPresenters(pamId, userId);
        }

        public bool DeletePamByPamID(int pamId, out string FailureReason)
        {
            return Data.DeletePamByPamID(pamId, out FailureReason);

        }


        public bool IsUserPamReviewer(int pamId, int userId)
        {
            return Data.IsUserPamReviewer(pamId, userId);
        }

        public int AddPublicMediaEvent(AddPublicMediaEventViewData viewData)
        {
            return Data.CreatePam(Mapper.Map<AddPublicMediaEventViewData, PublicMediaEvent>(viewData));

        }

        public void UpdatePublicMediaEvent(EditPublicMediaEventViewData viewData)
        {
            if (viewData == null)
                throw new ArgumentNullException("viewData");

            Data.UpdatePam(Mapper.Map<EditPublicMediaEventViewData, PublicMediaEvent>(viewData));
        }


        public IEnumerable<SearchPublicMediaEventViewData> GetRecentPams(int userId)
        {

            return Data.GetRecentPams(userId);
        }


        public IEnumerable<SearchPublicMediaEventViewData> SearchPams(PamSearchCriteria criteria)
        {

            return Data.SearchPams(
                                           criteria.UserId,
                                           criteria.scope.EnumValue<int>(),
                                           criteria.StateFIPS,
                                           criteria.FromDate,
                                           criteria.ToDate,
                                           criteria.PresenterId,
                                           criteria.SubmitterId,
                                           criteria.AgencyId
                                       );

        }






        public IEnumerable<SearchPublicMediaEventViewData> SearchPamByStartDate(DateTime startDate, string stateCode)
        {
            return Mapper.Map<IEnumerable<PublicMediaEvent>, IEnumerable<SearchPublicMediaEventViewData>>(Data.SearchPamByStartDate(startDate, stateCode));
        }

        public IEnumerable<SearchPublicMediaEventViewData> SearchPamByStartDateRange(DateTime startDateFrom, DateTime startDateTo, string stateCode)
        {
            return Mapper.Map<IEnumerable<PublicMediaEvent>, IEnumerable<SearchPublicMediaEventViewData>>(Data.SearchPamByStartDateRange(startDateFrom, startDateTo, stateCode));
        }

        public IEnumerable<SearchPublicMediaEventViewData> SearchPamBySubmitter(int SubmitterUserId, string stateCode)
        {
            return Mapper.Map<IEnumerable<PublicMediaEvent>, IEnumerable<SearchPublicMediaEventViewData>>(Data.SearchPamBySubmitter(SubmitterUserId, stateCode));
        }

        public IEnumerable<SearchPublicMediaEventViewData> SearchPamByPresentor(int PamUserID)
        {
            return Mapper.Map<IEnumerable<PublicMediaEvent>, IEnumerable<SearchPublicMediaEventViewData>>(Data.SearchPamByPresentor(PamUserID));
        }

        private void CreateMaps()
        {
            Mapper.CreateMap<AddPublicMediaEventViewData, PublicMediaEvent>()
                .ForMember(dest => dest.PAMSelectedTopics,
                           opt => opt.MapFrom(src => src.PAMSelectedTopics.ToIdList<PAMTopic>()))
                .ForMember(dest => dest.PAMSelectedAudiences,
                           opt => opt.MapFrom(src => src.PAMSelectedAudiences.ToIdList<PAMAudiance>()))
                .ForMember(dest => dest.StateSpecialUseFields,
                           opt => opt.MapFrom(src => src.StateSpecialUseFields.ToSpecialFieldValueList()))
                .ForMember(dest => dest.CMSSpecialUseFields,
                           opt => opt.MapFrom(src => src.CMSSpecialUseFields.ToSpecialFieldValueList()))

                           ;


            Mapper.CreateMap<PublicMediaEvent, ViewPublicMediaEventViewData>()
                .ForMember(dest => dest.PAMSelectedTopics,
                           opt => opt.MapFrom(src => src.PAMSelectedTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.PAMSelectedAudiences,
                           opt => opt.MapFrom(src => src.PAMSelectedAudiences.ToKeyValuePairList()))
                          ;

            Mapper.CreateMap<PublicMediaEvent, EditPublicMediaEventViewData>()
                .ForMember(dest => dest.PAMSelectedTopics,
                           opt => opt.MapFrom(src => src.PAMSelectedTopics.ToKeyValuePairList()))
                .ForMember(dest => dest.PAMSelectedAudiences,
                           opt => opt.MapFrom(src => src.PAMSelectedAudiences.ToKeyValuePairList()))

                .ForMember(dest => dest.StateSpecialUseFields,
                           opt => opt.MapFrom(src => src.StateSpecialUseFields.ToDictionary()))
                .ForMember(dest => dest.CMSSpecialUseFields,
                           opt => opt.MapFrom(src => src.CMSSpecialUseFields.ToDictionary()))
              ;

            Mapper.CreateMap<EditPublicMediaEventViewData, PublicMediaEvent>()
                .ForMember(dest => dest.PAMSelectedTopics,
                           opt => opt.MapFrom(src => src.PAMSelectedTopics.ToIdList<PAMTopic>()))
                .ForMember(dest => dest.PAMSelectedAudiences,
                           opt => opt.MapFrom(src => src.PAMSelectedAudiences.ToIdList<PAMAudiance>()))
                .ForMember(dest => dest.StateSpecialUseFields,
                           opt => opt.MapFrom(src => src.StateSpecialUseFields.ToSpecialFieldValueList()))
                .ForMember(dest => dest.CMSSpecialUseFields,
                           opt => opt.MapFrom(src => src.CMSSpecialUseFields.ToSpecialFieldValueList()))

                ;


            Mapper.CreateMap<PublicMediaEvent, SearchPublicMediaEventViewData>()
                ;
        }

    }
}
