using FirstExam.Models.dto;
using FirstExam.Repositories;

namespace FirstExam.Services
{
    public class AppointmentServices : IAppointmentServices
    {
        private readonly IAppointmentRepository _repo;
        private readonly IOwnerRepository _ownerRepo;
        public AppointmentServices(IAppointmentRepository repo, IOwnerRepository owners)
        {
            _repo = repo;
            _ownerRepo = owners;
        }
        public async Task<Appointment> Create(CreateAppointmentDto dto)
        {
            if (dto.ScheduledAt.Year < 1900)
            {
                throw new InvalidOperationException("Year must be between 1900 and the current year.");
            }
            var ownerExists = await _ownerRepo.GetById(dto.OwnerId);
            if (ownerExists == null)
            {
                throw new InvalidOperationException("Owner does not exist.");
            }
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                Reason = dto.Reason,
                ScheduledAt = dto.ScheduledAt,
                Status = dto.Status,
                Notes = dto.Notes,
                PetId = dto.PetId,
                OwnerId = dto.OwnerId,

            };
            await _repo.Add(appointment);
            return appointment;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existing = await _repo.GetById(id);
            if (existing == null) return false;
            await _repo.Delete(id);
            return true;
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Appointment?> GetById(Guid id)
        {
            var owner = await _repo.GetById(id);
            return owner;
        }
        public async Task<Appointment?> Update(Guid id, UpdateAppointmentDto dto)
        {
            var existing = await _repo.GetById(id);
            if (existing == null) return null;
            var ownerExists = await _ownerRepo.GetById(dto.OwnerId);
            if (ownerExists == null)
            {
                throw new InvalidOperationException("Owner does not exist.");
            }
            existing.Reason = dto.Reason;
            existing.ScheduledAt = dto.ScheduledAt;
            existing.Status = dto.Status;
            existing.Notes = dto.Notes;
            existing.PetId = dto.PetId;
            existing.OwnerId = dto.OwnerId;
            await _repo.Update(existing);
            return existing;
        }
    }
}
