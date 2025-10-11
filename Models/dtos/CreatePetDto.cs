using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record CreatePetDto
    {
        [Required]
        public Guid OwnerId { get; init; }

        [Required, StringLength(100)]
        public string Name { get; init; } = string.Empty;

        [Required, StringLength(100)]
        public string Species { get; init; } = string.Empty;

        [StringLength(100)]
        public string Breed { get; init; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; init; }

        [Required, StringLength(20)]
        public string sex { get; init; } = string.Empty;

        [Range(0, 500)]
        public decimal? WeightKg { get; init; }
    }
}