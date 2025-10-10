using FirstExam.Models;
using FirstExam.Models.dtos;
using Microsoft.Extensions.Logging;

namespace FirstExam.Repository
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);
        Task Create(CreateOwnerDto dto);
        Task Update(Guid id, UpdateOwnerDto dto);
        Task Delete(Guid id);
    }
}
