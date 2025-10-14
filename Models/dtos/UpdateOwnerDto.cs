using System.ComponentModel.DataAnnotations;

namespace FirstExam.Models.dtos
{
    public class UpdateOwnerDto
    {
        public string? Email { get; init; }
        public string? FullName { get; init; } 
        public string? Phone { get; init; }
        public bool? Active {  get; init; }
    }
}
