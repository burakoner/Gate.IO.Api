namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotTriggerOrderRequest
/// </summary>
public record GateSpotPriceTriggeredOrderRequest
{
    /// <summary>
    /// Gets or Sets Trigger
    /// </summary>
    [JsonProperty("trigger")]
    public GateSpotTriggerPrice Trigger { get; set; }

    /// <summary>
    /// Gets or Sets Put
    /// </summary>
    [JsonProperty("put")]
    public GateSpotTriggerOrder Order { get; set; }

    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("market")]
    public string Symbol { get; set; }
}

/// <summary>
/// GateSpotTriggerPrice
/// </summary>
public record GateSpotTriggerPrice
{
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
/// GateSpotTriggerOrder
/// </summary>
public record GateSpotTriggerOrder
{
    /// <summary>
    /// Order Type
    /// </summary>
    [JsonProperty("type")]
    public GateSpotOrderType Type { get; set; }
    
    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side")]
    public GateSpotOrderSide Side { get; set; }
    
    /// <summary>
    /// Order price
    /// </summary>
    [JsonProperty("price")]
    public string Price { get; set; }

    /// <summary>
    /// When type is limit, it refers to base currency. For instance, BTC_USDT means BTC
    /// When type is market, it refers to different currency according to side
    /// - side : buy means quote currency, BTC_USDT means USDT
    /// - side : sell means base currency，BTC_USDT means BTC
    /// </summary>
    [JsonProperty("amount")]
    public string Amount { get; set; }

    /// <summary>
    /// Trading account type. Portfolio margin account must set to cross_margin
    /// - normal: spot trading
    /// - margin: margin trading
    /// - cross_margin: cross_margin trading
    /// </summary>
    [JsonProperty("account")]
    public GateSpotAccountType Account { get; set; }
    
    /// <summary>
    /// time_in_force
    /// - gtc: GoodTillCancelled
    /// - ioc: ImmediateOrCancelled, taker only
    /// </summary>
    [JsonProperty("time_in_force", NullValueHandling = NullValueHandling.Ignore)]
    public GateSpotTriggerTimeInForce? TimeInForce { get; set; }

    /// <summary>
    /// User defined information. If not empty, must follow the rules below:  
    /// 1. prefixed with t-
    /// 2. no longer than 28 bytes without t- prefix
    /// 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.)
    /// </summary>
    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; }
}
