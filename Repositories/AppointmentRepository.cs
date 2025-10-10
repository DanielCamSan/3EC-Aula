using FirstExam.Data;
using FirstExam.Models;
using FirstExam.Models.dtos;
using Microsoft.EntityFrameworkCore;
using System;


namespace FirstExam.Repositories
{
    public class AppointmentRepository
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

        public async Task<Appointment?> Update(Guid id, UpdateAppointmentDto dto)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(x => x.Id == id);

            if (appointment == null) return null;

            appointment = new Appointment
            {
                PetId = dto.PetId,
                ScheduledAt = dto.ScheduledAt,
                Reason = dto.Reason,
                Status = dto.Status,
                Notes = dto.Notes != null ?dto.Notes: "",
            };

            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }


    }
}
