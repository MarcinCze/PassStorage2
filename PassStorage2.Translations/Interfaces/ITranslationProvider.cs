namespace PassStorage2.Translations.Interfaces
{
    public interface ITranslationProvider
    {
        void SetLanguage(Language language);
        string Translate(string key);
    }
}
