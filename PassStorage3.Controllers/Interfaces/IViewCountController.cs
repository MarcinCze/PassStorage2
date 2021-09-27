using System.Threading.Tasks;

namespace PassStorage3.Controllers.Interfaces
{
    public interface IViewCountController
    {
        Task IncrementViewCountAsync(int id);
    }
}