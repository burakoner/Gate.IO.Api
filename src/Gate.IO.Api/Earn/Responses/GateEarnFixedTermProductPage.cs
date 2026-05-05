namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn product list
/// </summary>
public record GateEarnFixedTermProductPage
{
    /// <summary>
    /// Product list
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnFixedTermProduct> List { get; set; } = [];

    /// <summary>
    /// Total records
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }
}
