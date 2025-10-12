using FirstExam.Data;
namespace FirstExam.Models
{
    public class Pet
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OwnerId { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string sex { get; set; } = string.Empty;

        public decimal? WeightKg { get; set; }
    }
}
