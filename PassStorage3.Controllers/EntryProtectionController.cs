using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PassStorage3.Controllers.Interfaces;

namespace PassStorage3.Controllers
{
    public class EntryProtectionController : BaseController, IEntryProtectionController
    {
        public Task<bool> ValidateAsync(string passwordPrimary, string passwordSecondary)
        {
            throw new NotImplementedException();
        }
    }
}
