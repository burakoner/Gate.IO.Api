namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx special fee rate
/// </summary>
public record GateCrossExSpecialFee
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("taker_fee_rate")]
    public decimal TakerFeeRate { get; set; }

    [JsonProperty("maker_fee_rate")]
    public decimal MakerFeeRate { get; set; }
}
