using System.Runtime.Intrinsics.Arm;
using FirstExam.Models;
using FirstExam.Models.Dtos;
using FirstExam.Repositories;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
namespace FirstExam.Services
{
    public class OwnerService: IOwnerService
    {
        private readonly IOwnerRepository _repository;
        public OwnerService(IOwnerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Owner> Create(CreateOwnerDto dto)
        {
            var owner = new Owner
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                FullName = dto.FullName,
                Phone = dto.Phone,
                Active = dto.Active
            };
            await _repository.Add(owner);
            return owner;
        }
        public async Task<bool> Delete(Guid id)
        {
            var existing = await _repository.GetById(id);
            if (existing == null) return false;
            await _repository.Delete(id);
            return true;
        }
        public Task<IEnumerable<Owner>> GetAll() => _repository.GetAll();
        public Task<Owner?> GetById(Guid id) => _repository.GetById(id);
        public async Task<Owner?> Update(Guid id, UpdateOwnerDto dto)
        {
            var existing = await _repository.GetById(id);
            if (existing == null) return null;
            existing.Email = dto.Email;
            existing.FullName = dto.FullName;
            existing.Phone = dto.Phone;
            existing.Active = dto.Active;
            await _repository.Update(existing);
            return existing;
        }
   }
}