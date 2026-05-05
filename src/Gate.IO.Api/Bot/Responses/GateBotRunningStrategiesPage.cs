namespace Gate.IO.Api.Bot;

/// <summary>
/// Running bot strategies page
/// </summary>
public record GateBotRunningStrategiesPage
{
    /// <summary>
    /// Gets or sets the Items.
    /// </summary>
    [JsonProperty("items")]
    public List<GateBotRunningStrategy> Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the Page.
    /// </summary>
    [JsonProperty("page")]
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets the Page Size.
    /// </summary>
    [JsonProperty("page_size")]
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the Total.
    /// </summary>
    [JsonProperty("total")]
    public int Total { get; set; }
}
