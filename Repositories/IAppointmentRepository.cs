using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAll();
        Task<Appointment?> GetById(Guid id);
        Task Add(Appointment appointment);
        Task Update(Appointment appointment);
        Task<bool> Delete(Guid id);
        Task<bool> ExistsById(Guid id);
        Task<bool> ExistsByReason(string reason);
        Task<bool> ExistsByReasonExcludingId(string reason, Guid excludeId);
    }
}