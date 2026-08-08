namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock trading fee rate
/// </summary>
public record GateStockFeeRate
{
    /// <summary>Gets or sets the VIP level.</summary>
    [JsonProperty("vip_level")]
    public int VipLevel { get; set; }
    /// <summary>Gets or sets the maker fee rate.</summary>
    [JsonProperty("maker_fee")]
    public decimal MakerFee { get; set; }
    /// <summary>Gets or sets the taker fee rate.</summary>
    [JsonProperty("taker_fee")]
    public decimal TakerFee { get; set; }
}
