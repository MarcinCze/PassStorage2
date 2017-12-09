using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Runtime.CompilerServices;

namespace PassStorage2.Base
{
    public sealed class Logger
    {
        public enum LogLevel { ALL, DEBUG, WARNING, ERROR }

        private static readonly Logger instance = new Logger();
        private LogLevel level = LogLevel.ERROR;

        private Logger() { }

        public static Logger Instance
        {
            get
            {
                return instance;
            }
        }

        #region Protected functions

        protected async void FunctionStartAsync(string name, string filePath, int line)
        {
            Console.WriteLine($"Function [{name}] starts");
        }

        protected async void FunctionEndAsync(string name, string filePath, int line)
        {
            Console.WriteLine($"Function [{name}] ends");
        }

        protected async void DebugAsync(string message, string name, string filePath, int line)
        {
            Console.WriteLine($"DEBUG :: {name} :: {message}");
        }

        protected async void WarningAsync(string message, string name, string filePath, int line)
        {
            Console.WriteLine($"WARNING :: {name} :: {message}");
        }

        protected async void Error(string message, string stackTrace, string name, string filePath, int line)
        {
            Console.WriteLine($"ERROR :: {name} :: {message}");
        }

        #endregion Protected functions

        #region Public functions

        public void SetLogLevel(LogLevel level)
        {
            this.Warning($"Setting log level to {level}");
            this.level = level;
        }

        /// <summary>
        /// Logging start of the function
        /// </summary>
        /// <param name="name">Name of the function</param>
        /// <param name="filePath">Path to the file</param>
        /// <param name="line">Line of code</param>
        public void FunctionStart([CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            if (level == LogLevel.ALL)
            {
                Task.Run(() => FunctionStartAsync(name, filePath, line));
            }   
        }

        /// <summary>
        /// Logging end of the function
        /// </summary>
        /// <param name="name">Name of the function</param>
        /// <param name="filePath">Path to the file</param>
        /// <param name="line">Line of code</param>
        public void FunctionEnd([CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            if (level == LogLevel.ALL)
            {
                Task.Run(() => FunctionEndAsync(name, filePath, line));
            }
        }

        public void Debug(string message, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            if (level == LogLevel.DEBUG || level == LogLevel.ALL)
            {
                Task.Run(() => DebugAsync(message, name, filePath, line));
            }
        }

        public void Warning(string message, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            if (level != LogLevel.ERROR)
            {
                Task.Run(() => WarningAsync(message, name, filePath, line));
            }
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
