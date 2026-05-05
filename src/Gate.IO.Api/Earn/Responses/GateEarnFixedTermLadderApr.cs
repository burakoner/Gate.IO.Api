namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn ladder APR
/// </summary>
public record GateEarnFixedTermLadderApr
{
    /// <summary>
    /// Annualized interest rate
    /// </summary>
    [JsonProperty("apr")]
    public decimal Apr { get; set; }

    /// <summary>
    /// Range lower limit
    /// </summary>
    [JsonProperty("left")]
    public decimal Left { get; set; }

    /// <summary>
    /// Range upper limit
    /// </summary>
    [JsonProperty("right")]
    public decimal Right { get; set; }
}
