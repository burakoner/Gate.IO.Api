namespace Gate.IO.Api.Spot;

/// <summary>
/// Trade
/// </summary>
public class GateSpotTrade
{
    /// <summary>
    /// Trade ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Trading time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Trading time, with millisecond precision
    /// </summary>
    [JsonProperty("create_time_ms")]
    public long CreateTimeInMilliseconds { get; set; }

    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side")]
    public GateSpotOrderSide Side { get; set; }

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

    #region Private (Signed) Response Fields
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public long? OrderId { get; set; }
    
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
    public decimal? GTFee { get; set; }
    
    /// <summary>
    /// The custom data that the user remarked when amending the order
    /// </summary>
    [JsonProperty("amend_text")]
    public string AmendText { get; set; }

    /// <summary>
    /// Represents a unique and consecutive trade ID within a single market.
    /// It is used to track and identify trades in the specific market
    /// </summary>
    [JsonProperty("sequence_id")]
    public string SequenceId { get; set; }

    /// <summary>
    /// User defined information. No value in public endpoints
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; }
    #endregion
}
