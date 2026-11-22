using Adrover.Client.Backend.Models.Intelisa;

namespace Adrover.Client.Backend.Services;

public interface IIntelisaApiService
{
    Task<LoginResponse> LoginAsync(LoginRequest? request = null, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Screen>> GetScreensAsync(CancellationToken cancellationToken = default);

    Task<ListCreativesResponse> ListCreativesAsync(ListCreativesRequest request, CancellationToken cancellationToken = default);

    Task<UploadCreativeResponse> UploadCreativeAsync(UploadCreativeRequest request, CancellationToken cancellationToken = default);

    Task<CreativeStatusResponse> GetCreativeStatusAsync(CreativeStatusRequest request, CancellationToken cancellationToken = default);

    Task<CreativeDetailReportResponse> GetCreativeDetailReportAsync(CreativeDetailReportRequest request, CancellationToken cancellationToken = default);

    Task<ScheduleContentResponse> ScheduleContentAsync(ScheduleContentRequest request, CancellationToken cancellationToken = default);
}
