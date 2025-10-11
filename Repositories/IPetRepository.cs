using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<Pet?> GetByIdAsync(Guid id);
        Task<Pet> CreateAsync(Pet pet);
        Task<Pet?> UpdateAsync(Guid id, Pet pet);
        Task<bool> DeleteAsync(Guid id);
    }
}