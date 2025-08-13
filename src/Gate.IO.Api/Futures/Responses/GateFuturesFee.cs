namespace Gate.IO.Api.Futures;

/// <summary>
/// Gate Futures Fee
/// </summary>
public record GateFuturesFee
{
    /// <summary>
    /// Taker Fee
    /// </summary>
    [JsonProperty("taker_fee")]
    public decimal TakerFee { get; set; }

    /// <summary>
    /// Maker Fee
    /// </summary>
    [JsonProperty("maker_fee")]
    public decimal MakerFee { get; set; }
}
