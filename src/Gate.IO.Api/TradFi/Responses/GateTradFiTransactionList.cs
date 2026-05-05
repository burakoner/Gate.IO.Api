namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi transaction page
/// </summary>
public record GateTradFiTransactionList
{
    /// <summary>
    /// Gets or sets the Total.
    /// </summary>
    [JsonProperty("total")]
    public int Total { get; set; }

    /// <summary>
    /// Gets or sets the Total Page.
    /// </summary>
    [JsonProperty("total_page")]
    public int TotalPage { get; set; }

    /// <summary>
    /// Gets or sets the List.
    /// </summary>
    [JsonProperty("list")]
    public List<GateTradFiTransaction> List { get; set; } = [];
}
