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

        public async Task<IEnumerable<Pet>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Pet?> GetById(Guid id)
        {
            var book = await _repo.GetById(id);
            return book;
        }

        public async Task<Pet?> Update(UpdatePetDto dto)
        {
            var existing = await _repo.GetById(id);
            if (existing == null) return null;

            existing.OwnerId = dto.OwnerId;
            existing.Name = dto.Name.Trim();
            existing.Species = dto.Species.Trim();
            existing.Breed = dto.Breed.Trim();
            existing.BirthDate = dto.BirthDate;
            existing.sex = dto.sex.Trim();
            existing.WeightKg = dto.WeightKg;

            await _repo.Update(existing);
            return existing;
        }
    }
}
