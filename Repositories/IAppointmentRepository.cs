using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAll();
        Task<Appointment?> GetById(Guid id);
        Task<Appointment> Create(Appointment appointment);
        Task<Appointment?> Update(Guid id, Appointment appointment);
        Task<bool> Delete(Guid id);
    }
}