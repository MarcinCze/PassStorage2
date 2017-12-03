using System.Collections.Generic;

namespace PassStorage2.Base.DataCryptoLayer.Interfaces
{
    public interface IEncodeData
    {
        IEnumerable<Models.Password> Encode();
    }
}
