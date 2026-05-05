namespace Gate.IO.Api.Options;

/// <summary>
/// Represents the Gate Options Stream Position Close.
/// </summary>
public record GateOptionsStreamPositionClose
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Gets or sets the PnL.
    /// </summary>
    [JsonProperty("pnl")]
    public decimal PNL { get; set; }

    /// <summary>
    /// Gets or sets the Settle Size.
    /// </summary>
    [JsonProperty("settle_size")]
    public long SettleSize { get; set; }
    
    /// <summary>
    /// Gets or sets the Side.
    /// </summary>
    [JsonProperty("side")]
    public GateOptionsSide Side { get; set; }

    /// <summary>
    /// Gets or sets the Client Order ID.
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Gets or sets the Underlying.
    /// </summary>
    [JsonProperty("underlying")]
    public string Underlying { get; set; }

    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the Time.
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Gets or sets the Time Milliseconds.
    /// </summary>
    [JsonProperty("time_ms")]
    public long TimeMilliseconds { get; set; }
}
