using FirstExam.Models.DTO;

namespace FirstExam.Services
{
    public class PetService : IPetService
    {

        public async Task<Pet> Create(CreatePetDto dto)
        {
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                OwnerId = dto.OwnerId,
                Name = dto.Name.Trim(),
                Species = dto.Species.Trim(),
                Breed = dto.Breed.Trim(),
                BirthDate = dto.BirthDate,
                sex = dto.sex,
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

        public Task<IEnumerable<Pet>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Pet?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Pet> Update(UpdatePetDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
