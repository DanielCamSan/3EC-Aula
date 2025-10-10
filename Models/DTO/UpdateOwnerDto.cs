using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.DTO
{
    public record UpdateOwnerDto
    {
        [Required, StringLength(200)]
        public string Email { get; set; }
        [Required, StringLength(200)]
        public string FullName { get; set; }
        [Required, StringLength(7, MinimumLength = 7)]
        public string Phone { get; init; }
        public bool Active { get; set; }
    }
}
