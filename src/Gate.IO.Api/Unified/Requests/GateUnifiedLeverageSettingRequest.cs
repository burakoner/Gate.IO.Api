namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified leverage setting request
/// </summary>
public record GateUnifiedLeverageSettingRequest
{
    /// <summary>
    /// Currency name
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Multiplier
    /// </summary>
    public decimal Leverage { get; set; }
}
