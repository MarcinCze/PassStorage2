using System.Collections.Generic;
using PassStorage2.Models;

namespace PassStorage2.Controller.Interfaces
{
    interface IController
    {
        IEnumerable<Password> GetList();
        Password Get(int id);
        void Save(int id);
        void SaveAll();
        void Delete(int id);
        void Backup();
        void BackupDecoded();
    }
}
