namespace Adrover.Client.Backend.Services;

public interface ITokenStore
{
    bool TryGetValidToken(out string token);

    void SetToken(string token, int expiresInSeconds);

    void Clear();
}
