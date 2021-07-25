using MaterialDesignThemes.Wpf;

using PassStorage2.Base;
using PassStorage2.Controller.Interfaces;
using PassStorage2.Logger.Interfaces;
using PassStorage2.Models;

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PassStorage2.Views
{
    /// <summary>
    /// Interaction logic for Modify.xaml
    /// </summary>
    public partial class Modify : UserControl
    {
        private readonly IController controller;
        private readonly ILogger logger;
        private readonly IServiceProvider serviceProvider;
        private Counters counters;
        private Password password;

        public Modify(IController controller, ILogger logger, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            this.logger = logger;
            this.controller = controller;
            this.serviceProvider = serviceProvider;

            logger.Debug("Creating Modify user control");

            lbBuild.Text = Utils.GetBuildName();
            TranslateControls();
        }

        public void SetCounters(Counters counters)
        {
            this.counters = counters;
        }

        public void SetPassword(int passwordId)
        {
            logger.Debug("Entering EDIT mode");
            password = controller?.Get(passwordId);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            logger.FunctionStart();
            logger.Debug($"Are counters need refreshing? - {counters.NeedRefresh}");
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
                tbAdditionalInfo.Text = password.AdditionalInfo;
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
            logger.Debug("Switching to dashboard. Mode = All");
            var dashboardView = (Dashboard)serviceProvider.GetService(typeof(Dashboard));
            dashboardView.SetMenuType(Dashboard.MenuType.All);
            Switcher.Switch(dashboardView);
        }

        private void btnMostlyUsed_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("Switching to dashboard. Mode = Most");
            var dashboardView = (Dashboard)serviceProvider.GetService(typeof(Dashboard));
            dashboardView.SetMenuType(Dashboard.MenuType.Most);
            Switcher.Switch(dashboardView);
        }

        private void btnExpiryWarning_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("Switching to dashboard. Mode = Expiry");
            var dashboardView = (Dashboard)serviceProvider.GetService(typeof(Dashboard));
            dashboardView.SetMenuType(Dashboard.MenuType.Expiry);
            Switcher.Switch(dashboardView);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            logger.FunctionStart();
            try
            {
                bool updTime = string.IsNullOrEmpty(password?.Pass) || !(password.Pass.Equals(tbPassword.Text));
                logger.Debug($"Update password change time - {updTime}");

                password.Title = tbTitle.Text;
                password.AdditionalInfo = tbAdditionalInfo.Text;
                password.Login = tbLogin.Text;
                password.Pass = tbPassword.Text;

                if (string.IsNullOrEmpty(password.Uid))
                    password.Uid = Guid.NewGuid().ToString();

                controller.Save(password, updTime);
                var dashboardView = (Dashboard)serviceProvider.GetService(typeof(Dashboard));
                Switcher.Switch(dashboardView);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            finally
            {
                logger.FunctionEnd();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("Switching to Dashboard. Mode = All");
            var dashboardView = (Dashboard)serviceProvider.GetService(typeof(Dashboard));
            Switcher.Switch(dashboardView);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            logger.FunctionStart();
            Application.Current.Shutdown();
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRandomPass_Click(object sender, RoutedEventArgs e)
        {
            logger.FunctionStart();
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
            HintAssist.SetHint(tbAdditionalInfo, controller.Translate(HintAssist.GetHint(tbAdditionalInfo).ToString()));
            HintAssist.SetHint(tbLogin, controller.Translate(HintAssist.GetHint(tbLogin).ToString()));
            HintAssist.SetHint(tbPassword, controller.Translate(HintAssist.GetHint(tbPassword).ToString()));
            sliderRandomPassLength.ToolTip = controller.Translate(sliderRandomPassLength.ToolTip.ToString());
            btnRandomPass.ToolTip = controller.Translate(btnRandomPass.ToolTip.ToString());
        }
    }
}
