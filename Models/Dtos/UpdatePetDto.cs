using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.Dtos
{
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
}

