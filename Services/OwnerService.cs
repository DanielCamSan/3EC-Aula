using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Repositories;

namespace FirstExam.Services
{
    public class OwnerService:IOwnerService
    {
        private readonly IOwnerRepository _owners;
        private readonly IPetRepository _pets; 
        private readonly IAppointmentRepository _appointments; 
        public OwnerService(IOwnerRepository owners, IPetRepository pets, IAppointmentRepository appointments)
        {
            _owners = owners;
            _pets = pets;
            _appointments = appointments;
        }


        public async Task<Owner> Create(CreateOwnerDto dto)
        {
            var email = dto.Email.Trim().ToLowerInvariant();

            var emailInUse = await _owners.ExistsByEmail(email);
            var owner = new Owner
            {
                Id = Guid.NewGuid(),
                Email = email,
                FullName = dto.FullName.Trim(),
                Phone = dto.Phone,
                Active=true,
            };
            await _owners.Add(owner);
            return owner;
        }
        public async Task<bool> Delete(Guid id)
        {
            var existing = await _owners.GetById(id);
            if (existing is null) return false;

            var hasPet = await _pets.ExistsByOwnerId(id);
            if (hasPet)
                throw new InvalidOperationException("No se puede eliminar: el Owner tiene una mascota asociada.");

            var hasAppointments = await _appointments.ExistsByOwnerId(id);
            if (hasAppointments)
                throw new InvalidOperationException("No se puede eliminar: el Owner tiene citas asociadas.");

            await _owners.Delete(id);
            return true;
        }
        public async Task<IEnumerable<Owner>> GetAll()
        {
            return await _owners.GetAll();
        }

        public async Task<Owner?> GetById(Guid id)
        {
            var owner = await _owners.GetById(id);
            return owner;
        }
        public async Task<Owner?> Update(Guid id, UpdateOwnerDto dto)
        {
            var existing = await _owners.GetById(id);
            if (existing is null) return null;

            var newEmail = dto.Email.Trim().ToLowerInvariant();
            if (!string.Equals(existing.Email, newEmail, StringComparison.OrdinalIgnoreCase))
            {
                var emailInUse = await _owners.ExistsByEmail(newEmail);
                if (emailInUse)
                    throw new InvalidOperationException("Email ya registrado por otro usuario.");
                existing.Email = newEmail;
            }

            existing.FullName = dto.FullName?.Trim()??existing.FullName;
            existing.Phone = dto.Phone?.Trim() ?? existing.Phone;
            existing.Active = dto.Active??existing.Active;

            await _owners.Update(existing);
            return existing;
        }
    }
}
