using System;
using System.Collections.Generic;
using System.Data;
using ShiptalkLogic.BusinessObjects;
using SP = ShiptalkLogic.Constants.StoredProcs;
using T = ShiptalkLogic.Constants.Tables;

namespace ShiptalkLogic.DataLayer
{
    internal abstract class FormDALBase : DALBase
    {
        public IEnumerable<SpecialField> GetSpecialFields(FormType formType, State state, bool restrictDate)
        {
            var specialFields = new List<SpecialField>();

            using (var command = database.GetStoredProcCommand("dbo.GetSpecialFieldsForForm"))
            {
                database.AddInParameter(command, SP.GetSpecialFielsForForm.FormType, DbType.Int32, formType);
                database.AddInParameter(command, SP.GetSpecialFielsForForm.StateFIPS, DbType.String, state.Code);
                database.AddInParameter(command, SP.GetSpecialFielsForForm.RestrictDate, DbType.Boolean, restrictDate);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        specialFields.Add(new SpecialField
                                        {
                                            CreatedBy = reader.GetDefaultIfDBNull(T.SpecialField.CreatedBy, GetNullableInt32, null),
                                            CreatedDate = reader.GetDefaultIfDBNull(T.SpecialField.CreatedDate, GetNullableDateTime, null),
                                            LastUpdatedBy = reader.GetDefaultIfDBNull(T.SpecialField.LastUpdatedBy, GetNullableInt32, null),
                                            LastUpdatedDate = reader.GetDefaultIfDBNull(T.SpecialField.LastUpdatedDate, GetNullableDateTime, null),
                                            Description = reader.GetDefaultIfDBNull(T.SpecialField.Description, GetString, null),
                                            EndDate = reader.GetDefaultIfDBNull(T.SpecialField.EndDate, GetDateTime, DateTime.MinValue),
                                            FormType = (FormType)reader.GetDefaultIfDBNull(T.SpecialField.FormType, GetNullableInt16, null),
                                            Id = reader.GetDefaultIfDBNull(T.SpecialField.SpecialFieldID, GetNullableInt32, null),
                                            IsRequired = reader.GetDefaultIfDBNull(T.SpecialField.IsRequired, GetBool, false),
                                            Name = reader.GetDefaultIfDBNull(T.SpecialField.Name, GetString, null),
                                            StartDate = reader.GetDefaultIfDBNull(T.SpecialField.StartDate, GetDateTime, DateTime.MinValue),
                                            State = new State(reader.GetDefaultIfDBNull(T.SpecialField.StateFIPS, GetString, null)),
                                            ValidationType = (ValidationType)reader.GetDefaultIfDBNull(T.SpecialField.ValidationType, GetNullableInt16, null),
                                            Ordinal = int.Parse(reader["Ordinal"].ToString()),
                                            //
                                            Range = reader.GetDefaultIfDBNull(T.SpecialField.Range, GetString, null),
                                            //
                                        });
                    }
                }

                return specialFields;
            }
        }
    }
}
