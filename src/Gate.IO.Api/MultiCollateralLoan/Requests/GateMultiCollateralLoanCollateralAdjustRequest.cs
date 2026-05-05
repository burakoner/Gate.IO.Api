namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan collateral adjustment request
/// </summary>
public record GateMultiCollateralLoanCollateralAdjustRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// Operation type
    /// </summary>
    public GateMultiCollateralLoanCollateralOperationType Type { get; set; }

    /// <summary>
    /// Collateral currency list
    /// </summary>
    public IEnumerable<GateMultiCollateralLoanCurrencyAmount> Collaterals { get; set; }
}
