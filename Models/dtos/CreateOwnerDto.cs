using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record CreateOwnerDto
    {
        [Required, EmailAddress, StringLength(200)]
        public string Email { get; init; } = string.Empty;

        [Required, StringLength(200)]
        public string FullName { get; init; } = string.Empty;

        [Required, StringLength(7, MinimumLength = 7)]
        [RegularExpression(@"^\d{7}$", ErrorMessage = "Phone debe tener exactamente 7 dígitos.")]
        public string Phone { get; init; } = string.Empty;
    }
}
