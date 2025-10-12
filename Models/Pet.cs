using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models
{
    public class Pet
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string sex { get; set; } = string.Empty;
        public decimal? WeightKg { get; set; }
        public Owner? Owner { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}