namespace Gate.IO.Api.Otc;

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
