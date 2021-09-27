using PassStorage3.Controllers.Interfaces;
using System;
using System.Threading.Tasks;

namespace PassStorage3.Controllers
{
    public class BackupController : BaseController, IBackupController
    {
        public Task BackupAsync()
        {
            throw new NotImplementedException();
        }

        public Task BackupDecodedAsync()
        {
            throw new NotImplementedException();
        }
    }
}
