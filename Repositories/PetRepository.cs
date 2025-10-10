using FirstExam.Data;
using FirstExam.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstExam.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly AppDbContext context;

        public PetRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            return await context.Pets.AsNoTracking().ToListAsync();
        }

        public async Task<Pet?> GetByIdAsync(Guid id)
        {
            return await context.Pets.FindAsync(id);
        }

        public async Task AddAsync(Pet pet)
        {
            await context.Pets.AddAsync(pet);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pet pet)
        {
            context.Pets.Update(pet);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var pet = await context.Pets.FindAsync(id);
            if (pet == null) return false;

            context.Pets.Remove(pet);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
