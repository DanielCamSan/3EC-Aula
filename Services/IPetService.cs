using apiwithdb.Models;
using FirstExam.Models.Dtos;

namespace FirstExam.Services
{
    public interface IPetService
    {
        Task<IEnumerable<Pet>> GetAll();
        Task<Pet?> GetById(Guid id);
        Task<Pet> Create(CreatePetDto dto);
        Task<bool> Delete(Guid id);
    }
}