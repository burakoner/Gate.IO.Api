namespace Gate.IO.Api.Options;

/// <summary>
/// Represents the Gate Options Stream Settlement.
/// </summary>
public record GateOptionsStreamSettlement
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Gets or sets the Order Book ID.
    /// </summary>
    [JsonProperty("orderbook_id")]
    public long OrderBookId { get; set; }

    /// <summary>
    /// Gets or sets the Position Size.
    /// </summary>
    [JsonProperty("position_size")]
    public long PositionSize { get; set; }

    /// <summary>
    /// Gets or sets the Profit.
    /// </summary>
    [JsonProperty("profit")]
    public decimal Profit { get; set; }

    /// <summary>
    /// Gets or sets the Settle Price.
    /// </summary>
    [JsonProperty("settle_price")]
    public decimal SettlePrice { get; set; }

    /// <summary>
    /// Gets or sets the Strike Price.
    /// </summary>
    [JsonProperty("strike_price")]
    public decimal StrikePrice { get; set; }

    /// <summary>
    /// Gets or sets the Period.
    /// </summary>
    [JsonProperty("tag")]
    public GateOptionsContractPeriod Period { get; set; }

    /// <summary>
    /// Gets or sets the Trade ID.
    /// </summary>
    [JsonProperty("trade_id")]
    public long TradeId { get; set; }
    
    /// <summary>
    /// Gets or sets the Trade Size.
    /// </summary>
    [JsonProperty("trade_size")]
    public long TradeSize { get; set; }
    
    /// <summary>
    /// Gets or sets the Underlying.
    /// </summary>
    [JsonProperty("underlying")]
    public string Underlying { get; set; }

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
}
