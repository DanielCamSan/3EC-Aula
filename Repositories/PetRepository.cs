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

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            return await _context.Pets.ToListAsync();
        }

        public async Task<Pet?> GetByIdAsync(Guid id)
        {
            return await _context.Pets.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pet> CreateAsync(Pet pet)
        {
            await _context.Pets.AddAsync(pet);
            await _context.SaveChangesAsync();
            return pet;
        }

        public async Task<Pet?> UpdateAsync(Guid id, Pet pet)
        {
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
            return pet;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var pet = await GetByIdAsync(id);
            if (pet == null) return false;

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}