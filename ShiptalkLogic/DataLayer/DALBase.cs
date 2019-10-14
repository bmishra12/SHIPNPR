using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ShiptalkLogic.DataLayer
{
    internal abstract class DALBase
    {
        protected static readonly Func<IDataRecord, int, DateTime?> GetNullableDateTime = (record, i) => record.GetDateTime(i);
        protected static readonly Func<IDataRecord, int, int?> GetNullableInt16 = (record, i) => record.GetInt16(i);
        protected static readonly Func<IDataRecord, int, int?> GetNullableInt32 = (record, i) => record.GetInt32(i);
        protected static readonly Func<IDataRecord, int, string> GetString = (record, i) => record.GetString(i);
        protected static readonly Func<IDataRecord, int, double?> GetNullableDouble = (record, i) => record.GetDouble(i);
        protected static readonly Func<IDataRecord, int, bool> GetBool = (record, i) => record.GetBoolean(i);
        protected static readonly Func<IDataRecord, int, short> GetInt16 = (record, i) => record.GetInt16(i);
        protected static readonly Func<IDataRecord, int, int> GetInt32 = (record, i) => record.GetInt32(i);
        protected static readonly Func<IDataRecord, int, DateTime> GetDateTime = (record, i) => record.GetDateTime(i);

        protected readonly Database database;

        protected DALBase()
        {
            database = DatabaseFactory.CreateDatabase();
        }
    }
}
