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

        public void FunctionStart([CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Console.WriteLine($"Function [{name}] starts");
        }

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
