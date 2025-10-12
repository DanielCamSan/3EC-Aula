using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FirstExam.Models;

public class Appointment
{
    public Guid Id { get; set; }
    [Required]
    public Guid PetId { get; set; }
    [Required]
    public Guid OwnerId { get; set; }

    [ForeignKey("PetId")]
    public Pet Pet { get; set; } = null!;  

    [ForeignKey("OwnerId")]
    public Owner Owner { get; set; } = null!; 
    public DateTime ScheduledAt { get; set; } = DateTime.Now;
    public string Reason { get; set; } = string.Empty;
    public string Status { get; set; } = "scheduled";
    public string? Notes { get; set; }

}
