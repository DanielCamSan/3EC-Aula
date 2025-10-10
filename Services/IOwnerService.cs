using FirstExam.Models;
using FirstExam.Models.dtos;
using Microsoft.Extensions.Logging;

namespace FirstExam.Services
{
    public interface IOwnerService
    {
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);
        Task Create(Owner owner);
        Task Update(Owner owner);
        Task Delete(Guid id);
    }
}
