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
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Text.RegularExpressions;

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
        Guid? detailsId;

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

        private void OpenDetailsDrawer(Password pass)
        {
            Logger.Instance.FunctionStart();
            try
            {
                controller.IncrementViewCount(pass.Id.Value);
                detailTitle.Text = pass.Title;
                detailLogin.Text = pass.Login;
                detailPassword.Text = pass.Pass;
                detailsId = pass.Id;

                ButtonAutomationPeer peer = new ButtonAutomationPeer(btnOpenDrawer);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        private void RefreshLabels()
        {
            passwords = controller.GetAll().ToList();

            ((btnAll.Content as StackPanel).Children[3] as TextBlock).Text = $" ({passwords.Count})";
            ((btnMostlyUsed.Content as StackPanel).Children[3] as TextBlock).Text = $" ({passwords.Count(x => x.ViewCount > 0)})";
            ((btnExpiryWarning.Content as StackPanel).Children[3] as TextBlock).Text = $" ({passwords.Count(x => x.IsExpired)})";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshLabels();

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
            RefreshLabels();
            passwords = controller.GetAll().ToList();
            listViewPasswords.ItemsSource = null;
            listViewPasswords.ItemsSource = passwords;
        }

        private void btnMostlyUsed_Click(object sender, RoutedEventArgs e)
        {
            RefreshLabels();
            passwords = controller.GetAll().Where(x => x.ViewCount > 0).ToList();
            listViewPasswords.ItemsSource = null;
            listViewPasswords.ItemsSource = passwords;
        }

        private void btnExpiryWarning_Click(object sender, RoutedEventArgs e)
        {
            RefreshLabels();
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

        }

        private void listViewPasswords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = (sender as ListView).SelectedItem as Password;
                if (item == null)
                    throw new Exception("Selected item is null");

                OpenDetailsDrawer(item);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
        }

        private void btnCopyLogin_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(detailLogin.Text);
        }

        private void btnCopyPassword_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(detailPassword.Text);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Modify(controller, detailsId));
        }
    }
}
