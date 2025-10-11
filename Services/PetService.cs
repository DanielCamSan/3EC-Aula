using FirstExam.Models;
using FirstExam.Models.Dtos;
using FirstExam.Repositories;

namespace FirstExam.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repo;
        public PetService(IPetRepository repo)
        {
            _repo = repo;
        }
        public async Task<Pet> Create(CreatePetDto dto)
        {
            if (dto.OwnerId == null){
                throw new InvalidOperationException("OwnerId no es valido");
            }
            var book = new Pet
            {
                Id = Guid.NewGuid(),
                Species = dto.Species,


                
            };
            await _repo.Add(book);
            return book;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existing = await _repo.GetById(id);
            if (existing == null) return false;
            await _repo.Delete(id);
            return true;
        }

        public async Task<IEnumerable<Pet>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Pet?> GetById(Guid id)
        {
            var book = await _repo.GetById(id);
            return book;
        }
    }
}
