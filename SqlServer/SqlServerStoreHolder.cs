using BaseCore.Helps;
using Core.Adapters.SqlServer;
using System.Data;
using System.Data.SqlClient;


namespace SqlServer
{
    internal class SqlServerStoreHolder : ISqlServerStoreHolder
    {
        private ISecretsKeyHolder SecretsKeyHolder { get; }

        public SqlServerStoreHolder(ISecretsKeyHolder secretsKeyHolder)
        {
            SecretsKeyHolder = secretsKeyHolder;
        }

        public IDbConnection GetDbConnection(string key) => GetLazyStore(key);


        private IDbConnection GetLazyStore(string key)
        {
            var connectionString = SecretsKeyHolder.GetValue(key);
            return new SqlConnection(connectionString);
        }
    }
}
