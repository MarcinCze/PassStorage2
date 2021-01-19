using Newtonsoft.Json;

using PassStorage2.ConfigurationProvider.Interfaces;
using PassStorage2.Logger.Interfaces;

using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace PassStorage2.Logger
{
    public class Logger : ILogger
    {
        private readonly log4net.ILog log;
        private readonly IConfigurationProvider configurationProvider;

        public Logger(IConfigurationProvider configurationProvider)
        {
            this.configurationProvider = configurationProvider;
            FileInfo configFileInfo = new FileInfo("log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(configFileInfo);
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void FunctionStart([CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            if (configurationProvider.LogFunctionStart)
                log.Info($"{name} @ {ShortenFileName(filePath)}:{line} - Function [{name}] starts");
        }

        public void FunctionEnd([CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            if (configurationProvider.LogFunctionEnd)
                log.Info($"{name} @ {ShortenFileName(filePath)}:{line} - Function [{name}] ended");
        }

        public void Debug(string message, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            log.Debug($"{name} @ {ShortenFileName(filePath)}:{line} - {message}");
        }

        public void Debug(string message, object item, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            string serializedObj;
            try
            {
                serializedObj = JsonConvert.SerializeObject(item);
            }
            catch (Exception e)
            {
                serializedObj = "CANNOT SERIALIZE OBJECT - ERROR: " + e.Message;
            }

            log.Debug($"{name} @ {ShortenFileName(filePath)}:{line} - {message} - {serializedObj}");
        }

        public void Warning(string message, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            log.Warn($"{name} @ {ShortenFileName(filePath)}:{line} - {message}");
        }

        public void Error(Exception ex, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            log.Error($"{name} @ {ShortenFileName(filePath)}:{line} - {ex.Message}", ex);
        }

        private string ShortenFileName(string fileName)
        {
            fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
            fileName = fileName.Substring(fileName.Substring(0, fileName.LastIndexOf("\\", StringComparison.Ordinal)).LastIndexOf("\\", StringComparison.Ordinal) + 1);
            return fileName;
        }
    }
}
