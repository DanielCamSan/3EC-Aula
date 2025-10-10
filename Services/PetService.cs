using FirstExam.Repositories;
using Microsoft.JSInterop.Infrastructure;

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
                Id = Guid.NewGuid(),
                OwnerId = dto.OwnerId,
                Name = dto.Name,
                Species = dto.Species,
                Breed = dto.Breed,
                BirthDate = dto.BirthDate,
                sex=dto.sex,
                WeightKg = dto.WeightKg!=null? dto.WeightKg:0,
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
            var pet = await _repo.Update(id, dto);
            return pet;
        }
    }
}
