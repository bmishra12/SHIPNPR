using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Data;
using AutoMapper;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkLogic.DataLayer;

namespace ShiptalkLogic.BusinessLayer
{
    public class AgencyBLL
    {
        public AgencyBLL()
        {
            CreateMaps();
        }

        private AgencyDAL _data;

        #region Properties

        private AgencyDAL Data
        {
            get
            {
                if (_data == null) _data = new AgencyDAL();

                return _data;
            }
        }

        #endregion

        public IEnumerable<KeyValuePair<int, string>> GetScopes()
        {
            return typeof(Scope).Descriptions();
        }

        public IEnumerable<KeyValuePair<string, string>> GetStates()
        {
            return State.GetStates();
        }
        
        public IEnumerable<KeyValuePair<string, string>> GetCounties(string stateFIPS)
        {
            return LookupDAL.GetCountiesNoMapping(stateFIPS);
        }


        public IEnumerable<KeyValuePair<string, string>> GetCountyByAgencyIdForReport(int AgencyId, FormType formType)
        {
            return LookupDAL.GetCountyByAgencyIdForReport(AgencyId, formType);

        }

        public IEnumerable<KeyValuePair<string, string>> GetCountyOfClientResidenceByAgencyIdForReport(int AgencyId)
        {
            return LookupDAL.GetCountyOfClientResidenceByAgencyIdForReport(AgencyId);

        }

        //most proabably not used now..
        public IEnumerable<KeyValuePair<string, string>> GetCountyForCounselorLocationByAgencyId(int AgencyId)
        {
            return Mapper.Map<IEnumerable<County>, IEnumerable<KeyValuePair<string, string>>>(LookupDAL.GetCountyForCounselorLocationByAgencyId(AgencyId));
        }




        public IEnumerable<KeyValuePair<string, string>> GetCountyForCounselorLocationByState(string stateFIPS, FormType formType)
        {
            return LookupDAL.GetCountyForCounselorLocationByState(stateFIPS, formType);
        }

        public IEnumerable<KeyValuePair<string, string>> GetCountyForClientResidenceByState(string stateFIPS)
        {
            return LookupDAL.GetCountyForClientResidenceByState(stateFIPS);
        }



        public IEnumerable<KeyValuePair<string, string>> GetZipByAgencyIdForReport(int AgencyId, FormType formType)
        {
            return LookupDAL.GetZipByAgencyIdForReport(AgencyId, formType);

        }

        public IEnumerable<KeyValuePair<string, string>> GetZipCodeOfClientResidenceByAgencyIdForReport(int AgencyId)
        {
            return LookupDAL.GetZipCodeOfClientResidenceByAgencyIdForReport(AgencyId);

        }

        public IEnumerable<KeyValuePair<string, string>> GetZipCodeForCounselorLocationByState(string stateFIPS, FormType formType)
        {
            return LookupDAL.GetZipForCounselorLocationByState(stateFIPS , formType);
        }

        public IEnumerable<KeyValuePair<string, string>> GetZipCodeForClientResidenceByState(string stateFIPS)
        {
            return LookupDAL.GetZipForClientResidenceByState(stateFIPS);
        }




        public IEnumerable<KeyValuePair<int, string>> GetAgencies(int userId, bool showAllAgencyUser)
        {
            return(Data.GetAgencies(userId, showAllAgencyUser));
        }

        public IEnumerable<KeyValuePair<int, string>> GetAgencies(string stateFIPS)
        {
            return Mapper.Map<IEnumerable<Agency>, IEnumerable<KeyValuePair<int, string>>>(LookupDAL.GetAgencies(stateFIPS));
        }
        public IEnumerable<KeyValuePair<int, string>> GetSubStateAgenciesForSubStateRegion(string stateFipsCode, int RegionID)
        {
            return Mapper.Map<IEnumerable<Agency>, IEnumerable<KeyValuePair<int, string>>>(Data.GetSubStateAgenciesForSubStateRegion(stateFipsCode, RegionID));
        }
        public IEnumerable<ViewAgencyProfileView> GetViewAgency(string stateFIPS, string CountyFIPS, string Zip)
        {
            return Mapper.Map<IEnumerable<Agency>, IEnumerable<ViewAgencyProfileView>>(
             new AgencyDAL().GetAgency(stateFIPS, CountyFIPS, Zip));
        }

