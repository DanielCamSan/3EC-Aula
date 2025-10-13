using FirstExam.DTOs;
using FirstExam.Models;
using FirstExam.Repositories;

namespace FirstExam.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository repository;

        public PetService(IPetRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<Pet?> GetByIdAsync(Guid id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<Pet> CreateAsync(CreatePetDto dto)
        {
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                OwnerId = dto.OwnerId,
                Name = dto.Name.Trim(),
                Species = dto.Species.Trim(),
                Breed = dto.Breed.Trim(),
                BirthDate = dto.BirthDate,
                sex = dto.sex.Trim(),
                WeightKg = dto.WeightKg
            };

            await repository.AddAsync(pet);
            return pet;
        }

        public async Task<Pet?> UpdateAsync(Guid id, UpdatePetDto dto)
        {
            var existing = await repository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.OwnerId = dto.OwnerId;
            existing.Name = dto.Name.Trim();
            existing.Species = dto.Species.Trim();
            existing.Breed = dto.Breed.Trim();
            existing.BirthDate = dto.BirthDate;
            existing.sex = dto.sex.Trim();
            existing.WeightKg = dto.WeightKg;

            await repository.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await repository.DeleteAsync(id);
        }
    }
}
