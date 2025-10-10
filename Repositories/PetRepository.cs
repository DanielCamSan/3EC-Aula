using FirstExam.Models;
using FirstExam.Services;

namespace FirstExam.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly List<Pet> pets = new();

        public IEnumerable<Pet> GetAll() => pets;

        public Pet? GetById(Guid id) => pets.FirstOrDefault(p => p.Id == id);

        public void Add(Pet pet) => pets.Add(pet);

        public void Update(Pet pet)
        {
            var index = pets.FindIndex(p => p.Id == pet.Id);
            if (index != -1) pets[index] = pet;
        }

        public bool Delete(Guid id)
        {
            var removed = pets.RemoveAll(p => p.Id == id);
            return removed > 0;
        }
    }
}
