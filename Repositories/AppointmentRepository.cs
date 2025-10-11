using apiwithdb.Data;
using apiwithdb.Models;
using Microsoft.EntityFrameworkCore;

namespace apiwithdb.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _db;
        public AppointmentRepository(AppDbContext db) => _db = db;

        public Task<List<Appointment>> GetAll() =>
            _db.Appointments.AsNoTracking().OrderBy(a => a.ScheduledAt).ToListAsync();

        public Task<Appointment?> GetById(Guid id) =>
            _db.Appointments.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

        public async Task Add(Appointment appointment)
        {
            await _db.Appointments.AddAsync(appointment);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Appointment appointment)
        {
            _db.Appointments.Update(appointment);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _db.Appointments.FirstOrDefaultAsync(a => a.Id == id);
            if (entity is null) return false;

            _db.Appointments.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<bool> ExistsById(Guid id) =>
            _db.Appointments.AnyAsync(a => a.Id == id);

        public Task<bool> ExistsByReason(string reason) =>
            _db.Appointments.AnyAsync(a => a.Reason.ToLower() == reason.ToLower());

        public Task<bool> ExistsByReasonExcludingId(string reason, Guid excludeId) =>
            _db.Appointments.AnyAsync(a => a.Id != excludeId && a.Reason.ToLower() == reason.ToLower());
    }
}