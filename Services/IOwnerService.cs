using FirstExam.Models;
using FirstExam.Models.dtos;

namespace FirstExam.Services
{
    public interface IOwnerService
    {
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);
        Task Update(Owner owner);
        Task Add(Owner owner);
        Task Delete(Guid id);
    }
}