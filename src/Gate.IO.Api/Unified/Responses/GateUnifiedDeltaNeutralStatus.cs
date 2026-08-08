namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified account Delta-neutral strategy mode setting
/// </summary>
public record GateUnifiedDeltaNeutralStatus
{
    /// <summary>
    /// Whether the account Delta-neutral strategy mode is enabled
    /// </summary>
    [JsonProperty("enabled")]
    public bool Enabled { get; set; }
}
