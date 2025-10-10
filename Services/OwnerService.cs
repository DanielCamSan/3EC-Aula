using FirstExam.Models;
using FirstExam.Models.DTO;
using FirstExam.Repository;

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
            var owner = new Owner
            {
                Id = Guid.NewGuid(),
                Email = dto.Email.Trim(),
                FullName = dto.FullName.Trim(),
                Phone = dto.Phone.Trim(),
                Active = true
            };

            await _repo.Add(owner);
            return owner;
        }

        public async Task<Owner?> Update(Guid id, UpdateOwnerDto dto)
        {
            var existing = await _repo.GetById(id);
            if (existing == null) return null;

            existing.Email = dto.Email.Trim();
            existing.FullName = dto.FullName.Trim();
            existing.Phone = dto.Phone.Trim();
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

        public async Task<IEnumerable<Owner>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Owner?> GetById(Guid id)
        {
            var book = await _repo.GetById(id);
            return book;
        }
    }
}
