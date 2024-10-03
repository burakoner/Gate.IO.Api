using Gate.IO.Api.Spot;

namespace Gate.IO.Api.Futures;

public class FuturesTriggerOrderRequest
{
    [JsonProperty("trigger")]
    public FuturesPriceTrigger Trigger { get; set; }

    [JsonProperty("initial")]
    public FuturesPriceOrder Order { get; set; }

    [JsonProperty("order_type", NullValueHandling = NullValueHandling.Ignore)]
    public FuturesTriggerOrderType? Type { get; set; }
}

public class FuturesPriceTrigger
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

    /// <summary>
    /// Price Type
    /// </summary>
    [JsonProperty("price_type")]
    public FuturesTriggerOrderPriceType PriceType { get; set; }

    /// <summary>
    /// How the order will be triggered
    /// </summary>
    [JsonProperty("strategy_type")]
    public FuturesTriggerOrderStrategyType StrategyType { get; set; }
}

public class FuturesPriceOrder
{
    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("price")]
    public string Price { get; set; }

    [JsonProperty("size")]
    public long Size { get; set; }
}