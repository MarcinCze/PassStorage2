namespace PassStorage2.ConfigurationProvider
{
    internal class ConfigurationModel
    {
        public string AppLang { get; set; }
        public int ExpirationDays { get; set; }
        public string FH { get; set; }
        public string SH { get; set; }
        public bool LogFunctionStart { get; set; }
        public bool LogFunctionEnd { get; set; }
    }
}
