using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record CreateAppointmentDto
    {
        [Required]
        public Guid PetId { get; init; }

        [Required]
        public DateTime ScheduledAt { get; init; }

        [Required, StringLength(100)]
        public string Reason { get; init; } = string.Empty;

        [Required, StringLength(100)]
        public string Status { get; init; } = "scheduled";

        public string? Notes { get; init; }
    }
}