namespace Gate.IO.Api.Futures;

/// <summary>
/// Represents the Gate Futures User Deleverage.
/// </summary>
public record GateFuturesUserDeleverage
{
    /// <summary>
    /// Gets or sets the Entry Price.
    /// </summary>
    [JsonProperty("entry_price")]
    public decimal EntryPrice { get; set; }

    /// <summary>
    /// Gets or sets the Fill Price.
    /// </summary>
    [JsonProperty("fill_price")]
    public decimal FillPrice { get; set; }

    /// <summary>
    /// Gets or sets the Position Size.
    /// </summary>
    [JsonProperty("position_size")]
    public decimal PositionSize { get; set; }

    /// <summary>
    /// Gets or sets the Trade Size.
    /// </summary>
    [JsonProperty("trade_size")]
    public decimal TradeSize { get; set; }

    /// <summary>
    /// Gets or sets the Time.
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Gets or sets the Time In Milliseconds.
    /// </summary>
    [JsonProperty("time_ms")]
    public long TimeInMilliseconds { get; set; }

    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user")]
    public long UserId { get; set; }
}
