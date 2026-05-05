namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi historical order
/// </summary>
public record GateTradFiOrderHistory : GateTradFiOrder
{
    [JsonProperty("order_opt_type")]
    public int OperationType { get; set; }

    [JsonProperty("fill_volume")]
    public decimal FillVolume { get; set; }

    [JsonProperty("close_pnl")]
    public decimal ClosePnl { get; set; }

    [JsonProperty("trigger_price")]
    public decimal TriggerPrice { get; set; }

    [JsonProperty("time_done")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime DoneTime { get; set; }
}
