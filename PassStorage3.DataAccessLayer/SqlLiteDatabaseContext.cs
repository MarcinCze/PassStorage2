using Microsoft.EntityFrameworkCore;
using PassStorage3.Models;

namespace PassStorage3.DataAccessLayer
{
    public class SqlLiteDatabaseContext : DbContext
    {
        public DbSet<Entities.DbPassword> Passwords { get; set; }

        public const string DatabaseName = "PassStorage3_DB.db";

        public SqlLiteDatabaseContext(DbContextOptions<SqlLiteDatabaseContext> options) : base(options)
        {
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DatabaseName}");
    }
}
