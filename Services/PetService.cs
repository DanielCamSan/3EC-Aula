using System;
using FirstExam.Models.Dtos;
using FirstExam.Repositories;
using System.Linq;
using FirstExam.Models;

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
                new List<Appointment1ListDto>(),
                new List<OwnerListsDto>()
            );
        }

        public async Task<bool> Delete(Guid id)
        {
            if (await _repo.HasAppointments(id)) return false;
            return await _repo.Delete(id);
        }

        public async Task<IEnumerable<PetsListDto>> GetAll()
        {
            var pets = await _repo.GetAllWithOwners();

            return pets.Select(p => new PetsListDto(
                p.Id,
                p.Name,
                p.Appointments?.Count() ?? 0,
                p.Owner != null ? 1 : 0
            ));
        }

        public async Task<PetsDetailsDto?> GetById(Guid id)
        {
            var pet = await _repo.GetByIdWithOwners(id);
            if (pet == null) return null;
            var pets = (pet.Owner != null ? new List<Owner> { pet.Owner } : new List<Owner>())
                .Select(o => new OwnerListsDto(
                    o.Id,
                    o.FullName,
                    o.Email,
                    o.Phone,
                    o.Active,
                    o.Appointments?.Count() ?? 0
                )).ToList();
            return new PetsDetailsDto(
                pet.Id,
                pet.Name,
                pet.Species,
                pet.Breed,
                pet.BirthDate,
                pet.WeightKg,
                pet.Sex,
                new List<Appointment1ListDto>(),
                new List<OwnerListsDto>(pets)
            );
        }

        public async Task<PetsDetailsDto?> Update(Guid id, UpdatePetDto dto)
        {
            var current = await _repo.GetByIdWithOwners(id);
            if (current == null) return null;

            var name = dto.Name.Trim();
            if (await _repo.ExistsByNameExcludingId(name, id))
                throw new InvalidOperationException("Pet with the same name already exists");
            current.Name = name;
            current.Species = dto.Species;
            current.Breed = dto.Breed;
            current.BirthDate = dto.BirthDate;
            current.WeightKg = dto.WeightKg;
            current.Sex = dto.Sex;
            current.OwnerId = dto.OwnerId;
            await _repo.Update(id, dto);
            var pets = new List<OwnerListsDto>();
            if (current.Owner != null)
            {
                pets.Add(new OwnerListsDto(
                    current.Owner.Id,
                    current.Owner.FullName,
                    current.Owner.Email,
                    current.Owner.Phone,
                    current.Owner.Active,
                    current.Owner.Appointments?.Count() ?? 0
                ));
            }
            return new PetsDetailsDto(
                current.Id,
                current.Name,
                current.Species,
                current.Breed,
                current.BirthDate,
                current.WeightKg,
                current.Sex,
                new List<Appointment1ListDto>(),
                new List<OwnerListsDto>(pets)
            );
        }
    }
}
