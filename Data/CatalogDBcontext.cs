using Microsoft.EntityFrameworkCore;
using Catalog.Models;

namespace Catalog.Data
{
    public class CatalogContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }

        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }
    }
}