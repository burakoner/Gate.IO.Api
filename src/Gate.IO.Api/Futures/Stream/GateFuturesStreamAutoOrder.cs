namespace Gate.IO.Api.Futures;

/// <summary>
/// Represents the Gate Futures Stream Auto Order.
/// </summary>
public record GateFuturesStreamAutoOrder
{
    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the Order ID.
    /// </summary>
    [JsonProperty("id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Gets or sets the Trade ID.
    /// </summary>
    [JsonProperty("trade_id")]
    public long? TradeId { get; set; }

    /// <summary>
    /// Gets or sets the Status.
    /// </summary>
    [JsonProperty("status")]
    public GateFuturesPriceTriggerStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the Reason.
    /// </summary>
    [JsonProperty("reason")]
    public string Reason { get; set; }

    /// <summary>
    /// Gets or sets the Create Time.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Gets or sets the Name.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets the Is Stop Order.
    /// </summary>
    [JsonProperty("is_stop_order")]
    public bool IsStopOrder { get; set; }
    
    /// <summary>
    /// Gets or sets the Stop Trigger.
    /// </summary>
    [JsonProperty("stop_trigger")]
    public GateFuturesStreamAutoOrderStopTrigger StopTrigger { get; set; }

    /// <summary>
    /// Gets or sets the Trigger.
    /// </summary>
    [JsonProperty("trigger")]
    public GateFuturesStreamAutoOrderTrigger Trigger { get; set; }

    /// <summary>
    /// Gets or sets the Order.
    /// </summary>
    [JsonProperty("initial")]
    public GateFuturesStreamAutoOrderInitial Order { get; set; }

    /// <summary>
    /// Gets or sets the Type.
    /// </summary>
    [JsonProperty("order_type", NullValueHandling = NullValueHandling.Ignore)]
    public GateFuturesTriggerType? Type { get; set; }

    /// <summary>
    /// Gets or sets the Me Order ID.
    /// </summary>
    [JsonProperty("me_order_id")]
    public long MeOrderId { get; set; }
}

/// <summary>
/// Represents the Gate Futures Stream Auto Order Stop Trigger.
/// </summary>
public record GateFuturesStreamAutoOrderStopTrigger
{
    /// <summary>
    /// Gets or sets the Rule.
    /// </summary>
    [JsonProperty("rule")]
    public GateSpotTriggerCondition? Rule { get; set; }

    /// <summary>
    /// Gets or sets the Trigger Price.
    /// </summary>
    [JsonProperty("trigger_price")]
    public decimal? TriggerPrice { get; set; }

    /// <summary>
    /// Gets or sets the Order Price.
    /// </summary>
    [JsonProperty("order_price")]
    public decimal? OrderPrice { get; set; }
}

/// <summary>
/// Represents the Gate Futures Stream Auto Order Trigger.
/// </summary>
public record GateFuturesStreamAutoOrderTrigger
{
    /// <summary>
    /// Gets or sets the Strategy Type.
    /// </summary>
    [JsonProperty("strategy_type")]
    public GateFuturesTriggerStrategy StrategyType { get; set; }

    /// <summary>
    /// Gets or sets the Price Type.
    /// </summary>
    [JsonProperty("price_type")]
    public GateFuturesTriggerPrice PriceType { get; set; }

    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    [JsonProperty("price")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Gets or sets the Rule.
    /// </summary>
    [JsonProperty("rule")]
    public GateSpotTriggerCondition? Rule { get; set; }

    /// <summary>
    /// Gets or sets the Expiration.
    /// </summary>
    [JsonProperty("expiration")]
    public int Expiration { get; set; }
}

/// <summary>
/// Represents the Gate Futures Stream Auto Order Initial.
/// </summary>
public record GateFuturesStreamAutoOrderInitial
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Gets or sets the Size.
    /// </summary>
    [JsonProperty("size")]
    public long? Size { get; set; }

    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    [JsonProperty("price")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Gets or sets the Time In Force.
    /// </summary>
    [JsonProperty("tif")]
    public GateFuturesTimeInForce? TimeInForce { get; set; }

    /// <summary>
    /// Gets or sets the Client Order ID.
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Gets or sets the Iceberg.
    /// </summary>
    [JsonProperty("iceberg")]
    public long? Iceberg { get; set; }
    
    /// <summary>
    /// Gets or sets the Is Close.
    /// </summary>
    [JsonProperty("is_close")]
    public bool? IsClose { get; set; }
    
    /// <summary>
    /// Gets or sets the Is Reduce Only.
    /// </summary>
    [JsonProperty("is_reduce_only")]
    public bool? IsReduceOnly { get; set; }
    
    /// <summary>
    /// Gets or sets the Auto Size.
    /// </summary>
    [JsonProperty("auto_size")]
    public long? AutoSize { get; set; }
}
