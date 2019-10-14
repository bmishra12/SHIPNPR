using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using T = ShiptalkLogic.Constants.Tables;
using SP = ShiptalkLogic.Constants.StoredProcs;
using ShiptalkLogic.BusinessObjects;

namespace ShiptalkLogic.DataLayer
{
    internal class AgencyDAL : DALBase
    {
        public int CreateAgency(Agency agency)
        {
            if (agency == null)
                throw new ArgumentNullException("agency");

            if (agency.Locations == null || agency.Locations.Count == 0)
                throw new ArgumentNullException("agency",
                                                "At least one agency location is required to register an agency.");

            var location = agency.Locations[0];

            using (var command = database.GetStoredProcCommand("dbo.CreateAgency"))
            {
                database.AddInParameter(command, SP.CreateAgency.AgencyName, DbType.String, agency.Name);
                database.AddInParameter(command, SP.CreateAgency.AgencyTypeId, DbType.Int16, (int)agency.Type);
                database.AddInParameter(command, SP.CreateAgency.SponsorFirstName, DbType.String, agency.SponsorFirstName);
                database.AddInParameter(command, SP.CreateAgency.SponsorMiddleName, DbType.String, agency.SponsorMiddleName);
                database.AddInParameter(command, SP.CreateAgency.SponsorLastName, DbType.String, agency.SponsorLastName);
                database.AddInParameter(command, SP.CreateAgency.SponsorTitle, DbType.String, agency.SponsorTitle);
                database.AddInParameter(command, SP.CreateAgency.Url, DbType.String, agency.URL);
                database.AddInParameter(command, SP.CreateAgency.AgencyComments, DbType.String, agency.Comments);
                database.AddInParameter(command, SP.CreateAgency.CreatedBy, DbType.Int32, agency.CreatedBy);
                database.AddInParameter(command, SP.CreateAgency.StateFIPS, DbType.String, agency.State.Code);
                database.AddInParameter(command, SP.CreateAgency.LocationName, DbType.String, agency.Name);
                database.AddInParameter(command, SP.CreateAgency.PhysicalAddress1, DbType.String, location.PhysicalAddress.Address1);
                database.AddInParameter(command, SP.CreateAgency.PhysicalAddress2, DbType.String, location.PhysicalAddress.Address2);
                database.AddInParameter(command, SP.CreateAgency.PhysicalCity, DbType.String, location.PhysicalAddress.City);
                database.AddInParameter(command, SP.CreateAgency.PhysicalCountyFIPS, DbType.String, location.PhysicalAddress.County.Code);
                database.AddInParameter(command, SP.CreateAgency.PhysicalZip, DbType.String, location.PhysicalAddress.Zip);
                ////Added by Lavanya
                database.AddInParameter(command, SP.CreateAgency.Longitude, DbType.Double, location.PhysicalAddress.Longitude);
                database.AddInParameter(command, SP.CreateAgency.Latitude, DbType.Double, location.PhysicalAddress.Latitude);
                ////end
                database.AddInParameter(command, SP.CreateAgency.MailingAddress1, DbType.String, location.MailingAddress.Address1);
                database.AddInParameter(command, SP.CreateAgency.MailingAddress2, DbType.String, location.MailingAddress.Address2);
                database.AddInParameter(command, SP.CreateAgency.MailingCity, DbType.String, location.MailingAddress.City);
                database.AddInParameter(command, SP.CreateAgency.MailingZip, DbType.String, location.MailingAddress.Zip);
                database.AddInParameter(command, SP.CreateAgency.MailingStateFIPS, DbType.String, location.MailingAddress.State.Value.Code);
                database.AddInParameter(command, SP.CreateAgency.ContactFirstName, DbType.String, location.ContactFirstName);
                database.AddInParameter(command, SP.CreateAgency.ContactMiddleName, DbType.String, location.ContactMiddleName);
                database.AddInParameter(command, SP.CreateAgency.ContactLastName, DbType.String, location.ContactLastName);
                database.AddInParameter(command, SP.CreateAgency.ContactTitle, DbType.String, location.ContactTitle);
                database.AddInParameter(command, SP.CreateAgency.HoursOfOperation, DbType.String, location.HoursOfOperation);
                database.AddInParameter(command, SP.CreateAgency.PrimaryPhone, DbType.String, location.PrimaryPhone);
                database.AddInParameter(command, SP.CreateAgency.SecondaryPhone, DbType.String, location.SecondaryPhone);
                database.AddInParameter(command, SP.CreateAgency.TollFreePhone, DbType.String, location.TollFreePhone);
                database.AddInParameter(command, SP.CreateAgency.TDD, DbType.String, location.TDD);
                database.AddInParameter(command, SP.CreateAgency.TollFreeTDD, DbType.String, location.TollFreeTDD);
                database.AddInParameter(command, SP.CreateAgency.Fax, DbType.String, location.Fax);
                database.AddInParameter(command, SP.CreateAgency.PrimaryEmail, DbType.String, location.PrimaryEmail);
                database.AddInParameter(command, SP.CreateAgency.SecondaryEmail, DbType.String, location.SecondaryEmail);
                database.AddInParameter(command, SP.CreateAgency.AgencyLocationComments, DbType.String, location.Comments);
                database.AddInParameter(command, SP.CreateAgency.ServiceAreas, DbType.String, string.Join(",", (from county in agency.ServiceAreas select county.Code).ToArray()));
                database.AddInParameter(command, SP.CreateAgency.HideAgencyFromPublic, DbType.Boolean, agency.HideAgencyFromPublic);
                //Added by Lavanya
                database.AddInParameter(command, SP.CreateAgency.AvailableLanguages, DbType.String, location.AvailableLanguages);
                database.AddInParameter(command, SP.CreateAgency.HideAgencyFromSearch, DbType.Boolean, location.HideAgencyFromSearch);
                //end
                database.AddOutParameter(command, SP.CreateAgency.AgencyCode, DbType.String, 6);
                database.AddOutParameter(command, SP.CreateAgency.AgencyId, DbType.Int32, 6);
                database.AddOutParameter(command, SP.CreateAgency.AgencyLocationId, DbType.Int32, 6);


                database.ExecuteNonQuery(command);

                return (int)database.GetParameterValue(command, SP.CreateAgency.AgencyId);
            }
        }
        
