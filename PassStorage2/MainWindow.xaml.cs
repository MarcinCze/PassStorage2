using System;
using System.Windows;
using System.Windows.Controls;

using PassStorage2.Controller.Interfaces;

namespace PassStorage2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IController controller)
        {
            InitializeComponent();

            Switcher.PageSwitcher = this;
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
