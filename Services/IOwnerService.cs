using FirstExam.Models;
using FirstExam.Models.DTO;

namespace FirstExam.Services
{
    public interface IOwnerService
    {
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);
        Task<Owner> Create(CreateOwnerDto dto);
        Task<Owner> Update (UpdateOwnerDto dto);
        Task<bool> Delete(Guid id);
    }
}
