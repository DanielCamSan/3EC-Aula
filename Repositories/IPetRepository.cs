using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAll();
        Task<Pet?> GetById(Guid id);
        Task Add(Pet pet);
        Task<bool> Delete(Guid id);
        Task<Pet?> Update(Guid id, UpdatePetDto dto);
    }
}