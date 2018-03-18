using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PassStorage2.Base
{
    public sealed class Logger
    {
        private readonly log4net.ILog log;

        private Logger()
        {
            FileInfo configFileInfo = new FileInfo("log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(configFileInfo);
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static Logger Instance { get; } = new Logger();

        #region Private functions

        string ShortenFileName(string fileName)
        {
            fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
            fileName = fileName.Substring(fileName.Substring(0, fileName.LastIndexOf("\\", StringComparison.Ordinal)).LastIndexOf("\\", StringComparison.Ordinal) + 1);
            return fileName;
        }

        async void FunctionStartAsync(string name, string filePath, int line)
        {
            log.Info($"{name} @ {ShortenFileName(filePath)}:{line} - Function [{name}] starts");
        }

        async void FunctionEndAsync(string name, string filePath, int line)
        {
            log.Info($"{name} @ {ShortenFileName(filePath)}:{line} - Function [{name}] ended");
        }

        async void DebugAsync(string message, string name, string filePath, int line)
        {
            log.Debug($"{name} @ {ShortenFileName(filePath)}:{line} - {message}");
        }

        async void DebugAsync(string message, object obj, string name, string filePath, int line)
        {
            string serializedObj;
            try
            {
                serializedObj = JsonConvert.SerializeObject(obj);
            }
            catch (Exception e)
            {
                serializedObj = "CANNOT SERIALIZE OBJECT - ERROR: " + e.Message;
            }

            log.Debug($"{name} @ {ShortenFileName(filePath)}:{line} - {message} - {serializedObj}");
        }

        async void WarningAsync(string message, string name, string filePath, int line)
        {
            log.Warn($"{name} @ {ShortenFileName(filePath)}:{line} - {message}");
        }

        async void ErrorAsync (Exception ex, string name, string filePath, int line)
        {
            log.Error($"{name} @ {ShortenFileName(filePath)}:{line} - {ex.Message}", ex);
        }

        #endregion Private functions

        #region Public functions

        /// <summary>
        /// Logging start of the function
        /// </summary>
        /// <param name="name">Name of the function</param>
        /// <param name="filePath">Path to the file</param>
        /// <param name="line">Line of code</param>
        public void FunctionStart([CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Task.Run(() => FunctionStartAsync(name, filePath, line));
        }

        /// <summary>
        /// Logging end of the function
        /// </summary>
        /// <param name="name">Name of the function</param>
        /// <param name="filePath">Path to the file</param>
        /// <param name="line">Line of code</param>
        public void FunctionEnd([CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Task.Run(() => FunctionEndAsync(name, filePath, line));
        }

        public void Debug(string message, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Task.Run(() => DebugAsync(message, name, filePath, line));
        }

        public void Debug(string message, object item, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Task.Run(() => DebugAsync(message, item, name, filePath, line));
        }

        public void Warning(string message, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Task.Run(() => WarningAsync(message, name, filePath, line));
        }

        public void Error(Exception ex, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Task.Run(() => ErrorAsync(ex, name, filePath, line));
        }

        #endregion Public functions
    }
}
