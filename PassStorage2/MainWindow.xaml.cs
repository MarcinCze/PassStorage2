using PassStorage2.Base;
using PassStorage2.Controller.Interfaces;
using PassStorage2.Logger.Interfaces;

using System;
using System.Windows;
using System.Windows.Controls;

namespace PassStorage2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IController controller, ILogger logger)
        {
            InitializeComponent();
            this.Title = GenerateTitle();

            Switcher.PageSwitcher = this;
            Switcher.Switch(new Views.Login(controller, logger));
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;

            if (nextPage is ISwitchable s)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not ISwitchable! " + nextPage.Name);
        }

        public string GenerateTitle() => $"PassStorage {Utils.GetVersionShort()}";
    }
}
