using System.Text.Json.Serialization;

namespace Adrover.Client.Backend.Models.Intelisa;

public sealed class Screen
{
    [JsonPropertyName("screenID")]
    public string ScreenID { get; set; } = string.Empty;

    [JsonPropertyName("screenName")]
    public string ScreenName { get; set; } = string.Empty;

    [JsonPropertyName("screenType")]
    public string ScreenType { get; set; } = string.Empty;

    [JsonPropertyName("spotCode")]
    public string SpotCode { get; set; } = string.Empty;

    [JsonPropertyName("zone")]
    public string Zone { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("lastDataRecorded")]
    public string LastDataRecorded { get; set; } = string.Empty;

    [JsonPropertyName("coordinates")]
    public Coordinates? Coordinates { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("hardwareRotation")]
    public string? HardwareRotation { get; set; }

    [JsonPropertyName("deviceIMEI")]
    public string? DeviceIMEI { get; set; }

    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }
}

public sealed class Coordinates
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lng")]
    public double Lng { get; set; }
}
