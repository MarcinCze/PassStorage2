using System;
using System.Runtime.CompilerServices;

namespace PassStorage2.Logger.Interfaces
{
    public interface ILogger
    {
        void FunctionStart([CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0);
        void FunctionEnd([CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0);
        void Debug(string message, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0);
        void Debug(string message, object item, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0);
        void Warning(string message, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0);
        void Error(Exception ex, [CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0);
    }
}
