using System;
using System.Diagnostics;
using System.Reflection;

namespace PassStorage2.Base
{
    public static class Utils
    {
        public static string GetComputerName() => Environment.MachineName;

        public static string GetUserName() => System.Security.Principal.WindowsIdentity.GetCurrent().Name;

        public static string GetVersion() => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
        
        public static string GetVersionShort()
        {
            var fileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            return $"{fileVersion.FileMajorPart}.{fileVersion.FileMinorPart}.{fileVersion.FileBuildPart}";
        }
    }
}
