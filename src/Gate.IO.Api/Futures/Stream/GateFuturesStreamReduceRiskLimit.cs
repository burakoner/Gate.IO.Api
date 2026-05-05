namespace Gate.IO.Api.Futures;

/// <summary>
/// Represents the Gate Futures Stream Reduce Risk Limit.
/// </summary>
public record GateFuturesStreamReduceRiskLimit
{
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
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the Cancel Orders.
    /// </summary>
    [JsonProperty("cancel_orders")]
    public int CancelOrders { get; set; }

    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Gets or sets the Leverage Max.
    /// </summary>
    [JsonProperty("leverage_max")]
    public decimal LeverageMax { get; set; }

    /// <summary>
    /// Gets or sets the Liq Price.
    /// </summary>
    [JsonProperty("liq_price")]
    public decimal LiqPrice { get; set; }

    /// <summary>
    /// Gets or sets the Maintenance Rate.
    /// </summary>
    [JsonProperty("maintenance_rate")]
    public decimal MaintenanceRate { get; set; }

    /// <summary>
    /// Gets or sets the Risk Limit.
    /// </summary>
    [JsonProperty("risk_limit")]
    public decimal RiskLimit { get; set; }
}