        public void UpdateAgency(Agency agency)
        {
            if (agency == null)
                throw new ArgumentNullException("agency");

            if (!agency.Id.HasValue)
                throw new ArgumentNullException("agency",
                                                "An agency id is requred to update an agency.");

            if (agency.Locations == null || agency.Locations.Count == 0)
                throw new ArgumentNullException("agency",
                                                "At least one agency location is required to update an agency.");

            var location = agency.Locations[0];

            using (var command = database.GetStoredProcCommand("dbo.UpdateAgency"))
            {
                command.Parameters.Clear();
                database.AddInParameter(command, SP.UpdateAgency.AgencyId, DbType.String, agency.Id.GetValueOrDefault(0));
                database.AddInParameter(command, SP.UpdateAgency.AgencyName, DbType.String, agency.Name);
                database.AddInParameter(command, SP.UpdateAgency.AgencyTypeId, DbType.Int16, (int)agency.Type);
                database.AddInParameter(command, SP.UpdateAgency.SponsorFirstName, DbType.String, agency.SponsorFirstName);
                database.AddInParameter(command, SP.UpdateAgency.SponsorMiddleName, DbType.String, agency.SponsorMiddleName);
                database.AddInParameter(command, SP.UpdateAgency.SponsorLastName, DbType.String, agency.SponsorLastName);
                database.AddInParameter(command, SP.UpdateAgency.SponsorTitle, DbType.String, agency.SponsorTitle);
                database.AddInParameter(command, SP.UpdateAgency.Url, DbType.String, agency.URL);
                database.AddInParameter(command, SP.UpdateAgency.AgencyComments, DbType.String, agency.Comments);
                database.AddInParameter(command, SP.UpdateAgency.LastUpdatedBy, DbType.Int32, agency.LastUpdatedBy);
                database.AddInParameter(command, SP.UpdateAgency.StateFIPS, DbType.String, agency.State.Code);
                database.AddInParameter(command, SP.UpdateAgency.LocationName, DbType.String, agency.Name);
                database.AddInParameter(command, SP.UpdateAgency.PhysicalAddress1, DbType.String, location.PhysicalAddress.Address1);
                database.AddInParameter(command, SP.UpdateAgency.PhysicalAddress2, DbType.String, location.PhysicalAddress.Address2);
                database.AddInParameter(command, SP.UpdateAgency.PhysicalCity, DbType.String, location.PhysicalAddress.City);
                database.AddInParameter(command, SP.UpdateAgency.PhysicalZip, DbType.String, location.PhysicalAddress.Zip);
                database.AddInParameter(command, SP.UpdateAgency.PhysicalCountyFIPS, DbType.String, location.PhysicalAddress.County.Code);
                ////Added by Lavanya
                database.AddInParameter(command, SP.UpdateAgency.Longitude, DbType.Double, location.PhysicalAddress.Longitude);
                database.AddInParameter(command, SP.UpdateAgency.Latitude, DbType.Double, location.PhysicalAddress.Latitude);
                ////end
                database.AddInParameter(command, SP.UpdateAgency.MailingAddress1, DbType.String, location.MailingAddress.Address1);
                database.AddInParameter(command, SP.UpdateAgency.MailingAddress2, DbType.String, location.MailingAddress.Address2);
                database.AddInParameter(command, SP.UpdateAgency.MailingCity, DbType.String, location.MailingAddress.City);
                database.AddInParameter(command, SP.UpdateAgency.MailingZip, DbType.String, location.MailingAddress.Zip);
                database.AddInParameter(command, SP.UpdateAgency.MailingStateFIPS, DbType.String,
                                         location.MailingAddress.State.Value.Code);
                database.AddInParameter(command, SP.UpdateAgency.ContactFirstName, DbType.String, location.ContactFirstName);
                database.AddInParameter(command, SP.UpdateAgency.ContactMiddleName, DbType.String, location.ContactMiddleName);
                database.AddInParameter(command, SP.UpdateAgency.ContactLastName, DbType.String, location.ContactLastName);
                database.AddInParameter(command, SP.UpdateAgency.ContactTitle, DbType.String, location.ContactTitle);
                database.AddInParameter(command, SP.UpdateAgency.HoursOfOperation, DbType.String, location.HoursOfOperation);
                database.AddInParameter(command, SP.UpdateAgency.PrimaryPhone, DbType.String, location.PrimaryPhone);
                database.AddInParameter(command, SP.UpdateAgency.SecondaryPhone, DbType.String, location.SecondaryPhone);
                database.AddInParameter(command, SP.UpdateAgency.TollFreePhone, DbType.String, location.TollFreePhone);
                database.AddInParameter(command, SP.UpdateAgency.TDD, DbType.String, location.TDD);
                database.AddInParameter(command, SP.UpdateAgency.TollFreeTDD, DbType.String, location.TollFreeTDD);
                database.AddInParameter(command, SP.UpdateAgency.Fax, DbType.String, location.Fax);
                database.AddInParameter(command, SP.UpdateAgency.PrimaryEmail, DbType.String, location.PrimaryEmail);
                database.AddInParameter(command, SP.UpdateAgency.SecondaryEmail, DbType.String, location.SecondaryEmail);
                database.AddInParameter(command, SP.UpdateAgency.AgencyLocationComments, DbType.String, location.Comments);
                database.AddInParameter(command, SP.UpdateAgency.ServiceAreas, DbType.String,
                                         string.Join(",", (from county in agency.ServiceAreas select county.Code).ToArray()));
                database.AddInParameter(command, SP.UpdateAgency.IsActive, DbType.Boolean, agency.IsActive);
                database.AddInParameter(command, SP.UpdateAgency.HideAgencyFromPublic, DbType.Boolean, agency.HideAgencyFromPublic);
                //Added by Lavanya
                database.AddInParameter(command, SP.UpdateAgency.AvailableLanguages, DbType.String, location.AvailableLanguages);
                database.AddInParameter(command, SP.UpdateAgency.HideAgencyFromSearch, DbType.Boolean, location.HideAgencyFromSearch);
                //end

                database.ExecuteNonQuery(command);
            }
        }

