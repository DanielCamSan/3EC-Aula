using static FirstExam.Service.AppointmentService;
using FirstExam.Models;
using FirstExam.Models.Dtos;
using FirstExam.Repository;
namespace FirstExam.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        public AppointmentService(IAppointmentService repo)
        {
            _repo = repo;
        }
        public async Task<Appointment> Create(CreateAppointmentDto dto)
        {
            if (dto.PetId == dto.PetId)
            {
                throw new InvalidOperationException("The id of the pet doesnt match with the appointment.");
            }
            var appointment = new Appointment
            {
                PetId = Guid.NewGuid(),
                ScheduledAt= DateTime.Now,
                Reason= dto.Reason.Trim(),
                Status= dto.Status.Trim(),
                Notes= dto.Notes?.Trim()
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
