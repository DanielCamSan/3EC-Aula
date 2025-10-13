using FirstExam.Models;
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
                Reason = dto.Reason.Trim(),
                Status = dto.Status,
                Notes = dto.Notes,
                PetId = dto.PetId,
                ScheduledAt = dto.ScheduledAt
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
            var appointment = _repo.GetById(id);
            return await appointment;
        }

        public async Task<Appointment?> Update(Guid id, UpdateAppointmentDto appointment)
        {
            var _appointment = await _repo.Update(id, appointment);
            return _appointment;
        }
    }
}