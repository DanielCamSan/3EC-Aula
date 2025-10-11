using System.Runtime.Intrinsics.Arm;
using FirstExam.Models;
using FirstExam.Models.Dtos;
using FirstExam.Repositories;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
namespace FirstExam.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _repository;
        public OwnerService(IOwnerRepository repository)
        {
            _repository = repository;
        }

        public async Task<OwnerDetailsDto> Create(CreateOwnerDto dto)
        {
            var name = dto.FullName.Trim();
            if (await _repository.GetByName(name))
            {
                throw new InvalidOperationException("Owner with the same name already exists");
            }
            var owner = new Owner
            {
                Email = dto.Email,
                FullName = name,
                Phone = dto.Phone,
                Active = dto.Active
            };
            await _repository.Add(owner);
            return new OwnerDetailsDto(
                owner.Id,
                owner.Email,
                owner.FullName,
                owner.Phone,
                owner.Active,
                new List<AppointmentListDto>()
            );
        }
        public async Task<bool> Delete(Guid id)
        {
            if (await _repository.HasAppointments(id)) return false;
            return await _repository.Delete(id);
        }
        public async Task<OwnerDetailsDto?> Update(Guid id, UpdateOwnerDto dto)
        {
            var current = await _repository.GetByIdWithOwner(id);
            if (current is null) return null;
            var name = dto.FullName.Trim();
            if (await _repository.ExistsByNameExcludingId(name, id))
            {
                throw new InvalidOperationException("Owner with the same name already exists");
            }
            current.FullName = name;
            current.Email = dto.Email;
            current.Phone = dto.Phone;
            current.Active = dto.Active;
            await _repository.Update(current);
            var owners = (current.Appointments ?? new List<Appointment>())
                .OrderBy(a => a.ScheduledAt)
                .Select(a => new AppointmentListDto(
                    a.Id,
                    a.ScheduledAt,
                    a.Reason,
                    a.Status,
                    a.Notes
                )).ToList();
            return new OwnerDetailsDto(current.Id,
                current.Email,
                current.FullName,
                current.Phone,
                current.Active,
                owners
            );

        }
        public async Task<OwnerDetailsDto?> GetById(Guid id)
        {
            var a = await _repository.GetByIdWithOwner(id);
            if (a is null) return null;
            var owners = (a.Appointments ?? new List<Appointment>())
                .OrderBy(a => a.ScheduledAt)
                .Select(a => new AppointmentListDto(
                    a.Id,
                    a.ScheduledAt,
                    a.Reason,
                    a.Status,
                    a.Notes
                )).ToList();
            return new OwnerDetailsDto(a.Id, a.Email, a.FullName, a.Phone, a.Active, owners);

        }
        public async Task<IEnumerable<OwnerListDto>> GetAll()
        {
            var owners = await _repository.GetAllWithAppointments();
            return owners.Select(o => new OwnerListDto(
                o.Id,
                o.FullName,
                o.Appointments?.Count() ?? 0
            ));
        }
    }
};
