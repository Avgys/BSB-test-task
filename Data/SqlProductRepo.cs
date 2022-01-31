using Catalog.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public class SqlProductRepo : IProductRepo
    {
        private readonly CatalogContext _catalogContext;
        public SqlProductRepo(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _catalogContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext.Products.ToListAsync();
        }
    }
}