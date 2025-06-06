using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToyStoreApi.Data;
using ToyStoreApi.Entities;
using ToyStoreApi.Repositories.Interfaces;

namespace ToyStoreApi.Repositories.Implementations
{
    public class ToyRepository : IToyRepository
    {
        private readonly AppDbContext _context;

        public ToyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Toy>> GetAllAsync()
        {
            return await _context.Toys.AsNoTracking().ToListAsync();
        }

        public async Task<Toy> GetByIdAsync(int id)
        {
            return await _context.Toys.FindAsync(id);
        }

        public async Task AddAsync(Toy toy)
        {
            await _context.Toys.AddAsync(toy);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Toy toy)
        {
            _context.Toys.Update(toy);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var toy = await _context.Toys.FindAsync(id);
            if (toy != null)
            {
                _context.Toys.Remove(toy);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Toys.AnyAsync(t => t.Id == id);
        }
    }
}
