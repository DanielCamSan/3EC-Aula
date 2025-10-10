using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.DTO
{
    public record CreatePetDto
    {
        [Required]
        public required Guid OwnerId { get; init; }
        [Required, StringLength(100)]
        public required string Name { get; init; }
        [Required, StringLength(100)]
        public required string Species { get; init; } // dog | cat | bird | reptile | other 
        [Required, StringLength(100)]
        public required string Breed { get; init; }
        [Required]
        public required DateTime BirthDate { get; init; }
        [Required, StringLength(20)]
        public required string sex { get; init; }
        [Range(0, 500)]
        public decimal? WeightKg { get; init; }
    }
}
