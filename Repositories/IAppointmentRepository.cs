using FirstExam.Models;
namespace FirstExam.Repositories
{
    public interface IAppointmentRepositoy
    {

        Task<IEnumerable<Appointment>> GetAll();
        Task<Appointment?> GetById(Guid id);
        Task Add(Appointment appointment);
        Task Delete(Guid id);
    }
}
