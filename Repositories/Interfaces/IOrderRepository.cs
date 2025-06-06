using System.Collections.Generic;
using System.Threading.Tasks;
using ToyStoreApi.Entities;

namespace ToyStoreApi.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
