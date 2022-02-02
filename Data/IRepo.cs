using Catalog.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public interface IRepo<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T ent);
        Task<T> UpdateAsync(T ent);
        bool Delete(T ent);
        Task<int> SaveChangesAsync();
    }
}
