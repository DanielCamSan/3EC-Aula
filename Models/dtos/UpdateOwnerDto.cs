using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record UpdateOwnerDto
    {
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(200)]
        public string? FullName { get; set; }

        [StringLength(7, MinimumLength = 7)]
        public string? Phone { get; set; }

        public bool? Active { get; set; }

        public string? Notes { get; set; } 
    }
}
