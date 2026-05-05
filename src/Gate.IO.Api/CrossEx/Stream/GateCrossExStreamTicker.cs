namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx ticker stream update.
/// </summary>
public record GateCrossExStreamTicker
{
    /// <summary>
    /// Trading pair symbol.
    /// </summary>
    [JsonProperty("s")]
    public string Symbol { get; set; }

    /// <summary>
    /// Latest trade price.
    /// </summary>
    [JsonProperty("lp")]
    public decimal LastPrice { get; set; }

    /// <summary>
    /// Best bid price.
    /// </summary>
    [JsonProperty("bp")]
    public decimal BidPrice { get; set; }

    /// <summary>
    /// Best bid size.
    /// </summary>
    [JsonProperty("bs")]
    public decimal BidSize { get; set; }

    /// <summary>
    /// Best ask price.
    /// </summary>
    [JsonProperty("ap")]
    public decimal AskPrice { get; set; }

    /// <summary>
    /// Best ask size.
    /// </summary>
    [JsonProperty("as")]
    public decimal AskSize { get; set; }

    /// <summary>
    /// 24-hour open price.
    /// </summary>
    [JsonProperty("o")]
    public decimal Open24h { get; set; }

    /// <summary>
    /// 24-hour high price.
    /// </summary>
    [JsonProperty("h")]
    public decimal High24h { get; set; }

    /// <summary>
    /// 24-hour low price.
    /// </summary>
    [JsonProperty("l")]
    public decimal Low24h { get; set; }

    /// <summary>
    /// 24-hour base volume.
    /// </summary>
    [JsonProperty("v")]
    public decimal Volume24h { get; set; }

    /// <summary>
    /// 24-hour quote volume.
    /// </summary>
    [JsonProperty("q")]
    public decimal? QuoteVolume24h { get; set; }

    /// <summary>
    /// Exchange timestamp in milliseconds.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }
}
