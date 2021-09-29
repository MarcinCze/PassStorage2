using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PassStorage3.DataAccessLayer.Interfaces;
using PassStorage3.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassStorage3.DataAccessLayer
{
    public class SqlLiteStorageHandler : IStorageHandler
    {
        private readonly ILogger logger;
        private readonly SqlLiteDatabaseContext databaseContext;
        private List<Password> passwords;

        public SqlLiteStorageHandler(ILogger<SqlLiteStorageHandler> logger, SqlLiteDatabaseContext databaseContext)
        {
            this.logger = logger;
            this.databaseContext = databaseContext;
        }

        public async Task<IEnumerable<Password>> GetAllAsync()
        {
            if (passwords == null)
                passwords = await databaseContext.Passwords.ToListAsync();

            return passwords;
        }

        public Task<Password> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Password pass)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
