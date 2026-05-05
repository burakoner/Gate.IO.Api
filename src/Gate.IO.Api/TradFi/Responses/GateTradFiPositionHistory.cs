namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi historical position
/// </summary>
public record GateTradFiPositionHistory
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
    /// Gets or sets the Realized PnL.
    /// </summary>
    [JsonProperty("realized_pnl")]
    public decimal RealizedPnl { get; set; }

    /// <summary>
    /// Gets or sets the Realized PnL Rate.
    /// </summary>
    [JsonProperty("realized_pnl_rate")]
    public decimal RealizedPnlRate { get; set; }

    /// <summary>
    /// Gets or sets the Volume.
    /// </summary>
    [JsonProperty("volume")]
    public decimal Volume { get; set; }

    /// <summary>
    /// Gets or sets the Closed Volume.
    /// </summary>
    [JsonProperty("volume_closed")]
    public decimal ClosedVolume { get; set; }

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
    /// Counterparty price can be returned as an empty string in historical responses.
    /// </summary>
    [JsonProperty("counterparty_price")]
    public string CounterpartyPrice { get; set; }

    /// <summary>
    /// Gets or sets the Close Price.
    /// </summary>
    [JsonProperty("close_price")]
    public decimal ClosePrice { get; set; }

    /// <summary>
    /// Gets or sets the Create Time.
    /// </summary>
    [JsonProperty("time_create")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Gets or sets the Close Time.
    /// </summary>
    [JsonProperty("time_close")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CloseTime { get; set; }

    /// <summary>
    /// Gets or sets the Position Status.
    /// </summary>
    [JsonProperty("position_status")]
    public int PositionStatus { get; set; }

    /// <summary>
    /// Gets or sets the Close Detail.
    /// </summary>
    [JsonProperty("close_detail")]
    public GateTradFiPositionCloseDetail CloseDetail { get; set; }

    /// <summary>
    /// Gets or sets the Realized PnL Detail.
    /// </summary>
    [JsonProperty("realized_pnl_detail")]
    public GateTradFiPositionRealizedPnlDetail RealizedPnlDetail { get; set; }
}

/// <summary>
/// TradFi position liquidation details
/// </summary>
public record GateTradFiPositionCloseDetail
{
    /// <summary>
    /// Gets or sets the Margin Level.
    /// </summary>
    [JsonProperty("margin_level")]
    public decimal MarginLevel { get; set; }

    /// <summary>
    /// Gets or sets the Margin.
    /// </summary>
    [JsonProperty("margin")]
    public decimal Margin { get; set; }

    /// <summary>
    /// Gets or sets the Equity.
    /// </summary>
    [JsonProperty("equity")]
    public decimal Equity { get; set; }

    /// <summary>
    /// Gets or sets the Stop Out Level.
    /// </summary>
    [JsonProperty("stop_out_level")]
    public decimal StopOutLevel { get; set; }
}

/// <summary>
/// TradFi realized PnL details
/// </summary>
public record GateTradFiPositionRealizedPnlDetail
{
    /// <summary>
    /// Gets or sets the Closed PnL.
    /// </summary>
    [JsonProperty("closed_pnl")]
    public decimal ClosedPnl { get; set; }

    /// <summary>
    /// Gets or sets the Swap.
    /// </summary>
    [JsonProperty("swap")]
    public decimal Swap { get; set; }

    /// <summary>
    /// Gets or sets the Fee.
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }
}
