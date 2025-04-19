namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsUnderlyingTicker
/// </summary>
public record GateOptionsUnderlyingTicker
{
    /// <summary>
    /// Total put options trades amount in last 24h
    /// </summary>
    [JsonProperty("trade_put")]
    public long TradePut { get; set; }

    /// <summary>
    /// Total call options trades amount in last 24h
    /// </summary>
    [JsonProperty("trade_call")]
    public long TradeCall { get; set; }

    /// <summary>
    /// Index price (quote currency)
    /// </summary>
    [JsonProperty("index_price")]
    public decimal IndexPrice { get; set; }
}