using PassStorage2.Base;
using PassStorage2.Controller.Interfaces;
using PassStorage2.Models;
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

namespace PassStorage2.Views
{
    /// <summary>
    /// Interaction logic for Modify.xaml
    /// </summary>
    public partial class Modify : UserControl
    {
        IController controller;
        Password password;
        Counters counters;

        public Modify(IController cntr, Counters c, int? passwordId = null)
        {
            InitializeComponent();
            Logger.Instance.Debug("Creating Modify user control");

            this.counters = c;

            if (cntr is null)
                Logger.Instance.Warning("Modify :: controller is empty");

            controller = cntr;

            if (passwordId.HasValue)
            {
                Logger.Instance.Debug("Entering EDIT mode");
                password = controller.Get(passwordId.Value);
            }
            else
            {
                Logger.Instance.Debug("Entering INSERT mode");
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (counters.NeedRefresh)
            {
                var passwords = controller.GetAll().ToList();
                counters.All = passwords.Count;
                counters.MostUsed = controller.GetMostUsed(passwords).Count();
                counters.Expired = passwords.Count(x => x.IsExpired);
            }

            ((btnAll.Content as StackPanel).Children[2] as TextBlock).Text += $" ({counters.All})";
            ((btnMostlyUsed.Content as StackPanel).Children[2] as TextBlock).Text += $" ({counters.MostUsed})";
            ((btnExpiryWarning.Content as StackPanel).Children[2] as TextBlock).Text += $" ({counters.Expired})";

            if (password != null)
            {
                tbTitle.Text = password.Title;
                tbLogin.Text = password.Login;
                tbPassword.Text = password.Pass;
            }
            else
            {
                password = new Password();
            }
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Dashboard(controller, Dashboard.MenuType.ALL));
        }

        private void btnMostlyUsed_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Dashboard(controller, Dashboard.MenuType.MOST));
        }

        private void btnExpiryWarning_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Dashboard(controller, Dashboard.MenuType.EXPIRY));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool updTime = !(password.Pass.Equals(tbPassword.Text));

            password.Title = tbTitle.Text;
            password.Login = tbLogin.Text;
            password.Pass = tbPassword.Text;
            
            controller.Save(password, updTime);
            Switcher.Switch(new Dashboard(controller, Dashboard.MenuType.ALL));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Dashboard(controller, Dashboard.MenuType.ALL));
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRandomPass_Click(object sender, RoutedEventArgs e)
        {
            tbPassword.Text = RandomPassword.Generate((int)sliderRandomPassLength.Value);
        }
    }
}
