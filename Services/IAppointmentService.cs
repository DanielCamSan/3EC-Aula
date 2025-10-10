using System;
using System.Collections.Generic;
using FirstExam.Models;

namespace FirstExam.Services
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> List(string? sort, string? order, int page, int limit, out int total);
        Appointment? Get(Guid id);
        Appointment Create(Appointment model);
        bool Update(Guid id, Appointment updated);
        bool Delete(Guid id);
    }
}
