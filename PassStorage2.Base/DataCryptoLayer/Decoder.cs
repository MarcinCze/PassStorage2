using PassStorage2.Models;
using System.Collections.Generic;

namespace PassStorage2.Base.DataCryptoLayer
{
    public class Decoder : Interfaces.IDecodeData
    {
        public IEnumerable<Password> Decode(IEnumerable<Password> passwords)
        {
            return passwords;
        }
    }
}
