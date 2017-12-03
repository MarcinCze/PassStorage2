using System.Collections.Generic;

namespace PassStorage2.Base.DataAccessLayer.Interfaces
{
    public interface ISaveData
    {
        void Save(IEnumerable<Models.Password> passwords);
    }
}
