namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking award list
/// </summary>
public record GateEarnStakingAwardPage
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
    /// Awards
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnStakingAward> List { get; set; } = [];
}
