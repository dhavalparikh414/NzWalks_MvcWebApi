namespace NZWalksApi.Models.DTO
{
    public record UpdateRegionRequestDto
    {
        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
        public string? RegionImageUrl { get; set; } = string.Empty;
    }
}
