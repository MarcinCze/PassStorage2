using Microsoft.Extensions.Logging;
using PassStorage3.Controllers.Interfaces;
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
    /// Interaction logic for DashboardControl.xaml
    /// </summary>
    public partial class DashboardControl : UserControl
    {
        private readonly ILogger logger;
        private readonly ICrudController crudController;

        public DashboardControl(ILogger<DashboardControl> logger, ICrudController crudController)
        {
            InitializeComponent();

            this.logger = logger;
            this.crudController = crudController;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ControlLoadingGrid(true, "Loading...");
            var passwords = await crudController.GetAllAsync();
            ControlLoadingGrid(false);
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMostlyUsed_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExpiryWarning_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
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

        private void ControlLoadingGrid(bool isVisible, string message = null)
        {
            logger.LogInformation($"Setting loading spinner grid to [{isVisible}] with message [{message}]");

            waitMsg.Text = string.IsNullOrEmpty(message) ? string.Empty : message;

            gridWait.Visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
        }
    }
}