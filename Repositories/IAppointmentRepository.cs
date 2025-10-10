using FirstExam.Models.dtos;

namespace FirstExam.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAll();
        Task<Appointment?> GetById(Guid id);
        Task Add(Appointment appointment);
        Task Delete(Guid id);
        Task Update(Appointment a);
    }
}
