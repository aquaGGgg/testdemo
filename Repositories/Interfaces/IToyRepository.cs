using System.Collections.Generic;
using System.Threading.Tasks;
using ToyStoreApi.Entities;

namespace ToyStoreApi.Repositories.Interfaces
{
    public interface IToyRepository
    {
        Task<IEnumerable<Toy>> GetAllAsync();
        Task<Toy> GetByIdAsync(int id);
        Task AddAsync(Toy toy);
        Task UpdateAsync(Toy toy);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
