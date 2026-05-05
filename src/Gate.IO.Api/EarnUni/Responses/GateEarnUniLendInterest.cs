namespace Gate.IO.Api.EarnUni;

/// <summary>
/// EarnUni lending interest income
/// </summary>
public record GateEarnUniLendInterest
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Interest income
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }
}
