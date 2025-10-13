using System;
using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public class UpdateOwnerDto
    {        
        [Required, StringLength(200)]
        public string Email { get; set; }
        [Required, StringLength(200)]
        public string FullName { get; set; }
        [Required, StringLength(7, MinimumLength = 7)]
        public string Phone { get; init; }
        [Required]
        public bool Active { get; set; }
        
    }
}
