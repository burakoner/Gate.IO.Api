namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi historical position
/// </summary>
public record GateTradFiPositionHistory
{
    [JsonProperty("position_id")]
    public long PositionId { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("realized_pnl")]
    public decimal RealizedPnl { get; set; }

    [JsonProperty("realized_pnl_rate")]
    public decimal RealizedPnlRate { get; set; }

    [JsonProperty("volume")]
    public decimal Volume { get; set; }

    [JsonProperty("volume_closed")]
    public decimal ClosedVolume { get; set; }

    [JsonProperty("price_open")]
    public decimal OpenPrice { get; set; }

    [JsonProperty("position_dir"), JsonConverter(typeof(MapConverter))]
    public GateTradFiPositionDirection Direction { get; set; }

    [JsonProperty("price_tp")]
    public decimal TakeProfitPrice { get; set; }

    [JsonProperty("price_sl")]
    public decimal StopLossPrice { get; set; }

    /// <summary>
    /// Counterparty price can be returned as an empty string in historical responses.
    /// </summary>
    [JsonProperty("counterparty_price")]
    public string CounterpartyPrice { get; set; }

    [JsonProperty("close_price")]
    public decimal ClosePrice { get; set; }

    [JsonProperty("time_create")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    [JsonProperty("time_close")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CloseTime { get; set; }

    [JsonProperty("position_status")]
    public int PositionStatus { get; set; }

    [JsonProperty("close_detail")]
    public GateTradFiPositionCloseDetail CloseDetail { get; set; }

    [JsonProperty("realized_pnl_detail")]
    public GateTradFiPositionRealizedPnlDetail RealizedPnlDetail { get; set; }
}

/// <summary>
/// TradFi position liquidation details
/// </summary>
public record GateTradFiPositionCloseDetail
{
    [JsonProperty("margin_level")]
    public decimal MarginLevel { get; set; }

    [JsonProperty("margin")]
    public decimal Margin { get; set; }

    [JsonProperty("equity")]
    public decimal Equity { get; set; }

    [JsonProperty("stop_out_level")]
    public decimal StopOutLevel { get; set; }
}

/// <summary>
/// TradFi realized PnL details
/// </summary>
public record GateTradFiPositionRealizedPnlDetail
{
    [JsonProperty("closed_pnl")]
    public decimal ClosedPnl { get; set; }

    [JsonProperty("swap")]
    public decimal Swap { get; set; }

    [JsonProperty("fee")]
    public decimal Fee { get; set; }
}
