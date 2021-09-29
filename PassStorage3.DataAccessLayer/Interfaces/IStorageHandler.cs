using PassStorage3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassStorage3.DataAccessLayer.Interfaces
{
    public interface IStorageHandler
    {
        Task<Password> GetAsync(int id);

        Task<IEnumerable<Password>> GetAllAsync();

        Task SaveAsync(Password pass);

        Task DeleteAsync(int id);
    }
}
