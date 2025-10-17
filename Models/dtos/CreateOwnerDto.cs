using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record CreateOwnerDto
    {
        [Required, EmailAddress, StringLength(200)]
        public string Email { get; init; } = string.Empty;

        [Required, StringLength(200)]
        public string FullName { get; init; } = string.Empty;

        [Required, StringLength(20, MinimumLength = 7)]
        public string Phone { get; init; } = string.Empty;

        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
        public bool Active { get; init; } = true;
    }
}