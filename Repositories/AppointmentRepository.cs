using FirstExam.Data;
using FirstExam.Models;
using FirstExam.Models.dtos;
using Microsoft.EntityFrameworkCore;
using System;


namespace FirstExam.Repositories
{
    public class AppointmentRepository:IAppointmentRepository
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
        public Task<bool> ExistsByOwnerId(Guid ownerId)
        {
            return _context.Appointments.AsNoTracking().AnyAsync(a => a.OwnerId == ownerId);
        }    

        public Task<bool> ExistsByPetId(Guid petId)
        {
            return _context.Appointments.AsNoTracking().AnyAsync(a => a.PetId == petId);
        }           

    }
}
