using FirstExam.Models.dtos;

namespace FirstExam.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentListDto>> GetAll();
        Task<AppointmentDetailDto?> GetById(Guid id);
        Task<AppointmentDetailDto> Create(CreateAppointmentDto dto);
        Task<AppointmentDetailDto?> Update(Guid id, UpdateAppointmentDto dto);
        Task<bool> Delete(Guid id); // false => no existe o está completada
        Task<IEnumerable<AppointmentListDto>> GetByPetId(Guid petId);
        Task<bool> UpdateStatus(Guid id, string status);
    }
}
