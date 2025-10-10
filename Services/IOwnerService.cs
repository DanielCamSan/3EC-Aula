using FirstExam.Models;
using FirstExam.Models.DTO;

namespace FirstExam.Services
{
    public interface IOwnerService
    {
        Task<Owner> Create(CreateOwnerDto dto);
        Task<Owner?> Update(Guid id, UpdateOwnerDto dto);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);
    }
}
