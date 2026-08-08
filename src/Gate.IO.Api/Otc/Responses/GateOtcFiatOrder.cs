namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC fiat order
/// </summary>
public record GateOtcFiatOrder
{
    /// <summary>
    /// Current time
    /// </summary>
    [JsonProperty("time")]
    public DateTime? Time { get; set; }

    /// <summary>
    /// Current timestamp
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public string OrderId { get; set; }

    /// <summary>
    /// Trade number
    /// </summary>
    [JsonProperty("trade_no")]
    public string TradeNumber { get; set; }

    /// <summary>
    /// BUY or SELL
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(MapConverter))]
    public GateOtcOrderType Type { get; set; }

    /// <summary>
    /// Order status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Database status
    /// </summary>
    [JsonProperty("db_status")]
    public string DatabaseStatus { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    [JsonProperty("fiat_currency")]
    public string FiatCurrency { get; set; }

    /// <summary>
    /// Fiat currency information
    /// </summary>
    [JsonProperty("fiat_currency_info")]
    public GateOtcCurrencyInfo FiatCurrencyInfo { get; set; }

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

    [JsonProperty("ceypto_currency")]
    private string CryptoCurrencyTypo { set => CryptoCurrency = value; }

    /// <summary>
    /// Cryptocurrency information
    /// </summary>
    [JsonProperty("crypto_currency_info")]
    public GateOtcCurrencyInfo CryptoCurrencyInfo { get; set; }

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
    /// Transfer remark. Empty when <see cref="ReferenceCode"/> is set.
    /// </summary>
    [JsonProperty("transfer_remark")]
    public string TransferRemark { get; set; }

    /// <summary>
    /// Unique bank transfer reference code for deposit buy orders. Mutually exclusive with <see cref="TransferRemark"/>.
    /// </summary>
    [JsonProperty("reference_code")]
    public string ReferenceCode { get; set; }

    /// <summary>
    /// Bank account
    /// </summary>
    [JsonProperty("gate_bank_account_iban")]
    public string GateBankAccountIban { get; set; }

    /// <summary>
    /// Promotion code
    /// </summary>
    [JsonProperty("promotion_code")]
    public string PromotionCode { get; set; }
}
