using FirstExam.Models;
using FirstExam.Models.DTOs;

namespace FirstExam.Services
{
    public interface IAppointmentService
    {
      
            Task<IEnumerable<Appointment>> GetAll();
            Task<Appointment?> GetById(Guid id);
            Task<Appointment> Create(CreateAppointmentDto dto);
            Task<bool> Delete(Guid id);
       
    }
}

