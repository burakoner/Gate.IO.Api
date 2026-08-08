namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx special fee rate
/// </summary>
public record GateCrossExSpecialFee
{
    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Taker Fee Rate.
    /// </summary>
    [JsonProperty("taker_fee_rate")]
    public decimal TakerFeeRate { get; set; }

    /// <summary>
    /// Gets or sets the Maker Fee Rate.
    /// </summary>
    [JsonProperty("maker_fee_rate")]
    public decimal MakerFeeRate { get; set; }

    /// <summary>
    /// Gets or sets the RPI Maker Fee Rate.
    /// </summary>
    [JsonProperty("rpi_fee_rate")]
    public decimal? RpiFeeRate { get; set; }
}
