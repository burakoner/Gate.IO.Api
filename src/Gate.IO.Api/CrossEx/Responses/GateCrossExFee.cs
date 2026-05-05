namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx user fee rate
/// </summary>
public record GateCrossExFee
{
    /// <summary>
    /// Gets or sets the Exchange Type.
    /// </summary>
    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    /// <summary>
    /// Gets or sets the Spot Maker Fee.
    /// </summary>
    [JsonProperty("spot_maker_fee")]
    public decimal SpotMakerFee { get; set; }

    /// <summary>
    /// Gets or sets the Spot Taker Fee.
    /// </summary>
    [JsonProperty("spot_taker_fee")]
    public decimal SpotTakerFee { get; set; }

    /// <summary>
    /// Gets or sets the Future Maker Fee.
    /// </summary>
    [JsonProperty("future_maker_fee")]
    public decimal FutureMakerFee { get; set; }

    /// <summary>
    /// Gets or sets the Future Taker Fee.
    /// </summary>
    [JsonProperty("future_taker_fee")]
    public decimal FutureTakerFee { get; set; }

    /// <summary>
    /// Gets or sets the Special Fees.
    /// </summary>
    [JsonProperty("special_fee_list")]
    public List<GateCrossExSpecialFee> SpecialFees { get; set; } = [];
}
