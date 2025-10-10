using FirstExam.Models.dtos;
using static FirstExam.Models.dtos.OwnersDtos;

namespace FirstExam.Services
{
    public interface IOwnerService
    {
        Task<IEnumerable<OwnerListDto>> GetAll();
        Task<OwnerDetailDto?> GetById(Guid id);
        Task<CreateOwnerDto> Create(CreateOwnerDto dto);
        Task<CreateOwnerDto?> Update(Guid id, UpdateOwnerDto dto);
        Task<bool> Delete(Guid id); // false => no existe o tiene libros
    }
}