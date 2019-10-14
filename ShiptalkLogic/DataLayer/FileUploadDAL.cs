using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using ShiptalkLogic.BusinessObjects;

namespace ShiptalkLogic.DataLayer
{
    public class FileUploadDAL
    {
        public FileUploadDAL()
        { }




        /// <summary>
        /// Adds a Batch upload status record
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="StateFIPS"></param>
        /// <param name="Status"></param>
        /// <param name="Comments"></param>
        /// <param name="UploadId"></param>
        /// <returns>Primary Key of batch status upload record created.</returns>
        public static int AddUploadStatus(string Status, string Comments, int UploadId)
        {
            int PK = -1;
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.BatchUpload.AddBatchUploadStatus.Description()))
            {
                db.AddInParameter(dbCmd, "@Status", DbType.String, Status);
                db.AddInParameter(dbCmd, "@Comments", DbType.String, Comments);
                db.AddInParameter(dbCmd, "@UploadId", DbType.Int32, UploadId);
                db.AddOutParameter(dbCmd, "@BatchUploadStatusID", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                PK = (int)db.GetParameterValue(dbCmd, "BatchUploadStatusID");
                
            }
            return PK;
        }

        /// <summary>
        /// Returns the Pam ID of the record that was uploaded.
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <param name="AgencyCode"></param>
        /// <param name="BatchStateUniqueID"></param>
        /// <returns></returns>
        public static int? GetUploadedPamID(string AgencyCode, string BatchStateUniqueID)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            IDataReader rdrPamRecord = null;
            try
            {
                using (DbCommand dbCmd = db.GetStoredProcCommand("GetUploadedPamRecord"))
                {
                    db.AddInParameter(dbCmd, "@AgencyCode", DbType.String, AgencyCode);
                    db.AddInParameter(dbCmd, "@BatchStateUniqueID", DbType.String, BatchStateUniqueID);
                    rdrPamRecord = db.ExecuteReader(dbCmd);
                    rdrPamRecord.Read();
                    if (!rdrPamRecord.IsDBNull(0))
                    {
                        return int.Parse(rdrPamRecord["PAMID"].ToString());
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            finally
            {
                if(rdrPamRecord != null)
                    rdrPamRecord.Close();
            }
          
        }

        public static IEnumerable<SpecialField> GetSpecialUploadFieldsValues(FormType DataFormat, State StateValue)
        {
            SpecialFieldsAccess sfa = new SpecialFieldsAccess();
            return sfa.GetSpecialFields(DataFormat, StateValue, false);
        }

        /// <summary>
        /// Determine if the State is a SubState
        /// </summary>
        /// <param name="StateFIPCode"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static bool IsSubStateValid(string StateFIPCode, int UserID)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.UserSubStateRegion.GetUserSubStateRegionProfiles.Description()))
            {
                bool  IsStateCodeFound = false;
                db.AddInParameter(dbCmd, "@UserID", DbType.Int32, UserID);
                IDataReader rdrSubState = null;
                try
                {

                    rdrSubState = db.ExecuteReader(dbCmd);
                    while (rdrSubState.Read())
                    {
                        if (rdrSubState["StateFIPS"].ToString() == StateFIPCode)
                        {
                            IsStateCodeFound = true;
                            break;
                        }
                    }
                }
                finally
                {
                    if (rdrSubState != null)
                    {
                        rdrSubState.Close();
                        rdrSubState.Dispose();
                    }
                }
                return IsStateCodeFound;
            }
        }

        public static int GetClientContactID(string BatchStateUniqueID, int AgencyID)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand("GetClientContactByBatchStateUniqueID"))
            {
                db.AddInParameter(dbCmd, "@BatchStateUniqueID", DbType.String, BatchStateUniqueID);
                db.AddInParameter(dbCmd, "@AgencyID", DbType.String, AgencyID);
                int? ID = (int)db.ExecuteScalar(dbCmd);
                return ID.Value;
            }

            


        }

        /// <summary>
        /// Determine if the Client Contact record has been previously uploaded.
        /// </summary>
        /// <param name="StateFIPCode"></param>
        /// <returns></returns>
        public static bool IsClientContactRecordUploaded(string AgencyCode, string RecordID)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.Lookup.IsClientContactRecordUploaded.Description()))
            {
                db.AddInParameter(dbCmd, "@AgencyCode", DbType.String, AgencyCode);
                db.AddInParameter(dbCmd, "@BatchStateUniqueID", DbType.String, RecordID);
                bool IsValid = Convert.ToBoolean(db.ExecuteScalar(dbCmd));
                return IsValid;
            }

        }


        public static string FindExistingAutoAssignedClientID(string StateSpecificClientID,  string AgencyCode, string StateFIPS)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.BatchUpload.GetExistingAutoAssignedClientID.Description()))
            {
                db.AddInParameter(dbCmd, "@StateSpecificClientID", DbType.String, StateSpecificClientID);
                db.AddInParameter(dbCmd, "@AgencyCode", DbType.String, AgencyCode);
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.String, StateFIPS);

                object ClientID = db.ExecuteScalar(dbCmd);
                if (ClientID == null)
                    return null;
                else
                    return Convert.ToString(ClientID);
                
            }
        }

        public static string GetNextAutoAssignedClientID(string AgencyCode)
        {
            CCFDAL ccFunc = new CCFDAL();
            return ccFunc.GetNextAutoAssignedClientID(AgencyCode);
        }

        /// <summary>
        /// Determine if the Client Contact record has been previously uploaded.
        /// </summary>
        /// <param name="StateFIPCode"></param>
        /// <returns></returns>
        public static bool IsPamRecordUploaded(string AgencyCode, string RecordID)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand("IsPamRecordUploaded"))
            {
                db.AddInParameter(dbCmd, "@AgencyCode", DbType.String, AgencyCode);
                db.AddInParameter(dbCmd, "@BatchStateUniqueID", DbType.String, RecordID);
                bool IsValid = Convert.ToBoolean(db.ExecuteScalar(dbCmd));
                return IsValid;
            }

        }


        /// <summary>
        ///  Adds a Records to the Upload files table.
        /// </summary>
        /// <param name="StateFIPS"></param>
        /// <param name="UploadedName"></param>
        /// <param name="InternalName"></param>
        /// <param name="FileType"></param>
        /// <param name="InvalidRecords"></param>
        /// <param name="RecordsProcessed"></param>
        /// <returns></returns>
        public static int AddUploadfile(string StateFIPS, string CleanFileName,string OriginalFileName, string UploadedFileName,string ErrorFileName, string FileType, int UserId)
        {
            int BatchUploadedPK = -1;

            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.BatchUpload.AddBatchUploadFile.Description()))
            {
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.String, StateFIPS);
                db.AddInParameter(dbCmd, "@UploadedName", DbType.String, UploadedFileName);
                db.AddInParameter(dbCmd, "@CleanFileName", DbType.String, CleanFileName);
                db.AddInParameter(dbCmd, "@ErrorFileName", DbType.String, ErrorFileName);
                db.AddInParameter(dbCmd, "@OriginalFileName", DbType.String, OriginalFileName);
                db.AddInParameter(dbCmd, "@FileType", DbType.String, FileType);
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                db.AddOutParameter(dbCmd, "@BatchUploadFileID", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                BatchUploadedPK = (int)db.GetParameterValue(dbCmd, "BatchUploadFileID");
            }
            return BatchUploadedPK;

        }

        /// <summary>
        /// Updates the number of records processed and invalid records of a upload file record.
        /// </summary>
        /// <param name="UploadId"></param>
        /// <param name="InvalidRecords"></param>
        /// <param name="RecordsProcessed"></param>
        /// <returns>Number of Records Updated</returns>
        public static int UpdateFileUploadsRecordsProcessed(int UploadId, int InvalidRecords, int RecordsProcessed)
        {
            int RecordsUpdated = 0;
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.BatchUpload.UpdateFileUploadsRecordsProcessed.Description()))
            {
                db.AddInParameter(dbCmd, "@UploadFileId", DbType.Int32, UploadId);
                db.AddInParameter(dbCmd, "@InvalidRecords", DbType.Int32, InvalidRecords);
                db.AddInParameter(dbCmd, "@RecordsProcessed", DbType.Int32, RecordsProcessed);
                RecordsUpdated = db.ExecuteNonQuery(dbCmd);
            }

            return RecordsUpdated;
        }

        public static DataSet GetFileUploadStatusByUser(int UserId)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.BatchUpload.GetFileUploadStatusByUser.Description()))
            {
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserId);
                return db.ExecuteDataSet(dbCmd);
                
            }
        }

        public static void UpdateBatchUploadState(int UploadID, string StateFips)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.BatchUpload.UpdateBatchUploadState.Description()))
            {
                db.AddInParameter(dbCmd, "@UploadID", DbType.Int32, UploadID);
                db.AddInParameter(dbCmd, "@StateFips", DbType.String, StateFips);
                db.ExecuteNonQuery(dbCmd);
            }
        }


        public static bool IsUserHasAgencyAssociation(string AgencyCode, int UserID)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand("IsUserHasAgencyAssociation"))
            {
                db.AddInParameter(dbCmd, "@AgencyCode", DbType.String, AgencyCode);
                db.AddInParameter(dbCmd, "@UserId", DbType.Int32, UserID);
                bool IsValid = Convert.ToBoolean(db.ExecuteScalar(dbCmd));
                return IsValid;
            }

        }
       


        class SpecialFieldsAccess : FormDALBase
        {}

       
    }

}
