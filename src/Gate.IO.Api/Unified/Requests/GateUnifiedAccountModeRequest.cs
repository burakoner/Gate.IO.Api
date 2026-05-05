namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified account mode set request
/// </summary>
public record GateUnifiedAccountModeRequest
{
    /// <summary>
    /// Unified account mode
    /// </summary>
    public GateUnifiedAccountMode Mode { get; set; }

    /// <summary>
    /// Account mode settings
    /// </summary>
    public GateUnifiedAccountModeSettings Settings { get; set; }
}

/// <summary>
/// Unified account mode settings
/// </summary>
public record GateUnifiedAccountModeSettings
{
    /// <summary>
    /// USDT futures switch
    /// </summary>
    public bool? UsdtFutures { get; set; }

    /// <summary>
    /// Spot hedging switch
    /// </summary>
    public bool? SpotHedge { get; set; }

    /// <summary>
    /// Whether to use Earn funds as margin
    /// </summary>
    public bool? UseFunding { get; set; }

    /// <summary>
    /// Options switch
    /// </summary>
    public bool? Options { get; set; }
}
