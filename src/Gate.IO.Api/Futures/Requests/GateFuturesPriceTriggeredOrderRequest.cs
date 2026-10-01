namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesPriceTriggeredOrderRequest
/// </summary>
public record GateFuturesPriceTriggeredOrderRequest
{
    /// <summary>
    /// Required initial order. Futures creation validates this object before sending.
    /// </summary>
    [JsonProperty("initial", Required = Required.Always)]
    public GateFuturesInitial Order { get; set; }

    /// <summary>
    /// Required trigger configuration.
    /// </summary>
    [JsonProperty("trigger", Required = Required.Always)]
    public GateFuturesTrigger Trigger { get; set; }

    /// <summary>
    /// Optional order type. On Futures creation, CloseLongOrder and CloseShortOrder are read-only response types.
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
    [JsonProperty("contract", Required = Required.Always)]
    public string Contract { get; set; }

    /// <summary>
    /// Contract quantity. Zero denotes full closing. Planned partial closing uses positive size for short positions
    /// and negative size for long positions. Amount takes precedence when both quantity fields are supplied.
    /// </summary>
    [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
    public long? Size { get; set; }

    /// <summary>
    /// Decimal contract quantity when supported. Takes precedence over Size when both are supplied.
    /// The wrapper preserves both fields and does not choose a quantity or direction.
    /// </summary>
    [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
    public string Amount { get; set; }

    /// <summary>
    /// Order price. Set to 0 to use market price
    /// </summary>
    [JsonProperty("price", Required = Required.Always)]
    public string Price { get; set; }

    /// <summary>
    /// Set true for a full close in single-position mode. Partial closing or hedge mode can omit it or set false.
    /// The wrapper does not infer the account's position mode.
    /// </summary>
    [JsonProperty("close", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Close { get; set; }

    /// <summary>
    /// Price-triggered orders support gtc and ioc; omission defaults to gtc on the server.
    /// Market-price Futures creation requires explicit ioc; it is never selected automatically.
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
    /// Read-only response flag. For Futures creation use ReduceOnly instead; this field must be omitted.
    /// </summary>
    [JsonProperty("is_reduce_only", NullValueHandling = NullValueHandling.Ignore)]
    public bool? IsReduceOnly { get; set; }

    /// <summary>
    /// Read-only response flag. For Futures creation use Close instead; this field must be omitted.
    /// </summary>
    [JsonProperty("is_close", NullValueHandling = NullValueHandling.Ignore)]
    public bool? IsClose { get; set; }
    
    /// <summary>
    /// Hedge-mode full closing (quantity zero) requires close_long or close_short. Partial closing does not require it.
    /// Use null to omit this DTO field; Futures preflight rejects the empty-string None sentinel.
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
    /// Trigger strategy. The current contract describes 0 and 1 but explicitly supports only 0 for creation.
    /// Futures creation rejects ByPriceGap; response values can still be read.
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
    [JsonProperty("price", Required = Required.Always)]
    public string Price { get; set; }

    /// <summary>
    /// Price trigger condition
    /// </summary>
    [JsonProperty("rule", Required = Required.Always), JsonConverter(typeof(GateFuturesTriggerConditionConverter))]
    public GateSpotTriggerCondition Rule { get; set; }

    /// <summary>
    /// How long (in seconds) to wait for the condition to be triggered before cancelling the order.
    /// </summary>
    [JsonProperty("expiration", NullValueHandling = NullValueHandling.Ignore)]
    public int? Expiration { get; set; }
}
