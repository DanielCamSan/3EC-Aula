using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record CreateAppointmentDto
    {
        [Required]
        public Guid PetId { get; init; }

        [Required, DataType(DataType.DateTime)]
        public DateTime ScheduledAt { get; init; }

        [Required, StringLength(100)]
        public string Reason { get; init; } = string.Empty;
        public string? Notes { get; init; }
    }
}
