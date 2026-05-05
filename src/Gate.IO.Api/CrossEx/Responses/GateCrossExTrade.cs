namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx fill record
/// </summary>
public record GateCrossExTrade
{
    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    /// <summary>
    /// Gets or sets the Transaction ID.
    /// </summary>
    [JsonProperty("transaction_id")]
    public long? TransactionId { get; set; }

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

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

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
    /// Gets or sets the Side.
    /// </summary>
    [JsonProperty("side")]
    public string Side { get; set; }

    /// <summary>
    /// Gets or sets the Quantity.
    /// </summary>
    [JsonProperty("qty")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    [JsonProperty("price")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Gets or sets the Fee.
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Gets or sets the Fee Coin.
    /// </summary>
    [JsonProperty("fee_coin")]
    public string FeeCoin { get; set; }

    /// <summary>
    /// Gets or sets the Fee Rate.
    /// </summary>
    [JsonProperty("fee_rate")]
    public decimal? FeeRate { get; set; }

    /// <summary>
    /// Gets or sets the Match Role.
    /// </summary>
    [JsonProperty("match_role")]
    public string MatchRole { get; set; }

    /// <summary>
    /// Gets or sets the Realized PnL.
    /// </summary>
    [JsonProperty("rpnl")]
    public decimal? RealizedPnl { get; set; }

    /// <summary>
    /// Gets or sets the Position Mode.
    /// </summary>
    [JsonProperty("position_mode")]
    public string PositionMode { get; set; }

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
}
