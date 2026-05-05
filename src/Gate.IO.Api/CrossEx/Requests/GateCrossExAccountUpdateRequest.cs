namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account update request
/// </summary>
public record GateCrossExAccountUpdateRequest
{
    /// <summary>
    /// Contract position mode
    /// </summary>
    public GateCrossExPositionMode? PositionMode { get; set; }

    /// <summary>
    /// Account mode
    /// </summary>
    public GateCrossExAccountMode? AccountMode { get; set; }

    /// <summary>
    /// Exchange type
    /// </summary>
    public GateCrossExExchangeType? ExchangeType { get; set; }
}
