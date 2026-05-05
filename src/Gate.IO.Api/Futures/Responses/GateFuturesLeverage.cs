namespace Gate.IO.Api.Futures;

/// <summary>
/// Gate Futures leverage information
/// </summary>
public record GateFuturesLeverage
{
    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("Lever")]
    public decimal Leverage { get; set; }
}
