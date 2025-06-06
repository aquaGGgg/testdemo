using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToyStoreApi.DTOs;
using ToyStoreApi.Entities;
using ToyStoreApi.Repositories.Interfaces;
using ToyStoreApi.Services.Interfaces;

namespace ToyStoreApi.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                RegisteredAt = c.RegisteredAt
            });
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var c = await _customerRepository.GetByIdAsync(id);
            if (c == null)
                return null;

            return new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                RegisteredAt = c.RegisteredAt
            };
        }

        public async Task<CustomerDto> CreateAsync(CustomerDto customerDto)
        {
            if (string.IsNullOrWhiteSpace(customerDto.FirstName) ||
                string.IsNullOrWhiteSpace(customerDto.LastName) ||
                string.IsNullOrWhiteSpace(customerDto.Email))
            {
                throw new ArgumentException("Имя, фамилия и email обязательны.");
            }

            var newCustomer = new Customer
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                RegisteredAt = DateTime.UtcNow
            };

            await _customerRepository.AddAsync(newCustomer);

            customerDto.Id = newCustomer.Id;
            customerDto.RegisteredAt = newCustomer.RegisteredAt;
            return customerDto;
        }

        public async Task<bool> UpdateAsync(int id, CustomerDto customerDto)
        {
            if (!await _customerRepository.ExistsAsync(id))
                return false;

            var existing = await _customerRepository.GetByIdAsync(id);
            existing.FirstName = customerDto.FirstName;
            existing.LastName = customerDto.LastName;
            existing.Email = customerDto.Email;
            existing.Phone = customerDto.Phone;
            // RegisteredAt оставляем без изменений

            await _customerRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _customerRepository.ExistsAsync(id))
                return false;

            await _customerRepository.DeleteAsync(id);
            return true;
        }
    }
}
