using FirstExam.Models.dtos;
using FirstExam.Repositories;
using Microsoft.JSInterop.Infrastructure;

namespace FirstExam.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repo;

        private readonly IOwnerRepository _ownerRepo;
        public PetService(IPetRepository repo, IOwnerRepository ownerRepo)
        {
            _repo = repo;
            _ownerRepo = ownerRepo;
        }
        public async Task<Pet> Create(CreatePetDto dto)
        {
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                Breed = dto.Breed,
                sex = dto.sex,
                OwnerId = dto.OwnerId,
                Species = dto.Species,
                WeightKg = dto.WeightKg,
            };
            await _repo.Add(pet);
            return pet;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existing = await _repo.GetById(id);
            if (existing == null) return false;
            await _repo.Delete(id);
            return true;
        }

        public async Task<IEnumerable<Pet>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Pet?> GetById(Guid id)
        {
            var pet = await _repo.GetById(id);
            return pet;
        }

        public async Task<Pet?> Update(Guid id, UpdatePetDto dto)
        {
            var current = await _repo.GetById(id);
            if (current == null) throw new InvalidOperationException("Pet not found");
            current.OwnerId = dto.OwnerId;
            current.Name = dto.Name;
            current.BirthDate = dto.BirthDate;
            current.Breed = dto.Breed;
            current.Species = dto.Species;
            current.WeightKg = dto.WeightKg;
            current.sex = dto.sex;

            await _repo.Update(current);
            return current;
        }
    }
}