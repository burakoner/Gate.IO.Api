namespace Gate.IO.Api.Options;

/// <summary>
/// Represents the Gate Options Stream Position.
/// </summary>
public record GateOptionsStreamPosition
{
    /// <summary>
    /// Gets or sets the Entry Price.
    /// </summary>
    [JsonProperty("entry_price")]
    public decimal EntryPrice { get; set; }

    /// <summary>
    /// Gets or sets the Realised PnL.
    /// </summary>
    [JsonProperty("realised_pnl")]
    public decimal RealisedPnl { get; set; }

    /// <summary>
    /// Gets or sets the Size.
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }

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
