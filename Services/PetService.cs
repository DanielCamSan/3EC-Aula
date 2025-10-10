using FirstExam.Models.dtos;
using FirstExam.Repositories;
namespace FirstExam.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repo;
        public PetService(IPetRepository repo)
        {
            _repo= repo;
        }
        public async Task<Pet> Create(CreatePetDto dto)
        {
            var pet = new Pet
            {
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                Breed = dto.Breed,
                sex = dto.sex,
                OwnerId = dto.OwnerId,
                Species = dto.Species,
                WeightKg = dto.WeightKg 
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

        public async Task<Pet> Update(Guid id, UpdatePetDto dto)
        {
            var a = await _repo.GetById(id);
            if (a == null) throw new Exception("Pet not found ");
            a.Name = dto.Name;
            a.BirthDate = dto.BirthDate;
            a.Breed = dto.Breed;
            a.OwnerId = dto.OwnerId;
            a.Species = dto.Species;
            a.WeightKg = dto.WeightKg;
            a.sex = dto.sex;
            await _repo.Update(a);
            return a;
        }
    }
}
