using FirstExam.Models.dtos;

namespace FirstExam.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<object>> GetAll();
        Task<object?> GetById(Guid id);
        Task<object> Create(CreateAppointmentDto dto);
        Task<object?> Update(Guid id, UpdateAppointmentDto dto);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<object>> GetByPetId(Guid petId);
        Task<bool> UpdateStatus(Guid id, string status);
    }
}