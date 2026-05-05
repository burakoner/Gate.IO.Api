namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot price-triggered order stream update.
/// </summary>
public class GateSpotStreamPriceOrder
{
    /// <summary>
    /// Market name.
    /// </summary>
    [JsonProperty("market")]
    public string Market { get; set; }

    /// <summary>
    /// User ID.
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// Price order ID.
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Base currency.
    /// </summary>
    [JsonProperty("currency_type")]
    public string CurrencyType { get; set; }

    /// <summary>
    /// Quote currency.
    /// </summary>
    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    /// <summary>
    /// Update reason.
    /// </summary>
    [JsonProperty("reason")]
    public string Reason { get; set; }

    /// <summary>
    /// Error message when the price order fails.
    /// </summary>
    [JsonProperty("err_msg")]
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Fired spot order ID.
    /// </summary>
    [JsonProperty("fired_order_id")]
    public long FiredOrderId { get; set; }

    /// <summary>
    /// Whether the fired order was immediately cancelled.
    /// </summary>
    [JsonProperty("instant_cancel")]
    public bool InstantCancel { get; set; }

    /// <summary>
    /// Trigger price.
    /// </summary>
    [JsonProperty("trigger_price")]
    public decimal TriggerPrice { get; set; }

    /// <summary>
    /// Trigger rule.
    /// </summary>
    [JsonProperty("trigger_rule")]
    public string TriggerRule { get; set; }

    /// <summary>
    /// Trigger expiration in seconds.
    /// </summary>
    [JsonProperty("trigger_expiration")]
    public int TriggerExpiration { get; set; }

    /// <summary>
    /// Order price.
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Order amount.
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Source value returned by Gate.
    /// </summary>
    [JsonProperty("source")]
    public string Source { get; set; }

    /// <summary>
    /// Order type.
    /// </summary>
    [JsonProperty("order_type"), JsonConverter(typeof(MapConverter))]
    public GateSpotOrderType OrderType { get; set; }

    /// <summary>
    /// Order side.
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(MapConverter))]
    public GateSpotOrderSide Side { get; set; }

    /// <summary>
    /// Matching engine type.
    /// </summary>
    [JsonProperty("engine_type")]
    public string EngineType { get; set; }

    /// <summary>
    /// Whether this is a stop order.
    /// </summary>
    [JsonProperty("is_stop_order")]
    public bool IsStopOrder { get; set; }

    /// <summary>
    /// Stop trigger price. Gate may return an empty string when it is not set.
    /// </summary>
    [JsonProperty("stop_trigger_price")]
    public string StopTriggerPrice { get; set; }

    /// <summary>
    /// Stop trigger rule. Gate may return an empty string when it is not set.
    /// </summary>
    [JsonProperty("stop_trigger_rule")]
    public string StopTriggerRule { get; set; }

    /// <summary>
    /// Stop order price. Gate may return an empty string when it is not set.
    /// </summary>
    [JsonProperty("stop_price")]
    public string StopPrice { get; set; }

    /// <summary>
    /// Creation time in milliseconds.
    /// </summary>
    [JsonProperty("ctime")]
    public long CreateTimeInMilliseconds { get; set; }

    /// <summary>
    /// Finish time in milliseconds.
    /// </summary>
    [JsonProperty("ftime")]
    public long FinishTimeInMilliseconds { get; set; }
}
