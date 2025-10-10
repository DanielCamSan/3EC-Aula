using FirstExam.Data;
using FirstExam.Models.dtos;
using Microsoft.EntityFrameworkCore;

namespace FirstExam.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Appointment book)
        {
            await _context.appointments.AddAsync(book);
        }

        public async Task Delete(Guid id)
        {
            var book = await _context.appointments.FirstOrDefaultAsync(x => x.Id == id);
            if (book != null)
            {
                _context.appointments.Remove(book);
            }
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _context.appointments.ToListAsync();
        }

        public async Task<Appointment?> GetById(Guid id)
        {
            return await _context.appointments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Appointment a)
        {
            _context.appointments.Update(a);
            await _context.SaveChangesAsync();
        }
    }
}
