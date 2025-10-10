using FirstExam.Models;
namespace FirstExam.Repository
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAll();
        Task<Appointment?> GetById(Guid id);
        Task Add(Appointment Appointment);
        Task Delete(Guid id);
    }
}
