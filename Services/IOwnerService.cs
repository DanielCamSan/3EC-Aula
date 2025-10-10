using FirstExam.Models;
using FirstExam.Models.dtos;

namespace FirstExam.Services
{
    public interface IOwnerService
    {
        Task<Owner> Create(CreateOwnerDto dto);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);

        Task<Owner?> Update(Guid id, UpdateOwnerDto dto);
    }
}
