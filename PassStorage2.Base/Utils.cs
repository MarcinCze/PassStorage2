using System;
using System.Diagnostics;
using System.Reflection;

namespace PassStorage2.Base
{
    public static class Utils
    {
        public static string GetComputerName()
        {
            try
            {
                return Environment.MachineName;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetUserName()
        {
            try
            {
                return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetVersion()
        {
            try
            {
                return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
