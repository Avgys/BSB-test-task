using Catalog.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Data
{
	public interface IProductRepo : IRepo<Product>
    {
        Task<IEnumerable<Product>> GetAllAsync(string term);
    }
}
