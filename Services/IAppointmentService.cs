using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstExam.Models;
using FirstExam.Models.DTO;

namespace FirstExam.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAll();
        Task<Appointment?> GetById(Guid id);
        Task<Appointment> Create(CreateAppointmentDto dto);
        Task<bool> Update(Guid id, UpdateAppointmentDto dto);
        Task<bool> Delete(Guid id);
    }
}
