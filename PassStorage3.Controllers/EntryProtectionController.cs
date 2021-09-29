using Microsoft.Extensions.Logging;
using PassStorage3.Controllers.Interfaces;
using PassStorage3.DataCryptoLayer.Interfaces;
using System.Threading.Tasks;
using PassStorage3.Exceptions;

namespace PassStorage3.Controllers
{
    public class EntryProtectionController : BaseController, IEntryProtectionController
    {
        private readonly ILogger logger;
        private readonly ISecretsVault secretsVault;

        public EntryProtectionController(ILogger<EntryProtectionController> logger, ISecretsVault secretsVault)
        {
            this.logger = logger;
            this.secretsVault = secretsVault;
        }

        public async Task ValidateAsync(string passwordPrimary, string passwordSecondary)
        {
            if (!await secretsVault.ValidatePasswordsAsync(passwordPrimary, passwordSecondary))
            {
                throw new AuthException();
            }
        }
    }
}
