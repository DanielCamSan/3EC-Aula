
using FirstExam.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace FirstExam.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;
        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async  Task Delete(Guid id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(x => x.Id == id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<Appointment?> GetById(Guid id)
        {
            return await _context.Appointments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Appointment appointment)
        {
           _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Appointment>> GetWithDetails()
        {
            return await _context.Appointments
                .Include(a => a.Pet)      // Incluir Pet
                .Include(a => a.Owner)    // Incluir Owner
                .ToListAsync();
        }
    }
}
