using apiwithdb.Models;
using apiwithdb.Models.Dtos.Appointments;
using apiwithdb.Repositories;

namespace apiwithdb.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        public AppointmentService(IAppointmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<AppointmentListDto>> GetAll()
        {
            var list = await _repo.GetAll();
            return list.OrderBy(a => a.ScheduledAt)
                       .Select(a => new AppointmentListDto(
                           a.Id,
                           a.PetId,
                           a.ScheduledAt,
                           a.Reason,
                           a.Status
                       ));
        }

        public async Task<AppointmentDetailDto?> GetById(Guid id)
        {
            var a = await _repo.GetById(id);
            return a is null ? null : new AppointmentDetailDto(
                a.Id,
                a.PetId,
                a.ScheduledAt,
                a.Reason,
                a.Status,
                a.Notes
            );
        }

        public async Task<AppointmentDetailDto> Create(CreateAppointmentDto dto)
        {
            var reason = dto.Reason.Trim();
            if (await _repo.ExistsByReason(reason))
                throw new InvalidOperationException("An appointment with this reason already exists.");

            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PetId = dto.PetId,
                ScheduledAt = dto.ScheduledAt,
                Reason = reason,
                Status = dto.Status.Trim(),
                Notes = dto.Notes?.Trim()
            };

            await _repo.Add(appointment);

            return new AppointmentDetailDto(
                appointment.Id,
                appointment.PetId,
                appointment.ScheduledAt,
                appointment.Reason,
                appointment.Status,
                appointment.Notes
            );
        }

        public async Task<AppointmentDetailDto?> Update(Guid id, UpdateAppointmentDto dto)
        {
            var current = await _repo.GetById(id);
            if (current is null) return null;

            var reason = dto.Reason.Trim();
            if (await _repo.ExistsByReasonExcludingId(reason, id))
                throw new InvalidOperationException("Another appointment already uses that reason.");

            current.PetId = dto.PetId;
            current.ScheduledAt = dto.ScheduledAt;
            current.Reason = reason;
            current.Status = dto.Status.Trim();
            current.Notes = dto.Notes?.Trim();

            await _repo.Update(current);

            return new AppointmentDetailDto(
                current.Id,
                current.PetId,
                current.ScheduledAt,
                current.Reason,
                current.Status,
                current.Notes
            );
        }

        public async Task<bool> Delete(Guid id)
        {
            if (!await _repo.ExistsById(id)) return false;
            return await _repo.Delete(id);
        }
    }
}
