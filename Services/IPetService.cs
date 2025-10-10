namespace FirstExam.Services
{
    public interface IPetService
    {

        Task<IEnumerable<Pet>> GetAll();
        Task<Pet?> GetByid(int id);
        T

    }
}
