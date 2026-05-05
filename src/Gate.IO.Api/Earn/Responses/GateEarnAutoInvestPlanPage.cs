namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest plan list
/// </summary>
public record GateEarnAutoInvestPlanPage
{
    /// <summary>
    /// Page number
    /// </summary>
    [JsonProperty("page")]
    public long Page { get; set; }

    /// <summary>
    /// Items per page
    /// </summary>
    [JsonProperty("page_size")]
    public long PageSize { get; set; }

    /// <summary>
    /// Total pages
    /// </summary>
    [JsonProperty("page_count")]
    public long PageCount { get; set; }

    /// <summary>
    /// Total entries
    /// </summary>
    [JsonProperty("total_count")]
    public long TotalCount { get; set; }

    /// <summary>
    /// Plans
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnAutoInvestPlan> List { get; set; } = [];
}
