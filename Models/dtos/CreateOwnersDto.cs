using System;
using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record CreateOwnerDto
    {
        public Guid Id { get; init; }   

        [Required, StringLength(200)]
        public string Email { get; init; } = string.Empty;

        [Required, StringLength(200)]
        public string FullName { get; init; } = string.Empty;

        [Required, StringLength(maximumLength: 20, MinimumLength = 7)]
        public string Phone { get; init; } = string.Empty;

        public bool Active { get; init; } = true;
    }
}
