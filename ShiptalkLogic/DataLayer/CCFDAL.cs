using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using T = ShiptalkLogic.Constants.Tables;
using SP = ShiptalkLogic.Constants.StoredProcs;
using ShiptalkLogic.BusinessObjects.UI;

namespace ShiptalkLogic.DataLayer
{
    internal class CCFDAL : FormDALBase
    {

        public string GetNextAutoAssignedClientID(string agencyCode)
        {
            if (string.IsNullOrEmpty(agencyCode))
                throw new ArgumentNullException("agencyCode");

            using (DbCommand command = database.GetStoredProcCommand("dbo.GetNextAutoAssignedClientID"))
            {
                database.AddInParameter(command, SP.GetNextAutoAssignedClientID.AgencyCode, DbType.String, agencyCode);

                return database.ExecuteScalar(command).ToString();
            }
        }



        public  bool DeleteClientContact(int clientContactID, out string FailureReason)
        {
            bool result = false;
            using (var command = database.GetStoredProcCommand("dbo.DeleteClientContactById"))
            {
                database.AddInParameter(command, "@ClientContactID", DbType.Int32, clientContactID);

                database.AddOutParameter(command, "@IsSuccess", DbType.Boolean, 1);
                database.AddOutParameter(command, "@FailureReason", DbType.String, 500);

                database.ExecuteNonQuery(command);

                Object outObj = command.Parameters["@FailureReason"].Value;

                string failureMessage = string.Empty;
                if (outObj != null && outObj != DBNull.Value)
                    failureMessage = (string)outObj;
                else
                    result = true;

                //Finally set the output param value
                FailureReason = failureMessage;
            }
            return result;
        }

