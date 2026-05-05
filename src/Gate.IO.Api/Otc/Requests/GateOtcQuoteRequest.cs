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
