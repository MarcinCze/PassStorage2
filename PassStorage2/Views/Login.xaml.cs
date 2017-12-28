using PassStorage2.Base;
using PassStorage2.Controller.Interfaces;
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

        public Login(IController cntr)
        {
            InitializeComponent();
            Logger.Instance.Debug("Creating Login user control");
            gridWrongPass.Visibility = Visibility.Hidden;

            if (cntr is null)
            {
                Logger.Instance.Warning("Login :: controller is empty");
            }

            controller = cntr;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.FunctionStart();
            bool result = controller.SetPasswords(passPrimary.Password, passSecondary.Password);

            if (!result)
            {
                Logger.Instance.Warning("SetPass result is failure. Showing message");
                gridWrongPass.Visibility = Visibility.Visible;
            }
            else
            {
                Logger.Instance.Debug("SetPass ok. Switching to DASHBOARD");
                Switcher.Switch(new Dashboard(controller));
            }
        }

        private void menuMinimize_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.FunctionStart();
            Switcher.PageSwitcher.WindowState = WindowState.Minimized;
        }

        private void menuClose_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.FunctionStart();
            Application.Current.Shutdown();
        }

        private void menuAbout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Logger.Instance.Debug($"Key [{e.Key} pressed]");
                btnLogin_Click(null, null);
            }
        }
    }
}
