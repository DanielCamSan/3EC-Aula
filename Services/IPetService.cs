using FirstExam.DTOs;
using FirstExam.Models;

namespace FirstExam.Services
{
    public interface IPetService
    {
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<Pet?> GetByIdAsync(Guid id);
        Task<Pet> CreateAsync(CreatePetDto dto);
        Task<Pet?> UpdateAsync(Guid id, UpdatePetDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
