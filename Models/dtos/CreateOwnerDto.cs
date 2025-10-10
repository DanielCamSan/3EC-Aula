using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record CreateOwnerDto
    {
        public Guid Id { get; set; }

        [Required, StringLength(200)]
        public string Email { get; set; } = string.Empty;
        [Required, StringLength(200)]
        public string FullName { get; set; } = string.Empty;
        [Required, StringLength(maximumLength: 20, MinimumLength = 7)]
        public string Phone { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
