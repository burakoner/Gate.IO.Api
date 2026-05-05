namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx user fee rate
/// </summary>
public record GateCrossExFee
{
    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    [JsonProperty("spot_maker_fee")]
    public decimal SpotMakerFee { get; set; }

    [JsonProperty("spot_taker_fee")]
    public decimal SpotTakerFee { get; set; }

    [JsonProperty("future_maker_fee")]
    public decimal FutureMakerFee { get; set; }

    [JsonProperty("future_taker_fee")]
    public decimal FutureTakerFee { get; set; }

    [JsonProperty("special_fee_list")]
    public List<GateCrossExSpecialFee> SpecialFees { get; set; } = [];
}
