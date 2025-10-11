using FirstExam.Models;
using FirstExam.Models.dtos;

namespace FirstExam.Services
{
    public interface IOwnerService
    {
        Task<IEnumerable<Owner>> GetAllAsync(string? q, string? sort, string? order);
        Task<Owner?> GetByIdAsync(Guid id);
        Task<Owner> CreateAsync(CreateOwnerDto dto);
        Task<Owner?> UpdateAsync(Guid id, UpdateOwnerDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}