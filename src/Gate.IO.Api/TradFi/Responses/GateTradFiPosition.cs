namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi active position
/// </summary>
public record GateTradFiPosition
{
    /// <summary>
    /// Gets or sets the Position ID.
    /// </summary>
    [JsonProperty("position_id")]
    public long PositionId { get; set; }

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Description.
    /// </summary>
    [JsonProperty("symbol_desc")]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the Margin.
    /// </summary>
    [JsonProperty("margin")]
    public decimal Margin { get; set; }

    /// <summary>
    /// Gets or sets the Unrealized PnL.
    /// </summary>
    [JsonProperty("unrealized_pnl")]
    public decimal UnrealizedPnl { get; set; }

    /// <summary>
    /// Gets or sets the Unrealized PnL Rate.
    /// </summary>
    [JsonProperty("unrealized_pnl_rate")]
    public decimal UnrealizedPnlRate { get; set; }

    /// <summary>
    /// Gets or sets the Volume.
    /// </summary>
    [JsonProperty("volume")]
    public decimal Volume { get; set; }

    /// <summary>
    /// Gets or sets the Open Price.
    /// </summary>
    [JsonProperty("price_open")]
    public decimal OpenPrice { get; set; }

    /// <summary>
    /// Gets or sets the Direction.
    /// </summary>
    [JsonProperty("position_dir"), JsonConverter(typeof(MapConverter))]
    public GateTradFiPositionDirection Direction { get; set; }

    /// <summary>
    /// Gets or sets the Take Profit Price.
    /// </summary>
    [JsonProperty("price_tp")]
    public decimal TakeProfitPrice { get; set; }

    /// <summary>
    /// Gets or sets the Stop Loss Price.
    /// </summary>
    [JsonProperty("price_sl")]
    public decimal StopLossPrice { get; set; }

    /// <summary>
    /// Gets or sets the Counterparty Price.
    /// </summary>
    [JsonProperty("counterparty_price")]
    public decimal CounterpartyPrice { get; set; }

    /// <summary>
    /// Gets or sets the Create Time.
    /// </summary>
    [JsonProperty("time_create")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
}
