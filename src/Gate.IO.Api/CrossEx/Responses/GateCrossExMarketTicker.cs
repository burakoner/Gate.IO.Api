namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx exchange market ticker
/// </summary>
public record GateCrossExMarketTicker
{
    /// <summary>
    /// Trading pair
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Last price
    /// </summary>
    [JsonProperty("last_price"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// 24-hour opening price
    /// </summary>
    [JsonProperty("open_24h"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal? Open24h { get; set; }

    /// <summary>
    /// 24-hour low price
    /// </summary>
    [JsonProperty("low_24h"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal? Low24h { get; set; }

    /// <summary>
    /// 24-hour high price
    /// </summary>
    [JsonProperty("high_24h"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal? High24h { get; set; }

    /// <summary>
    /// 24-hour trading volume in base currency
    /// </summary>
    [JsonProperty("volume_24h_base"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal? Volume24hBase { get; set; }

    /// <summary>
    /// 24-hour trading volume in quote currency
    /// </summary>
    [JsonProperty("volume_24h_quote"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal? Volume24hQuote { get; set; }

    /// <summary>
    /// Mark price
    /// </summary>
    [JsonProperty("mark_price"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal? MarkPrice { get; set; }

    /// <summary>
    /// Index price
    /// </summary>
    [JsonProperty("index_price"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal? IndexPrice { get; set; }

    /// <summary>
    /// Open interest
    /// </summary>
    [JsonProperty("open_interest"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal? OpenInterest { get; set; }

    /// <summary>
    /// Open interest in quote currency
    /// </summary>
    [JsonProperty("open_interest_quote"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal? OpenInterestQuote { get; set; }

    /// <summary>
    /// Update timestamp
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }
}
