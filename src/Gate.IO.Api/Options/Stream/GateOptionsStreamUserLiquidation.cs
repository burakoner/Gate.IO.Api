namespace Gate.IO.Api.Options;

/// <summary>
/// Represents the Gate Options Stream User Liquidation.
/// </summary>
public record GateOptionsStreamUserLiquidation
{
    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the Initial Margin.
    /// </summary>
    [JsonProperty("init_margin")]
    public decimal InitialMargin { get; set; }
    
    /// <summary>
    /// Gets or sets the Maintenance Margin.
    /// </summary>
    [JsonProperty("maint_margin")]
    public decimal MaintenanceMargin { get; set; }
    
    /// <summary>
    /// Gets or sets the Order Margin.
    /// </summary>
    [JsonProperty("order_margin")]
    public decimal OrderMargin { get; set; }

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
