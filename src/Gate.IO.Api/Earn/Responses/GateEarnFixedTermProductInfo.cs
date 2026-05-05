namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn product configuration
/// </summary>
public record GateEarnFixedTermProductInfo
{
    /// <summary>
    /// Whether early redemption is supported
    /// </summary>
    [JsonProperty("pre_redeem")]
    public int PreRedeem { get; set; }

    /// <summary>
    /// Whether auto-renewal is supported
    /// </summary>
    [JsonProperty("reinvest")]
    public int Reinvest { get; set; }

    /// <summary>
    /// Redemption payout account type
    /// </summary>
    [JsonProperty("redeem_account")]
    public int RedeemAccount { get; set; }

    /// <summary>
    /// Minimum VIP level requirement
    /// </summary>
    [JsonProperty("min_vip")]
    public int MinVip { get; set; }

    /// <summary>
    /// Maximum VIP level requirement
    /// </summary>
    [JsonProperty("max_vip")]
    public int MaxVip { get; set; }
}
