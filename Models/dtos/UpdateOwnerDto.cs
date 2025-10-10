using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record UpdateOwnerDto
    {
        [Required, StringLength(200)]
        public string Email { get; set; } = string.Empty;
        [Required, StringLength(200)]
        public string FullName { get; set; } = string.Empty;
        [Required, StringLength(7, MinimumLength = 7)]
        public string Phone { get; init; } = string.Empty;
        public bool Active { get; set; }
    }
}