using System.ComponentModel.DataAnnotations;
public class Pet
{
    [Required]
    public Guid Id { get; set; }= Guid.NewGuid();
    [Required]
    public Guid OwnerId { get; set; } = Guid.NewGuid();
    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string Species { get; set; } = string.Empty; // dog | cat | bird | reptile | other 
    [Required, StringLength(100)]
    public string Breed { get; set; } = string.Empty;
    [Required]
    public DateTime BirthDate { get; set; }
    [Required, StringLength(20)]
    public string sex { get; set; } = string.Empty; // macho | hembra
    [Range(0,500)]
    public decimal? WeightKg { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();



    public record UpdatePetDto
    {
        [Required]
        public Guid Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Species { get; set; }

        [StringLength(100)]
        public string? Breed { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(20)]
        public string? Sex { get; set; }

        [Range(0, 500)]
        public decimal? WeightKg { get; set; }

        public Guid? OwnerId { get; set; }
    }

    public record CreatePetDto
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid OwnerId { get; set; } = Guid.NewGuid();
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string Species { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string Breed { get; set; } = string.Empty;
        [Required]
        public DateTime BirthDate { get; set; }
        [Required, StringLength(20)]
        public string sex { get; set; } = string.Empty;
        [Range(0, 500)]
        public decimal? WeightKg { get; set; }
    }
}
