using System.ComponentModel.DataAnnotations;
using FirstExam.Models;

public class Appointment
{
    public Guid Id { get; set; }

    [Required]
    public DateTime ScheduledAt { get; set; } = DateTime.Now;
    [Required, StringLength(100)]
    public string Reason { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string Status { get; set; } = "scheduled";
    public string? Notes { get; set; }

    public Owner? owner { get; set; }

    public Guid? OwnerId { get; set; }

<<<<<<< HEAD
    public Pet? Pet { get; set; }
    public Guid PetId { get; set; }


=======
   public Pet? pet { get; set; }
public ICollection<Pet> Pets { get; set; } = new List<Pet>();
>>>>>>> d4b14ef4c366493bf224b086c58df291ef7a6f21


}
