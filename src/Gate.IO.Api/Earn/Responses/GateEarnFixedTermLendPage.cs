namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn subscription order list
/// </summary>
public record GateEarnFixedTermLendPage
{
    /// <summary>
    /// Subscription order list
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnFixedTermLendOrder> List { get; set; } = [];

    /// <summary>
    /// Total records
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }
}
