using PassStorage2.Models;

using System.Collections.Generic;

namespace PassStorage2.Controller.Interfaces
{
    public interface IController
    {
        IEnumerable<Password> GetAll();
        IEnumerable<Password> GetAllExpired(IEnumerable<Password> passwords);
        IEnumerable<Password> GetMostUsed(IEnumerable<Password> passwords);
        IEnumerable<Password> GetBySearchWord(string searchWord, IEnumerable<Password> passwords);

        Password Get(int id);
        void Save(Password pass, bool updatePassTime);
        void Delete(int id);

        void IncrementViewCount(int id);

        void Backup();
        void BackupDecoded();

        string Translate(string key);
        bool SetPasswords(string primary, string secondary);
    }
}
