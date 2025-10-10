using FirstExam.Models.dto;
using FirstExam.Repositories;

namespace FirstExam.Services
{
    public class AppointmentServices : IAppointmentServices
    {
        private readonly IAppointmentRepository _repo;
        public AppointmentServices(IAppointmentRepository repo)
        {
            _repo = repo;
        }
        public async Task<Appointment> Create(CreateAppointmentDto dto)
        {
            if (dto.ScheduledAt.Year < 1900)
            {
                throw new InvalidOperationException("Year must be between 1900 and the current year.");
            }
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                Reason = dto.Reason,
                ScheduledAt = dto.ScheduledAt,
                Status = dto.Status,
                Notes = dto.Notes,
                PetId = dto.PetId,
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
            var book = await _repo.GetById(id);
            return book;
        }
    }
}
