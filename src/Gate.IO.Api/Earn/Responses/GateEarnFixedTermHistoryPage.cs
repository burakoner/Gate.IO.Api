namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn history page
/// </summary>
public record GateEarnFixedTermHistoryPage
{
    /// <summary>
    /// History records
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnFixedTermHistoryRecord> List { get; set; } = [];

    /// <summary>
    /// Total records
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }
}
