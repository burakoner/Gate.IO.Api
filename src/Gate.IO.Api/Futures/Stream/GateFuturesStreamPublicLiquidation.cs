namespace Gate.IO.Api.Futures;

/// <summary>
/// Represents a Futures public liquidation update.
/// </summary>
public record GateFuturesStreamPublicLiquidation
{
    /// <summary>
    /// Futures contract.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Liquidation trade price.
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Liquidation trade size.
    /// </summary>
    [JsonProperty("size")]
    public decimal Size { get; set; }

    /// <summary>
    /// Liquidation timestamp in milliseconds.
    /// </summary>
    [JsonProperty("time_ms")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
