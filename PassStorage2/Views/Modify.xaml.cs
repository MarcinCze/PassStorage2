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

        public Modify(IController cntr, Guid? passwordId = null)
        {
            InitializeComponent();
            Logger.Instance.Debug("Creating Modify user control");

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
            var passwords = controller.GetAll().ToList();

            ((btnAll.Content as StackPanel).Children[2] as TextBlock).Text += $" ({passwords.Count})";
            ((btnMostlyUsed.Content as StackPanel).Children[2] as TextBlock).Text += $" ({passwords.Count(x => x.ViewCount > 0)})";
            ((btnExpiryWarning.Content as StackPanel).Children[2] as TextBlock).Text += $" ({passwords.Count(x => x.IsExpired)})";

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
            password.Title = tbTitle.Text;
            password.Login = tbLogin.Text;
            password.Pass = tbPassword.Text;
            
            controller.Save(password);
            Switcher.Switch(new Dashboard(controller, Dashboard.MenuType.ALL));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Dashboard(controller, Dashboard.MenuType.ALL));
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

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
