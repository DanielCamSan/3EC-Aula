using System;
using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models
{
    public class Owner
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
    }
};

