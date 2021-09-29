using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PassStorage3.Controllers;
using PassStorage3.Controllers.Interfaces;
using PassStorage3.DataCryptoLayer;
using PassStorage3.DataCryptoLayer.Interfaces;
using PassStorage3.DataAccessLayer.Interfaces;
using PassStorage3.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace PassStorage3
{
    public static class DependencyRegister
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogging(configure => configure.AddConsole())
                .AddDbContext<SqlLiteDatabaseContext>(options =>
                {
                    options.UseSqlite($"Data Source = {DataAccessLayer.SqlLiteDatabaseContext.DatabaseName}");
                })
                .AddControllers()
                .AddCrypto()
                .AddStorage()
                .AddViews();
        }

        private static IServiceCollection AddControllers(this IServiceCollection services) =>
            services.AddSingleton<IBackupController, BackupController>()
                    .AddSingleton<IEntryProtectionController, EntryProtectionController>()
                    .AddSingleton<ICrudController, CrudController>()
                    .AddSingleton<IDetailedGetController, DetailedGetController>()
                    .AddSingleton<IViewCountController, ViewCountController>();

        private static IServiceCollection AddCrypto(this IServiceCollection services) =>
            services.AddSingleton<ISecretsVault, SecretsVault>();

        private static IServiceCollection AddStorage(this IServiceCollection services) =>
            services.AddSingleton<IStorageHandler, SqlLiteStorageHandler>();

        private static IServiceCollection AddViews(this IServiceCollection services) =>
            services.AddSingleton<MainWindow>()
                    .AddSingleton<Views.DashboardControl>()
                    .AddSingleton<Views.LoginControl>();
    }
}
