using Catalog.Models;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public interface ICategoryRepo : IRepo<Category>
    {
        Task<Category> GetByNameAsync(string name);
    }
}