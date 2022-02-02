using Catalog.Models;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public interface IAccountRepo : IRepo<Account> {
        Task<Account> GetAsync(string login, string password);
        Task<Account> GetByNameAsync(string name);
    }
}