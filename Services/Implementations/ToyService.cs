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
    public class ToyService : IToyService
    {
        private readonly IToyRepository _toyRepository;

        public ToyService(IToyRepository toyRepository)
        {
            _toyRepository = toyRepository;
        }

        public async Task<IEnumerable<ToyDto>> GetAllAsync()
        {
            var toys = await _toyRepository.GetAllAsync();
            return toys.Select(t => new ToyDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Price = t.Price,
                Stock = t.Stock
            });
        }

        public async Task<ToyDto> GetByIdAsync(int id)
        {
            var toy = await _toyRepository.GetByIdAsync(id);
            if (toy == null)
                return null;

            return new ToyDto
            {
                Id = toy.Id,
                Name = toy.Name,
                Description = toy.Description,
                Price = toy.Price,
                Stock = toy.Stock
            };
        }

        public async Task<ToyDto> CreateAsync(ToyDto toyDto)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(toyDto.Name))
                throw new ArgumentException("Название игрушки обязательно.");

            var newToy = new Toy
            {
                Name = toyDto.Name,
                Description = toyDto.Description,
                Price = toyDto.Price,
                Stock = toyDto.Stock
            };

            await _toyRepository.AddAsync(newToy);

            toyDto.Id = newToy.Id;
            return toyDto;
        }

        public async Task<bool> UpdateAsync(int id, ToyDto toyDto)
        {
            if (!await _toyRepository.ExistsAsync(id))
                return false;

            var existing = await _toyRepository.GetByIdAsync(id);
            existing.Name = toyDto.Name;
            existing.Description = toyDto.Description;
            existing.Price = toyDto.Price;
            existing.Stock = toyDto.Stock;

            await _toyRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _toyRepository.ExistsAsync(id))
                return false;

            await _toyRepository.DeleteAsync(id);
            return true;
        }
    }
}
