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
using PassStorage2.Controller.Interfaces;
using PassStorage2.Base;

namespace PassStorage2.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        IController controller;

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
