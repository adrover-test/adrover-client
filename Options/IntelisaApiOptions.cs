namespace Adrover.Client.Backend.Options;

public sealed class IntelisaApiOptions
{
    public const string SectionName = "IntelisaApi";

    public string BaseUrl { get; set; } = "https://login.intelisa.in";

    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
