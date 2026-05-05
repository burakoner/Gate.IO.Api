namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx fill record
/// </summary>
public record GateCrossExTrade
{
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    [JsonProperty("transaction_id")]
    public long? TransactionId { get; set; }

    [JsonProperty("order_id")]
    public long? OrderId { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    [JsonProperty("business_type")]
    public string BusinessType { get; set; }

    [JsonProperty("side")]
    public string Side { get; set; }

    [JsonProperty("qty")]
    public decimal? Quantity { get; set; }

    [JsonProperty("price")]
    public decimal? Price { get; set; }

    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    [JsonProperty("fee_coin")]
    public string FeeCoin { get; set; }

    [JsonProperty("fee_rate")]
    public decimal? FeeRate { get; set; }

    [JsonProperty("match_role")]
    public string MatchRole { get; set; }

    [JsonProperty("rpnl")]
    public decimal? RealizedPnl { get; set; }

    [JsonProperty("position_mode")]
    public string PositionMode { get; set; }

    [JsonProperty("position_side")]
    public string PositionSide { get; set; }

    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }
}
