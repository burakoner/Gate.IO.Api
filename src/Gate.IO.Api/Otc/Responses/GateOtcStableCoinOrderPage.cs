namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC stablecoin order page
/// </summary>
public record GateOtcStableCoinOrderPage
{
    /// <summary>
    /// Total item count
    /// </summary>
    [JsonProperty("total")]
    public int Total { get; set; }

    /// <summary>
    /// Number of records per page
    /// </summary>
    [JsonProperty("page_size")]
    public int PageSize { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    [JsonProperty("page_number")]
    public int PageNumber { get; set; }

    /// <summary>
    /// Total pages
    /// </summary>
    [JsonProperty("total_page")]
    public int TotalPage { get; set; }

    /// <summary>
    /// Orders
    /// </summary>
    [JsonProperty("list")]
    public List<GateOtcStableCoinOrder> List { get; set; } = [];
}
