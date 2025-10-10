using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Repositories;

namespace FirstExam.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _repo;
        public OwnerService(IOwnerRepository repo)
        {
            _repo = repo;
        }
        public async Task<Owner> Create(CreateOwnerDto dto)
        {
            if (dto.Active == false) throw new InvalidOperationException("owner must be true");
            var owner = new Owner
            {
                Id = Guid.NewGuid(),
                Email = dto.Email.Trim(),
                FullName = dto.FullName.Trim(),
                Phone = dto.Phone.Trim(),
                Active = dto.Active
            };
            await _repo.Add(owner);
            return owner;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existing = await _repo.GetById(id);
            if (existing == null) return false;
            await _repo.Delete(id);
            return true;
        }

        public async Task<IEnumerable<Owner>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Owner?> GetById(Guid id)
        {
            return await _repo.GetById(id);
        }

        public async Task<UpdateOwnerDto?> Update(Guid id, UpdateOwnerDto dto)
        {
            var current = await _repo.GetById(id);
            if (current is null) return null;
            await _repo.Update(current);
            
            current.Email = dto.Email.Trim();
            current.FullName = dto.FullName.Trim();
            current.Phone = dto.Phone.Trim();
            current.Active = dto.Active;

            await _repo.Update(current);
            return new UpdateOwnerDto
            {
                Email = dto.Email.Trim(),
                FullName = dto.FullName.Trim(),
                Phone = dto.Phone.Trim(),
                Active = dto.Active,
            };
        }
    }
}
