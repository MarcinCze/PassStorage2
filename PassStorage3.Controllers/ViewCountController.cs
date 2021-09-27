using PassStorage3.Controllers.Interfaces;
using System;
using System.Threading.Tasks;

namespace PassStorage3.Controllers
{
    public class ViewCountController : BaseController, IViewCountController
    {
        public Task IncrementViewCountAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
