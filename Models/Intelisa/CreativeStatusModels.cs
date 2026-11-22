using System.Text.Json.Serialization;

namespace Adrover.Client.Backend.Models.Intelisa;

public sealed class CreativeStatusRequest
{
    [JsonPropertyName("creative_ids")]
    public List<string> CreativeIds { get; set; } = [];
}

public sealed class CreativeStatusResponse
{
    [JsonPropertyName("creatives")]
    public List<CreativeProcessingStatus> Creatives { get; set; } = [];
}

public sealed class CreativeProcessingStatus
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("file")]
    public string File { get; set; } = string.Empty;

    [JsonPropertyName("group")]
    public string Group { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("resolution")]
    public string? Resolution { get; set; }

    [JsonPropertyName("file_size")]
    public long? FileSize { get; set; }

    [JsonPropertyName("duration_seconds")]
    public int? DurationSeconds { get; set; }

    [JsonPropertyName("pages")]
    public int? Pages { get; set; }
}
