
using FirstExam.Models.Dtos;

namespace FirstExam.Services
{
    public interface IPetService
    {
        Task<IEnumerable<PetsListDto>> GetAll();
        Task<PetsDetailsDto?> GetById(Guid id);
        Task<PetsDetailsDto> Create(CreatePetDto dto);
        Task<bool> Delete(Guid id);
        Task<PetsDetailsDto?> Update(Guid id, UpdatePetDto dto);
    }
}