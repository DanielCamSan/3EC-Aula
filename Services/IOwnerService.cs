using FirstExam.Models;
using FirstExam.Models.Dtos;
namespace FirstExam.Services
{
    public interface IOwnerService
    {
        Task<IEnumerable<OwnerListDto>> GetAll();
        Task<OwnerDetailsDto?> GetById(Guid id);
        Task<OwnerDetailsDto> Create(CreateOwnerDto dto);
        Task<OwnerDetailsDto?> Update(Guid id, UpdateOwnerDto dto);
        Task<bool> Delete(Guid id);
        
    }
}