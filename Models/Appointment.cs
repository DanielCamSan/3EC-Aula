using FirstExam.Models;
using System.ComponentModel.DataAnnotations;

public class Appointment
{
    public Guid Id { get; set; }
    public DateTime ScheduledAt { get; set; }
    [Required, StringLength(100)]
    public string Reason { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string Status { get; set; } = "scheduled";
    public string? Notes { get; set; }

    // FKs
    public Guid OwnerId { get; set; }
    public Guid PetId { get; set; }

    // Navegación
    public Owner Owner { get; set; } = default!;
    public Pet Pet { get; set; } = default!;

}
public record AppointmentDto
{
    public Guid Id { get; init; }
    public DateTime ScheduledAt { get; init; }
    public string Reason { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public string? Notes { get; init; }
    public Guid PetId { get; init; }
    public Guid OwnerId { get; init; }
}