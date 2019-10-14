using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using T = ShiptalkLogic.Constants.Tables;
using SP = ShiptalkLogic.Constants.StoredProcs;
using ShiptalkLogic.BusinessObjects;

namespace ShiptalkLogic.DataLayer
{

    internal static class LookupDAL
    {
        private static Func<IDataRecord, int, string> GetString = (record, i) => record.GetString(i);
        private static Func<IDataRecord, int, DateTime?> GetDateTime = (record, i) => record.GetDateTime(i);
        private static Func<IDataRecord, int, int?> GetInt32 = (record, i) => record.GetInt32(i);
        private static Func<IDataRecord, int, double?> GetDouble = (record, i) => record.GetDouble(i);
        private static Func<IDataRecord, int, bool> GetBool = (record, i) => record.GetBoolean(i);
        private static Func<IDataRecord, int, int?> GetNullableInt32 = (record, i) => record.GetInt32(i);

        public static List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();

                      
            roles.Add(new Role(9, "Agency User", "Agency Counselor, Presenter, Data Submitter", false, Scope.Agency));
            roles.Add(new Role(8, "Agency Administrator", "Agency Director, and his/her staff who does his/her tasks, staff supervisor", true, Scope.Agency));
            roles.Add(new Role(7, "Sub State Regional User", "Sub State Counselor, Sub state data submitter", false, Scope.SubStateRegion));
            roles.Add(new Role(6, "Sub State Regional Administrator", "Sub state region admin, staff supervisor", true, Scope.SubStateRegion));
            roles.Add(new Role(5, "State User", "State Data submitter, counselor, presenter", false, Scope.State));
            roles.Add(new Role(4, "State Administrator", "State SHIP Director and his/her staff who does his/her tasks, staff supervisor", true, Scope.State));
            roles.Add(new Role(3, "CMS Regional User", "CMS regional staff", false, Scope.CMSRegional));
            roles.Add(new Role(2, "CMS User", "CMS main office staff ", false, Scope.CMS));
           
            //roles.Sort();
            return roles;
        }

