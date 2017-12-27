using PassStorage2.Base;
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
        Controller.Interfaces.IController controller;

        public MainWindow()
        {
            InitializeComponent();
            //ConsoleManager.Show();
            Logger.Instance.SetLogLevel(Logger.LogLevel.DEBUG);
            controller = new Controller.SqliteController();
            Switcher.pageSwitcher = this;
            Switcher.Switch(new Views.Login(controller));
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
    }
}
