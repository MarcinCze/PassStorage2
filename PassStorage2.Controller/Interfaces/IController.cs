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
        Password Get(Guid id);
        Password Get(Guid id, IEnumerable<Password> passwords);
        void Save(Password pass);
        void Delete(Guid id);
        void UpdateViewCount(Guid id, int counter);
        void IncrementViewCount(Guid id);
        void IncrementViewCount(Guid id, IEnumerable<Password> passwords);
        void Backup();
        void BackupDecoded();
    }
}
