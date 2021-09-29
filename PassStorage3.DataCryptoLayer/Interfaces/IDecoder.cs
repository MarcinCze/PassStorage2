using PassStorage3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassStorage3.DataCryptoLayer.Interfaces
{
    public interface IDecoder
    {
        Task<Password> DecodeAsync(Password password);
        Task<Password> DecodeAsync(Password password, string[] propertiesToDecode);

        Task<IEnumerable<Password>> DecodeBatchAsync(IEnumerable<Password> passwords);
        Task<IEnumerable<Password>> DecodeBatchAsync(IEnumerable<Password> passwords, string[] propertiesToDecode);
    }
}
