namespace Gate.IO.Api.Alpha;

/// <summary>
/// Alpha currency ticker.
/// </summary>
public record GateAlphaTicker
{
    /// <summary>
    /// Currency symbol.
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Last trading price.
    /// </summary>
    [JsonProperty("last")]
    public decimal Last { get; set; }

    /// <summary>
    /// 24 hour price change percentage.
    /// </summary>
    [JsonProperty("change")]
    public decimal Change { get; set; }

    /// <summary>
    /// 24 hour trading volume in USDT.
    /// </summary>
    [JsonProperty("volume")]
    public decimal Volume { get; set; }

    /// <summary>
    /// Current token market capitalization.
    /// </summary>
    [JsonProperty("market_cap")]
    public decimal MarketCap { get; set; }
}
