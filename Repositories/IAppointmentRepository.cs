namespace FirstExam.Repositories
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAll();
        Task<Appointment?> GetById(Guid id);
        Task<List<Appointment>> GetByOwnerId(Guid ownerId);
        Task<List<Appointment>> GetByStatus(string status);
        Task<bool> ExistsById(Guid id);
        Task Add(Appointment appointment);
        Task Update(Appointment appointment);
        Task<bool> Delete(Guid id);
        Task<bool> UpdateStatus(Guid id, string status);
    }
}