        /// <summary>
        /// Deletes an Uploaded Client Contact Record
        /// </summary>
        /// <param name="AgencyID"></param>
        /// <param name="BatchStateUniqueID"></param>
        public void DeleteUploadedClientContactRecord(string StateFIPS, string AgencyCode, string @BatchStateUniqueID)
        {
            using (var command = database.GetStoredProcCommand("dbo.DeleteUploadClientContact"))
            {
                database.AddInParameter(command, "@AgencyCode", DbType.String, AgencyCode);
                database.AddInParameter(command, "@BatchStateUniqueID", DbType.String, BatchStateUniqueID);
                database.AddInParameter(command, "@StateFIPS", DbType.String, StateFIPS);
                database.ExecuteNonQuery(command);
            }

        }
        /// <summary>
        /// Update a client contact record  based on Agency Code, BatchStateUniqueID
        /// </summary>
        /// <param name="clientContact"></param>
        public void UpdateUploadedClientContactRecord(ClientContact clientContact)
        {
            if (clientContact == null)
                throw new ArgumentNullException("clientContact");

            using (var command = database.GetStoredProcCommand("dbo.UpdateUploadClientContact"))
            {
                database.AddInParameter(command, SP.CreateClientContact.CounselorUserID, DbType.Int32, clientContact.CounselorUserId);
                database.AddInParameter(command, SP.CreateClientContact.SubmitterUserID, DbType.Int32, clientContact.CreatedBy);
                database.AddInParameter(command, SP.CreateClientContact.IsBatchUploadData, DbType.Boolean, clientContact.IsBatchUploadData);
                database.AddInParameter(command, SP.CreateClientContact.AgencyID, DbType.Int32, clientContact.AgencyId);
                database.AddInParameter(command, SP.CreateClientContact.CountyOfCounselorLocation, DbType.String, clientContact.CounselorCountyCode);
                database.AddInParameter(command, SP.CreateClientContact.ZIPCodeOfCounselorLocation, DbType.String, clientContact.CounselorZIPCode);
                database.AddInParameter(command, SP.CreateClientContact.ClientID, DbType.String, clientContact.ClientId);
                database.AddInParameter(command, SP.CreateClientContact.DateOfContact, DbType.DateTime, clientContact.DateOfContact);
                database.AddInParameter(command, SP.CreateClientContact.StateSpecificClientID, DbType.String, clientContact.StateSpecificClientId);
                database.AddInParameter(command, SP.CreateClientContact.BatchStateUniqueID, DbType.String, clientContact.StateSpecificClientId);
                database.AddInParameter(command, SP.CreateClientContact.HowLearnedSHIP, DbType.Int32, clientContact.ClientLearnedAboutSHIP);
                database.AddInParameter(command, SP.CreateClientContact.FirstVSContinuingService, DbType.Int32, clientContact.ClientFirstVsContinuingContact);
                database.AddInParameter(command, SP.CreateClientContact.MethodOfContact, DbType.Int32, clientContact.ClientMethodOfContact);
                database.AddInParameter(command, SP.CreateClientContact.ClientFirstName, DbType.String, clientContact.ClientFirstName);
                database.AddInParameter(command, SP.CreateClientContact.ClientLastName, DbType.String, clientContact.ClientLastName);
                database.AddInParameter(command, SP.CreateClientContact.ClientPhone, DbType.String, clientContact.ClientPhoneNumber);
                database.AddInParameter(command, SP.CreateClientContact.RepresentativeFirstName, DbType.String, clientContact.RepresentativeFirstName);
                database.AddInParameter(command, SP.CreateClientContact.RepresentativeLastName, DbType.String, clientContact.RepresentativeLastName);
                database.AddInParameter(command, SP.CreateClientContact.ZIPCodeOfClientRes, DbType.String, clientContact.ClientZIPCode);
                database.AddInParameter(command, SP.CreateClientContact.CountyCodeOfClientRes, DbType.String, clientContact.ClientCountyCode);
                database.AddInParameter(command, SP.CreateClientContact.ClientAgeGroup, DbType.Int32, clientContact.ClientAgeGroup);
                database.AddInParameter(command, SP.CreateClientContact.ClientGender, DbType.Int32, clientContact.ClientGender);
                database.AddInParameter(command, SP.CreateClientContact.PrimaryLanguageOtherThanEnglish, DbType.Int32, clientContact.ClientPrimaryLanguageOtherThanEnglish);
                database.AddInParameter(command, SP.CreateClientContact.MonthlyIncome, DbType.Int32, clientContact.ClientMonthlyIncome);
                database.AddInParameter(command, SP.CreateClientContact.ClientAsset, DbType.Int32, clientContact.ClientAssets);
                database.AddInParameter(command, SP.CreateClientContact.Disability, DbType.Int32, clientContact.ClientReceivingSSOrMedicareDisability);
                database.AddInParameter(command, SP.CreateClientContact.DualMental, DbType.Int32, clientContact.ClientDualEligble);
                database.AddInParameter(command, SP.CreateClientContact.HoursSpent, DbType.Int32, clientContact.HoursSpent);
                database.AddInParameter(command, SP.CreateClientContact.MinutesSpent, DbType.Int32, clientContact.MinutesSpent);
                database.AddInParameter(command, SP.CreateClientContact.CurrentStatus, DbType.Int32, clientContact.ClientStatus);
                database.AddInParameter(command, SP.CreateClientContact.Comments, DbType.String, clientContact.Comments);
                database.AddInParameter(command, SP.CreateClientContact.ClientRaceDescriptions, DbType.String,
                    string.Join(",", (from race in clientContact.ClientRaceDescriptions select ((int)race).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.MedicarePrescriptionDrugCoverageTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.MedicarePrescriptionDrugCoverageTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.MedicareAdvantageTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.MedicareAdvantageTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.PartDLowIncomeSubsidyTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.PartDLowIncomeSubsidyTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.MedicareSupplementTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.MedicareSupplementTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.OtherPrescriptionAssistanceTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.OtherPrescriptionAssistanceTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.OtherPreseriptionAssitanceSpecified, DbType.String, clientContact.OtherPrescriptionAssitanceSpecified);
                database.AddInParameter(command, SP.CreateClientContact.MedicaidTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.MedicaidTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.MedicarePartsAandBTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.MedicarePartsAandBTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.OtherDrugTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.OtherDrugTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.OtherDrugTopicsSpecified, DbType.String, clientContact.OtherDrugTopicsSpecified);
                database.AddInParameter(command, SP.CreateClientContact.CMSSpecialUseFields, DbType.Xml, clientContact.CMSSpecialUseFields.SerializeToXmlString());
                database.AddInParameter(command, SP.CreateClientContact.StateSpecialUseFields, DbType.Xml, clientContact.StateSpecialUseFields.SerializeToXmlString());
                if (clientContact.Id > 0)
                {
                    database.AddInParameter(command, SP.CreateClientContact.ClientContactID, DbType.Int32, clientContact.Id);
                }
                database.ExecuteNonQuery(command);

            }
        }

        public int CreateClientContact(ClientContact clientContact)
        {
            if (clientContact == null)
                throw new ArgumentNullException("clientContact");

            using (var command = database.GetStoredProcCommand("dbo.CreateClientContact"))
            {
                database.AddInParameter(command, SP.CreateClientContact.CounselorUserID, DbType.Int32, clientContact.CounselorUserId);
                database.AddInParameter(command, SP.CreateClientContact.SubmitterUserID, DbType.Int32, clientContact.CreatedBy);
                database.AddInParameter(command, SP.CreateClientContact.IsBatchUploadData, DbType.Boolean, clientContact.IsBatchUploadData);
                database.AddInParameter(command, SP.CreateClientContact.AgencyID, DbType.Int32, clientContact.AgencyId);
                database.AddInParameter(command, SP.CreateClientContact.CountyOfCounselorLocation, DbType.String, clientContact.CounselorCountyCode);
                database.AddInParameter(command, SP.CreateClientContact.ZIPCodeOfCounselorLocation, DbType.String, clientContact.CounselorZIPCode);
                database.AddInParameter(command, SP.CreateClientContact.ClientID, DbType.String, clientContact.ClientId);
                database.AddInParameter(command, SP.CreateClientContact.DateOfContact, DbType.DateTime, clientContact.DateOfContact);
                database.AddInParameter(command, SP.CreateClientContact.StateSpecificClientID, DbType.String, clientContact.StateSpecificClientId);
                database.AddInParameter(command, SP.CreateClientContact.BatchStateUniqueID, DbType.String, clientContact.BatchStateUniqueID);
                database.AddInParameter(command, SP.CreateClientContact.HowLearnedSHIP, DbType.Int32, clientContact.ClientLearnedAboutSHIP);
                database.AddInParameter(command, SP.CreateClientContact.FirstVSContinuingService, DbType.Int32, clientContact.ClientFirstVsContinuingContact);
                database.AddInParameter(command, SP.CreateClientContact.MethodOfContact, DbType.Int32, clientContact.ClientMethodOfContact);
                database.AddInParameter(command, SP.CreateClientContact.ClientFirstName, DbType.String, clientContact.ClientFirstName);
                database.AddInParameter(command, SP.CreateClientContact.ClientLastName, DbType.String, clientContact.ClientLastName);
                database.AddInParameter(command, SP.CreateClientContact.ClientPhone, DbType.String, clientContact.ClientPhoneNumber);
                database.AddInParameter(command, SP.CreateClientContact.RepresentativeFirstName, DbType.String, clientContact.RepresentativeFirstName);
                database.AddInParameter(command, SP.CreateClientContact.RepresentativeLastName, DbType.String, clientContact.RepresentativeLastName);
                database.AddInParameter(command, SP.CreateClientContact.ZIPCodeOfClientRes, DbType.String, clientContact.ClientZIPCode);
                database.AddInParameter(command, SP.CreateClientContact.CountyCodeOfClientRes, DbType.String, clientContact.ClientCountyCode);
                database.AddInParameter(command, SP.CreateClientContact.ClientAgeGroup, DbType.Int32, clientContact.ClientAgeGroup);
                database.AddInParameter(command, SP.CreateClientContact.ClientGender, DbType.Int32, clientContact.ClientGender);
                database.AddInParameter(command, SP.CreateClientContact.PrimaryLanguageOtherThanEnglish, DbType.Int32, clientContact.ClientPrimaryLanguageOtherThanEnglish);
                database.AddInParameter(command, SP.CreateClientContact.MonthlyIncome, DbType.Int32, clientContact.ClientMonthlyIncome);
                database.AddInParameter(command, SP.CreateClientContact.ClientAsset, DbType.Int32, clientContact.ClientAssets);
                database.AddInParameter(command, SP.CreateClientContact.Disability, DbType.Int32, clientContact.ClientReceivingSSOrMedicareDisability);
                database.AddInParameter(command, SP.CreateClientContact.DualMental, DbType.Int32, clientContact.ClientDualEligble);
                database.AddInParameter(command, SP.CreateClientContact.HoursSpent, DbType.Int32, clientContact.HoursSpent);
                database.AddInParameter(command, SP.CreateClientContact.MinutesSpent, DbType.Int32, clientContact.MinutesSpent);
                database.AddInParameter(command, SP.CreateClientContact.CurrentStatus, DbType.Int32, clientContact.ClientStatus);
                database.AddInParameter(command, SP.CreateClientContact.Comments, DbType.String, clientContact.Comments);
                database.AddInParameter(command, SP.CreateClientContact.ClientRaceDescriptions, DbType.String,
                    string.Join(",", (from race in clientContact.ClientRaceDescriptions select ((int)race).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.MedicarePrescriptionDrugCoverageTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.MedicarePrescriptionDrugCoverageTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.MedicareAdvantageTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.MedicareAdvantageTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.PartDLowIncomeSubsidyTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.PartDLowIncomeSubsidyTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.MedicareSupplementTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.MedicareSupplementTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.OtherPrescriptionAssistanceTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.OtherPrescriptionAssistanceTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.OtherPreseriptionAssitanceSpecified, DbType.String, clientContact.OtherPrescriptionAssitanceSpecified);
                database.AddInParameter(command, SP.CreateClientContact.MedicaidTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.MedicaidTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.MedicarePartsAandBTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.MedicarePartsAandBTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.OtherDrugTopics, DbType.String,
                    string.Join(",", (from topic in clientContact.OtherDrugTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreateClientContact.OtherDrugTopicsSpecified, DbType.String, clientContact.OtherDrugTopicsSpecified);
                database.AddInParameter(command, SP.CreateClientContact.CMSSpecialUseFields, DbType.Xml, clientContact.CMSSpecialUseFields.SerializeToXmlString());
                database.AddInParameter(command, SP.CreateClientContact.StateSpecialUseFields, DbType.Xml, clientContact.StateSpecialUseFields.SerializeToXmlString());
                database.AddOutParameter(command, SP.CreateClientContact.ClientContactID, DbType.Int32, 6);

                database.ExecuteNonQuery(command);

                return (int)database.GetParameterValue(command, SP.CreateClientContact.ClientContactID);
            }
        }

        public ClientContact GetClientContact(int id)
        {
            ClientContact clientContact = null;

            using (var command = database.GetStoredProcCommand("dbo.GetClientContact"))
            {
                database.AddInParameter(command, SP.GetClientContact.ClientContactID, DbType.Int32, id);

                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        //Init. client contact data.
                        clientContact = new ClientContact
                                            {
                                                Id = reader.GetDefaultIfDBNull(T.ClientContact.ClientContactID, GetNullableInt32, null),
                                                AgencyCode = reader.GetDefaultIfDBNull(T.Agency.AgencyCode, GetString, null),
                                                AgencyId = reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetInt32, 0),
                                                AgencyName = reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, null),
                                                AgencyState = new State(reader.GetDefaultIfDBNull(T.Agency.StateFIPS, GetString, null)),
                                                ClientAgeGroup = (ClientAgeGroup)reader.GetDefaultIfDBNull(T.ClientContact.ClientAgeGroup, GetNullableInt16, null),
                                                ClientAssets = (ClientAssets)reader.GetDefaultIfDBNull(T.ClientContact.ClientAsset, GetNullableInt16, null),
                                                ClientCountyCode = reader.GetDefaultIfDBNull(T.ClientContact.CountycodeOfClientRes, GetString, null),
                                                ClientCountyName = reader.GetDefaultIfDBNull(T.ClientContact.CountyNameOfClientRes, GetString, null),
                                                ClientDualEligble = (ClientDualEligble)reader.GetDefaultIfDBNull(T.ClientContact.DualMental, GetNullableInt16, null),
                                                ClientFirstName = reader.GetDefaultIfDBNull(T.ClientContact.ClientFirstName, GetString, null),
                                                ClientFirstVsContinuingContact = (ClientFirstVsContinuingContact)reader.GetDefaultIfDBNull(T.ClientContact.FirstVSContinuingService, GetNullableInt16, null),
                                                ClientGender = (ClientGender)reader.GetDefaultIfDBNull(T.ClientContact.ClientGender, GetNullableInt16, null),
                                                ClientId = reader.GetDefaultIfDBNull(T.ClientContact.ClientID, GetString, null),
                                                ClientLastName = reader.GetDefaultIfDBNull(T.ClientContact.ClientLastName, GetString, null),
                                                ClientLearnedAboutSHIP = (ClientLearnedAboutSHIP)reader.GetDefaultIfDBNull(T.ClientContact.HowLearnedSHIP, GetNullableInt16, null),
                                                ClientMethodOfContact = (ClientMethodOfContact)reader.GetDefaultIfDBNull(T.ClientContact.MethodOfContact, GetNullableInt16, null),
                                                ClientMonthlyIncome = (ClientMonthlyIncome)reader.GetDefaultIfDBNull(T.ClientContact.MonthlyIncome, GetNullableInt16, null),
                                                ClientPhoneNumber = reader.GetDefaultIfDBNull(T.ClientContact.ClientPhone, GetString, null),
                                                ClientPrimaryLanguageOtherThanEnglish =
                                                    (ClientPrimaryLanguageOtherThanEnglish)reader.GetDefaultIfDBNull(T.ClientContact.PrimaryLanguageOtherThanEnglish, GetNullableInt16, null),
                                                ClientRaceDescriptions = new List<ClientRaceDescription>(),
                                                ClientReceivingSSOrMedicareDisability =
                                                    (ClientReceivingSSOrMedicareDisability)reader.GetDefaultIfDBNull(T.ClientContact.Disability, GetNullableInt16, null),
                                                ClientStatus = (ClientStatus)reader.GetDefaultIfDBNull(T.ClientContact.CurrentStatus, GetNullableInt16, null),
                                                ClientZIPCode = reader.GetDefaultIfDBNull(T.ClientContact.ZIPCodeOfClientRes, GetString, null),
                                                Comments = reader.GetDefaultIfDBNull(T.ClientContact.Comments, GetString, null),
                                                CounselorCountyCode = reader.GetDefaultIfDBNull(T.ClientContact.CountyOfCounselorLocation, GetString, null),
                                                CounselorCountyName = reader.GetDefaultIfDBNull(T.ClientContact.CountyNameOfCounselorLocation, GetString, null),
                                                Counselor = new UserProfile(),
                                                Submitter = new UserProfile(),
                                                Reviewer = new UserProfile(),
                                                CounselorZIPCode = reader.GetDefaultIfDBNull(T.ClientContact.ZIPCodeOfCounselorLocation, GetString, null),
                                                CreatedBy = reader.GetDefaultIfDBNull(T.ClientContact.SubmitterUserID, GetNullableInt32, null),
                                                CreatedDate = reader.GetDefaultIfDBNull(T.ClientContact.CreatedDate, GetNullableDateTime, null),
                                                DateOfContact = reader.GetDefaultIfDBNull(T.ClientContact.DateOfContact, GetDateTime, DateTime.MinValue),
                                                HoursSpent = reader.GetDefaultIfDBNull<short>(T.ClientContact.HoursSpent, GetInt16, 0),
                                                IsBatchUploadData = reader.GetDefaultIfDBNull(T.ClientContact.IsBatchUploadData, GetBool, false),
                                                LastUpdatedBy = reader.GetDefaultIfDBNull(T.ClientContact.LastUpdatedBy, GetNullableInt32, null),
                                                LastUpdatedDate = reader.GetDefaultIfDBNull(T.ClientContact.LastUpdatedDate, GetNullableDateTime, null),
                                                MinutesSpent = reader.GetDefaultIfDBNull<short>(T.ClientContact.MinutesSpent, GetInt16, 0),
                                                MedicarePrescriptionDrugCoverageTopics = new List<Topic_MedicarePrescriptionDrugCoverage_PartD>(),
                                                PartDLowIncomeSubsidyTopics = new List<Topic_PartDLowIncomeSubsidy_LISOrExtraHelp>(),
                                                OtherPrescriptionAssistanceTopics = new List<Topic_OtherPrescriptionAssistance>(),
                                                MedicarePartsAandBTopics = new List<Topic_MEDICARE_PartsAandB>(),
                                                MedicareAdvantageTopics = new List<Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost>(),
                                                MedicareSupplementTopics = new List<Topic_MedicareSupplementOrSelect>(),
                                                MedicaidTopics = new List<Topic_MEDICAID>(),
                                                OtherDrugTopics = new List<Topic_OTHER>(),
                                                CMSSpecialUseFields = new List<SpecialFieldValue>(),
                                                StateSpecialUseFields = new List<SpecialFieldValue>(),
                                                RepresentativeFirstName = reader.GetDefaultIfDBNull(T.ClientContact.RepresentativeFirstName, GetString, null),
                                                RepresentativeLastName = reader.GetDefaultIfDBNull(T.ClientContact.RepresentativeLastName, GetString, null),
                                                ReviewedDate = reader.GetDefaultIfDBNull(T.ClientContact.ReviewedDate, GetNullableDateTime, null),
                                                StateSpecificClientId = reader.GetDefaultIfDBNull(T.ClientContact.StateSpecificClientID, GetString, null).Trim()
                                            };

                        reader.NextResult();

                        Func<IDataReader, UserProfile> CreateUserProfile =
                            record => new UserProfile
                                          {
                                              UserId = record.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                              FirstName = record.GetDefaultIfDBNull(T.User.FirstName, GetString, null),
                                              MiddleName = record.GetDefaultIfDBNull(T.User.MiddleName, GetString, null),
                                              LastName = record.GetDefaultIfDBNull(T.User.LastName, GetString, null)
                                          };

                        while (reader.Read())
                        {
                            var descriptor = (Descriptor?)reader.GetDefaultIfDBNull(T.Descriptor.DescriptorID, GetNullableInt32, null);

                            if (descriptor.HasValue)
                            {
                                switch (descriptor.Value)
                                {
                                    case Descriptor.Counselor:
                                        clientContact.Counselor = CreateUserProfile(reader);
                                        clientContact.CounselorUserId = clientContact.Counselor.UserId;

                                        break;
                                    case Descriptor.DataSubmitter:
                                        clientContact.Submitter = CreateUserProfile(reader);

                                        break;
                                    case Descriptor.DataEditor_Reviewer:
                                        clientContact.Reviewer = CreateUserProfile(reader);

                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            var groupId = reader.GetDefaultIfDBNull<short>(T.ClientTopic.GroupID, GetInt16, 0);

                            switch (groupId)
                            {
                                case 1:
                                    clientContact.MedicarePrescriptionDrugCoverageTopics.Add(
                                        (Topic_MedicarePrescriptionDrugCoverage_PartD)
                                        reader.GetDefaultIfDBNull<short>(T.ClientTopic.ClientTopicID, GetInt16, 0));

                                    break;
                                case 2:
                                    clientContact.PartDLowIncomeSubsidyTopics.Add(
                                        (Topic_PartDLowIncomeSubsidy_LISOrExtraHelp)
                                        reader.GetDefaultIfDBNull<short>(T.ClientTopic.ClientTopicID, GetInt16, 0));

                                    break;
                                case 3:

                                    var other = (Topic_OtherPrescriptionAssistance)reader.GetDefaultIfDBNull<short>(T.ClientTopic.ClientTopicID, GetInt16, 0);

                                    clientContact.OtherPrescriptionAssistanceTopics.Add(other);

                                    if (other == Topic_OtherPrescriptionAssistance.Other)
                                        clientContact.OtherPrescriptionAssitanceSpecified =
                                            reader.GetDefaultIfDBNull(T.ClientTopic.OtherDescription, GetString, null);

                                    break;
                                case 4:
                                    clientContact.MedicarePartsAandBTopics.Add(
                                        (Topic_MEDICARE_PartsAandB)
                                        reader.GetDefaultIfDBNull<short>(T.ClientTopic.ClientTopicID, GetInt16, 0));

                                    break;
                                case 5:
                                    clientContact.MedicareAdvantageTopics.Add(
                                        (Topic_MedicareAdvantage_HMO_POS_PPO_PFFS_SNP_MSA_Cost)
                                        reader.GetDefaultIfDBNull<short>(T.ClientTopic.ClientTopicID, GetInt16, 0));

                                    break;
                                case 6:
                                    clientContact.MedicareSupplementTopics.Add(
                                        (Topic_MedicareSupplementOrSelect)
                                        reader.GetDefaultIfDBNull<short>(T.ClientTopic.ClientTopicID, GetInt16, 0));

                                    break;
                                case 7:
                                    clientContact.MedicaidTopics.Add((Topic_MEDICAID)reader.GetDefaultIfDBNull<short>(T.ClientTopic.ClientTopicID, GetInt16, 0));

                                    break;
                                case 8:
                                    var topicOther = (Topic_OTHER)reader.GetDefaultIfDBNull<short>(T.ClientTopic.ClientTopicID, GetInt16, 0);

                                    clientContact.OtherDrugTopics.Add(topicOther);

                                    if (topicOther == Topic_OTHER.Other)
                                        clientContact.OtherDrugTopicsSpecified =
                                            reader.GetDefaultIfDBNull(T.ClientTopic.OtherDescription, GetString, null);

                                    break;
                                default:
                                    break;
                            }
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            clientContact.ClientRaceDescriptions.Add((ClientRaceDescription)reader.GetDefaultIfDBNull<short>(T.ClientRace.RaceID, GetInt16, 0));
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            var state =
                                new State(reader.GetDefaultIfDBNull(T.SpecialField.StateFIPS, GetString, null));

                            var specialFieldValue = new SpecialFieldValue
                                                        {
                                                            Id = reader.GetDefaultIfDBNull<short>(T.ClientContactSpecialField.SpecialFieldID, GetInt16, 0),
                                                            Name = reader.GetDefaultIfDBNull(T.SpecialField.Name, GetString, string.Empty),
                                                            Value = reader.GetDefaultIfDBNull(T.ClientContactSpecialField.SpecialFieldValue, GetString, string.Empty)
                                                        };

                            if (state.Code == "99")
                                clientContact.CMSSpecialUseFields.Add(specialFieldValue);
                            else
                                clientContact.StateSpecialUseFields.Add(specialFieldValue);
                        }
                    }
                }
            }

            return clientContact;
        }

        public void UpdateClientContact(ClientContact clientContact)
        {
            if (clientContact == null)
                throw new ArgumentNullException("clientContact");

            using (var command = database.GetStoredProcCommand("dbo.UpdateClientContact"))
            {
                database.AddInParameter(command, SP.UpdateClientContact.UserID, DbType.Int32,
                                        clientContact.UserId);
                database.AddInParameter(command, SP.UpdateClientContact.ClientContactID, DbType.Int32,
                                        clientContact.Id.GetValueOrDefault(0));
                database.AddInParameter(command, SP.UpdateClientContact.CounselorUserID, DbType.Int32,
                                        clientContact.CounselorUserId);
                database.AddInParameter(command, SP.UpdateClientContact.ReviewerUserID, DbType.Int32,
                                (clientContact.Reviewer.UserId > 0) ? (int?)clientContact.Reviewer.UserId : null);
                database.AddInParameter(command, SP.UpdateClientContact.AgencyID, DbType.Int32, clientContact.AgencyId);
                database.AddInParameter(command, SP.UpdateClientContact.CountyOfCounselorLocation, DbType.String,
                                        clientContact.CounselorCountyCode);
                database.AddInParameter(command, SP.UpdateClientContact.ZIPCodeOfCounselorLocation, DbType.String,
                                        clientContact.CounselorZIPCode);
                database.AddInParameter(command, SP.UpdateClientContact.ClientID, DbType.String, clientContact.ClientId);
                database.AddInParameter(command, SP.UpdateClientContact.DateOfContact, DbType.DateTime,
                                        clientContact.DateOfContact);
                database.AddInParameter(command, SP.UpdateClientContact.HowLearnedSHIP, DbType.Int32,
                                        clientContact.ClientLearnedAboutSHIP);
                database.AddInParameter(command, SP.UpdateClientContact.FirstVSContinuingService, DbType.Int32,
                                        clientContact.ClientFirstVsContinuingContact);
                database.AddInParameter(command, SP.UpdateClientContact.MethodOfContact, DbType.Int32,
                                        clientContact.ClientMethodOfContact);
                database.AddInParameter(command, SP.UpdateClientContact.ClientFirstName, DbType.String,
                                        clientContact.ClientFirstName);
                database.AddInParameter(command, SP.UpdateClientContact.ClientLastName, DbType.String,
                                        clientContact.ClientLastName);
                database.AddInParameter(command, SP.UpdateClientContact.ClientPhone, DbType.String,
                                        clientContact.ClientPhoneNumber);
                database.AddInParameter(command, SP.UpdateClientContact.RepresentativeFirstName, DbType.String,
                                        clientContact.RepresentativeFirstName);
                database.AddInParameter(command, SP.UpdateClientContact.RepresentativeLastName, DbType.String,
                                        clientContact.RepresentativeLastName);
                database.AddInParameter(command, SP.UpdateClientContact.ZIPCodeOfClientRes, DbType.String,
                                        clientContact.ClientZIPCode);
                database.AddInParameter(command, SP.UpdateClientContact.CountyCodeOfClientRes, DbType.String,
                                        clientContact.ClientCountyCode);
                database.AddInParameter(command, SP.UpdateClientContact.ClientAgeGroup, DbType.Int32,
                                        clientContact.ClientAgeGroup);
                database.AddInParameter(command, SP.UpdateClientContact.ClientGender, DbType.Int32,
                                        clientContact.ClientGender);
                database.AddInParameter(command, SP.UpdateClientContact.PrimaryLanguageOtherThanEnglish, DbType.Int32,
                                        clientContact.ClientPrimaryLanguageOtherThanEnglish);
                database.AddInParameter(command, SP.UpdateClientContact.MonthlyIncome, DbType.Int32,
                                        clientContact.ClientMonthlyIncome);
                database.AddInParameter(command, SP.UpdateClientContact.ClientAsset, DbType.Int32,
                                        clientContact.ClientAssets);
                database.AddInParameter(command, SP.UpdateClientContact.Disability, DbType.Int32,
                                        clientContact.ClientReceivingSSOrMedicareDisability);
                database.AddInParameter(command, SP.UpdateClientContact.DualMental, DbType.Int32,
                                        clientContact.ClientDualEligble);
                database.AddInParameter(command, SP.UpdateClientContact.HoursSpent, DbType.Int32,
                                        clientContact.HoursSpent);
                database.AddInParameter(command, SP.UpdateClientContact.MinutesSpent, DbType.Int32,
                                        clientContact.MinutesSpent);
                database.AddInParameter(command, SP.UpdateClientContact.CurrentStatus, DbType.Int32,
                                        clientContact.ClientStatus);
                database.AddInParameter(command, SP.UpdateClientContact.Comments, DbType.String, clientContact.Comments);
                database.AddInParameter(command, SP.UpdateClientContact.ClientRaceDescriptions, DbType.String,
                                        string.Join(",", (from race in clientContact.ClientRaceDescriptions
                                                          select ((int)race).ToString()).ToArray()));
                database.AddInParameter(command, SP.UpdateClientContact.MedicarePrescriptionDrugCoverageTopics, DbType.String,
                                        string.Join(",", (from topic in clientContact.MedicarePrescriptionDrugCoverageTopics
                                                          select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.UpdateClientContact.MedicareAdvantageTopics, DbType.String,
                                        string.Join(",", (from topic in clientContact.MedicareAdvantageTopics
                                                          select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.UpdateClientContact.PartDLowIncomeSubsidyTopics, DbType.String,
                                        string.Join(",", (from topic in clientContact.PartDLowIncomeSubsidyTopics
                                                          select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.UpdateClientContact.MedicareSupplementTopics, DbType.String,
                                        string.Join(",", (from topic in clientContact.MedicareSupplementTopics
                                                          select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.UpdateClientContact.OtherPrescriptionAssistanceTopics, DbType.String,
                                        string.Join(",", (from topic in clientContact.OtherPrescriptionAssistanceTopics
                                                          select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.UpdateClientContact.OtherPreseriptionAssitanceSpecified, DbType.String,
                                        clientContact.OtherPrescriptionAssitanceSpecified);
                database.AddInParameter(command, SP.UpdateClientContact.MedicaidTopics, DbType.String,
                                        string.Join(",", (from topic in clientContact.MedicaidTopics
                                                          select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.UpdateClientContact.MedicarePartsAandBTopics, DbType.String,
                                        string.Join(",", (from topic in clientContact.MedicarePartsAandBTopics
                                                          select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.UpdateClientContact.OtherDrugTopics, DbType.String,
                                        string.Join(",", (from topic in clientContact.OtherDrugTopics
                                                          select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.UpdateClientContact.OtherDrugTopicsSpecified, DbType.String,
                                        clientContact.OtherDrugTopicsSpecified);
                database.AddInParameter(command, SP.UpdateClientContact.CMSSpecialUseFields, DbType.Xml,
                                        clientContact.CMSSpecialUseFields.SerializeToXmlString());
                database.AddInParameter(command, SP.UpdateClientContact.StateSpecialUseFields, DbType.Xml,
                                        clientContact.StateSpecialUseFields.SerializeToXmlString());

                database.ExecuteNonQuery(command);
            }
        }

        public IEnumerable<SearchClientContactsViewData> GetRecentClientContacts(int userId)
        {
            var clientContacts = new List<SearchClientContactsViewData>();

            using (var command = database.GetStoredProcCommand("dbo.GetRecentClientContacts"))
            {
                database.AddInParameter(command, SP.GetRecentClientContacts.UserId, DbType.Int32, userId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        clientContacts.Add(new SearchClientContactsViewData
                        {
                            Id = reader.GetDefaultIfDBNull(T.ClientContact.ClientContactID, GetNullableInt32, null),
                            AgencyCode = reader.GetDefaultIfDBNull(T.Agency.AgencyCode, GetString, null),
                            AgencyName = reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, null),
                            //AgencyId = reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetInt32, 0),
                           // AgencyState = new State(reader.GetDefaultIfDBNull(T.Agency.StateFIPS, GetString, null)),
                            DateOfContact = reader.GetDefaultIfDBNull(T.ClientContact.DateOfContact, GetDateTime, DateTime.MinValue),
                            ClientFirstName = reader.GetDefaultIfDBNull(T.ClientContact.ClientFirstName, GetString, null),
                            AutoAssignedClientId = reader.GetDefaultIfDBNull(T.ClientContact.ClientID, GetString, null),
                            ClientLastName = reader.GetDefaultIfDBNull(T.ClientContact.ClientLastName, GetString, null),
                            ClientPhone = reader.GetDefaultIfDBNull(T.ClientContact.ClientPhone, GetString, null),
                            CounselorUserID = reader.GetDefaultIfDBNull(T.ClientContact.CounselorUserID, GetInt32, 0),
                            SubmitterUserID = reader.GetDefaultIfDBNull(T.ClientContact.SubmitterUserID, GetInt32, 0),
                            ReviewerUserID = reader.GetDefaultIfDBNull(T.ClientContact.ReviewerUserID, GetInt32, 0),
                            RepresentativeFirstName = reader.GetDefaultIfDBNull(T.ClientContact.RepresentativeFirstName, GetString, null),
                            RepresentativeLastName = reader.GetDefaultIfDBNull(T.ClientContact.RepresentativeLastName, GetString, null),
                            ReviewedDate = reader.GetDefaultIfDBNull(T.ClientContact.ReviewedDate, GetNullableDateTime, null),
                            StateSpecificClientId = reader.GetDefaultIfDBNull(T.ClientContact.StateSpecificClientID, GetString, null)
                        });
                    }
                }

            }

            return clientContacts;
        }

        public IEnumerable<SearchClientContactsViewData> SearchClientContacts(int UserID, int scopeId, string StateFIPS, string keywords, DateTime? fromContactDate, DateTime? toContactDate, int? counselorId, int? submitterId, int? AgencyId)
        {
            var clientContacts = new List<SearchClientContactsViewData>();
            
            using (var command = database.GetStoredProcCommand("SearchClientContacts"))
            {
                //database.AddInParameter(command, SP.SearchClientContacts.Keywords, DbType.String,
                database.AddInParameter(command, SP.SearchClientContacts.UserId, DbType.Int32, UserID);
                database.AddInParameter(command, SP.SearchClientContacts.ScopeId, DbType.Int32, scopeId);
                database.AddInParameter(command, SP.SearchClientContacts.StateFIPS, DbType.String, StateFIPS);
                //    (keywordsBuilder.ToString() == "''") ? null : keywordsBuilder.ToString());
                database.AddInParameter(command, SP.SearchClientContacts.Keywords, DbType.String, string.IsNullOrEmpty(keywords) ? string.Empty : keywords);
                database.AddInParameter(command, SP.SearchClientContacts.FromContactDate, DbType.Date, fromContactDate);
                database.AddInParameter(command, SP.SearchClientContacts.ToContactDate, DbType.Date, toContactDate);
                database.AddInParameter(command, SP.SearchClientContacts.CounselorId, DbType.Int32, counselorId);
                database.AddInParameter(command, SP.SearchClientContacts.SubmitterId, DbType.Int32, submitterId);
                database.AddInParameter(command, SP.SearchClientContacts.AgencyId, DbType.Int32, AgencyId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        clientContacts.Add(new SearchClientContactsViewData
                        {
                            Id = reader.GetDefaultIfDBNull(T.ClientContact.ClientContactID, GetNullableInt32, null),
                            AgencyCode = reader.GetDefaultIfDBNull(T.Agency.AgencyCode, GetString, null),
                            AgencyName = reader.GetDefaultIfDBNull(T.Agency.AgencyName, GetString, null),
                           // AgencyId = reader.GetDefaultIfDBNull(T.Agency.AgencyId, GetInt32, 0),
                          //  AgencyState = new State(reader.GetDefaultIfDBNull(T.Agency.StateFIPS, GetString, null)),
                            DateOfContact = reader.GetDefaultIfDBNull(T.ClientContact.DateOfContact, GetDateTime, DateTime.MinValue),
                            ClientFirstName = reader.GetDefaultIfDBNull(T.ClientContact.ClientFirstName, GetString, null),
                            AutoAssignedClientId = reader.GetDefaultIfDBNull(T.ClientContact.ClientID, GetString, null),
                            ClientLastName = reader.GetDefaultIfDBNull(T.ClientContact.ClientLastName, GetString, null),
                            ClientPhone = reader.GetDefaultIfDBNull(T.ClientContact.ClientPhone, GetString, null),
                            CounselorUserID =  reader.GetDefaultIfDBNull(T.ClientContact.CounselorUserID, GetInt32, 0) ,
                            SubmitterUserID =  reader.GetDefaultIfDBNull(T.ClientContact.SubmitterUserID, GetInt32, 0) ,
                            ReviewerUserID  = reader.GetDefaultIfDBNull(T.ClientContact.ReviewerUserID, GetInt32, 0) ,
                            RepresentativeFirstName = reader.GetDefaultIfDBNull(T.ClientContact.RepresentativeFirstName, GetString, null),
                            RepresentativeLastName = reader.GetDefaultIfDBNull(T.ClientContact.RepresentativeLastName, GetString, null),
                            ReviewedDate = reader.GetDefaultIfDBNull(T.ClientContact.ReviewedDate, GetNullableDateTime, null),
                            StateSpecificClientId = reader.GetDefaultIfDBNull(T.ClientContact.StateSpecificClientID, GetString, null)
                        });
                    }
                }

            }

            return clientContacts;
        }

        public IEnumerable<KeyValuePair<int, string>> GetClientContactCounselorsByAgency(int UserId, string StateFIPS, int ScopeId, int AgencyId)
        {
            var counselors = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand("dbo.GetClientContactCounselorsByAgency"))
            {
                database.AddInParameter(command, SP.GetClientContactCounselors.UserId, DbType.Int32, UserId);
                database.AddInParameter(command, SP.GetClientContactCounselors.StateFIPS, DbType.String, StateFIPS);
                database.AddInParameter(command, SP.GetClientContactCounselors.ScopeId, DbType.Int32, ScopeId);
                database.AddInParameter(command, SP.GetClientContactCounselors.AgencyId, DbType.Int32, AgencyId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        //var isActive = reader.GetDefaultIfDBNull(T.User.IsActive, GetBool, false);
                        counselors.Add(
                            new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                          String.Format("{0} {1} {2}",
                                                                        reader.GetDefaultIfDBNull(T.User.FirstName,
                                                                                                  GetString,
                                                                                                  string.Empty),
                                                                        reader.GetDefaultIfDBNull(T.User.LastName,
                                                                                                  GetString,
                                                                                                  string.Empty),
                                                                        reader.GetDefaultIfDBNull(
                                                                            T.User.RegionName, GetString,
                                                                            string.Empty))));

                    }
                }

            }

            return counselors;
        }


        public IEnumerable<KeyValuePair<int, string>> GetAllClientContactCounselors(int UserId, string StateFIPS, int ScopeId)
        {
            var counselors = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand("dbo.GetAllClientContactCounselors"))
            {
                database.AddInParameter(command, SP.GetClientContactCounselors.UserId, DbType.Int32, UserId);
                database.AddInParameter(command, SP.GetClientContactCounselors.StateFIPS, DbType.String, StateFIPS);
                database.AddInParameter(command, SP.GetClientContactCounselors.ScopeId, DbType.Int32, ScopeId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        //var isActive = reader.GetDefaultIfDBNull(T.User.IsActive, GetBool, false);
                        counselors.Add(
                            new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                          String.Format("{0} {1} {2}",
                                                                        reader.GetDefaultIfDBNull(T.User.FirstName,
                                                                                                  GetString,
                                                                                                  string.Empty),
                                                                        reader.GetDefaultIfDBNull(T.User.LastName,
                                                                                                  GetString,
                                                                                                  string.Empty),
                                                                        reader.GetDefaultIfDBNull(
                                                                            T.User.RegionName, GetString,
                                                                            string.Empty))));

                    }
                }

            }

            return counselors;
        }


        public IEnumerable<KeyValuePair<int, string>> GetClientContactSubmittersByAgency(int UserId, string StateFIPS, int ScopeId, int AgencyId)
        {
            var submitters = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand("dbo.GetClientContactSubmittersByAgency"))
            {
                database.AddInParameter(command, SP.GetClientContactCounselors.UserId, DbType.Int32, UserId);
                database.AddInParameter(command, SP.GetClientContactCounselors.StateFIPS, DbType.String, StateFIPS);
                database.AddInParameter(command, SP.GetClientContactCounselors.ScopeId, DbType.Int32, ScopeId);
                database.AddInParameter(command, SP.GetClientContactCounselors.AgencyId, DbType.Int32, AgencyId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        //var isActive = reader.GetDefaultIfDBNull(T.User.IsActive, GetBool, false);
                        submitters.Add(
                            new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                          String.Format("{0} {1} {2}",
                                                                        reader.GetDefaultIfDBNull(T.User.FirstName,
                                                                                                  GetString,
                                                                                                  string.Empty),
                                                                        reader.GetDefaultIfDBNull(T.User.LastName,
                                                                                                  GetString,
                                                                                                  string.Empty),
                                                                        reader.GetDefaultIfDBNull(
                                                                            T.User.RegionName, GetString,
                                                                            string.Empty))));

                    }
                }

            }

            return submitters;
        }


        public IEnumerable<KeyValuePair<int, string>> GetClientContactCounselorsByAgencyActiveInactive(int UserId, string StateFIPS, int ScopeId, int AgencyId,bool IsActive)
        {
            var submitters = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand("dbo.GetClientContactCounselorsByAgencyActiveInactive"))
            {
                database.AddInParameter(command, SP.GetClientContactCounselorsByAgencyActiveInactive.UserId, DbType.Int32, UserId);
                database.AddInParameter(command, SP.GetClientContactCounselorsByAgencyActiveInactive.StateFIPS, DbType.String, StateFIPS);
                database.AddInParameter(command, SP.GetClientContactCounselorsByAgencyActiveInactive.ScopeId, DbType.Int32, ScopeId);
                database.AddInParameter(command, SP.GetClientContactCounselorsByAgencyActiveInactive.AgencyId, DbType.Int32, AgencyId);
                database.AddInParameter(command, SP.GetClientContactCounselorsByAgencyActiveInactive.IsActive, DbType.Boolean, IsActive);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        //var isActive = reader.GetDefaultIfDBNull(T.User.IsActive, GetBool, false);
                        submitters.Add(
                            new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                          String.Format("{0} {1} {2}",
                                                                        reader.GetDefaultIfDBNull(T.User.FirstName,
                                                                                                  GetString,
                                                                                                  string.Empty),
                                                                        reader.GetDefaultIfDBNull(T.User.LastName,
                                                                                                  GetString,
                                                                                                  string.Empty),
                                                                        reader.GetDefaultIfDBNull(
                                                                            T.User.RegionName, GetString,
                                                                            string.Empty))));

                    }
                }

            }

            return submitters;
        }



        public IEnumerable<KeyValuePair<int, string>> GetAllClientContactSubmitters(int UserId, string StateFIPS, int ScopeId)
        {
            var submitters = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand("dbo.GetAllClientContactDataSubmitters"))
            {
                database.AddInParameter(command, SP.GetClientContactCounselors.UserId, DbType.Int32, UserId);
                database.AddInParameter(command, SP.GetClientContactCounselors.StateFIPS, DbType.String, StateFIPS);
                database.AddInParameter(command, SP.GetClientContactCounselors.ScopeId, DbType.Int32, ScopeId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                            submitters.Add(
                                 new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                               String.Format("{0} {1} {2}",
                                                                             reader.GetDefaultIfDBNull(T.User.FirstName,
                                                                                                       GetString,
                                                                                                       string.Empty),
                                                                             reader.GetDefaultIfDBNull(T.User.LastName,
                                                                                                       GetString,
                                                                                                       string.Empty),
                                                                             reader.GetDefaultIfDBNull(
                                                                                 T.User.RegionName, GetString,
                                                                                 string.Empty))));
                    }
                }

            }

            return submitters;
        }

        //public IEnumerable<KeyValuePair<int, string>> GetClientContactCounselors(State state)
        //{
        //    var counselors = new List<KeyValuePair<int, string>>();

        //    using (var command = database.GetStoredProcCommand("dbo.GetClientContactCounselorsByState"))
        //    {
        //        database.AddInParameter(command, SP.GetClientContactCounselorsByState.StateFIPS, DbType.String, state.Code);

        //        using (var reader = database.ExecuteReader(command))
        //        {
        //            while (reader.Read())
        //            {
        //                var isActive = reader.GetDefaultIfDBNull(T.User.IsActive, GetBool, false);

        //                if (isActive)
        //                {
        //                    counselors.Add(
        //                        new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
        //                                                      String.Format("{0} {1} [{2}]",
        //                                                                    reader.GetDefaultIfDBNull(T.User.FirstName,
        //                                                                                              GetString,
        //                                                                                              string.Empty),
        //                                                                    reader.GetDefaultIfDBNull(T.User.LastName,
        //                                                                                              GetString,
        //                                                                                              string.Empty),
        //                                                                    reader.GetDefaultIfDBNull(
        //                                                                        T.Agency.AgencyName, GetString,
        //                                                                        string.Empty))));
        //                }
        //                else
        //                {
        //                    counselors.Add(
        //                        new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
        //                                                      String.Format("{0} {1} [Inactive] [{2}]",
        //                                                                    reader.GetDefaultIfDBNull(T.User.FirstName,
        //                                                                                              GetString,
        //                                                                                              string.Empty),
        //                                                                    reader.GetDefaultIfDBNull(T.User.LastName,
        //                                                                                              GetString,
        //                                                                                              string.Empty),
        //                                                                    reader.GetDefaultIfDBNull(
        //                                                                        T.Agency.AgencyName, GetString,
        //                                                                        string.Empty))));
        //                }
        //            }
        //        }
        //    }

        //    return counselors;
        //}

        //public IEnumerable<KeyValuePair<int, string>> GetClientContactSubmitters(State state)
        //{
        //    var submitters = new List<KeyValuePair<int, string>>();

        //    using (var command = database.GetStoredProcCommand("dbo.GetClientContactSubmittersByState"))
        //    {
        //        database.AddInParameter(command, SP.GetClientContactSubmittersByState.StateFIPS, DbType.String, state.Code);

        //        using (var reader = database.ExecuteReader(command))
        //        {
        //            while (reader.Read())
        //            {
        //                var isActive = reader.GetDefaultIfDBNull(T.User.IsActive, GetBool, false);

        //                if (isActive)
        //                {
        //                    submitters.Add(
        //                        new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
        //                                                      String.Format("{0} {1} [{2}]",
        //                                                                    reader.GetDefaultIfDBNull(T.User.FirstName,
        //                                                                                              GetString,
        //                                                                                              string.Empty),
        //                                                                    reader.GetDefaultIfDBNull(T.User.LastName,
        //                                                                                              GetString,
        //                                                                                              string.Empty),
        //                                                                    reader.GetDefaultIfDBNull(
        //                                                                        T.Agency.AgencyName, GetString,
        //                                                                        string.Empty))));
        //                }
        //                else
        //                {
        //                    submitters.Add(
        //                         new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
        //                                                       String.Format("{0} {1} [Inactive] [{2}]",
        //                                                                     reader.GetDefaultIfDBNull(T.User.FirstName,
        //                                                                                               GetString,
        //                                                                                               string.Empty),
        //                                                                     reader.GetDefaultIfDBNull(T.User.LastName,
        //                                                                                               GetString,
        //                                                                                               string.Empty),
        //                                                                     reader.GetDefaultIfDBNull(
        //                                                                         T.Agency.AgencyName, GetString,
        //                                                                         string.Empty))));
        //                }
        //            }
        //        }

        //    }

        //    return submitters;
        //}

        //public IEnumerable<KeyValuePair<int, string>> GetClientContactCounselors(int userId)
        //{
        //    var counselors = new List<KeyValuePair<int, string>>();

        //    using (var command = database.GetStoredProcCommand("dbo.GetClientContactCounselorsByUserId"))
        //    {
        //        database.AddInParameter(command, SP.GetClientContactCounselorsByUserId.UserId, DbType.Int32, userId);

        //        using (var reader = database.ExecuteReader(command))
        //        {
        //            while (reader.Read())
        //            {
        //                var isActive = reader.GetDefaultIfDBNull(T.User.IsActive, GetBool, false);

        //                if (isActive)
        //                {
        //                    counselors.Add(
        //                        new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
        //                                                      String.Format("{0} {1} [{2}]",
        //                                                                    reader.GetDefaultIfDBNull(T.User.FirstName,
        //                                                                                              GetString,
        //                                                                                              string.Empty),
        //                                                                    reader.GetDefaultIfDBNull(T.User.LastName,
        //                                                                                              GetString,
        //                                                                                              string.Empty),
        //                                                                    reader.GetDefaultIfDBNull(
        //                                                                        T.Agency.AgencyName, GetString,
        //                                                                        string.Empty))));
        //                }
        //                else
        //                {
        //                    counselors.Add(
        //                        new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
        //                                                      String.Format("{0} {1} [Inactive] [{2}]",
        //                                                                    reader.GetDefaultIfDBNull(T.User.FirstName,
        //                                                                                              GetString,
        //                                                                                              string.Empty),
        //                                                                    reader.GetDefaultIfDBNull(T.User.LastName,
        //                                                                                              GetString,
        //                                                                                              string.Empty),
        //                                                                    reader.GetDefaultIfDBNull(
        //                                                                        T.Agency.AgencyName, GetString,
        //                                                                        string.Empty))));
        //                }
        //            }
        //        }
        //    }

        //    return counselors;
        //}

        //public IEnumerable<KeyValuePair<int, string>> GetClientContactSubmitters(int userId)
        //{
        //    var submitters = new List<KeyValuePair<int, string>>();

        //    using (var command = database.GetStoredProcCommand("dbo.GetClientContactSubmittersByUserId"))
        //    {
        //        database.AddInParameter(command, SP.GetClientContactSubmittersByUserId.UserId, DbType.Int32, userId);

        //        using (var reader = database.ExecuteReader(command))
        //        {
        //            while (reader.Read())
        //            {
        //                var isActive = reader.GetDefaultIfDBNull(T.User.IsActive, GetBool, false);

        //                if (isActive)
        //                {
        //                    submitters.Add(
        //                        new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
        //                                                      String.Format("{0} {1} [{2}]",
        //                                                                    reader.GetDefaultIfDBNull(T.User.FirstName,
        //                                                                                              GetString,
        //                                                                                              string.Empty),
        //                                                                    reader.GetDefaultIfDBNull(T.User.LastName,
        //                                                                                              GetString,
        //                                                                                              string.Empty),
        //                                                                    reader.GetDefaultIfDBNull(
        //                                                                        T.Agency.AgencyName, GetString,
        //                                                                        string.Empty))));
        //                }
        //                else
        //                {
        //                    submitters.Add(
        //                         new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
        //                                                       String.Format("{0} {1} [Inactive] [{2}]",
        //                                                                     reader.GetDefaultIfDBNull(T.User.FirstName,
        //                                                                                               GetString,
        //                                                                                               string.Empty),
        //                                                                     reader.GetDefaultIfDBNull(T.User.LastName,
        //                                                                                               GetString,
        //                                                                                               string.Empty),
        //                                                                     reader.GetDefaultIfDBNull(
        //                                                                         T.Agency.AgencyName, GetString,
        //                                                                         string.Empty))));
        //                }
        //            }
        //        }

        //    }

        //    return submitters;
        //}

        public bool IsZIPCodeValid(string zipCode)
        {
            using (var command = database.GetStoredProcCommand("dbo.IsZIPCodeValid"))
            {
                database.AddInParameter(command, SP.IsZIPCodeValid.ZIPCode, DbType.String, zipCode);

                return Convert.ToBoolean(database.ExecuteScalar(command));
            }
        }

        public bool IsCounselingCountyCodeValid(int userId, string countyCode)
        {
            using (var command = database.GetStoredProcCommand("dbo.IsCounselingCountyCodeValid"))
            {
                database.AddInParameter(command, SP.IsCounselingCountyCodeValid.UserId, DbType.Int32, userId);
                database.AddInParameter(command, SP.IsCounselingCountyCodeValid.CountyCode, DbType.String, countyCode);

                return Convert.ToBoolean(database.ExecuteScalar(command));
            }
        }

        public bool IsCounselingZIPCodeValid(int userId, string zipCode)
        {
            using (var command = database.GetStoredProcCommand("dbo.IsCounselingZIPCodeValid"))
            {
                database.AddInParameter(command, SP.IsCounselingZIPCodeValid.UserId, DbType.Int32, userId);
                database.AddInParameter(command, SP.IsCounselingZIPCodeValid.ZIPCode, DbType.String, zipCode);

                return Convert.ToBoolean(database.ExecuteScalar(command));
            }
        }

        public bool IsUserClientContactReviewer(int clientContactId, int userId)
        {
            using (var command = database.GetStoredProcCommand("dbo.IsUserClientContactReviewer"))
            {
                database.AddInParameter(command, SP.IsUserClientContactReviewer.ClientContactId, DbType.Int32, clientContactId);
                database.AddInParameter(command, SP.IsUserClientContactReviewer.UserId, DbType.String, userId);

                return Convert.ToBoolean(database.ExecuteScalar(command));
            }
        }

        public bool IsDuplicateClientContact(CCFBLL.DuplicateCheckType checkType, int agencyId, string stateSpecifiedClientId, string autoAssignedClientId, string clientFirstName, string clientLastName, DateTime dateOfContact, int CounselorID)
        {
            using (var command = database.GetStoredProcCommand("dbo.IsDuplicateClientContact"))
            {
                database.AddInParameter(command, SP.IsDuplicateClientContact.CheckType, DbType.Int32, (int)checkType);
                database.AddInParameter(command, SP.IsDuplicateClientContact.AgencyID, DbType.Int32, agencyId);
                database.AddInParameter(command, SP.IsDuplicateClientContact.ClientID, DbType.String, autoAssignedClientId.PadRight(50));
                database.AddInParameter(command, SP.IsDuplicateClientContact.StateSpecifiedClientID, DbType.String, stateSpecifiedClientId.PadRight(50));
                database.AddInParameter(command, SP.IsDuplicateClientContact.ClientFirstName, DbType.String, clientFirstName);
                database.AddInParameter(command, SP.IsDuplicateClientContact.ClientLastName, DbType.String, clientLastName);
                database.AddInParameter(command, SP.IsDuplicateClientContact.DateOfContact, DbType.DateTime, dateOfContact);
                database.AddInParameter(command, SP.IsDuplicateClientContact.CounselorID, DbType.Int32, CounselorID);
                return Convert.ToBoolean(database.ExecuteScalar(command));
            }
        }

        public IEnumerable<KeyValuePair<int, string>> GetCounselors(Scope scope, IList<int> agencies)
        {
            var counselors = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand("dbo.GetCounselorsForAgencies"))
            {
                database.AddInParameter(command, SP.GetCounselorsForAgencies.ScopeID, DbType.Int32, scope);
                database.AddInParameter(command, SP.GetCounselorsForAgencies.Agencies, DbType.String,
                    string.Join(",", (from agency in agencies select agency.ToString()).ToArray()));

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        var isActive = reader.GetDefaultIfDBNull(T.User.IsActive, GetBool, false);

                        if (isActive)
                        {
                            counselors.Add(
                                new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                              String.Format("{0} {1} [{2}]",
                                                                            reader.GetDefaultIfDBNull(T.User.FirstName,
                                                                                                      GetString,
                                                                                                      string.Empty),
                                                                            reader.GetDefaultIfDBNull(T.User.LastName,
                                                                                                      GetString,
                                                                                                      string.Empty),
                                                                            reader.GetDefaultIfDBNull(
                                                                                T.Agency.AgencyName, GetString,
                                                                                string.Empty))));
                        }
                        else
                        {
                            counselors.Add(
                                new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                              String.Format("{0} {1} [Inactive] [{2}]",
                                                                            reader.GetDefaultIfDBNull(T.User.FirstName,
                                                                                                      GetString,
                                                                                                      string.Empty),
                                                                            reader.GetDefaultIfDBNull(T.User.LastName,
                                                                                                      GetString,
                                                                                                      string.Empty),
                                                                            reader.GetDefaultIfDBNull(
                                                                                T.Agency.AgencyName, GetString,
                                                                                string.Empty))));
                        }
                    }
                }
            }

            return counselors;
        }

        public IEnumerable<KeyValuePair<int, string>> GetSubmitters(Scope scope, List<int> agencies)
        {
            var submitters = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand("dbo.GetSubmittersForAgencies"))
            {
                database.AddInParameter(command, SP.GetSubmittersForAgencies.ScopeID, DbType.Int32, scope);
                database.AddInParameter(command, SP.GetSubmittersForAgencies.Agencies, DbType.String,
                    string.Join(",", (from agency in agencies select agency.ToString()).ToArray()));

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        var isActive = reader.GetDefaultIfDBNull(T.User.IsActive, GetBool, false);

                        if (isActive)
                        {
                            submitters.Add(
                                new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                              String.Format("{0} {1} [{2}]",
                                                                            reader.GetDefaultIfDBNull(T.User.FirstName,
                                                                                                      GetString,
                                                                                                      string.Empty),
                                                                            reader.GetDefaultIfDBNull(T.User.LastName,
                                                                                                      GetString,
                                                                                                      string.Empty),
                                                                            reader.GetDefaultIfDBNull(
                                                                                T.Agency.AgencyName, GetString,
                                                                                string.Empty))));
                        }
                        else
                        {
                            submitters.Add(
                                 new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                               String.Format("{0} {1} [Inactive] [{2}]",
                                                                             reader.GetDefaultIfDBNull(T.User.FirstName,
                                                                                                       GetString,
                                                                                                       string.Empty),
                                                                             reader.GetDefaultIfDBNull(T.User.LastName,
                                                                                                       GetString,
                                                                                                       string.Empty),
                                                                             reader.GetDefaultIfDBNull(
                                                                                 T.Agency.AgencyName, GetString,
                                                                                 string.Empty))));
                        }
                    }
                }

            }

            return submitters;
        }

        public IEnumerable<KeyValuePair<int, string>> GetReviewerSubmitters(Scope scope, List<int> agencies, int userId)
        {
            var submitters = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand("dbo.GetReviewerSubmittersForAgencies"))
            {
                database.AddInParameter(command, SP.GetReviewerSubmittersForAgencies.ScopeID, DbType.Int32, scope);
                database.AddInParameter(command, SP.GetReviewerSubmittersForAgencies.Agencies, DbType.String,
                    string.Join(",", (from agency in agencies select agency.ToString()).ToArray()));
                database.AddInParameter(command, SP.GetReviewerSubmittersForAgencies.UserId, DbType.Int32, userId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        var isActive = reader.GetDefaultIfDBNull(T.User.IsActive, GetBool, false);

                        if (isActive)
                        {
                            submitters.Add(
                                new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                              String.Format("{0} {1} [{2}]",
                                                                            reader.GetDefaultIfDBNull(T.User.FirstName,
                                                                                                      GetString,
                                                                                                      string.Empty),
                                                                            reader.GetDefaultIfDBNull(T.User.LastName,
                                                                                                      GetString,
                                                                                                      string.Empty),
                                                                            reader.GetDefaultIfDBNull(
                                                                                T.Agency.AgencyName, GetString,
                                                                                string.Empty))));
                        }
                        else
                        {
                            submitters.Add(
                                 new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                               String.Format("{0} {1} [Inactive] [{2}]",
                                                                             reader.GetDefaultIfDBNull(T.User.FirstName,
                                                                                                       GetString,
                                                                                                       string.Empty),
                                                                             reader.GetDefaultIfDBNull(T.User.LastName,
                                                                                                       GetString,
                                                                                                       string.Empty),
                                                                             reader.GetDefaultIfDBNull(
                                                                                 T.Agency.AgencyName, GetString,
                                                                                 string.Empty))));
                        }
                    }
                }

            }

            return submitters;
        }

        public IEnumerable<KeyValuePair<int, string>> GetCounselorsForSuperDataEditor(int userId)
        {
            var counselors = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand("dbo.GetCounselorsForSuperDataEditor"))
            {
                database.AddInParameter(command, SP.GetCounselorsForSuperDataEditor.UserId, DbType.Int32, userId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        var isActive = reader.GetDefaultIfDBNull(T.User.IsActive, GetBool, false);

                        if (isActive)
                        {
                            counselors.Add(
                                new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                              String.Format("{0} {1} [{2}]",
                                                                            reader.GetDefaultIfDBNull(T.User.FirstName,
                                                                                                      GetString,
                                                                                                      string.Empty),
                                                                            reader.GetDefaultIfDBNull(T.User.LastName,
                                                                                                      GetString,
                                                                                                      string.Empty),
                                                                            reader.GetDefaultIfDBNull(
                                                                                T.Agency.AgencyName, GetString,
                                                                                string.Empty))));
                        }
                        else
                        {
                            counselors.Add(
                                new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                              String.Format("{0} {1} [Inactive] [{2}]",
                                                                            reader.GetDefaultIfDBNull(T.User.FirstName,
                                                                                                      GetString,
                                                                                                      string.Empty),
                                                                            reader.GetDefaultIfDBNull(T.User.LastName,
                                                                                                      GetString,
                                                                                                      string.Empty),
                                                                            reader.GetDefaultIfDBNull(
                                                                                T.Agency.AgencyName, GetString,
                                                                                string.Empty))));
                        }
                    }
                }
            }

            return counselors;
        }

        public IEnumerable<KeyValuePair<int, string>> GetSubmittersForSuperDataEditor(int userId)
        {
            var submitters = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand("dbo.GetSubmittersForSuperDataEditor"))
            {
                database.AddInParameter(command, SP.GetSubmittersForSuperDataEditor.UserId, DbType.Int32, userId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        var isActive = reader.GetDefaultIfDBNull(T.User.IsActive, GetBool, false);

                        if (isActive)
                        {
                            submitters.Add(
                                new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                              String.Format("{0} {1} [{2}]",
                                                                            reader.GetDefaultIfDBNull(T.User.FirstName,
                                                                                                      GetString,
                                                                                                      string.Empty),
                                                                            reader.GetDefaultIfDBNull(T.User.LastName,
                                                                                                      GetString,
                                                                                                      string.Empty),
                                                                            reader.GetDefaultIfDBNull(
                                                                                T.Agency.AgencyName, GetString,
                                                                                string.Empty))));
                        }
                        else
                        {
                            submitters.Add(
                                 new KeyValuePair<int, string>(reader.GetDefaultIfDBNull(T.User.UserId, GetInt32, 0),
                                                               String.Format("{0} {1} [Inactive] [{2}]",
                                                                             reader.GetDefaultIfDBNull(T.User.FirstName,
                                                                                                       GetString,
                                                                                                       string.Empty),
                                                                             reader.GetDefaultIfDBNull(T.User.LastName,
                                                                                                       GetString,
                                                                                                       string.Empty),
                                                                             reader.GetDefaultIfDBNull(
                                                                                 T.Agency.AgencyName, GetString,
                                                                                 string.Empty))));
                        }
                    }
                }

            }

            return submitters;
        }
    }
}
