using FirstExam.Models.dtos;
using FirstExam.Repositories;

namespace FirstExam.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repo;

        public PetService(IPetRepository repo)
        {
            _repo = repo;
        }
        public async Task<Pet> Create(CreatePetDto dto)
        {
            var pet = new Pet
            {
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                Breed = dto.Breed,
                OwnerId = dto.OwnerId,
                sex = dto.sex,
                Species = dto.Species,
                WeightKg = dto.WeightKg
            };
            await _repo.Add(pet);
            return pet;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existing = _repo.GetById(id);
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
            var pet = _repo.GetById(id);
            return await pet;
        }

        public async Task<Pet> Update(Guid id, UpdatePetDto dto)
        {
            var pet = new Pet
            {
                Id= id,
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                Breed = dto.Breed,
                OwnerId = dto.OwnerId,
                sex = dto.sex,
                Species = dto.Species,
                WeightKg = dto.WeightKg
            };
            await _repo.Update(id,pet);
            return pet;
        }
    }
}
