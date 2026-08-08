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
    public string UserId { get; set; }

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the CrossEx position-reduction rank. Values range from 1 to 5; larger values rank higher.
    /// </summary>
    [JsonProperty("crossex_adl_rank")]
    public int? CrossExAdlRank { get; set; }

    /// <summary>
    /// Gets or sets the exchange-native ADL rank. Binance uses 0-4, OKX and Bybit use 0-5, and larger values rank higher.
    /// Gate uses 1-5 and Kraken uses 20, 40, 80, or 100; smaller values rank higher for those venues.
    /// </summary>
    [JsonProperty("exchange_adl_rank")]
    public int? ExchangeAdlRank { get; set; }
}
