using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstExam.Models;
using FirstExam.Models.DTO;
using FirstExam.Repositories;

namespace FirstExam.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        public AppointmentService(IAppointmentRepository repo) { _repo = repo; }

        public Task<IEnumerable<Appointment>> GetAll() => _repo.GetAll();
        public Task<Appointment?> GetById(Guid id) => _repo.GetById(id);

        public async Task<Appointment> Create(CreateAppointmentDto dto)
        {
            if (dto.ScheduledAt == default) throw new InvalidOperationException("ScheduledAt is required.");
            if (string.IsNullOrWhiteSpace(dto.Reason)) throw new InvalidOperationException("Reason is required.");
            if (string.IsNullOrWhiteSpace(dto.Status)) throw new InvalidOperationException("Status is required.");

            var a = new Appointment
            {
                Id = Guid.NewGuid(),
                PetId = dto.PetId,
                OwnerId = dto.OwnerId,
                ScheduledAt = dto.ScheduledAt,
                Reason = dto.Reason.Trim(),
                Status = dto.Status.Trim(),
                Notes = dto.Notes?.Trim()
            };
            await _repo.Add(a);
            return a;
        }

        public async Task<bool> Update(Guid id, UpdateAppointmentDto dto)
        {
            var existing = await _repo.GetById(id);
            if (existing is null) return false;

            if (string.IsNullOrWhiteSpace(dto.Reason)) throw new InvalidOperationException("Reason is required.");
            if (string.IsNullOrWhiteSpace(dto.Status)) throw new InvalidOperationException("Status is required.");

            existing.PetId = dto.PetId;
            existing.OwnerId = dto.OwnerId;
            existing.ScheduledAt = dto.ScheduledAt;
            existing.Reason = dto.Reason.Trim();
            existing.Status = dto.Status.Trim();
            existing.Notes = dto.Notes?.Trim();

            return await _repo.Update(existing);
        }

        public async Task<bool> Delete(Guid id)
        {
            var existing = await _repo.GetById(id);
            if (existing is null) return false;
            return await _repo.Delete(id);
        }
    }
}
