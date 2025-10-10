using System.ComponentModel.DataAnnotations;
public class Pet
{

    public Guid Id { get; set; }= Guid.NewGuid();
    public Guid OwnerId { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public string Species { get; set; } // dog | cat | bird | reptile | other 
    public string Breed { get; set; }
    public DateTime BirthDate { get; set; }

    public string sex { get; set; } // macho | hembra
    public decimal? WeightKg { get; set; }
}
