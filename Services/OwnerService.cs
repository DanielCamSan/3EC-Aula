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

        public async Task<Owner?> Create(CreateOwnerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new InvalidOperationException("Email is required");

            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new InvalidOperationException("Full name is required");

            var owner = new Owner
            {
                Id = Guid.NewGuid(),
                Email = dto.Email.Trim(),
                FullName = dto.FullName.Trim(),
                Phone = dto.Phone?.Trim() ?? string.Empty,
                Active = true
            };

            await _repo.Create(owner);
            return owner;
        }

        public async Task Update(Guid id, UpdateOwnerDto dto)
        {
            var existing = await _repo.GetById(id);
            if (existing == null)
                throw new KeyNotFoundException("Owner not found");

            existing.FullName = dto.FullName?.Trim() ?? existing.FullName;
            existing.Email = dto.Email?.Trim() ?? existing.Email;
            existing.Phone = dto.Phone?.Trim() ?? existing.Phone;
            existing.Active = dto.Active;

            await _repo.Update(existing);
        }

        public async Task Delete(Guid id)
        {
            var existing = await _repo.GetById(id);
            if (existing == null)
                throw new KeyNotFoundException("Owner not found");

            await _repo.Delete(id);
        }
    }
}