        public IEnumerable<KeyValuePair<int, string>> GetAgencyTypes()
        {
            return typeof(AgencyType).Descriptions();
        }
         
        public string GetAgencyCode(int id)
        {
            var agency = Data.GetAgency(id, false);

            return (agency != null) ? agency.Code : string.Empty;
        }

        public int RegisterAgency(RegisterAgencyViewData viewData)
        {
            if (viewData == null)
                throw new ArgumentNullException("viewData");
            
            return Data.CreateAgency(Mapper.Map<RegisterAgencyViewData, Agency>(viewData));
        }

        public void UpdateAgency(EditAgencyViewData viewData)
        {
            if (viewData == null)
                throw new ArgumentNullException("viewData");

            Data.UpdateAgency(Mapper.Map<EditAgencyViewData, Agency>(viewData));
        }

        public IEnumerable<SearchAgenciesViewData> SearchAgencies(int userId, State state, Scope scope, string keywords)
        {

            return Mapper.Map<IEnumerable<Agency>, IEnumerable<SearchAgenciesViewData>>(Data.SearchAgencies(userId, state, scope, keywords));
        }

        public IEnumerable<SearchAgenciesViewData> ListAllAgencies(int userId, State state, Scope scope)
        {
            return Mapper.Map<IEnumerable<Agency>, IEnumerable<SearchAgenciesViewData>>(Data.ListAllAgencies(userId, state, scope));
        }

        public ViewAgencyViewData GetViewAgency(int id, bool includeLocations)
        {
            return Mapper.Map<Agency, ViewAgencyViewData>(Data.GetAgency(id, includeLocations));
        }

        public EditAgencyViewData GetEditAgency(int id, bool includeLocations)
        {
            var agency = Data.GetAgency(id, includeLocations);

            return Mapper.Map<Agency, EditAgencyViewData>(agency);
        }

        public ViewAgencyLocationViewData GetViewAgencyLocation (int id)
        {
            return Mapper.Map<AgencyLocation, ViewAgencyLocationViewData>(Data.GetAgencyLocation(id));
        }

        public EditAgencyLocationViewData GetEditAgencyLocation(int id)
        {
            return Mapper.Map<AgencyLocation, EditAgencyLocationViewData>(Data.GetAgencyLocation(id));
        }

        public int AddAgencyLocation(AddAgencyLocationViewData viewData)
        {
            if (viewData == null)
                throw new ArgumentNullException("viewData");

            return Data.CreateAgencyLocation(Mapper.Map<AddAgencyLocationViewData, AgencyLocation>(viewData));
        }

        public void UpdateAgencyLocation(EditAgencyLocationViewData viewData)
        {
            if (viewData == null)
                throw new ArgumentNullException("viewData");

            Data.UpdateAgencyLocation(Mapper.Map<EditAgencyLocationViewData, AgencyLocation>(viewData));
        }

        public void DeleteAgency(int id)
        {
            Data.DeleteAgency(id);
        }

        public void DeleteAgencyLocation(int id)
        {
            Data.DeleteAgencyLocation(id);
        }

        public int AddSubStateRegion(AddSubStateRegionViewData viewData)
        {
            return Data.CreateSubStateRegion(Mapper.Map<AddSubStateRegionViewData, SubStateRegion>(viewData));
        }

        public IEnumerable<SearchSubStateRegionsViewData> GetSubStateRegionsForCCReports(string stateFipsCode, int SubStateRegionType)
        {
            return Mapper.Map<IEnumerable<SubStateRegion>, IEnumerable<SearchSubStateRegionsViewData>>(Data.GetSubStateRegionsForCCReports(stateFipsCode, SubStateRegionType));
        }
       
