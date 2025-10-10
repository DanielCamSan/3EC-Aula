using System.ComponentModel.DataAnnotations;

public class Appointment
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public DateTime ScheduledAt { get; set; } = DateTime.Now;
    public string Reason { get; set; } = string.Empty;
    public string Status { get; set; } = "scheduled";
    public string? Notes { get; set; }

}
