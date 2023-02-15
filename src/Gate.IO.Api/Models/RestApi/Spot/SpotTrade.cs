namespace Gate.IO.Api.Models.RestApi.Spot;

/// <summary>
/// Trade
/// </summary>
public  class SpotTrade
{
    /// <summary>
    /// Trade ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Trading time
    /// </summary>
    [JsonProperty("create_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Trading time, with millisecond precision
    /// </summary>
    [JsonProperty("create_time_ms")]
    public decimal CreateTimeMilliseconds { get; set; }

    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(SpotOrderSideConverter))]
    public SpotOrderSide Side { get; set; }

    /// <summary>
    /// Trade role. No value in public endpoints
    /// </summary>
    [JsonProperty("role"), JsonConverter(typeof(SpotTraderRoleConverter))]
    public SpotTraderRole Role { get; set; }

    /// <summary>
    /// Trade amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Order price
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Related order ID. No value in public endpoints
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Fee deducted. No value in public endpoints
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Fee currency unit. No value in public endpoints
    /// </summary>
    [JsonProperty("fee_currency")]
    public string FeeCurrency { get; set; }

    /// <summary>
    /// Points used to deduct fee. No value in public endpoints
    /// </summary>
    [JsonProperty("point_fee")]
    public decimal? PointFee { get; set; }

    /// <summary>
    /// GT used to deduct fee. No value in public endpoints
    /// </summary>
    [JsonProperty("gt_fee")]
    public decimal? GtFee { get; set; }
}
