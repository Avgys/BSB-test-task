using Catalog.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Catalog.Data
{
    public class SqlProductRepo : IProductRepo
    {
        private readonly CatalogContext _catalogContext;
        public SqlProductRepo(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<Product> AddAsync(Product product)
        {
            var p = await _catalogContext.AddAsync(product);            
            return product;
        }

        public bool Delete(Product product)
        {
            var ent = _catalogContext.Products.Remove(product);
            return ent.Entity.Id == product.Id;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _catalogContext.Products.FirstOrDefaultAsync(p => p.Id == id);      
            
            var category = _catalogContext.Categories.FirstAsync(c => c.Id == product.CategoryId);
            product.Category = await category;
            
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _catalogContext.Products.ToListAsync();
            foreach(var product in products)
            {
                var category = _catalogContext.Categories.FirstAsync(c => c.Id == product.CategoryId);
                product.Category = await category;
            }
            return products;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(string term)
        {            
            var products = await _catalogContext.Products.Where(p => p.Name.Contains(term)).ToListAsync();
            foreach (var product in products)
            {
                var category = _catalogContext.Categories.FirstAsync(c => c.Id == product.CategoryId);
                product.Category = await category;
            }
            return products;
        }

        public Task<Product> UpdateAsync(Product product)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            return _catalogContext.SaveChangesAsync();
        }
    }
}