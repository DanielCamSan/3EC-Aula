using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAll();
        Task<Appointment?> GetById(Guid id);
        Task Add(Appointment appt);
        Task<bool> Update(Appointment appt);
        Task<bool> Delete(Guid id);
    }
}
