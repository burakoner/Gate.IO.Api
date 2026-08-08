namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock paginated response
/// </summary>
public record GateStockPage<T> where T : class
{
    /// <summary>
    /// Gets or sets the total item count.
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }

    /// <summary>
    /// Gets or sets the total page count.
    /// </summary>
    [JsonProperty("total_page")]
    public long TotalPages { get; set; }

    /// <summary>
    /// Gets or sets the page items.
    /// </summary>
    [JsonProperty("list")]
    public List<T> List { get; set; } = [];
}
