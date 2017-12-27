using System.Collections.Generic;
using PassStorage2.Models;

namespace PassStorage2.Base.DataCryptoLayer.Interfaces
{
    public interface IDecodeData
    {
        IEnumerable<Password> Decode(IEnumerable<Password> passwords, string primaryKey, string secondaryKey);
        Password Decode(Password password, string primaryKey, string secondaryKey);
    }
}
