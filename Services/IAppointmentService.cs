using FirstExam.Models;
using static Appointment;

namespace FirstExam.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAsync(string? q, string? sort, string? order);
        Task<Appointment?> GetByIdAsync(Guid id);
        Task<Appointment> CreateAsync(CreateAppointmentDto dto);
        Task<Appointment?> UpdateAsync(Guid id, UpdateAppointmentDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}