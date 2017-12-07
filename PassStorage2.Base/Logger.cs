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
        private static readonly Logger instance = new Logger();

        private Logger() { }

        public static Logger Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Logging start of the function
        /// </summary>
        /// <param name="name">Name of the function</param>
        /// <param name="filePath">Path to the file</param>
        /// <param name="line">Line of code</param>
        public void FunctionStart([CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Console.WriteLine($"Function [{name}] starts");
        }

        /// <summary>
        /// Logging end of the function
        /// </summary>
        /// <param name="name">Name of the function</param>
        /// <param name="filePath">Path to the file</param>
        /// <param name="line">Line of code</param>
        public void FunctionEnd([CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Console.WriteLine($"Function [{name}] ends");
        }

        public void Debug(string message, 
            [CallerMemberName] string name = "", 
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int line = 0)
        {
            Console.WriteLine($"DEBUG :: {name} :: {message}");
        }

        public void Warning(string message,
            [CallerMemberName] string name = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int line = 0)
        {
            Console.WriteLine($"WARNING :: {name} :: {message}");
        }

        public void Error(string message,
            [CallerMemberName] string name = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int line = 0)
        {
            Console.WriteLine($"ERROR :: {name} :: {message}");
        }

        public void Error(Exception ex,
            [CallerMemberName] string name = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int line = 0)
        {
            Console.WriteLine($"ERROR :: {name} :: {ex.Message}");
        }
    }
}
