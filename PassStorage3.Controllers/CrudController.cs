using PassStorage3.Controllers.Interfaces;
using PassStorage3.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassStorage3.Controllers
{
    public class CrudController : BaseController, ICrudController
    {
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Password>> GetAllAsync()
        {
            throw new NotImplementedException();
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
