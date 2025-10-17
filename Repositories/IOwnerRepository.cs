using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAllAsync();

        Task<Owner?> GetByIdAsync(Guid id);
        Task<Owner> CreateAsync(Owner owner);
        Task<Owner?> UpdateAsync(Owner owner);
        Task<bool> DeleteAsync(Guid id);
    }
}