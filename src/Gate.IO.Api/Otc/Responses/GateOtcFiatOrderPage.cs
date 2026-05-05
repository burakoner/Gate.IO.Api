namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC fiat order page
/// </summary>
public record GateOtcFiatOrderPage
{
    /// <summary>
    /// Page number
    /// </summary>
    [JsonProperty("pn")]
    public int PageNumber { get; set; }

    /// <summary>
    /// Number of items per page
    /// </summary>
    [JsonProperty("ps")]
    public int PageSize { get; set; }

    /// <summary>
    /// Total pages
    /// </summary>
    [JsonProperty("total_pn")]
    public int TotalPages { get; set; }

    /// <summary>
    /// Total item count
    /// </summary>
    [JsonProperty("count")]
    public int Count { get; set; }

    /// <summary>
    /// Orders
    /// </summary>
    [JsonProperty("list")]
    public List<GateOtcFiatOrder> List { get; set; } = [];
}
