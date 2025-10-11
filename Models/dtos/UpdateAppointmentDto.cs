using System;
using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.Dtos.Appointments
{
    public record UpdateAppointmentDto(
        [Required] Guid PetId,
        [Required] DateTime ScheduledAt,
        [Required, StringLength(100)] string Reason,
        [Required, StringLength(100)] string Status,
        string? Notes
    );
}
