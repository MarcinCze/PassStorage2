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
using MaterialDesignThemes.Wpf;
using PassStorage2.Controller.Interfaces;
using PassStorage2.Base;
using PassStorage2.Models;

namespace PassStorage2.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        readonly IController controller;
        readonly MenuType menu;
        List<Password> passwords;

        public enum MenuType { ALL, MOST, EXPIRY }

        public Dashboard(IController cntr, MenuType menu = MenuType.ALL)
        {
            InitializeComponent();
            Logger.Instance.Debug("Creating Dashboard user control");

            if (cntr is null)
            {
                Logger.Instance.Warning("Dashboard :: controller is empty");
            }

            controller = cntr;
            this.menu = menu;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            passwords = controller.GetAll().ToList();

            ((btnAll.Content as StackPanel).Children[2] as TextBlock).Text += $" ({passwords.Count})";
            ((btnMostlyUsed.Content as StackPanel).Children[2] as TextBlock).Text += $" ({passwords.Count(x => x.ViewCount > 0)})";
            ((btnExpiryWarning.Content as StackPanel).Children[2] as TextBlock).Text += $" ({passwords.Count(x => x.IsExpired)})";

            switch (menu)
            {
                case MenuType.ALL:
                    {
                        btnAll_Click(sender, e);
                        break;
                    }
                case MenuType.MOST:
                    {
                        btnMostlyUsed_Click(sender, e);
                        break;
                    }
                case MenuType.EXPIRY:
                    {
                        btnExpiryWarning_Click(sender, e);
                        break;
                    }
            }
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            passwords = controller.GetAll().ToList();
            listViewPasswords.ItemsSource = null;
            listViewPasswords.ItemsSource = passwords;
        }

        private void btnMostlyUsed_Click(object sender, RoutedEventArgs e)
        {
            passwords = controller.GetAll().Where(x => x.ViewCount >0).ToList();
            listViewPasswords.ItemsSource = null;
            listViewPasswords.ItemsSource = passwords;
        }

        private void btnExpiryWarning_Click(object sender, RoutedEventArgs e)
        {
            passwords = controller.GetAllExpired().ToList();
            listViewPasswords.ItemsSource = null;
            listViewPasswords.ItemsSource = passwords;
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Modify(controller));
        }

        private void btnSaveAll_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBackupDecoded_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            controller.Save(new Password
            {
                Title = "Titel1",
                Login = "Login1",
                Pass = "Pass1"
            });

            controller.Save(new Password
            {
                Title = "Titel2",
                Login = "Login2",
                Pass = "Pass2"
            });

            controller.Save(new Password
            {
                Title = "Titel3",
                Login = "Login3",
                Pass = "Pass3"
            });
        }
    }
}
