using PassStorage3.Controllers.Interfaces;
using PassStorage3.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassStorage3.Controllers
{
    public class DetailedGetController : BaseController, IDetailedGetController
    {
        public Task<IEnumerable<Password>> GetAllExpiredAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Password>> GetMostUsedAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Password>> GetSearchResultsAsync(string searchPhrase)
        {
            throw new NotImplementedException();
        }
    }
}
