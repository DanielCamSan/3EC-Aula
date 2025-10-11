using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Repositories;
using System.Reflection;

namespace FirstExam.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repository;

        public PetService(IPetRepository repository)
        {
            _repository = repository;
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

        public async Task<IEnumerable<Pet>> GetAllAsync(string? q, string? sort, string? order)
        {
            var pets = await _repository.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(q))
            {
                pets = pets.Where(p => p.Name.Contains(q, StringComparison.OrdinalIgnoreCase) || p.Species.Contains(q, StringComparison.OrdinalIgnoreCase) || p.Breed.Contains(q, StringComparison.OrdinalIgnoreCase));
            }
            return OrderByProp(pets, sort, order);
        }

        public async Task<Pet?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Pet> CreateAsync(CreatePetDto dto)
        {
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                OwnerId = dto.OwnerId,
                Name = dto.Name.Trim(),
                Species = dto.Species.Trim(),
                Breed = dto.Breed.Trim(),
                BirthDate = dto.BirthDate,
                sex = dto.sex.Trim(),
                WeightKg = dto.WeightKg
            };
            return await _repository.CreateAsync(pet);
        }

        public async Task<Pet?> UpdateAsync(Guid id, UpdatePetDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null) return null;

            existing.OwnerId = dto.OwnerId;
            existing.Name = dto.Name.Trim();
            existing.Species = dto.Species.Trim();
            existing.Breed = dto.Breed.Trim();
            existing.BirthDate = dto.BirthDate;
            existing.sex = dto.sex.Trim();
            existing.WeightKg = dto.WeightKg;

            return await _repository.UpdateAsync(id, existing);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}