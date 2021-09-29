using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassStorage3.DataAccessLayer.Interfaces
{
    public interface IStorageHandler
    {
        Task<Entities.DbPassword> GetAsync(int id);

        Task<IEnumerable<Entities.DbPassword>> GetAllAsync();

        Task SaveAsync(Entities.DbPassword pass);

        Task DeleteAsync(int id);
    }
}