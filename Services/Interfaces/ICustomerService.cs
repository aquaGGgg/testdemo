using System.Collections.Generic;
using System.Threading.Tasks;
using ToyStoreApi.DTOs;

namespace ToyStoreApi.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetByIdAsync(int id);
        Task<CustomerDto> CreateAsync(CustomerDto customerDto);
        Task<bool> UpdateAsync(int id, CustomerDto customerDto);
        Task<bool> DeleteAsync(int id);
    }
}
