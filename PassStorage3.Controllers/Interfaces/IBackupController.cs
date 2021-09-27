using System.Threading.Tasks;

namespace PassStorage3.Controllers.Interfaces
{
    public interface IBackupController
    {
        Task BackupAsync();
        Task BackupDecodedAsync();
    }
}
