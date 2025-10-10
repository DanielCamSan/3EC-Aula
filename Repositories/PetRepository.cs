using FirstExam.Data;
using FirstExam.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstExam.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly AppDbContext _context;
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
            return await _context.Pets.ToListAsync();
        }

        public async Task<Pet?> GetById(Guid id)
        {
            return await _context.Pets.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Pet?> Update(Guid id, UpdatePetDto dto)
        {
            var pet = await _context.Pets.FirstOrDefaultAsync(x => x.Id == id);

            if (pet == null) return null;

            pet = new Pet
            {
                OwnerId = dto.OwnerId,
                Name = dto.Name,
                Species = dto.Species,
                Breed = dto.Breed,
                BirthDate = dto.BirthDate,
                sex = dto.sex,
                WeightKg = dto.WeightKg != null ? dto.WeightKg : 0,
            };

            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
            return pet;
        }
    }
}
