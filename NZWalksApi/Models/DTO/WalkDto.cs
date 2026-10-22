using NZWalksApi.Models.Domain;

namespace NZWalksApi.Models.DTO;

public record WalkDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
    public double LengthInKm { get; set; } = 0;

    public string? WalkImageUrl { get; set; } = string.Empty;

    // Navigation property for Difficulty
    public DifficultyDto Difficulty { get; set; }

    // Navigation property for RegionDto
    public RegionDto Region { get; set; }
}
