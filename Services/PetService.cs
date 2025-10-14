using FirstExam.Repositories;
using Microsoft.JSInterop.Infrastructure;

namespace FirstExam.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _pets;
        private readonly IOwnerRepository _owners;

        public PetService(IPetRepository pets, IOwnerRepository owners)
        {
            _pets = pets;
            _owners = owners;
        }

        public async Task<Pet> Create(CreatePetDto dto)
        {
            // Validar Owner
            var owner = await _owners.GetById(dto.OwnerId);
            if (owner is null)
                throw new KeyNotFoundException("Owner no existe.");

            if (!owner.Active)
                throw new InvalidOperationException("Owner inactivo.");

            var alreadyHasPet = await _pets.ExistsByOwnerId(dto.OwnerId);
            if (alreadyHasPet)
                throw new InvalidOperationException("Este Owner ya tiene una mascota asignada.");

            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                OwnerId = dto.OwnerId,
                Name = dto.Name.Trim(),
                Species = dto.Species.Trim(),
                Breed = dto.Breed.Trim(),
                BirthDate = dto.BirthDate,
                Sex = dto.Sex.Trim(),
                WeightKg = dto.WeightKg // deja null si no envían
            };

            await _pets.Add(pet);
            return pet;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existing = await _pets.GetById(id);
            if (existing is null) return false;
            await _pets.Delete(id);
            return true;
        }

        public async Task<IEnumerable<Pet>> GetAll()
        {
            return await _pets.GetAll();
        }

        public async Task<Pet?> GetById(Guid id)
        {
            var pet = await _pets.GetById(id);
            return pet;
        }

        public async Task<Pet?> Update(Guid id, UpdatePetDto dto)
        {
            var existing = await _pets.GetById(id);
            if (existing is null) return null;

            // Si permites cambiar OwnerId en 1:1, valida que el nuevo Owner no tenga ya Pet
            if (dto.OwnerId.HasValue && dto.OwnerId.Value != existing.OwnerId)
            {
                var newOwner = await _owners.GetById(dto.OwnerId.Value)
                               ?? throw new KeyNotFoundException("Nuevo Owner no existe.");
                if (!newOwner.Active) throw new InvalidOperationException("Nuevo Owner inactivo.");

                var ownerHasPet = await _pets.ExistsByOwnerId(dto.OwnerId.Value);
                if (ownerHasPet)
                    throw new InvalidOperationException("El nuevo Owner ya tiene una mascota asignada.");

                existing.OwnerId = dto.OwnerId.Value;
            }

            existing.Name = dto.Name?.Trim() ?? existing.Name;
            existing.Species = dto.Species?.Trim() ?? existing.Species;
            existing.Breed = dto.Breed?.Trim() ?? existing.Breed;
            existing.BirthDate = dto.BirthDate ?? existing.BirthDate;
            existing.Sex = dto.Sex?.Trim() ?? existing.Sex;
            existing.WeightKg = dto.WeightKg ?? existing.WeightKg;

            await _pets.Update(existing);
            return existing;
        }
    }
}
