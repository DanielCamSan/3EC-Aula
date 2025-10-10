using FirstExam.Models;
using FirstExam.Models.DTOs;
using FirstExam.Repository;
using static FirstExam.Services.AppointmentService;

namespace FirstExam.Services
{
    public class AppointmentService
    {
        public class BookService : IAppointmentService
        {
            private readonly IAppointmentRepository _repo;
            public BookService(IAppointmentRepository repo)
            {
                _repo = repo;
            }
            public async Task<Appointment> Create(CreateAppointmentDto dto)
            {
            
                var Appointment = new Appointment
                {
                    Id = Guid.NewGuid(),
                    PetId = dto.PetId,
                    ScheduledAt = dto.ScheduledAt,
                    Reason = dto.Reason,    
                    Status = dto.Status,
                    Notes = dto.Notes
                };
                await _repo.Add(Appointment);
                return Appointment;
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
                var Appointment = await _repo.GetById(id);
                return Appointment;
            }
        }
    }
}
