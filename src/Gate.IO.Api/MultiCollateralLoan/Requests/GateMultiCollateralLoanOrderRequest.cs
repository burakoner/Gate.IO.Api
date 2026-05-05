namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan order creation request
/// </summary>
public record GateMultiCollateralLoanOrderRequest
{
    /// <summary>
    /// Optional order ID
    /// </summary>
    public long? OrderId { get; set; }

    /// <summary>
    /// Order type. Defaults to current if not specified.
    /// </summary>
    public GateMultiCollateralLoanOrderType? OrderType { get; set; }

    /// <summary>
    /// Fixed interest rate lending period. Required for fixed rate orders.
    /// </summary>
    public GateMultiCollateralLoanFixedType? FixedType { get; set; }

    /// <summary>
    /// Fixed interest rate. Required for fixed rate orders.
    /// </summary>
    public decimal? FixedRate { get; set; }

    /// <summary>
    /// Fixed interest rate auto-renewal
    /// </summary>
    public bool? AutoRenew { get; set; }

    /// <summary>
    /// Fixed interest rate auto-repayment
    /// </summary>
    public bool? AutoRepay { get; set; }

    /// <summary>
    /// Borrowed currency
    /// </summary>
    public string BorrowCurrency { get; set; }

    /// <summary>
    /// Borrowed amount
    /// </summary>
    public decimal BorrowAmount { get; set; }

    /// <summary>
    /// Collateral currency and amount
    /// </summary>
    public IEnumerable<GateMultiCollateralLoanCurrencyAmount> CollateralCurrencies { get; set; }
}
