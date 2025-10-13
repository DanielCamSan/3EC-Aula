using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record UpdateAppointmentDto
    {
        public Guid PetId { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime ScheduledAt { get; set; }
        [Required, StringLength(100)]
        public string Reason { get; set; }
        [Required, StringLength(100)]
        public string Status { get; set; }
        public string? Notes { get; set; }

    }
}