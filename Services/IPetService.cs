using apiwithdb.Models;
using apiwithdb.Models.dtos;

namespace apiwithdb.Services
{
    public interface IPetService
    {
        Task<IEnumerable<Pet>> GetAll();
        Task<Pet?> GetById(Guid id);
        Task<Pet> Create(CreatePetDto dto);
        Task<bool> Delete(Guid id);
    }
}