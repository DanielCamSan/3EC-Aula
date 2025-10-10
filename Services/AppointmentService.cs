using FirstExam.Models.dtos;
using FirstExam.Repositories;

namespace FirstExam.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
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
            await _repository.Add(appointment);
            return appointment;


        }

        public async Task<bool> Delete(Guid id)
        {
            var existingAppointment = await _repository.GetById(id);
            if (existingAppointment == null)
            {
                return false;
            }
            await _repository.Delete(id);
            return true;
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Appointment?> GetById(Guid id)
        {
            var appointment = await _repository.GetById(id);
            return appointment;
        }
    }
}
