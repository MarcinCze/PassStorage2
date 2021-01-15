using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PassStorage2.Base.DataAccessLayer;
using PassStorage2.Models;

namespace PassStorage2.Base
{
    public class BackupHandler
    {
        private const string BackupPath = "Backups";

        public static void Backup()
        {
            CreateIfNotExist();

            int idx = 1;
            string fileName = $"{DbHandler.FileName}_{DateTime.Now:yyyy-MM-dd}";

            while (true)
            {
                if (!File.Exists($"{BackupPath}\\{fileName}"))
                {
                    File.Copy(DbHandler.FileName, $"{BackupPath}\\{fileName}");
                    break;
                }

                fileName = $"{DbHandler.FileName}_{DateTime.Now:yyyy-MM-dd}" + $"_{idx}";
                idx++;
            }
        }

        public static void BackupDecoded(IEnumerable<Password> passwords)
        {
            string content = JsonConvert.SerializeObject(passwords);

            CreateIfNotExist();

            int idx = 1;
            string fileName = $"{DbHandler.FileName}_DECODED_{DateTime.Now:yyyy-MM-dd}.json";

            while (true)
            {
                if (!File.Exists($"{BackupPath}\\{fileName}"))
                {
                    using (var writer = new StreamWriter($"{BackupPath}\\{fileName}"))
                    {
                        writer.Write(content);
                    }
                    break;
                }

                fileName = $"{DbHandler.FileName}_{DateTime.Now:yyyy-MM-dd}" + $"_{idx}";
                idx++;
            }
        }

        private static void CreateIfNotExist()
        {
            if (!Directory.Exists(BackupPath))
                Directory.CreateDirectory(BackupPath);
        }
    }
}