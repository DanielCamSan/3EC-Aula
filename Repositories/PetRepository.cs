using FirstExam.Data;
using FirstExam.Models;
using FirstExam.Models.dtos;
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

        public async Task Add(Pet pet)
        {
            await _context.Pets.AddAsync(pet);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var pet = await _context.Pets.FirstOrDefaultAsync(x => x.Id == id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Pet>> GetAll()
        {
            return await _context.Pets.AsNoTracking().Include(p => p.Owner).Include(p => p.Appointments).ToListAsync();
        }

        public async Task<Pet?> GetById(Guid id)
        {
            return await _context.Pets.Include(p => p.Owner).Include(p => p.Appointments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Pet?> Update(Pet pet)
        {
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
            return pet;
        }
    }
}