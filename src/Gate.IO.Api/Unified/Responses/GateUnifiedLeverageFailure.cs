namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified leverage update failure
/// </summary>
public record GateUnifiedLeverageFailure
{
    /// <summary>
    /// Currency whose leverage could not be updated
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Reason the leverage update failed
    /// </summary>
    [JsonProperty("reason")]
    public string Reason { get; set; }
}
