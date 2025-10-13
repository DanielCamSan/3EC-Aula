using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.Dtos
{
    public class CreateAppointmentDto
    {

        public Guid id { get; set; } = Guid.NewGuid();
        public Guid PetId { get; init; } = Guid.NewGuid();
        public DateTime ScheduledAt { get; init; }

        [Required, StringLength(100)]
        public string Reason { get; init; } = string.Empty;

        [Required, StringLength(100)]
        public string Status { get; init; } = string.Empty;
        public string? Notes { get; init; }

        [Required]
        public Guid OwnerId { get; init; }



    }
}
