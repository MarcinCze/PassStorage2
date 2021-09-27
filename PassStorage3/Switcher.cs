using System.Windows.Controls;

namespace PassStorage3
{
    public static class Switcher
    {
        public static MainWindow PageSwitcher { get; set; }

        public static void Switch(UserControl newPage)
        {
            PageSwitcher.Navigate(newPage);
        }

        public static void Switch(UserControl newPage, object state)
        {
            PageSwitcher.Navigate(newPage, state);
        }
    }
}
