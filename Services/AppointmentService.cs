using FirstExam.Models.Dtos.Appointments;
using FirstExam.Repositories;

namespace FirstExam.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        public AppointmentService(IAppointmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _repo.GetAll();            
        }

        public async Task<Appointment?> GetById(Guid id)
        {
            return await _repo.GetById(id);
        }

        public async Task<Appointment> Create(CreateAppointmentDto dto)
        {
            var reason = dto.Reason.Trim();
            if(reason is null)
                throw new InvalidOperationException("An appointment without Reason is indefined.");

            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PetId = Guid.NewGuid(),
                ScheduledAt = dto.ScheduledAt,
                Reason = reason,
                Status = dto.Status.Trim(),
                Notes = dto.Notes?.Trim()
            };
            await _repo.Add(appointment);
            return appointment; 
        }

        public async Task<Appointment?> Update(Guid id, UpdateAppointmentDto dto)
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
            return current; 
        }

        public async Task<bool> Delete(Guid id)
        {
            if (!await _repo.ExistsById(id)) return false;
            return await _repo.Delete(id);
        }
    }
}
