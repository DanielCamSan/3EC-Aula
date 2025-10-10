using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IOwnerRepository 
    {
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);
        Task Add(Owner guest);
        Task Delete(Guid id);

        //Update
        Task Update(Owner owner);
    }
}
