using FirstExam.Data;
using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Owner>> GetAll()
        {
            return await _context.Owners.ToListAsync();
        }

        public async Task<Owner?> GetById(Guid id)
        {
            return await _context.Owners.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Create(Owner owner)
        {
            await _context.Owners.AddAsync(owner);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Owner existingOwner)
        {
            _context.Owners.Update(existingOwner);
            await _context.SaveChangesAsync();
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
    }
}