namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate Margin Uni Order
/// </summary>
public record GateMarginOrder
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public GateMarginUniOrderType Type { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Repaid All
    /// </summary>
    [JsonProperty("repaid_all")]
    public string RepaidAll { get; set; }
}
