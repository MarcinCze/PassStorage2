using PassStorage3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassStorage3.Controllers.Interfaces
{
    public interface ICrudController
    {
        Task<Password> GetAsync(int id);

        Task<IEnumerable<Password>> GetAllAsync();
        
        Task SaveAsync(Password pass);

        Task DeleteAsync(int id);
    }
}