using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record UpdateAppointmentDto
    {
        public Guid OwnerId { get; set; }
        public DateTime ScheduledAt { get; set; }
        [Required, StringLength(100)]
        public string Reason { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string Status { get; set; } = "scheduled";
        public string? Notes { get; set; }
    }
}