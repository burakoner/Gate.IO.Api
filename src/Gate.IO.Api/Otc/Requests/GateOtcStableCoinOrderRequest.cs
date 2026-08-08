namespace Gate.IO.Api.Otc;

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
    public decimal PayAmount { get; set; }

    /// <summary>
    /// Amount of currency received by the user
    /// </summary>
    public decimal GetAmount { get; set; }

    /// <summary>
    /// Quote direction returned by the quote API
    /// </summary>
    public GateOtcQuoteSide Side { get; set; }

    /// <summary>
    /// Promotion code
    /// </summary>
    public string PromotionCode { get; set; }

    /// <summary>
    /// Quote token returned by the quote API
    /// </summary>
    public string QuoteToken { get; set; }
}
