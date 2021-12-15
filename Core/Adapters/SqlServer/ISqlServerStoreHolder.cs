using System.Data;

namespace Core.Adapters.SqlServer
{
    public interface ISqlServerStoreHolder
    {
        IDbConnection GetDbConnection(string key);
    }
}
