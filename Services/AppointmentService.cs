using FirstExam.Models.dtos;
using FirstExam.Repositories;

namespace FirstExam.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IPetService _petService;
        private readonly IOwnerService _ownerService;
        public AppointmentService(IAppointmentRepository repository, IPetService petService,
            IOwnerService ownerService)
        {
            _petService = petService;
            _ownerService = ownerService;
            _repository = repository;
        }
        public async Task<Appointment> Create(CreateAppointmentDto dto)
        {
            var petExists = await _petService.Exists(dto.PetId);
            if (!petExists)
                return null;

            // Validar que Owner existe
            var ownerExists = await _ownerService.Exists(dto.OwnerId);
            if (!ownerExists)
                return null;
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
        public async Task<bool> Update(Guid id, UpdateAppointmentDto dto)
        {
            var appointment = await _repository.GetById(id);
            if(appointment == null)
            {
                return false;
            }
            appointment.Reason = dto.Reason;
            appointment.Status  = dto.Status;
            appointment.Notes = dto.Notes;  
            appointment.ScheduledAt = dto.ScheduledAt;
            appointment.PetId = dto.PetId;
            await _repository.Update(appointment);
            return true;

        }
        public Task<List<Appointment>> GetWithDetails() => _repository.GetWithDetails();
    }
}
