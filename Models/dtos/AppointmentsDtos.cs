using System;
using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public class AppointmentsDtos
    {
        public record CreateAppointmentDto
        {
            public Guid Id { get; init; }

            [Required, StringLength(200)]
            public string Title { get; init; }

            [Required]
            public DateTime Date { get; init; }

            [Range(1, 5000)]
            public int Capacity { get; init; }

            [Required]
            public Guid OwnerId { get; init; }  // Relación con Owners, si aplica

            public bool Active { get; init; }
        }

        public record UpdateAppointmentDto
        {
            [Required, StringLength(200)]
            public string Title { get; set; }

            [Required]
            public DateTime Date { get; set; }

            [Range(1, 5000)]
            public int Capacity { get; set; }

            [Required]
            public Guid OwnerId { get; set; }

            public bool Active { get; set; }
        }
    }
}
