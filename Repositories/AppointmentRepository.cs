using FirstExam.Data;
using FirstExam.Models;
using FirstExam.Models.dtos;
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
            await _context.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(x => x.Id == id);
            if (appointment != null)
            {
                _context.Remove(appointment);
            }
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _context.Appointments.AsNoTracking().Include(a => a.Pet).Include(a => a.Owner).ToListAsync();
        }
        public async Task<Appointment?> GetById(Guid id)
        {
            return await _context.Appointments.Include(a => a.Pet).Include(a => a.Owner).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Appointment a)
        {
            _context.Appointments.Update(a);
            await _context.SaveChangesAsync();
        }


    }
}