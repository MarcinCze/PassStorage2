using MaterialDesignThemes.Wpf;

using PassStorage2.Base;
using PassStorage2.Controller.Interfaces;
using PassStorage2.Logger.Interfaces;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PassStorage2.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl, ISwitchable
    {
        private readonly IController controller;
        private readonly ILogger logger;
        private readonly IServiceProvider serviceProvider;

        public Login(IController controller, ILogger logger, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            this.controller = controller;
            this.logger = logger;
            this.serviceProvider = serviceProvider;

            logger.Debug("Creating Login user control");
            gridWrongPass.Visibility = Visibility.Hidden;

            lbAppName.Content = $"PassStorage {Utils.GetVersionShort()}";
            lbBuild.Text = Utils.GetBuildName();

            TranslateControls();
        }

        public void UtilizeState(object state) => throw new NotImplementedException();

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            logger.FunctionStart();
            bool result = controller.SetPasswords(passPrimary.Password, passSecondary.Password);

            if (!result)
            {
                logger.Warning("SetPass result is failure. Showing message");
                gridWrongPass.Visibility = Visibility.Visible;
            }
            else
            {
                logger.Debug("SetPass ok. Switching to DASHBOARD");
                var dashboard = (Dashboard)serviceProvider.GetService(typeof(Dashboard));
                Switcher.Switch(dashboard);
            }
        }

        private void menuMinimize_Click(object sender, RoutedEventArgs e)
        {
            logger.FunctionStart();
            Switcher.PageSwitcher.WindowState = WindowState.Minimized;
        }

        private void menuClose_Click(object sender, RoutedEventArgs e)
        {
            logger.FunctionStart();
            Application.Current.Shutdown();
        }

        private void menuAbout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                logger.Debug($"Key [{e.Key} pressed]");
                btnLogin_Click(null, null);
            }
        }

        private void TranslateControls()
        {
            btnLogin.Content = controller.Translate(btnLogin.Content.ToString());
            HintAssist.SetHint(passPrimary, controller.Translate(HintAssist.GetHint(passPrimary).ToString()));
            HintAssist.SetHint(passSecondary, controller.Translate(HintAssist.GetHint(passSecondary).ToString()));
            txtWrongPassCard.Text = controller.Translate(txtWrongPassCard.Text);
            menuMinimize.Content = controller.Translate(menuMinimize.Content.ToString());
            menuClose.Content = controller.Translate(menuClose.Content.ToString());
            menuAbout.Content = controller.Translate(menuAbout.Content.ToString());
        }
    }
}
