namespace Gate.IO.Api.Options;

/// <summary>
/// Represents the Gate Options Stream User Settlement.
/// </summary>
public record GateOptionsStreamUserSettlement
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Gets or sets the Realised PnL.
    /// </summary>
    [JsonProperty("realised_pnl")]
    public decimal RealisedPnl { get; set; }

    /// <summary>
    /// Gets or sets the Settle Price.
    /// </summary>
    [JsonProperty("settle_price")]
    public decimal SettlePrice { get; set; }

    /// <summary>
    /// Gets or sets the Settlement Profit.
    /// </summary>
    [JsonProperty("settle_profit")]
    public decimal SettlementProfit { get; set; }

    /// <summary>
    /// Gets or sets the Size.
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }

    /// <summary>
    /// Gets or sets the Strike Price.
    /// </summary>
    [JsonProperty("strike_price")]
    public decimal StrikePrice { get; set; }

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
