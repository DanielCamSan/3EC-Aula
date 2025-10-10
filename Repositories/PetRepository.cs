using FirstExam.Models;

namespace FirstExam.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly List<Pet> pets = new()
        {
            new Pet { Name="Perla", Species="cat", Breed="egipcio", sex="macho", WeightKg=10, BirthDate=DateTime.Now.AddMonths(-24) },
            new Pet { Name="Sadu", Species="dog", Breed="Shar-Pei", sex="macho", WeightKg=25, BirthDate=DateTime.Now.AddMonths(-60) },
            new Pet { Name="Fadu", Species="dog", Breed="american bully", sex="macho", WeightKg=30, BirthDate=DateTime.Now.AddMonths(-36) }
        };

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
