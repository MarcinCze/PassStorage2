using PassStorage3.Controllers.Interfaces;
using PassStorage3.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PassStorage3.DataAccessLayer.Interfaces;
using Microsoft.Extensions.Logging;
using PassStorage3.DataCryptoLayer.Interfaces;
using System.Linq;

namespace PassStorage3.Controllers
{
    public class CrudController : BaseController, ICrudController
    {
        private readonly ILogger logger;
        private readonly IStorageHandler storageHandler;
        private readonly IDecoder decoder;

        public CrudController(
            ILogger<CrudController> logger, 
            IStorageHandler storageHandler,
            IDecoder decoder)
        {
            this.logger = logger;
            this.decoder = decoder;
            this.storageHandler = storageHandler;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Password>> GetAllAsync()
        {
            var passwords = await storageHandler.GetAllAsync();

            var decodedPasswords = await decoder.DecodeBatchAsync(
                passwords.Select(x => new Password()
                {
                    AdditionalInfo = x.AdditionalInfo,
                    Id = x.Id,
                    Login = x.Login,
                    Pass = x.Pass,
                    PassChangeTime = x.PassChangeTime,
                    SaveTime = x.SaveTime,
                    Title = x.Title,
                    Uid = x.Uid,
                    ViewCount = x.ViewCount,
                    EncodedPropertyStatuses = new Dictionary<string, bool>
                    {
                        { nameof(x.Title), true },
                        { nameof(x.Login), true },
                        { nameof(x.Pass), true },
                        { nameof(x.AdditionalInfo), true },
                    }
                }));

            return decodedPasswords;
        }

        public Task<Password> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Password pass)
        {
            throw new NotImplementedException();
        }
    }
}
