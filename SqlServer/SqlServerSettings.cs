using System.Linq;

namespace SqlServer
{
    internal class SqlServerSettings
    {
        public SqlServerSettingsValue[] Values { get; set; }

        public string GetConnectionString(string key)
        {
            var settings = Values.FirstOrDefault(x => x.Key == key);
            return settings?.Value ?? string.Empty;
        }
    }

    internal class SqlServerSettingsValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
