using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record UpdatePetDto
    {
        [Required]
        public Guid OwnerId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Species { get; set; } = string.Empty;

        [StringLength(100)]
        public string Breed { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required, StringLength(20)]
        public string sex { get; set; } = string.Empty;

        [Range(0, 500)]
        public decimal? WeightKg { get; set; }
    }
}