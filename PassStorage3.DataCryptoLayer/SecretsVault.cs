using PassStorage3.DataCryptoLayer.Interfaces;
using System.Threading.Tasks;

namespace PassStorage3.DataCryptoLayer
{
    public class SecretsVault : ISecretsVault
    {
        public string PasswordPrimary { get; private set; }

        public string PasswordSecondary { get; private set; }

        public async Task<bool> ValidatePasswordsAsync(string passPrimary, string passSecondary)
        {
            // TODO Add Hash checking
            if (passPrimary == "SkodaFabia" && passSecondary == "martusia")
            {
                PasswordPrimary = passPrimary;
                PasswordSecondary = passSecondary;
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
