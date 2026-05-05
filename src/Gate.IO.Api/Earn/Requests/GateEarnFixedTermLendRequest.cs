namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn subscription request
/// </summary>
public record GateEarnFixedTermLendRequest
{
    /// <summary>
    /// Product ID
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// Subscription amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Annual interest rate
    /// </summary>
    public decimal? YearRate { get; set; }

    /// <summary>
    /// Auto-renewal status
    /// </summary>
    public int? ReinvestStatus { get; set; }

    /// <summary>
    /// Redemption payout account type
    /// </summary>
    public int? RedeemAccountType { get; set; }

    /// <summary>
    /// Interest rate boost coupon ID
    /// </summary>
    public long? FinancialRateId { get; set; }

    /// <summary>
    /// Sub-business type
    /// </summary>
    public int? SubBusiness { get; set; }
}
