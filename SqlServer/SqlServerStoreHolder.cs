using Core.Adapters.SqlServer;
using System.Data;
using System.Data.SqlClient;


namespace SqlServer
{
    internal class SqlServerStoreHolder : ISqlServerStoreHolder
    {
        public IDbConnection GetDbConnection(string key) => GetLazyStore(key);

        private IDbConnection GetLazyStore(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
