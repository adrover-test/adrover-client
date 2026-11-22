using System.Text.Json.Serialization;

namespace Adrover.Client.Backend.Models.Intelisa;

public sealed class ScheduleContentRequest
{
    [JsonPropertyName("content")]
    public List<ScheduleContentItem> Content { get; set; } = [];

    [JsonPropertyName("screen_ids")]
    public List<string> ScreenIds { get; set; } = [];

    [JsonPropertyName("start_date")]
    public string StartDate { get; set; } = string.Empty;

    [JsonPropertyName("end_date")]
    public string EndDate { get; set; } = string.Empty;

    [JsonPropertyName("days_of_week")]
    public List<string>? DaysOfWeek { get; set; }

    [JsonPropertyName("all_day")]
    public bool? AllDay { get; set; }

    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    [JsonPropertyName("end_time")]
    public string? EndTime { get; set; }

    [JsonPropertyName("mode")]
    public string? Mode { get; set; }

    [JsonPropertyName("dry_run")]
    public bool? DryRun { get; set; }
}

public sealed class ScheduleContentItem
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "creative";

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("seconds")]
    public int? Seconds { get; set; }
}

public sealed class ScheduleContentResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("note")]
    public string? Note { get; set; }

    [JsonPropertyName("preview")]
    public SchedulePreview? Preview { get; set; }

    [JsonPropertyName("warnings")]
    public List<string>? Warnings { get; set; }
}

public sealed class SchedulePreview
{
    [JsonPropertyName("what")]
    public List<SchedulePreviewItem> What { get; set; } = [];

    [JsonPropertyName("screens")]
    public SchedulePreviewScreens? Screens { get; set; }

    [JsonPropertyName("dates")]
    public SchedulePreviewDates? Dates { get; set; }

    [JsonPropertyName("time")]
    public string? Time { get; set; }

    [JsonPropertyName("mode")]
    public string? Mode { get; set; }
}

public sealed class SchedulePreviewItem
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("group")]
    public string Group { get; set; } = string.Empty;

    [JsonPropertyName("on_screen")]
    public string OnScreen { get; set; } = string.Empty;
}

public sealed class SchedulePreviewScreens
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("names")]
    public List<string> Names { get; set; } = [];
}

public sealed class SchedulePreviewDates
{
    [JsonPropertyName("from")]
    public string From { get; set; } = string.Empty;

    [JsonPropertyName("to")]
    public string To { get; set; } = string.Empty;

    [JsonPropertyName("days")]
    public string Days { get; set; } = string.Empty;

    [JsonPropertyName("count")]
    public int Count { get; set; }
}
