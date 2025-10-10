using FirstExam.Models;
using FirstExam.Models.dtos;
using System;

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
            await _context.SaveChangesAsync();
        }
        public async Task<Owner?> Update(Guid id, UpdateOwnerDto dto)
        {
            var owner = await _context.Owners.FirstOrDefaultAsync(x => x.Id == id);

            if (owner == null) return null;

            owner = new Owner
            {
                Id = Guid.NewGuid(),
                Email = dto.Email.Trim(),
                FullName = dto.FullName.Trim(),
                Phone = dto.Phone,
                Active = dto.Active,
            };

            _context.Owners.Update(owner);
            await _context.SaveChangesAsync();
            return owner;
        }
        public async Task Delete(Guid id)
        {
            var owner = await _context.Owners.FirstOrDefaultAsync(x => x.Id == id);
            if (owner != null)
            {
                _context.Owners.Remove(owner);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Owner>> GetAll()
        {
            return await _context.Owners.AsNoTracking()
            .Include(b => b.FullName).ToListAsync();
        }
        public async Task<Owner?> GetById(Guid id)
        {
            return await _context.Owners.FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
