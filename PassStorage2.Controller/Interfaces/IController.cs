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
        Password Get(Guid id);
        void Save(Password pass);
        void Delete(Guid id);
        void UpdateViewCount(Guid id, int counter);
        void IncrementViewCount(Guid id);
        void Backup();
        void BackupDecoded();
    }
}
