namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual investment reinvest update request
/// </summary>
public record GateEarnDualReinvestUpdateRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long? OrderId { get; set; }

    /// <summary>
    /// Reinvest status. 0: off, 1: on
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// Effective duration in seconds
    /// </summary>
    public long? EffectiveTimeDuration { get; set; }
}
