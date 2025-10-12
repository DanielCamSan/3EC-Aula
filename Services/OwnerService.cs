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

        public async Task<IEnumerable<Owner>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Owner?> GetById(Guid id)
        {
            return await _repo.GetById(id);
        }

        public async Task<Owner> Create(CreateOwnerDto dto)
        {
            if (await _repo.EmailExists(dto.Email))
                throw new InvalidOperationException("Email is already in use.");

            var owner = new Owner
            {
                Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id, 
                Email = dto.Email.Trim(),
                FullName = dto.FullName.Trim(),
                Phone = dto.Phone.Trim(),
                Active = dto.Active
            };

            await _repo.Add(owner);
            return owner;
        }

        public async Task<Owner?> Update(Guid id, UpdateOwnerDto dto)
        {
            var existing = await _repo.GetById(id);
            if (existing == null) return null;

            if (await _repo.EmailExists(dto.Email, excludeId: id))
                throw new InvalidOperationException("Email is already in use.");

            existing.Email = dto.Email.Trim();
            existing.FullName = dto.FullName.Trim();
            existing.Active = dto.Active;

            await _repo.Update(existing);
            return existing;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existing = await _repo.GetById(id);
            if (existing == null) return false;

            await _repo.Delete(id);
            return true;
        }
    }
}
