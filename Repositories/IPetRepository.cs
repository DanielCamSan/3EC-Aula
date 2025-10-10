using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAll();
        Task<Pet?> GetById(Guid id);
        Task Add(Pet pet);
        Task Update(Pet pet);
        Task Delete(Guid id);
    }
}
