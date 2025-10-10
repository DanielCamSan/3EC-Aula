using apiwithdb.Models;
using apiwithdb.Models.dtos;
using apiwithdb.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apiwithdb.Services
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
                Email = dto.Email,
                FullName = dto.FullName,
                Phone = dto.Phone,
                Active = dto.Active,
                Notes = dto.Notes
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

        public async Task<Owner> Update(Guid id, UpdateOwnerDto dto)
        {
            var owner = await _repo.GetById(id);
            if (owner == null) throw new Exception("Owner not found");

            if (dto.Email != null) owner.Email = dto.Email;
            if (dto.FullName != null) owner.FullName = dto.FullName;
            if (dto.Phone != null) owner.Phone = dto.Phone;
            if (dto.Active.HasValue) owner.Active = dto.Active.Value;
            if (dto.Notes != null) owner.Notes = dto.Notes;

            await _repo.Update(owner);
            return owner;
        }
    }
}
