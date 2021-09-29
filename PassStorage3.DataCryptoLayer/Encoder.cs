using PassStorage3.DataCryptoLayer.Interfaces;
using PassStorage3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassStorage3.DataCryptoLayer
{
    public class Encoder : IEncoder
    {
        public async Task<Password> EncodeAsync(Password password) => await Task.FromResult(password);

        public async Task<IEnumerable<Password>> EncodeBatchAsync(IEnumerable<Password> passwords) => await Task.FromResult(passwords);
    }
}
