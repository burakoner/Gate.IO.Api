namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC quote request
/// </summary>
public record GateOtcQuoteRequest
{
    /// <summary>
    /// Quote direction. PAY means user inputs pay amount, GET means user inputs get amount.
    /// </summary>
    public GateOtcQuoteSide Side { get; set; }

    /// <summary>
    /// Currency the user pays
    /// </summary>
    public string PayCoin { get; set; }

    /// <summary>
    /// Currency the user receives
    /// </summary>
    public string GetCoin { get; set; }

    /// <summary>
    /// User payment currency amount
    /// </summary>
    public decimal? PayAmount { get; set; }

    /// <summary>
    /// Amount of currency received by the user
    /// </summary>
    public decimal? GetAmount { get; set; }

    /// <summary>
    /// Generate quote token for order placement
    /// </summary>
    public bool CreateQuoteToken { get; set; }

    /// <summary>
    /// Promotion code
    /// </summary>
    public string PromotionCode { get; set; }
}

/// <summary>
/// OTC fiat order request
/// </summary>
public record GateOtcFiatOrderRequest
{
    /// <summary>
    /// BUY for on-ramp or SELL for off-ramp
    /// </summary>
    public GateOtcOrderType Type { get; set; }

    /// <summary>
    /// Order kind returned by the quote API
    /// </summary>
    public GateOtcOrderKind Side { get; set; } = GateOtcOrderKind.Fiat;

    /// <summary>
    /// Cryptocurrency
    /// </summary>
    public string CryptoCurrency { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    public string FiatCurrency { get; set; }

    /// <summary>
    /// Amount of cryptocurrency
    /// </summary>
    public decimal CryptoAmount { get; set; }

    /// <summary>
    /// Fiat amount
    /// </summary>
    public decimal FiatAmount { get; set; }

    /// <summary>
    /// Promotion code
    /// </summary>
    public string PromotionCode { get; set; }

    /// <summary>
    /// Quote token returned by the quote API
    /// </summary>
    public string QuoteToken { get; set; }

    /// <summary>
    /// Bank card ID used for the order
    /// </summary>
    public long BankId { get; set; }
}

/// <summary>
/// OTC stablecoin order request
/// </summary>
public record GateOtcStableCoinOrderRequest
{
    /// <summary>
    /// Currency paid by the user
    /// </summary>
    public string PayCoin { get; set; }

    /// <summary>
    /// Currency to be received by the user
    /// </summary>
    public string GetCoin { get; set; }

    /// <summary>
    /// User payment currency amount
    /// </summary>
    public decimal? PayAmount { get; set; }

    /// <summary>
    /// Amount of currency received by the user
    /// </summary>
    public decimal? GetAmount { get; set; }

    /// <summary>
    /// Quote direction returned by the quote API
    /// </summary>
    public GateOtcQuoteSide? Side { get; set; }

    /// <summary>
    /// Promotion code
    /// </summary>
    public string PromotionCode { get; set; }

    /// <summary>
    /// Quote token returned by the quote API
    /// </summary>
    public string QuoteToken { get; set; }
}

/// <summary>
/// OTC order ID request
/// </summary>
public record GateOtcOrderIdRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long OrderId { get; set; }
}

/// <summary>
/// OTC fiat order list request
/// </summary>
public record GateOtcFiatOrderListRequest
{
    /// <summary>
    /// BUY, SELL, or ALL
    /// </summary>
    public GateOtcOrderType? Type { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    public string FiatCurrency { get; set; }

    /// <summary>
    /// Digital currency
    /// </summary>
    public string CryptoCurrency { get; set; }

    /// <summary>
    /// Start time
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// End time
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Order status
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? PageNumber { get; set; }

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int? PageSize { get; set; }
}

/// <summary>
/// OTC stablecoin order list request
/// </summary>
public record GateOtcStableCoinOrderListRequest
{
    /// <summary>
    /// Number of records per page
    /// </summary>
    public int? PageSize { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? PageNumber { get; set; }

    /// <summary>
    /// Order currency
    /// </summary>
    public string CoinName { get; set; }

    /// <summary>
    /// Start time
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// End time
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Status: PROCESSING, DONE, or FAILED
    /// </summary>
    public string Status { get; set; }
}
