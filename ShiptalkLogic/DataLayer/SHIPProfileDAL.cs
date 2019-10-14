using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ShiptalkLogic.BusinessObjects;
using T = ShiptalkLogic.Constants.Tables;
using SP = ShiptalkLogic.Constants.StoredProcs;

namespace ShiptalkLogic.DataLayer
{
    internal class SHIPProfileDAL :DALBase
    {
        public  SHIPProfile GetSHIPProfile(string ID)
        {
            SHIPProfile shipProfile = null;
            using (var command = database.GetStoredProcCommand("dbo.GetSHIPProfile"))
            {
                database.AddInParameter(command, SP.GetSHIPProfile.Id, DbType.String, ID);


                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        shipProfile = new SHIPProfile
                        {
                            ID = reader.GetDefaultIfDBNull(T.SHIPProfile.ID,GetString,null),
                            AdminAgencyAddress = reader.GetDefaultIfDBNull(T.SHIPProfile.AdminAgencyAddress, GetString, null),
                            AdminAgencyCity = reader.GetDefaultIfDBNull(T.SHIPProfile.AdminAgencyCity, GetString, null),
                            AdminAgencyContactName = reader.GetDefaultIfDBNull(T.SHIPProfile.AdminAgencyContactName, GetString, null),
                            AdminAgencyContactTitle = reader.GetDefaultIfDBNull(T.SHIPProfile.AdminAgencyContactTitle, GetString, null),
                            AdminAgencyEmail = reader.GetDefaultIfDBNull(T.SHIPProfile.AdminAgencyEmail, GetString, null),
                            AdminAgencyFax = reader.GetDefaultIfDBNull(T.SHIPProfile.AdminAgencyFax, GetString, null),
                            AdminAgencyName = reader.GetDefaultIfDBNull(T.SHIPProfile.AdminAgencyName, GetString, null),
                            AdminAgencyPhone = reader.GetDefaultIfDBNull(T.SHIPProfile.AdminAgencyPhone, GetString, null),
                            AdminAgencyZipcode = reader.GetDefaultIfDBNull(T.SHIPProfile.AdminAgencyZipcode, GetString, null),
                            AvailableLanguages = reader.GetDefaultIfDBNull(T.SHIPProfile.AvailableLanguages, GetString, null),
                            BeneficiaryContactEmail = reader.GetDefaultIfDBNull(T.SHIPProfile.BeneficiaryContactEmail, GetString, null),
                            BeneficiaryContactHours = reader.GetDefaultIfDBNull(T.SHIPProfile.BeneficiaryContactHours, GetString, null),
                            BeneficiaryContactPhoneTollFree = reader.GetDefaultIfDBNull(T.SHIPProfile.BeneficiaryContactPhoneTollFree, GetString, null),
                            BeneficiaryContactPhoneTollFreeInStateOnly = reader.GetDefaultIfDBNull(T.SHIPProfile.BeneficiaryContactPhoneTollFreeInStateOnly, GetBool, false),
                            BeneficiaryContactPhoneTollLine = reader.GetDefaultIfDBNull(T.SHIPProfile.BeneficiaryContactPhoneTollLine, GetString, null),
                            BeneficiaryContactTDDLine = reader.GetDefaultIfDBNull(T.SHIPProfile.BeneficiaryContactTDDLine, GetString, null),
                            BeneficiaryContactWebsite = reader.GetDefaultIfDBNull(T.SHIPProfile.BeneficiaryContactWebsite, GetString, null),

                            NumberOfCountiesServed = reader.GetDefaultIfDBNull(T.SHIPProfile.NumberOfCountiesServed, GetNullableInt16, null),

                            NumberOfSponsors = reader.GetDefaultIfDBNull(T.SHIPProfile.NumberOfSponsors, GetNullableInt16, null),
                            NumberOfStateStaff = reader.GetDefaultIfDBNull(T.SHIPProfile.NumberOfStateStaff, GetNullableInt16, null),
                            NumberOfVolunteerCounselors = reader.GetDefaultIfDBNull(T.SHIPProfile.NumberOfVolunteerCounselors, GetNullableInt16, null),


                            ProgramCoordinatorAddress = reader.GetDefaultIfDBNull(T.SHIPProfile.ProgramCoordinatorAddress, GetString, null),

                            ProgramCoordinatorCity = reader.GetDefaultIfDBNull(T.SHIPProfile.ProgramCoordinatorCity, GetString, null),
                            ProgramCoordinatorEmail = reader.GetDefaultIfDBNull(T.SHIPProfile.ProgramCoordinatorEmail, GetString, null),
                            ProgramCoordinatorFax = reader.GetDefaultIfDBNull(T.SHIPProfile.ProgramCoordinatorFax, GetString, null),
                            ProgramCoordinatorName = reader.GetDefaultIfDBNull(T.SHIPProfile.ProgramCoordinatorName, GetString, null),
                            ProgramCoordinatorPhone = reader.GetDefaultIfDBNull(T.SHIPProfile.ProgramCoordinatorPhone, GetString, null),
                            ProgramCoordinatorZipcode = reader.GetDefaultIfDBNull(T.SHIPProfile.ProgramCoordinatorZipcode, GetString, null),
                            ProgramName = reader.GetDefaultIfDBNull(T.SHIPProfile.ProgramName, GetString, null),
                            ProgramSummary = reader.GetDefaultIfDBNull(T.SHIPProfile.ProgramSummary, GetString, null),

                            ProgramWebsite = reader.GetDefaultIfDBNull(T.SHIPProfile.ProgramWebsite, GetString, null),
                            StateName = reader.GetDefaultIfDBNull(T.SHIPProfile.StateName, GetString, null),
                            TotalCounties = reader.GetDefaultIfDBNull(T.SHIPProfile.TotalCounties, GetNullableInt16, null),

                            //New fields: added by Lavanya Maram - 04/23/2013

                            StateOversightAgency = reader.GetDefaultIfDBNull(T.SHIPProfile.StateOversightAgency, GetString, null),
                            NumberOfPaidStaff = reader.GetDefaultIfDBNull(T.SHIPProfile.NumberOfPaidStaff, GetNullableInt16, null),
                            NumberOfCoordinators = reader.GetDefaultIfDBNull(T.SHIPProfile.NumberOfCoordinators, GetNullableInt16, null),
                            NumberOfCertifiedCounselors = reader.GetDefaultIfDBNull(T.SHIPProfile.NumberOfCertifiedCounselors, GetNullableInt16, null),
                            NumberOfEligibleBeneficiaries = reader.GetDefaultIfDBNull(T.SHIPProfile.NumberOfEligibleBeneficiaries, GetString, null),
                            NumberOfBeneficiaryContacts = reader.GetDefaultIfDBNull(T.SHIPProfile.NumberOfBeneficiaryContacts, GetString, null),
                            LocalAgencies = reader.GetDefaultIfDBNull(T.SHIPProfile.LocalAgencies, GetString, null),
                            Longitude = reader.GetDefaultIfDBNull(T.SHIPProfile.Longitude, GetNullableDouble, null),
                            Latitude = reader.GetDefaultIfDBNull(T.SHIPProfile.Latitude, GetNullableDouble, null),
                            
                        };

                    }
                }

            }
            return shipProfile;
        }

      
        public void UpdateShipProfile(SHIPProfile shipProfile)
        {
            if (shipProfile == null)
                throw new ArgumentNullException("shipProfile");

            using (var command = database.GetStoredProcCommand("dbo.UpdateShipProfile"))
            {
                database.AddInParameter(command, SP.UpdateSHIPProfile.ID, DbType.String, shipProfile.ID);
                database.AddInParameter(command, SP.UpdateSHIPProfile.ProgramName, DbType.String, shipProfile.ProgramName);
                database.AddInParameter(command, SP.UpdateSHIPProfile.ProgramWebsite, DbType.String, shipProfile.ProgramWebsite);
                database.AddInParameter(command, SP.UpdateSHIPProfile.ProgramSummary, DbType.String, shipProfile.ProgramSummary);

                database.AddInParameter(command, SP.UpdateSHIPProfile.BeneficiaryContactPhoneTollFree, DbType.String, shipProfile.BeneficiaryContactPhoneTollFree);
                database.AddInParameter(command, SP.UpdateSHIPProfile.BeneficiaryContactPhoneTollFreeInStateOnly, DbType.String, shipProfile.BeneficiaryContactPhoneTollFreeInStateOnly);
                database.AddInParameter(command, SP.UpdateSHIPProfile.BeneficiaryContactPhoneTollLine, DbType.String, shipProfile.BeneficiaryContactPhoneTollLine);
                database.AddInParameter(command, SP.UpdateSHIPProfile.BeneficiaryContactWebsite, DbType.String, shipProfile.BeneficiaryContactWebsite);
                database.AddInParameter(command, SP.UpdateSHIPProfile.BeneficiaryContactTDDLine, DbType.String, shipProfile.BeneficiaryContactTDDLine);
                database.AddInParameter(command, SP.UpdateSHIPProfile.BeneficiaryContactEmail, DbType.String, shipProfile.BeneficiaryContactEmail);
                database.AddInParameter(command, SP.UpdateSHIPProfile.BeneficiaryContactHours, DbType.String, shipProfile.BeneficiaryContactHours);

                database.AddInParameter(command, SP.UpdateSHIPProfile.AdminAgencyContactName, DbType.String, shipProfile.AdminAgencyContactName);
                database.AddInParameter(command, SP.UpdateSHIPProfile.AdminAgencyName, DbType.String, shipProfile.AdminAgencyName);
                database.AddInParameter(command, SP.UpdateSHIPProfile.AdminAgencyContactTitle, DbType.String, shipProfile.AdminAgencyContactTitle);
                database.AddInParameter(command, SP.UpdateSHIPProfile.AdminAgencyAddress, DbType.String, shipProfile.AdminAgencyAddress);
                database.AddInParameter(command, SP.UpdateSHIPProfile.AdminAgencyCity, DbType.String, shipProfile.AdminAgencyCity);
                database.AddInParameter(command, SP.UpdateSHIPProfile.AdminAgencyZipcode, DbType.String, shipProfile.AdminAgencyZipcode);
                database.AddInParameter(command, SP.UpdateSHIPProfile.AdminAgencyPhone, DbType.String, shipProfile.AdminAgencyPhone);
                database.AddInParameter(command, SP.UpdateSHIPProfile.AdminAgencyFax, DbType.String, shipProfile.AdminAgencyFax);
                database.AddInParameter(command, SP.UpdateSHIPProfile.AdminAgencyEmail, DbType.String, shipProfile.AdminAgencyEmail);

                database.AddInParameter(command, SP.UpdateSHIPProfile.ProgramCoordinatorName, DbType.String, shipProfile.ProgramCoordinatorName);
                database.AddInParameter(command, SP.UpdateSHIPProfile.ProgramCoordinatorAddress, DbType.String, shipProfile.ProgramCoordinatorAddress);
                database.AddInParameter(command, SP.UpdateSHIPProfile.ProgramCoordinatorCity, DbType.String, shipProfile.ProgramCoordinatorCity);
                database.AddInParameter(command, SP.UpdateSHIPProfile.ProgramCoordinatorZipcode, DbType.String, shipProfile.ProgramCoordinatorZipcode);
                database.AddInParameter(command, SP.UpdateSHIPProfile.ProgramCoordinatorPhone, DbType.String, shipProfile.ProgramCoordinatorPhone);
                database.AddInParameter(command, SP.UpdateSHIPProfile.ProgramCoordinatorFax, DbType.String, shipProfile.ProgramCoordinatorFax);
                database.AddInParameter(command, SP.UpdateSHIPProfile.ProgramCoordinatorEmail, DbType.String, shipProfile.ProgramCoordinatorEmail);

                database.AddInParameter(command, SP.UpdateSHIPProfile.AvailableLanguages, DbType.String, shipProfile.AvailableLanguages);
                database.AddInParameter(command, SP.UpdateSHIPProfile.NumberOfVolunteerCounselors, DbType.Int16, shipProfile.NumberOfVolunteerCounselors);
                database.AddInParameter(command, SP.UpdateSHIPProfile.NumberOfStateStaff, DbType.Int16, shipProfile.NumberOfStateStaff);
                database.AddInParameter(command, SP.UpdateSHIPProfile.TotalCounties, DbType.Int16, shipProfile.TotalCounties);
                database.AddInParameter(command, SP.UpdateSHIPProfile.NumberOfCountiesServed, DbType.Int16, shipProfile.NumberOfCountiesServed);
                database.AddInParameter(command, SP.UpdateSHIPProfile.NumberOfSponsors, DbType.Int16, shipProfile.NumberOfSponsors);
                database.AddInParameter(command, SP.UpdateSHIPProfile.LastUpdatedBy, DbType.Int16, shipProfile.LastUpdatedBy);

                //New fields: added by Lavanya Maram - 04/25/2013

                database.AddInParameter(command, SP.UpdateSHIPProfile.StateOversightAgency, DbType.String, shipProfile.StateOversightAgency);
                database.AddInParameter(command, SP.UpdateSHIPProfile.NumberOfPaidStaff, DbType.Int16, shipProfile.NumberOfPaidStaff);
                database.AddInParameter(command, SP.UpdateSHIPProfile.NumberOfCoordinators, DbType.Int16, shipProfile.NumberOfCoordinators);
                database.AddInParameter(command, SP.UpdateSHIPProfile.NumberOfCertifiedCounselors, DbType.Int16, shipProfile.NumberOfCertifiedCounselors);
                database.AddInParameter(command, SP.UpdateSHIPProfile.NumberOfEligibleBeneficiaries, DbType.String, shipProfile.NumberOfEligibleBeneficiaries);
                database.AddInParameter(command, SP.UpdateSHIPProfile.NumberOfBeneficiaryContacts, DbType.String, shipProfile.NumberOfBeneficiaryContacts);
                database.AddInParameter(command, SP.UpdateSHIPProfile.LocalAgencies, DbType.String, shipProfile.LocalAgencies);
                database.AddInParameter(command, SP.UpdateSHIPProfile.Longitude, DbType.Double, shipProfile.Longitude);
                database.AddInParameter(command, SP.UpdateSHIPProfile.Latitude, DbType.Double, shipProfile.Latitude);
                
                database.ExecuteNonQuery(command);
            }
        }

        public DataSet GetSHIPProfileAgencyDetails(string StateFIPS)
        {
         using (var command = database.GetStoredProcCommand("GetSHIPProfileAgencyDetails"))
            {

                database.AddInParameter(command, "@StateFIPS", DbType.StringFixedLength, StateFIPS);
                database.AddInParameter(command, "@IncludeInactive", DbType.Boolean, false);
                database.AddInParameter(command, "@IncludeAgencyLocations", DbType.Boolean, true);

                DataSet dsProfileAgencies = database.ExecuteDataSet(command);

                return dsProfileAgencies;

            }

          
        }

        public DataSet GetSHIPProfileAgencyDetailsByAddress(Double Latitude, Double Longitude, string state, int Radius)
        {
            using (var command = database.GetStoredProcCommand("GetSHIPProfileAgencyDetailsByAddress"))
            {
                database.AddInParameter(command, "@Latitude", DbType.Double, Latitude);
                database.AddInParameter(command, "@Longitude", DbType.Double, Longitude);
                database.AddInParameter(command, "@State", DbType.String, state);
                database.AddInParameter(command, "@Radius", DbType.Int32, Radius);
                database.AddInParameter(command, "@IncludeInactive", DbType.Boolean, false);
                database.AddInParameter(command, "@IncludeAgencyLocations", DbType.Boolean, true);

                DataSet dsProfileAgencies = database.ExecuteDataSet(command);

                return dsProfileAgencies;

            }


        }
    
    }
}
