namespace FirstExam.Repositories
{
    public interface IPetRepository
    {
        Task<List<Pet>> GetAll();
        Task<Pet?> GetById(Guid id);
        Task Add(Pet pet);
        Task Update (Pet pet);
        Task Delete(Guid id);

        Task<bool> ExistsByOwnerId(Guid ownerId);
    }
}
