using FirstExam.Models.dtos;

namespace FirstExam.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAll();
        Task<Appointment?> GetById(Guid id);
        Task<Appointment> Create(CreateAppointmentDto dto);
        Task<bool> Delete(Guid id);
        Task<Appointment> Update(Guid id, UpdateAppointmentDto dto);
    }
}
