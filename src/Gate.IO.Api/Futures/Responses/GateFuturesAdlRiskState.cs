namespace Gate.IO.Api.Futures;

/// <summary>
/// Market-level ADL risk state and the time at which it was calculated.
/// </summary>
public record GateFuturesAdlRiskState
{
    /// <summary>
    /// Market ADL risk state: normal, warning or adl_risk. Other returned strings are preserved,
    /// not classified as normal. An omitted/null state is not a valid response.
    /// </summary>
    [JsonProperty("state", Required = Required.Always)]
    public string State { get; set; }

    /// <summary>
    /// State calculation time as an exact int64 Unix timestamp in milliseconds.
    /// An omitted/null timestamp is not replaced with zero, and returned values are not converted or rounded.
    /// </summary>
    [JsonProperty("calculated_at_ms", Required = Required.Always)]
    public long CalculatedAtInMilliseconds { get; set; }
}
