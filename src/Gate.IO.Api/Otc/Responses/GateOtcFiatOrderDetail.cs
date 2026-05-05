namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC fiat order detail
/// </summary>
public record GateOtcFiatOrderDetail
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(MapConverter))]
    public GateOtcOrderType Type { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    [JsonProperty("fiat_currency")]
    public string FiatCurrency { get; set; }

    /// <summary>
    /// Fiat amount
    /// </summary>
    [JsonProperty("fiat_amount")]
    public decimal FiatAmount { get; set; }

    /// <summary>
    /// Cryptocurrency
    /// </summary>
    [JsonProperty("crypto_currency")]
    public string CryptoCurrency { get; set; }

    /// <summary>
    /// Cryptocurrency amount
    /// </summary>
    [JsonProperty("crypto_amount")]
    public decimal CryptoAmount { get; set; }

    /// <summary>
    /// Exchange rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// Remark
    /// </summary>
    [JsonProperty("transfer_remark")]
    public string TransferRemark { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Database status
    /// </summary>
    [JsonProperty("db_status")]
    public string DatabaseStatus { get; set; }

    /// <summary>
    /// Created time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// Cancellation or rejection reason
    /// </summary>
    [JsonProperty("memo")]
    public string Memo { get; set; }

    /// <summary>
    /// Quote direction
    /// </summary>
    [JsonProperty("side")]
    [JsonConverter(typeof(MapConverter))]
    public GateOtcOrderKind Side { get; set; }

    /// <summary>
    /// Promotion code
    /// </summary>
    [JsonProperty("promotion_code")]
    public string PromotionCode { get; set; }

    /// <summary>
    /// Trade number
    /// </summary>
    [JsonProperty("trade_no")]
    public string TradeNumber { get; set; }
}
