namespace FirstExam.Models
{
    public class Pet
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OwnerId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public DateTime BirthDate { get; set; }
        public string sex { get; set; }
        public decimal? WeightKg { get; set; }
    }
}