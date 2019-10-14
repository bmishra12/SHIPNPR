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
    internal class SubStateRegionForReportDAL : DALBase
    {
        public int CreateSubStateRegionForReport(SubStateRegionForReport subStateRegionForReport)
        {
            if (subStateRegionForReport == null)
                throw new ArgumentNullException("subStateRegionForReport");

            if (subStateRegionForReport.ServiceAreas == null || subStateRegionForReport.ServiceAreas.Count == 0)
                throw new ArgumentNullException("subStateRegionForReport",
                                                "At least one  location is required to create a subStateRegionForReport.");


            using (var command = database.GetStoredProcCommand("dbo.CreateSubStateRegionForReport"))
            {
                database.AddInParameter(command, "@StateFIPS", DbType.String, subStateRegionForReport.State.Code);
                database.AddInParameter(command, "@SubStateRegionName", DbType.String, subStateRegionForReport.SubStateRegionName);
                database.AddInParameter(command, "@SubStateRegionServiceEntityCode", DbType.String, subStateRegionForReport.SubStateRegionServiceEntityCode);
               
                database.AddInParameter(command, "@SubStateRegionType", DbType.Int16, (int)subStateRegionForReport.Type);
                database.AddInParameter(command, "@CreatedBy", DbType.Int32, subStateRegionForReport.CreatedBy);
                database.AddInParameter(command, "@FormType", DbType.Int32, (int)subStateRegionForReport.ReprotFormType);

               
                database.AddInParameter(command, SP.CreateAgency.ServiceAreas, DbType.String, string.Join(",", (from p in subStateRegionForReport.ServiceAreas select p.ZIPFIPSCountyCode).ToArray()));
                database.AddOutParameter(command, "@SubStateRegionID", DbType.Int32, 6);  

                database.ExecuteNonQuery(command);

                return (int)database.GetParameterValue(command, "@SubStateRegionID");
            }
        }
        

        public IEnumerable<SubStateRegionForReport> ListAllSubStates()
        {

            var substates = new List<SubStateRegionForReport>();

            using (var command = database.GetStoredProcCommand("dbo.ListAllSubStateRegionForReport"))
            {

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        substates.Add(new SubStateRegionForReport
                        {
                            ID = reader.GetDefaultIfDBNull("SubStateRegionID", GetNullableInt32, null),
                            SubStateRegionName = reader.GetDefaultIfDBNull("SubStateRegionName", GetString, null),
                            Type = (SubStateReportType)reader.GetDefaultIfDBNull("SubStateRegionType", GetNullableInt16, null),

                            State = new State(reader.GetDefaultIfDBNull("StateFIPSCode", GetString, null)),
                        });
                    }
                }
            }

            return substates;
        }


        public IEnumerable<SubStateRegionForReport> ListSubStatesForState(string stateCode)
        {

            var substates = new List<SubStateRegionForReport>();

            using (var command = database.GetStoredProcCommand("dbo.ListSubStateRegionForReportForState"))
            {
                database.AddInParameter(command, "@StateFIPS", DbType.String, stateCode);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        substates.Add(new SubStateRegionForReport
                        {
                            ID = reader.GetDefaultIfDBNull("SubStateRegionID", GetNullableInt32, null),
                            SubStateRegionName = reader.GetDefaultIfDBNull("SubStateRegionName", GetString, null),
                            Type = (SubStateReportType)reader.GetDefaultIfDBNull("SubStateRegionType", GetNullableInt16, null),

                            State = new State(reader.GetDefaultIfDBNull("StateFIPSCode", GetString, null)),
                        });
                    }
                }
            }

            return substates;
        }

        public SubStateRegionForReport GetSubStateRegionForReport(int id)
        {
            SubStateRegionForReport subStateRegionForReport = null;

            using (var command = database.GetStoredProcCommand("dbo.GetSubStateRegionForReport"))
            {
                database.AddInParameter(command, "@SubStateRegionID", DbType.Int32, id);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        subStateRegionForReport = new SubStateRegionForReport
                        {
                            ID = reader.GetDefaultIfDBNull("SubStateRegionID", GetNullableInt32, null),
                            SubStateRegionName = reader.GetDefaultIfDBNull("SubStateRegionName", GetString, null),

                            SubStateRegionServiceEntityCode = reader.GetDefaultIfDBNull("SubStateRegionServiceEntityCode", GetString, null),
                       
                            
                            ReprotFormType = (FormType)reader.GetDefaultIfDBNull("FormType", GetNullableInt16, null),

                            Type = (SubStateReportType)reader.GetDefaultIfDBNull("SubStateRegionType", GetNullableInt16, null),

                            State = new State(reader.GetDefaultIfDBNull("StateFIPSCode", GetString, null)),

                            ServiceAreas = new List<SubStateRegionZIPFIPSForReport>()
                        };

                        reader.NextResult();

                        while (reader.Read())
                        {
                            subStateRegionForReport.ServiceAreas.Add(
                                new SubStateRegionZIPFIPSForReport
                                {
                                    ZIPFIPSCountyCode = reader.GetDefaultIfDBNull("ZIPFIPSCountyCode", GetString, null),
                                    Name = reader.GetDefaultIfDBNull("Name", GetString, null),
                                    CreatedDate = reader.GetDefaultIfDBNull(T.County.CreatedDate, GetNullableDateTime, null)
                                });
                        }

                        reader.NextResult();

                    }
                }
            }

            return subStateRegionForReport;
        }


        public void UpdateSubStateRegionForReport(SubStateRegionForReport subStateRegionForReport)
        {
            if (subStateRegionForReport == null)
                throw new ArgumentNullException("subStateRegionForReport");


            if (subStateRegionForReport.ServiceAreas == null || subStateRegionForReport.ServiceAreas.Count == 0)
                throw new ArgumentNullException("subStateRegionForReport",
                                                "At least one  location is required to create a subStateRegionForReport.");


            using (var command = database.GetStoredProcCommand("dbo.UpdateSubStateRegionForReport"))
            {

                database.AddInParameter(command, "@SubStateRegionID", DbType.String, subStateRegionForReport.ID);

                database.AddInParameter(command, "@StateFIPS", DbType.String, subStateRegionForReport.State.Code);
                database.AddInParameter(command, "@SubStateRegionName", DbType.String, subStateRegionForReport.SubStateRegionName);
                database.AddInParameter(command, "@SubStateRegionServiceEntityCode", DbType.String, subStateRegionForReport.SubStateRegionServiceEntityCode);
               
                database.AddInParameter(command, "@SubStateRegionType", DbType.Int16, (int)subStateRegionForReport.Type);
                database.AddInParameter(command, "@CreatedBy", DbType.Int32, subStateRegionForReport.CreatedBy);
                database.AddInParameter(command, "@FormType", DbType.Int32, (int)subStateRegionForReport.ReprotFormType);

               
                database.AddInParameter(command, SP.CreateAgency.ServiceAreas, DbType.String, string.Join(",", (from p in subStateRegionForReport.ServiceAreas select p.ZIPFIPSCountyCode).ToArray()));

                database.ExecuteNonQuery(command);
            }
        }

        public void DeleteSubStateRegionForReport(int id)
        {
            using (var command = database.GetStoredProcCommand("dbo.DeleteSubStateRegionForReport"))
            {
                database.AddInParameter(command, SP.DeleteAgency.Id, DbType.Int32, id);

                database.ExecuteNonQuery(command);
            }
        }



        public bool DoesSubStateRegionForReportNameExist(string SubStateRegionForReportName)
        {
            if (string.IsNullOrEmpty(SubStateRegionForReportName))
                throw new ArgumentNullException("SubStateRegionForReportName");

            using (var command = database.GetStoredProcCommand("dbo.DoesSubStateRegionForReportNameExist"))
            {
                database.AddInParameter(command, "@SubStateRegionForReportName", DbType.String, SubStateRegionForReportName);

                return Convert.ToBoolean(database.ExecuteScalar(command));
            }
        }

        public string GetSubStateRegionAgencyNameForReport(int SubStateRegionID)
        {
            string agencyName = string.Empty;
            using (var command = database.GetStoredProcCommand("dbo.GetSubStateRegionAgencyNameForReport"))
            {
                database.AddInParameter(command, "@SubStateRegionID", DbType.Int16, SubStateRegionID);


                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        agencyName = agencyName + " " + reader.GetDefaultIfDBNull("AgencyName", GetString, null);
                    }
                }
            }

            return agencyName.TrimStart();
        }

        public string GetSubStateRegionCountyNameForReport(int SubStateRegionID)
        {
            string countyName = string.Empty;
            using (var command = database.GetStoredProcCommand("dbo.GetSubStateRegionCountyNameForReport"))
            {
                database.AddInParameter(command, "@SubStateRegionID", DbType.Int16, SubStateRegionID);


                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        countyName = countyName + " " + reader.GetDefaultIfDBNull("CountyNameShort", GetString, null);
                    }
                }
            }

            return countyName.TrimStart();
        }



        public string GetSubStateRegionZipCodeForReport(int SubStateRegionID)
        {
            string Zips = string.Empty;
            using (var command = database.GetStoredProcCommand("dbo.GetSubStateRegionZipCodeForReport"))
            {
                database.AddInParameter(command, "@SubStateRegionID", DbType.Int16, SubStateRegionID);


                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        Zips = Zips + " " + reader.GetDefaultIfDBNull("ZIPFIPSCountyCode", GetString, null);
                    }
                }
            }

            return Zips.TrimStart();
        }
    }
}
