using Catalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Catalog.Data
{
    public class SqlAccountRepo : IAccountRepo
    {
        private readonly CatalogContext _catalogContext;
        public SqlAccountRepo(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public Task<Account> AddAsync(Account ent)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(Account ent)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Account>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Account> GetAsync(string login, string password)
        {
            var account = _catalogContext.Accounts.FirstOrDefault(acc => acc.Login == login && acc.Password == password);

            if (account != null)
            {
                var role = await _catalogContext.Roles.FindAsync(account.RoleId);
                if (role != null)
                {
                    account.Role = role;
                }
            }
            return account;            
        }

        public Task<Account> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Account> GetByNameAsync(string name)
        {
            var acc = _catalogContext.Accounts.FirstOrDefault(acc => acc.Login == name);
            if (acc != null)
            {
                var role = await _catalogContext.Roles.FindAsync(acc.RoleId);
                if (role != null)
                {
                    acc.Role = role;
                }
            }

            return acc;
        }

        public Task<int> SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Account> UpdateAsync(Account ent)
        {
            throw new System.NotImplementedException();
        }
    }
}