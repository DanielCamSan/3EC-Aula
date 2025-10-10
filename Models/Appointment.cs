using System.ComponentModel.DataAnnotations;

public class Appointment
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public DateTime ScheduledAt { get; set; } = DateTime.Now;

    [Required, StringLength(100)]
    public string Reason { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Status { get; set; } = "scheduled"; // scheduled|done|canceled (libre)

    public string? Notes { get; set; }

    // --- DTOs anidados (como tu base) ---
    public record CreateAppointmentDto
    {
        public Guid PetId { get; init; }
        public DateTime ScheduledAt { get; init; }

        [Required, StringLength(100)]
        public string Reason { get; init; } = string.Empty;

        [Required, StringLength(100)]
        public string Status { get; init; } = "scheduled";

        public string? Notes { get; init; }
    }

    public record UpdateAppointmentDto
    {
        public Guid PetId { get; set; }
        public DateTime ScheduledAt { get; set; }

        [Required, StringLength(100)]
        public string Reason { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Status { get; set; } = "scheduled";

        public string? Notes { get; set; }
    }
}
