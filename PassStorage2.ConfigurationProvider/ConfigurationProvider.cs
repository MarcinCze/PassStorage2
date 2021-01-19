using Newtonsoft.Json;

using PassStorage2.ConfigurationProvider.Interfaces;

using System;
using System.IO;
using System.Threading.Tasks;

namespace PassStorage2.ConfigurationProvider
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        protected const int defaultExpirationDays = 365;
        protected const string fileName = "PassStorage2.Configuration.json";
        internal ConfigurationModel configModel;

        public ConfigurationProvider()
        {
            Task.Run(LoadAsync).Wait();
        }

        public int ExpirationDays => configModel?.ExpirationDays ?? defaultExpirationDays;
        public string PrimaryHash => configModel?.FH;
        public string SecondaryHash => configModel?.SH;
        public string ApplicationLanguage => configModel?.AppLang;
        public bool LogFunctionStart => configModel?.LogFunctionStart ?? false;
        public bool LogFunctionEnd => configModel?.LogFunctionEnd ?? false;

        protected async Task LoadAsync()
        {
            if (!File.Exists(fileName))
                throw new Exception("Config file doesn't exist!");

            string content;
            using (var reader = new StreamReader(fileName))
            {
                content = await reader.ReadToEndAsync();
            }

            if (string.IsNullOrEmpty(content))
                throw new Exception("Configuration file cannot be read");

            configModel = JsonConvert.DeserializeObject<ConfigurationModel>(content);
        }
    }
}
