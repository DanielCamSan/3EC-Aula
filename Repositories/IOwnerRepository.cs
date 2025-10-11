using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);
        Task Add(Owner owner);
        Task Update(Owner owner);
        Task<bool> Delete(Guid id);
        Task<bool> GetByName(string name);
        Task<bool> HasAppointments(Guid ownerId);

        Task<Owner?> GetByIdWithOwner(Guid id);

        Task<bool> ExistsByNameExcludingId(string name, Guid excludeId);

        Task<List<Owner>> GetAllWithAppointments();
    }
}
  