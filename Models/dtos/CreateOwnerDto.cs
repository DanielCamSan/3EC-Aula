using System;
using System.ComponentModel.DataAnnotations;

namespace apiwithdb.Models.dtos
{
    public record CreateOwnerDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(200)]
        public string FullName { get; set; } = string.Empty;

        [Required, StringLength(7, MinimumLength = 7)]
        public string Phone { get; set; } = string.Empty;

        public bool Active { get; set; } = true;

        public string? Notes { get; set; } 
    }
}
