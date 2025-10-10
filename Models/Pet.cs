using System.ComponentModel.DataAnnotations;
namespace apiwithdb.Models
{
    public class Pet
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;// dog | cat | bird | reptile | other 
        public string Breed { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string sex { get; set; } = string.Empty;// macho | hembra
        public decimal? WeightKg { get; set; }
    }
}
