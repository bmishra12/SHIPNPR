using System;
using System.Data;
using System.Linq;
using ShiptalkLogic.BusinessObjects;
using SP = ShiptalkLogic.Constants.StoredProcs;
using T = ShiptalkLogic.Constants.Tables;
using System.Collections.Generic;
using ShiptalkLogic.BusinessObjects.UI;



namespace ShiptalkLogic.DataLayer
{
    internal class PAMDAL : FormDALBase
    {

        public int CreatePam(PublicMediaEvent publicMediaEvent)
        {
            if (publicMediaEvent == null)
                throw new ArgumentNullException("publicMediaEvent");

            using (var command = database.GetStoredProcCommand("dbo.CreatePam"))
            {
                database.AddInParameter(command, SP.CreatePAM.SubmitterUserID, DbType.Int16, publicMediaEvent.SubmitterUserID);
                database.AddInParameter(command, SP.CreatePAM.ReviewerUserID, DbType.Int32, publicMediaEvent.ReviewerUserID);
                database.AddInParameter(command, SP.CreatePAM.AgencyID, DbType.Int32, publicMediaEvent.AgencyId);
                database.AddInParameter(command, SP.CreatePAM.InteractiveEstAttendees, DbType.Int16, publicMediaEvent.InteractiveEstAttendees);
                database.AddInParameter(command, SP.CreatePAM.InteractiveEstProvidedEnrollAssistance, DbType.Int16, publicMediaEvent.InteractiveEstProvidedEnrollAssistance);
                database.AddInParameter(command, SP.CreatePAM.BoothEstDirectContacts, DbType.Int16, publicMediaEvent.BoothEstDirectContacts);
                database.AddInParameter(command, SP.CreatePAM.BoothEstEstProvidedEnrollAssistance, DbType.Int16, publicMediaEvent.BoothEstEstProvidedEnrollAssistance);

                database.AddInParameter(command, SP.CreatePAM.RadioEstListenerReach, DbType.Int32, publicMediaEvent.RadioEstListenerReach);
                database.AddInParameter(command, SP.CreatePAM.TVEstViewersReach, DbType.Int32, publicMediaEvent.TVEstViewersReach);

                database.AddInParameter(command, SP.CreatePAM.DedicatedEstPersonsReached, DbType.Int16, publicMediaEvent.DedicatedEstPersonsReached);
                database.AddInParameter(command, SP.CreatePAM.DedicatedEstAnyEnrollmentAssistance, DbType.Int16, publicMediaEvent.DedicatedEstAnyEnrollmentAssistance);
                database.AddInParameter(command, SP.CreatePAM.DedicatedEstPartDEnrollmentAssistance, DbType.Int16, publicMediaEvent.DedicatedEstPartDEnrollmentAssistance);
                database.AddInParameter(command, SP.CreatePAM.DedicatedEstLISEnrollmentAssistance, DbType.Int16, publicMediaEvent.DedicatedEstLISEnrollmentAssistance);
                database.AddInParameter(command, SP.CreatePAM.DedicatedEstMSPEnrollmentAssistance, DbType.Int16, publicMediaEvent.DedicatedEstMSPEnrollmentAssistance);
                database.AddInParameter(command, SP.CreatePAM.DedicatedEstOtherEnrollmentAssistance, DbType.Int16, publicMediaEvent.DedicatedEstOtherEnrollmentAssistance);

                database.AddInParameter(command, SP.CreatePAM.ElectronicEstPersonsViewingOrListening, DbType.Int32, publicMediaEvent.ElectronicEstPersonsViewingOrListening);
                database.AddInParameter(command, SP.CreatePAM.PrintEstPersonsReading, DbType.Int32, publicMediaEvent.PrintEstPersonsReading);
                database.AddInParameter(command, SP.CreatePAM.ActivityStartDate, DbType.DateTime, publicMediaEvent.ActivityStartDate);
                database.AddInParameter(command, SP.CreatePAM.ActivityEndDate, DbType.DateTime, publicMediaEvent.ActivityEndDate);


                database.AddInParameter(command, SP.CreatePAM.EventName, DbType.String, publicMediaEvent.EventName);
                database.AddInParameter(command, SP.CreatePAM.ContactFirstName, DbType.String, publicMediaEvent.ContactFirstName);
                database.AddInParameter(command, SP.CreatePAM.ContactLastName, DbType.String, publicMediaEvent.ContactLastName);
                database.AddInParameter(command, SP.CreatePAM.ContactPhone, DbType.String, publicMediaEvent.ContactPhone);


                database.AddInParameter(command, SP.CreatePAM.EventStateCode, DbType.String, publicMediaEvent.EventState.Code);
                database.AddInParameter(command, SP.CreatePAM.EventCountycode, DbType.String, publicMediaEvent.EventCountycode);
                database.AddInParameter(command, SP.CreatePAM.EventZIPCode, DbType.String, publicMediaEvent.EventZIPCode);
                database.AddInParameter(command, SP.CreatePAM.EventCity, DbType.String, publicMediaEvent.EventCity);
                database.AddInParameter(command, SP.CreatePAM.EventStreet, DbType.String, publicMediaEvent.EventStreet);
                database.AddInParameter(command, SP.CreatePAM.LastUpdatedBy, DbType.Int32, publicMediaEvent.LastUpdatedBy);
                database.AddInParameter(command, SP.CreatePAM.IsBatchUploadData, DbType.Byte, publicMediaEvent.IsBatchUploadData);
                database.AddInParameter(command, SP.CreatePAM.BatchStateUniqueID, DbType.String, publicMediaEvent.BatchStateUniqueID);

                

                database.AddInParameter(command, SP.CreatePAM.PamTopics, DbType.String, string.Join(",", (from topic in publicMediaEvent.PAMSelectedTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreatePAM.OtherPamTopicSpecified, DbType.String, publicMediaEvent.OtherPamTopicSpecified);

                database.AddInParameter(command, SP.CreatePAM.PAMAudiences, DbType.String, string.Join(",", (from topic in publicMediaEvent.PAMSelectedAudiences select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.CreatePAM.OtherPamAudienceSpecified, DbType.String, publicMediaEvent.OtherPamAudienceSpecified);

                database.AddInParameter(command, SP.CreatePAM.CMSSpecialUseFields, DbType.Xml, publicMediaEvent.CMSSpecialUseFields.SerializeToXmlString());
                database.AddInParameter(command, SP.CreatePAM.StateSpecialUseFields, DbType.Xml, publicMediaEvent.StateSpecialUseFields.SerializeToXmlString());

                database.AddInParameter(command, SP.CreatePAM.PamPresenters, DbType.Xml, publicMediaEvent.PamPresenters.SerializeToXmlString());


                database.AddOutParameter(command, SP.CreatePAM.PAMID, DbType.Int32, 6);

                database.ExecuteNonQuery(command);

                return (int)database.GetParameterValue(command, SP.CreatePAM.PAMID);
            }
        }

        public void DeletePam(string StateFips, string AgencyCode, string BatchStateUniqueID)
        {
            using (var command = database.GetStoredProcCommand(StoredProcNames.BatchUpload.DeleteUploadedPamRecord.Description()))
            {
                database.AddInParameter(command, "StateFips", DbType.String, StateFips);
                database.AddInParameter(command, "AgencyCode", DbType.String, AgencyCode);
                database.AddInParameter(command, "BatchStateUniqueID", DbType.String, BatchStateUniqueID);
                database.ExecuteNonQuery(command);
            }

        }


        public bool DeletePamByPamID(int pamId, out string FailureReason)
        {
            bool result = false;
            using (var command = database.GetStoredProcCommand("dbo.deletePamByPamID"))
            {
                database.AddInParameter(command, "@PAMID", DbType.Int32, pamId);

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
        



        public bool IsUserPamReviewer(int pamId, int userId)
        {
            using (var command = database.GetStoredProcCommand("dbo.IsUserPamReviewer"))
            {
                database.AddInParameter(command, SP.IsUserPamReviewer.PamID, DbType.Int32, pamId);
                database.AddInParameter(command, SP.IsUserPamReviewer.UserId, DbType.String, userId);

                return Convert.ToBoolean(database.ExecuteScalar(command));
            }
        }


        public bool IsUserIdInPresenters(int pamId, int userId)
        {
            using (var command = database.GetStoredProcCommand("dbo.IsUserIdInPresenters"))
            {
                database.AddInParameter(command, SP.IsUserIdInPresenters.PamID, DbType.Int32, pamId);
                database.AddInParameter(command, SP.IsUserIdInPresenters.UserId, DbType.String, userId);

                return Convert.ToBoolean(database.ExecuteScalar(command));
            }
        }

        

        public void UpdatePam(PublicMediaEvent publicMediaEvent)
        {
            if (publicMediaEvent == null)
                throw new ArgumentNullException("publicMediaEvent");

            using (var command = database.GetStoredProcCommand("dbo.UpdatePam"))
            {

                database.AddInParameter(command, SP.CreatePAM.PAMID, DbType.Int32, publicMediaEvent.PamID);

                database.AddInParameter(command, SP.UpdatePam.SubmitterUserID, DbType.Int16, publicMediaEvent.SubmitterUserID);

                database.AddInParameter(command, SP.UpdatePam.ReviewerUserID, DbType.Int32, publicMediaEvent.ReviewerUserID);
                database.AddInParameter(command, SP.UpdatePam.AgencyID, DbType.Int32, publicMediaEvent.AgencyId);
                database.AddInParameter(command, SP.UpdatePam.InteractiveEstAttendees, DbType.Int16, publicMediaEvent.InteractiveEstAttendees);
                database.AddInParameter(command, SP.UpdatePam.InteractiveEstProvidedEnrollAssistance, DbType.Int16, publicMediaEvent.InteractiveEstProvidedEnrollAssistance);
                database.AddInParameter(command, SP.UpdatePam.BoothEstDirectContacts, DbType.Int16, publicMediaEvent.BoothEstDirectContacts);
                database.AddInParameter(command, SP.UpdatePam.BoothEstEstProvidedEnrollAssistance, DbType.Int16, publicMediaEvent.BoothEstEstProvidedEnrollAssistance);

                database.AddInParameter(command, SP.UpdatePam.RadioEstListenerReach, DbType.Int32, publicMediaEvent.RadioEstListenerReach);
                database.AddInParameter(command, SP.UpdatePam.TVEstViewersReach, DbType.Int32, publicMediaEvent.TVEstViewersReach);

                database.AddInParameter(command, SP.UpdatePam.DedicatedEstPersonsReached, DbType.Int16, publicMediaEvent.DedicatedEstPersonsReached);
                database.AddInParameter(command, SP.UpdatePam.DedicatedEstAnyEnrollmentAssistance, DbType.Int16, publicMediaEvent.DedicatedEstAnyEnrollmentAssistance);
                database.AddInParameter(command, SP.UpdatePam.DedicatedEstPartDEnrollmentAssistance, DbType.Int16, publicMediaEvent.DedicatedEstPartDEnrollmentAssistance);
                database.AddInParameter(command, SP.UpdatePam.DedicatedEstLISEnrollmentAssistance, DbType.Int16, publicMediaEvent.DedicatedEstLISEnrollmentAssistance);
                database.AddInParameter(command, SP.UpdatePam.DedicatedEstMSPEnrollmentAssistance, DbType.Int16, publicMediaEvent.DedicatedEstMSPEnrollmentAssistance);
                database.AddInParameter(command, SP.UpdatePam.DedicatedEstOtherEnrollmentAssistance, DbType.Int16, publicMediaEvent.DedicatedEstOtherEnrollmentAssistance);

                database.AddInParameter(command, SP.UpdatePam.ElectronicEstPersonsViewingOrListening, DbType.Int32, publicMediaEvent.ElectronicEstPersonsViewingOrListening);
                database.AddInParameter(command, SP.UpdatePam.PrintEstPersonsReading, DbType.Int32, publicMediaEvent.PrintEstPersonsReading);
                database.AddInParameter(command, SP.UpdatePam.ActivityStartDate, DbType.DateTime, publicMediaEvent.ActivityStartDate);
                database.AddInParameter(command, SP.UpdatePam.ActivityEndDate, DbType.DateTime, publicMediaEvent.ActivityEndDate);


                database.AddInParameter(command, SP.UpdatePam.EventName, DbType.String, publicMediaEvent.EventName);
                database.AddInParameter(command, SP.UpdatePam.ContactFirstName, DbType.String, publicMediaEvent.ContactFirstName);
                database.AddInParameter(command, SP.UpdatePam.ContactLastName, DbType.String, publicMediaEvent.ContactLastName);
                database.AddInParameter(command, SP.UpdatePam.ContactPhone, DbType.String, publicMediaEvent.ContactPhone);


                database.AddInParameter(command, SP.UpdatePam.EventStateCode, DbType.String, publicMediaEvent.EventState.Code);
                database.AddInParameter(command, SP.UpdatePam.EventCountycode, DbType.String, publicMediaEvent.EventCountycode);
                database.AddInParameter(command, SP.UpdatePam.EventZIPCode, DbType.String, publicMediaEvent.EventZIPCode);
                database.AddInParameter(command, SP.UpdatePam.EventCity, DbType.String, publicMediaEvent.EventCity);
                database.AddInParameter(command, SP.UpdatePam.EventStreet, DbType.String, publicMediaEvent.EventStreet);
                database.AddInParameter(command, SP.UpdatePam.LastUpdatedBy, DbType.Int32, publicMediaEvent.LastUpdatedBy);
                database.AddInParameter(command, SP.UpdatePam.IsBatchUploadData, DbType.Byte, publicMediaEvent.IsBatchUploadData);
                database.AddInParameter(command, SP.UpdatePam.BatchStateUniqueID, DbType.String, publicMediaEvent.BatchStateUniqueID);


                database.AddInParameter(command, SP.UpdatePam.PamTopics, DbType.String, string.Join(",", (from topic in publicMediaEvent.PAMSelectedTopics select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.UpdatePam.OtherPamTopicSpecified, DbType.String, publicMediaEvent.OtherPamTopicSpecified);

                database.AddInParameter(command, SP.UpdatePam.PAMAudiences, DbType.String, string.Join(",", (from topic in publicMediaEvent.PAMSelectedAudiences select ((int)topic).ToString()).ToArray()));
                database.AddInParameter(command, SP.UpdatePam.OtherPamAudienceSpecified, DbType.String, publicMediaEvent.OtherPamAudienceSpecified);

                database.AddInParameter(command, SP.UpdatePam.CMSSpecialUseFields, DbType.Xml, publicMediaEvent.CMSSpecialUseFields.SerializeToXmlString());
                database.AddInParameter(command, SP.UpdatePam.StateSpecialUseFields, DbType.Xml, publicMediaEvent.StateSpecialUseFields.SerializeToXmlString());

                database.AddInParameter(command, SP.UpdatePam.PamPresenters, DbType.Xml, publicMediaEvent.PamPresenters.SerializeToXmlString());


                database.ExecuteNonQuery(command);
            }
        }

        public PublicMediaEvent GetPam(int id)
        {
            PublicMediaEvent pam = null;
            IDataReader reader = null;

            try
            {
                using (var command = database.GetStoredProcCommand("dbo.GetPam"))
                {
                    database.AddInParameter(command, SP.GetPam.PamId, DbType.Int32, id);

                    using (reader = database.ExecuteReader(command))
                    {
                        if (reader.Read())
                        {
                            pam = new PublicMediaEvent
                            {
                                PamID = reader.GetDefaultIfDBNull(T.Pam.PAMID, GetNullableInt32, null),

                                SubmitterName = reader.GetDefaultIfDBNull(T.Pam.SubmitterName, GetString, null),

                                SubmitterUserID = reader.GetDefaultIfDBNull(T.Pam.SubmitterUserID, GetNullableInt32, null),

                                CreatedDate = reader.GetDefaultIfDBNull(T.Pam.CreatedDate, GetNullableDateTime, null),
                                AgencyCode = reader.GetDefaultIfDBNull(T.Pam.AgencyCode, GetString, null),
                                AgencyId = reader.GetDefaultIfDBNull(T.Pam.AgencyID, GetNullableInt32, null),
                                AgencyName = reader.GetDefaultIfDBNull(T.Pam.AgencyName, GetString, null),

                                InteractiveEstAttendees = reader.GetDefaultIfDBNull(T.Pam.InteractiveEstAttendees, GetNullableInt16, null),
                                InteractiveEstProvidedEnrollAssistance = reader.GetDefaultIfDBNull(T.Pam.InteractiveEstProvidedEnrollAssistance, GetNullableInt16, null),

                                BoothEstDirectContacts = reader.GetDefaultIfDBNull(T.Pam.BoothEstDirectContacts, GetNullableInt16, null),
                                BoothEstEstProvidedEnrollAssistance = reader.GetDefaultIfDBNull(T.Pam.BoothEstEstProvidedEnrollAssistance, GetNullableInt16, null),

                                RadioEstListenerReach = reader.GetDefaultIfDBNull(T.Pam.RadioEstListenerReach, GetNullableInt32, null),
                                TVEstViewersReach = reader.GetDefaultIfDBNull(T.Pam.TVEstViewersReach, GetNullableInt32, null),

                                DedicatedEstPersonsReached = reader.GetDefaultIfDBNull(T.Pam.DedicatedEstPersonsReached, GetNullableInt16, null),
                                DedicatedEstAnyEnrollmentAssistance = reader.GetDefaultIfDBNull(T.Pam.DedicatedEstAnyEnrollmentAssistance, GetNullableInt16, null),
                                DedicatedEstPartDEnrollmentAssistance = reader.GetDefaultIfDBNull(T.Pam.DedicatedEstPartDEnrollmentAssistance, GetNullableInt16, null),
                                DedicatedEstLISEnrollmentAssistance = reader.GetDefaultIfDBNull(T.Pam.DedicatedEstLISEnrollmentAssistance, GetNullableInt16, null),
                                DedicatedEstMSPEnrollmentAssistance = reader.GetDefaultIfDBNull(T.Pam.DedicatedEstMSPEnrollmentAssistance, GetNullableInt16, null),
                                DedicatedEstOtherEnrollmentAssistance = reader.GetDefaultIfDBNull(T.Pam.DedicatedEstOtherEnrollmentAssistance, GetNullableInt16, null),


                                ElectronicEstPersonsViewingOrListening = reader.GetDefaultIfDBNull(T.Pam.ElectronicEstPersonsViewingOrListening, GetNullableInt32, null),
                                PrintEstPersonsReading = reader.GetDefaultIfDBNull(T.Pam.PrintEstPersonsReading, GetNullableInt32, null),

                                ActivityStartDate = reader.GetDefaultIfDBNull(T.Pam.ActivityStartDate, GetNullableDateTime, null),
                                ActivityEndDate = reader.GetDefaultIfDBNull(T.Pam.ActivityEndDate, GetNullableDateTime, null),

                                EventName = reader.GetDefaultIfDBNull(T.Pam.EventName, GetString, null),
                                ContactFirstName = reader.GetDefaultIfDBNull(T.Pam.ContactFirstName, GetString, null),
                                ContactLastName = reader.GetDefaultIfDBNull(T.Pam.ContactLastName, GetString, null),
                                ContactPhone = reader.GetDefaultIfDBNull(T.Pam.ContactPhone, GetString, null),

                                EventState = new State(reader.GetDefaultIfDBNull(T.Pam.EventStateCode, GetString, null)),
                                EventCountycode = reader.GetDefaultIfDBNull(T.Pam.EventCountycode, GetString, null),
                                EventCountyName = reader.GetDefaultIfDBNull(T.Pam.EventCountyName, GetString, null),
                                EventZIPCode = reader.GetDefaultIfDBNull(T.Pam.EventZIPCode, GetString, null),
                                EventCity = reader.GetDefaultIfDBNull(T.Pam.EventCity, GetString, null),
                                EventStreet = reader.GetDefaultIfDBNull(T.Pam.EventStreet, GetString, null),

                                PAMSelectedTopics = new List<PAMTopic>(),
                                PAMSelectedAudiences = new List<PAMAudiance>(),

                                CMSSpecialUseFields = new List<SpecialFieldValue>(),
                                StateSpecialUseFields = new List<SpecialFieldValue>(),


                                PamPresenters = new List<ShiptalkLogic.BusinessObjects.UI.PamPresenters>()
                            };

                            reader.NextResult();

                            while (reader.Read())
                            {
                                pam.PamPresenters.Add(
                                    new ShiptalkLogic.BusinessObjects.UI.PamPresenters
                                    {
                                        PAMUserId = reader.GetInt32(getOrdinal(reader, PamPresenter.PamUserID)).ToString(),
                                        PAMUserName = reader.GetString(getOrdinal(reader, PamPresenter.UserName)),
                                        Affiliation = reader.GetDefaultIfDBNull(PamPresenter.Affiliation, GetString, null),
                                        HoursSpent = reader.GetDecimal(getOrdinal(reader, PamPresenter.HoursSpent))

                                    });
                            }

                            reader.NextResult();

                            while (reader.Read())
                            {
                                pam.PAMSelectedTopics.Add(
                                    (PAMTopic)reader.GetDefaultIfDBNull<short>(T.Pam.PAMTopicID, GetInt16, 0));
                            }


                            reader.NextResult();

                            while (reader.Read())
                            {
                                pam.OtherPamTopicSpecified = reader.GetDefaultIfDBNull(T.Pam.Description, GetString, null);
                            }

                            reader.NextResult();

                            while (reader.Read())
                            {
                                pam.PAMSelectedAudiences.Add(
                                    (PAMAudiance)reader.GetDefaultIfDBNull<short>(T.Pam.PAMAudienceID, GetInt16, 0));
                            }


                            reader.NextResult();

                            while (reader.Read())
                            {
                                pam.OtherPamAudienceSpecified = reader.GetDefaultIfDBNull(T.Pam.Description, GetString, null);
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
                                    pam.CMSSpecialUseFields.Add(specialFieldValue);
                                else
                                    pam.StateSpecialUseFields.Add(specialFieldValue);
                            }

                        }
                    }
                }

                return pam;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

        }


        public IEnumerable<PublicMediaEvent> SearchPamByStartDate(DateTime startDate,string stateCode)
        {
            var pams = new List<PublicMediaEvent>();
            IDataReader reader = null;

            try
            {

                using (var command = database.GetStoredProcCommand("dbo.SearchPAMbyStartDate"))
                {
                    database.AddInParameter(command, "@ActivityStartDate", DbType.DateTime, startDate);
                    database.AddInParameter(command, "@StateFIPS", DbType.String, stateCode);

                    using (reader = database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            pams.Add(new PublicMediaEvent
                            {
                                PamID = reader.GetDefaultIfDBNull(T.Pam.PAMID, GetNullableInt32, null),

                                AgencyId = reader.GetDefaultIfDBNull(T.Pam.AgencyID, GetNullableInt32, null),
                                AgencyName = reader.GetDefaultIfDBNull(T.Pam.AgencyName, GetString, null),
                                ActivityStartDate = reader.GetDefaultIfDBNull(T.Pam.ActivityStartDate, GetNullableDateTime, null),
                                ActivityEndDate = reader.GetDefaultIfDBNull(T.Pam.ActivityEndDate, GetNullableDateTime, null),

                                EventName = reader.GetDefaultIfDBNull(T.Pam.EventName, GetString, null),
                                ContactFirstName = reader.GetDefaultIfDBNull(T.Pam.ContactFirstName, GetString, null),
                                ContactLastName = reader.GetDefaultIfDBNull(T.Pam.ContactLastName, GetString, null),
                                ContactPhone = reader.GetDefaultIfDBNull(T.Pam.ContactPhone, GetString, null),

                                EventState = new State(reader.GetDefaultIfDBNull(T.Pam.EventStateCode, GetString, null)),
                                EventCountycode = reader.GetDefaultIfDBNull(T.Pam.EventCountycode, GetString, null),
                                EventZIPCode = reader.GetDefaultIfDBNull(T.Pam.EventZIPCode, GetString, null),
                                EventCity = reader.GetDefaultIfDBNull(T.Pam.EventCity, GetString, null),
                                EventStreet = reader.GetDefaultIfDBNull(T.Pam.EventStreet, GetString, null),
                            });
                        }
                    }
                }

                return pams;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public IEnumerable<PublicMediaEvent> SearchPamByStartDateRange(DateTime startDateFrom, DateTime startDateTo, string stateCode)
        {
            var pams = new List<PublicMediaEvent>();
            IDataReader reader = null;

            try
            {

                using (var command = database.GetStoredProcCommand("dbo.SearchPAMbyStartDateRange"))
                {
                    database.AddInParameter(command, "@ActivityStartDateFrom", DbType.DateTime, startDateFrom);
                    database.AddInParameter(command, "@ActivityStartDateTo", DbType.DateTime, startDateTo);
                    database.AddInParameter(command, "@StateFIPS", DbType.String, stateCode);

                    using (reader = database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            pams.Add(new PublicMediaEvent
                            {
                                PamID = reader.GetDefaultIfDBNull(T.Pam.PAMID, GetNullableInt32, null),
                                AgencyName = reader.GetDefaultIfDBNull(T.Pam.AgencyName, GetString, null),

                                AgencyId = reader.GetDefaultIfDBNull(T.Pam.AgencyID, GetNullableInt32, null),

                                ActivityStartDate = reader.GetDefaultIfDBNull(T.Pam.ActivityStartDate, GetNullableDateTime, null),
                                ActivityEndDate = reader.GetDefaultIfDBNull(T.Pam.ActivityEndDate, GetNullableDateTime, null),

                                EventName = reader.GetDefaultIfDBNull(T.Pam.EventName, GetString, null),
                                ContactFirstName = reader.GetDefaultIfDBNull(T.Pam.ContactFirstName, GetString, null),
                                ContactLastName = reader.GetDefaultIfDBNull(T.Pam.ContactLastName, GetString, null),
                                ContactPhone = reader.GetDefaultIfDBNull(T.Pam.ContactPhone, GetString, null),

                                EventState = new State(reader.GetDefaultIfDBNull(T.Pam.EventStateCode, GetString, null)),
                                EventCountycode = reader.GetDefaultIfDBNull(T.Pam.EventCountycode, GetString, null),
                                EventZIPCode = reader.GetDefaultIfDBNull(T.Pam.EventZIPCode, GetString, null),
                                EventCity = reader.GetDefaultIfDBNull(T.Pam.EventCity, GetString, null),
                                EventStreet = reader.GetDefaultIfDBNull(T.Pam.EventStreet, GetString, null),
                            });
                        }
                    }
                }

                return pams;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public IEnumerable<PublicMediaEvent> SearchPamBySubmitter(int SubmitterUserId, string stateCode)
        {
            var pams = new List<PublicMediaEvent>();
            IDataReader reader = null;

            try
            {

                using (var command = database.GetStoredProcCommand("dbo.SearchPAMbySubmitter"))
                {
                    database.AddInParameter(command, "@SubmitterUserId", DbType.Int32, SubmitterUserId);
                    database.AddInParameter(command, "@StateFIPS", DbType.String, stateCode);

                    using (reader = database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            pams.Add(new PublicMediaEvent
                            {
                                PamID = reader.GetDefaultIfDBNull(T.Pam.PAMID, GetNullableInt32, null),

                                AgencyId = reader.GetDefaultIfDBNull(T.Pam.AgencyID, GetNullableInt32, null),
                                AgencyName = reader.GetDefaultIfDBNull(T.Pam.AgencyName, GetString, null),

                                ActivityStartDate = reader.GetDefaultIfDBNull(T.Pam.ActivityStartDate, GetNullableDateTime, null),
                                ActivityEndDate = reader.GetDefaultIfDBNull(T.Pam.ActivityEndDate, GetNullableDateTime, null),

                                EventName = reader.GetDefaultIfDBNull(T.Pam.EventName, GetString, null),
                                ContactFirstName = reader.GetDefaultIfDBNull(T.Pam.ContactFirstName, GetString, null),
                                ContactLastName = reader.GetDefaultIfDBNull(T.Pam.ContactLastName, GetString, null),
                                ContactPhone = reader.GetDefaultIfDBNull(T.Pam.ContactPhone, GetString, null),

                                EventState = new State(reader.GetDefaultIfDBNull(T.Pam.EventStateCode, GetString, null)),
                                EventCountycode = reader.GetDefaultIfDBNull(T.Pam.EventCountycode, GetString, null),
                                EventZIPCode = reader.GetDefaultIfDBNull(T.Pam.EventZIPCode, GetString, null),
                                EventCity = reader.GetDefaultIfDBNull(T.Pam.EventCity, GetString, null),
                                EventStreet = reader.GetDefaultIfDBNull(T.Pam.EventStreet, GetString, null),
                            });
                        }
                    }
                }

                return pams;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public IEnumerable<PublicMediaEvent> SearchPamByPresentor(int PamUserID)
        {
            var pams = new List<PublicMediaEvent>();
            IDataReader reader = null;

            try
            {

                using (var command = database.GetStoredProcCommand("dbo.SearchPAMbyPresentor"))
                {
                    database.AddInParameter(command, "@PamUserId", DbType.Int32, PamUserID);

                    using (reader = database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            pams.Add(new PublicMediaEvent
                            {
                                PamID = reader.GetDefaultIfDBNull(T.Pam.PAMID, GetNullableInt32, null),

                                AgencyId = reader.GetDefaultIfDBNull(T.Pam.AgencyID, GetNullableInt32, null),
                                AgencyName = reader.GetDefaultIfDBNull(T.Pam.AgencyName, GetString, null),

                                ActivityStartDate = reader.GetDefaultIfDBNull(T.Pam.ActivityStartDate, GetNullableDateTime, null),
                                ActivityEndDate = reader.GetDefaultIfDBNull(T.Pam.ActivityEndDate, GetNullableDateTime, null),

                                EventName = reader.GetDefaultIfDBNull(T.Pam.EventName, GetString, null),
                                ContactFirstName = reader.GetDefaultIfDBNull(T.Pam.ContactFirstName, GetString, null),
                                ContactLastName = reader.GetDefaultIfDBNull(T.Pam.ContactLastName, GetString, null),
                                ContactPhone = reader.GetDefaultIfDBNull(T.Pam.ContactPhone, GetString, null),

                                EventState = new State(reader.GetDefaultIfDBNull(T.Pam.EventStateCode, GetString, null)),
                                EventCountycode = reader.GetDefaultIfDBNull(T.Pam.EventCountycode, GetString, null),
                                EventZIPCode = reader.GetDefaultIfDBNull(T.Pam.EventZIPCode, GetString, null),
                                EventCity = reader.GetDefaultIfDBNull(T.Pam.EventCity, GetString, null),
                                EventStreet = reader.GetDefaultIfDBNull(T.Pam.EventStreet, GetString, null),
                            });
                        }
                    }
                }

                return pams;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }


        //sammit not used..
        public IEnumerable<SearchPublicMediaEventViewData> SearchPam(int AgencyId, DateTime? ActivityStartDateFrom, DateTime? ActivityStartDateTo)
        {
            var pams = new List<SearchPublicMediaEventViewData>();
            IDataReader reader = null;

            try 
            {

                using (var command = database.GetStoredProcCommand("dbo.SearchPAM"))
                {

                    database.AddInParameter(command, Constants.StoredProcs.SearchPAM.AgencyId, DbType.Int32, AgencyId);
                    database.AddInParameter(command, Constants.StoredProcs.SearchPAM.ActivityStartDateFrom, DbType.Date, ActivityStartDateFrom);
                    database.AddInParameter(command, Constants.StoredProcs.SearchPAM.ActivityStartDateTo, DbType.Date, ActivityStartDateTo);


                    using (reader = database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            pams.Add(new SearchPublicMediaEventViewData
                            {
                                PamID = reader.GetDefaultIfDBNull(T.Pam.PAMID, GetNullableInt32, null),

                                AgencyId = reader.GetDefaultIfDBNull(T.Pam.AgencyID, GetNullableInt32, null),
                                AgencyName = reader.GetDefaultIfDBNull(T.Pam.AgencyName, GetString, null),

                                ActivityStartDate = reader.GetDateTime(reader.GetOrdinal(T.Pam.ActivityStartDate)),
                                ActivityEndDate = reader.GetDateTime(reader.GetOrdinal(T.Pam.ActivityEndDate)),

                                EventName = reader.GetDefaultIfDBNull(T.Pam.EventName, GetString, null),

                            });
                        }
                    }
                }

                return pams;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }



        public IEnumerable<SearchPublicMediaEventViewData> GetRecentPams(int userId)
        {
            var pams = new List<SearchPublicMediaEventViewData>();

            using (var command = database.GetStoredProcCommand("dbo.GetRecentPams"))
            {
                database.AddInParameter(command, SP.GetRecentClientContacts.UserId, DbType.Int32, userId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        pams.Add(new SearchPublicMediaEventViewData
                        {
                            PamID = reader.GetDefaultIfDBNull(T.Pam.PAMID, GetNullableInt32, null),

                            AgencyId = reader.GetDefaultIfDBNull(T.Pam.AgencyID, GetNullableInt32, null),
                            AgencyName = reader.GetDefaultIfDBNull(T.Pam.AgencyName, GetString, null),

                            ActivityStartDate = reader.GetDateTime(reader.GetOrdinal(T.Pam.ActivityStartDate)),
                            ActivityEndDate = reader.GetDateTime(reader.GetOrdinal(T.Pam.ActivityEndDate)),

                            EventName = reader.GetDefaultIfDBNull(T.Pam.EventName, GetString, null),

                        });
                    }
                }

            }

            return pams;
        }

        public IEnumerable<SearchPublicMediaEventViewData> SearchPams(int UserID, int scopeId, string StateFIPS,  DateTime? fromContactDate, DateTime? toContactDate, int? presenterId, int? submitterId, int? AgencyId)
        {
            var pams = new List<SearchPublicMediaEventViewData>();

            using (var command = database.GetStoredProcCommand("dbo.SearchPams"))
            {
                database.AddInParameter(command, SP.SearchClientContacts.UserId, DbType.Int32, UserID);
                database.AddInParameter(command, SP.SearchClientContacts.ScopeId, DbType.Int32, scopeId);
                database.AddInParameter(command, SP.SearchClientContacts.StateFIPS, DbType.String, StateFIPS);
                //database.AddInParameter(command, SP.SearchClientContacts.Keywords, DbType.String, string.IsNullOrEmpty(keywords) ? string.Empty : keywords);
                database.AddInParameter(command, SP.SearchClientContacts.FromContactDate, DbType.Date, fromContactDate);
                database.AddInParameter(command, SP.SearchClientContacts.ToContactDate, DbType.Date, toContactDate);
                database.AddInParameter(command, "PresenterId", DbType.Int32, presenterId);
                database.AddInParameter(command, SP.SearchClientContacts.SubmitterId, DbType.Int32, submitterId);
                database.AddInParameter(command, SP.SearchClientContacts.AgencyId, DbType.Int32, AgencyId);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        pams.Add(new SearchPublicMediaEventViewData
                        {
                            PamID = reader.GetDefaultIfDBNull(T.Pam.PAMID, GetNullableInt32, null),

                            AgencyId = reader.GetDefaultIfDBNull(T.Pam.AgencyID, GetNullableInt32, null),
                            AgencyName = reader.GetDefaultIfDBNull(T.Pam.AgencyName, GetString, null),

                            ActivityStartDate = reader.GetDateTime(reader.GetOrdinal(T.Pam.ActivityStartDate)),
                            ActivityEndDate = reader.GetDateTime(reader.GetOrdinal(T.Pam.ActivityEndDate)),

                            EventName = reader.GetDefaultIfDBNull(T.Pam.EventName, GetString, null),

                        });
                    }
                }

            }

            return pams;
        }


        public  List<KeyValuePair<int, string>>  GetPAMSubmittersForScope(string StateFIPS, int ScopeId, int userId, bool IsActive)
        {
            var SubmittersList = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand(StoredProcNames.Lookup.GetPAMSubmittersForScope.Description()))
            {
                database.AddInParameter(command, Constants.StoredProcs.GetPAMSubmittersForScope.StateFIPS, DbType.String, StateFIPS);
                database.AddInParameter(command, Constants.StoredProcs.GetPAMSubmittersForScope.ScopeId, DbType.Int16, ScopeId);
                database.AddInParameter(command, Constants.StoredProcs.GetPAMSubmittersForScope.userId, DbType.Int16, userId);
                database.AddInParameter(command, Constants.StoredProcs.GetPAMSubmittersForScope.IsActive, DbType.Boolean, IsActive);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        SubmittersList.Add(new KeyValuePair<int, string>(reader.GetInt32(getOrdinal(reader, "SubmitterUserID")), reader.GetString(getOrdinal(reader, "Name"))));
                    }
                }
            }
            return SubmittersList;
        }


        public  List<KeyValuePair<int, string>> GetPAMPresentorsForScope(string StateFIPS, int ScopeId, int userId, bool IsActive)
        {
            var PresentorsList = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand(StoredProcNames.Lookup.GetPAMPresentersForScope.Description()))
            {
                database.AddInParameter(command, Constants.StoredProcs.GetPAMPresentersForScope.StateFIPS, DbType.String, StateFIPS);
                database.AddInParameter(command, Constants.StoredProcs.GetPAMPresentersForScope.ScopeId, DbType.Int16, ScopeId);
                database.AddInParameter(command, Constants.StoredProcs.GetPAMPresentersForScope.userId, DbType.Int16, userId);
                database.AddInParameter(command, Constants.StoredProcs.GetPAMPresentersForScope.IsActive, DbType.Boolean, IsActive);


                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        PresentorsList.Add(new KeyValuePair<int, string>(reader.GetInt32(getOrdinal(reader, "PAMUserID")), reader.GetString(getOrdinal(reader, "Name"))));
                    }
                }
            }
            return PresentorsList;
        }


        public IEnumerable<KeyValuePair<int, string>> GetPamPresentersByAgency(int UserId, string StateFIPS, int ScopeId, int AgencyId)
        {
            var counselors = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand("dbo.GetPamPresentersByAgency"))
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


        public IEnumerable<KeyValuePair<int, string>> GetAllPamPresenters(int UserId, string StateFIPS, int ScopeId)
        {
            var counselors = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand("dbo.GetAllPamPresenters"))
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


        public IEnumerable<KeyValuePair<int, string>> GetPamPresentersByAgencyActiveInactive(int UserId, string StateFIPS, int ScopeId, int AgencyId, bool IsActive)
        {
            var submitters = new List<KeyValuePair<int, string>>();

            using (var command = database.GetStoredProcCommand("dbo.GetPamPresentersByAgencyActiveInactive"))
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

        int getOrdinal(IDataRecord dataRecord, string name)
        {
            int ordinal = dataRecord.GetOrdinal(name);
            return ordinal;
        }

    }
}
