using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstExam.Data;
using FirstExam.Models;

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
            return await _context.Owners.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task Add(Owner owner)
        {
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner != null)
            {
                _context.Owners.Remove(owner);
                await _context.SaveChangesAsync();
            }
        }
    }
}
