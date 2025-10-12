using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public class UpdateAppointmentDto
    {
        public Guid PetId { get; init; }
        public DateTime ScheduledAt { get; init; }
        [Required, StringLength(100)]
        public string Reason { get; init; }
        [Required, StringLength(100)]
        public string Status { get; init; }
        public string? Notes { get; init; }
    }
}
