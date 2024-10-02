using Gate.IO.Api.Spot;

namespace Gate.IO.Api.Models.RestApi.Futures;

public class FuturesTriggerOrderRequest
{
    [JsonProperty("trigger")]
    public FuturesPriceTrigger Trigger { get; set; }

    [JsonProperty("initial")]
    public FuturesPriceOrder Order { get; set; }

    [JsonProperty("order_type", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(FuturesTriggerOrderTypeConverter))]
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
    [JsonProperty("rule"), JsonConverter(typeof(PriceTriggerConditionConverter))]
    public GateSpotTriggerCondition Rule { get; set; }

    /// <summary>
    /// How long (in seconds) to wait for the condition to be triggered before cancelling the order.
    /// </summary>
    [JsonProperty("expiration")]
    public int Expiration { get; set; }

    /// <summary>
    /// Price Type
    /// </summary>
    [JsonProperty("price_type"), JsonConverter(typeof(FuturesTriggerOrderPriceTypeConverter))]
    public FuturesTriggerOrderPriceType PriceType { get; set; }

    /// <summary>
    /// How the order will be triggered
    /// </summary>
    [JsonProperty("strategy_type"), JsonConverter(typeof(FuturesTriggerOrderStrategyTypeConverter))]
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