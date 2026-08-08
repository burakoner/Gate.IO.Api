namespace Gate.IO.Api.TradFi;

/// <summary>
/// Paginated TradFi historical positions.
/// </summary>
public record GateTradFiPositionHistoryList
{
    /// <summary>
    /// Total number of matching historical positions.
    /// </summary>
    [JsonProperty("total")]
    public int Total { get; set; }

    /// <summary>
    /// Total number of pages.
    /// </summary>
    [JsonProperty("total_page")]
    public int TotalPage { get; set; }

    /// <summary>
    /// Historical positions on the current page.
    /// </summary>
    [JsonProperty("list")]
    public List<GateTradFiPositionHistory> List { get; set; } = [];
}
