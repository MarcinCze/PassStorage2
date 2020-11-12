using PassStorage2.Base.DataCryptoLayer;
using System;
using System.Windows.Forms;

namespace PassStorage2.HashGen
{
    public partial class PasswordGenerator : Form
    {
        public PasswordGenerator()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(tbHash.Text);
                MessageBox.Show("Copied successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Copied with error {Environment.NewLine} {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                tbHash.Text = SHA.GenerateSHA256String(tbPassword.Text);
            }
            catch(Exception)
            { }
        }
    }
}
