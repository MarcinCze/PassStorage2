using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Runtime.CompilerServices;

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
            return fileName.Substring((fileName.Length - 10));
        }

        async void FunctionStartAsync(string name, string filePath, int line)
        {
            Console.WriteLine($"Function [{name}] starts");
        }

        async void FunctionEndAsync(string name, string filePath, int line)
        {
            Console.WriteLine($"Function [{name}] ends");
        }

        async void DebugAsync(string message, string name, string filePath, int line)
        {
            //Console.WriteLine($"DEBUG :: {name} :: {message}");
            log.Debug($"{name} @ {ShortenFileName(filePath)}:{line} - {message}");
        }

        async void DebugAsync(string message, object obj, string name, string filePath, int line)
        {
            Console.WriteLine($"DEBUG :: {name} :: {message}");
        }

        async void WarningAsync(string message, string name, string filePath, int line)
        {
            Console.WriteLine($"WARNING :: {name} :: {message}");
        }

        async void Error(string message, string stackTrace, string name, string filePath, int line)
        {
            Console.WriteLine($"ERROR :: {name} :: {message}");
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

        public void Error(string message, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Task.Run(() => Error(message, null, name, filePath, line));
        }

        public void Error(Exception ex, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Task.Run(() => Error(ex.Message, ex.StackTrace, name, filePath, line));
        }

        #endregion Public functions
    }
}
