using FirstExam.Models;
using FirstExam.Repositories;
using System.Reflection;
using static Appointment;

namespace FirstExam.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
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
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PetId = dto.PetId,
                ScheduledAt = dto.ScheduledAt,
                Reason = dto.Reason.Trim(),
                Notes = dto.Notes?.Trim(),
                Status = dto.Status.Trim()
            };
            return await _repository.CreateAsync(appointment);
        }

        public async Task<Appointment?> UpdateAsync(Guid id, UpdateAppointmentDto dto)
        {
            var existingAppointment = await _repository.GetByIdAsync(id);
            if (existingAppointment is null) return null;

            var updated = new Appointment
            {
                Id = id,
                PetId = dto.PetId,
                ScheduledAt = dto.ScheduledAt,
                Reason = dto.Reason.Trim(),
                Status = dto.Status.Trim(),
                Notes = dto.Notes?.Trim()
            };

            return await _repository.UpdateAsync(id, updated);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}