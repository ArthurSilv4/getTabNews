using System;
using System.Text.Json.Serialization;

namespace getTabNews.Dtos
{
    public record class PostDetailsDto
    {
        [JsonPropertyName("id")]
        public required string Id { get; init; }

        [JsonPropertyName("owner_id")]
        public required string OwnerId { get; init; }

        [JsonPropertyName("parent_id")]
        public string? ParentId { get; init; }

        [JsonPropertyName("slug")]
        public required string Slug { get; init; }

        [JsonPropertyName("title")]
        public required string Title { get; init; }

        [JsonPropertyName("body")]
        public required string Body { get; init; }

        [JsonPropertyName("status")]
        public required string Status { get; init; }

        [JsonPropertyName("type")]
        public required string Type { get; init; }

        [JsonPropertyName("source_url")]
        public required string SourceUrl { get; init; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; init; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; init; }

        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; init; }

        [JsonPropertyName("deleted_at")]
        public DateTime? DeletedAt { get; init; }

        [JsonPropertyName("owner_username")]
        public required string OwnerUsername { get; init; }

        [JsonPropertyName("tabcoins")]
        public int Tabcoins { get; init; }

        [JsonPropertyName("tabcoins_credit")]
        public int TabcoinsCredit { get; init; }

        [JsonPropertyName("tabcoins_debit")]
        public int TabcoinsDebit { get; init; }

        [JsonPropertyName("children_deep_count")]
        public int ChildrenDeepCount { get; init; }
    }
}
