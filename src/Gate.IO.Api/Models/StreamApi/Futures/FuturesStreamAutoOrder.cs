namespace Gate.IO.Api.Models.StreamApi.Futures;

public class FuturesStreamAutoOrder
{
    [JsonProperty("user")]
    public int UserId { get; set; }

    [JsonProperty("id")]
    public long OrderId { get; set; }

    [JsonProperty("trade_id")]
    public long? TradeId { get; set; }

    [JsonProperty("status")]
    public FuturesPriceTriggerStatus Status { get; set; }

    [JsonProperty("reason")]
    public string Reason { get; set; }

    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("is_stop_order")]
    public bool IsStopOrder { get; set; }
    
    [JsonProperty("stop_trigger")]
    public FuturesStreamAutoOrderStopTrigger StopTrigger { get; set; }

    [JsonProperty("trigger")]
    public FuturesStreamAutoOrderTrigger Trigger { get; set; }

    [JsonProperty("initial")]
    public FuturesStreamAutoOrderInitial Order { get; set; }

    [JsonProperty("order_type", NullValueHandling = NullValueHandling.Ignore)]
    public FuturesTriggerOrderType? Type { get; set; }

    [JsonProperty("me_order_id")]
    public long MeOrderId { get; set; }
}

public class FuturesStreamAutoOrderStopTrigger
{
    [JsonProperty("rule")]
    public GateSpotTriggerCondition? Rule { get; set; }

    [JsonProperty("trigger_price")]
    public decimal? TriggerPrice { get; set; }

    [JsonProperty("order_price")]
    public decimal? OrderPrice { get; set; }
}

public class FuturesStreamAutoOrderTrigger
{
    [JsonProperty("strategy_type")]
    public FuturesTriggerOrderStrategyType StrategyType { get; set; }

    [JsonProperty("price_type")]
    public FuturesTriggerOrderPriceType PriceType { get; set; }

    [JsonProperty("price")]
    public decimal? Price { get; set; }

    [JsonProperty("rule")]
    public GateSpotTriggerCondition? Rule { get; set; }

    [JsonProperty("expiration")]
    public int Expiration { get; set; }
}

public class FuturesStreamAutoOrderInitial
{
    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("size")]
    public long? Size { get; set; }

    [JsonProperty("price")]
    public decimal? Price { get; set; }

    [JsonProperty("tif")]
    public FuturesTimeInForce? TimeInForce { get; set; }

    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    [JsonProperty("iceberg")]
    public long? Iceberg { get; set; }
    
    [JsonProperty("is_close")]
    public bool? IsClose { get; set; }
    
    [JsonProperty("is_reduce_only")]
    public bool? IsReduceOnly { get; set; }
    
    [JsonProperty("auto_size")]
    public long? AutoSize { get; set; }
}