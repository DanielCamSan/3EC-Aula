using System;
using System.Collections.Generic;
using System.Linq;
using FirstExam.Models;

namespace FirstExam.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        // DB en memoria (semilla)
        private static readonly List<Appointment> _db = new()
        {
            new Appointment {
                Id = Guid.Parse("00000000-0000-0000-0000-0000000000A1"),
                PetId = Guid.Parse("00000000-0000-0000-0000-0000000000AA"),
                ScheduledAt = DateTime.UtcNow.AddDays(1),
                Reason = "vacunación",
                Status = "scheduled",
                Notes = "Primera dosis"
            },
            new Appointment {
                Id = Guid.Parse("00000000-0000-0000-0000-0000000000B2"),
                PetId = Guid.Parse("00000000-0000-0000-0000-0000000000BB"),
                ScheduledAt = DateTime.UtcNow.AddHours(8),
                Reason = "control",
                Status = "scheduled"
            }
        };

        public IEnumerable<Appointment> Query() => _db.AsEnumerable();
        public Appointment? Get(Guid id) => _db.FirstOrDefault(x => x.Id == id);

        public void Add(Appointment appt) => _db.Add(appt);

        public bool Update(Appointment appt)
        {
            var idx = _db.FindIndex(x => x.Id == appt.Id);
            if (idx < 0) return false;
            _db[idx] = appt;
            return true;
        }

        public bool Delete(Guid id) => _db.RemoveAll(x => x.Id == id) > 0;

        public int Count() => _db.Count;
    }
}
