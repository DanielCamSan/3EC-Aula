using FirstExam.Models;
using System.Collections.Concurrent;

namespace FirstExam.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private static readonly List<Appointment> _appointments = new()
        {
            new Appointment() { Id = Guid.NewGuid(), PetId = Guid.NewGuid(), ScheduledAt = DateTime.Now , Reason = "a", Notes = "a"},
            new Appointment() { Id = Guid.NewGuid(), PetId = Guid.NewGuid(), ScheduledAt = DateTime.Now , Reason = "b", Notes = "b"}
        };

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await Task.FromResult(_appointments);
        }

        public async Task<Appointment?> GetByIdAsync(Guid id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            return await Task.FromResult(appointment);
        }

        public async Task<Appointment> CreateAsync(Appointment appointment)
        {
            _appointments.Add(appointment);
            return await Task.FromResult(appointment);
        }

        public async Task<Appointment?> UpdateAsync(Guid id, Appointment appointment)
        {
            var index = _appointments.FindIndex(a => a.Id == id);
            if (index == -1)
            {
                return null;
            }
            _appointments[index] = appointment;
            return await Task.FromResult(appointment);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var removedCount = _appointments.RemoveAll(a => a.Id == id);
            return await Task.FromResult(removedCount > 0);
        }
    }
}