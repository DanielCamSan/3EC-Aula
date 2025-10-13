using FirstExam.Models;
using FirstExam.Models.dtos;
namespace FirstExam.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAll();
        Task<Appointment?> GetById(Guid id);
        Task Add(Appointment appointment);
        Task Delete(Guid id);
        Task Update(Appointment appointment);
        Task<bool> ExistsByOwnerId(Guid ownerId);
        Task<bool> ExistsByPetId(Guid petId);
    }
}
