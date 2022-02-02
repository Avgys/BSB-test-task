using Catalog.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public class SqlCategoryRepo : ICategoryRepo
    {
        private readonly CatalogContext _catalogContext;
        public SqlCategoryRepo(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<Category> AddAsync(Category category)
        {
            var p = await _catalogContext.AddAsync(category);
            return category;
        }

        public bool Delete(Category category)
        { 
            var ent = _catalogContext.Categories.Remove(category);
            return category.Id == ent.Entity.Id;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _catalogContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _catalogContext.Categories.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            _catalogContext.Entry(category).State = EntityState.Modified;

            try
            {
                await _catalogContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await _catalogContext.Categories.AnyAsync(ent => ent.Id == category.Id)))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            
            return await _catalogContext.Categories.FirstOrDefaultAsync(e => e.Id == category.Id);
        }

        public Task<int> SaveChangesAsync()
        {
            return _catalogContext.SaveChangesAsync();
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _catalogContext.Categories.FirstOrDefaultAsync(cat => cat.Name == name);
        }
    }
}