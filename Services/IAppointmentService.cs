using FirstExam.Models;
using FirstExam.Models.dtos;

namespace FirstExam.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAll(string? q, string? sort, string? order);
        Task<Appointment?> GetById(Guid id);
        Task<Appointment> Create(CreateAppointmentDto dto);
        Task<Appointment?> Update(Guid id, UpdateAppointmentDto dto);
        Task<bool> Delete(Guid id);
    }
}