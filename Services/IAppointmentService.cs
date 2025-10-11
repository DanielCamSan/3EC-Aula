using apiwithdb.Models.Dtos.Appointments;

namespace apiwithdb.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentListDto>> GetAll();
        Task<AppointmentDetailDto?> GetById(Guid id);
        Task<AppointmentDetailDto> Create(CreateAppointmentDto dto);
        Task<AppointmentDetailDto?> Update(Guid id, UpdateAppointmentDto dto);
        Task<bool> Delete(Guid id);
    }
}
