using System.Collections.Generic;
using System.Threading.Tasks;
using ToyStoreApi.DTOs;

namespace ToyStoreApi.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> GetByIdAsync(int id);
        Task<OrderDto> CreateAsync(OrderDto orderDto);
        Task<bool> UpdateAsync(int id, OrderDto orderDto);
        Task<bool> DeleteAsync(int id);
    }
}
