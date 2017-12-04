using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PassStorage2.Base.DataAccessLayer.Interfaces;
using PassStorage2.Models;

namespace PassStorage2.Base.DataAccessLayer
{
    public class FileHandler : IStorage
    {
        protected const string FileName = "PassStorage2.Storage.dat";

        public IEnumerable<Password> Read()
        {
            Logger.Instance.FunctionStart();

            try
            {
                if (!File.Exists(FileName))
                {
                    Logger.Instance.Warning($"File {FileName} doesn't exist!");
                    Logger.Instance.Debug($"Saving empty list");
                    Save(new List<Password>());
                }

                Logger.Instance.Debug($"File {FileName} exist. Reading data...");

                string content = string.Empty;
                using (var reader = new StreamReader(FileName))
                {
                    content = reader.ReadToEnd();
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Save(IEnumerable<Password> passwords)
        {
            try
            {
                var content = new Root
                {
                    ComputerName = Utils.GetComputerName(),
                    SaveTime = DateTime.Now.ToString("yyyy-MM-dd H:m:s"),
                    User = Utils.GetUserName(),
                    Version = Utils.GetVersion(),
                    Passwords = passwords.ToList()
                };
            }
            catch (Exception)
            {

            }
        }
    }
}
