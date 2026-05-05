namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx last trade price stream update.
/// </summary>
public record GateCrossExStreamLastPrice
{
    /// <summary>
    /// Trading pair symbol.
    /// </summary>
    [JsonProperty("s")]
    public string Symbol { get; set; }

    /// <summary>
    /// Last trade price.
    /// </summary>
    [JsonProperty("lp")]
    public decimal LastPrice { get; set; }
}
