using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record UpdateAppointmentDto
    {
        [Required]
        public Guid PetId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public DateTime ScheduledAt { get; set; }

        [Required, StringLength(100)]
        public string Reason { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Status { get; set; } = string.Empty;

        public string? Notes { get; set; }

    }
}