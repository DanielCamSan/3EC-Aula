using FirstExam.Models;
using FirstExam.Models.dtos;

namespace FirstExam.Services
{
    public interface IPetService
    {
        Task<IEnumerable<Pet>> GetAllAsync(string? q, string? sort, string? order);
        Task<Pet?> GetByIdAsync(Guid id);
        Task<Pet> CreateAsync(CreatePetDto dto);
        Task<Pet?> UpdateAsync(Guid id, UpdatePetDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}