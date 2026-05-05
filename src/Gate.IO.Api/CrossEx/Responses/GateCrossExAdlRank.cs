namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx ADL ranking
/// </summary>
public record GateCrossExAdlRank
{
    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Cross Ex ADL Rank.
    /// </summary>
    [JsonProperty("crossex_adl_rank")]
    public int? CrossExAdlRank { get; set; }

    /// <summary>
    /// Gets or sets the Exchange ADL Rank.
    /// </summary>
    [JsonProperty("exchange_adl_rank")]
    public int? ExchangeAdlRank { get; set; }
}
