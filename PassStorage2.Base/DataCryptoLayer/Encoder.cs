using PassStorage2.Models;
using System.Collections.Generic;

namespace PassStorage2.Base.DataCryptoLayer
{
    public class Encoder : Interfaces.IEncodeData
    {
        public IEnumerable<Password> Encode(IEnumerable<Password> passwords)
        {
            return passwords;
        }
    }
}
