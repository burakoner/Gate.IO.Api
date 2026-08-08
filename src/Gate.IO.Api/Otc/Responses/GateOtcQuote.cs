namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC quote
/// </summary>
public record GateOtcQuote
{
    /// <summary>
    /// BUY for on-ramp or SELL for off-ramp
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(MapConverter))]
    public GateOtcOrderType Type { get; set; }

    /// <summary>
    /// Payment currency
    /// </summary>
    [JsonProperty("pay_coin")]
    public string PayCoin { get; set; }

    /// <summary>
    /// Received currency
    /// </summary>
    [JsonProperty("get_coin")]
    public string GetCoin { get; set; }

    /// <summary>
    /// Payment amount
    /// </summary>
    [JsonProperty("pay_amount")]
    public decimal PayAmount { get; set; }

    /// <summary>
    /// Redemption amount
    /// </summary>
    [JsonProperty("get_amount")]
    public decimal GetAmount { get; set; }

    /// <summary>
    /// Exchange rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// Reciprocal of the exchange rate
    /// </summary>
    [JsonProperty("rate_reci")]
    public decimal RateReciprocal { get; set; }

    /// <summary>
    /// Promotion code
    /// </summary>
    [JsonProperty("promotion_code")]
    public string PromotionCode { get; set; }

    /// <summary>
    /// Quote method
    /// </summary>
    [JsonProperty("side")]
    [JsonConverter(typeof(MapConverter))]
    public GateOtcQuoteSide Side { get; set; }

    /// <summary>
    /// Quote validity period in seconds
    /// </summary>
    [JsonProperty("validity_period")]
    public long? ValidityPeriod { get; set; }

    /// <summary>
    /// Order kind
    /// </summary>
    [JsonProperty("order_type")]
    [JsonConverter(typeof(MapConverter))]
    public GateOtcOrderKind OrderType { get; set; }

    /// <summary>
    /// Quote token required when placing an order
    /// </summary>
    [JsonProperty("quote_token")]
    public string QuoteToken { get; set; }

    /// <summary>
    /// Remaining refresh count
    /// </summary>
    [JsonProperty("refresh_limit")]
    public int? RefreshLimit { get; set; }

    /// <summary>
    /// Refresh limit message
    /// </summary>
    [JsonProperty("refresh_limit_msg")]
    public string RefreshLimitMessage { get; set; }

    /// <summary>
    /// Server timestamp returned by the quote response envelope
    /// </summary>
    [JsonIgnore]
    public DateTime Timestamp { get; internal set; }
}
