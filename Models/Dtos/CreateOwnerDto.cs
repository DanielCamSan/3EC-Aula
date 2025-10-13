using System.ComponentModel.DataAnnotations;
namespace FirstExam.Models.Dtos
{
    public class CreateOwnerDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, StringLength(200)]
        public string FullName { get; set; } = string.Empty;
        [Required, StringLength(7, MinimumLength = 7)]
        public string Phone { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
    }
    public record OwnerListDto(
        Guid Id,
        string FullName,
        int AppointmentsCount
    );
    
    public record OwnerDetailsDto(
        Guid Id,
        string Email,
        string FullName,
        string Phone,
        bool Active,
        List<AppointmentListDto> Appointments
    );
    public record AppointmentListDto(
        Guid Id,
        DateTime ScheduledAt,
        string Reason,
        string Status,
        string? Notes
    );

}