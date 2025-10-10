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

        public async Task Add(Pet Pet)
        {
            await _context.Pets.AddAsync(Pet);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var Pet = await _context.Pets.FirstOrDefaultAsync(x => x.Id == id);
            if (Pet != null)
            {
                _context.Pets.Remove(Pet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Pet>> GetAll()
        {
            return await _context.Pets.ToListAsync();
        }

        public async Task<Pet?> GetById(Guid id)
        {
            return await _context.Pets.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task Update(Pet Pet)
        {
            _context.Pets.Update(Pet);
            await _context.SaveChangesAsync();
        }
    }
}