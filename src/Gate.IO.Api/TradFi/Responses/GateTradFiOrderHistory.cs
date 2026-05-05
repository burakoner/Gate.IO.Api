namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi historical order
/// </summary>
public record GateTradFiOrderHistory : GateTradFiOrder
{
    /// <summary>
    /// Gets or sets the Operation Type.
    /// </summary>
    [JsonProperty("order_opt_type")]
    public int OperationType { get; set; }

    /// <summary>
    /// Gets or sets the Fill Volume.
    /// </summary>
    [JsonProperty("fill_volume")]
    public decimal FillVolume { get; set; }

    /// <summary>
    /// Gets or sets the Close PnL.
    /// </summary>
    [JsonProperty("close_pnl")]
    public decimal ClosePnl { get; set; }

    /// <summary>
    /// Gets or sets the Trigger Price.
    /// </summary>
    [JsonProperty("trigger_price")]
    public decimal TriggerPrice { get; set; }

    /// <summary>
    /// Gets or sets the Done Time.
    /// </summary>
    [JsonProperty("time_done")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime DoneTime { get; set; }
}
