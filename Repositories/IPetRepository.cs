using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IPetRepository
    {
        Task <IEnumerable<Pet>> GetAll();

        Task<Pet?> GetById(Guid Id);

        Task Add(Pet pet);

        Task Delete(Guid Id);

        Task Update(Pet pet);
    }
}
