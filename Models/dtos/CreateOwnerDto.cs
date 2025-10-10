using System;
using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public record CreateOwnerDto
    {
        public Guid Id { get; init; }
        [Required, StringLength(200)]
        public string Email { get; init; }
        [Required, StringLength(200)]
        public string FullName { get; init; }
        [Required, StringLength(maximumLength: 20, MinimumLength = 7)]
        public string Phone { get; init; }
        public bool Active { get; init; }
        
    }
}
