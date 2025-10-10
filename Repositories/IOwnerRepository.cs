using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IOwnerRepository
    {
        Task Add(Owner owner);
        Task Update(Owner owner);
        Task<bool> Delete(Guid id);   // false si no existe o FK bloquea

        Task<bool> ExistsById(Guid id);
    }
}