        public static IDictionary<int, string> GetSubStateRegionsForState(string StateFIPS)
        {
            IDictionary<int, string> dict = new Dictionary<int, string>();

            //Fill UserProfile here.
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetSubStateRegionsForState.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, @StateFIPS);
                db.AddInParameter(dbCmd, "@IncludeInactive", DbType.Boolean, false);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        //Fill
                        dict.Add(reader.GetInt32(0), reader.GetString(1));
                    }
                }
            }
            return dict;
        }

        public static IDictionary<int, string> GetCMSRegions()
        {
            IDictionary<int, string> dict = new Dictionary<int, string>();

            //Fill UserProfile here.
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetCMSRegions.Description()))
            {
                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        //Fill
                        dict.Add(reader.GetInt32(0), reader.GetString(1));
                    }
                }
            }
            return dict;
        }

        /// <summary>
        /// This is a simplified look up version.
        /// Call this method only you need to know the AgencyID, Name of Agencies in a State
        /// For detailed version, you must refer to Agency related DAL.
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <returns></returns>
        public static IDictionary<int, string> GetAgenciesForStateLookup(string StateFIPS)
        {
            IDictionary<int, string> dict = new Dictionary<int, string>();

            //Fill UserProfile here.
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetAgenciesForStateLookup.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, @StateFIPS);
                db.AddInParameter(dbCmd, "@IncludeInactive", DbType.Boolean, false);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        //Fill
                        dict.Add(reader.GetInt32(0), reader.GetString(1));
                    }
                }
            }
            return dict;
        }

        public static IDictionary<int, string> GetAgenciesForStateLookup(string StateFIPS, bool Inactive)
        {
            IDictionary<int, string> dict = new Dictionary<int, string>();

            //Fill UserProfile here.
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetAgenciesForStateLookup.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, @StateFIPS);
                db.AddInParameter(dbCmd, "@IncludeInactive", DbType.Boolean, Inactive);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        //Fill
                        dict.Add(reader.GetInt32(0), reader.GetString(1));
                    }
                }
            }
            return dict;
        }
        


        public static List<KeyValuePair<int, string>> GetPresentorsForState(string StateFIPS, bool IsActive)
        {
            var PresentorsList = new List<KeyValuePair<int, string>>();
              var database = DatabaseFactory.CreateDatabase();

              using (var command = database.GetStoredProcCommand(StoredProcNames.Lookup.GetPresentorsForState.Description()))
              {
                  database.AddInParameter(command, Constants.StoredProcs.GetPAMPresentersForScope.StateFIPS, DbType.String, StateFIPS);
                  database.AddInParameter(command, Constants.StoredProcs.GetPAMPresentersForScope.IsActive, DbType.Boolean, IsActive);

                  using (var reader = database.ExecuteReader(command))
                  {
                      while (reader.Read())
                      {
                          PresentorsList.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
                      }
                  }
              }
              return PresentorsList;
        }


        public static List<KeyValuePair<int, string>> GetPAMPresentorsForState(string StateFIPS)
        {
            var PresentorsList = new List<KeyValuePair<int, string>>();
            var database = DatabaseFactory.CreateDatabase();

            using (var command = database.GetStoredProcCommand(StoredProcNames.Lookup.GetPAMPresentorsForState.Description()))
            {
                database.AddInParameter(command, Constants.StoredProcs.GetCountiesByStateFIPS.StateFIPS, DbType.String, StateFIPS);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        PresentorsList.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
            }
            return PresentorsList;
        }
        public static List<KeyValuePair<int, string>> GetSubmittersForState(string StateFIPS)
        {
            var SubmittersList = new List<KeyValuePair<int, string>>();
              var database = DatabaseFactory.CreateDatabase();

              using (var command = database.GetStoredProcCommand(StoredProcNames.Lookup.GetSubmittersForState.Description()))
              {
                  database.AddInParameter(command, Constants.StoredProcs.GetCountiesByStateFIPS.StateFIPS, DbType.String, StateFIPS);

                  using (var reader = database.ExecuteReader(command))
                  {
                      while (reader.Read())
                      {
                          SubmittersList.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
                      }
                  }
              }
              return SubmittersList;
        }





        public static IEnumerable<KeyValuePair<string, string>> GetCountiesNoMapping(string stateFIPS)
        {
            List<KeyValuePair<string, string>> counties = new List<KeyValuePair<string, string>>();

           var database = DatabaseFactory.CreateDatabase();

            using (var command = database.GetStoredProcCommand(StoredProcNames.Lookup.GetCountiesByStateFIPS.Description()))
            {
                database.AddInParameter(command, Constants.StoredProcs.GetCountiesByStateFIPS.StateFIPS, DbType.String, stateFIPS);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        counties.Add(new KeyValuePair<string, string>
                        (
                             reader.GetDefaultIfDBNull(T.County.CountyFIPS, GetString, null),
                             reader.GetDefaultIfDBNull(T.County.CountyNameShort, GetString, null)
                        ));
                    }
                }
            }

            return counties;
        }

        public static IEnumerable<County> GetCounties(string stateFIPS)
        {
            var counties = new List<County>();
            var database = DatabaseFactory.CreateDatabase();

            using (var command = database.GetStoredProcCommand(StoredProcNames.Lookup.GetCountiesByStateFIPS.Description()))
            {
                database.AddInParameter(command, Constants.StoredProcs.GetCountiesByStateFIPS.StateFIPS, DbType.String, stateFIPS);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        counties.Add(new County
                        {
                            Code = reader.GetDefaultIfDBNull(T.County.CountyFIPS, GetString, null),
                            CBSACode = reader.GetDefaultIfDBNull(T.County.CBSACODE, GetString, null),
                            CBSALSAD = reader.GetDefaultIfDBNull(T.County.CBSALSAD, GetString, null),
                            CBSATitle = reader.GetDefaultIfDBNull(T.County.CBSATITLE, GetString, null),
                            CreatedDate = reader.GetDefaultIfDBNull(T.County.CreatedDate, GetDateTime, null),
                            LastUpdatedBy = reader.GetDefaultIfDBNull(T.County.LastUpdatedBy, GetInt32, null),
                            LastUpdatedDate = reader.GetDefaultIfDBNull(T.County.LastUpdatedDate, GetDateTime, null),
                            Latitude = reader.GetDefaultIfDBNull(T.County.LAT, GetDouble, null),
                            Longitude = reader.GetDefaultIfDBNull(T.County.LONG, GetDouble, null),
                            LongName = reader.GetDefaultIfDBNull(T.County.CountyNameLong, GetString, null),
                            MediumName = reader.GetDefaultIfDBNull(T.County.CountyNameMedium, GetString, null),
                            ShortName = reader.GetDefaultIfDBNull(T.County.CountyNameShort, GetString, null),
                            State = new State(reader.GetDefaultIfDBNull(T.County.StateFIPS, GetString, null)),
                            WebLinkText = reader.GetDefaultIfDBNull(T.County.WEBLINKTEXT, GetString, null)
                        });
                    }
                }
            }

            return counties;
        }
        public static IEnumerable<County> GetCountyForCounselorLocationByAgencyId(int AgencyId)
        {
            var counties = new List<County>();
            var database = DatabaseFactory.CreateDatabase();

            using (var command = database.GetStoredProcCommand(StoredProcNames.Lookup.GetCountyOfCounselorLocationByAgencyId.Description()))
            {
                database.AddInParameter(command, Constants.StoredProcs.GetAgency.AgencyId, DbType.Int32, AgencyId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        counties.Add(new County
                        {
                            Code = reader.GetDefaultIfDBNull(T.County.CountyFIPS, GetString, null),
                            MediumName = reader.GetDefaultIfDBNull(T.County.CountyNameMedium, GetString, null)
                        });
                    }
                }
            }

            return counties;
        }

        public static IEnumerable<KeyValuePair<string, string>> GetCountyByAgencyIdForReport(int AgencyId, FormType formType)
        {
            List<KeyValuePair<string, string>> counties = new List<KeyValuePair<string, string>>();

            var database = DatabaseFactory.CreateDatabase();

            using (var command = database.GetStoredProcCommand("GetCountyByAgencyIdForReport"))
            {
                database.AddInParameter(command, Constants.StoredProcs.GetAgency.AgencyId, DbType.Int32, AgencyId);
                database.AddInParameter(command, Constants.StoredProcs.GetCCSummaryReport.FormType, DbType.Int16, (int)formType);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        counties.Add(new KeyValuePair<string, string>
                        (
                             reader.GetDefaultIfDBNull(T.County.CountyFIPS, GetString, null),
                             reader.GetDefaultIfDBNull(T.County.CountyNameShort, GetString, null)
                        ));
                    }
                }
            }

            return counties;
        }
        public static IEnumerable<KeyValuePair<string, string>> GetCountyOfClientResidenceByAgencyIdForReport(int AgencyId)
        {
            List<KeyValuePair<string, string>> counties = new List<KeyValuePair<string, string>>();

            var database = DatabaseFactory.CreateDatabase();

            using (var command = database.GetStoredProcCommand("GetCountyOfClientResidenceByAgencyIdForReport"))
            {
                database.AddInParameter(command, Constants.StoredProcs.GetAgency.AgencyId, DbType.Int32, AgencyId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        counties.Add(new KeyValuePair<string, string>
                        (
                             reader.GetDefaultIfDBNull(T.County.CountyFIPS, GetString, null),
                             reader.GetDefaultIfDBNull(T.County.CountyNameShort, GetString, null)
                        ));
                    }
                }
            }

            return counties;
        }
        public static IEnumerable<KeyValuePair<string, string>> GetCountyForCounselorLocationByState(string StateFips, FormType FormType)
        {

            List<KeyValuePair<string, string>> counties = new List<KeyValuePair<string, string>>();

            var database = DatabaseFactory.CreateDatabase();

            using (var command = database.GetStoredProcCommand(StoredProcNames.Lookup.GetCountyOfCounselorLocationByState.Description()))
            {
                database.AddInParameter(command, Constants.StoredProcs.GetCCSummaryReport.StateFIPS, DbType.String, StateFips);
                database.AddInParameter(command, Constants.StoredProcs.GetCCSummaryReport.FormType, DbType.Int16, (int)FormType);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        counties.Add(new KeyValuePair<string, string>
                        (
                             reader.GetDefaultIfDBNull(T.County.CountyFIPS, GetString, null),
                             reader.GetDefaultIfDBNull(T.County.CountyNameShort, GetString, null)
                        ));
                    }
                }
            }

            return counties;
        }
        public static IEnumerable<KeyValuePair<string, string>> GetCountyForClientResidenceByState(string StateFips)
        {
            var counties = new List<KeyValuePair<string, string>>();
            var database = DatabaseFactory.CreateDatabase();

            using (var command = database.GetStoredProcCommand(StoredProcNames.Lookup.GetCountyOfClientResidenceByState.Description()))
            {
                database.AddInParameter(command, Constants.StoredProcs.GetCCSummaryReport.StateFIPS, DbType.String, StateFips);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        counties.Add(new KeyValuePair<string, string>
                        (
                             reader.GetDefaultIfDBNull(T.County.CountyFIPS, GetString, null),
                             reader.GetDefaultIfDBNull(T.County.CountyNameShort, GetString, null)
                        ));
                    }
                }
            }

            return counties;
        }

        public static IEnumerable<KeyValuePair<string, string>> GetZipByAgencyIdForReport(int AgencyId, FormType formType)
        {
            List<KeyValuePair<string, string>> zipCode = new List<KeyValuePair<string, string>>();

            var database = DatabaseFactory.CreateDatabase();

            using (var command = database.GetStoredProcCommand("GetZipByAgencyIdForReport"))
            {
                database.AddInParameter(command, Constants.StoredProcs.GetAgency.AgencyId, DbType.Int32, AgencyId);
                database.AddInParameter(command, Constants.StoredProcs.GetCCSummaryReport.FormType, DbType.Int16, (int)formType);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        zipCode.Add(new KeyValuePair<string, string>
                        (
                            reader.GetDefaultIfDBNull(T.Zip.ZipCode, GetString, null),
                            reader.GetDefaultIfDBNull(T.Zip.ZipCode, GetString, null)
                        ));
                    }
                }
            }

            return zipCode;
        }
        public static IEnumerable<KeyValuePair<string, string>> GetZipCodeOfClientResidenceByAgencyIdForReport(int AgencyId)
        {
            var zipCodes = new List<KeyValuePair<string, string>>();

            var database = DatabaseFactory.CreateDatabase();

            using (var command = database.GetStoredProcCommand("GetZipCodeOfClientResidenceByAgencyIdForReport"))
            {
                database.AddInParameter(command, Constants.StoredProcs.GetAgency.AgencyId, DbType.Int32, AgencyId);


                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        zipCodes.Add(new KeyValuePair<string, string>
                        (
                            reader.GetDefaultIfDBNull(T.Zip.ZipCode, GetString, null),
                            reader.GetDefaultIfDBNull(T.Zip.ZipCodeDisplay, GetString, null)

                        )
                        );
                    }
                }
            }

            return zipCodes;
        }
        public static IEnumerable<KeyValuePair<string, string>> GetZipForCounselorLocationByState(string StateFips, FormType FormType)
        {
            List<KeyValuePair<string, string>> zipCode = new List<KeyValuePair<string, string>>();
            var database = DatabaseFactory.CreateDatabase();

            using (var command = database.GetStoredProcCommand(StoredProcNames.Lookup.GetZipCodeOfCounselorLocationByState.Description()))
            {
                database.AddInParameter(command, Constants.StoredProcs.GetCCSummaryReport.StateFIPS, DbType.String, StateFips);
                database.AddInParameter(command, Constants.StoredProcs.GetCCSummaryReport.FormType, DbType.Int16, (int)FormType);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        zipCode.Add(new KeyValuePair<string, string>
                        (
                            reader.GetDefaultIfDBNull(T.Zip.ZipCode, GetString, null),
                            reader.GetDefaultIfDBNull(T.Zip.ZipCode, GetString, null)
                        ));
                    }
                }
            }

            return zipCode;
        }
        public static IEnumerable<KeyValuePair<string, string>> GetZipForClientResidenceByState(string StateFips)
        {
           var zipCodes = new List<KeyValuePair<string, string>>();

            var database = DatabaseFactory.CreateDatabase();

            using (var command = database.GetStoredProcCommand(StoredProcNames.Lookup.GetZipCodeOfClientResidenceByState.Description()))
            {
                database.AddInParameter(command, Constants.StoredProcs.GetCCSummaryReport.StateFIPS, DbType.String, StateFips);


               using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        zipCodes.Add(new KeyValuePair<string, string>
                        (
                            reader.GetDefaultIfDBNull(T.Zip.ZipCode, GetString, null),
                            reader.GetDefaultIfDBNull(T.Zip.ZipCodeDisplay, GetString, null)

                        )
                        );
                    }
                }
            }

            return zipCodes;
        }

        public static IEnumerable<Agency> GetAgencies(string stateFIPS)
        {
            var agencies = new List<Agency>();
            var database = DatabaseFactory.CreateDatabase();

            using (var command = database.GetStoredProcCommand("GetAgenciesForState_Lookup"))
            {
                database.AddInParameter(command, SP.GetAgenciesForStateLookup.StateFIPS, DbType.StringFixedLength, stateFIPS);
                // to get only active agencies
                database.AddInParameter(command, SP.GetAgenciesForStateLookup.IncludeInactive, DbType.Boolean, false);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        agencies.Add(new Agency
                        {
                            Id = reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetInt32, null),
                            Name = reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, null)
                        });
                    }
                }
            }

            return agencies;
        }

        public static List<KeyValuePair<string, string>> GetCountyForAgency(int AgencyID)
        {
            var CountiesList = new List<KeyValuePair<string, string>>();
            var database = DatabaseFactory.CreateDatabase();

            using (var command = database.GetStoredProcCommand("GetCountiesByAgencyID"))
            {
                database.AddInParameter(command, "@AgencyId", DbType.Int32, AgencyID);


                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        CountiesList.Add(new KeyValuePair<string, string>(reader.GetString(0), reader.GetString(1)));
                    }
                }
            }

            return CountiesList;
        }
        public static IEnumerable<Agency> GetAgencies(string stateFIPS,string countyFIPS,string zip)
        {
            var agencies = new List<Agency>();
            var database = DatabaseFactory.CreateDatabase();

            using (var command = database.GetStoredProcCommand("dbo.GetAgencyForStateCountyZip"))
            {
                database.AddInParameter(command, SP.GetAgenciesForStateCountyZipLookup.StateFIPS, DbType.StringFixedLength, stateFIPS);
                database.AddInParameter(command, SP.GetAgenciesForStateCountyZipLookup.CountyFIPS, DbType.StringFixedLength, countyFIPS);
                database.AddInParameter(command, SP.GetAgenciesForStateCountyZipLookup.Zip, DbType.StringFixedLength, zip);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        agencies.Add(new Agency
                        {
                            Id = reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetInt32, null),
                            Name = reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, null),
                            Code = reader.GetDefaultIfDBNull(T.Agency.AgencyCode, GetString, null),
                            
                            Locations = new List<AgencyLocation> { 
                                        new AgencyLocation { 
                                            LocationName = reader.GetDefaultIfDBNull(T.AgencyLocation.LocationName, GetString, null),
                                            HoursOfOperation = reader.GetDefaultIfDBNull(T.AgencyLocation.HoursOfOperation, GetString, null),
                                            PrimaryPhone = reader.GetDefaultIfDBNull(T.AgencyLocation.PrimaryPhone, GetString, null),
                                            PrimaryEmail = reader.GetDefaultIfDBNull(T.AgencyLocation.PrimaryEmail, GetString, null),
                                            IsMainOffice = reader.GetDefaultIfDBNull(T.AgencyLocation.IsMainOffice, GetBool,false),
                                            PhysicalAddress = 
                                                new AgencyAddress { 
                                                    Id = reader. GetDefaultIfDBNull(T.AgencyLocation.PhysicalAddressID,GetNullableInt32, null),
                                                    Address1 = reader.GetDefaultIfDBNull(T.AgencyAddress.Address1,GetString, null),
                                                    Address2 = reader.GetDefaultIfDBNull(T.AgencyAddress.Address2,GetString, null),
                                                    City = reader.GetDefaultIfDBNull(T.AgencyAddress.City,GetString, null),
                                                    State = new State(reader.GetDefaultIfDBNull(T.AgencyAddress.StateFIPS,GetString,null)),
                                                    Zip = reader.GetDefaultIfDBNull(T.AgencyAddress.Zip,GetString, null)},
                                                 }
                                         },
                            State = new State(reader.GetDefaultIfDBNull(T.Agency.StateFIPS, GetString, null)),
                        });
                    }
                }
            }

            return agencies;
        }



        /// <summary>
        /// Get List of State FIPS that fall under a CMS Region
        /// </summary>
        /// <param name="CMSRegionId"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetStatesForCMSRegion(int CMSRegionId)
        {
            List<string> StateFIPS = new List<string>();

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetStatesForCMSRegion.Description()))
            {
                db.AddInParameter(dbCmd, "@CMSRegionId", DbType.Int32, CMSRegionId);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        StateFIPS.Add(reader.GetString(0));
                    }
                }
            }
            return StateFIPS;
        }


        /// <summary>
        /// Returns Agencies for a Sub State Region
        /// </summary>
        /// <param name="SubStateRegionId"></param>
        /// <returns></returns>
        public static List<Agency> GetAgenciesForSubStateRegion(int SubStateRegionId)
        {
            List<Agency> Agencies = new List<Agency>();

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetAgenciesForSubStateRegion.Description()))
            {
                db.AddInParameter(dbCmd, "@SubStateRegionId", DbType.Int32, SubStateRegionId);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        Agencies.Add(GetAgencySummaryData(reader));
                    }
                }
            }
            return Agencies;
        }


        /// <summary>
        /// Returns Supervisors(Reviewers) for Users of State Scope in a State
        /// For Agency/SubState scope, use GetReviewersByUserRegion
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <returns></returns>
        public static List<KeyValuePair<int, string>> GetReviewersForStateScope(string StateFIPS)
        {
            List<KeyValuePair<int, string>> ReviewerList = new List<KeyValuePair<int, string>>();

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetReviewersForStateScope.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, StateFIPS);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        ReviewerList.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
            }
            return ReviewerList;
        }
        public static List<KeyValuePair<int, string>> GetClientContactCounselorForState(string StateFIPS)
        {
            List<KeyValuePair<int, string>> CounselorList = new List<KeyValuePair<int, string>>();

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetClientContactCounselorForState.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, StateFIPS);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        CounselorList.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
            }
            return CounselorList;
        }
        public static List<KeyValuePair<int, string>> GetClientContactSubmitterForState(string StateFIPS)
        {
            List<KeyValuePair<int, string>> SubmitterList = new List<KeyValuePair<int, string>>();

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetClientContactSubmitterForState.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, StateFIPS);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        SubmitterList.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
            }
            return SubmitterList;
        }
        public static List<KeyValuePair<int, string>> GetPresenterForState(string StateFIPS)
        {
            List<KeyValuePair<int, string>> PresenterList = new List<KeyValuePair<int, string>>();

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.User.GetPresenterForState.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, StateFIPS);

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        PresenterList.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
            }
            return PresenterList;
        }
        

        /// <summary>
        /// Returns Supervisors(Reviewers) for Agency or Sub State.
        /// For State Users, to find Supervisors, use GetReviewersForStateScope
        /// </summary>
        /// <param name="UserRegionId"></param>
        /// <returns></returns>
        public static List<KeyValuePair<int, string>> GetReviewersByUserRegion(int UserRegionId, Scope UserScope)
        {
            List<KeyValuePair<int, string>> ReviewerList = new List<KeyValuePair<int, string>>();

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetReviewersByUserRegion.Description()))
            {
                db.AddInParameter(dbCmd, "@UserRegionId", DbType.Int32, UserRegionId);
                db.AddInParameter(dbCmd, "@ScopeId", DbType.Int32, UserScope.EnumValue<int>());

                using (IDataReader reader = db.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        ReviewerList.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
            }
            return ReviewerList;
        }

        private static readonly Func<IDataReader, Agency> GetAgencySummaryData = FillAgencySummaryData;
        private static Agency FillAgencySummaryData(IDataReader reader)
        {
            Agency agencyObj = new Agency();
            agencyObj.Id = reader.GetInt32(0);
            agencyObj.Name = reader.GetString(1);
            agencyObj.Type = reader.GetInt16(2).ToEnumObject<AgencyType>();
            if (!reader.IsDBNull(3))
                agencyObj.SponsorFirstName = reader.GetString(3);
            if (!reader.IsDBNull(4))
                agencyObj.SponsorMiddleName = reader.GetString(4);
            if (!reader.IsDBNull(5))
                agencyObj.SponsorLastName = reader.GetString(5);
            if (!reader.IsDBNull(6))
                agencyObj.SponsorTitle = reader.GetString(6);
            if (!reader.IsDBNull(7))
                agencyObj.URL = reader.GetString(7);
            if (!reader.IsDBNull(8))
                agencyObj.Comments = reader.GetString(8);
            if (!reader.IsDBNull(9))
                agencyObj.IsActive = reader.GetBoolean(9);
            if (!reader.IsDBNull(10))
                agencyObj.Code = reader.GetString(10);

            return agencyObj;
        }





        /// <summary>
        /// Returns ShipDirectorID for a given state, if available; else returns null
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <returns></returns>
        public static int? GetShipDirectorForState(string StateFIPS)
        {

            int? ShipDirectorId = null;

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetShipDirector.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.StringFixedLength, @StateFIPS);
                object o = db.ExecuteScalar(dbCmd);
                if (o != null && o != DBNull.Value)
                    ShipDirectorId = (int)o;
            }
            return ShipDirectorId;
        }

        /// <summary>
        /// Returns the Agencies for a given state
        /// </summary>
        /// <param name="StateFIPS">Unique State FIPS value.</param>
        /// <returns></returns>
        public static IDataReader GetAgencyCodesForState(string StateFIPS)
        {
            
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            IDataReader StateAgencies = null;
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetAgenciesForState.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.String, StateFIPS);
                db.AddInParameter(dbCmd, "@IncludeInactive", DbType.Boolean, true);
                db.AddInParameter(dbCmd, "@IncludeAgencyLocations", DbType.Boolean, false);
                StateAgencies = db.ExecuteReader(dbCmd);
            }

            return StateAgencies;

        }


        /// <summary>
        /// Returns Reader with State Agency data.
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <param name="AgencyCode"></param>
        /// <returns></returns>
        public static IDataReader GetAgencyByCodeState(string AgencyCode,string StateFIPS )
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            IDataReader rdrStateAgency = null;
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetAgencyByCodeState.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.String, StateFIPS);
                db.AddInParameter(dbCmd, "@AgencyCode", DbType.String, AgencyCode);
                rdrStateAgency = db.ExecuteReader(dbCmd);
            }

            return rdrStateAgency;

        }


        /// <summary>
        /// Returns the zipcodes of a state.
        /// </summary>
        /// <param name="StateFIPS">Stat FIPS code</param>
        /// <returns></returns>
        public static IDataReader GetZipCodeForStateFips(string StateFIPS)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            IDataReader ZipCodes = null;
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetZipCodeForStateFips.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.String, StateFIPS);
                ZipCodes = db.ExecuteReader(dbCmd);
            }

            return ZipCodes;

        }
         /// <summary>
        /// Returns the zipcodes of a state.
        /// </summary>
        /// <param name="StateFIPS">State FIPS code</param>
        /// <returns></returns>
        public static IDataReader GetZipCodeOfClientResidencesForState(string StateFIPS)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            IDataReader ZipCodes = null;
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetCountyOfClientResidenceByState.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.String, StateFIPS);
                ZipCodes = db.ExecuteReader(dbCmd);
            }

            return ZipCodes;
        }

        ///// <summary>
        ///// Returns the zipcodes of a state.
        ///// </summary>
        ///// <param name="StateFIPS">State FIPS code</param>
        ///// <returns></returns>
        //public static IDataReader GetZipCodeOfClientResidencesForState(string StateFIPS)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
        //    IDataReader ZipCodes = null;
        //    using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetCountyOfClientResidenceByState.Description()))
        //    {
        //        db.AddInParameter(dbCmd, "@StateFIPS", DbType.String, StateFIPS);
        //        ZipCodes = db.ExecuteReader(dbCmd);
        //    }

        //    return ZipCodes;
        //}



        /// <summary>
        /// Checks to see if the County code is valid in the database or as per say valid in Country.
        /// </summary>
        /// <param name="ZipCode"></param>
        /// <returns></returns>
        public static bool IsCountyCodeValid(string CountyFIPS)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand("dbo.IsCountyCodeValid"))
            {
                db.AddInParameter(dbCmd, "@CountyFips ", DbType.String, CountyFIPS);
                bool IsValid = Convert.ToBoolean(db.ExecuteScalar(dbCmd));
                return IsValid;
            }
        }


        /// <summary>
        /// Checks to see if the zip code is valid in the database or as per say valid in Country.
        /// </summary>
        /// <param name="ZipCode"></param>
        /// <returns></returns>
        public static bool IsZipCodeValid(string ZipCode)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand("dbo.IsZIPCodeValid"))
            {
                db.AddInParameter(dbCmd, "@ZipCode", DbType.String, ZipCode);
                bool IsValid = Convert.ToBoolean(db.ExecuteScalar(dbCmd));
                return IsValid;
            }
        }

        /// <summary>
        /// Checks to see if the zip code is valid for the state.
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <param name="ZipCode"></param>
        /// <returns></returns>
        public static bool IsZipCodeValidForState(string StateFIPS, string ZipCode)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.IsZipCodeValidForState.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.String, StateFIPS);
                db.AddInParameter(dbCmd, "@ZipCode", DbType.String, ZipCode);
                bool IsValid = Convert.ToBoolean(db.ExecuteScalar(dbCmd));
                return IsValid;
            }
        }


        
        /// <summary>
        /// Checks to see if the zip code is valid for the county.
        /// </summary>
        /// <param name="CountyFIPS"></param>
        /// <param name="ZipCode"></param>
        /// <returns></returns>
        public static bool IsZipCodeValidForCounty(string CountyFIPS, string ZipCode)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.IsZipCodeValidForCounty.Description()))
            {
                db.AddInParameter(dbCmd, "@CountyFips ", DbType.String, CountyFIPS);
                db.AddInParameter(dbCmd, "@ZipCode", DbType.String, ZipCode);
                bool IsValid = Convert.ToBoolean(db.ExecuteScalar(dbCmd));
                return IsValid;
            }
        }

        /// <summary>
        /// Determine if the state fip code is valid
        /// </summary>
        /// <param name="StateFIPCode"></param>
        /// <returns></returns>
        public static bool IsStateFipCodeValid(string StateFIPCode)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.IsStateFipCodeValid.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIP", DbType.String, StateFIPCode);
                bool IsValid = Convert.ToBoolean(db.ExecuteScalar(dbCmd));
                return IsValid;
            }

        }

        /// <summary>
        /// Checks to see if the zip code is valid for the state.
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <param name="ZipCode"></param>
        /// <returns></returns>
        public static bool IsAgencyValidForState(string AgencyCode, string StateFIPS)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.IsAgencyValidForState.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS ", DbType.String, StateFIPS);
                db.AddInParameter(dbCmd, "@AgencyCode", DbType.String, AgencyCode);
                bool IsValid = Convert.ToBoolean(db.ExecuteScalar(dbCmd));
                return IsValid;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ZipCode"></param>
        /// <returns></returns>
        public static IDataReader GetCountiesByZip(string ZipCode)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            IDataReader ZipCodes = null;
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetCountiesByZip.Description()))
            {
                db.AddInParameter(dbCmd, "@ZipCode", DbType.String, ZipCode);
                ZipCodes = db.ExecuteReader(dbCmd);
            }

            return ZipCodes;
        }

        /// <summary>
        /// returns the look up table countyFips,CountyName for give zipcode
        /// </summary>
        /// <param name="ZipCode"></param>
        /// <returns></returns>
        public static IDataReader GetCountiesByZipCode(string ZipCode)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            IDataReader ZipCodes = null;
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetCountyByZipCode.Description()))
            {
                db.AddInParameter(dbCmd, "@ZipCode", DbType.String, ZipCode);
                ZipCodes = db.ExecuteReader(dbCmd);
            }

            return ZipCodes;
        }

        /// <summary>
        /// Returns the CountyZipId, Zipcode based on the CountyFips.
        /// </summary>
        /// <param name="CountyFips"></param>
        /// <returns></returns>
        public static IDataReader GetZipCodeForCountyFips(string CountyFips)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            IDataReader ZipCodes = null;
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetZipCodeForCountyFips.Description()))
            {
                db.AddInParameter(dbCmd, "@CountyFips", DbType.String, CountyFips);
                ZipCodes = db.ExecuteReader(dbCmd);
            }

            return ZipCodes;
        }

        /// <summary>
        /// Returns the States Fips code based on the short name passed in.
        /// </summary>
        /// <param name="StateShortName"></param>
        /// <returns></returns>
        public static string GetStateFipsCodeByShortName(string StateShortName)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            IDataReader FipCodesFound = null;
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.GetStateFipCodeByShortName.Description()))
            {
                db.AddInParameter(dbCmd, "@ShortName", DbType.String, StateShortName);
                FipCodesFound = db.ExecuteReader(dbCmd);
                FipCodesFound.Read();
                return FipCodesFound["FIPS"].ToString();
            }
        }
    }
}
