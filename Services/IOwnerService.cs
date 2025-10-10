using FirstExam.Models;
using FirstExam.Models.dtos;
using Microsoft.Extensions.Logging;

namespace FirstExam.Services
{
    public interface IOwnerServices
    {
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);
        Task Create(CreateOwnerDto dto);
        Task Update(Guid id, UpdateOwnerDto owner);
        Task Delete(Guid id);
    }
}
