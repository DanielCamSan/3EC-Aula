using System.ComponentModel.DataAnnotations;

    public record CreatePetDto
    {
        [Required]
        public Guid OwnerId { get; init; }

        [Required, StringLength(100)]
        public string Name { get; init; } = string.Empty;

        [Required, StringLength(100)]
        public string Species { get; init; } = string.Empty; // dog | cat | bird | reptile | other

        [Required, StringLength(100)]
        public string Breed { get; init; } = string.Empty;

        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; init; }

        [Required, StringLength(20)]
        public string Sex { get; init; } = string.Empty;

        [Range(0, 500)]
        public decimal? WeightKg { get; init; }
    }

