namespace NZWalksApi.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public double LengthInKm { get; set; } = 0;

        public string? WalkImageUrl { get; set; } = string.Empty;

        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        // Navigation property for Difficulty
        public Difficulty Difficulty { get; set; }

        // Navigation property for RegionDto
        public Region Region { get; set; }
    }
}
