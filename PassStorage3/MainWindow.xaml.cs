using PassStorage3.Views;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PassStorage3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(LoginControl loginView)
        {
            InitializeComponent();
            this.Title = GenerateTitle();

            Switcher.PageSwitcher = this;
            Switcher.Switch(loginView);
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
                throw new ArgumentException($"NextPage is not ISwitchable! {nextPage.Name}");
        }

        //public string GenerateTitle() => $"PassStorage {Utils.GetVersionShort()}";
        public string GenerateTitle() => $"PassStorage 3";
    }
}