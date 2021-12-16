using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BaseCore.Helps
{
    internal class SecretsKeyHolder : ISecretsKeyHolder
    {
        private readonly SecretKeysSettings settings;

        public SecretsKeyHolder(IOptions<SecretKeysSettings> optionsDatabaseSettings)
        {
            settings = optionsDatabaseSettings.Value;
        }

        public string GetValue(string key) => GetLazyStore(key);

        private string GetLazyStore(string key)
        {
            return settings.GetValue(key);
        }


       
    }
}
