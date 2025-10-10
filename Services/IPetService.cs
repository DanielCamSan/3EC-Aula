using FirstExam.DTOs;
using FirstExam.Models;

namespace FirstExam.Services
{
    public interface IPetService
    {
        IEnumerable<Pet> GetAll();
        Pet? GetById(Guid id);
        Pet Create(CreatePetDto dto);
        Pet? Update(Guid id, UpdatePetDto dto);
        bool Delete(Guid id);
    }
}
