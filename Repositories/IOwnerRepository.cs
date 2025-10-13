using FirstExam.Models;
using FirstExam.Models.dtos;

namespace FirstExam.Repositories
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);
        Task Add(Owner owner);
        Task<Owner?> Update(Guid id, UpdateOwnerDto dto);
        Task Delete(Guid id);
    }
}