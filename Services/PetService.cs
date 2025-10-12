using FirstExam.Models;
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
            // Ejemplo de validación (puedes ajustar según tus reglas)
            if (dto.WeightKg < 0)
                throw new InvalidOperationException("El peso no puede ser negativo.");

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
            return await _repo.GetById(id);
        }
    }
}
