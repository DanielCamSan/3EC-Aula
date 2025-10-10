using System;
using System.ComponentModel.DataAnnotations;

namespace FirstExam.Dtos
{
    public class CreateAppointmentDto
    {
        public Guid PetId { get; set; }

        [Required]
        public DateTime ScheduledAt { get; set; }

        [Required, MinLength(3)]
        public string Reason { get; set; } = string.Empty;

        public string Status { get; set; } = "scheduled";
        public string? Notes { get; set; }
    }
}
