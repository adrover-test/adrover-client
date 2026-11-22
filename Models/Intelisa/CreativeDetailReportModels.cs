using System.Text.Json.Serialization;

namespace Adrover.Client.Backend.Models.Intelisa;

public sealed class CreativeDetailReportRequest
{
    [JsonPropertyName("creative_id")]
    public string CreativeId { get; set; } = string.Empty;

    [JsonPropertyName("start_date")]
    public string? StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public string? EndDate { get; set; }

    [JsonPropertyName("screen_ids")]
    public List<string>? ScreenIds { get; set; }

    [JsonPropertyName("locations")]
    public List<string>? Locations { get; set; }

    [JsonPropertyName("screen_types")]
    public List<string>? ScreenTypes { get; set; }
}

public sealed class CreativeDetailReportResponse
{
    [JsonPropertyName("creative")]
    public ReportCreative Creative { get; set; } = new();

    [JsonPropertyName("period")]
    public ReportPeriod Period { get; set; } = new();

    [JsonPropertyName("totals")]
    public ReportTotals Totals { get; set; } = new();

    [JsonPropertyName("per_day")]
    public List<ReportDayStat> PerDay { get; set; } = [];

    [JsonPropertyName("dates")]
    public List<string> Dates { get; set; } = [];

    [JsonPropertyName("screens")]
    public List<ReportScreen> Screens { get; set; } = [];

    [JsonPropertyName("screens_without_plays")]
    public List<ReportScreenWithoutPlays>? ScreensWithoutPlays { get; set; }

    [JsonPropertyName("data_freshness")]
    public ReportDataFreshness? DataFreshness { get; set; }

    [JsonPropertyName("screens_not_found")]
    public List<string>? ScreensNotFound { get; set; }

    [JsonPropertyName("notes")]
    public List<string>? Notes { get; set; }
}

public sealed class ReportCreative
{
    [JsonPropertyName("creative_id")]
    public string CreativeId { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("ad_group")]
    public string AdGroup { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("resolution")]
    public string? Resolution { get; set; }

    [JsonPropertyName("orientation")]
    public string? Orientation { get; set; }

    [JsonPropertyName("uploaded")]
    public string? Uploaded { get; set; }

    [JsonPropertyName("valid_until")]
    public string? ValidUntil { get; set; }
}

public sealed class ReportPeriod
{
    [JsonPropertyName("start")]
    public string Start { get; set; } = string.Empty;

    [JsonPropertyName("end")]
    public string End { get; set; } = string.Empty;

    [JsonPropertyName("days")]
    public int Days { get; set; }
}

public sealed class ReportTotals
{
    [JsonPropertyName("plays")]
    public long Plays { get; set; }

    [JsonPropertyName("minutes")]
    public double Minutes { get; set; }

    [JsonPropertyName("screens_played")]
    public int ScreensPlayed { get; set; }

    [JsonPropertyName("screens_in_report")]
    public int ScreensInReport { get; set; }

    [JsonPropertyName("average_frequency_seconds")]
    public int AverageFrequencySeconds { get; set; }

    [JsonPropertyName("missed_screen_days")]
    public int MissedScreenDays { get; set; }
}

public sealed class ReportDayStat
{
    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("plays")]
    public long Plays { get; set; }

    [JsonPropertyName("minutes")]
    public double Minutes { get; set; }
}

public sealed class ReportScreen
{
    [JsonPropertyName("screen_id")]
    public string ScreenId { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("location")]
    public string Location { get; set; } = string.Empty;

    [JsonPropertyName("screen_type")]
    public string ScreenType { get; set; } = string.Empty;

    [JsonPropertyName("plays")]
    public long Plays { get; set; }

    [JsonPropertyName("minutes")]
    public double Minutes { get; set; }

    [JsonPropertyName("days_played")]
    public int DaysPlayed { get; set; }

    [JsonPropertyName("days_not_played")]
    public int DaysNotPlayed { get; set; }

    [JsonPropertyName("last_played")]
    public string? LastPlayed { get; set; }

    [JsonPropertyName("share_percent")]
    public double SharePercent { get; set; }

    [JsonPropertyName("daily_plays")]
    public List<int> DailyPlays { get; set; } = [];
}

public sealed class ReportScreenWithoutPlays
{
    [JsonPropertyName("screen_id")]
    public string ScreenId { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("location")]
    public string Location { get; set; } = string.Empty;
}

public sealed class ReportDataFreshness
{
    [JsonPropertyName("latest_day_with_play_data")]
    public string LatestDayWithPlayData { get; set; } = string.Empty;

    [JsonPropertyName("processing")]
    public string Processing { get; set; } = string.Empty;
}
