namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi active position
/// </summary>
public record GateTradFiPosition
{
    [JsonProperty("position_id")]
    public long PositionId { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("symbol_desc")]
    public string Description { get; set; }

    [JsonProperty("margin")]
    public decimal Margin { get; set; }

    [JsonProperty("unrealized_pnl")]
    public decimal UnrealizedPnl { get; set; }

    [JsonProperty("unrealized_pnl_rate")]
    public decimal UnrealizedPnlRate { get; set; }

    [JsonProperty("volume")]
    public decimal Volume { get; set; }

    [JsonProperty("price_open")]
    public decimal OpenPrice { get; set; }

    [JsonProperty("position_dir"), JsonConverter(typeof(MapConverter))]
    public GateTradFiPositionDirection Direction { get; set; }

    [JsonProperty("price_tp")]
    public decimal TakeProfitPrice { get; set; }

    [JsonProperty("price_sl")]
    public decimal StopLossPrice { get; set; }

    [JsonProperty("counterparty_price")]
    public decimal CounterpartyPrice { get; set; }

    [JsonProperty("time_create")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
}
