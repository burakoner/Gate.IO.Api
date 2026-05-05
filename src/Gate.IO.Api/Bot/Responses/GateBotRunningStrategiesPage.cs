namespace Gate.IO.Api.Bot;

/// <summary>
/// Running bot strategies page
/// </summary>
public record GateBotRunningStrategiesPage
{
    [JsonProperty("items")]
    public List<GateBotRunningStrategy> Items { get; set; } = [];

    [JsonProperty("page")]
    public int Page { get; set; }

    [JsonProperty("page_size")]
    public int PageSize { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }
}
