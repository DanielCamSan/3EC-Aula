using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dto
{
    public class CreateAppointmentDto
    {      
        public Guid PetId { get; init; }
            public DateTime ScheduledAt { get; init; }

            [Required, StringLength(100)]
            public string Reason { get; init; } = string.Empty;

        [Required, StringLength(100)]
        public string Status { get; init; } = string.Empty;
            public string? Notes { get; init; }
    
    }
}
