using FirstExam.Data;
using FirstExam.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstExam.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly AppDbContext _context;

        public OwnerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            return await _context.Owners.ToListAsync();
        }

        public async Task<Owner?> GetByIdAsync(Guid id)
        {
            return await _context.Owners.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Owner> CreateAsync(Owner owner)
        {
            await _context.Owners.AddAsync(owner);
            await _context.SaveChangesAsync();
            return owner;
        }

        public async Task<Owner?> UpdateAsync(Owner owner)
        {
            _context.Owners.Update(owner);
            await _context.SaveChangesAsync();
            return owner;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var owner = await GetByIdAsync(id);
            if (owner == null) return false;

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}