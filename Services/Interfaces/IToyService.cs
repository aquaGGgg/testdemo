using System.Collections.Generic;
using System.Threading.Tasks;
using ToyStoreApi.DTOs;

namespace ToyStoreApi.Services.Interfaces
{
    public interface IToyService
    {
        Task<IEnumerable<ToyDto>> GetAllAsync();
        Task<ToyDto> GetByIdAsync(int id);
        Task<ToyDto> CreateAsync(ToyDto toyDto);
        Task<bool> UpdateAsync(int id, ToyDto toyDto);
        Task<bool> DeleteAsync(int id);
    }
}
