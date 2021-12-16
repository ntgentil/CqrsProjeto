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
            var value = settings.GetValue(key);
            return Decrypt(value);
        }


        private static string Decrypt(string value)
        {
            var cert = PrivateCertificate.GetCertificate();


            var decodeValue = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(cert.Decrypt(decodeValue, RSAEncryptionPadding.OaepSHA512));
        }
    }


    internal static class PrivateCertificate
    {
        internal static string Key =>
        "MIIEogIBAAKCAQEAtQUqJ3X/0J+MjvVGXZVcP8gy2yPqsSVpRRUNvcE6I2FhFb6w" +
        "SbuL/3TK3VPNbWSSIC3QmjK91VKcwdZS9/jZGcOZfBKbQjhjh1XNDXP9bBYUyRw0" +
        "7GCf6PHi7KAsQoLqAnvtOh+lHa701Wv8e2ncuPfteZW5qa7CTi6OeBWNV8M5tyOV" +
        "4rxaarE+JW3ZJaxQnQTSY0iVN5Uf+Pm3sfBBitroMKWK3sYtY+qlh2c74X6aBjr9" +
        "NjrmnOuoqL11PjXr8xMCe++EjzzQXv0NARtfsLB4o0DCeU5Cg3t2f+8dd3S4UKV4" +
        "5tqKcJiVVUn5Q0TckzwWKf+IAWaTVs7i2lg1LwIDAQABAoIBAGbVTkMoh3COkpzL" +
        "vXOAW+1n32waFUMZC5pRAYNgd5SNx0dBEohwYHF4eZ4N809HqLNbE3vlg23iBmcf" +
        "R/bSiRJXyaNwNn8B5ZiGJ3yS/c4fJSOyQGZf5bfBYbYmr74A/Mr4d6VjQLuCHlf3" +
        "hnDdjKQPQ/qkEdrRClS7ofeZ1reZnQmN8vC0VDg9Xfo1u5ipBid/oSMezva89/XI" +
        "vYBhaifwurzO2B/jdX0616dPXIwMHKJz0KAuI93lQSunT1VamJlDDDRbjy9MpJ+m" +
        "KYXYrBvH2oUhX3hdPibYoVvC4YJ/0AArH/WxQEjWf3DvHADCiOcfRP/9KmQLyNeA" +
        "kE1OBhECgYEAy/fg4blC5j9yvxgkNt9rKE4cSJxRF9MYltv1YRxPprCBL7Ujy2mT" +
        "dt62CIZ5eQcsdglasumRyCzvRm7KGGnq6nbj1qqM1NiNUf0B7LkKqMNgft+t8GDZ" +
        "nmRGLO1N1+3ryx54K2UXzsSJMqpPx3Xk2OlMs7lEo1CxRLwsj/3lwO0CgYEA4zKp" +
        "21L/b8OmiAV26/TyRLQH5yaIPxj1gp33yYhdIdYw8LSKa1DeBDrpvUsIfydwg8vq" +
        "KDGTtMcj+66+/KQJVrIVCBfov1gZGTfrvi+ZOH+fuTQjIjt21oKJN3HJp06VTNuE" +
        "Aqy6e1oMqRj6hKW1pwtJa5rBj4N9QpbiPvZeNwsCgYBwI0zI7PDh45ozg5NNc9HX" +
        "K/KS3RYccht/vViP1l/YNgwgbYdLazT+0nzj/QLFznLcRlJOMOnMUyAN6hVEDN9x" +
        "noGu4L9iudggbmhjfGxm0lu4BUP891JNqbWHN8RnZdAQMSVCPmczb4w6hbYs5B04" +
        "QDPCT6Zz34ZGUupxE9cAFQKBgBNJCqgGXI5lIi6roBOZYDM6wOz7FVedy8FBnXXq" +
        "8FTd9JuKB7HiVGYxhgO+acM5WtClL4Qn6SzQNpq3k5ioZ+6HAUgFl4kedkrwtz8W" +
        "lT/5fVpgNZdDSuwcen9NVluwTPYQMB12AgJUe3yjFSjLVCcQyzOqGEIRXEGZwyyr" +
        "BwZPAoGARaY049C58ES3/BgrtX6s8WZiegXqi7m6v5IXexnOCqljIFZNZLSUSuzg" +
        "dUcW8EUXXZTEI7/x3Vo+ohfFOMReWIzHASQqQG3i0M0/hdUvlzjvpvSem5aK/1Fa" +
        "yXKIwn8smd4l60Eo/OJz/g9TN8b0hsWUziptECMlJy6vm5UhfRU=";



        internal static RSA GetCertificate()
        {
            var privateKeyBytes = Convert.FromBase64String(PrivateCertificate.Key);
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
            return rsa;
        }

    }
}
