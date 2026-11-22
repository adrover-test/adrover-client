namespace Adrover.Client.Backend.Services;

public sealed class InMemoryTokenStore : ITokenStore
{
    private readonly Lock _lock = new();
    private string? _token;
    private DateTimeOffset _expiresAtUtc;

    public bool TryGetValidToken(out string token)
    {
        lock (_lock)
        {
            if (!string.IsNullOrWhiteSpace(_token) && DateTimeOffset.UtcNow < _expiresAtUtc)
            {
                token = _token;
                return true;
            }

            token = string.Empty;
            return false;
        }
    }

    public void SetToken(string token, int expiresInSeconds)
    {
        var validFor = Math.Max(1, expiresInSeconds - 30);

        lock (_lock)
        {
            _token = token;
            _expiresAtUtc = DateTimeOffset.UtcNow.AddSeconds(validFor);
        }
    }

    public void Clear()
    {
        lock (_lock)
        {
            _token = null;
            _expiresAtUtc = DateTimeOffset.MinValue;
        }
    }
}
