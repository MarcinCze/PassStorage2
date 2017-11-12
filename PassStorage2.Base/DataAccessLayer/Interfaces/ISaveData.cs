using System.Collections.Generic;

namespace PassStorage2.Base.DataAccessLayer.Interfaces
{
    interface ISaveData
    {
        void Save(IEnumerable<Models.Password> passwords);
    }
}
