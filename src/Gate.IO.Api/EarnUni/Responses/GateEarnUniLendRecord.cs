namespace Gate.IO.Api.EarnUni;

/// <summary>
/// EarnUni lending record
/// </summary>
public record GateEarnUniLendRecord
{
    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Current amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Previous available amount
    /// </summary>
    [JsonProperty("last_wallet_amount")]
    public decimal LastWalletAmount { get; set; }

    /// <summary>
    /// Previous lent amount
    /// </summary>
    [JsonProperty("last_lent_amount")]
    public decimal LastLentAmount { get; set; }

    /// <summary>
    /// Previous frozen amount
    /// </summary>
    [JsonProperty("last_frozen_amount")]
    public decimal LastFrozenAmount { get; set; }

    /// <summary>
    /// Record type
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(MapConverter))]
    public GateEarnUniLendOperationType Type { get; set; }

    /// <summary>
    /// Created time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
}
