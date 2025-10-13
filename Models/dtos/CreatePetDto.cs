using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.Dtos
{
    public record CreatePetDto
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid OwnerId { get; set; } 
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string Species { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string Breed { get; set; } = string.Empty;
        [Required]
        public DateTime BirthDate { get; set; }
        [Required, StringLength(20)]
        public string Sex { get; set; } = string.Empty;
        [Range(0, 500)]
        public decimal? WeightKg { get; set; }


    }
    public record PetsListDto(
        Guid Id,
        string Name,
        int AppointmentsCount,
        int OwnersCount
    );
    public record PetsDetailsDto(
        Guid Id,
        string Name,
        string Species,
        string Breed,
        DateTime BirthDate,
        decimal? WeightKg,
        string Sex,
        List<Appointment1ListDto> Appointments,
        List<OwnerListDto> Owners
    );
    
    public record Appointment1ListDto(
        DateTime ScheduledAt,
        string Reason,
        string Status,
        string? Notes,
        Guid id
    );
    public record OwnerListsDto(
        Guid Id,
        string FullName,
        string Email,
        string Phone,
        bool Active,
        int AppointmentsCount
    );
    
}
