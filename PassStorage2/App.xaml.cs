using Microsoft.Extensions.DependencyInjection;

using PassStorage2.Base.DataAccessLayer;
using PassStorage2.Base.DataAccessLayer.Interfaces;
using PassStorage2.Base.DataCryptoLayer;
using PassStorage2.Base.DataCryptoLayer.Interfaces;
using PassStorage2.ConfigurationProvider.Interfaces;
using PassStorage2.Controller;
using PassStorage2.Controller.Interfaces;
using PassStorage2.Logger.Interfaces;
using PassStorage2.Translations;
using PassStorage2.Translations.Interfaces;

using System.Windows;

namespace PassStorage2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddScoped<ILogger, Logger.Logger>();
            
            services.AddSingleton<IController, SqliteController>();
            services.AddSingleton<IDecodeData, Decoder>();
            services.AddSingleton<IEncodeData, Encoder>();
            services.AddSingleton<IEntryProtection, EntryProtection>();
            services.AddSingleton<IStorageHandler, DbHandlerAdditionalInfo>();
            services.AddSingleton<ITranslationProvider, TranslationProvider>();
            services.AddSingleton<IConfigurationProvider, ConfigurationProvider.ConfigurationProvider>();

            services.AddSingleton<MainWindow>();
            services.AddSingleton<Views.Login>();

            services.AddTransient<Views.Dashboard>();
            services.AddTransient<Views.Modify>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            serviceProvider.GetService<MainWindow>().Show();
        }
    }
}
