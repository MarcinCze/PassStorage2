using System.Collections.Generic;
using PassStorage2.Models;

namespace PassStorage2.Base.DataCryptoLayer.Interfaces
{
    public interface IEncodeData
    {
        IEnumerable<Password> Encode(IEnumerable<Password> passwords, string primaryKey, string secondaryKey);
    }
}
