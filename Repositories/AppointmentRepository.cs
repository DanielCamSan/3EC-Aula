using FirstExam.Data;
using FirstExam.Models;
using FirstExam.Repository;
using Microsoft.EntityFrameworkCore;

namespace FirstExam.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _context.Appointments
                .Include(a => a.Pet)
                .Include(a => a.Owner)
                .ToListAsync();
        }

        public async Task<Appointment?> GetById(Guid id)
        {
            return await _context.Appointments
                .Include(a => a.Pet)
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Appointment> Create(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment?> Update(Guid id, Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<bool> Delete(Guid id)
        {
            var appointment = await GetById(id);
            if (appointment == null) return false;

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}