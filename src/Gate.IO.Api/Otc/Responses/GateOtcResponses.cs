namespace Gate.IO.Api.Otc;

internal record GateOtcResponse<T> where T : class
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public T Data { get; set; }

    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }
}

/// <summary>
/// OTC action result
/// </summary>
public record GateOtcActionResult
{
    /// <summary>
    /// Return code
    /// </summary>
    [JsonProperty("code")]
    public int Code { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }

    /// <summary>
    /// Response timestamp
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }
}

/// <summary>
/// OTC quote
/// </summary>
public record GateOtcQuote
{
    /// <summary>
    /// Redirect URL
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; set; }

    /// <summary>
    /// Memo
    /// </summary>
    [JsonProperty("memo")]
    public string Memo { get; set; }

    /// <summary>
    /// BUY for on-ramp or SELL for off-ramp
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(MapConverter))]
    public GateOtcOrderType? Type { get; set; }

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
    /// Signature flag
    /// </summary>
    [JsonProperty("has_signature")]
    public int? HasSignature { get; set; }

    /// <summary>
    /// Quote validity period in seconds
    /// </summary>
    [JsonProperty("validity_period")]
    public long? ValidityPeriod { get; set; }

    /// <summary>
    /// Exchange rate
    /// </summary>
    [JsonProperty("ex_rate")]
    public decimal? ExchangeRate { get; set; }

    /// <summary>
    /// USDC exchange rate
    /// </summary>
    [JsonProperty("usdc_rate")]
    public decimal? UsdcRate { get; set; }

    /// <summary>
    /// File requirement flag
    /// </summary>
    [JsonProperty("is_need_file")]
    public int? IsNeedFile { get; set; }

    /// <summary>
    /// Gate bank ID
    /// </summary>
    [JsonProperty("gate_bank_id")]
    public long? GateBankId { get; set; }

    /// <summary>
    /// Gate bank name
    /// </summary>
    [JsonProperty("gate_bank_name")]
    public string GateBankName { get; set; }

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
}

/// <summary>
/// OTC bank account
/// </summary>
public record GateOtcBankAccount
{
    /// <summary>
    /// Bank ID
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// Bank account name
    /// </summary>
    [JsonProperty("bank_account_name")]
    public string BankAccountName { get; set; }

    /// <summary>
    /// Bank name
    /// </summary>
    [JsonProperty("bank_name")]
    public string BankName { get; set; }

    /// <summary>
    /// Bank country
    /// </summary>
    [JsonProperty("bank_country")]
    public string BankCountry { get; set; }

    /// <summary>
    /// Bank address
    /// </summary>
    [JsonProperty("bank_address")]
    public string BankAddress { get; set; }

    /// <summary>
    /// Bank code
    /// </summary>
    [JsonProperty("bank_code")]
    public string BankCode { get; set; }

    /// <summary>
    /// Branch code
    /// </summary>
    [JsonProperty("branch_code")]
    public string BranchCode { get; set; }

    /// <summary>
    /// IBAN number
    /// </summary>
    [JsonProperty("iban")]
    public string Iban { get; set; }

    /// <summary>
    /// SWIFT code
    /// </summary>
    [JsonProperty("swift")]
    public string Swift { get; set; }

    /// <summary>
    /// Remittance routing number
    /// </summary>
    [JsonProperty("remittance_line_number")]
    public string RemittanceLineNumber { get; set; }

    /// <summary>
    /// Correspondent bank name
    /// </summary>
    [JsonProperty("agent_bank_name")]
    public string AgentBankName { get; set; }

    /// <summary>
    /// Correspondent bank SWIFT code
    /// </summary>
    [JsonProperty("agent_bank_swift")]
    public string AgentBankSwift { get; set; }

    /// <summary>
    /// Submission time
    /// </summary>
    [JsonProperty("submit_time")]
    public DateTime? SubmitTime { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("update_time")]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Document file type
    /// </summary>
    [JsonProperty("documentation_file_type")]
    public string DocumentationFileType { get; set; }

    /// <summary>
    /// Remark
    /// </summary>
    [JsonProperty("memo")]
    public string Memo { get; set; }

    /// <summary>
    /// Whether it is the default bank card
    /// </summary>
    [JsonProperty("is_default")]
    public int? IsDefault { get; set; }

    /// <summary>
    /// Bank ID
    /// </summary>
    [JsonProperty("bank_id")]
    public long? BankId { get; set; }

    /// <summary>
    /// Document file URL
    /// </summary>
    [JsonProperty("documentation_file_key_url")]
    public string DocumentationFileKeyUrl { get; set; }

    /// <summary>
    /// Message returned when bank information is unavailable
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }

    /// <summary>
    /// Action URL returned when bank information is unavailable
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; set; }

    /// <summary>
    /// Display flag returned when bank information is unavailable
    /// </summary>
    [JsonProperty("show")]
    public int? Show { get; set; }
}

internal record GateOtcBankList
{
    [JsonProperty("lists")]
    public List<GateOtcBankAccount> Lists { get; set; } = [];
}

/// <summary>
/// OTC fiat order page
/// </summary>
public record GateOtcFiatOrderPage
{
    /// <summary>
    /// Page number
    /// </summary>
    [JsonProperty("pn")]
    public int PageNumber { get; set; }

    /// <summary>
    /// Number of items per page
    /// </summary>
    [JsonProperty("ps")]
    public int PageSize { get; set; }

    /// <summary>
    /// Total pages
    /// </summary>
    [JsonProperty("total_pn")]
    public int TotalPages { get; set; }

    /// <summary>
    /// Total item count
    /// </summary>
    [JsonProperty("count")]
    public int Count { get; set; }

    /// <summary>
    /// Orders
    /// </summary>
    [JsonProperty("list")]
    public List<GateOtcFiatOrder> List { get; set; } = [];
}

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
    public long OrderId { get; set; }

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
    /// Remark
    /// </summary>
    [JsonProperty("transfer_remark")]
    public string TransferRemark { get; set; }

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

/// <summary>
/// OTC currency information
/// </summary>
public record GateOtcCurrencyInfo
{
    /// <summary>
    /// Name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Icon URL
    /// </summary>
    [JsonProperty("icon")]
    public string Icon { get; set; }
}

/// <summary>
/// OTC stablecoin order page
/// </summary>
public record GateOtcStableCoinOrderPage
{
    /// <summary>
    /// Total item count
    /// </summary>
    [JsonProperty("total")]
    public int Total { get; set; }

    /// <summary>
    /// Number of records per page
    /// </summary>
    [JsonProperty("page_size")]
    public int PageSize { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    [JsonProperty("page_number")]
    public int PageNumber { get; set; }

    /// <summary>
    /// Total pages
    /// </summary>
    [JsonProperty("total_page")]
    public int TotalPage { get; set; }

    /// <summary>
    /// Orders
    /// </summary>
    [JsonProperty("list")]
    public List<GateOtcStableCoinOrder> List { get; set; } = [];
}

/// <summary>
/// OTC stablecoin order
/// </summary>
public record GateOtcStableCoinOrder
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

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
    public decimal PayAmount { get; set; }

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
