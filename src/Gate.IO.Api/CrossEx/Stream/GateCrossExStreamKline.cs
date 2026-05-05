namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx kline stream update.
/// </summary>
public record GateCrossExStreamKline
{
    /// <summary>
    /// Trading pair symbol.
    /// </summary>
    [JsonProperty("s")]
    public string Symbol { get; set; }

    /// <summary>
    /// Open price.
    /// </summary>
    [JsonProperty("o")]
    public decimal OpenPrice { get; set; }

    /// <summary>
    /// High price.
    /// </summary>
    [JsonProperty("h")]
    public decimal HighPrice { get; set; }

    /// <summary>
    /// Low price.
    /// </summary>
    [JsonProperty("l")]
    public decimal LowPrice { get; set; }

    /// <summary>
    /// Close price.
    /// </summary>
    [JsonProperty("c")]
    public decimal ClosePrice { get; set; }

    /// <summary>
    /// Volume.
    /// </summary>
    [JsonProperty("v")]
    public decimal Volume { get; set; }

    /// <summary>
    /// Kline start time in milliseconds.
    /// </summary>
    [JsonProperty("t")]
    public long StartTime { get; set; }

    /// <summary>
    /// Kline end time in milliseconds.
    /// </summary>
    [JsonProperty("T")]
    public long EndTime { get; set; }

    /// <summary>
    /// Whether the kline is closed.
    /// </summary>
    [JsonProperty("x")]
    public bool IsClosed { get; set; }
}
