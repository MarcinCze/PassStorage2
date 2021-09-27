using PassStorage3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassStorage3.Controllers.Interfaces
{
    public interface IDetailedGetController
    {
        Task<IEnumerable<Password>> GetAllExpiredAsync();
        Task<IEnumerable<Password>> GetMostUsedAsync();
        Task<IEnumerable<Password>> GetSearchResultsAsync(string searchPhrase);
    }
}
