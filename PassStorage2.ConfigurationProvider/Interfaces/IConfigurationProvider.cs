namespace PassStorage2.ConfigurationProvider.Interfaces
{
    public interface IConfigurationProvider
    {
        string ApplicationLanguage { get; }
        int ExpirationDays { get; }
        string PrimaryHash { get; }
        string SecondaryHash { get; }
        bool LogFunctionStart { get; }
        bool LogFunctionEnd { get; }
    }
}
