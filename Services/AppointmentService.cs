using FirstExam.Models.dtos;
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
        public async Task<Appointment> Create(CreateAppointmentDto dto)
        {
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PetId = dto.PetId,
                ScheduledAt = dto.ScheduledAt,
                Reason = dto.Reason,
                Status = dto.Status,
                Notes = dto.Notes
            };
            await _repo.Add(appointment);
            return appointment;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existing = _repo.GetById(id);
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
            var appointment = _repo.GetById(id);
            return await appointment;
        }

        public async Task<Appointment> Update(Guid id, UpdateAppointmentDto dto)
        {
            var a = await _repo.GetById(id);
            if (a == null) throw new Exception("Appointment not found");
            a.PetId = dto.PetId;
            a.ScheduledAt = dto.ScheduledAt;
            a.Reason = dto.Reason;
            a.Status = dto.Status;
            a.Notes = dto.Notes;
            await _repo.Update(a);
            return a;
        }
    }
}