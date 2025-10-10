using FirstExam.Models.dtos;
using FirstExam.Repositories;
using FirstExam.Services;
using FirstExam.Models;

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
            var owner = new owner
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Active = dto.Active,
                Phone = dto.Phone
            };
            await _repo.Add(owner);
            return owner;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existing = _repo.GetById(id);
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
            var pet = _repo.GetById(id);
            return await owner;
        }

        public async Task<Owner> Update(Guid id, UpdateOwnerDto dto)
        {
            var pet = new Pet
            {
                Id = id,
                FullName = dto.FullName
                Email = dto.Email,
                Active = dto.Active,
                Phone = dto.Phone,
            };
            await _repo.Update(id, owner);
            return owner;
        }
    }
}