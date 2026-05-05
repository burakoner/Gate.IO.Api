namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx order
/// </summary>
public record GateCrossExOrder
{
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    [JsonProperty("order_id")]
    public long? OrderId { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("client_order_id")]
    private string ClientOrderId { set => Text = value; }

    [JsonProperty("state")]
    public string State { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("side")]
    public string Side { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("attribute")]
    public string Attribute { get; set; }

    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    [JsonProperty("business_type")]
    public string BusinessType { get; set; }

    [JsonProperty("qty")]
    public decimal? Quantity { get; set; }

    [JsonProperty("quote_qty")]
    public decimal? QuoteQuantity { get; set; }

    [JsonProperty("price")]
    public decimal? Price { get; set; }

    [JsonProperty("time_in_force")]
    public string TimeInForce { get; set; }

    [JsonProperty("tif")]
    private string TimeInForceAlias { set => TimeInForce = value; }

    [JsonProperty("executed_qty")]
    public decimal? ExecutedQuantity { get; set; }

    [JsonProperty("executed_amount")]
    public decimal? ExecutedAmount { get; set; }

    [JsonProperty("executed_avg_price")]
    public decimal? ExecutedAveragePrice { get; set; }

    [JsonProperty("fee_coin")]
    public string FeeCoin { get; set; }

    [JsonProperty("fee_currency")]
    private string FeeCurrency { set => FeeCoin = value; }

    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    [JsonProperty("reduce_only")]
    public bool? ReduceOnly { get; set; }

    [JsonProperty("leverage")]
    public decimal? Leverage { get; set; }

    [JsonProperty("reason")]
    public string Reason { get; set; }

    [JsonProperty("last_executed_qty")]
    public decimal? LastExecutedQuantity { get; set; }

    [JsonProperty("last_executed_price")]
    public decimal? LastExecutedPrice { get; set; }

    [JsonProperty("last_executed_amount")]
    public decimal? LastExecutedAmount { get; set; }

    [JsonProperty("position_side")]
    public string PositionSide { get; set; }

    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }

    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }
}