        public IEnumerable<SearchSubStateRegionsViewData> SearchSubStateRegions(State state)
        {
            return Mapper.Map<IEnumerable<SubStateRegion>, IEnumerable<SearchSubStateRegionsViewData>>(Data.SearchSubStateRegions(state));
        }
        public ViewSubStateRegionViewData GetViewSubStateRegion(int id)
        {
            return Mapper.Map<SubStateRegion, ViewSubStateRegionViewData>(Data.GetSubStateRegion(id));
        }

        public EditSubStateRegionViewData GetEditSubStateRegion(int id)
        {
            return Mapper.Map<SubStateRegion, EditSubStateRegionViewData>(Data.GetSubStateRegion(id));
        }

        public IEnumerable<UserRegionalAccessProfile> GetSubStateRegionalAccessProfiles(int userId)
        {
            return UserDAL.GetUserRegionalProfiles(userId, Scope.SubStateRegion);
        }

        public void UpdateSubStateRegion(EditSubStateRegionViewData viewData)
        {
            Data.UpdateSubStateRegion(Mapper.Map<EditSubStateRegionViewData, SubStateRegion>(viewData));
        }

        public void DeleteSubStateRegion(int id)
        {
            Data.DeleteSubStateRegion(id);
        }

        public IEnumerable<KeyValuePair<int, string>> GetAgencyUsers(int id, Descriptor descriptor)
        {
            return GetAgencyUsers(id, descriptor, false);
        }

        public IEnumerable<KeyValuePair<int, string>> GetAgencyUsers(int id, Descriptor descriptor, bool includeInactive)
        {
            return Mapper.Map<IEnumerable<User>, IEnumerable<KeyValuePair<int, string>>>(Data.GetAgencyUsers(id, descriptor, includeInactive));
        }

        public AgencyIdentifiers GetAgencyIdentifiers(int id)
        {
            return Mapper.Map<Agency, AgencyIdentifiers>(Data.GetAgency(id, false));
        }

        public bool DoesAgencyNameExist(string agencyName)
        {
            return Data.DoesAgencyNameExist(agencyName);
        }

