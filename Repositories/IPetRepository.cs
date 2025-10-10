using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<Pet?> GetByIdAsync(Guid id);
        Task AddAsync(Pet pet);
        Task UpdateAsync(Pet pet);
        Task<bool> DeleteAsync(Guid id);
    }
}
