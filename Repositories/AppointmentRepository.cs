
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
            await _context.Appointment.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var appointment = await _context.Appointment.FirstOrDefaultAsync(x => x.Id == id);
            if (appointment != null)
            {
                _context.Appointment.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _context.Appointment.ToListAsync();
        }

        public async Task<Appointment?> GetById(Guid id)
        {
            return await _context.Appointment.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
