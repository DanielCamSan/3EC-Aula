using System;
using System.Collections.Generic;
using FirstExam.Models;

namespace FirstExam.Repositories
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> Query();                 
        Appointment? Get(Guid id);
        void Add(Appointment appt);
        bool Update(Appointment appt);
        bool Delete(Guid id);
        int Count();
    }
}
