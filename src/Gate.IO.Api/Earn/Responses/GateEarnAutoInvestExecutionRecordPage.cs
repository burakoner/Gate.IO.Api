namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest plan execution records
/// </summary>
public record GateEarnAutoInvestExecutionRecordPage
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
    [JsonProperty("total_page")]
    public long TotalPage { get; set; }

    /// <summary>
    /// Total entries
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }

    /// <summary>
    /// Execution records
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnAutoInvestExecutionRecord> List { get; set; } = [];
}
