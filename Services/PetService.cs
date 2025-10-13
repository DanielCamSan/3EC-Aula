using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Repositories;

namespace FirstExam.Services
{
    public class PetService: IPetService
    {
        private readonly IPetRepository _repo;
        private readonly IOwnerService _ownerService;
        public PetService(IPetRepository repo, IOwnerService ownerService)
        {
            _repo = repo;
            _ownerService = ownerService;  // ← INYECTAR
        }

        public async Task<Pet> Create(CreatePetDto dto)
        {
            var ownerExists = await _ownerService.Exists(dto.OwnerId);
            if (!ownerExists)
                return null;
            var pet = new Pet { Id = Guid.NewGuid(),OwnerId = Guid.NewGuid(), Name = dto.Name.Trim(), 
                Species = dto.Species.Trim(), Breed = dto.Breed , BirthDate = dto.BirthDate, sex = dto.sex, WeightKg = dto.WeightKg };
            await _repo.Add(pet);
            return pet;
        }
        public async Task<Pet?> Update(Guid id, UpdatePetDto dto)
        {
            var existingPet = await _repo.GetById(id);
            if (existingPet is null) return null;

            
            existingPet.Name = dto.Name?.Trim();
            existingPet.Species = dto.Species?.Trim();
            existingPet.Breed = dto.Breed;
            existingPet.BirthDate = dto.BirthDate;
            existingPet.sex = dto.sex;
            existingPet.WeightKg = dto.WeightKg;
            

            await _repo.Update(existingPet);
            return existingPet;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existing = await _repo.GetById(id);
            if (existing is null) return false;
            await _repo.Delete(id);
            return true;
        }

        public Task<IEnumerable<Pet>> GetAll() => _repo.GetAll();
        public Task<Pet?> GetById(Guid id) => _repo.GetById(id);

        public async Task<bool> Exists(Guid id)
        {
            var pet = await _repo.GetById(id);
            return pet != null;
        }
    }
}
