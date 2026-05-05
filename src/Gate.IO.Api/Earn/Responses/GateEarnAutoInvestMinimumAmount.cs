namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest minimum amount
/// </summary>
public record GateEarnAutoInvestMinimumAmount
{
    /// <summary>
    /// Minimum amount
    /// </summary>
    [JsonProperty("min_amount")]
    public decimal MinAmount { get; set; }
}
