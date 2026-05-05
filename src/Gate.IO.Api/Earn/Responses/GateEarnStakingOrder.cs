namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking order
/// </summary>
public record GateEarnStakingOrder
{
    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("pid")]
    public long ProductId { get; set; }

    /// <summary>
    /// Staked and redeemed currencies
    /// </summary>
    [JsonProperty("coin")]
    public string Coin { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Operation type
    /// </summary>
    [JsonProperty("type")]
    public int Type { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// Redemption credit time
    /// </summary>
    [JsonProperty("redeem_stamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? RedeemTime { get; set; }

    /// <summary>
    /// Order time
    /// </summary>
    [JsonProperty("createStamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Exchange amount
    /// </summary>
    [JsonProperty("exchange_amount")]
    public decimal ExchangeAmount { get; set; }

    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }
}
