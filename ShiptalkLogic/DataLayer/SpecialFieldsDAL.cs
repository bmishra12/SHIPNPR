using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using T = ShiptalkLogic.Constants.Tables;
using SP = ShiptalkLogic.Constants.StoredProcs;


namespace ShiptalkLogic.DataLayer
{
    public class SpecialFieldsDAL 
    {
        public static int AddSpecialField(SpecialField Rec)
        {
             
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd  = db.GetStoredProcCommand(StoredProcNames.SpecialFields.AddSpecialFields.Description()))
            {
                db.AddInParameter(dbCmd, "@SpecialFieldID", DbType.String, Rec.Id);
                db.AddInParameter(dbCmd, "@Name", DbType.String, Rec.Name);
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.String, Rec.State.Code);
                db.AddInParameter(dbCmd, "@FormType", DbType.Int32, (int)Rec.FormType );
                db.AddInParameter(dbCmd, "@StartDate", DbType.Date, Rec.StartDate);
                db.AddInParameter(dbCmd, "@EndDate", DbType.Date, Rec.EndDate);
                db.AddInParameter(dbCmd, "@Description", DbType.String, Rec.Description);
                db.AddInParameter(dbCmd, "@ValidationType", DbType.Int32, (int)Rec.ValidationType);
                db.AddInParameter(dbCmd, "@IsRequired", DbType.Boolean, Rec.IsRequired);               
                db.AddInParameter(dbCmd, "@Range", DbType.String, Rec.Range);
                db.AddInParameter(dbCmd, "@CreatedBy", DbType.Int32, Rec.CreatedBy);
                db.AddInParameter(dbCmd, "@Ordinal", DbType.Int32, Rec.Ordinal);
                db.AddInParameter(dbCmd, "@CreatedDate", DbType.DateTime, Rec.CreatedDate);
                db.AddOutParameter(dbCmd, "@ReturnID", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCmd);
                return int.Parse(db.GetParameterValue(dbCmd, "@ReturnID").ToString());
                
            }
        }


        public static SpecialFieldValue GetSpecialFieldInfo(DateTime DateOfContact, FormType FrmType, int Ordinal, string StateFips)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.SpecialFields.GetSpecialFieldName.Description()))
            {
                db.AddInParameter(dbCmd, "@DateOfContact", DbType.DateTime, DateOfContact);
                db.AddInParameter(dbCmd, "@FormType", DbType.Int32, (int)FrmType);
                db.AddInParameter(dbCmd, "@Ordinal", DbType.Int32, Ordinal);
                db.AddInParameter(dbCmd, "@StateFips", DbType.String, StateFips);
                IDataReader rdr = null;
                try
                {
                    rdr = db.ExecuteReader(dbCmd);
                    if (!rdr.Read())
                    {
                        return null;
                    }
                    else
                    {
                        SpecialFieldValue spVal = new SpecialFieldValue();
                        spVal.Name = rdr["Name"].ToString();
                        spVal.Id = Convert.ToInt32(rdr["SpecialFieldID"]);
                        spVal.Range = rdr["Range"].ToString().Trim();
                        spVal.ValidationType = int.Parse(rdr["ValidationType"].ToString());
                        spVal.IsRequired = Convert.ToBoolean(rdr["IsRequired"].ToString());
                        return spVal;
                    }
                }
                finally
                {
                    if (rdr != null)
                    {
                        rdr.Close();
                        rdr.Dispose();
                    }
                }
            }

        }
        //Added by Lavanya
        public static DataTable GetSpecialFieldInformation(DateTime DateOfContact, FormType FrmType, string StateFips)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.SpecialFields.GetSpecialFieldName.Description()))
            {
                db.AddInParameter(dbCmd, "@DateOfContact", DbType.DateTime, DateOfContact);
                db.AddInParameter(dbCmd, "@FormType", DbType.Int32, (int)FrmType);
                //db.AddInParameter(dbCmd, "@Ordinal", DbType.Int32, Ordinal);
                db.AddInParameter(dbCmd, "@StateFips", DbType.String, StateFips);

                try
                {
                    DataSet ds = db.ExecuteDataSet(dbCmd);
                    DataTable dt = null;

                    if (ds != null)
                    {
                        dt = ds.Tables[0];
                        return dt;
                    }
                    else
                        return null;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
               
            }

        }

        public static string GetSpecialFieldName(DateTime DateOfContact, FormType FrmType, int Ordinal, string StateFips)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.SpecialFields.GetSpecialFieldName.Description()))
            {
                db.AddInParameter(dbCmd, "@DateOfContact", DbType.DateTime, DateOfContact);
                db.AddInParameter(dbCmd, "@FormType", DbType.Int32, (int)FrmType);
                db.AddInParameter(dbCmd, "@Ordinal", DbType.Int32, Ordinal);
                db.AddInParameter(dbCmd, "@StateFips", DbType.String, StateFips);
                 IDataReader rdr = null;
                try
                {
                    rdr = db.ExecuteReader(dbCmd);
                    if (!rdr.Read())
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return rdr["Name"].ToString();
                    }
                }
                finally
                {
                    if(rdr != null)
                    {
                        rdr.Close();
                        rdr.Dispose();
                    }
                }
            }

        }
        public static void DeleteSpecialField(int SpecialFieldId)
        {
               Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
               using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.SpecialFields.DeleteSpecialField.Description()))
               {
                   db.AddInParameter(dbCmd, "@SpecialFieldId", DbType.Int32, SpecialFieldId);
                   db.ExecuteNonQuery(dbCmd);
               }
         
        }


        public static void SetSpecialFieldsValidStartEndDate(ref DateTime? StartDate, ref DateTime? EndDate, string StateFIPS, FormType DataForm)
        {
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.SpecialFields.GetSpecialFieldsValidStartEndDate.Description()))
            {
                db.AddInParameter(dbCmd, "@StartDate", DbType.DateTime, StartDate);
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.String, StateFIPS);
                db.AddInParameter(dbCmd, "@FormType", DbType.Int32, (int)DataForm);
                IDataReader rdrDates = null;
                
                try
                {
                    rdrDates = db.ExecuteReader(dbCmd);
                    while (rdrDates.Read())
                    {
                        StartDate = Convert.ToDateTime(rdrDates["StartDate"].ToString());
                        EndDate = Convert.ToDateTime(rdrDates["EndDate"].ToString());
                    }
                }
                finally
                {
                    if (rdrDates != null)
                    {
                        rdrDates.Close();
                        rdrDates.Dispose();
                    }
                }
            }
            

        }

        public static IEnumerable<ViewSpecialFieldsViewData> GetSpecialFields(string StateFIPS, DateTime StartDate, DateTime EndDate, FormType DataForm)
        {
            List<ViewSpecialFieldsViewData> FoundSpecialFields = new List<ViewSpecialFieldsViewData>();
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.SpecialFields.GetSpecialFieldsByDate.Description()))
            {
                if( EndDate.Year != 1)
                    db.AddInParameter(dbCmd, "@EndDate", DbType.DateTime, EndDate);
                
                db.AddInParameter(dbCmd, "@StartDate", DbType.DateTime, StartDate);
                db.AddInParameter(dbCmd, "@StateFIPS", DbType.String, StateFIPS);
                db.AddInParameter(dbCmd, "@FormType", DbType.Int32, (int)DataForm);
                IDataReader rdrSpecialFields = null;
                try
                {
                    rdrSpecialFields = db.ExecuteReader(dbCmd);
                    while (rdrSpecialFields.Read())
                    {
                        if (!rdrSpecialFields.IsDBNull(0))
                        {
                            ViewSpecialFieldsViewData FoundField = new ViewSpecialFieldsViewData();

                            if (rdrSpecialFields["CreatedBy"] != null)
                                FoundField.CreatedBy = (int)rdrSpecialFields["CreatedBy"];


                            if (rdrSpecialFields["CreatedDate"] != null)
                                FoundField.CreatedDate = ((DateTime)rdrSpecialFields["CreatedDate"]).ToLongDateString();


                            if (!string.IsNullOrEmpty(rdrSpecialFields["Description"].ToString()))
                                FoundField.Description = (string)rdrSpecialFields["Description"];

                            FoundField.EndDate = ((DateTime)rdrSpecialFields["EndDate"]).ToLongDateString();
                            FoundField.FormType = ((FormType)int.Parse(rdrSpecialFields["FormType"].ToString())).ToString();
                            FoundField.Id = int.Parse(rdrSpecialFields["SpecialFieldId"].ToString());
                            if ((bool)rdrSpecialFields["IsRequired"])
                            {
                                FoundField.IsRequired = "Yes";
                            }
                            else
                            {
                                FoundField.IsRequired = "No";
                            }
                            FoundField.Name = rdrSpecialFields["Name"].ToString();
                            FoundField.StartDate = Convert.ToDateTime(rdrSpecialFields["StartDate"].ToString()).ToLongDateString();
                            FoundField.State = new State(rdrSpecialFields["StateFIPS"].ToString());
                            FoundField.ValidationType = (ValidationType)int.Parse(rdrSpecialFields["ValidationType"].ToString());
                            FoundField.Ordinal = int.Parse(rdrSpecialFields["Ordinal"].ToString());
                            FoundSpecialFields.Add(FoundField);
                        }
                    }
                    return FoundSpecialFields;
                }
                finally
                {
                    if (rdrSpecialFields != null)
                    {
                        rdrSpecialFields.Close();
                        rdrSpecialFields.Dispose();
                    }
                }

            }

        }
        public static SpecialField GetSpecialFieldsById(int SpecialFieldId)
        {
            //GetSpecialFieldsById
            Database db = DatabaseFactory.CreateDatabase("DB_SHIP-NPR");
            using (DbCommand dbCmd = db.GetStoredProcCommand(StoredProcNames.SpecialFields.GetSpecialFieldsById.Description()))
            {
                db.AddInParameter(dbCmd, "@SpecialFieldId", DbType.Int32, SpecialFieldId);
                IDataReader rdrSpecialFields = null;
                try
                {
                    rdrSpecialFields = db.ExecuteReader(dbCmd);
                    rdrSpecialFields.Read();
                    if (!rdrSpecialFields.IsDBNull(0))
                    {
                        SpecialField FoundField = new SpecialField();
                        
                        if(rdrSpecialFields["CreatedBy"] != null)
                            FoundField.CreatedBy = (int?)rdrSpecialFields["CreatedBy"];
                        
                        if (rdrSpecialFields["CreatedDate"] != null)
                            FoundField.CreatedDate = (DateTime?)rdrSpecialFields["CreatedDate"];
                        
                        if ( !string.IsNullOrEmpty(rdrSpecialFields["LastUpdatedBy"].ToString()))
                            FoundField.LastUpdatedBy = (int?)rdrSpecialFields["LastUpdatedBy"];
                        
                        if (!string.IsNullOrEmpty(rdrSpecialFields["Description"].ToString()))
                            FoundField.Description = (string)rdrSpecialFields["Description"];

                        FoundField.EndDate = (DateTime)rdrSpecialFields["EndDate"];
                        FoundField.FormType = (FormType)int.Parse(rdrSpecialFields["FormType"].ToString());
                        FoundField.Id = int.Parse(rdrSpecialFields["SpecialFieldId"].ToString());
                        FoundField.IsRequired = (bool)rdrSpecialFields["IsRequired"];
                        FoundField.Name = rdrSpecialFields["Name"].ToString();
                        FoundField.StartDate = Convert.ToDateTime(rdrSpecialFields["StartDate"].ToString());
                        FoundField.State = new State(rdrSpecialFields["StateFIPS"].ToString());
                        FoundField.ValidationType = (ValidationType)int.Parse(rdrSpecialFields["ValidationType"].ToString());
                        FoundField.Ordinal = int.Parse(rdrSpecialFields["Ordinal"].ToString());
                        //Added Range : Lavnaya Maram: 06/28/2013
                        FoundField.Range = rdrSpecialFields["Range"].ToString();
                        //
                        return FoundField;  
                    }
                }
                finally
                {
                    if (rdrSpecialFields != null)
                    {
                        rdrSpecialFields.Close();
                        rdrSpecialFields.Dispose();
                        
                    }
                }
                return null;


            }
        }
        
    }
}
