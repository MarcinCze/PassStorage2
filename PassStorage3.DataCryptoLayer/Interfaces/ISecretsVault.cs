using System.Threading.Tasks;

namespace PassStorage3.DataCryptoLayer.Interfaces
{
    public interface ISecretsVault
    {
        string PasswordPrimary { get; }
        string PasswordSecondary { get; }
        Task<bool> ValidatePasswordsAsync(string passPrimary, string passSecondary);
    }
}
