using Newtonsoft.Json;

using PassStorage2.Translations.Interfaces;

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PassStorage2.Translations
{
    public class TranslationProvider : ITranslationProvider
    {
        protected Language SelectedLang{ get; set; }
        internal List<TranslationModel> Dictionary { get; set; }

        public TranslationProvider()
        {
            SetLanguage(Language.EN);
        }

        public void SetLanguage(Language language)
        {
            try
            {
                SelectedLang = language;
                Task.Run(LoadTranslationsAsync).Wait();
            }
            catch (Exception ex)
            {
                Dictionary = new List<TranslationModel>();
            }
        }

        public string Translate(string key)
        {
            if (!Dictionary.Any(x => x.Name.Equals(key)))
                return key;

            return Dictionary.FirstOrDefault(x => x.Name.Equals(key)).Value;
        }

        protected async Task LoadTranslationsAsync()
        {
            string fileName = Path.Combine("Translations", $"{SelectedLang}.json");

            if (!File.Exists(fileName))
                throw new Exception("Translation file doesn't exist");

            string content = null;
            using (var reader = new StreamReader(fileName))
            {
                content = await reader.ReadToEndAsync();
            }

            if (string.IsNullOrEmpty(content))
                throw new Exception("Cannot load translations");

            Dictionary = JsonConvert.DeserializeObject<List<TranslationModel>>(content);
        }
    }
}
