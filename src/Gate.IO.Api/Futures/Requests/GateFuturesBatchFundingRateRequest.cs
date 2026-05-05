namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures batch funding rate query request
/// </summary>
public record GateFuturesBatchFundingRateRequest
{
    [JsonProperty("contracts")]
    public List<string> Contracts { get; set; } = [];
}
