namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesPriceTriggeredOrderRequest
/// </summary>
public class GateFuturesPriceTriggeredOrderRequest
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
    [JsonProperty("order_type", NullValueHandling = NullValueHandling.Ignore)]
    public GateFuturesTriggerType? Type { get; set; }
}

/// <summary>
/// GateFuturesTrigger
/// </summary>
public class GateFuturesTrigger
{
    /// <summary>
    /// How the order will be triggered
    /// </summary>
    [JsonProperty("strategy_type")]
    public GateFuturesTriggerStrategy StrategyType { get; set; }
    
    /// <summary>
    /// Price Type
    /// </summary>
    [JsonProperty("price_type")]
    public GateFuturesTriggerPrice PriceType { get; set; }

    /// <summary>
    /// Trigger price
    /// </summary>
    [JsonProperty("price")]
    public string Price { get; set; }

    /// <summary>
    /// Price trigger condition
    /// </summary>
    [JsonProperty("rule")]
    public GateSpotTriggerCondition Rule { get; set; }

    /// <summary>
    /// How long (in seconds) to wait for the condition to be triggered before cancelling the order.
    /// </summary>
    [JsonProperty("expiration")]
    public int Expiration { get; set; }
}

/// <summary>
/// GateFuturesInitial
/// </summary>
public class GateFuturesInitial
{
    /// <summary>
    /// Futures contract
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Order size. Positive size means to buy, while negative one means to sell. Set to 0 to close the position
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }

    /// <summary>
    /// Order price. Set to 0 to use market price
    /// </summary>
    [JsonProperty("price")]
    public string Price { get; set; }

    /// <summary>
    /// Set to true if trying to close the position
    /// </summary>
    [JsonProperty("close")]
    public bool Close { get; set; }

    /// <summary>
    /// Time in force. If using market price, only ioc is supported.
    /// </summary>
    [JsonProperty("tif")]
    public GateFuturesTimeInForce TimeInForce { get; set; }
    
    /// <summary>
    /// The source of the order
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }
    
    /// <summary>
    /// Set to true to create a reduce-only order
    /// </summary>
    [JsonProperty("reduce_only")]
    public bool ReduceOnly { get; set; }
    
    /// <summary>
    /// Set side to close dual-mode position. close_long closes the long side; while close_short the short one. Note size also needs to be set to 0
    /// </summary>
    [JsonProperty("auto_size")]
    public GateFuturesOrderAutoSize AutoSize { get; set; }
}