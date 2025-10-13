using System;
using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models
{
    public class Owner
    {
        public Guid Id { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, StringLength(200)]
        public string FullName { get; set; } = string.Empty;
        [Required, StringLength(7, MinimumLength = 7)]
        public string Phone { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public Pet Pet { get; set; } = default!;
    }


};