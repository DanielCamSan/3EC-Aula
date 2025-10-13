using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);
        Task Add(Owner owner);
        Task Update(Owner owner);
        Task Delete(Guid id);
        Task<bool> EmailExists(string email, Guid? excludeId = null);
        Task<bool> Exists(Guid id);
    }
}
