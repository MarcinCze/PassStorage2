using System;
using PassStorage2.Base;
using PassStorage2.Controller.Interfaces;
using PassStorage2.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace PassStorage2.Views
{
    /// <summary>
    /// Interaction logic for Modify.xaml
    /// </summary>
    public partial class Modify : UserControl
    {
        private readonly IController controller;
        private readonly Counters counters;
        private Password password;

        public Modify(IController cntr, Counters c, int? passwordId = null)
        {
            InitializeComponent();
            Logger.Instance.Debug("Creating Modify user control");
            
            counters = c;
            Logger.Instance.Debug("Rewriting counters.", counters);

            if (cntr is null)
                Logger.Instance.Warning("Modify :: controller is empty");

            controller = cntr;

            if (passwordId.HasValue)
            {
                Logger.Instance.Debug("Entering EDIT mode");
                password = controller?.Get(passwordId.Value);
            }
            else
            {
                Logger.Instance.Debug("Entering INSERT mode");
            }

            TranslateControls();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Logger.Instance.FunctionStart();
            Logger.Instance.Debug($"Are counters need refreshing? - {counters.NeedRefresh}");
            if (counters.NeedRefresh)
            {
                var passwords = controller.GetAll().ToList();
                counters.All = passwords.Count;
                counters.MostUsed = controller.GetMostUsed(passwords).Count();
                counters.Expired = passwords.Count(x => x.IsExpired);
            }

            ((btnAll.Content as StackPanel).Children[2] as TextBlock).Text += $" ({counters.All})";
            ((btnMostlyUsed.Content as StackPanel).Children[2] as TextBlock).Text += $" ({counters.MostUsed})";
            ((btnExpiryWarning.Content as StackPanel).Children[2] as TextBlock).Text += $" ({counters.Expired})";

            if (password != null)
            {
                tbTitle.Text = password.Title;
                tbLogin.Text = password.Login;
                tbPassword.Text = password.Pass;
            }
            else
            {
                password = new Password();
            }
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.Debug("Switching to dashboard. Mode = All");
            Switcher.Switch(new Dashboard(controller, Dashboard.MenuType.All));
        }

        private void btnMostlyUsed_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.Debug("Switching to dashboard. Mode = Most");
            Switcher.Switch(new Dashboard(controller, Dashboard.MenuType.Most));
        }

        private void btnExpiryWarning_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.Debug("Switching to dashboard. Mode = Expiry");
            Switcher.Switch(new Dashboard(controller, Dashboard.MenuType.Expiry));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.FunctionStart();
            try
            {
                bool updTime;
                if (string.IsNullOrEmpty(password?.Pass))
                {
                    updTime = true;
                }
                else
                {
                    updTime = !(password.Pass.Equals(tbPassword.Text));
                }

                Logger.Instance.Debug($"Update password change time - {updTime}");

                password.Title = tbTitle.Text;
                password.Login = tbLogin.Text;
                password.Pass = tbPassword.Text;

                if (string.IsNullOrEmpty(password.Uid))
                    password.Uid = Guid.NewGuid().ToString();

                controller.Save(password, updTime);
                Switcher.Switch(new Dashboard(controller));
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.Debug("Switching to Dashboard. Mode = All");
            Switcher.Switch(new Dashboard(controller, Dashboard.MenuType.All));
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.FunctionStart();
            Application.Current.Shutdown();
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRandomPass_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.FunctionStart();
            tbPassword.Text = RandomPassword.Generate((int)sliderRandomPassLength.Value);
        }

        private void TranslateControls()
        {
            labelBtnAll.Text = controller.Translate(labelBtnAll.Text);
            btnAll.ToolTip = controller.Translate(btnAll.ToolTip.ToString());
            labelBtnMostlyUsed.Text = controller.Translate(labelBtnMostlyUsed.Text);
            btnMostlyUsed.ToolTip = controller.Translate(btnMostlyUsed.ToolTip.ToString());
            labelBtnExpiryWarning.Text = controller.Translate(labelBtnExpiryWarning.Text);
            btnExpiryWarning.ToolTip = controller.Translate(btnExpiryWarning.ToolTip.ToString());
            labelBtnExit.Text = controller.Translate(labelBtnExit.Text);
            btnExit.ToolTip = controller.Translate(btnExit.ToolTip.ToString());
            labelBtnAbout.Text = controller.Translate(labelBtnAbout.Text);
            btnAbout.ToolTip = controller.Translate(btnAbout.ToolTip.ToString());
            labelNavBtnSave.Text = controller.Translate(labelNavBtnSave.Text);
            btnSave.ToolTip = controller.Translate(btnSave.ToolTip.ToString());
            labelNavBtnCancel.Text = controller.Translate(labelNavBtnCancel.Text);
            btnCancel.ToolTip = controller.Translate(btnCancel.ToolTip.ToString());
            HintAssist.SetHint(tbTitle, controller.Translate(HintAssist.GetHint(tbTitle).ToString()));
            HintAssist.SetHint(tbLogin, controller.Translate(HintAssist.GetHint(tbLogin).ToString()));
            HintAssist.SetHint(tbPassword, controller.Translate(HintAssist.GetHint(tbPassword).ToString()));
            sliderRandomPassLength.ToolTip = controller.Translate(sliderRandomPassLength.ToolTip.ToString());
            btnRandomPass.ToolTip = controller.Translate(btnRandomPass.ToolTip.ToString());
        }
    }
}
