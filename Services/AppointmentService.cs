using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Repositories;
using System.Reflection;

namespace FirstExam.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IPetRepository _petRepository;
        private readonly IOwnerRepository _ownerRepository;

        public AppointmentService(IAppointmentRepository repository, IPetRepository petRepository, IOwnerRepository ownerRepository)
        {
            _repository = repository;
            _petRepository = petRepository;
            _ownerRepository = ownerRepository;
        }

        private static IEnumerable<T> OrderByProp<T>(IEnumerable<T> src, string? sort, string? order)
        {
            if (string.IsNullOrEmpty(sort)) return src;
            var prop = typeof(T).GetProperty(sort, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null) return src;

            return string.Equals(order, "desc", StringComparison.OrdinalIgnoreCase)
                ? src.OrderByDescending(x => prop.GetValue(x))
                : src.OrderBy(x => prop.GetValue(x));
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync(string? q, string? sort, string? order)
        {
            var appointments = await _repository.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(q))
            {
                appointments = appointments.Where(a => a.Reason.Contains(q, StringComparison.OrdinalIgnoreCase));
            }
            return OrderByProp(appointments, sort, order);
        }

        public async Task<Appointment?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Appointment> CreateAsync(CreateAppointmentDto dto)
        {
            var ownerExists = await _ownerRepository.GetByIdAsync(dto.OwnerId);
            var petExists = await _petRepository.GetByIdAsync(dto.PetId);

            if (ownerExists is null || petExists is null)
            {
                throw new InvalidOperationException("El dueño o la mascota especificados no existen.");
            }
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PetId = dto.PetId,
                OwnerId = dto.OwnerId,
                ScheduledAt = dto.ScheduledAt,
                Reason = dto.Reason.Trim(),
                Notes = dto.Notes?.Trim(),
                Status = dto.Status.Trim()
            };
            return await _repository.CreateAsync(appointment);
        }

        public async Task<Appointment?> UpdateAsync(Guid id, UpdateAppointmentDto dto)
        {
            var ownerExists = await _ownerRepository.GetByIdAsync(dto.OwnerId);
            var petExists = await _petRepository.GetByIdAsync(dto.PetId);

            if (ownerExists is null|| petExists is null)
            {
                throw new InvalidOperationException("El dueño o la mascota especificados no existen.");
            }
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null) return null;

            existing.PetId = dto.PetId;
            existing.OwnerId = dto.OwnerId;
            existing.ScheduledAt = dto.ScheduledAt;
            existing.Reason = dto.Reason.Trim();
            existing.Status = dto.Status.Trim();
            existing.Notes = dto.Notes?.Trim();

            return await _repository.UpdateAsync(id, existing);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}