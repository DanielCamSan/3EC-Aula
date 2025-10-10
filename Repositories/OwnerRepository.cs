using FirstExam.Models;
using FirstExam.Data;
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
        public async Task Add(Owner owner)
        {
            await _context.Owners.AddAsync(owner);
        }

        public async Task Delete(Guid id)
        {
            var owner = await _context.Owners.FirstOrDefaultAsync(g => g.Id == id);
            if (owner != null)
            {
                _context.Owners.Remove(owner);
            }
        }

        public async Task<IEnumerable<Owner>> GetAll()
        {
            return await _context.Owners.ToListAsync();
        }

        public async Task<Owner?> GetById(Guid id)
        {
            return await _context.Owners.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task Update(Owner owner)
        {
            var existing = await _context.Owners.FirstOrDefaultAsync(o => o.Id == owner.Id);
            if (existing != null)
            {
                existing.FullName = owner.FullName;
                existing.Email = owner.Email;
                existing.Phone = owner.Phone;
                existing.Active = owner.Active;

                _context.Owners.Update(existing);
            }
        }

    }
}
