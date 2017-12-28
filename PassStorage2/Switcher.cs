using System.Windows.Controls;

namespace PassStorage2
{
    public static class Switcher
    {
        public static MainWindow PageSwitcher;

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
