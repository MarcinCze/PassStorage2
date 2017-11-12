using System.Collections.Generic;

namespace PassStorage2.Base.DataAccessLayer.Interfaces
{
    interface IReadData
    {
        IEnumerable<Models.Password> Read();
    }
}
