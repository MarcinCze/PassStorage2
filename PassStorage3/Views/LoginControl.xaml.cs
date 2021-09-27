using Microsoft.Extensions.Logging;
using PassStorage3.Controllers.Interfaces;
using PassStorage3.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PassStorage3.Views
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl, ISwitchable
    {
        private readonly ILogger logger;
        private readonly IServiceProvider serviceProvider;
        private readonly IEntryProtectionController entryController;

        public LoginControl(ILogger<LoginControl> logger, IServiceProvider serviceProvider, IEntryProtectionController entryController)
        {
            InitializeComponent();

            this.logger = logger;
            this.serviceProvider = serviceProvider;
            this.entryController = entryController;
        }

        public void UtilizeState(object state) => throw new NotImplementedException();

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await entryController.ValidateAsync(passPrimary.Password, passSecondary.Password);
                logger.LogInformation("Entry passwords are OK. Switching to Dashboard");
                Switcher.Switch(serviceProvider.GetService(typeof(DashboardControl)) as DashboardControl);
            }
            catch (AuthException)
            {
                logger.LogWarning("Entry validation failed. Showing message");
                gridWrongPass.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception during validation of entry params");
                MessageBox.Show("Entry validation failed. Please try again later.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            logger.LogInformation($"Key [{e.Key} pressed]");
            btnLogin_Click(null, null);
        }

        private void menuMinimize_Click(object sender, RoutedEventArgs e) => Switcher.PageSwitcher.WindowState = WindowState.Minimized;

        private void menuClose_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void menuAbout_Click(object sender, RoutedEventArgs e) =>
            MessageBox.Show("Not implemented.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);

        private void TranslateControls()
        {
            //btnLogin.Content = controller.Translate(btnLogin.Content.ToString());
            //HintAssist.SetHint(passPrimary, controller.Translate(HintAssist.GetHint(passPrimary).ToString()));
            //HintAssist.SetHint(passSecondary, controller.Translate(HintAssist.GetHint(passSecondary).ToString()));
            //txtWrongPassCard.Text = controller.Translate(txtWrongPassCard.Text);
            //menuMinimize.Content = controller.Translate(menuMinimize.Content.ToString());
            //menuClose.Content = controller.Translate(menuClose.Content.ToString());
            //menuAbout.Content = controller.Translate(menuAbout.Content.ToString());
        }
    }
}
