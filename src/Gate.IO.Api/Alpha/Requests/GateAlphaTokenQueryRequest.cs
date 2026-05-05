namespace Gate.IO.Api.Alpha;

/// <summary>
/// Request to query Alpha token information.
/// </summary>
public record GateAlphaTokenQueryRequest
{
    /// <summary>
    /// Chain name, such as solana, eth, bsc, base, or gatelayer.
    /// </summary>
    public string Chain { get; set; }

    /// <summary>
    /// Launch platform, such as pump, fourmeme, moonshot, or gatefun.
    /// </summary>
    public string LaunchPlatform { get; set; }

    /// <summary>
    /// Contract address.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Page number.
    /// </summary>
    public int? Page { get; set; }
}
