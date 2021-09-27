using System.Threading.Tasks;

namespace PassStorage3.Controllers.Interfaces
{
    public interface IEntryProtectionController
    {
        Task<bool> ValidateAsync(string passwordPrimary, string passwordSecondary);
    }
}
