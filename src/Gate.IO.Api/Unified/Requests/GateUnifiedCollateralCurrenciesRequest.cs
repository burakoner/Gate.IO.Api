namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified collateral currencies request
/// </summary>
public record GateUnifiedCollateralCurrenciesRequest
{
    /// <summary>
    /// User-set collateral mode
    /// </summary>
    public GateUnifiedCollateralType Type { get; set; }

    /// <summary>
    /// Currency list to enable when type is custom
    /// </summary>
    public IEnumerable<string> EnableList { get; set; }

    /// <summary>
    /// Currency list to disable when type is custom
    /// </summary>
    public IEnumerable<string> DisableList { get; set; }
}
