using PassStorage3.Controllers.Interfaces;
using PassStorage3.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PassStorage3.DataAccessLayer.Interfaces;
using Microsoft.Extensions.Logging;

namespace PassStorage3.Controllers
{
    public class CrudController : BaseController, ICrudController
    {
        private readonly ILogger logger;
        private readonly IStorageHandler storageHandler;

        public CrudController(ILogger<CrudController> logger, IStorageHandler storageHandler)
        {
            this.logger = logger;
            this.storageHandler = storageHandler;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Password>> GetAllAsync()
        {
            return await storageHandler.GetAllAsync();
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
