using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PassStorage3.Controllers;
using PassStorage3.Controllers.Interfaces;

namespace PassStorage3
{
    public static class DependencyRegister
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogging(configure => configure.AddConsole())
                .AddControllers()
                .AddViews();

            //services.AddScoped<ILogger, Logger.Logger>();

            //services.AddSingleton<IController, SqliteController>();
            //services.AddSingleton<IDecodeData, Decoder>();
            //services.AddSingleton<IEncodeData, Encoder>();
            //services.AddSingleton<IEntryProtection, EntryProtection>();
            //services.AddSingleton<IStorageHandler, DbHandlerAdditionalInfo>();
            //services.AddSingleton<ITranslationProvider, TranslationProvider>();
            //services.AddSingleton<IConfigurationProvider, ConfigurationProvider.ConfigurationProvider>();

            //services.AddSingleton<MainWindow>();
            //services.AddSingleton<Views.LoginControl>();

            //services.AddTransient<Views.Dashboard>();
            //services.AddTransient<Views.Modify>();
        }

        private static IServiceCollection AddControllers(this IServiceCollection services) =>
            services.AddSingleton<IBackupController, BackupController>()
                    .AddSingleton<IEntryProtectionController, EntryProtectionController>()
                    .AddSingleton<ICrudController, CrudController>()
                    .AddSingleton<IDetailedGetController, DetailedGetController>()
                    .AddSingleton<IViewCountController, ViewCountController>();

        private static IServiceCollection AddViews(this IServiceCollection services) =>
            services.AddSingleton<MainWindow>()
                    .AddSingleton<Views.DashboardControl>()
                    .AddSingleton<Views.LoginControl>();
    }
}
