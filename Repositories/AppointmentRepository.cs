using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstExam.Models;
using FirstExam.Repositories;

namespace FirstExam.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private static readonly List<Appointment> _db = new()
        {
            new Appointment { Id = Guid.Parse("00000000-0000-0000-0000-0000000000A1"),
                              PetId = Guid.Parse("00000000-0000-0000-0000-0000000000AA"),
                              ScheduledAt = DateTime.UtcNow.AddDays(1),
                              Reason = "vacunación", Status = "scheduled", Notes = "Primera dosis" },
            new Appointment { Id = Guid.Parse("00000000-0000-0000-0000-0000000000B2"),
                              PetId = Guid.Parse("00000000-0000-0000-0000-0000000000BB"),
                              ScheduledAt = DateTime.UtcNow.AddHours(8),
                              Reason = "control", Status = "scheduled" }
        };

        public Task<IEnumerable<Appointment>> GetAll()
            => Task.FromResult(_db.AsEnumerable());

        public Task<Appointment?> GetById(Guid id)
            => Task.FromResult(_db.FirstOrDefault(x => x.Id == id));

        public Task Add(Appointment appt) { _db.Add(appt); return Task.CompletedTask; }

        public Task<bool> Update(Appointment appt)
        {
            var i = _db.FindIndex(x => x.Id == appt.Id);
            if (i < 0) return Task.FromResult(false);
            _db[i] = appt;
            return Task.FromResult(true);
        }

        public Task<bool> Delete(Guid id)
            => Task.FromResult(_db.RemoveAll(x => x.Id == id) > 0);
    }
}
