namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking order list
/// </summary>
public record GateEarnStakingOrderPage
{
    /// <summary>
    /// Page
    /// </summary>
    [JsonProperty("page")]
    public int Page { get; set; }

    /// <summary>
    /// Items per page
    /// </summary>
    [JsonProperty("pageSize")]
    public int PageSize { get; set; }

    /// <summary>
    /// Total pages
    /// </summary>
    [JsonProperty("pageCount")]
    public int PageCount { get; set; }

    /// <summary>
    /// Total entries
    /// </summary>
    [JsonProperty("totalCount")]
    public int TotalCount { get; set; }

    /// <summary>
    /// Orders
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnStakingOrder> List { get; set; } = [];
}
