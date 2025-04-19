namespace Gate.IO.Api.Margin;

/// <summary>
/// Margin Account Balance
/// </summary>
public record GateMarginBalance
{
    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Whether account is locked
    /// </summary>
    [JsonProperty("locked")]
    public bool Locked { get; set; }

    /// <summary>
    /// Current risk rate of margin account
    /// </summary>
    [JsonProperty("risk")]
    public decimal Risk { get; set; }

    /// <summary>
    /// Account currency details
    /// </summary>
    [JsonProperty("base")]
    public GateMarginBalanceData Base { get; set; }

    /// <summary>
    /// Account currency details
    /// </summary>
    [JsonProperty("quote")]
    public GateMarginBalanceData Quote { get; set; }
}