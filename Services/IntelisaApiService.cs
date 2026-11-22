using System.Net.Http.Json;
using System.Text.Json;
using Adrover.Client.Backend.Models.Intelisa;
using Adrover.Client.Backend.Options;
using Microsoft.Extensions.Options;

namespace Adrover.Client.Backend.Services;

public sealed class IntelisaApiService : IIntelisaApiService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly ITokenStore _tokenStore;
    private readonly IntelisaApiOptions _options;

    public IntelisaApiService(HttpClient httpClient, ITokenStore tokenStore, IOptions<IntelisaApiOptions> options)
    {
        _httpClient = httpClient;
        _tokenStore = tokenStore;
        _options = options.Value;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest? request = null, CancellationToken cancellationToken = default)
    {
        var loginRequest = request ?? new LoginRequest
        {
            UserName = _options.UserName,
            Password = _options.Password
        };

        if (string.IsNullOrWhiteSpace(loginRequest.UserName) || string.IsNullOrWhiteSpace(loginRequest.Password))
        {
            throw new InvalidOperationException("Intelisa credentials are required.");
        }

        var response = await PostAsync<LoginRequest, LoginResponse>("/api/v1/auth/login", loginRequest, null, cancellationToken);
        _tokenStore.SetToken(response.Token, response.ExpiresIn);
        return response;
    }

    public async Task<IReadOnlyList<Screen>> GetScreensAsync(CancellationToken cancellationToken = default)
    {
        return await ExecuteWithTokenRetryAsync(async token =>
            await PostAsync<object, List<Screen>>("/api/v1/screens", new { }, token, cancellationToken));
    }

    public async Task<ListCreativesResponse> ListCreativesAsync(ListCreativesRequest request, CancellationToken cancellationToken = default)
    {
        request.Limit ??= 50;
        request.Offset ??= 0;
        request.Type ??= "any";

        return await ExecuteWithTokenRetryAsync(async token =>
            await PostAsync<ListCreativesRequest, ListCreativesResponse>("/api/v1/creatives", request, token, cancellationToken));
    }

    public async Task<UploadCreativeResponse> UploadCreativeAsync(UploadCreativeRequest request, CancellationToken cancellationToken = default)
    {
        if (request.File is null || request.File.Length == 0)
        {
            throw new InvalidOperationException("File is required for upload.");
        }

        if (string.IsNullOrWhiteSpace(request.AdGroup))
        {
            throw new InvalidOperationException("AdGroup is required for upload.");
        }

        return await ExecuteWithTokenRetryAsync(async token =>
        {
            using var formContent = new MultipartFormDataContent();
            await using var stream = request.File.OpenReadStream();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(request.File.ContentType);

            formContent.Add(fileContent, "file", request.File.FileName);
            formContent.Add(new StringContent(request.AdGroup), "adGroup");

            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                formContent.Add(new StringContent(request.Description), "description");
            }

            if (!string.IsNullOrWhiteSpace(request.StartDate))
            {
                formContent.Add(new StringContent(request.StartDate), "startDate");
            }

            if (!string.IsNullOrWhiteSpace(request.EndDate))
            {
                formContent.Add(new StringContent(request.EndDate), "endDate");
            }

            return await PostMultipartAsync<UploadCreativeResponse>("/api/v1/creatives/upload", formContent, token, cancellationToken);
        });
    }

    public async Task<CreativeStatusResponse> GetCreativeStatusAsync(CreativeStatusRequest request, CancellationToken cancellationToken = default)
    {
        if (request.CreativeIds.Count is < 1 or > 50)
        {
            throw new InvalidOperationException("creative_ids must contain 1 to 50 IDs.");
        }

        return await ExecuteWithTokenRetryAsync(async token =>
            await PostAsync<CreativeStatusRequest, CreativeStatusResponse>("/api/v1/creatives/status", request, token, cancellationToken));
    }

    public async Task<CreativeDetailReportResponse> GetCreativeDetailReportAsync(CreativeDetailReportRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.CreativeId))
        {
            throw new InvalidOperationException("creative_id is required.");
        }

        return await ExecuteWithTokenRetryAsync(async token =>
            await PostAsync<CreativeDetailReportRequest, CreativeDetailReportResponse>("/api/v1/reports/creative-detail", request, token, cancellationToken));
    }

    public async Task<ScheduleContentResponse> ScheduleContentAsync(ScheduleContentRequest request, CancellationToken cancellationToken = default)
    {
        if (request.Content.Count == 0)
        {
            throw new InvalidOperationException("content must contain at least one item.");
        }

        if (request.ScreenIds.Count == 0)
        {
            throw new InvalidOperationException("screen_ids must contain at least one screen id.");
        }

        if (string.IsNullOrWhiteSpace(request.StartDate) || string.IsNullOrWhiteSpace(request.EndDate))
        {
            throw new InvalidOperationException("start_date and end_date are required.");
        }

        request.Mode ??= "add";

        return await ExecuteWithTokenRetryAsync(async token =>
            await PostAsync<ScheduleContentRequest, ScheduleContentResponse>("/api/v1/schedules", request, token, cancellationToken));
    }

    private async Task<T> ExecuteWithTokenRetryAsync<T>(Func<string, Task<T>> action)
    {
        var token = await GetOrRefreshTokenAsync();

        try
        {
            return await action(token);
        }
        catch (IntelisaApiException ex) when (ex.StatusCode == 401)
        {
            _tokenStore.Clear();
            var freshToken = await GetOrRefreshTokenAsync();
            return await action(freshToken);
        }
    }

    private async Task<string> GetOrRefreshTokenAsync()
    {
        if (_tokenStore.TryGetValidToken(out var cachedToken))
        {
            return cachedToken;
        }

        var login = await LoginAsync();
        return login.Token;
    }

    private async Task<TResponse> PostAsync<TRequest, TResponse>(
        string path,
        TRequest payload,
        string? token,
        CancellationToken cancellationToken)
    {
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = JsonContent.Create(payload, options: JsonOptions)
        };

        if (!string.IsNullOrWhiteSpace(token))
        {
            requestMessage.Headers.Add("auth-token", token);
        }

        using var response = await _httpClient.SendAsync(requestMessage, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await TryReadErrorAsync(response, cancellationToken);
            throw new IntelisaApiException(
                error?.Message ?? $"Intelisa API error: {(int)response.StatusCode}",
                (int)response.StatusCode,
                error?.Code);
        }

        var result = await response.Content.ReadFromJsonAsync<TResponse>(JsonOptions, cancellationToken);
        if (result is null)
        {
            throw new IntelisaApiException("Intelisa API returned an empty response.", (int)response.StatusCode);
        }

        return result;
    }

    private async Task<TResponse> PostMultipartAsync<TResponse>(
        string path,
        MultipartFormDataContent content,
        string token,
        CancellationToken cancellationToken)
    {
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = content
        };
        requestMessage.Headers.Add("auth-token", token);

        using var response = await _httpClient.SendAsync(requestMessage, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await TryReadErrorAsync(response, cancellationToken);
            throw new IntelisaApiException(
                error?.Message ?? $"Intelisa API error: {(int)response.StatusCode}",
                (int)response.StatusCode,
                error?.Code);
        }

        var result = await response.Content.ReadFromJsonAsync<TResponse>(JsonOptions, cancellationToken);
        if (result is null)
        {
            throw new IntelisaApiException("Intelisa API returned an empty response.", (int)response.StatusCode);
        }

        return result;
    }

    private static async Task<ApiErrorResponse?> TryReadErrorAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var raw = await response.Content.ReadAsStringAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(raw))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<ApiErrorResponse>(raw, JsonOptions);
        }
        catch (JsonException)
        {
            return new ApiErrorResponse
            {
                Error = "Unknown",
                Code = $"HTTP_{(int)response.StatusCode}",
                Message = raw
            };
        }
    }
}
