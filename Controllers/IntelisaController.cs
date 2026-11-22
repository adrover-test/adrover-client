using Adrover.Client.Backend.Models.Intelisa;
using Adrover.Client.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Adrover.Client.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class IntelisaController : ControllerBase
{
    private readonly IIntelisaApiService _intelisaApiService;

    public IntelisaController(IIntelisaApiService intelisaApiService)
    {
        _intelisaApiService = intelisaApiService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest? request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _intelisaApiService.LoginAsync(request, cancellationToken);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (IntelisaApiException ex)
        {
            return StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
        }
    }

    [HttpPost("screens")]
    public async Task<ActionResult<IReadOnlyList<Screen>>> GetScreens(CancellationToken cancellationToken)
    {
        try
        {
            var response = await _intelisaApiService.GetScreensAsync(cancellationToken);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (IntelisaApiException ex)
        {
            return StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
        }
    }

    [HttpPost("creatives")]
    public async Task<ActionResult<ListCreativesResponse>> ListCreatives(
        [FromBody] ListCreativesRequest? request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _intelisaApiService.ListCreativesAsync(request ?? new ListCreativesRequest(), cancellationToken);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (IntelisaApiException ex)
        {
            return StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
        }
    }

    [HttpPost("creatives/upload")]
    public async Task<ActionResult<UploadCreativeResponse>> UploadCreative(
        [FromForm] UploadCreativeRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _intelisaApiService.UploadCreativeAsync(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (IntelisaApiException ex)
        {
            return StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
        }
    }

    [HttpPost("creatives/status")]
    public async Task<ActionResult<CreativeStatusResponse>> CreativeStatus(
        [FromBody] CreativeStatusRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _intelisaApiService.GetCreativeStatusAsync(request, cancellationToken);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (IntelisaApiException ex)
        {
            return StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
        }
    }

    [HttpPost("reports/creative-detail")]
    public async Task<ActionResult<CreativeDetailReportResponse>> CreativeDetailReport(
        [FromBody] CreativeDetailReportRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _intelisaApiService.GetCreativeDetailReportAsync(request, cancellationToken);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (IntelisaApiException ex)
        {
            return StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
        }
    }

    [HttpPost("schedules")]
    public async Task<ActionResult<ScheduleContentResponse>> Schedule(
        [FromBody] ScheduleContentRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _intelisaApiService.ScheduleContentAsync(request, cancellationToken);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (IntelisaApiException ex)
        {
            return StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
        }
    }
}
