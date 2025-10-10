using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

namespace FirstExam.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _repo;
        public OwnerService(IOwnerRepository repo) => _repo = repo;

        public async Task<Owner> Create(CreateOwnerDto dto)
        {
            var owner = new Owner { 
                Id = Guid.NewGuid(), 
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                Active = dto.Active,
            };
            await _repo.Add(owner);
            return owner;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existing = await _repo.GetById(id);
            if (existing is null) return false;
            await _repo.Delete(id);
            return true;
        }

        public Task<IEnumerable<Owner>> GetAll() => _repo.GetAll();

        public Task<Owner?> GetById(Guid id) => _repo.GetById(id);

        public async Task<Owner?> Update(Guid id, UpdateOwnerDto dto)
        {
            var existing = await _repo.GetById(id);
            if (existing is null)
                return null;

            existing.FullName = dto.FullName;
            existing.Email = dto.Email;
            existing.Phone = dto.Phone;
            existing.Active = dto.Active;

            await _repo.Update(existing);
            return existing;
        }
    }
}
