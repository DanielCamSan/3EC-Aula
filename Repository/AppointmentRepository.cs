using FirstExam.Data;
using FirstExam.Models;

namespace FirstExam.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;
        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Appointment Appointment)
        {
            await _context.Books.AddAsync(Appointment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var Appointment = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (Appointment != null)
            {
                _context.Books.Remove(Appointment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Appointment?> GetById(Guid id)
        {
            return await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
