using FirstExam.Models.Dtos;


namespace FirstExam.Services
{
    public interface IAppointmentServices
    {

        Task<IEnumerable<Appointment>> GetAll();
        Task<Appointment?> GetById(Guid id);
        Task<Appointment> Create(CreateAppointmentDto dto);
        Task<bool> Delete(Guid id);
            
        Task<Appointment?> Update(Guid id, UpdateAppointmentDto dto);
    }
}
