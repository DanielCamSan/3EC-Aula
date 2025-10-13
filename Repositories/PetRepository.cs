using FirstExam.Models;
using FirstExam.Data;
using Microsoft.EntityFrameworkCore;
using FirstExam.Models.Dtos;

namespace FirstExam.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly AppDbContext _context;

        public PetRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Pet>> GetAllWithOwner()
        {
            return await _context.Pets
                .AsNoTracking()
                .Include(p => p.OwnerId)
                .ToListAsync();
        }

        public async Task<Pet?> GetByIdWithOwner(Guid id)
        {
            return await _context.Pets
                .AsNoTracking()
                .Include(p => p.OwnerId)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pet>> GetAll()
        {
            return await _context.Pets
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Pet?> GetById(Guid id)
        {
            return await _context.Pets
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> ExistsByName(string name)
        {
            return await _context.Pets.AnyAsync(p => p.Name.ToLower() == name.ToLower());
        }

        
        public async Task<bool> ExistsByNameExcludingId(string name, Guid excludeId)
        {
            return await _context.Pets.AnyAsync(p => p.Id != excludeId && p.Name.ToLower() == name.ToLower());
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
        public async Task<Pet?> Update(Guid id, UpdatePetDto dto)
        {
            var pet = await _context.Pets.FirstOrDefaultAsync(p => p.Id == id);
            if (pet == null) return null;
            pet.Name = dto.Name;
            pet.Species = dto.Species;
            pet.Breed = dto.Breed;
            pet.BirthDate = dto.BirthDate;
            pet.sex = dto.Sex;
            pet.WeightKg = dto.WeightKg;
            pet.OwnerId = dto.OwnerId;

            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
            return pet;
        }
        public async Task<bool> Delete(Guid id)
        {
            var entity = await _context.Pets.FirstOrDefaultAsync(p => p.Id == id);
            if (entity is null) return false;

            _context.Pets.Remove(entity);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public Task<bool> GetByName(string name)
        {
            return _context.Pets.AnyAsync(o => o.Name.ToLower() == name.ToLower());

        }

        public Task<bool> HasAppointments(Guid petId)
        {
            return _context.Pets.AnyAsync(o => o.Id == petId);

        }

        public Task<Pet?> GetByIdWithPet(Guid id)
        {
            return _context.Pets.AsNoTracking().Include(o => o.Appointments).FirstOrDefaultAsync(o => o.Id == id);

        }

        public Task<List<Pet>> GetAllWithAppointments()
        {
            return _context.Pets.AsNoTracking().Include(o => o.Appointments).ToListAsync();

        }
    }
}
