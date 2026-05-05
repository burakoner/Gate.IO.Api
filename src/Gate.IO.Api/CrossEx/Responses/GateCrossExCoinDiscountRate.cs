namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx coin discount rate
/// </summary>
public record GateCrossExCoinDiscountRate
{
    /// <summary>
    /// Gets or sets the Coin.
    /// </summary>
    [JsonProperty("coin")]
    public string Coin { get; set; }

    /// <summary>
    /// Gets or sets the Exchange Type.
    /// </summary>
    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    /// <summary>
    /// Gets or sets the Tier.
    /// </summary>
    [JsonProperty("tier")]
    public int? Tier { get; set; }

    /// <summary>
    /// Gets or sets the Minimum Value.
    /// </summary>
    [JsonProperty("min_value")]
    public decimal? MinimumValue { get; set; }

    /// <summary>
    /// Gets or sets the Maximum Value.
    /// </summary>
    [JsonProperty("max_value")]
    public decimal? MaximumValue { get; set; }

    /// <summary>
    /// Gets or sets the Discount Rate.
    /// </summary>
    [JsonProperty("discount_rate")]
    public decimal? DiscountRate { get; set; }
}
