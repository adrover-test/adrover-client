using System.Text.Json.Serialization;

namespace Adrover.Client.Backend.Models.Intelisa;

public sealed class UploadCreativeRequest
{
    public IFormFile File { get; set; } = default!;

    public string AdGroup { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }
}

public sealed class UploadCreativeResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("creative")]
    public UploadedCreative Creative { get; set; } = new();
}

public sealed class UploadedCreative
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("file")]
    public string File { get; set; } = string.Empty;

    [JsonPropertyName("group")]
    public string Group { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}
