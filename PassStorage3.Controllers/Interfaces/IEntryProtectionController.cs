using System.Threading.Tasks;

namespace PassStorage3.Controllers.Interfaces
{
    public interface IEntryProtectionController
    {
        Task ValidateAsync(string passwordPrimary, string passwordSecondary);
    }
}
