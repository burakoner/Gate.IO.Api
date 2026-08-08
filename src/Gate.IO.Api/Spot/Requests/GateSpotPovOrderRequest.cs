namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot POV order creation request
/// </summary>
public record GateSpotPovOrderRequest
{
    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Buy or sell side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(MapConverter))]
    public GateSpotOrderSide Side { get; set; }

    /// <summary>
    /// Trade amount
    /// </summary>
    [JsonProperty("amount"), JsonConverter(typeof(GateDecimalStringConverter))]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Target participation rate
    /// </summary>
    [JsonProperty("participation_rate"), JsonConverter(typeof(GateMapConverter))]
    public GateSpotPovParticipationRate ParticipationRate { get; set; }

    /// <summary>
    /// Time to live
    /// </summary>
    [JsonProperty("ttl"), JsonConverter(typeof(MapConverter))]
    public GateSpotPovTimeToLive TimeToLive { get; set; }

    /// <summary>
    /// Limit price. When omitted, the market price is used.
    /// </summary>
    [JsonProperty("limit_price", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateDecimalStringConverter))]
    public decimal? LimitPrice { get; set; }

    /// <summary>
    /// Trigger price. When omitted, the order is triggered immediately.
    /// </summary>
    [JsonProperty("trigger_price", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateDecimalStringConverter))]
    public decimal? TriggerPrice { get; set; }

    /// <summary>
    /// User-defined order ID. When set, it must start with t-, contain at most 28 ASCII letters, digits, underscores, hyphens, or dots after the prefix.
    /// </summary>
    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; }
}
