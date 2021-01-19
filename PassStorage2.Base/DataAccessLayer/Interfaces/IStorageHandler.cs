using PassStorage2.Models;

using System.Collections.Generic;

namespace PassStorage2.Base.DataAccessLayer.Interfaces
{
    public interface IStorageHandler
    {
        Password Get(int id);
        IEnumerable<Password> GetAll();
        bool Save(Password pass, bool isPassUpdate);
        void Delete(int id);
        bool IncrementViewCount(int id);
    }
}
