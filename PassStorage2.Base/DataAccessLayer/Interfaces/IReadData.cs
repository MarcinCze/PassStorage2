using System.Collections.Generic;

namespace PassStorage2.Base.DataAccessLayer.Interfaces
{
    public interface IReadData
    {
        IEnumerable<Models.Password> Read();
    }
}
