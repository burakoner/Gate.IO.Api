namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx order
/// </summary>
public record GateCrossExOrder
{
    /// <summary>
    /// User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    /// <summary>
    /// Order ID.
    /// </summary>
    [JsonProperty("order_id")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client-defined order ID.
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("client_order_id")]
    private string ClientOrderId { set => Text = value; }

    /// <summary>
    /// Order state: NEW, OPEN, PARTIALLY_FILLED, FILLED, FAIL, or REJECT.
    /// </summary>
    [JsonProperty("state")]
    public string State { get; set; }

    /// <summary>
    /// Unique trading pair identifier, for example BINANCE_SPOT_BTC_USDT.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Order side: BUY or SELL.
    /// </summary>
    [JsonProperty("side")]
    public string Side { get; set; }

    /// <summary>
    /// Order type: LIMIT or MARKET.
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// Order attribute: COMMON, LIQ, REDUCE, ADL, or SETTLEMENT.
    /// </summary>
    [JsonProperty("attribute")]
    public string Attribute { get; set; }

    /// <summary>
    /// Venue: BINANCE, OKX, GATE, BYBIT, KRAKEN, HYPERLIQUID, or DERIBIT.
    /// </summary>
    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    /// <summary>
    /// Business type: SPOT, FUTURE, or MARGIN.
    /// </summary>
    [JsonProperty("business_type")]
    public string BusinessType { get; set; }

    /// <summary>
    /// Order quantity in the base currency.
    /// </summary>
    [JsonProperty("qty")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Order quantity in the quote currency.
    /// </summary>
    [JsonProperty("quote_qty")]
    public decimal? QuoteQuantity { get; set; }

    /// <summary>
    /// Order price.
    /// </summary>
    [JsonProperty("price")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Time-in-force policy: GTC, IOC, FOK, POC, or RPI.
    /// </summary>
    [JsonProperty("time_in_force")]
    public string TimeInForce { get; set; }

    [JsonProperty("tif")]
    private string TimeInForceAlias { set => TimeInForce = value; }

    /// <summary>
    /// Filled base amount.
    /// </summary>
    [JsonProperty("executed_qty")]
    public decimal? ExecutedQuantity { get; set; }

    /// <summary>
    /// Filled quote amount.
    /// </summary>
    [JsonProperty("executed_amount")]
    public decimal? ExecutedAmount { get; set; }

    /// <summary>
    /// Average filled price.
    /// </summary>
    [JsonProperty("executed_avg_price")]
    public decimal? ExecutedAveragePrice { get; set; }

    /// <summary>
    /// Fee currency.
    /// </summary>
    [JsonProperty("fee_coin")]
    public string FeeCoin { get; set; }

    [JsonProperty("fee_currency")]
    private string FeeCurrency { set => FeeCoin = value; }

    /// <summary>
    /// Fee amount.
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Whether this is a reduce-only order.
    /// </summary>
    [JsonProperty("reduce_only")]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Order leverage multiplier.
    /// </summary>
    [JsonProperty("leverage")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Failure reason description.
    /// </summary>
    [JsonProperty("reason")]
    public string Reason { get; set; }

    /// <summary>
    /// Base quantity of the latest fill.
    /// </summary>
    [JsonProperty("last_executed_qty")]
    public decimal? LastExecutedQuantity { get; set; }

    /// <summary>
    /// Price of the latest fill.
    /// </summary>
    [JsonProperty("last_executed_price")]
    public decimal? LastExecutedPrice { get; set; }

    /// <summary>
    /// Quote amount of the latest fill.
    /// </summary>
    [JsonProperty("last_executed_amount")]
    public decimal? LastExecutedAmount { get; set; }

    /// <summary>
    /// Position side: NONE, LONG, or SHORT.
    /// </summary>
    [JsonProperty("position_side")]
    public string PositionSide { get; set; }

    /// <summary>
    /// Created time.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// Updated time.
    /// </summary>
    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }
}
