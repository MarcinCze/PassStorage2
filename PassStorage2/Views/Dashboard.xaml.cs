﻿using System;
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
        List<Password> passwords;

        public Dashboard(IController cntr)
        {
            InitializeComponent();
            Logger.Instance.Debug("Creating Dashboard user control");

            if (cntr is null)
            {
                Logger.Instance.Warning("Dashboard :: controller is empty");
            }

            controller = cntr;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            passwords = controller.GetAll().ToList();

            ((btnAll.Content as StackPanel).Children[2] as TextBlock).Text += $" ({passwords.Count})";
            ((btnMostlyUsed.Content as StackPanel).Children[2] as TextBlock).Text += $" ({passwords.Count(x => x.ViewCount > 0)})";
            ((btnExpiryWarning.Content as StackPanel).Children[2] as TextBlock).Text += $" ({controller.GetAllExpired().Count()})";
        }

        private void menuMinimize_Click(object sender, RoutedEventArgs e)
        {
            Switcher.pageSwitcher.WindowState = WindowState.Minimized;
        }

        private void menuClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void menuAbout_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            //controller.Save(new Password
            //{
            //    Title = "Titel1",
            //    Login = "Login1",
            //    Pass = "Pass1"
            //});

            //controller.Save(new Password
            //{
            //    Title = "Titel2",
            //    Login = "Login2",
            //    Pass = "Pass2"
            //});

            //controller.Save(new Password
            //{
            //    Title = "Titel3",
            //    Login = "Login3",
            //    Pass = "Pass3"
            //});
        }

        private void btnMostlyUsed_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExpiryWarning_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {

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
            menuClose_Click(sender, e);
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
