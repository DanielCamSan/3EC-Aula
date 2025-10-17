using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Repositories;
using System.Reflection;

namespace FirstExam.Services;
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _repository;
        private readonly IPetRepository _petRepo;

        public OwnerService(IOwnerRepository repository, IPetRepository petRepo )
        {
            _repository = repository;
            _petRepo = petRepo;
        }

        private static IEnumerable<T> OrderByProp<T>(IEnumerable<T> src, string? sort, string? order)
        {
            if (string.IsNullOrEmpty(sort)) return src;
            var prop = typeof(T).GetProperty(sort, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null) return src;

            return string.Equals(order, "desc", StringComparison.OrdinalIgnoreCase)
                ? src.OrderByDescending(x => prop.GetValue(x))
                : src.OrderBy(x => prop.GetValue(x));
        }

        public async Task<IEnumerable<Owner>> GetAllAsync(string? q, string? sort, string? order)
        {
            var owners = await _repository.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(q))
            {
                owners = owners.Where(o => o.FullName.Contains(q, StringComparison.OrdinalIgnoreCase) || o.Email.Contains(q, StringComparison.OrdinalIgnoreCase));
            }
            return OrderByProp(owners, sort, order);
        }

        public async Task<Owner?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Owner> CreateAsync(CreateOwnerDto dto)
        {
            var owner = new Owner
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName.Trim(),
                Email = dto.Email.Trim(),
                Phone = dto.Phone.Trim(),
                Active = dto.Active
            };
            return await _repository.CreateAsync(owner);
        }

        public async Task<Owner?> UpdateAsync(Guid id, UpdateOwnerDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null) return null;

            existing.FullName = dto.FullName.Trim();
            existing.Email = dto.Email.Trim();
            existing.Phone = dto.Phone.Trim();
            existing.Active = dto.Active;

            return await _repository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}