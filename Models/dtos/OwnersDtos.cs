using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public class OwnersDtos
    {
        public record OwnerListDto(
            Guid Id,
            string Email,
            string FullName
        );
        public record OwnerDetailDto(
       Guid Id,
       string Email,
       string FullName
   );

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
}
