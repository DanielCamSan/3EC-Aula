using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Repositories;
namespace FirstExam.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointments;
        private readonly IPetRepository _pets;
        private readonly IOwnerRepository _owners;

        public AppointmentService(IAppointmentRepository appointments, IPetRepository pets, IOwnerRepository owners)
        {
            _appointments = appointments;
            _pets = pets;
            _owners = owners;
        }


        public async Task<Appointment> Create(CreateAppointmentDto dto)
        {
            // 1) Verificar Pet
            var pet = await _pets.GetById(dto.PetId);
            if (pet is null)
                throw new KeyNotFoundException("Pet no existe.");

            // 2) Derivar Owner desde Pet
            var owner = await _owners.GetById(pet.OwnerId);
            if (owner is null)
                throw new InvalidOperationException("Owner del Pet no existe.");

            if (!owner.Active)
                throw new InvalidOperationException("El Owner está inactivo.");

            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PetId = pet.Id,
                OwnerId = owner.Id,
                ScheduledAt = dto.ScheduledAt,
                Reason = dto.Reason.Trim(),
                Status = "scheduled", // Siempre set por dominio
                Notes = string.IsNullOrWhiteSpace(dto.Notes) ? null : dto.Notes.Trim()
            };

            await _appointments.Add(appointment);
            return appointment;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existing = await _appointments.GetById(id);
            if (existing == null) return false;
            await _appointments.Delete(id);
            return true;

        }
        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _appointments.GetAll();
        }

        public async Task<Appointment?> GetById(Guid id)
        {
            var appointment = _appointments.GetById(id);
            return await appointment;
        }

        public async Task<Appointment?> Update(Guid id, UpdateAppointmentDto dto)
        {
            var existing = await _appointments.GetById(id);
            if (existing is null) return null;

            existing.ScheduledAt = dto.ScheduledAt ?? existing.ScheduledAt;
            existing.Reason = dto.Reason?.Trim() ?? existing.Reason;            
            existing.Notes = string.IsNullOrWhiteSpace(dto.Notes) ? null : dto.Notes.Trim();
            await _appointments.Update(existing);
            return existing;
        }
    }
}
