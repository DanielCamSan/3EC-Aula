using System.ComponentModel.DataAnnotations;

public class Appointment
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public DateTime ScheduledAt { get; set; }
    [Required, StringLength(100)]
    public string Reason { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string Status { get; set; } = "scheduled";
    public string? Notes { get; set; }   
}
