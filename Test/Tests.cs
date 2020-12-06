using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Test
{
    public class Tests
    {
        private UnitTestConfig _SecretConfig;

        [SetUp]
        public void Setup()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddUserSecrets<Tests>()
                .AddEnvironmentVariables()
                .Build();

            //access directly section:key
            string key = configuration["UnitTestConfig:ApiKeyPath"];
            //access by section
            key = configuration.GetSection("UnitTestConfig")["ApiKeyPath"];
            //bind it
            _SecretConfig = configuration.GetSection("UnitTestConfig").Get<UnitTestConfig>();
        }

        [Test]
        public void Test_Secrets_Are_Loaded()
        {
            Assert.AreEqual(_SecretConfig.ApiKeyPath, @"C:\secrets\key.json");
            Assert.AreEqual(_SecretConfig.User, "info@example.com");
        }
    }
}