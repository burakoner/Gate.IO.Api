namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC stablecoin order
/// </summary>
public record GateOtcStableCoinOrder
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// Transaction reference number
    /// </summary>
    [JsonProperty("trade_no")]
    public string TradeNumber { get; set; }

    /// <summary>
    /// Payment currency
    /// </summary>
    [JsonProperty("pay_coin")]
    public string PayCoin { get; set; }

    /// <summary>
    /// Payment currency icon
    /// </summary>
    [JsonProperty("pay_icon")]
    public string PayIcon { get; set; }

    /// <summary>
    /// Payment amount
    /// </summary>
    [JsonProperty("pay_amount")]
    public decimal? PayAmount { get; set; }

    /// <summary>
    /// Received currency
    /// </summary>
    [JsonProperty("get_coin")]
    public string GetCoin { get; set; }

    /// <summary>
    /// Received currency icon
    /// </summary>
    [JsonProperty("get_icon")]
    public string GetIcon { get; set; }

    /// <summary>
    /// Received amount
    /// </summary>
    [JsonProperty("get_amount")]
    public decimal? GetAmount { get; set; }

    /// <summary>
    /// Exchange rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal? Rate { get; set; }

    /// <summary>
    /// Reciprocal of the exchange rate
    /// </summary>
    [JsonProperty("rate_reci")]
    public decimal? RateReciprocal { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Created timestamp
    /// </summary>
    [JsonProperty("create_timest")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTimeStamp { get; set; }

    /// <summary>
    /// Created time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime? CreateTime { get; set; }
}
