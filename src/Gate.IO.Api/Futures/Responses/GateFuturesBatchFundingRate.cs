namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures batch funding rate result
/// </summary>
public record GateFuturesBatchFundingRate
{
    /// <summary>
    /// Futures contract
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Funding rate data
    /// </summary>
    [JsonProperty("data")]
    public List<GateFuturesFundingRate> Data { get; set; } = [];
}
