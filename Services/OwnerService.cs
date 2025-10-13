using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Repositories;

namespace FirstExam.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _repo;
        private readonly IPetRepository _petRepo;
        public OwnerService(IOwnerRepository repo, IPetRepository petRepo)
        {
            _repo = repo;
            _petRepo = petRepo;
        }

        public async Task<Owner> Create(CreateOwnerDto dto)
        {
            var owner = new Owner
            {
                Id = Guid.NewGuid(),
                Email = dto.Email.Trim(),
                FullName = dto.FullName.Trim(),
                Phone = dto.Phone,
                Active = dto.Active,
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
            var owner = await _repo.GetById(id);
            return owner;
        }
        public async Task<Owner?> Update(Guid id, UpdateOwnerDto dto)
        {
            var owner = await _repo.GetById(id);
            if (owner == null) throw new Exception("Owner not found");

            if (dto.Email != null) owner.Email = dto.Email;
            if (dto.FullName != null) owner.FullName = dto.FullName;
            if (dto.Phone != null) owner.Phone = dto.Phone;
            if (dto.Active) owner.Active = dto.Active;
            await _repo.Update(owner);
            return owner;
        }
    }
}