namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx ADL ranking
/// </summary>
public record GateCrossExAdlRank
{
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("crossex_adl_rank")]
    public int? CrossExAdlRank { get; set; }

    [JsonProperty("exchange_adl_rank")]
    public int? ExchangeAdlRank { get; set; }
}
