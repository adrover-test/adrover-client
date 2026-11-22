namespace Adrover.Client.Backend.Services;

public sealed class IntelisaApiException : Exception
{
    public IntelisaApiException(string message, int statusCode, string? code = null)
        : base(message)
    {
        StatusCode = statusCode;
        Code = code;
    }

    public int StatusCode { get; }

    public string? Code { get; }
}
