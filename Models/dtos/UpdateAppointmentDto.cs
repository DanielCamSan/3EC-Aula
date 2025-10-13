using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public class UpdateAppointmentDto
    {
        public Guid? PetId { get; init; }
        public DateTime? ScheduledAt { get; init; }
        public string? Reason { get; init; }
        public string? Notes { get; init; }
    }
}
