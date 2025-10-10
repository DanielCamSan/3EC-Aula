
using FirstExam.Data;
using FirstExam.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstExam.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _db;
        public AppointmentRepository(AppDbContext db) { _db = db; }

        public Task<List<Appointment>> GetAll() =>
            _db.Appointments.AsNoTracking().ToListAsync();

        public Task<Appointment?> GetById(Guid id) =>
            _db.Appointments.AsNoTracking()
               .FirstOrDefaultAsync(a => a.Id == id);

        public Task<List<Appointment>> GetByPetId(Guid petId) =>
            _db.Appointments.AsNoTracking()
               .Where(a => a.PetId == petId)
               .ToListAsync();

        public Task<List<Appointment>> GetByStatus(string status) =>
            _db.Appointments.AsNoTracking()
               .Where(a => a.Status.ToLower() == status.ToLower())
               .ToListAsync();

        public Task<bool> ExistsById(Guid id) =>
            _db.Appointments.AnyAsync(a => a.Id == id);

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
            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                // Posible violación por FK si hay relaciones dependientes
                return false;
            }
        }

        public async Task<bool> UpdateStatus(Guid id, string status)
        {
            var entity = await _db.Appointments.FirstOrDefaultAsync(a => a.Id == id);
            if (entity is null) return false;

            entity.Status = status;
            _db.Appointments.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
