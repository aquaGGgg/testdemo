using System.Collections.Generic;
using System.Threading.Tasks;
using ToyStoreApi.Entities;

namespace ToyStoreApi.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
