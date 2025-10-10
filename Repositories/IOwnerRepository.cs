using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IOwnerRepository
    {
        Task<List<Owner>> GetAllWithBooks();
        Task<Owner?> GetByIdWithBooks(Guid id);
        Task<bool> ExistsByName(string name);
        Task<bool> ExistsByNameExcludingId(string name, Guid excludeId);
        Task Add(Owner owner);
        Task Update(Owner owner);
        Task<bool> Delete(Guid id);   // false si no existe o FK bloquea
        Task<bool> HasBooks(Guid id); // para proteger borrado

        Task<bool> ExistsById(Guid id);
    }
}
