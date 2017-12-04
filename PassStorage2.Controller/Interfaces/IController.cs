using System.Collections.Generic;
using PassStorage2.Models;

namespace PassStorage2.Controller.Interfaces
{
    public interface IController
    {
        string PasswordFirst { get; set; }
        string PasswordSecond { get; set; }

        IEnumerable<Password> GetAll();
        Password Get(int id);
        void Save(Password pass);
        void Delete(int id);
        void Backup();
        void BackupDecoded();
    }
}
