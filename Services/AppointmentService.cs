using FirstExam.Services;
using FirstExam.Models;
using FirstExam.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstExam.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        public AppointmentService(IAppointmentRepository repo) { _repo = repo; }

        public IEnumerable<Appointment> List(string? sort, string? order, int page, int limit, out int total)
        {
            var data = _repo.Query();

            // Ordenamiento: sort = scheduledAt|reason|status|petId
            bool desc = string.Equals(order, "desc", StringComparison.OrdinalIgnoreCase);
            data = (sort?.ToLower()) switch
            {
                "scheduledat" => desc ? data.OrderByDescending(x => x.ScheduledAt) : data.OrderBy(x => x.ScheduledAt),
                "reason" => desc ? data.OrderByDescending(x => x.Reason) : data.OrderBy(x => x.Reason),
                "status" => desc ? data.OrderByDescending(x => x.Status) : data.OrderBy(x => x.Status),
                "petid" => desc ? data.OrderByDescending(x => x.PetId) : data.OrderBy(x => x.PetId),
                _ => desc ? data.OrderByDescending(x => x.Id) : data.OrderBy(x => x.Id)
            };

            total = data.Count();
            page = Math.Max(1, page);
            limit = Math.Clamp(limit, 1, 100);

            return data.Skip((page - 1) * limit).Take(limit);
        }

        public Appointment? Get(Guid id) => _repo.Get(id);

        public Appointment Create(Appointment model)
        {
            _repo.Add(model);
            return model;
        }

        public bool Update(Guid id, Appointment updated)
        {
            var current = _repo.Get(id);
            if (current is null) return false;

            current.PetId = updated.PetId;
            current.ScheduledAt = updated.ScheduledAt;
            current.Reason = updated.Reason;
            current.Status = updated.Status;
            current.Notes = updated.Notes;

            return _repo.Update(current);
        }

        public bool Delete(Guid id) => _repo.Delete(id);
    }
}
