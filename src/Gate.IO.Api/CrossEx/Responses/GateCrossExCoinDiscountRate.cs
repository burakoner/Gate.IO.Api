namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx coin discount rate
/// </summary>
public record GateCrossExCoinDiscountRate
{
    [JsonProperty("coin")]
    public string Coin { get; set; }

    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    [JsonProperty("tier")]
    public int? Tier { get; set; }

    [JsonProperty("min_value")]
    public decimal? MinimumValue { get; set; }

    [JsonProperty("max_value")]
    public decimal? MaximumValue { get; set; }

    [JsonProperty("discount_rate")]
    public decimal? DiscountRate { get; set; }
}
