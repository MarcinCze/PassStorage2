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

            string content = string.Empty;

            if (!File.Exists(FileName))
            {
                Logger.Instance.Warning($"File {FileName} doesn't exist!");
                Logger.Instance.Debug($"Returning empty list");
                return new List<Password>();
            }

            Logger.Instance.Debug($"File {FileName} exist. Reading data...");

            using (var reader = new StreamReader(FileName))
            {
                content = reader.ReadToEnd();
            }

            return new List<Password>();
        }

        public void Save(IEnumerable<Password> passwords)
        {
            throw new NotImplementedException();
        }
    }
}
