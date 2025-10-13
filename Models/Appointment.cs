using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        [Required]
        public Guid PetId { get; set; }
        public Pet? Pet { get; set; }

        public DateTime ScheduledAt { get; set; } = DateTime.Now;

        [Required, StringLength(100)]
        public string Reason { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Status { get; set; } = "scheduled";

        public string? Notes { get; set; }
    }
}
