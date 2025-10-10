using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record CreateAppointmentDto
    {
        public Guid PetId { get; set; }
        [Required]
        public DateTime ScheduledAt { get; set; } = DateTime.Now;
        [Required, StringLength(100)]
        public string Reason { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string Status { get; set; } = "scheduled";
        public string? Notes { get; set; }
    }
}
