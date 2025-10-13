using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models
{
    public class Pet
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid OwnerId { get; set; } = Guid.NewGuid();
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(100)]
        public string Species { get; set; }
        [Required, StringLength(100)]
        public string Breed { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required, StringLength(20)]
        public string sex { get; set; }
        [Range(0, 500)]
        public decimal? WeightKg { get; set; }
    }
}
