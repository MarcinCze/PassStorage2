using System;
using System.Collections.Generic;
using PassStorage2.Models;

namespace PassStorage2.Controller.Interfaces
{
    public interface IController
    {
        bool SetPasswords(string primary, string secondary);
        IEnumerable<Password> GetAll();
        IEnumerable<Password> GetAllExpired();
        IEnumerable<Password> GetAllExpired(IEnumerable<Password> passwords);
        IEnumerable<Password> GetMostUsed();
        IEnumerable<Password> GetMostUsed(IEnumerable<Password> passwords);
        Password Get(int id);
        Password Get(int id, IEnumerable<Password> passwords);
        void Save(Password pass, bool updatePassTime);
        void Delete(int id);
        void UpdateViewCount(int id, int counter);
        void IncrementViewCount(int id);
        void IncrementViewCount(int id, IEnumerable<Password> passwords);
        void Backup();
        void BackupDecoded();
    }
}
