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

        public IEnumerable<Pet> GetAll() => repository.GetAll();

        public Pet? GetById(Guid id) => repository.GetById(id);

        public Pet Create(CreatePetDto dto)
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
            repository.Add(pet);
            return pet;
        }

        public Pet? Update(Guid id, UpdatePetDto dto)
        {
            var existing = repository.GetById(id);
            if (existing == null) return null;

            existing.OwnerId = dto.OwnerId;
            existing.Name = dto.Name.Trim();
            existing.Species = dto.Species.Trim();
            existing.Breed = dto.Breed.Trim();
            existing.BirthDate = dto.BirthDate;
            existing.sex = dto.sex.Trim();
            existing.WeightKg = dto.WeightKg;

            repository.Update(existing);
            return existing;
        }

        public bool Delete(Guid id) => repository.Delete(id);
    }
}
