using FirstExam.Models.dtos;

namespace FirstExam.Repositories
{
    public interface IPetRepository
    {
        Task<List<Pet>> GetAll();
        Task<Pet?> GetById(Guid id);
        Task Add(Pet pet);
        Task<Pet?> Update(Guid id, UpdatePetDto dto);
        Task Delete(Guid id);
    }
}
