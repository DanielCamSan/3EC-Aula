using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IPetRepository
    {
        IEnumerable<Pet> GetAll();
        Pet? GetById(Guid id);
        void Add(Pet pet);
        void Update(Pet pet);
        bool Delete(Guid id);
    }
}
