using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record CreateAppointmentDto
    {
        public Guid PetId { get; init; }
        [Required]
        public Guid OwnerId { get; set; }
        public DateTime ScheduledAt { get; init; }
        [Required, StringLength(100)]
        public string Reason { get; init; }
        [Required, StringLength(100)]
        public string Status { get; init; }
        public string? Notes { get; init; }
    }
}
