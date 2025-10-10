using FirstExam.Models;
using FirstExam.Models.dtos;
using Microsoft.Extensions.Logging;

namespace FirstExam.Services
{
    public interface IOwnerService
    {
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);
        Task<Owner> Create(CreateOwnerDto dto);
        Task Update(Owner owner);
        Task Add(Owner owner);
        Task Delete(Guid id);
    }
}
