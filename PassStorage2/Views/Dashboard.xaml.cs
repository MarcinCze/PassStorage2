using PassStorage2.Base;
using PassStorage2.Controller.Interfaces;
using PassStorage2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

namespace PassStorage2.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        private readonly IController controller;
        private readonly MenuType menu;
        private List<Password> passwords;
        private Counters counters;
        private int detailsId;

        public enum MenuType { All, Most, Expiry }

        public Dashboard(IController cntr, MenuType menu = MenuType.All)
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
                Task.Run(() => controller.IncrementViewCount(pass.Id));
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
            Logger.Instance.FunctionStart();
            try
            {
                counters = new Counters(passwords.Count, controller.GetMostUsed(passwords).Count(), passwords.Count(x => x.IsExpired));
                ((btnAll.Content as StackPanel).Children[3] as TextBlock).Text = $" ({counters.All})";
                ((btnMostlyUsed.Content as StackPanel).Children[3] as TextBlock).Text = $" ({counters.MostUsed})";
                ((btnExpiryWarning.Content as StackPanel).Children[3] as TextBlock).Text = $" ({counters.Expired})";
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ControlLoadingGrid(true, "Loading...");

            passwords = null;
            listViewPasswords.ItemsSource = null;
            Task.Run(LoadList);
        }

        private void LoadList()
        {
            passwords = controller.GetAll()?.ToList();

            Dispatcher.Invoke(() =>
            {
                ControlLoadingGrid(false, null);

                switch (menu)
                {
                    case MenuType.All:
                        {
                            btnAll_Click(null, null);
                            break;
                        }
                    case MenuType.Most:
                        {
                            btnMostlyUsed_Click(null, null);
                            break;
                        }
                    case MenuType.Expiry:
                        {
                            btnExpiryWarning_Click(null, null);
                            break;
                        }
                }
            });
        }

        private void ControlLoadingGrid(bool isVisible, string message)
        {
            Logger.Instance.Debug($"Setting loading spinner grid to [{isVisible}] with message [{message}]");

            if (!string.IsNullOrEmpty(message)) waitMsg.Text = message;

            gridWait.Visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.FunctionStart();
            try
            {
                RefreshLabels();
                listViewPasswords.ItemsSource = null;
                listViewPasswords.ItemsSource = passwords;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        private void btnMostlyUsed_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.FunctionStart();
            try
            {
                RefreshLabels();
                var mostUsed = controller.GetMostUsed(passwords);
                listViewPasswords.ItemsSource = null;
                listViewPasswords.ItemsSource = mostUsed;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        private void btnExpiryWarning_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.FunctionStart();
            try
            {
                RefreshLabels();
                var expired = controller.GetAllExpired(passwords);
                listViewPasswords.ItemsSource = null;
                listViewPasswords.ItemsSource = expired;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.Debug("Switching to Modify view");
            Switcher.Switch(new Modify(controller, counters));
        }

        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.FunctionStart();
            try
            {
                controller.Backup();
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        private void btnBackupDecoded_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.FunctionStart();
            try
            {
                controller.BackupDecoded();
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.FunctionStart();
            Application.Current.Shutdown();
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listViewPasswords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Logger.Instance.FunctionStart();
            try
            {
                if (!((sender as ListView).SelectedItem is Password item))
                    throw new Exception("Selected item is null");

                OpenDetailsDrawer(item);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        private void btnCopyLogin_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.Debug("Copying login");
            Clipboard.SetText(detailLogin.Text);
        }

        private void btnCopyPassword_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.Debug("Copying password");
            Clipboard.SetText(detailPassword.Text);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.Debug("Switching to Modify view");
            Switcher.Switch(new Modify(controller, counters, detailsId));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("Are you sure to remove?", "Confirm", MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (response != MessageBoxResult.Yes)
                return;
            
            controller.Delete(detailsId);
            UserControl_Loaded(null, null);
        }
    }
}
