using System.Collections.Generic;

namespace PassStorage2.Base.DataCryptoLayer.Interfaces
{
    public interface IDecodeData
    {
        IEnumerable<Models.Password> Decode();
    }
}
