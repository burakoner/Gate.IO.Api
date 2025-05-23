namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified account mode
/// </summary>
public record GateUnifiedAccountModeInfo
{
    /// <summary>
    /// Account mode
    /// </summary>
    [JsonProperty("mode")]
    public GateUnifiedAccountMode Mode { get; set; }

    /// <summary>
    /// Settings
    /// </summary>
    [JsonProperty("settings")]
    public GateIoUnifiedAccountSettings Settings { get; set; } = null!;
}

/// <summary>
/// Unified account settings
/// </summary>
public record GateIoUnifiedAccountSettings
{
    /// <summary>
    /// USDT contract switch
    /// </summary>
    [JsonProperty("usdt_futures")]
    public bool UsdtFutures { get; set; }

    /// <summary>
    /// Spot hedging switch
    /// </summary>
    [JsonProperty("spot_hedge")]
    public bool SpotHedge { get; set; }

    /// <summary>
    /// When the mode is set to combined margin mode, will funds be used as margin
    /// </summary>
    [JsonProperty("use_funding")]
    public bool UseFunding { get; set; }

    /// <summary>
    /// Option switch
    /// </summary>
    [JsonProperty("options")]
    public bool Options { get; set; }
}
