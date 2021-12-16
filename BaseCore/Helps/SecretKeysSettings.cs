using System.Linq;

namespace BaseCore.Helps
{
    internal class SecretKeysSettings
    {
        public SecretKeysSettingsItem[] Items { get; set; }

        public string GetValue(string key)
        {
            var settings = Items.FirstOrDefault(x => x.Key == key);
            return settings?.Value ?? string.Empty;
        }
    }

    internal class SecretKeysSettingsItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
