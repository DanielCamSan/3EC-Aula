using FirstExam.Models;
using FirstExam.Models.dtos;

namespace FirstExam.Services
{
    public interface IOwnerService
    {
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);
        Task<Owner> Create(CreateOwnerDto dto);
        Task<bool> Delete(Guid id);
        Task<UpdateOwnerDto?> Update(Guid id, UpdateOwnerDto dto); 
    }
}
