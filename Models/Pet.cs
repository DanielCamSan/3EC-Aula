using FirstExam.Models;
using System.ComponentModel.DataAnnotations;
public class Pet
{
    [Required]
    public Guid Id { get; set; }= Guid.NewGuid();
    [Required, StringLength(100)]
    public string Name { get; set; }
    [Required, StringLength(100)]
    public string Species { get; set; } // dog | cat | bird | reptile | other 
    [Required, StringLength(100)]
    public string Breed { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required, StringLength(20)]
    public string Sex { get; set; } // macho | hembra
    [Range(0,500)]
    public decimal? WeightKg { get; set; }

    [Required]
    public Guid OwnerId { get; set; } = Guid.NewGuid();

    public Owner Owner { get; set; } = default!;
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

public record PetDto
{
    public string Name { get; init; } = string.Empty;
}
