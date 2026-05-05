namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx order
/// </summary>
public record GateCrossExOrder
{
    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    /// <summary>
    /// Gets or sets the Order ID.
    /// </summary>
    [JsonProperty("order_id")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Gets or sets the Text.
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("client_order_id")]
    private string ClientOrderId { set => Text = value; }

    /// <summary>
    /// Gets or sets the State.
    /// </summary>
    [JsonProperty("state")]
    public string State { get; set; }

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Side.
    /// </summary>
    [JsonProperty("side")]
    public string Side { get; set; }

    /// <summary>
    /// Gets or sets the Type.
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the Attribute.
    /// </summary>
    [JsonProperty("attribute")]
    public string Attribute { get; set; }

    /// <summary>
    /// Gets or sets the Exchange Type.
    /// </summary>
    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    /// <summary>
    /// Gets or sets the Business Type.
    /// </summary>
    [JsonProperty("business_type")]
    public string BusinessType { get; set; }

    /// <summary>
    /// Gets or sets the Quantity.
    /// </summary>
    [JsonProperty("qty")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Gets or sets the Quote Quantity.
    /// </summary>
    [JsonProperty("quote_qty")]
    public decimal? QuoteQuantity { get; set; }

    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    [JsonProperty("price")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Gets or sets the Time In Force.
    /// </summary>
    [JsonProperty("time_in_force")]
    public string TimeInForce { get; set; }

    [JsonProperty("tif")]
    private string TimeInForceAlias { set => TimeInForce = value; }

    /// <summary>
    /// Gets or sets the Executed Quantity.
    /// </summary>
    [JsonProperty("executed_qty")]
    public decimal? ExecutedQuantity { get; set; }

    /// <summary>
    /// Gets or sets the Executed Amount.
    /// </summary>
    [JsonProperty("executed_amount")]
    public decimal? ExecutedAmount { get; set; }

    /// <summary>
    /// Gets or sets the Executed Average Price.
    /// </summary>
    [JsonProperty("executed_avg_price")]
    public decimal? ExecutedAveragePrice { get; set; }

    /// <summary>
    /// Gets or sets the Fee Coin.
    /// </summary>
    [JsonProperty("fee_coin")]
    public string FeeCoin { get; set; }

    [JsonProperty("fee_currency")]
    private string FeeCurrency { set => FeeCoin = value; }

    /// <summary>
    /// Gets or sets the Fee.
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Gets or sets the Reduce Only.
    /// </summary>
    [JsonProperty("reduce_only")]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Gets or sets the Leverage.
    /// </summary>
    [JsonProperty("leverage")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Gets or sets the Reason.
    /// </summary>
    [JsonProperty("reason")]
    public string Reason { get; set; }

    /// <summary>
    /// Gets or sets the Last Executed Quantity.
    /// </summary>
    [JsonProperty("last_executed_qty")]
    public decimal? LastExecutedQuantity { get; set; }

    /// <summary>
    /// Gets or sets the Last Executed Price.
    /// </summary>
    [JsonProperty("last_executed_price")]
    public decimal? LastExecutedPrice { get; set; }

    /// <summary>
    /// Gets or sets the Last Executed Amount.
    /// </summary>
    [JsonProperty("last_executed_amount")]
    public decimal? LastExecutedAmount { get; set; }

    /// <summary>
    /// Gets or sets the Position Side.
    /// </summary>
    [JsonProperty("position_side")]
    public string PositionSide { get; set; }

    /// <summary>
    /// Gets or sets the Create Time.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// Gets or sets the Update Time.
    /// </summary>
    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }
}
