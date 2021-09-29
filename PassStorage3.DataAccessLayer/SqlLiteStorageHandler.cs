using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PassStorage3.DataAccessLayer.Interfaces;
using PassStorage3.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PassStorage3.DataAccessLayer
{
    public class SqlLiteStorageHandler : IStorageHandler
    {
        private readonly ILogger logger;
        private readonly SqlLiteDatabaseContext databaseContext;

        private List<Entities.DbPassword> passwords;

        public SqlLiteStorageHandler(ILogger<SqlLiteStorageHandler> logger, SqlLiteDatabaseContext databaseContext)
        {
            this.logger = logger;
            this.databaseContext = databaseContext;
        }

        public async Task<IEnumerable<Entities.DbPassword>> GetAllAsync()
        {
            if (passwords == null)
                passwords = await databaseContext.Passwords.ToListAsync();

            return passwords;
        }

        public async Task<Entities.DbPassword> GetAsync(int id)
        {
            if (passwords == null || !passwords.Any(x => x.Id == id))
                passwords = await databaseContext.Passwords.ToListAsync();

            return passwords.FirstOrDefault(x => x.Id == id);
        }

        public Task SaveAsync(Entities.DbPassword pass)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
