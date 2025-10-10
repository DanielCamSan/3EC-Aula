using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Repositories;
using static FirstExam.Models.dtos.OwnersDtos;

namespace FirstExam.Services
{
    public class OwnerService : IOwnerService
    {
        public Task<CreateOwnerDto> Create(CreateOwnerDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OwnerListDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OwnerDetailDto?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CreateOwnerDto?> Update(Guid id, UpdateOwnerDto dto)
        {
            throw new NotImplementedException();
        }
    }
}