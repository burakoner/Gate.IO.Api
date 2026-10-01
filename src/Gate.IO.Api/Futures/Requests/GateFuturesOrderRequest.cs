namespace Gate.IO.Api.Futures;

/// <summary>
/// Standard Futures creation instructions. Saved JSON requires contract, size and price;
/// explicit numeric values must fit decimal/Int64 exactly rather than being rounded or discarded.
/// </summary>
public record GateFuturesOrderRequest
{
    /// <summary>
    /// Futures contract
    /// </summary>
    [JsonProperty("contract", Required = Required.Always)]
    public string Contract { get; set; }

    /// <summary>
    /// Number of contracts, not currency units. Positive buys; negative sells. Full close uses zero with explicit closing flags.
    /// </summary>
    [JsonProperty("size", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal Size { get; set; }

    /// <summary>
    /// Display size for iceberg order. 0 for non-iceberg. Note that you will have to pay the taker fee for the hidden size
    /// </summary>
    [JsonProperty("iceberg", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? Iceberg { get; set; }

    /// <summary>
    /// Order price. 0 for market order with &#x60;tif&#x60; set as &#x60;ioc&#x60;
    /// </summary>
    [JsonProperty("price", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal Price { get; set; }

    /// <summary>
    /// Set as &#x60;true&#x60; to close the position, with &#x60;size&#x60; set to 0
    /// </summary>
    [JsonProperty("close", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Close { get; set; }

    /// <summary>
    /// Set as &#x60;true&#x60; to be reduce-only order
    /// </summary>
    [JsonProperty("reduce_only", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Time in force  - gtc: GoodTillCancelled - ioc: ImmediateOrCancelled, taker only - poc: PendingOrCancelled, makes a post-only order that always enjoys a maker fee - fok: FillOrKill, fill either completely or none
    /// </summary>
    [JsonProperty("tif", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesPriceOrderMapConverter))]
    public GateFuturesTimeInForce? TimeInForce { get; set; }

    /// <summary>
    /// User defined information. If not empty, must follow the rules below:  
    /// 1. prefixed with t-
    /// 2. no longer than 28 bytes without t- prefix
    /// 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.)
    /// </summary>
    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; } = null;

    /// <summary>
    /// Full hedge-mode close side: close_long or close_short, with size=0 and reduce_only=true.
    /// Omit with null, not None. The client does not select the account's position mode.
    /// </summary>
    [JsonProperty("auto_size", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesPriceOrderMapConverter))]
    public GateFuturesOrderAutoSize? AutoSize { get; set; }

    /// <summary>
    /// Self-Trading Prevention Action. Users can use this field to set self-trade prevetion strategies
    /// </summary>
    [JsonProperty("stp_act", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesPriceOrderMapConverter))]
    public GateFuturesSelfTradeAction? SelfTradeAction { get; set; }

    /// <summary>
    /// Position ID. This field is write-only.
    /// </summary>
    [JsonProperty("pid", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long? PositionId { get; set; }

    /// <summary>
    /// The maximum slippage allowed for market orders, based on the latest market price
    /// </summary>
    [JsonProperty("market_order_slip_ratio", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? MarketOrderSlipRatio { get; set; }

    /// <summary>
    /// Position margin mode.
    /// </summary>
    [JsonProperty("pos_margin_mode", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesPriceOrderMapConverter))]
    public GateFuturesPositionMarginMode? PositionMarginMode { get; set; }

    /// <summary>
    /// Controls how much order data is returned.
    /// </summary>
    [JsonProperty("action_mode", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesPriceOrderMapConverter))]
    public GateFuturesActionMode? ActionMode { get; set; }

    /// <summary>
    /// Take-profit trigger price.
    /// </summary>
    [JsonProperty("tpsl_tp_trigger_price", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Stop-loss trigger price.
    /// </summary>
    [JsonProperty("tpsl_sl_trigger_price", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// Take-profit BBO price type.
    /// </summary>
    [JsonProperty("tpsl_tp_bbo_type", NullValueHandling = NullValueHandling.Ignore)]
    public string TakeProfitBboType { get; set; }

    /// <summary>
    /// Stop-loss BBO price type.
    /// </summary>
    [JsonProperty("tpsl_sl_bbo_type", NullValueHandling = NullValueHandling.Ignore)]
    public string StopLossBboType { get; set; }
}
