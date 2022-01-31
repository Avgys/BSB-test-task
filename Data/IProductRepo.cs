using Catalog.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Data
{
	public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProduct(int id);
    }
}
