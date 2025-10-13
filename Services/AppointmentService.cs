using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Repositories;
namespace FirstExam.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        private readonly IPetRepository _petRepo;
        private readonly IOwnerRepository _ownerRepo;
        public AppointmentService(IAppointmentRepository repo, IPetRepository petRepo, IOwnerRepository ownerRepo)
        {
            _repo = repo;
            _petRepo = petRepo;
            _ownerRepo = ownerRepo;
        }

        public async Task<Appointment> Create(CreateAppointmentDto dto)
        {
            var pet = await _petRepo.GetById(dto.PetId);
            if (pet == null)
                throw new InvalidOperationException("Pet not found");
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PetId = dto.PetId,
                OwnerId = pet.OwnerId,
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

        public async Task<Appointment?> Update(Guid id, UpdateAppointmentDto dto)
        {
            var current = await _repo.GetById(id);
            if (current == null) throw new InvalidOperationException("Appointment not found");

            if (current.PetId != dto.PetId)
            {
                var Pet = await _petRepo.GetById(dto.PetId);
                if (Pet == null)
                    throw new InvalidOperationException("New Pet ID not found.");

                current.PetId = dto.PetId;
                current.OwnerId = Pet.OwnerId;
            }
            current.ScheduledAt = dto.ScheduledAt;
            current.Reason = dto.Reason;
            current.Status = dto.Status;
            current.Notes = dto.Notes;

            await _repo.Update(current);
            return current;
        }
    }
}