using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Models.DTOs;
using FirstExam.Repositories;
using System.Reflection;

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

        public async Task<IEnumerable<Appointment>> GetAll(string? q, string? sort, string? order)
        {
            var appointments = await _repository.GetAll();
            if (!string.IsNullOrWhiteSpace(q))
            {
                appointments = appointments.Where(a => a.Reason.Contains(q, StringComparison.OrdinalIgnoreCase));
            }
            return OrderByProp(appointments, sort, order);
        }

        public async Task<Appointment?> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Appointment> Create(CreateAppointmentDto dto)
        {
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
            return await _repository.Create(appointment);
        }

        public async Task<Appointment?> Update(Guid id, UpdateAppointmentDto dto)
        {
            var existing = await _repository.GetById(id);
            if (existing is null) return null;

            existing.PetId = dto.PetId;
            existing.OwnerId = dto.OwnerId;
            existing.ScheduledAt = dto.ScheduledAt;
            existing.Reason = dto.Reason.Trim();
            existing.Status = dto.Status.Trim();
            existing.Notes = dto.Notes?.Trim();

            return await _repository.Update(id, existing);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}