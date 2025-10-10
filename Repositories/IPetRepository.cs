using apiwithdb.Models;

namespace apiwithdb.Repositories
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAll();
        Task<Pet?> GetById(Guid id);
        Task Add(Pet book);
        Task Delete(Guid id);
    }
}