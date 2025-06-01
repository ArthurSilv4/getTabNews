using System.Text.Json.Serialization;

namespace getTabNews.Dtos
{
    public record class FilterPostsDto
    {
        [JsonPropertyName("id")]
        public required string Id { get; init; }

        [JsonPropertyName("title")]
        public required string Title { get; init; }

        [JsonPropertyName("slug")]
        public required string Slug { get; init; }

        [JsonPropertyName("owner_username")]
        public required string OwnerUsername { get; init; }
    }
}
