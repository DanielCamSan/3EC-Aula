using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.Dtos
{
    public record UpdatePetDto
    {
        [Required]
        public required Guid OwnerId { get; set; }
        [Required, StringLength(100)]
        public required string Name { get; set; }
        [Required, StringLength(100)]
        public required string Species { get; set; } // dog | cat | bird | reptile | other 
        [Required, StringLength(100)]
        public required string Breed { get; set; }
        [Required]
        public required DateTime BirthDate { get; set; }
        [Required, StringLength(20)]
        public required string sex { get; set; }
        [Range(0, 500)]
        public decimal? WeightKg { get; set; }
        public Guid Id { get; internal set; }
    }

}
