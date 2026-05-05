namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures batch funding rate query request
/// </summary>
public record GateFuturesBatchFundingRateRequest
{
    /// <summary>
    /// Gets or sets the Contracts.
    /// </summary>
    [JsonProperty("contracts")]
    public List<string> Contracts { get; set; } = [];
}
