using PassStorage3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassStorage3.DataCryptoLayer.Interfaces
{
    public interface IEncoder
    {
        Task<Password> EncodeAsync(Password password);

        Task<IEnumerable<Password>> EncodeBatchAsync(IEnumerable<Password> passwords);
    }
}
