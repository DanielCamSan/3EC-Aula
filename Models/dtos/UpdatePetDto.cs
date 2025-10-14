using System.ComponentModel.DataAnnotations;
public record UpdatePetDto
{
    public Guid? OwnerId { get; init; }
    public string? Name { get; init; } 
    public string? Species { get; init; } 
    public string? Breed { get; init; } 
    public DateTime? BirthDate { get; init; }
    public string? Sex { get; init; } 
    public decimal? WeightKg { get; init; }
}
