namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesPriceTriggeredOrderRequest
/// </summary>
public record GateFuturesPriceTriggeredOrderRequest
{
    /// <summary>
    /// Initial Order
    /// </summary>
    [JsonProperty("initial")]
    public GateFuturesInitial Order { get; set; }

    /// <summary>
    /// Trigger
    /// </summary>
    [JsonProperty("trigger")]
    public GateFuturesTrigger Trigger { get; set; }

    /// <summary>
    /// Order Type
    /// </summary>
    [JsonProperty("order_type", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MapConverter))]
    public GateFuturesTriggerType? Type { get; set; }

    /// <summary>
    /// Position margin mode. Supported values are isolated and cross.
    /// </summary>
    [JsonProperty("pos_margin_mode", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MapConverter))]
    public GateFuturesPositionMarginMode? PositionMarginMode { get; set; }
}

/// <summary>
/// GateFuturesInitial
/// </summary>
public record GateFuturesInitial
{
    /// <summary>
    /// Futures contract
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Order size. Positive size means to buy, while negative one means to sell. Set to 0 to close the position
    /// </summary>
    [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
    public long? Size { get; set; }

    /// <summary>
    /// Order amount. Used for decimal contract size when supported.
    /// </summary>
    [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
    public string Amount { get; set; }

    /// <summary>
    /// Order price. Set to 0 to use market price
    /// </summary>
    [JsonProperty("price")]
    public string Price { get; set; }

    /// <summary>
    /// Set to true if trying to close the position
    /// </summary>
    [JsonProperty("close", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Close { get; set; }

    /// <summary>
    /// Time in force. If using market price, only ioc is supported.
    /// </summary>
    [JsonProperty("tif", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MapConverter))]
    public GateFuturesTimeInForce? TimeInForce { get; set; }
    
    /// <summary>
    /// The source of the order
    /// </summary>
    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; }
    
    /// <summary>
    /// Set to true to create a reduce-only order
    /// </summary>
    [JsonProperty("reduce_only", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Is the order reduce-only
    /// </summary>
    [JsonProperty("is_reduce_only", NullValueHandling = NullValueHandling.Ignore)]
    public bool? IsReduceOnly { get; set; }

    /// <summary>
    /// Is the order to close position
    /// </summary>
    [JsonProperty("is_close", NullValueHandling = NullValueHandling.Ignore)]
    public bool? IsClose { get; set; }
    
    /// <summary>
    /// Set side to close dual-mode position. close_long closes the long side; while close_short the short one. Note size also needs to be set to 0
    /// </summary>
    [JsonProperty("auto_size", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MapConverter))]
    public GateFuturesOrderAutoSize? AutoSize { get; set; }
}

/// <summary>
/// GateFuturesTrigger
/// </summary>
public record GateFuturesTrigger
{
    /// <summary>
    /// How the order will be triggered
    /// </summary>
    [JsonProperty("strategy_type", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesNumericTriggerEnumConverter))]
    public GateFuturesTriggerStrategy? StrategyType { get; set; }
    
    /// <summary>
    /// Price Type
    /// </summary>
    [JsonProperty("price_type", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesNumericTriggerEnumConverter))]
    public GateFuturesTriggerPrice? PriceType { get; set; }

    /// <summary>
    /// Trigger price
    /// </summary>
    [JsonProperty("price")]
    public string Price { get; set; }

    /// <summary>
    /// Price trigger condition
    /// </summary>
    [JsonProperty("rule"), JsonConverter(typeof(GateFuturesTriggerConditionConverter))]
    public GateSpotTriggerCondition Rule { get; set; }

    /// <summary>
    /// How long (in seconds) to wait for the condition to be triggered before cancelling the order.
    /// </summary>
    [JsonProperty("expiration", NullValueHandling = NullValueHandling.Ignore)]
    public int? Expiration { get; set; }
}
