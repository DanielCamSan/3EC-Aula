using System.ComponentModel.DataAnnotations;
namespace apiwithdb.Models.dtos
{
    public record CreatePetDto
    {
        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(200)]
        public string Species { get; set; } = string.Empty;// dog | cat | bird | reptile | other 
        [Required, StringLength(200)]
        public string Breed { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        [Required, StringLength(200)]
        public string sex { get; set; } = string.Empty;// macho | hembra
        [Range(1000, 2100)]
        public decimal? WeightKg { get; set; }
    }
}