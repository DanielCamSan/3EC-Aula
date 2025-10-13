using FirstExam.Models;
using FirstExam.Models.Dtos;

namespace FirstExam.Repositories
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAll();
        Task<Pet?> GetById(Guid id);
        Task Add(Pet pet);
        Task<bool> Delete(Guid id);
        Task<Pet?> Update(Guid id, UpdatePetDto dto);
        Task<bool> ExistsByName(string name);
        Task<bool> HasAppointments(Guid petId);

        Task<Pet?> GetByIdWithPet(Guid id);

        Task<bool> ExistsByNameExcludingId(string name, Guid excludeId);

        Task<List<Pet>> GetAllWithAppointments();

        Task<List<Pet>> GetAllWithOwners();

        Task<Pet?> GetByIdWithOwners(Guid id);
        Task Update(Pet current);
    }
}