using PassStorage3.DataCryptoLayer.Interfaces;
using PassStorage3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassStorage3.DataCryptoLayer
{
    public class Decoder : IDecoder
    {
        public async Task<Password> DecodeAsync(Password password) => await Task.FromResult(password);

        public async Task<Password> DecodeAsync(Password password, string[] propertiesToDecode) => await Task.FromResult(password);

        public async Task<IEnumerable<Password>> DecodeBatchAsync(IEnumerable<Password> passwords) => await Task.FromResult(passwords);

        public async Task<IEnumerable<Password>> DecodeBatchAsync(IEnumerable<Password> passwords, string[] propertiesToDecode) => await Task.FromResult(passwords);
    }
}
