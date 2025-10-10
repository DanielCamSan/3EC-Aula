using apiwithdb.Models;

namespace apiwithdb.Repositories
{
    public interface IPetsRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book?> GetById(Guid id);
        Task Add(Book book);
        Task Delete(Guid id);
    }
}