namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking coin
/// </summary>
public record GateEarnStakingCoin
{
    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("pid")]
    public long ProductId { get; set; }

    /// <summary>
    /// Project type
    /// </summary>
    [JsonProperty("productType")]
    public int ProductType { get; set; }

    /// <summary>
    /// Whether this is a DeFi protocol
    /// </summary>
    [JsonProperty("isDefi")]
    public int IsDefi { get; set; }

    /// <summary>
    /// Staked currencies
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Estimated yield rate
    /// </summary>
    [JsonProperty("estimateApr")]
    public decimal EstimateApr { get; set; }

    /// <summary>
    /// Minimum staked amount
    /// </summary>
    [JsonProperty("minStakeAmount")]
    public decimal MinStakeAmount { get; set; }

    /// <summary>
    /// Maximum staked amount
    /// </summary>
    [JsonProperty("maxStakeAmount")]
    public decimal MaxStakeAmount { get; set; }

    /// <summary>
    /// Protocol name
    /// </summary>
    [JsonProperty("protocolName")]
    public string ProtocolName { get; set; }

    /// <summary>
    /// Redemption period in days
    /// </summary>
    [JsonProperty("redeemPeriod")]
    public int RedeemPeriod { get; set; }

    /// <summary>
    /// Exchange rate
    /// </summary>
    [JsonProperty("exchangeRate")]
    public decimal ExchangeRate { get; set; }

    /// <summary>
    /// Reverse exchange rate
    /// </summary>
    [JsonProperty("exchangeRateReserve")]
    public decimal ExchangeRateReserve { get; set; }

    /// <summary>
    /// Additional rewards
    /// </summary>
    [JsonProperty("extraInterest")]
    public List<GateEarnStakingExtraInterest> ExtraInterest { get; set; } = [];

    /// <summary>
    /// Reward currency information
    /// </summary>
    [JsonProperty("currencyRewards")]
    public List<GateEarnStakingCurrencyReward> CurrencyRewards { get; set; } = [];
}