        private void CreateMaps()
        {
            Mapper.CreateMap<RegisterAgencyViewData, Agency>()
                .ForMember(dest => dest.ServiceAreas, opt => opt.MapFrom(src => src.ServiceAreas.Split(',').ToCountyList()))
                .ForMember(dest => dest.Locations, opt => opt.MapFrom(src => new List<AgencyLocation> { src.ToAgencyLocation() }));

            Mapper.CreateMap<Agency, EditAgencyViewData>()
               .ForMember(dest => dest.PhysicalAddress1, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.Address1))
               .ForMember(dest => dest.PhysicalAddress2, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.Address2))
               .ForMember(dest => dest.PhysicalCity, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.City))
               .ForMember(dest => dest.PhysicalCountyFIPS, opt => opt.MapFrom(
                   src => (src.Locations[0].PhysicalAddress.County == null) ? string.Empty : src.Locations[0].PhysicalAddress.County.Code))
               .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.State))
               .ForMember(dest => dest.PhysicalZip, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.Zip))
               //Added by Lavanya
               .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.Longitude))
               .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.Latitude))
               .ForMember(dest => dest.AvailableLanguages, opt => opt.MapFrom(src => src.Locations[0].AvailableLanguages))
               .ForMember(dest => dest.HideAgencyFromSearch, opt => opt.MapFrom(src => src.Locations[0].HideAgencyFromSearch))
               //end
               .ForMember(dest => dest.MailingAddress1, opt => opt.MapFrom(src => src.Locations[0].MailingAddress.Address1))
               .ForMember(dest => dest.MailingAddress2, opt => opt.MapFrom(src => src.Locations[0].MailingAddress.Address2))
               .ForMember(dest => dest.MailingCity, opt => opt.MapFrom(src => src.Locations[0].MailingAddress.City))
               .ForMember(dest => dest.MailingState, opt => opt.MapFrom(src => src.Locations[0].MailingAddress.State))
               .ForMember(dest => dest.MailingZip, opt => opt.MapFrom(src => src.Locations[0].MailingAddress.Zip))
               .ForMember(dest => dest.HoursOfOperation, opt => opt.MapFrom(src => src.Locations[0].HoursOfOperation))
               .ForMember(dest => dest.PrimaryPhone, opt => opt.MapFrom(src => src.Locations[0].PrimaryPhone))
               .ForMember(dest => dest.SecondaryPhone, opt => opt.MapFrom(src => src.Locations[0].SecondaryPhone))
               .ForMember(dest => dest.PrimaryEmail, opt => opt.MapFrom(src => src.Locations[0].PrimaryEmail))
               .ForMember(dest => dest.SecondaryEmail, opt => opt.MapFrom(src => src.Locations[0].SecondaryEmail))
               .ForMember(dest => dest.TollFreePhone, opt => opt.MapFrom(src => src.Locations[0].TollFreePhone))
               .ForMember(dest => dest.TDD, opt => opt.MapFrom(src => src.Locations[0].TDD))
               .ForMember(dest => dest.TollFreeTDD, opt => opt.MapFrom(src => src.Locations[0].TollFreeTDD))
               .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.Locations[0].Fax))
               .ForMember(dest => dest.ServiceAreas, opt => opt.MapFrom(src =>
                   string.Join(",", (from sa in src.ServiceAreas select sa.Code).ToArray())));

            Mapper.CreateMap<EditAgencyViewData, Agency>()
                .ForMember(dest => dest.ServiceAreas, opt => opt.MapFrom(src => src.ServiceAreas.Split(',').ToCountyList()))
                .ForMember(dest => dest.Locations, opt => opt.MapFrom(src => new List<AgencyLocation> { src.ToAgencyLocation() }));

            Mapper.CreateMap<Agency, SearchAgenciesViewData>()
                .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.Address1))
                .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.Address2))
                .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.Id))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.City))
                .ForMember(dest => dest.HoursOfOperation, opt => opt.MapFrom(src => src.Locations[0].HoursOfOperation))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Locations[0].LocationName))
                .ForMember(dest => dest.PrimaryPhone, opt => opt.MapFrom(src => src.Locations[0].PrimaryPhone))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.State))
                .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.Zip));

            Mapper.CreateMap<Agency, ViewAgencyProfileView>()
              .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.AgencyCode, opt => opt.MapFrom(src => src.Code))
              .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Locations[0].LocationName))
              .ForMember(dest => dest.PhysicalAddress1, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.Address1))
              .ForMember(dest => dest.PhysicalAddress2, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.Address2))
              .ForMember(dest => dest.PhysicalCity, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.City))
              .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.State))
              .ForMember(dest => dest.PhysicalZip, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.Zip))
              .ForMember(dest => dest.HoursOfOperation, opt => opt.MapFrom(src => src.Locations[0].HoursOfOperation))
              .ForMember(dest => dest.PrimaryPhone, opt => opt.MapFrom(src => src.Locations[0].PrimaryPhone))
              .ForMember(dest => dest.PrimaryEmail, opt => opt.MapFrom(src => src.Locations[0].PrimaryEmail))
              .ForMember(dest => dest.IsMainOffice, opt => opt.MapFrom(src => src.Locations[0].IsMainOffice));

            Mapper.CreateMap<AgencyLocation, ViewAgencyProfileLocationViewData>()
                .ForMember(dest => dest.PhysicalAddress1, opt => opt.MapFrom(src => src.PhysicalAddress.Address1))
                .ForMember(dest => dest.PhysicalAddress2, opt => opt.MapFrom(src => src.PhysicalAddress.Address2))
                .ForMember(dest => dest.PhysicalCity, opt => opt.MapFrom(src => src.PhysicalAddress.City))
                .ForMember(dest => dest.PhysicalCounty, opt => opt.MapFrom(
                    src => (src.PhysicalAddress.County == null) ? string.Empty : src.PhysicalAddress.County.ShortName))
                 .ForMember(dest => dest.PhysicalCountyFIPS, opt => opt.MapFrom(
                    src => (src.PhysicalAddress.County == null) ? string.Empty : src.PhysicalAddress.County.Code))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.PhysicalAddress.State))
                .ForMember(dest => dest.PhysicalZip, opt => opt.MapFrom(src => src.PhysicalAddress.Zip));

            Mapper.CreateMap<Agency, ViewAgencyViewData>() 
                .ForMember(dest => dest.PhysicalAddress1, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.Address1))
                .ForMember(dest => dest.PhysicalAddress2, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.Address2))
                .ForMember(dest => dest.PhysicalCity, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.City))
                .ForMember(dest => dest.PhysicalCounty, opt => opt.MapFrom(
                    src => (src.Locations[0].PhysicalAddress.County == null) ? string.Empty : src.Locations[0].PhysicalAddress.County.ShortName))
                 .ForMember(dest => dest.PhysicalCountyFIPS, opt => opt.MapFrom(
                    src => (src.Locations[0].PhysicalAddress.County == null) ? string.Empty : src.Locations[0].PhysicalAddress.County.Code))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.State))
                .ForMember(dest => dest.PhysicalZip, opt => opt.MapFrom(src => src.Locations[0].PhysicalAddress.Zip))
                .ForMember(dest => dest.MailingAddress1, opt => opt.MapFrom(src => src.Locations[0].MailingAddress.Address1))
                .ForMember(dest => dest.MailingAddress2, opt => opt.MapFrom(src => src.Locations[0].MailingAddress.Address2))
                .ForMember(dest => dest.MailingCity, opt => opt.MapFrom(src => src.Locations[0].MailingAddress.City))
                .ForMember(dest => dest.MailingState, opt => opt.MapFrom(src => src.Locations[0].MailingAddress.State))
                .ForMember(dest => dest.MailingZip, opt => opt.MapFrom(src => src.Locations[0].MailingAddress.Zip))
                .ForMember(dest => dest.HoursOfOperation, opt => opt.MapFrom(src => src.Locations[0].HoursOfOperation))
                .ForMember(dest => dest.PrimaryPhone, opt => opt.MapFrom(src => src.Locations[0].PrimaryPhone))
                .ForMember(dest => dest.SecondaryPhone, opt => opt.MapFrom(src => src.Locations[0].SecondaryPhone))
                .ForMember(dest => dest.PrimaryEmail, opt => opt.MapFrom(src => src.Locations[0].PrimaryEmail))
                .ForMember(dest => dest.SecondaryEmail, opt => opt.MapFrom(src => src.Locations[0].SecondaryEmail))
                .ForMember(dest => dest.TollFreePhone, opt => opt.MapFrom(src => src.Locations[0].TollFreePhone))
                .ForMember(dest => dest.TDD, opt => opt.MapFrom(src => src.Locations[0].TDD))
                .ForMember(dest => dest.TollFreeTDD, opt => opt.MapFrom(src => src.Locations[0].TollFreeTDD))
                .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.Locations[0].Fax))
                //Added by Lavanya
                .ForMember(dest => dest.AvailableLanguages, opt => opt.MapFrom(src => src.Locations[0].AvailableLanguages))
                .ForMember(dest => dest.HideAgencyFromSearch, opt => opt.MapFrom(src => src.Locations[0].HideAgencyFromSearch));
                //end

            Mapper.CreateMap<AgencyLocation, ViewAgencyLocationViewData>()
                .ForMember(dest => dest.PhysicalAddress1, opt => opt.MapFrom(src => src.PhysicalAddress.Address1))
                .ForMember(dest => dest.PhysicalAddress2, opt => opt.MapFrom(src => src.PhysicalAddress.Address2))
                .ForMember(dest => dest.PhysicalCity, opt => opt.MapFrom(src => src.PhysicalAddress.City))
                .ForMember(dest => dest.PhysicalCounty, opt => opt.MapFrom(
                    src => (src.PhysicalAddress.County == null) ? string.Empty : src.PhysicalAddress.County.ShortName))
                 .ForMember(dest => dest.PhysicalCountyFIPS, opt => opt.MapFrom(
                    src => (src.PhysicalAddress.County == null) ? string.Empty : src.PhysicalAddress.County.Code))
                .ForMember(dest => dest.PhysicalState, opt => opt.MapFrom(src => src.PhysicalAddress.State))
                .ForMember(dest => dest.PhysicalZip, opt => opt.MapFrom(src => src.PhysicalAddress.Zip))
                .ForMember(dest => dest.MailingAddress1, opt => opt.MapFrom(src => src.MailingAddress.Address1))
                .ForMember(dest => dest.MailingAddress2, opt => opt.MapFrom(src => src.MailingAddress.Address2))
                .ForMember(dest => dest.MailingCity, opt => opt.MapFrom(src => src.MailingAddress.City))
                .ForMember(dest => dest.MailingState, opt => opt.MapFrom(src => src.MailingAddress.State))
                .ForMember(dest => dest.MailingZip, opt => opt.MapFrom(src => src.MailingAddress.Zip))
                 //Added by Lavanya
                .ForMember(dest => dest.AvailableLanguages, opt => opt.MapFrom(src => src.AvailableLanguages))
                .ForMember(dest => dest.HideAgencyFromSearch, opt => opt.MapFrom(src => src.HideAgencyFromSearch));
                 //end;

            Mapper.CreateMap<AgencyLocation, EditAgencyLocationViewData>()
                .ForMember(dest => dest.PhysicalAddress1, opt => opt.MapFrom(src => src.PhysicalAddress.Address1))
                .ForMember(dest => dest.PhysicalAddress2, opt => opt.MapFrom(src => src.PhysicalAddress.Address2))
                .ForMember(dest => dest.PhysicalCity, opt => opt.MapFrom(src => src.PhysicalAddress.City))
                .ForMember(dest => dest.PhysicalCountyFIPS, opt => opt.MapFrom(
                    src => (src.PhysicalAddress.County == null) ? string.Empty : src.PhysicalAddress.County.Code))
                .ForMember(dest => dest.PhysicalState, opt => opt.MapFrom(src => src.PhysicalAddress.State.Value.StateAbbr))
                .ForMember(dest => dest.PhysicalZip, opt => opt.MapFrom(src => src.PhysicalAddress.Zip))               
                .ForMember(dest => dest.MailingAddress1, opt => opt.MapFrom(src => src.MailingAddress.Address1))
                .ForMember(dest => dest.MailingAddress2, opt => opt.MapFrom(src => src.MailingAddress.Address2))
                .ForMember(dest => dest.MailingCity, opt => opt.MapFrom(src => src.MailingAddress.City))
                .ForMember(dest => dest.MailingState, opt => opt.MapFrom(src => src.MailingAddress.State))
                .ForMember(dest => dest.MailingZip, opt => opt.MapFrom(src => src.MailingAddress.Zip))
                 //Added by Lavanya
                .ForMember(dest => dest.AvailableLanguages, opt => opt.MapFrom(src => src.AvailableLanguages))
                .ForMember(dest => dest.HideAgencyFromSearch, opt => opt.MapFrom(src => src.HideAgencyFromSearch));
                //end

            Mapper.CreateMap<AddAgencyLocationViewData, AgencyLocation>()
                .ForMember(dest => dest.PhysicalAddress, opt => opt.MapFrom(src =>
                    new AgencyAddress
                    {
                        Address1 = src.PhysicalAddress1,
                        Address2 = src.PhysicalAddress2,
                        City = src.PhysicalCity,
                        County = new County { Code = src.PhysicalCounty },
                        CreatedBy = src.CreatedBy,
                        State = src.State,
                        Zip = src.PhysicalZip,
                        //Added by Lavanya
                        Longitude = src.Longitude,
                        Latitude = src.Latitude
                        //end
                    }))
                .ForMember(dest => dest.MailingAddress, opt => opt.MapFrom(src =>
                    new AgencyAddress
                    {
                        Address1 = src.MailingAddress1,
                        Address2 = src.MailingAddress2,
                        City = src.MailingCity,
                        CreatedBy = src.CreatedBy,
                        State = src.MailingState,
                        Zip = src.MailingZip
                    }));

            Mapper.CreateMap<EditAgencyLocationViewData, AgencyLocation>()
                .ForMember(dest => dest.PhysicalAddress, opt => opt.MapFrom(src =>
                    new AgencyAddress
                    {
                        Address1 = src.PhysicalAddress1,
                        Address2 = src.PhysicalAddress2,
                        City = src.PhysicalCity,
                        County = new County { Code = src.PhysicalCountyFIPS },
                        CreatedBy = src.CreatedBy,
                        State = new State(src.PhysicalState),
                        Zip = src.PhysicalZip,
                        //Added by Lavanya
                        Longitude = src.Longitude,
                        Latitude = src.Latitude
                        //end
                    }))
                .ForMember(dest => dest.MailingAddress, opt => opt.MapFrom(src =>
                    new AgencyAddress
                    {
                        Address1 = src.MailingAddress1,
                        Address2 = src.MailingAddress2,
                        City = src.MailingCity,
                        CreatedBy = src.CreatedBy,
                        State = src.MailingState,
                        Zip = src.MailingZip
                    }));

            Mapper.CreateMap<County, KeyValuePair<string, string>>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ShortName));

            Mapper.CreateMap<Zip, KeyValuePair<string, string>>()
              .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.ZipCode))
              .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ZipCode));

            Mapper.CreateMap<Agency, KeyValuePair<int, string>>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name));

            Mapper.CreateMap<AddSubStateRegionViewData, SubStateRegion>()
                .ForMember(dest => dest.Agencies, opt => opt.MapFrom(src => src.Agencies.Split(',').ToAgencyList()));

            Mapper.CreateMap<SubStateRegion, SearchSubStateRegionsViewData>();

            Mapper.CreateMap<SubStateRegion, ViewSubStateRegionViewData>()
                .ForMember(dest => dest.Agencies, opt => opt.MapFrom(src => (from agency in src.Agencies select agency.Name).ToList()));

            Mapper.CreateMap<SubStateRegion, EditSubStateRegionViewData>()
                .ForMember(dest => dest.Agencies, opt => opt.MapFrom(src => 
                    string.Join(",", (from agency in src.Agencies select agency.Id.ToString()).ToArray())));

            Mapper.CreateMap<EditSubStateRegionViewData, SubStateRegion>()
                .ForMember(dest => dest.Agencies, opt => opt.MapFrom(src => src.Agencies.Split(',').ToAgencyList()));

            Mapper.CreateMap<User, KeyValuePair<int, string>>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.UserAccount.UserId))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => (src.UserAccount.IsActive) ? string.Format("{0} {1} [{2}]", src.UserProfile.FirstName, src.UserProfile.LastName, src.UserAccount.RegionName) 
                    : string.Format("{0} {1} [{2}] [{3}]", src.UserProfile.FirstName, src.UserProfile.LastName, "Inactive", src.UserAccount.RegionName)));

            Mapper.CreateMap<Agency, AgencyIdentifiers>();
        }

        public IEnumerable<KeyValuePair<int, string>> GetAgencies(string stateFIPS,string countyFIPS,string zip)
        {
            return Mapper.Map<IEnumerable<Agency>, IEnumerable<KeyValuePair<int, string>>>(LookupDAL.GetAgencies(stateFIPS,countyFIPS,zip));
        }

        public bool IsAgencyUserActive(int agencyId, int userId)
        {
            return Data.IsAgencyUserActive(agencyId, userId);
        }

        public IDictionary<int, IList<int>> GetActiveAgencyDescriptors(int userId)
        {
            return Data.GetActiveAgencyDescriptors(userId);
        }

        public bool IsSuperDataEditor(int userId)
        {
            return Data.IsSuperDataEditor(userId);
        }

        public IList<int> GetActiveAgencies(int userId)
        {
            return Data.GetActiveAgencies(userId);
        }

        public IList<KeyValuePair<int, string>> GetStateAgenciesList(Scope scope, State state, int userId)
        {
            return Data.GetStateAgenciesList(scope, state, userId);
        }

        //Added by Lavanya
        public DataTable GetAgencyLocationForGeoSearch(int LocationId)
        {
            return Data.GetAgencyLocationForGeoSearch(LocationId);
        }
        //end
       
    }
}