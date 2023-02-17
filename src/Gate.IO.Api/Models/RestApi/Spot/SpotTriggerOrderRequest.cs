namespace Gate.IO.Api.Models.RestApi.Spot;

public class SpotTriggerOrderRequest
{
    /// <summary>
    /// Gets or Sets Trigger
    /// </summary>
    [JsonProperty("trigger")]
    public SpotPriceTrigger Trigger { get; set; }

    /// <summary>
    /// Gets or Sets Put
    /// </summary>
    [JsonProperty("put")]
    public SpotPriceOrder Order { get; set; }

    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("market")]
    public string Symbol { get; set; }
}

public class SpotPriceTrigger
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
    public PriceTriggerCondition Rule { get; set; }

    /// <summary>
    /// How long (in seconds) to wait for the condition to be triggered before cancelling the order.
    /// </summary>
    [JsonProperty("expiration")]
    public int Expiration { get; set; }
}

public class SpotPriceOrder
{
    /// <summary>
    /// Order type, default to &#x60;limit&#x60;
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(SpotOrderTypeConverter))]
    public SpotOrderType Type { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(SpotOrderSideConverter))]
    public SpotOrderSide Side { get; set; }

    /// <summary>
    /// Order price
    /// </summary>
    [JsonProperty("price")]
    public string Price { get; set; }

    /// <summary>
    /// Order amount
    /// </summary>
    [JsonProperty("amount")]
    public string Amount { get; set; }

    /// <summary>
    /// Trading account type.
    /// </summary>
    [JsonProperty("account"), JsonConverter(typeof(AccountType2Converter))]
    public AccountType2 Account { get; set; }

    /// <summary>
    /// time_in_force
    /// </summary>
    [JsonProperty("time_in_force"), JsonConverter(typeof(SpotTriggerOrderTimeInForceConverter))]
    public SpotTriggerOrderTimeInForce? TimeInForce { get; set; }
}