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
using System.Xaml;
using PassStorage2.Controller.Interfaces;
using PassStorage2.Base;

namespace PassStorage2.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl, ISwitchable
    {
        IController controller;

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
            passPrimary.Password = "SkodaFabia";
            passSecondary.Password = "martusia";

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
            Switcher.pageSwitcher.WindowState = WindowState.Minimized;
        }

        private void menuClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void menuAbout_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
