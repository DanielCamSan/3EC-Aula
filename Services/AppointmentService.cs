using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Repositories;

namespace FirstExam.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        public AppointmentService(IAppointmentRepository repo) { _repo = repo; }

        public async Task<IEnumerable<object>> GetAll()
        {
            var appointments = await _repo.GetAll();
            return appointments
                .OrderBy(a => a.ScheduledAt)
                .Select(a => new
                {
                    a.Id,
                    a.PetId,
                    a.ScheduledAt,
                    a.Reason,
                    a.Status
                });
        }

        public async Task<object?> GetById(Guid id)
        {
            var appointment = await _repo.GetById(id);
            if (appointment is null) return null;

            return new
            {
                appointment.Id,
                appointment.PetId,
                appointment.ScheduledAt,
                appointment.Reason,
                appointment.Status,
                appointment.Notes
            };
        }

        public async Task<object> Create(CreateAppointmentDto dto)
        {
            if (dto.ScheduledAt < DateTime.Now)
                throw new InvalidOperationException("Appointment cannot be scheduled in the past.");

            var validStatuses = new[] { "scheduled", "confirmed", "cancelled", "completed" };
            if (!validStatuses.Contains(dto.Status.ToLower()))
                throw new InvalidOperationException("Invalid appointment status.");

            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PetId = dto.PetId,
                ScheduledAt = dto.ScheduledAt,
                Reason = dto.Reason.Trim(),
                Status = dto.Status.ToLower(),
                Notes = dto.Notes?.Trim()
            };

            await _repo.Add(appointment);

            return new
            {
                appointment.Id,
                appointment.PetId,
                appointment.ScheduledAt,
                appointment.Reason,
                appointment.Status,
                appointment.Notes
            };
        }

        public async Task<object?> Update(Guid id, UpdateAppointmentDto dto)
        {
            var current = await _repo.GetById(id);
            if (current is null) return null;

            if (dto.ScheduledAt < DateTime.Now)
                throw new InvalidOperationException("Appointment cannot be scheduled in the past.");

            var validStatuses = new[] { "scheduled", "confirmed", "cancelled", "completed" };
            if (!validStatuses.Contains(dto.Status.ToLower()))
                throw new InvalidOperationException("Invalid appointment status.");

            current.PetId = dto.PetId;
            current.ScheduledAt = dto.ScheduledAt;
            current.Reason = dto.Reason.Trim();
            current.Status = dto.Status.ToLower();
            current.Notes = dto.Notes?.Trim();

            await _repo.Update(current);

            return new
            {
                current.Id,
                current.PetId,
                current.ScheduledAt,
                current.Reason,
                current.Status,
                current.Notes
            };
        }

        public async Task<bool> Delete(Guid id)
        {
            var appointment = await _repo.GetById(id);
            if (appointment?.Status == "completed")
                return false;

            return await _repo.Delete(id);
        }

        public async Task<IEnumerable<object>> GetByPetId(Guid petId)
        {
            var appointments = await _repo.GetByPetId(petId);
            return appointments
                .OrderBy(a => a.ScheduledAt)
                .Select(a => new
                {
                    a.Id,
                    a.PetId,
                    a.ScheduledAt,
                    a.Reason,
                    a.Status
                });
        }

        public async Task<bool> UpdateStatus(Guid id, string status)
        {
            var validStatuses = new[] { "scheduled", "confirmed", "cancelled", "completed" };
            if (!validStatuses.Contains(status.ToLower()))
                throw new InvalidOperationException("Invalid appointment status.");

            return await _repo.UpdateStatus(id, status.ToLower());
        }
    }
}