        public IEnumerable<Agency> SearchAgencies(int userId, State state, Scope scope,string keywords)
        {
            var agencies = new List<Agency>();
            var keywordsBuilder = new StringBuilder();
            var keywordList = keywords.Split(' ');

            for (var i = 0; i < keywordList.Length; i++)
                if (i == 0)
                    keywordsBuilder.AppendFormat("'{0}'", keywordList[i]);
                else
                    keywordsBuilder.AppendFormat(" OR '{0}'", keywordList[i]);

            using (var command = database.GetStoredProcCommand("dbo.SearchAgencies"))
            {
                database.AddInParameter(command, SP.SearchAgencies.UserId, DbType.Int32, userId);

                if (string.IsNullOrEmpty(state.Code))
                    database.AddInParameter(command, SP.SearchAgencies.StateFIPS, DbType.String, DBNull.Value);
                else
                    database.AddInParameter(command, SP.SearchAgencies.StateFIPS, DbType.String, state.Code);

                database.AddInParameter(command, SP.SearchAgencies.ScopeId, DbType.Int32, (int)scope);
                database.AddInParameter(command, SP.SearchAgencies.Keywords, DbType.String, keywordsBuilder.ToString());

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        agencies.Add(new Agency
                        {
                            Id = reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetNullableInt32, null),
                            Name = reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, null),
                            Type = (AgencyType)reader.GetDefaultIfDBNull(T.Agency.AgencyTypeID, GetNullableInt16, null),
                            Locations = new List<AgencyLocation> { 
                                        new AgencyLocation { 
                                            LocationName = reader.GetDefaultIfDBNull(T.AgencyLocation.LocationName, GetString, null),
                                            HoursOfOperation = reader.GetDefaultIfDBNull(T.AgencyLocation.HoursOfOperation, GetString, null),
                                            PrimaryPhone = reader.GetDefaultIfDBNull(T.AgencyLocation.PrimaryPhone, GetString, null),
                                            PhysicalAddress = 
                                                new AgencyAddress { 
                                                    Id = reader. GetDefaultIfDBNull(T.AgencyLocation.PhysicalAddressID,GetNullableInt32, null),
                                                    Address1 = reader.GetDefaultIfDBNull(T.AgencyAddress.Address1,GetString, null),
                                                    Address2 = reader.GetDefaultIfDBNull(T.AgencyAddress.Address2,GetString, null),
                                                    City = reader.GetDefaultIfDBNull(T.AgencyAddress.City,GetString, null),
                                                    State = new State(reader.GetDefaultIfDBNull(T.AgencyAddress.StateFIPS,GetString,null)),
                                                    Zip = reader.GetDefaultIfDBNull(T.AgencyAddress.Zip,GetString, null)}
                                                 }
                                         },
                            Code = reader.GetDefaultIfDBNull(T.Agency.AgencyCode, GetString, null),
                            State = new State(reader.GetDefaultIfDBNull(T.Agency.StateFIPS, GetString, null)),
                        });
                    }
                }
            }

            return agencies;
        }

        public IEnumerable<Agency> ListAllAgencies(int userId, State state, Scope scope)
        {

            var agencies = new List<Agency>();

            using (var command = database.GetStoredProcCommand("dbo.ListAllAgencies"))
            {
                database.AddInParameter(command, SP.ListAllAgencies.UserId, DbType.Int32, userId);

                if (string.IsNullOrEmpty(state.Code))
                    database.AddInParameter(command, SP.ListAllAgencies.StateFIPS, DbType.String, DBNull.Value);
                else
                    database.AddInParameter(command, SP.ListAllAgencies.StateFIPS, DbType.String, state.Code);

                database.AddInParameter(command, SP.ListAllAgencies.ScopeId, DbType.Int32, (int)scope);


                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        agencies.Add(new Agency
                        {
                            Id = reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetNullableInt32, null),
                            Name = reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, null),
                            Type = (AgencyType)reader.GetDefaultIfDBNull(T.Agency.AgencyTypeID, GetNullableInt16, null),
                            Locations = new List<AgencyLocation> { 
                                        new AgencyLocation { 
                                            LocationName = reader.GetDefaultIfDBNull(T.AgencyLocation.LocationName, GetString, null),
                                            HoursOfOperation = reader.GetDefaultIfDBNull(T.AgencyLocation.HoursOfOperation, GetString, null),
                                            PrimaryPhone = reader.GetDefaultIfDBNull(T.AgencyLocation.PrimaryPhone, GetString, null),
                                            PhysicalAddress = 
                                                new AgencyAddress { 
                                                    Id = reader. GetDefaultIfDBNull(T.AgencyLocation.PhysicalAddressID,GetNullableInt32, null),
                                                    Address1 = reader.GetDefaultIfDBNull(T.AgencyAddress.Address1,GetString, null),
                                                    Address2 = reader.GetDefaultIfDBNull(T.AgencyAddress.Address2,GetString, null),
                                                    City = reader.GetDefaultIfDBNull(T.AgencyAddress.City,GetString, null),
                                                    State = new State(reader.GetDefaultIfDBNull(T.AgencyAddress.StateFIPS,GetString,null)),
                                                    Zip = reader.GetDefaultIfDBNull(T.AgencyAddress.Zip,GetString, null)}
                                                 }
                                         },
                            Code = reader.GetDefaultIfDBNull(T.Agency.AgencyCode, GetString, null),
                            State = new State(reader.GetDefaultIfDBNull(T.Agency.StateFIPS, GetString, null)),
                        });
                    }
                }
            }

            return agencies;
        }

        public Agency GetAgency(int id, bool includeLocations)
        {
            Agency agency = null;

            using (var command = database.GetStoredProcCommand("dbo.GetAgency"))
            {
                database.AddInParameter(command, SP.GetAgency.AgencyId, DbType.Int32, id);
                database.AddInParameter(command, SP.GetAgency.IncludeAllLocations, DbType.Boolean, includeLocations);
                
                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        agency = new Agency
                                 {
                                     Id = reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetNullableInt32, null),
                                     Name = reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, null),
                                     Code = reader.GetDefaultIfDBNull(T.Agency.AgencyCode, GetString, null),
                                     Type = (AgencyType)reader.GetDefaultIfDBNull(T.Agency.AgencyTypeID, GetNullableInt16, null),
                                     SponsorFirstName = reader.GetDefaultIfDBNull(T.Agency.SponsorFirstName, GetString, null),
                                     SponsorMiddleName = reader.GetDefaultIfDBNull(T.Agency.SponsorMiddleName, GetString, null),
                                     SponsorLastName = reader.GetDefaultIfDBNull(T.Agency.SponsorLastName, GetString, null),
                                     SponsorTitle = reader.GetDefaultIfDBNull(T.Agency.SponsorTitle, GetString, null),
                                     URL = reader.GetDefaultIfDBNull(T.Agency.URL, GetString, null),
                                     Comments = reader.GetDefaultIfDBNull(T.Agency.Comments, GetString, null),
                                     CreatedBy = reader.GetDefaultIfDBNull(T.Agency.CreatedBy, GetNullableInt32, null),
                                     CreatedDate = reader.GetDefaultIfDBNull(T.Agency.CreatedDate, GetNullableDateTime, null),
                                     LastUpdatedBy = reader.GetDefaultIfDBNull(T.Agency.LastUpdatedBy, GetNullableInt32, null),
                                     LastUpdatedDate = reader.GetDefaultIfDBNull(T.Agency.LastUpdatedDate, GetNullableDateTime, null),
                                     ActiveInactiveDate = reader.GetDefaultIfDBNull(T.Agency.ActiveInactiveDate, GetNullableDateTime, null),
                                     IsActive = reader.GetDefaultIfDBNull(T.Agency.IsActive, GetBool, false),
                                     HideAgencyFromPublic = reader.GetDefaultIfDBNull(T.Agency.HideAgencyFromPublic, GetBool, false),
                                     State = new State(reader.GetDefaultIfDBNull(T.Agency.StateFIPS, GetString, null)),
                                     ServiceAreas = new List<County>(),
                                     Locations = new List<AgencyLocation>()
                                 };

                        reader.NextResult();

                        while (reader.Read())
                        {
                            agency.ServiceAreas.Add(
                                new County
                                {
                                    Code = reader.GetDefaultIfDBNull(T.County.CountyFIPS, GetString, null),
                                    CBSACode = reader.GetDefaultIfDBNull(T.County.CBSACODE, GetString, null),
                                    CBSALSAD = reader.GetDefaultIfDBNull(T.County.CBSALSAD, GetString, null),
                                    CBSATitle = reader.GetDefaultIfDBNull(T.County.CBSATITLE, GetString, null), 
                                    CreatedDate = reader.GetDefaultIfDBNull(T.County.CreatedDate, GetNullableDateTime, null), 
                                    LastUpdatedBy = reader.GetDefaultIfDBNull(T.County.LastUpdatedBy, GetNullableInt32, null), 
                                    LastUpdatedDate = reader.GetDefaultIfDBNull(T.County.LastUpdatedDate, GetNullableDateTime, null), 
                                    Latitude = reader.GetDefaultIfDBNull(T.County.LAT, GetNullableDouble, null), 
                                    Longitude = reader.GetDefaultIfDBNull(T.County.LONG, GetNullableDouble, null), 
                                    LongName = reader.GetDefaultIfDBNull(T.County.CountyNameLong, GetString, null),
                                    MediumName = reader.GetDefaultIfDBNull(T.County.CountyNameMedium, GetString, null), 
                                    ShortName = reader.GetDefaultIfDBNull(T.County.CountyNameShort, GetString, null), 
                                    State = new State(reader.GetDefaultIfDBNull(T.County.StateFIPS, GetString, null)), 
                                    WebLinkText = reader.GetDefaultIfDBNull(T.County.WEBLINKTEXT, GetString, null) });
                                }

                        reader.NextResult();
                        
                        while (reader.Read())
                        {
                            agency.Locations.Add(
                                new AgencyLocation
                                {
                                    Id = reader.GetDefaultIfDBNull(T.AgencyLocation.AgencyLocationID, GetNullableInt32, null),
                                    LocationName = reader.GetDefaultIfDBNull(T.AgencyLocation.LocationName, GetString, null),
                                    ContactFirstName = reader.GetDefaultIfDBNull(T.AgencyLocation.ContactFirstName, GetString, null),
                                    ContactMiddleName = reader.GetDefaultIfDBNull(T.AgencyLocation.ContactMiddleName, GetString, null),
                                    ContactLastName = reader.GetDefaultIfDBNull(T.AgencyLocation.ContactLastName, GetString, null),
                                    HoursOfOperation = reader.GetDefaultIfDBNull(T.AgencyLocation.HoursOfOperation, GetString, null),
                                    PrimaryPhone = reader.GetDefaultIfDBNull(T.AgencyLocation.PrimaryPhone, GetString, null),
                                    PrimaryEmail = reader.GetDefaultIfDBNull(T.AgencyLocation.PrimaryEmail, GetString, null),
                                    SecondaryPhone = reader.GetDefaultIfDBNull(T.AgencyLocation.SecondaryPhone, GetString, null),
                                    SecondaryEmail = reader.GetDefaultIfDBNull(T.AgencyLocation.SecondaryEmail, GetString, null),
                                    TollFreePhone = reader.GetDefaultIfDBNull(T.AgencyLocation.TollFreePhone, GetString, null),
                                    TDD = reader.GetDefaultIfDBNull(T.AgencyLocation.TDD, GetString, null),
                                    TollFreeTDD = reader.GetDefaultIfDBNull(T.AgencyLocation.TollFreeTDD, GetString, null),
                                    Fax = reader.GetDefaultIfDBNull(T.AgencyLocation.Fax, GetString, null),
                                    IsMainOffice = reader.GetDefaultIfDBNull(T.AgencyLocation.IsMainOffice, GetBool, false),
                                    //Added by Lavanya
                                    AvailableLanguages = reader.GetDefaultIfDBNull(T.AgencyLocation.AvailableLanguages, GetString, null),
                                    HideAgencyFromSearch = reader.GetDefaultIfDBNull(T.AgencyLocation.HideAgencyFromSearch, GetBool, false),
                                    //end
                                    PhysicalAddress = new AgencyAddress
                                    {
                                        Id = reader.GetDefaultIfDBNull(T.AgencyLocation.PhysicalAddressID, GetNullableInt32, null),
                                        County = new County { Code = reader.GetDefaultIfDBNull(T.AgencyAddress.PhysicalCountyFIPS, GetString, null),
                                        ShortName = reader.GetDefaultIfDBNull(T.AgencyAddress.PhysicalCountyShortName, GetString, null)},
                                        Address1 = reader.GetDefaultIfDBNull(T.AgencyAddress.PhysicalAddress1, GetString, null),
                                        Address2 = reader.GetDefaultIfDBNull(T.AgencyAddress.PhysicalAddress2, GetString, null),
                                        City = reader.GetDefaultIfDBNull(T.AgencyAddress.PhysicalCity, GetString, null),
                                        State = new State(reader.GetDefaultIfDBNull(T.AgencyAddress.PhysicalStateFIPS, GetString, null)),
                                        Zip = reader.GetDefaultIfDBNull(T.AgencyAddress.PhysicalZip, GetString, null),
                                        //Added by Lavanya
                                        Longitude = reader.GetDefaultIfDBNull(T.AgencyAddress.Longitude,GetNullableDouble, null),
                                        Latitude = reader.GetDefaultIfDBNull(T.AgencyAddress.Latitude,GetNullableDouble, null)
                                        //
                                    },
                                    MailingAddress = new AgencyAddress
                                    {
                                        Id = reader.GetDefaultIfDBNull(T.AgencyLocation.MailingAddressID, GetNullableInt32, null),
                                        Address1 = reader.GetDefaultIfDBNull(T.AgencyAddress.MailingAddress1, GetString, null),
                                        Address2 = reader.GetDefaultIfDBNull(T.AgencyAddress.MailingAddress2, GetString, null),
                                        City = reader.GetDefaultIfDBNull(T.AgencyAddress.MailingCity, GetString, null),
                                        State = new State(reader.GetDefaultIfDBNull(T.AgencyAddress.MailingStateFIPS, GetString, null)),
                                        Zip = reader.GetDefaultIfDBNull(T.AgencyAddress.MailingZip, GetString, null)
                                    },
                                });
                        }
                    }
                }
            }
            
            return agency;
        }
       
        public IEnumerable<Agency> GetAgency(string stateFIPS, string countyFIPS, string zip)
        {
            Agency agency = null;

            var agencies = new List<Agency>();

            using (var command = database.GetStoredProcCommand("dbo.GetAgencyForStateCountyZip"))
            {
                database.AddInParameter(command, SP.GetAgenciesForStateCountyZipLookup.StateFIPS, DbType.StringFixedLength, stateFIPS);
                database.AddInParameter(command, SP.GetAgenciesForStateCountyZipLookup.CountyFIPS, DbType.StringFixedLength, countyFIPS);
                database.AddInParameter(command, SP.GetAgenciesForStateCountyZipLookup.Zip, DbType.StringFixedLength, zip);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        using (var command1 = database.GetStoredProcCommand("dbo.GetAgencyForUser"))
                        {
                            database.AddInParameter(command1, SP.GetAgency.AgencyId, DbType.Int32, reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetNullableInt32, null));
                            database.AddInParameter(command1, SP.GetAgency.IncludeAllLocations, DbType.Boolean, true);

                            using (var reader1 = database.ExecuteReader(command1))
                            {
                                while (reader1.Read())
                                {
                                    agency = new Agency
                                    {
                                        Id = reader1.GetDefaultIfDBNull(T.Agency.AgencyId, GetNullableInt32, null),
                                        Name = reader1.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, null),
                                        Code = reader1.GetDefaultIfDBNull(T.Agency.AgencyCode, GetString, null),
                                        Locations = new List<AgencyLocation>(),
                                        ServiceAreas = new List<County>(),
                                    };
                                }
                                reader1.NextResult();

                                while (reader1.Read())
                                {
                                    agency.ServiceAreas.Add(
                                        new County
                                        {
                                            Code = reader1.GetDefaultIfDBNull(T.County.CountyFIPS, GetString, null),
                                            CBSACode = reader1.GetDefaultIfDBNull(T.County.CBSACODE, GetString, null),
                                            CBSALSAD = reader1.GetDefaultIfDBNull(T.County.CBSALSAD, GetString, null),
                                            CBSATitle = reader1.GetDefaultIfDBNull(T.County.CBSATITLE, GetString, null),
                                            CreatedDate = reader1.GetDefaultIfDBNull(T.County.CreatedDate, GetNullableDateTime, null),
                                            LastUpdatedBy = reader1.GetDefaultIfDBNull(T.County.LastUpdatedBy, GetNullableInt32, null),
                                            LastUpdatedDate = reader1.GetDefaultIfDBNull(T.County.LastUpdatedDate, GetNullableDateTime, null),
                                            Latitude = reader1.GetDefaultIfDBNull(T.County.LAT, GetNullableDouble, null),
                                            Longitude = reader1.GetDefaultIfDBNull(T.County.LONG, GetNullableDouble, null),
                                            LongName = reader1.GetDefaultIfDBNull(T.County.CountyNameLong, GetString, null),
                                            MediumName = reader1.GetDefaultIfDBNull(T.County.CountyNameMedium, GetString, null),
                                            ShortName = reader1.GetDefaultIfDBNull(T.County.CountyNameShort, GetString, null),
                                            WebLinkText = reader1.GetDefaultIfDBNull(T.County.WEBLINKTEXT, GetString, null),
                                            State = new State(reader1.GetDefaultIfDBNull(T.County.StateFIPS, GetString, null))
                                        });
                                }
                                reader1.NextResult();

                                while (reader1.Read())
                                {
                                    agency.Locations.Add(
                                        new AgencyLocation
                                        {

                                            Id = reader1.GetDefaultIfDBNull(T.AgencyLocation.AgencyLocationID, GetNullableInt32, null),
                                            LocationName = reader1.GetDefaultIfDBNull(T.AgencyLocation.LocationName, GetString, null),
                                            HoursOfOperation = reader1.GetDefaultIfDBNull(T.AgencyLocation.HoursOfOperation, GetString, null),
                                            PrimaryPhone = reader1.GetDefaultIfDBNull(T.AgencyLocation.PrimaryPhone, GetString, null),
                                            PrimaryEmail = reader1.GetDefaultIfDBNull(T.AgencyLocation.PrimaryEmail, GetString, null),
                                            IsMainOffice = reader1.GetDefaultIfDBNull(T.AgencyLocation.IsMainOffice, GetBool, false),
                                            PhysicalAddress = new AgencyAddress
                                            {
                                                Id = reader1.GetDefaultIfDBNull(T.AgencyLocation.PhysicalAddressID, GetNullableInt32, null),
                                                County = new County
                                                {
                                                    Code = reader1.GetDefaultIfDBNull(T.AgencyAddress.PhysicalCountyFIPS, GetString, null),
                                                    ShortName = reader1.GetDefaultIfDBNull(T.AgencyAddress.PhysicalCountyShortName, GetString, null)
                                                },
                                                Address1 = reader1.GetDefaultIfDBNull(T.AgencyAddress.PhysicalAddress1, GetString, null),
                                                Address2 = reader1.GetDefaultIfDBNull(T.AgencyAddress.PhysicalAddress2, GetString, null),
                                                City = reader1.GetDefaultIfDBNull(T.AgencyAddress.PhysicalCity, GetString, null),
                                                State = new State(reader1.GetDefaultIfDBNull(T.AgencyAddress.PhysicalStateFIPS, GetString, null)),
                                                Zip = reader1.GetDefaultIfDBNull(T.AgencyAddress.PhysicalZip, GetString, null)
                                            },
                                        });
                                }
                               
                            }
                        }
                        agencies.Add(agency);
                    }
                }
            }
            return agencies;
        }
                
        public AgencyLocation GetAgencyLocation(int id)
        {
            AgencyLocation agencyLocation = null;

            using (var command = database.GetStoredProcCommand("dbo.GetAgencyLocation"))
            {
                database.AddInParameter(command, SP.GetAgencyLocation.AgencyLocationId, DbType.Int32, id);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        agencyLocation = new AgencyLocation
                                {
                                    AgencyId = reader.GetDefaultIfDBNull(T.AgencyLocation.AgencyID, GetNullableInt32, null),
                                    Id = reader.GetDefaultIfDBNull(T.AgencyLocation.AgencyLocationID, GetNullableInt32, null),
                                    LocationName = reader.GetDefaultIfDBNull(T.AgencyLocation.LocationName, GetString, null),
                                    ContactFirstName = reader.GetDefaultIfDBNull(T.AgencyLocation.ContactFirstName, GetString, null),
                                    ContactMiddleName = reader.GetDefaultIfDBNull(T.AgencyLocation.ContactMiddleName, GetString, null),
                                    ContactLastName = reader.GetDefaultIfDBNull(T.AgencyLocation.ContactLastName, GetString, null),
                                    ContactTitle = reader.GetDefaultIfDBNull(T.AgencyLocation.ContactTitle, GetString, null),
                                    Comments = reader.GetDefaultIfDBNull(T.AgencyLocation.Comments, GetString, null),
                                    HoursOfOperation = reader.GetDefaultIfDBNull(T.AgencyLocation.HoursOfOperation, GetString, null),
                                    PrimaryPhone = reader.GetDefaultIfDBNull(T.AgencyLocation.PrimaryPhone, GetString, null),
                                    PrimaryEmail = reader.GetDefaultIfDBNull(T.AgencyLocation.PrimaryEmail, GetString, null),
                                    SecondaryPhone = reader.GetDefaultIfDBNull(T.AgencyLocation.SecondaryPhone, GetString, null),
                                    SecondaryEmail = reader.GetDefaultIfDBNull(T.AgencyLocation.SecondaryEmail, GetString, null),
                                    TollFreePhone = reader.GetDefaultIfDBNull(T.AgencyLocation.TollFreePhone, GetString, null),
                                    TDD = reader.GetDefaultIfDBNull(T.AgencyLocation.TDD, GetString, null),
                                    TollFreeTDD = reader.GetDefaultIfDBNull(T.AgencyLocation.TollFreeTDD, GetString, null),
                                    Fax = reader.GetDefaultIfDBNull(T.AgencyLocation.Fax, GetString, null),
                                    IsActive = reader.GetDefaultIfDBNull(T.AgencyLocation.IsActive, GetBool, false),
                                    IsMainOffice = reader.GetDefaultIfDBNull(T.AgencyLocation.IsMainOffice, GetBool, false),
                                    //Added by Lavanya
                                    AvailableLanguages = reader.GetDefaultIfDBNull(T.AgencyLocation.AvailableLanguages, GetString, null),
                                    HideAgencyFromSearch = reader.GetDefaultIfDBNull(T.AgencyLocation.HideAgencyFromSearch, GetBool, false),
                                    //end
                                    PhysicalAddress = new AgencyAddress
                                    {
                                        Id = reader.GetDefaultIfDBNull(T.AgencyLocation.PhysicalAddressID, GetNullableInt32, null),
                                        County = new County
                                        {
                                            Code = reader.GetDefaultIfDBNull(T.AgencyAddress.PhysicalCountyFIPS, GetString, null),
                                            ShortName = reader.GetDefaultIfDBNull(T.AgencyAddress.PhysicalCountyShortName, GetString, null)
                                        },
                                        Address1 = reader.GetDefaultIfDBNull(T.AgencyAddress.PhysicalAddress1, GetString, null),
                                        Address2 = reader.GetDefaultIfDBNull(T.AgencyAddress.PhysicalAddress2, GetString, null),
                                        City = reader.GetDefaultIfDBNull(T.AgencyAddress.PhysicalCity, GetString, null),
                                        State = new State(reader.GetDefaultIfDBNull(T.AgencyAddress.PhysicalStateFIPS, GetString, null)),
                                        Zip = reader.GetDefaultIfDBNull(T.AgencyAddress.PhysicalZip, GetString, null)
                                    },
                                    MailingAddress = new AgencyAddress
                                    {
                                        Id = reader.GetDefaultIfDBNull(T.AgencyLocation.MailingAddressID, GetNullableInt32, null),
                                        Address1 = reader.GetDefaultIfDBNull(T.AgencyAddress.MailingAddress1, GetString, null),
                                        Address2 = reader.GetDefaultIfDBNull(T.AgencyAddress.MailingAddress2, GetString, null),
                                        City = reader.GetDefaultIfDBNull(T.AgencyAddress.MailingCity, GetString, null),
                                        State = new State(reader.GetDefaultIfDBNull(T.AgencyAddress.MailingStateFIPS, GetString, null)),
                                        Zip = reader.GetDefaultIfDBNull(T.AgencyAddress.MailingZip, GetString, null)
                                    }
                                };
                    }
                }
            }

            return agencyLocation;
        }

        public IEnumerable<User> GetAgencyUsers(int id, Descriptor descriptor, bool includeInactive)
        {
            var users = new List<User>();

            using (var command = database.GetStoredProcCommand((includeInactive) ? "dbo.GetAllAgencyUsersByDescriptor" : "dbo.GetAgencyUsersByDescriptor"))
            {
                database.AddInParameter(command, SP.GetAgencyUsersByDescriptor.AgencyId, DbType.Int32, id);
                database.AddInParameter(command, SP.GetAgencyUsersByDescriptor.DescriptorId, DbType.Int32, Convert.ToInt32(descriptor));

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            CreatedBy = reader.GetDefaultIfDBNull(T.User.CreatedBy, GetNullableInt32, null),
                            CreatedDate = reader.GetDefaultIfDBNull(T.User.CreatedDate, GetNullableDateTime, null),
                            LastUpdatedBy = reader.GetDefaultIfDBNull(T.User.LastUpdatedBy, GetNullableInt32, null),
                            LastUpdatedDate = reader.GetDefaultIfDBNull(T.User.LastUpdatedDate, GetNullableDateTime, null),
                            ActiveInactiveDate = reader.GetDefaultIfDBNull(T.User.ActiveInactiveDate, GetNullableDateTime, null),
                            IsActive = reader.GetDefaultIfDBNull(T.User.IsActive, GetBool, false),
                            UserAccount = new UserAccount 
                                            {
                                                UserId = reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                RegionName = reader.GetDefaultIfDBNull(T.User.RegionName, GetString, null),
                                                CounselingCounty = reader.GetDefaultIfDBNull(T.User.CountyOfCounselingCounty, GetString, null),
                                                CounselingLocation = reader.GetDefaultIfDBNull(T.User.CounselingLocation, GetString, null),
                                                IsActive = reader.GetDefaultIfDBNull(T.User.IsActive, GetBool, false),
                                                IsAdmin = reader.GetDefaultIfDBNull(T.User.IsAdmin, GetBool, false),
                                                PrimaryEmail = reader.GetDefaultIfDBNull(T.User.PrimaryEmail, GetString, null),
                                                ScopeId = reader.GetDefaultIfDBNull<short>(T.User.ScopeID, GetInt16, 0),
                                                PrimaryDescriptor = Convert.ToInt32(descriptor),
                                                StateFIPS = reader.GetDefaultIfDBNull(T.User.StateFIPS, GetString, null),
                                            },
                            UserProfile = new UserProfile
                                            {
                                              FirstName = reader.GetDefaultIfDBNull(T.User.FirstName, GetString, null),
                                              Honorifics = reader.GetDefaultIfDBNull(T.User.Honorifics, GetString, null),
                                              LastName = reader.GetDefaultIfDBNull(T.User.LastName, GetString, null),
                                              MiddleName = reader.GetDefaultIfDBNull(T.User.MiddleName, GetString, null),
                                              NickName = reader.GetDefaultIfDBNull(T.User.Nickname, GetString, null),
                                              PrimaryPhone = reader.GetDefaultIfDBNull(T.User.PrimaryPhone, GetString, null),
                                              SecondaryEmail = reader.GetDefaultIfDBNull(T.User.SecondaryEmail, GetString, null),
                                              SecondaryPhone = reader.GetDefaultIfDBNull(T.User.SecondaryPhone, GetString, null),
                                              Suffix = reader.GetDefaultIfDBNull(T.User.Suffix, GetString, null),
                                              UserId = reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                            }
                        });
                    }
                }
            }

            return users;
        }

        public IEnumerable<Agency> GetSubStateRegionAgencies(int userId)
        {
            return GetAgenciesForUser(userId, "dbo.GetSubStateRegionAgenciesForUser");
        }

        public IEnumerable<Agency> GetStateAgencies(int userId)
        {
            return GetAgenciesForUser(userId, "dbo.GetStateAgenciesForUser");
        }

        public IEnumerable<KeyValuePair<int, string>> GetAgencies(int userId, bool showAllAgencyUser)
        {
            //sammit for reports/search pass showAllAgencyUser= true (agency user active or inactive)
            //for data entry pam/cc showAllAgencyUser= false (only active agency user)
            return GetAgenciesForUserNoMapping(userId, showAllAgencyUser);
        }

        public IEnumerable<Agency> GetAgencies()
        {
            var agencies = new List<Agency>();

            using (var command = database.GetStoredProcCommand("dbo.GetAgencies"))
            {
                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        agencies.Add(new Agency
                        {
                            Id = reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetNullableInt32, null),
                            Name = reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, null),
                            Code = reader.GetDefaultIfDBNull(T.Agency.AgencyCode, GetString, null),
                            Type = (AgencyType)reader.GetDefaultIfDBNull(T.Agency.AgencyTypeID, GetNullableInt16, null),
                            SponsorFirstName = reader.GetDefaultIfDBNull(T.Agency.SponsorFirstName, GetString, null),
                            SponsorMiddleName = reader.GetDefaultIfDBNull(T.Agency.SponsorMiddleName, GetString, null),
                            SponsorLastName = reader.GetDefaultIfDBNull(T.Agency.SponsorLastName, GetString, null),
                            SponsorTitle = reader.GetDefaultIfDBNull(T.Agency.SponsorTitle, GetString, null),
                            URL = reader.GetDefaultIfDBNull(T.Agency.URL, GetString, null),
                            Comments = reader.GetDefaultIfDBNull(T.Agency.Comments, GetString, null),
                            CreatedBy = reader.GetDefaultIfDBNull(T.Agency.CreatedBy, GetNullableInt32, null),
                            CreatedDate = reader.GetDefaultIfDBNull(T.Agency.CreatedDate, GetNullableDateTime, null),
                            LastUpdatedBy = reader.GetDefaultIfDBNull(T.Agency.LastUpdatedBy, GetNullableInt32, null),
                            LastUpdatedDate = reader.GetDefaultIfDBNull(T.Agency.LastUpdatedDate, GetNullableDateTime, null),
                            ActiveInactiveDate = reader.GetDefaultIfDBNull(T.Agency.ActiveInactiveDate, GetNullableDateTime, null),
                            IsActive = reader.GetDefaultIfDBNull(T.Agency.IsActive, GetBool, false)
                        });
                    }
                }
            }

            return agencies;
        }

        private IEnumerable<Agency> GetAgenciesForUser(int userId, string proc)
        {
            var agencies = new List<Agency>();

            using (var command = database.GetStoredProcCommand(proc))
            {
                database.AddInParameter(command, SP.GetAgenciesForUser.UserId, DbType.Int32, userId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        agencies.Add(new Agency
                         {
                             Id = reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetNullableInt32, null),
                             Name = reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, null),
                             Code = reader.GetDefaultIfDBNull(T.Agency.AgencyCode, GetString, null),
                             Type = (AgencyType)reader.GetDefaultIfDBNull(T.Agency.AgencyTypeID, GetNullableInt16, null),
                             SponsorFirstName = reader.GetDefaultIfDBNull(T.Agency.SponsorFirstName, GetString, null),
                             SponsorMiddleName = reader.GetDefaultIfDBNull(T.Agency.SponsorMiddleName, GetString, null),
                             SponsorLastName = reader.GetDefaultIfDBNull(T.Agency.SponsorLastName, GetString, null),
                             SponsorTitle = reader.GetDefaultIfDBNull(T.Agency.SponsorTitle, GetString, null),
                             URL = reader.GetDefaultIfDBNull(T.Agency.URL, GetString, null),
                             Comments = reader.GetDefaultIfDBNull(T.Agency.Comments, GetString, null),
                             CreatedBy = reader.GetDefaultIfDBNull(T.Agency.CreatedBy, GetNullableInt32, null),
                             CreatedDate = reader.GetDefaultIfDBNull(T.Agency.CreatedDate, GetNullableDateTime, null),
                             LastUpdatedBy = reader.GetDefaultIfDBNull(T.Agency.LastUpdatedBy, GetNullableInt32, null),
                             LastUpdatedDate = reader.GetDefaultIfDBNull(T.Agency.LastUpdatedDate, GetNullableDateTime, null),
                             ActiveInactiveDate = reader.GetDefaultIfDBNull(T.Agency.ActiveInactiveDate, GetNullableDateTime, null),
                             IsActive = reader.GetDefaultIfDBNull(T.Agency.IsActive, GetBool, false)
                         });
                    }
                }
            }

            return agencies;
        }

        private IEnumerable<KeyValuePair<int, string>> GetAgenciesForUserNoMapping(int userId, bool showAllAgencyUser)
        {
            List<KeyValuePair<int, string>> agencies = new List<KeyValuePair<int, string>>();


            using (var command = database.GetStoredProcCommand("dbo.GetAgenciesForUser"))
            {
                database.AddInParameter(command, SP.GetAgenciesForUser.UserId, DbType.Int32, userId);
                database.AddInParameter(command, SP.GetAgenciesForUser.ShowAllAgencyUser, DbType.Boolean, showAllAgencyUser);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        agencies.Add(new KeyValuePair<int, string>
                        (
                             reader.GetInt32(reader.GetOrdinal(T.Agency.AgencyId)),
                             reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, null)
                        ));
                    }
                }
            }

            return agencies;
        }

        private IEnumerable<Agency> GetAgenciesForUser(int userId, bool showAllAgencyUser, string proc)
        {
            var agencies = new List<Agency>();

            using (var command = database.GetStoredProcCommand(proc))
            {
                database.AddInParameter(command, SP.GetAgenciesForUser.UserId, DbType.Int32, userId);
                database.AddInParameter(command, SP.GetAgenciesForUser.ShowAllAgencyUser, DbType.Boolean, showAllAgencyUser);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        agencies.Add(new Agency
                        {
                            Id = reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetNullableInt32, null),
                            Name = reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, null),
                            Code = reader.GetDefaultIfDBNull(T.Agency.AgencyCode, GetString, null),
                            Type = (AgencyType)reader.GetDefaultIfDBNull(T.Agency.AgencyTypeID, GetNullableInt16, null),
                            SponsorFirstName = reader.GetDefaultIfDBNull(T.Agency.SponsorFirstName, GetString, null),
                            SponsorMiddleName = reader.GetDefaultIfDBNull(T.Agency.SponsorMiddleName, GetString, null),
                            SponsorLastName = reader.GetDefaultIfDBNull(T.Agency.SponsorLastName, GetString, null),
                            SponsorTitle = reader.GetDefaultIfDBNull(T.Agency.SponsorTitle, GetString, null),
                            URL = reader.GetDefaultIfDBNull(T.Agency.URL, GetString, null),
                            Comments = reader.GetDefaultIfDBNull(T.Agency.Comments, GetString, null),
                            CreatedBy = reader.GetDefaultIfDBNull(T.Agency.CreatedBy, GetNullableInt32, null),
                            CreatedDate = reader.GetDefaultIfDBNull(T.Agency.CreatedDate, GetNullableDateTime, null),
                            LastUpdatedBy = reader.GetDefaultIfDBNull(T.Agency.LastUpdatedBy, GetNullableInt32, null),
                            LastUpdatedDate = reader.GetDefaultIfDBNull(T.Agency.LastUpdatedDate, GetNullableDateTime, null),
                            ActiveInactiveDate = reader.GetDefaultIfDBNull(T.Agency.ActiveInactiveDate, GetNullableDateTime, null),
                            IsActive = reader.GetDefaultIfDBNull(T.Agency.IsActive, GetBool, false)
                        });
                    }
                }
            }

            return agencies;
        }
        public int CreateAgencyLocation(AgencyLocation agencyLocation)
        {
           if (agencyLocation == null)
                throw new ArgumentNullException("agencyLocation");

           using (var command = database.GetStoredProcCommand("dbo.CreateAgencyLocation"))
           {
               database.AddInParameter(command, SP.CreateAgencyLocation.AgencyId, DbType.Int32, agencyLocation.AgencyId);
               database.AddInParameter(command, SP.CreateAgencyLocation.LocationName, DbType.String, agencyLocation.LocationName);
               database.AddInParameter(command, SP.CreateAgencyLocation.PhysicalAddress1, DbType.String, agencyLocation.PhysicalAddress.Address1);
               database.AddInParameter(command, SP.CreateAgencyLocation.PhysicalAddress2, DbType.String, agencyLocation.PhysicalAddress.Address2);
               database.AddInParameter(command, SP.CreateAgencyLocation.PhysicalCity, DbType.String, agencyLocation.PhysicalAddress.City);
               database.AddInParameter(command, SP.CreateAgencyLocation.PhysicalZip, DbType.String, agencyLocation.PhysicalAddress.Zip);
               database.AddInParameter(command, SP.CreateAgencyLocation.PhysicalCountyFIPS, DbType.String, agencyLocation.PhysicalAddress.County.Code);
               database.AddInParameter(command, SP.CreateAgencyLocation.PhysicalStateFIPS, DbType.String, agencyLocation.PhysicalAddress.State.Value.Code);
               database.AddInParameter(command, SP.CreateAgencyLocation.MailingAddress1, DbType.String, agencyLocation.MailingAddress.Address1);
               database.AddInParameter(command, SP.CreateAgencyLocation.MailingAddress2, DbType.String, agencyLocation.MailingAddress.Address2);
               database.AddInParameter(command, SP.CreateAgencyLocation.MailingCity, DbType.String, agencyLocation.MailingAddress.City);
               database.AddInParameter(command, SP.CreateAgencyLocation.MailingZip, DbType.String, agencyLocation.MailingAddress.Zip);
               database.AddInParameter(command, SP.CreateAgencyLocation.MailingStateFIPS, DbType.String, agencyLocation.MailingAddress.State.Value.Code);
               database.AddInParameter(command, SP.CreateAgencyLocation.ContactFirstName, DbType.String, agencyLocation.ContactFirstName);
               database.AddInParameter(command, SP.CreateAgencyLocation.ContactMiddleName, DbType.String, agencyLocation.ContactMiddleName);
               database.AddInParameter(command, SP.CreateAgencyLocation.ContactLastName, DbType.String, agencyLocation.ContactLastName);
               database.AddInParameter(command, SP.CreateAgencyLocation.ContactTitle, DbType.String, agencyLocation.ContactTitle);
               database.AddInParameter(command, SP.CreateAgencyLocation.Comments, DbType.String, agencyLocation.Comments);
               database.AddInParameter(command, SP.CreateAgencyLocation.HoursOfOperation, DbType.String, agencyLocation.HoursOfOperation);
               database.AddInParameter(command, SP.CreateAgencyLocation.PrimaryPhone, DbType.String, agencyLocation.PrimaryPhone);
               database.AddInParameter(command, SP.CreateAgencyLocation.SecondaryPhone, DbType.String, agencyLocation.SecondaryPhone);
               database.AddInParameter(command, SP.CreateAgencyLocation.TollFreePhone, DbType.String, agencyLocation.TollFreePhone);
               database.AddInParameter(command, SP.CreateAgencyLocation.TDD, DbType.String, agencyLocation.TDD);
               database.AddInParameter(command, SP.CreateAgencyLocation.TollFreeTDD, DbType.String, agencyLocation.TollFreeTDD);
               database.AddInParameter(command, SP.CreateAgencyLocation.Fax, DbType.String, agencyLocation.Fax);
               database.AddInParameter(command, SP.CreateAgencyLocation.PrimaryEmail, DbType.String, agencyLocation.PrimaryEmail);
               database.AddInParameter(command, SP.CreateAgencyLocation.SecondaryEmail, DbType.String, agencyLocation.SecondaryEmail);
               database.AddInParameter(command, SP.CreateAgencyLocation.CreatedBy, DbType.Int32, agencyLocation.CreatedBy);
               database.AddInParameter(command, SP.CreateAgencyLocation.IsMainOffice, DbType.Boolean, false);
               database.AddOutParameter(command, SP.CreateAgencyLocation.AgencyLocationId, DbType.Int32, 6);
               //Added by Lavanya
               database.AddInParameter(command, SP.CreateAgencyLocation.Longitude, DbType.Double, agencyLocation.PhysicalAddress.Longitude);
               database.AddInParameter(command, SP.CreateAgencyLocation.Latitude, DbType.Double, agencyLocation.PhysicalAddress.Latitude);
               database.AddInParameter(command, SP.CreateAgencyLocation.AvailableLanguages, DbType.String, agencyLocation.AvailableLanguages);
               database.AddInParameter(command, SP.CreateAgencyLocation.HideAgencyFromSearch, DbType.Boolean, agencyLocation.HideAgencyFromSearch);
               //end

               database.ExecuteNonQuery(command);

               return (int)database.GetParameterValue(command, SP.CreateAgencyLocation.AgencyLocationId);
           }
        }

        public void UpdateAgencyLocation(AgencyLocation agencyLocation)
        {
            if (agencyLocation == null)
                throw new ArgumentNullException("agencyLocation");

            using (var command = database.GetStoredProcCommand("dbo.UpdateAgencyLocation"))
            {
                database.AddInParameter(command, SP.UpdateAgencyLocation.AgencyLocationId, DbType.Int32, agencyLocation.Id);
                database.AddInParameter(command, SP.UpdateAgencyLocation.LocationName, DbType.String, agencyLocation.LocationName);
                database.AddInParameter(command, SP.UpdateAgencyLocation.PhysicalAddress1, DbType.String, agencyLocation.PhysicalAddress.Address1);
                database.AddInParameter(command, SP.UpdateAgencyLocation.PhysicalAddress2, DbType.String, agencyLocation.PhysicalAddress.Address2);
                database.AddInParameter(command, SP.UpdateAgencyLocation.PhysicalCity, DbType.String, agencyLocation.PhysicalAddress.City);
                database.AddInParameter(command, SP.UpdateAgencyLocation.PhysicalZip, DbType.String, agencyLocation.PhysicalAddress.Zip);
                database.AddInParameter(command, SP.UpdateAgencyLocation.PhysicalCountyFIPS, DbType.String, agencyLocation.PhysicalAddress.County.Code);
                database.AddInParameter(command, SP.UpdateAgencyLocation.PhysicalStateFIPS, DbType.String, agencyLocation.PhysicalAddress.State.Value.Code);
                database.AddInParameter(command, SP.UpdateAgencyLocation.MailingAddress1, DbType.String, agencyLocation.MailingAddress.Address1);
                database.AddInParameter(command, SP.UpdateAgencyLocation.MailingAddress2, DbType.String, agencyLocation.MailingAddress.Address2);
                database.AddInParameter(command, SP.UpdateAgencyLocation.MailingCity, DbType.String, agencyLocation.MailingAddress.City);
                database.AddInParameter(command, SP.UpdateAgencyLocation.MailingZip, DbType.String, agencyLocation.MailingAddress.Zip);
                database.AddInParameter(command, SP.UpdateAgencyLocation.MailingStateFIPS, DbType.String, agencyLocation.MailingAddress.State.Value.Code);
                database.AddInParameter(command, SP.UpdateAgencyLocation.ContactFirstName, DbType.String, agencyLocation.ContactFirstName);
                database.AddInParameter(command, SP.UpdateAgencyLocation.ContactMiddleName, DbType.String, agencyLocation.ContactMiddleName);
                database.AddInParameter(command, SP.UpdateAgencyLocation.ContactLastName, DbType.String, agencyLocation.ContactLastName);
                database.AddInParameter(command, SP.UpdateAgencyLocation.ContactTitle, DbType.String, agencyLocation.ContactTitle);
                database.AddInParameter(command, SP.UpdateAgencyLocation.Comments, DbType.String, agencyLocation.Comments);
                database.AddInParameter(command, SP.UpdateAgencyLocation.HoursOfOperation, DbType.String, agencyLocation.HoursOfOperation);
                database.AddInParameter(command, SP.UpdateAgencyLocation.PrimaryPhone, DbType.String, agencyLocation.PrimaryPhone);
                database.AddInParameter(command, SP.UpdateAgencyLocation.SecondaryPhone, DbType.String, agencyLocation.SecondaryPhone);
                database.AddInParameter(command, SP.UpdateAgencyLocation.TollFreePhone, DbType.String, agencyLocation.TollFreePhone);
                database.AddInParameter(command, SP.UpdateAgencyLocation.TDD, DbType.String, agencyLocation.TDD);
                database.AddInParameter(command, SP.UpdateAgencyLocation.TollFreeTDD, DbType.String, agencyLocation.TollFreeTDD);
                database.AddInParameter(command, SP.UpdateAgencyLocation.Fax, DbType.String, agencyLocation.Fax);
                database.AddInParameter(command, SP.UpdateAgencyLocation.PrimaryEmail, DbType.String, agencyLocation.PrimaryEmail);
                database.AddInParameter(command, SP.UpdateAgencyLocation.SecondaryEmail, DbType.String, agencyLocation.SecondaryEmail);
                database.AddInParameter(command, SP.UpdateAgencyLocation.LastUpdatedBy, DbType.Int32, agencyLocation.LastUpdatedBy);
                database.AddInParameter(command, SP.UpdateAgencyLocation.IsMainOffice, DbType.Boolean, false);

                //Added by Lavanya
                database.AddInParameter(command, SP.CreateAgencyLocation.Longitude, DbType.Double, agencyLocation.PhysicalAddress.Longitude);
                database.AddInParameter(command, SP.CreateAgencyLocation.Latitude, DbType.Double, agencyLocation.PhysicalAddress.Latitude);
                database.AddInParameter(command, SP.CreateAgencyLocation.AvailableLanguages, DbType.String, agencyLocation.AvailableLanguages);
                database.AddInParameter(command, SP.CreateAgencyLocation.HideAgencyFromSearch, DbType.Boolean, agencyLocation.HideAgencyFromSearch);
                //end

                database.ExecuteNonQuery(command);
            }
        }

        public void DeleteAgency(int id)
        {
            using (var command = database.GetStoredProcCommand("dbo.DeleteAgency"))
            {
                database.AddInParameter(command, SP.DeleteAgency.Id, DbType.Int32, id);

                database.ExecuteNonQuery(command);
            }
        }

        public void DeleteAgencyLocation(int id)
        {
            using (var command = database.GetStoredProcCommand("dbo.DeleteAgencyLocation"))
            {
                database.AddInParameter(command, SP.DeleteAgency.Id, DbType.Int32, id);

                database.ExecuteNonQuery(command);
            }
        }

        public int CreateSubStateRegion(SubStateRegion region)
        {
            using (var command = database.GetStoredProcCommand("dbo.CreateSubStateRegion"))
            {
                database.AddInParameter(command, SP.CreateSubStateRegion.Name, DbType.String, region.Name);
                database.AddInParameter(command, SP.CreateSubStateRegion.StateFIPS, DbType.String, region.State.Code);
                database.AddInParameter(command, SP.CreateSubStateRegion.Agencies, DbType.String, string.Join(",", (from agency in region.Agencies select agency.Id.ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateSubStateRegion.CreatedBy, DbType.Int32, region.CreatedBy);
                database.AddOutParameter(command, SP.CreateSubStateRegion.SubStateRegionId, DbType.Int32, 6);

                database.ExecuteNonQuery(command);

                return (int)database.GetParameterValue(command, SP.CreateSubStateRegion.SubStateRegionId);
            }
        }

        public IEnumerable<SubStateRegion> SearchSubStateRegions(State state)
        {
            List<SubStateRegion> subStateRegions = new List<SubStateRegion>();

            using (var command = database.GetStoredProcCommand("dbo.SearchSubStateRegions"))
            {
                database.AddInParameter(command, SP.SearchSubStateRegions.StateFIPS, DbType.String, state.Code);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        subStateRegions.Add(
                            new SubStateRegion {
                                Id = reader.GetDefaultIfDBNull(T.SubStateRegion.SubStateRegionID, GetNullableInt32, null),
                                Name = reader.GetDefaultIfDBNull(T.SubStateRegion.Name, GetString, null),
                                State = new State(reader.GetDefaultIfDBNull(T.SubStateRegion.StateFIPS, GetString, null))
                            });
                    }
                }
            }

            return subStateRegions;
        }
        public IEnumerable<SubStateRegion> GetSubStateRegionsForCCReports(string stateFipsCode, int SubStateRegionType)
        {
            List<SubStateRegion> subStateRegions = new List<SubStateRegion>();

            using (var command = database.GetStoredProcCommand("dbo.GetCCReportSubStateRegionForState"))
            {
                database.AddInParameter(command, SP.SearchSubStateRegions.StateFIPS, DbType.String, stateFipsCode);
                database.AddInParameter(command, SP.SearchSubStateRegions.SubStateRegionType, DbType.Int16, SubStateRegionType);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        subStateRegions.Add(
                            new SubStateRegion
                            {
                                Id = reader.GetDefaultIfDBNull(T.SubStateRegion.SubStateRegionID, GetNullableInt32, null),
                                Name = reader.GetDefaultIfDBNull(T.SubStateRegion.Name, GetString, null)
                            });
                    }
                }
            }

            return subStateRegions;
        }
        public IEnumerable<Agency> GetSubStateAgenciesForSubStateRegion(string stateFipsCode, int RegionID)
        {

            List<Agency> agencies = new List<Agency>();

            using (var command = database.GetStoredProcCommand("dbo.GetSubStateRegionAgencyForRegionId"))
            {
                database.AddInParameter(command, SP.SearchSubStateRegions.StateFIPS, DbType.String, stateFipsCode);
                database.AddInParameter(command, SP.SearchSubStateRegions.SubStateRegionID, DbType.Int32, RegionID);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        agencies.Add(
                            new Agency
                            {
                                Id = reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetNullableInt32, null),
                                Name = reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, null)
                            });
                    }
                }
            }

            return agencies;
        }
        public SubStateRegion GetSubStateRegion(int id)
        {
            SubStateRegion subStateRegion = null;

            using (var command = database.GetStoredProcCommand("dbo.GetSubStateRegion"))
            {
                database.AddInParameter(command, SP.GetSubStateRegion.Id, DbType.Int32, id);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        subStateRegion = new SubStateRegion
                                         {
                                            Id = reader.GetDefaultIfDBNull(T.SubStateRegion.SubStateRegionID, GetNullableInt32, null),
                                            Name = reader.GetDefaultIfDBNull(T.SubStateRegion.Name, GetString, null), 
                                            State = new State(reader.GetDefaultIfDBNull(T.SubStateRegion.StateFIPS, GetString, null)),
                                            CreatedBy = reader.GetDefaultIfDBNull(T.SubStateRegion.CreatedBy, GetNullableInt32, null),
                                            CreatedDate = reader.GetDefaultIfDBNull(T.SubStateRegion.CreatedDate, GetNullableDateTime, null),
                                            LastUpdatedBy = reader.GetDefaultIfDBNull(T.SubStateRegion.LastUpdatedBy, GetNullableInt32, null),
                                            LastUpdatedDate = reader.GetDefaultIfDBNull(T.SubStateRegion.LastUpdatedDate, GetNullableDateTime, null),
                                            ActiveInactiveDate = reader.GetDefaultIfDBNull(T.SubStateRegion.ActiveInactiveDate, GetNullableDateTime, null),
                                            IsActive = reader.GetDefaultIfDBNull(T.SubStateRegion.IsActive, GetBool, false),
                                            Agencies = new List<Agency>()
                                         };

                        reader.NextResult();

                        while (reader.Read())
                        {
                            subStateRegion.Agencies.Add(new Agency
                                                            {
                                                                Id = reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetNullableInt32, null),
                                                                Name = reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, null),
                                                                Code = reader.GetDefaultIfDBNull(T.Agency.AgencyCode, GetString, null),
                                                                Type = (AgencyType)reader.GetDefaultIfDBNull(T.Agency.AgencyTypeID, GetNullableInt16, null),
                                                                SponsorFirstName = reader.GetDefaultIfDBNull(T.Agency.SponsorFirstName, GetString, null),
                                                                SponsorMiddleName = reader.GetDefaultIfDBNull(T.Agency.SponsorMiddleName, GetString, null),
                                                                SponsorLastName = reader.GetDefaultIfDBNull(T.Agency.SponsorLastName, GetString, null),
                                                                SponsorTitle = reader.GetDefaultIfDBNull(T.Agency.SponsorTitle, GetString, null),
                                                                URL = reader.GetDefaultIfDBNull(T.Agency.URL, GetString, null),
                                                                Comments = reader.GetDefaultIfDBNull(T.Agency.Comments, GetString, null),
                                                                CreatedBy = reader.GetDefaultIfDBNull(T.Agency.CreatedBy, GetNullableInt32, null),
                                                                CreatedDate = reader.GetDefaultIfDBNull(T.Agency.CreatedDate, GetNullableDateTime, null),
                                                                LastUpdatedBy = reader.GetDefaultIfDBNull(T.Agency.LastUpdatedBy, GetNullableInt32, null),
                                                                LastUpdatedDate = reader.GetDefaultIfDBNull(T.Agency.LastUpdatedDate, GetNullableDateTime, null),
                                                                ActiveInactiveDate = reader.GetDefaultIfDBNull(T.Agency.ActiveInactiveDate, GetNullableDateTime, null),
                                                                IsActive = reader.GetDefaultIfDBNull(T.Agency.IsActive, GetBool, false)
                                                            });
                        }
                    }
                }
            }

            return subStateRegion;
        }

        public void UpdateSubStateRegion(SubStateRegion subStateRegion)
        {
            if (subStateRegion == null) 
                throw new ArgumentNullException("subStateRegion");

            using (var command = database.GetStoredProcCommand("dbo.UpdateSubStateRegion"))
            {
                database.AddInParameter(command, SP.UpdateSubStateRegion.Id, DbType.Int32, subStateRegion.Id);
                database.AddInParameter(command, SP.UpdateSubStateRegion.Name, DbType.String, subStateRegion.Name);
                database.AddInParameter(command, SP.UpdateSubStateRegion.StateFIPS, DbType.String, subStateRegion.State.Code);
                database.AddInParameter(command, SP.UpdateSubStateRegion.LastUpdatedBy, DbType.Int32, subStateRegion.LastUpdatedBy);
                database.AddInParameter(command, SP.UpdateSubStateRegion.IsActive, DbType.Boolean, subStateRegion.IsActive);
                database.AddInParameter(command, SP.UpdateSubStateRegion.ActiveInactiveDate, DbType.DateTime, subStateRegion.ActiveInactiveDate);
                database.AddInParameter(command, SP.UpdateSubStateRegion.Agencies, DbType.String, string.Join(",", (from agency in subStateRegion.Agencies select agency.Id.ToString()).ToArray()));

                database.ExecuteNonQuery(command);
            }
        }

        public void DeleteSubStateRegion(int id)
        {
            using (var command = database.GetStoredProcCommand("dbo.DeleteSubStateRegion"))
            {
                database.AddInParameter(command, SP.DeleteSubStateRegion.Id, DbType.Int32, id);

                database.ExecuteNonQuery(command);
            }
        }

        public bool DoesAgencyNameExist(string agencyName)
        {
            if (string.IsNullOrEmpty(agencyName))
                throw new ArgumentNullException("agencyName");

            using (var command = database.GetStoredProcCommand("dbo.DoesAgencyNameExist"))
            {
                database.AddInParameter(command, SP.DoesAgencyNameExist.AgencyName, DbType.String, agencyName);

                return Convert.ToBoolean(database.ExecuteScalar(command));
            }
        }

        public bool IsAgencyUserActive(int agencyId, int userId)
        {
            using (var command = database.GetStoredProcCommand("dbo.IsAgencyUserActive"))
            {
                database.AddInParameter(command, SP.IsAgencyUserActive.AgencyId, DbType.Int32, agencyId);
                database.AddInParameter(command, SP.IsAgencyUserActive.UserId, DbType.Int32, userId);

                return Convert.ToBoolean(database.ExecuteScalar(command));
            } 
        }

         public IDictionary<int, IList<int>> GetActiveAgencyDescriptors(int userId)
         {
             IDictionary<int, IList<int>> activeAgencyDescriptors = null;
             int? agencyId = null;
             List<int> descriptors = null;

            using (var command = database.GetStoredProcCommand("dbo.GetActiveAgencyDescriptors"))
            {
                database.AddInParameter(command, SP.GetActiveAgencyDescriptors.UserId, DbType.Int32, userId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        var currentAgencyId = reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetInt32, 0);

                        if (activeAgencyDescriptors == null)
                            activeAgencyDescriptors = new Dictionary<int, IList<int>>();

                        if (!agencyId.HasValue)
                        {
                            agencyId = currentAgencyId;
                            descriptors = new List<int> {reader.GetDefaultIfDBNull(T.Descriptor.DescriptorID, GetInt32, 0)};
                        }
                        else
                        {
                            if (agencyId == currentAgencyId)
                            {
                                descriptors.Add(reader.GetDefaultIfDBNull(T.Descriptor.DescriptorID, GetInt32, 0));
                            }
                            else
                            {
                                activeAgencyDescriptors.Add(agencyId.Value, new List<int>(descriptors));
                                agencyId = currentAgencyId;
                                descriptors = new List<int> {reader.GetDefaultIfDBNull(T.Descriptor.DescriptorID, GetInt32, 0)};
                            }
                        }
                    }

                    if (agencyId.HasValue)
                        activeAgencyDescriptors.Add(agencyId.Value, new List<int>(descriptors));
                }
            }

             return activeAgencyDescriptors;
         }

         public bool IsSuperDataEditor(int userId)
         {
             using (var command = database.GetStoredProcCommand("dbo.IsSuperDataEditorForActiveAgencies"))
             {
                 database.AddInParameter(command, SP.IsSuperDataEditorForActiveAgencies.UserId, DbType.Int32, userId);

                 return Convert.ToBoolean(database.ExecuteScalar(command));
             }
         }

         public IList<int> GetActiveAgencies(int userId)
         {
             var activeAgencies = new List<int>();

             using (var command = database.GetStoredProcCommand("dbo.GetActiveAgencies"))
             {
                 database.AddInParameter(command, SP.GetActiveAgencies.UserId, DbType.Int32, userId);

                 using (var reader = database.ExecuteReader(command))
                 {
                     while (reader.Read())
                     {
                         activeAgencies.Add(reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetInt32, 0));
                     }
                 }
             }

             return activeAgencies;
         }

        public IList<KeyValuePair<int, string>> GetStateAgenciesList(Scope scope, State state, int userId)
        {
            var agencies = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand("dbo.GetStateAgenciesList"))
            {
                database.AddInParameter(command, SP.GetStateAgenciesList.StateFIPS, DbType.String, state.Code);
                database.AddInParameter(command, SP.GetStateAgenciesList.Scope, DbType.Int32, scope);
                database.AddInParameter(command, SP.GetStateAgenciesList.UserId, DbType.Int32, userId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        agencies.Add(new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetInt32, 0),
                          (reader.GetDefaultIfDBNull(T.Agency.IsActive, GetBool, false)) 
                                    ? reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, string.Empty)
                                    : string.Format("{0} [Inactive]", reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, string.Empty))));
                    }
                }
            }

            return agencies;
        }

        //Added by Lavanya
        public DataTable GetAgencyLocationForGeoSearch(int LocationId)
        {
            using (var command = database.GetStoredProcCommand("GetAgencyLocationForGeoSearch"))
            {
                database.AddInParameter(command, "@AgencyLocationId", DbType.Int32, LocationId);
              

                DataSet dsAgency = database.ExecuteDataSet(command);

                return dsAgency.Tables[0];

            }
        }
        //end
               
    }
}
