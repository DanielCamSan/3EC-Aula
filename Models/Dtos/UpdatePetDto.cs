using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.Dtos
{
    public record UpdatePetDto
    {
        [Required]
        public Guid Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Species { get; set; } = string.Empty ;

        [StringLength(100)]
        public string? Breed { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string? Sex { get; set; } = string.Empty;

        [Range(0, 500)]
        public decimal? WeightKg { get; set; }

        public Guid OwnerId { get; set; }
    }
}

