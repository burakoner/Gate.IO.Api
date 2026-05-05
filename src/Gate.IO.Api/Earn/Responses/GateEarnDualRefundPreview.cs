namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual-currency early redemption preview
/// </summary>
public record GateEarnDualRefundPreview
{
    /// <summary>
    /// Order creation timestamp
    /// </summary>
    [JsonProperty("create_timest")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Order delivery timestamp
    /// </summary>
    [JsonProperty("delivery_timest")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime DeliveryTime { get; set; }

    /// <summary>
    /// Strike price
    /// </summary>
    [JsonProperty("exercise_price")]
    public decimal ExercisePrice { get; set; }

    /// <summary>
    /// Investment amount
    /// </summary>
    [JsonProperty("invest_amount")]
    public decimal InvestAmount { get; set; }

    /// <summary>
    /// Investment token
    /// </summary>
    [JsonProperty("invest_currency")]
    public string InvestCurrency { get; set; }

    /// <summary>
    /// Order name identifier
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Request ID used for actual redemption
    /// </summary>
    [JsonProperty("req_id")]
    public string RequestId { get; set; }

    /// <summary>
    /// Refund fee
    /// </summary>
    [JsonProperty("refund_service_charge")]
    public decimal RefundServiceCharge { get; set; }

    /// <summary>
    /// Settlement price
    /// </summary>
    [JsonProperty("settle_price")]
    public decimal SettlePrice { get; set; }

    /// <summary>
    /// Settlement amount
    /// </summary>
    [JsonProperty("settlement_amount")]
    public decimal SettlementAmount { get; set; }

    /// <summary>
    /// Settlement currency
    /// </summary>
    [JsonProperty("settlement_currency")]
    public string SettlementCurrency { get; set; }

    /// <summary>
    /// Settlement interest
    /// </summary>
    [JsonProperty("settlement_interest")]
    public decimal SettlementInterest { get; set; }

    /// <summary>
    /// Settlement principal
    /// </summary>
    [JsonProperty("settlement_principle")]
    public decimal SettlementPrincipal { get; set; }

    /// <summary>
    /// Product type
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(MapConverter))]
    public GateEarnDualOptionType Type { get; set; }

    /// <summary>
    /// Redemption time
    /// </summary>
    [JsonProperty("money_back_timest")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime MoneyBackTime { get; set; }
}
