using FirstExam.Models;
using FirstExam.Models.Dtos;
using FirstExam.Repositories;
using System.Linq;

namespace FirstExam.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repo;
        public PetService(IPetRepository repo)
        {
            _repo = repo;
        }

        public async Task<PetsDetailsDto> Create(CreatePetDto dto)
        {
            var name = dto.Name.Trim();

            if (await _repo.ExistsByName(name))
                throw new InvalidOperationException("Pet with the same name already exists");

            var pet = new Pet
            {
                Id = dto.Id,
                Name = name,
                Species = dto.Species,
                Breed = dto.Breed,
                BirthDate = dto.BirthDate,
                WeightKg = dto.WeightKg,
                Sex = dto.Sex,
                OwnerId = dto.OwnerId
            };

            await _repo.Add(pet);

            return new PetsDetailsDto(
                pet.Id,
                pet.Name,
                pet.Species,
                pet.Breed,
                pet.BirthDate,
                pet.WeightKg,
                pet.Sex,
                new List<Appointment1ListDto>() 
            );
        }

        public async Task<bool> Delete(Guid id)
        {
            if (await _repo.HasAppointments(id)) return false;
            return await _repo.Delete(id);
        }

        public async Task<IEnumerable<PetsListDto>> GetAll()
        {
            var pets = await _repo.GetAll();

            return pets.Select(p => new PetsListDto(
                p.Id,
                p.Name,
                p.Appointments?.Count() ?? 0
            ));
        }

        public async Task<PetsDetailsDto?> GetById(Guid id)
        {
            var pet = await _repo.GetById(id);
            if (pet == null) return null;

            return new PetsDetailsDto(
                pet.Id,
                pet.Name,
                pet.Species,
                pet.Breed,
                pet.BirthDate,
                pet.WeightKg,
                pet.Sex,
                new List<Appointment1ListDto>()
            );
        }

        public async Task<PetsDetailsDto?> Update(Guid id, UpdatePetDto dto)
        {
            var updated = await _repo.Update(id, dto);
            if (updated == null) return null;

            return new PetsDetailsDto(
                updated.Id,
                updated.Name,
                updated.Species,
                updated.Breed,
                updated.BirthDate,
                updated.WeightKg,
                updated.Sex,
                new List<Appointment1ListDto>()
            );
        }
    }
}
