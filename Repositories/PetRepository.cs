using System;
using FirstExam.Data;
using FirstExam.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstExam.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly AppDbContext _context;

        public PetRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pet>> GetAll()
        {
           
            return await _context.Pets.AsNoTracking().ToListAsync();
        }

        public async Task<Pet?> GetById(Guid id)
        {
            return await _context.Pets.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Add(Pet pet)
        {
            await _context.Pets.AddAsync(pet);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Pet pet)
        {
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var pet = await _context.Pets.FirstOrDefaultAsync(p => p.Id == id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
            }
        }
    }
}
